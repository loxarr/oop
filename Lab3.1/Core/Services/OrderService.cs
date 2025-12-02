using Lab3._1.Core.Enums;
using Lab3._1.Core.Interfaces;
using Lab3._1.Core.Models;
using System.Collections.Generic;
using System.Linq;

namespace Lab3._1.Core.Services
{
    public class OrderService
    {
        private readonly List<IOrder> _orders = new List<IOrder>();
        private readonly IOrderFactory _factory;

        public OrderService(IOrderFactory factory)
        {
            _factory = factory;
        }

        public IOrder CreateNewOrder()
        {
            IOrder newOrder = _factory.CreateOrder();
            _orders.Add(newOrder);
            return newOrder;
        }

        public IOrder GetOrderById(int orderId)
        {
            return _orders.FirstOrDefault(o => o.OrderId == orderId);
        }

        public bool UpdateOrderStatus(int orderId, OrderStatus newStatus)
        {
            var order = GetOrderById(orderId);
            if (order is BaseOrder baseOrder)
            {
                baseOrder.SetStatus(newStatus);
                return true;
            }
            return false;
        }

        public IEnumerable<IOrder> GetAllOrders() => _orders;
    }
}