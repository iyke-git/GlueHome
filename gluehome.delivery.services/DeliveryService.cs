using System;
using System.Collections.Generic;
using gluehome.delivery.repository;
using gluehome.delivery.repository.models;

namespace gluehome.delivery.services
{
    public class DeliveryService : IDeliveryService
    {
        IDeliveryRepository _deliveryRepo;
        IRecipientRepository _recipientRepo;
        IOrdersRepository _orderRepo;
        IPartnersRepository _partnerRepo;
        public DeliveryService(IDeliveryRepository deliveryRepo, IRecipientRepository recipientRepo,
            IOrdersRepository orderRepo, IPartnersRepository partnersRepo)
        {
            _deliveryRepo = deliveryRepo;
            _recipientRepo = recipientRepo;
            _orderRepo = orderRepo;
            _partnerRepo = partnersRepo;
        }

        public bool ApproveDelivery(Guid deliveryId, AccessWindow accessWindow)
        {
            if (accessWindow.startTime == default(DateTime) || accessWindow.endTime == default(DateTime) || deliveryId == default(Guid))
            {
                return false;
            }
            if (accessWindow.startTime < DateTime.Now && accessWindow.endTime < DateTime.Now || deliveryId == default(Guid))
            {
                return false;
            }
            var delivery = _deliveryRepo.GetById(deliveryId);
            if (delivery?.state == DeliveryStates.approved)
                return true;
            if (delivery?.state == DeliveryStates.created)
            {
                delivery.state = DeliveryStates.approved;
                delivery.accessWindow = accessWindow;
                _deliveryRepo.Update(delivery);
                return true;
            }
            return false;
        }

        public bool CancelDelivery(Guid deliveryId)
        {
            if (deliveryId == default(Guid))
            {
                return false;
            }
            var delivery = _deliveryRepo.GetById(deliveryId);
            if (delivery?.state == DeliveryStates.cancelled)
                return true;
            if (delivery?.state == DeliveryStates.created || delivery?.state == DeliveryStates.approved)
            {
                delivery.state = DeliveryStates.cancelled;
                _deliveryRepo.Update(delivery);
                return true;
            }
            return false;
        }

        public bool CompleteDelivery(Guid deliveryId)
        {
            if (deliveryId == default(Guid))
            {
                return false;
            }
            var delivery = _deliveryRepo.GetById(deliveryId);
            if (delivery?.state == DeliveryStates.completed) return true;
            if (delivery?.state == DeliveryStates.expired || delivery?.state == DeliveryStates.cancelled) return false;
            if (delivery.accessWindow.startTime > DateTime.Now) return false;

            if (delivery.accessWindow.endTime < DateTime.Now)
            {
                delivery.state = DeliveryStates.expired;
                _deliveryRepo.Update(delivery);
                return false;
            }
            if (delivery?.state == DeliveryStates.approved)
            {
                delivery.state = DeliveryStates.completed;
                _deliveryRepo.Update(delivery);
                return true;
            }
            return false;
        }

        public Guid CreateDelivery(Guid recipientId, Guid partnerId, string ordernNumber)
        {
            var recipient = _recipientRepo.GetById(recipientId);
            if (recipient == null) return Guid.Empty;
            var partner = _partnerRepo.GetById(partnerId);
            if (partner == null) return Guid.Empty;
            var order = _orderRepo.GetOrderByNo(ordernNumber);
            if (order == null) return Guid.Empty;
            return _deliveryRepo.Add(new Delivery
            {
                id = Guid.NewGuid(),
                state = DeliveryStates.created,
                recipient = recipient,
                order = order,
                partner = partner
            });
        }

        public IEnumerable<Delivery> GetDeliveriesByOrder(string orderNumber)
        {
            return _deliveryRepo.GetDeliveryByOrder(orderNumber);
        }

        public IEnumerable<Delivery> GetAllDeliveries()
        {
            return _deliveryRepo.GetAll();
        }

        public Delivery GetDeliveryById(Guid id)
        {
            return _deliveryRepo.GetById(id);
        }
    }
}