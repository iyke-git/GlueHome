using System;
using System.Collections.Generic;
using gluehome.delivery.repository.models;

namespace gluehome.delivery.repository
{
    public interface IDeliveryRepository:IBaseRepository<Delivery>
    {
        IEnumerable<Delivery> GetDeliveryByState(DeliveryStates state);
        IEnumerable<Delivery> GetDeliveryByRecipient(Guid recipientId);
        IEnumerable<Delivery> GetDeliveryByPartner(Guid partnerId);
        IEnumerable<Delivery> GetDeliveryByOrder(string orderNumber);
    }
}