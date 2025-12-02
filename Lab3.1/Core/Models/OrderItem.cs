using Lab3._1.Core.Interfaces;

namespace Lab3._1.Core.Models
{
    public class OrderItem
    {
        public IMenuItem Item { get; }
        public int Quantity { get; }
        public decimal TotalPrice => Item.Price * Quantity;

        public OrderItem(IMenuItem item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }
    }
}