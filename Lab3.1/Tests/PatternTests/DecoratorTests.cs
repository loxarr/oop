using Lab3._1.Core.Interfaces;
using Lab3._1.Core.Models;
using Lab3._1.Patterns.AbstractFactory;
using Lab3._1.Patterns.Decorator;
using NUnit.Framework;
using static NUnit.Framework.Assert; 
using static NUnit.Framework.StringAssert;

namespace Lab3._1.Tests.PatternTests
{
    [TestFixture]
    public class DecoratorTests
    {
        private IOrder _baseOrder;
        private const decimal BaseCost = 37.55m; 
        [SetUp]
        public void Setup()
        {
            IOrderFactory factory = new StandardOrderFactory();
            _baseOrder = factory.CreateOrder();
            _baseOrder.AddItem(new Dish("Пицца", 12.50m), 2);
            _baseOrder.AddItem(new Dish("Кола", 2.00m), 3);
            Assert.AreEqual(BaseCost, _baseOrder.GetCost());
        }

        [Test]
        public void PriorityDeliveryDecorator_AddsSurchargeToCost()
        {
            IOrder decoratedOrder = new PriorityDeliveryDecorator(_baseOrder);
            decimal expectedCost = BaseCost + 15.00m;

            decimal finalCost = decoratedOrder.GetCost();

            Assert.AreEqual(expectedCost, finalCost);
            StringAssert.Contains("Приоритетная доставка", decoratedOrder.Description);
        }

        [Test]
        public void SpecialInstructionDecorator_DoesNotChangeCost()
        {
            IOrder decoratedOrder = new SpecialInstructionDecorator(_baseOrder, "No onions");

            decimal finalCost = decoratedOrder.GetCost();

            Assert.AreEqual(BaseCost, finalCost);
            StringAssert.Contains("Особые инструкции: 'No onions'", decoratedOrder.Description);
        }

        [Test]
        public void MultipleDecorators_CombineFunctionality()
        {
            IOrder decoratedOrder = new PriorityDeliveryDecorator(_baseOrder);
            decoratedOrder = new SpecialInstructionDecorator(decoratedOrder, "Extra cheese");

            decimal finalCost = decoratedOrder.GetCost();
            decimal expectedCost = BaseCost + 15.00m;

            Assert.AreEqual(expectedCost, finalCost);
            StringAssert.Contains("Приоритетная доставка", decoratedOrder.Description);
            StringAssert.Contains("Extra cheese", decoratedOrder.Description);
        }
    }
}