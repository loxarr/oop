using Lab3._1.Core.Enums;
using Lab3._1.Core.Interfaces;
using Lab3._1.Core.Services;
using Lab3._1.Patterns.AbstractFactory;
using NUnit.Framework;
using System.Linq;
using static NUnit.Framework.Assert; 

namespace Lab3._1.Tests.ServiceTests
{
    [TestFixture]
    public class OrderServiceTests
    {
        private OrderService _standardService;
        private IOrderFactory _standardFactory;

        [SetUp]
        public void Setup()
        {
            _standardFactory = new StandardOrderFactory();
            _standardService = new OrderService(_standardFactory);
        }

        [Test]
        public void CreateNewOrder_ShouldReturnOrderAndAddToCollection()
        {
            IOrder order = _standardService.CreateNewOrder();
            
            Assert.IsNotNull(order);
            Assert.AreEqual(1, _standardService.GetAllOrders().Count());
            Assert.AreEqual(OrderType.Standard, order.Type);
        }

        [Test]
        public void GetOrderById_ShouldRetrieveCorrectOrder()
        {
            IOrder order1 = _standardService.CreateNewOrder();
            IOrder order2 = _standardService.CreateNewOrder();
            int idToFind = order1.OrderId;

            IOrder foundOrder = _standardService.GetOrderById(idToFind);
            
            Assert.IsNotNull(foundOrder);
            Assert.AreEqual(idToFind, foundOrder.OrderId);
            Assert.AreNotEqual(order2.OrderId, foundOrder.OrderId);
        }

        [Test]
        public void UpdateOrderStatus_ShouldChangeStatus_WhenTransitionIsValid()
        {
            IOrder order = _standardService.CreateNewOrder();

            bool success = _standardService.UpdateOrderStatus(order.OrderId, OrderStatus.Preparing);

            Assert.IsTrue(success);
            Assert.AreEqual(OrderStatus.Preparing, order.Status);
        }

        [Test]
        public void UpdateOrderStatus_ShouldNotChangeStatus_WhenTransitionIsInvalid()
        {
            IOrder order = _standardService.CreateNewOrder(); 

            _standardService.UpdateOrderStatus(order.OrderId, OrderStatus.Delivering);

            Assert.AreEqual(OrderStatus.Pending, order.Status);
        }
    }
}