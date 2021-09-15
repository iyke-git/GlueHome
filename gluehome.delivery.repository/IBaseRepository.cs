using System;
using System.Collections.Generic;
using System.Linq;
using gluehome.delivery.repository.models;

namespace gluehome.delivery.repository
{
    public interface IBaseRepository<T>
    {
        T GetById(Guid id);
        IEnumerable<T> GetAll(Func<T, bool> lambda);
        Guid Add(T data);
        void Update(T data);
        void Delete(Guid id);

    }
}