using System;
using System.Collections.Generic;
using System.Linq;
using gluehome.delivery.repository.models;

namespace gluehome.delivery.repository
{
    public class RecipientRepository : IRecipientRepository
    {

        public Recipient GetById(Guid id)
        {
            return Database.Recipients.FirstOrDefault(x => x.id == id);
        }

        public IEnumerable<Recipient> GetAll(Func<Recipient, bool> lambda = null)
        {
            if (lambda == null) return Database.Recipients;
            return Database.Recipients.Where(lambda);
        }

        public Guid Add(Recipient data)
        {
            data.id = Guid.NewGuid();
            Database.Recipients.Add(data);
            return data.id;
        }

        public void Update(Recipient data)
        {
            if (data.id == Guid.Empty)
            {
                throw new ArgumentException("Please specify the id of the record you are updating");
            }
            Database.Recipients.RemoveAll(x => x.id == data.id);
            Database.Recipients.Add(data);
        }

        public void Delete(Guid id)
        {
            Database.Recipients.RemoveAll(x => x.id == id);
        }

        public IEnumerable<Recipient> GetRecipientByEmail(string email)
        {
            return Database.Recipients.Where(x => x.email == email);
        }

        public IEnumerable<Recipient> GetRecipientByPhone(string phone)
        {
            return Database.Recipients.Where(x => x.phoneNumber == phone);
        }
    }
}