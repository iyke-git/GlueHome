using System;
using System.Collections.Generic;

namespace gluehome.delivery.repository.models
{
    public class Partner
    {
        public Guid id { get; set; }
        public string name { get; set; }
        public IEnumerable<string> areas { get; set; }
    }
}