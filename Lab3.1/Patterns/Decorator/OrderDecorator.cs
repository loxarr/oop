using Lab3._1.Core.Enums;
using Lab3._1.Core.Interfaces;
using Lab3._1.Core.Models;
using System.Collections.Generic;

namespace Lab3._1.Patterns.Decorator
{
    public abstract class OrderDecorator : IOrder
    {
        protected IOrder _order;

        public OrderDecorator(IOrder order)
        {
            _order = order;
        }

        public int OrderId => _order.OrderId;
        public OrderType Type => _order.Type;
        public List<OrderItem> Items => _order.Items;
        public OrderStatus Status { get => _order.Status; set => _order.Status = value; } 

        public void AddItem(IMenuItem item, int quantity) => _order.AddItem(item, quantity);
        public void SetStatus(OrderStatus newStatus) => _order.SetStatus(newStatus);

        public abstract string Description { get; }
        public abstract decimal GetCost();
    }
}