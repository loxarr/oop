using Lab3._1.Core.Enums;
using Lab3._1.Core.Interfaces;
using Lab3._1.Core.Models;
using Lab3._1.Patterns.AbstractFactory;
using Lab3._1.Patterns.Strategy;
using NUnit.Framework;
using static NUnit.Framework.Assert; 


namespace Lab3._1.Tests.PatternTests
{
    [TestFixture]
    public class AbstractFactoryTests
    {
        [Test]
        public void StandardFactory_CreatesStandardOrderAndCalculator()
        {
            IOrderFactory factory = new StandardOrderFactory();

            IOrder order = factory.CreateOrder();
            IOrderCostCalculator calculator = factory.CreateCalculator();

            Assert.IsInstanceOf<BaseOrder>(order);
            Assert.AreEqual(OrderType.Standard, order.Type);
            Assert.IsInstanceOf<StandardCostCalculator>(calculator);
            
            Assert.IsInstanceOf<StandardCostCalculator>(((BaseOrder)order).Calculator);
        }

        [Test]
        public void SpecialFactory_CreatesSpecialOrderAndDiscountCalculator()
        {
            IOrderFactory factory = new SpecialOrderFactory();

            IOrder order = factory.CreateOrder();
            IOrderCostCalculator calculator = factory.CreateCalculator();

            Assert.IsInstanceOf<BaseOrder>(order);
            Assert.AreEqual(OrderType.Special, order.Type);
            Assert.IsInstanceOf<DiscountCostCalculator>(calculator);
            
            Assert.IsInstanceOf<DiscountCostCalculator>(((BaseOrder)order).Calculator);
        }
    }
}