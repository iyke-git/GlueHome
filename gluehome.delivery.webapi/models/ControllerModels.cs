using System;

namespace gluehome.delivery.webapi.models
{

    public class DeliveryCreation
    {
        public Guid recipientId { get; set; }
        public Guid partnerId { get; set; }
        public string orderNumber { get; set; }
    }

}