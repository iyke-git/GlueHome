using System;
using System.Collections.Generic;
using System.Linq;
using gluehome.delivery.repository.models;

namespace gluehome.delivery.repository
{
    public class DeliveryRepository : IDeliveryRepository
    {

        public Delivery GetById(Guid id)
        {
            return Database.Deliveries.FirstOrDefault(x => x.id == id);
        }

        public IEnumerable<Delivery> GetAll(Func<Delivery, bool> lambda = null)
        {
            if (lambda == null) return Database.Deliveries;
            return Database.Deliveries.Where(lambda);
        }

        public Guid Add(Delivery data)
        {
            data.id = Guid.NewGuid();
            Database.Deliveries.Add(data);
            return data.id;
        }

        public void Update(Delivery data)
        {
            if (data.id == Guid.Empty)
            {
                throw new ArgumentException("Please specify the id of the record you are updating");
            }
            Database.Deliveries.RemoveAll(x => x.id == data.id);
            Database.Deliveries.Add(data);
        }

        public void Delete(Guid id)
        {
            Database.Deliveries.RemoveAll(x => x.id == id);
        }

        public IEnumerable<Delivery> GetDeliveryByState(DeliveryStates state)
        {
            return Database.Deliveries.Where(x => x.state == state);
        }

        public IEnumerable<Delivery> GetDeliveryByRecipient(Guid recipientId)
        {
            return Database.Deliveries.Where(x => x.recipient?.id == recipientId);
        }

        public IEnumerable<Delivery> GetDeliveryByPartner(Guid partnerId)
        {
            return Database.Deliveries.Where(x => x.partner?.id == partnerId);
        }

        public IEnumerable<Delivery> GetDeliveryByOrder(string orderNumber)
        {
            return Database.Deliveries.Where(x => x.order?.orderNumber == orderNumber);
        }
    }
}