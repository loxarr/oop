using Lab3._1.Core.Interfaces;
using Lab3._1.Core.Models;
using System;

namespace Lab3._1.Patterns.Strategy
{
    public class DiscountCostCalculator : IOrderCostCalculator
    {
        private readonly decimal _discountRate;
        private const decimal DeliveryFee = 5.00m;
        private const decimal TaxRate = 0.05m;

        public DiscountCostCalculator(decimal discountRate)
        {
            _discountRate = discountRate;
        }

        public decimal CalculateTotal(BaseOrder order)
        {
            decimal subtotal = order.GetItemsSubtotal();
            decimal discount = subtotal * _discountRate;
            decimal subtotalAfterDiscount = subtotal - discount;
            
            decimal tax = subtotalAfterDiscount * TaxRate;
            decimal total = subtotalAfterDiscount + tax + DeliveryFee;

            Console.WriteLine($" - Discount calculation: Subtotal={subtotal:C}, Discount={discount:C}, After Discount={subtotalAfterDiscount:C}, Tax={tax:C}, Delivery={DeliveryFee:C}");
            return Math.Round(total, 2);
        }
    }
}