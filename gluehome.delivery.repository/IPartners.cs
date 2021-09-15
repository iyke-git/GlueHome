using System.Collections.Generic;
using gluehome.delivery.repository.models;

namespace gluehome.delivery.repository
{
    public interface IPartnersRepository:IBaseRepository<Partner>
    {
        IEnumerable<Partner> GetPartnersByName(string name);
        IEnumerable<Partner> GetPartnersByArea(string name);
    }
}