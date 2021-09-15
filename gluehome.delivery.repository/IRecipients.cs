using System.Collections.Generic;
using gluehome.delivery.repository.models;

namespace gluehome.delivery.repository
{
    public interface IRecipientRepository:IBaseRepository<Recipient>
    {
        IEnumerable<Recipient> GetRecipientByEmail(string email);
        IEnumerable<Recipient> GetRecipientByPhone(string phone);
    }
}