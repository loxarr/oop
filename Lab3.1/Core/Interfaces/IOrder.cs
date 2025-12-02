using Lab3._1.Core.Enums;
using Lab3._1.Core.Models;
using System.Collections.Generic;

namespace Lab3._1.Core.Interfaces
{
    public interface IOrder
    {
        int OrderId { get; }
        OrderType Type { get; }
        OrderStatus Status { get; set; }
        List<OrderItem> Items { get; }
        string Description { get; } 

        decimal GetCost();
        void AddItem(IMenuItem item, int quantity);
        void SetStatus(OrderStatus newStatus);
    }
}