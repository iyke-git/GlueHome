using System;

namespace gluehome.delivery.repository.models
{
    public class Recipient
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
    }
}