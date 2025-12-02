using Lab3._1.Core.Interfaces;
using System;

namespace Lab3._1.Patterns.Decorator
{
    public class PriorityDeliveryDecorator : OrderDecorator
    {
        private const decimal PrioritySurcharge = 15.00m;

        public PriorityDeliveryDecorator(IOrder order) : base(order) { }

        public override string Description => _order.Description + " + Приоритетная доставка (+$15.00).";

        public override decimal GetCost()
        {
            decimal baseCost = _order.GetCost();
            Console.WriteLine($" - Декоратор: Доплата за приоритет {PrioritySurcharge:C}");
            return baseCost + PrioritySurcharge;
        }
    }
}