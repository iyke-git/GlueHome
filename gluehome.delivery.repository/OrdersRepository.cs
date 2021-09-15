using System;
using System.Collections.Generic;
using System.Linq;
using gluehome.delivery.repository.models;

namespace gluehome.delivery.repository
{
    public class OrdersRepository : IOrdersRepository
    {
        public Order GetOrderByNo(string orderNumber)
        {
            return Database.Orders.FirstOrDefault(x => x.orderNumber == orderNumber);
        }

        public IEnumerable<Order> GetAll(Func<Order, bool> lambda)
        {
            return Database.Orders.Where(lambda);
        }

        public string AddOrder(Order data)
        {
            data.orderNumber = DateTime.Now.ToString("MMddhhmmss");
            Database.Orders.Add(data);
            return data.orderNumber;
        }

        public void UpdateOrder(Order data)
        {
            if (string.IsNullOrEmpty(data.orderNumber))
            {
                throw new ArgumentException("Please specify the order numer of the order you are updating");
            }
            Database.Orders.RemoveAll(x => x.orderNumber == data.orderNumber);
            Database.Orders.Add(data);
        }

        public void DeleteOrder(string orderNumber)
        {
            Database.Orders.RemoveAll(x => x.orderNumber == orderNumber);
        }
    }
}