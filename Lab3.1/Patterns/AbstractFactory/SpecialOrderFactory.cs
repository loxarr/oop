using Lab3._1.Core.Enums;
using Lab3._1.Core.Interfaces;
using Lab3._1.Core.Models;
using Lab3._1.Patterns.Strategy;

namespace Lab3._1.Patterns.AbstractFactory
{
    public class SpecialOrderFactory : IOrderFactory
    {
        public IOrder CreateOrder()
        {
            return new BaseOrder(OrderType.Special, CreateCalculator());
        }

        public IOrderCostCalculator CreateCalculator()
        {
            return new DiscountCostCalculator(0.10m);
        }

        public OrderType GetOrderType() => OrderType.Special;
    }
}