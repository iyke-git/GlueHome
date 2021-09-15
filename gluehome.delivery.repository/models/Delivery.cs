using System;

namespace gluehome.delivery.repository.models
{
    public class Delivery
    {
        public Guid id { get; set; }
        public DeliveryStates state { get; set; }
        public AccessWindow accessWindow { get; set; }
        public Recipient recipient { get; set; }
        public Order order { get; set; }
        public Partner partner { get; set; }
    }

    public enum DeliveryStates
    {
        created = 0,
        approved = 1,
        completed = 2,
        cancelled = 3,
        expired = 4
    }
}