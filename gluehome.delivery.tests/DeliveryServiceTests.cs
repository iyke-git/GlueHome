using System;
using System.Linq;
using System.Collections.Generic;
using gluehome.delivery.services;
using gluehome.delivery.repository;
using Xunit;
using gluehome.delivery.repository.models;

namespace gluehome.delivery.tests
{
    public class DeliveryServiceTests
    {
        private IDeliveryService _deliverySvc;
        private IOrdersRepository _orderRepo;
        private IRecipientRepository _recipientRepo;
        private IPartnersRepository _partnerRepo;

        public DeliveryServiceTests()
        {
            _orderRepo = new OrdersRepository();
            _recipientRepo = new RecipientRepository();
            _partnerRepo = new PartnerRepository();
            _deliverySvc = new DeliveryService(new DeliveryRepository(), _recipientRepo, _orderRepo, _partnerRepo);
        }

        [Fact]
        public void IfRecipientDoesNotExistCreateDeliveryReturnsEmptyGuid()
        {
            var partnerId = _partnerRepo.GetAll().ToList().First().id;
            var orderNumber = _orderRepo.GetAll().ToList().First().orderNumber;
            var id = _deliverySvc.CreateDelivery(Guid.NewGuid(), partnerId, orderNumber);
            Assert.Equal(id, Guid.Empty);
        }

        [Fact]
        public void IfOrderDoesNotExistCreateDeliveryReturnsEmptyGuid()
        {
            var partnerId = _partnerRepo.GetAll().ToList().First().id;
            var recipientId = _recipientRepo.GetAll().ToList().First().id;
            var id = _deliverySvc.CreateDelivery(recipientId, partnerId, "orderNumber");
            Assert.Equal(id, Guid.Empty);
        }

        [Fact]
        public void IfPartnerDoesNotExistCreateDeliveryReturnsEmptyGuid()
        {
            var orderNumber = _orderRepo.GetAll().ToList().First().orderNumber;
            var recipientId = _recipientRepo.GetAll().ToList().First().id;
            var id = _deliverySvc.CreateDelivery(recipientId, Guid.NewGuid(), orderNumber);
            Assert.Equal(id, Guid.Empty);
        }

        [Fact]
        public void IfAllParametersExistCreateDeliveryReturnsNonEmptyGuid()
        {
            var orderNumber = _orderRepo.GetAll().ToList().First().orderNumber;
            var partnerId = _partnerRepo.GetAll().ToList().First().id;
            var recipientId = _recipientRepo.GetAll().ToList().First().id;
            var id = _deliverySvc.CreateDelivery(recipientId, partnerId, orderNumber);
            Assert.NotEqual(id, Guid.Empty);
        }

        [Fact]
        public void CreatedDeliveryIsInCreatedState()
        {
            var orderNumber = _orderRepo.GetAll().ToList().First().orderNumber;
            var partnerId = _partnerRepo.GetAll().ToList().First().id;
            var recipientId = _recipientRepo.GetAll().ToList().First().id;
            var id = _deliverySvc.CreateDelivery(recipientId, partnerId, orderNumber);
            var delivery = _deliverySvc.GetDeliveryById(id);
            Assert.Equal(DeliveryStates.created, delivery.state);
        }

        [Fact]
        public void IfAccessWindowIsIncorrectApproveDeliveryReturnsFalse()
        {
            var orderNumber = _orderRepo.GetAll().ToList().First().orderNumber;
            var partnerId = _partnerRepo.GetAll().ToList().First().id;
            var recipientId = _recipientRepo.GetAll().ToList().First().id;
            var id = _deliverySvc.CreateDelivery(recipientId, partnerId, orderNumber);
            var window = new AccessWindow();
            Assert.False(_deliverySvc.ApproveDelivery(id, window));
        }

        [Fact]
        public void IfAccessWindowIsInThePastApproveDeliveryReturnsFalse()
        {
            var orderNumber = _orderRepo.GetAll().ToList().First().orderNumber;
            var partnerId = _partnerRepo.GetAll().ToList().First().id;
            var recipientId = _recipientRepo.GetAll().ToList().First().id;
            var id = _deliverySvc.CreateDelivery(recipientId, partnerId, orderNumber);
            var window = new AccessWindow
            {
                endTime = DateTime.Now.AddDays(-1),
                startTime = DateTime.Now.AddDays(-2)
            };
            Assert.False(_deliverySvc.ApproveDelivery(id, window));
        }

        [Fact]
        public void IfAccessWindowIsCorrectApproveDeliveryReturnsTrue()
        {
            var orderNumber = _orderRepo.GetAll().ToList().First().orderNumber;
            var partnerId = _partnerRepo.GetAll().ToList().First().id;
            var recipientId = _recipientRepo.GetAll().ToList().First().id;
            var id = _deliverySvc.CreateDelivery(recipientId, partnerId, orderNumber);
            var window = new AccessWindow
            {
                endTime = DateTime.Now,
                startTime = DateTime.Now.AddDays(1)
            };
            Assert.True(_deliverySvc.ApproveDelivery(id, window));
            var delivery = _deliverySvc.GetDeliveryById(id);
            Assert.Equal(DeliveryStates.approved, delivery.state);
        }

        [Fact]
        public void IfDeliveryIsNotApprovedCompleteDeliveryReturnsFalse()
        {
            var orderNumber = _orderRepo.GetAll().ToList().Last().orderNumber;
            var partnerId = _partnerRepo.GetAll().ToList().Last().id;
            var recipientId = _recipientRepo.GetAll().ToList().Last().id;
            var id = _deliverySvc.CreateDelivery(recipientId, partnerId, orderNumber);
            Assert.False(_deliverySvc.CompleteDelivery(id));
        }

        [Fact]
        public void IfDeliveryIsCancelledCompleteDeliveryReturnsFalse()
        {
            var orderNumber = _orderRepo.GetAll().ToList().First().orderNumber;
            var partnerId = _partnerRepo.GetAll().ToList().First().id;
            var recipientId = _recipientRepo.GetAll().ToList().First().id;
            var id = _deliverySvc.CreateDelivery(recipientId, partnerId, orderNumber);
            var window = new AccessWindow
            {
                endTime = DateTime.Now,
                startTime = DateTime.Now.AddDays(1)
            };
            _deliverySvc.ApproveDelivery(id,window);
            _deliverySvc.CancelDelivery(id);
            Assert.False(_deliverySvc.CompleteDelivery(id));
        }
    }
}
