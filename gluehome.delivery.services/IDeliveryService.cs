using System;
using System.Collections.Generic;
using gluehome.delivery.repository.models;

namespace gluehome.delivery.services
{
    public interface IDeliveryService
    {
        bool ApproveDelivery(Guid deliveryId, AccessWindow accessWindow);
        Guid CreateDelivery(Guid recipientId, Guid partnerId, string ordernNumber);
        bool CompleteDelivery(Guid deliveryId);
        bool CancelDelivery(Guid deliveryId);
        IEnumerable<Delivery> GetDeliveriesByOrder(string orderNumber);
        IEnumerable<Delivery> GetAllDeliveries();
        Delivery GetDeliveryById(Guid id);
    }
}