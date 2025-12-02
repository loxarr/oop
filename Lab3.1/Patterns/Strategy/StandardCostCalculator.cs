using Lab3._1.Core.Interfaces;
using Lab3._1.Core.Models;
using System;

namespace Lab3._1.Patterns.Strategy
{
    public class StandardCostCalculator : IOrderCostCalculator
    {
        private const decimal DeliveryFee = 5.00m;
        private const decimal TaxRate = 0.05m;

        public decimal CalculateTotal(BaseOrder order)
        {
            decimal subtotal = order.GetItemsSubtotal();
            decimal tax = subtotal * TaxRate;
            decimal total = subtotal + tax + DeliveryFee;

            Console.WriteLine($" - Standard calculation: Subtotal={subtotal:C}, Tax={tax:C}, Delivery={DeliveryFee:C}");
            return Math.Round(total, 2);
        }
    }
}