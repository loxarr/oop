using Lab3._1.Core.Interfaces;
using Lab3._1.Core.Models;
using Lab3._1.Patterns.AbstractFactory;
using NUnit.Framework; 
using static NUnit.Framework.Assert;

namespace Lab3._1.Tests.PatternTests
{
    [TestFixture]
    public class StrategyTests
    {
        private IOrder _baseOrder;
        private BaseOrder _baseBaseOrder; 

        [SetUp]
        public void Setup()
        {
            IOrderFactory factory = new StandardOrderFactory();
            _baseOrder = factory.CreateOrder();
            _baseOrder.AddItem(new Dish("Пицца", 12.50m), 2);
            _baseOrder.AddItem(new Dish("Кола", 2.00m), 3);
            _baseBaseOrder = (BaseOrder)_baseOrder;
        }

        [Test]
        public void StandardCalculator_CalculatesCorrectTotal()
        {
            decimal totalCost = _baseOrder.GetCost();

            Assert.AreEqual(37.55m, totalCost);
        }

        [Test]
        public void DiscountCalculator_AppliesDiscountAndCalculatesCorrectTotal()
        {

            _baseBaseOrder.Calculator = new Patterns.Strategy.DiscountCostCalculator(0.10m);

            decimal totalCost = _baseBaseOrder.GetCost();

            Assert.AreEqual(34.30m, totalCost);
        }
    }
}