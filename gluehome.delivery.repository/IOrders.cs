using System;
using System.Collections.Generic;
using gluehome.delivery.repository.models;

namespace gluehome.delivery.repository
{
    public interface IOrdersRepository
    {
        Order GetOrderByNo(string orderNumber);
        IEnumerable<Order> GetAll(Func<Order,bool> lambda);
        string AddOrder(Order data);
        void UpdateOrder(Order data);
        void DeleteOrder(string orderNumber);
    }
}