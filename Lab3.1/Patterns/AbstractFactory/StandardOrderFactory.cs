using Lab3._1.Core.Enums;
using Lab3._1.Core.Interfaces;
using Lab3._1.Core.Models;
using Lab3._1.Patterns.Strategy;

namespace Lab3._1.Patterns.AbstractFactory
{
    public class StandardOrderFactory : IOrderFactory
    {
        public IOrder CreateOrder()
        {
            return new BaseOrder(OrderType.Standard, CreateCalculator());
        }

        public IOrderCostCalculator CreateCalculator()
        {
            return new StandardCostCalculator();
        }

        public OrderType GetOrderType() => OrderType.Standard;
    }
}