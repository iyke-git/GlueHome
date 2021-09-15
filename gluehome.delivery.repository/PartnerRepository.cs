using System;
using System.Collections.Generic;
using System.Linq;
using gluehome.delivery.repository.models;

namespace gluehome.delivery.repository
{
    public class PartnerRepository : IPartnersRepository
    {

        public Partner GetById(Guid id)
        {
            return Database.Partners.FirstOrDefault(x => x.id == id);
        }

        public IEnumerable<Partner> GetAll(Func<Partner, bool> lambda = null)
        {
            if (lambda == null) return Database.Partners;
            return Database.Partners.Where(lambda);
        }

        public Guid Add(Partner data)
        {
            data.id = Guid.NewGuid();
            Database.Partners.Add(data);
            return data.id;
        }

        public void Update(Partner data)
        {
            if (data.id == Guid.Empty)
            {
                throw new ArgumentException("Please specify the id of the record you are updating");
            }
            Database.Partners.RemoveAll(x => x.id == data.id);
            Database.Partners.Add(data);
        }

        public void Delete(Guid id)
        {
            Database.Partners.RemoveAll(x => x.id == id);
        }

        public IEnumerable<Partner> GetPartnersByName(string name)
        {
            return Database.Partners.Where(x => x.name.Contains(name));
        }

        public IEnumerable<Partner> GetPartnersByArea(string area)
        {
            return Database.Partners.Where(x => x.areas.Any(y => y.Equals(area)));
        }
    }
}