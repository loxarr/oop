using Lab3._1.Core.Enums;

namespace Lab3._1.Core.Interfaces
{
    public interface IOrderFactory
    {
        IOrder CreateOrder();
        IOrderCostCalculator CreateCalculator();
        OrderType GetOrderType();
    }
}