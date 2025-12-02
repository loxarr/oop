using Lab3._1.Core.Enums;
using Lab3._1.Core.Interfaces;
using Lab3._1.Core.Models;
using Lab3._1.Patterns.AbstractFactory;
using NUnit.Framework;
using static NUnit.Framework.Assert; 


namespace Lab3._1.Tests.PatternTests
{
    [TestFixture]
    public class StateTests
    {
        private BaseOrder _order;

        [SetUp]
        public void Setup()
        {
            IOrderFactory factory = new StandardOrderFactory();
            _order = (BaseOrder)factory.CreateOrder();
        }

        [Test]
        public void InitialState_IsPending()
        {
            Assert.AreEqual(OrderStatus.Pending, _order.Status);
        }

        [Test]
        public void PendingState_CanTransitionToPreparingAndCancelled()
        {
            _order.SetStatus(OrderStatus.Preparing);
            Assert.AreEqual(OrderStatus.Preparing, _order.Status);

            _order.SetStatus(OrderStatus.Cancelled);
            Assert.AreEqual(OrderStatus.Cancelled, _order.Status);
        }

        [Test]
        public void PreparingState_CanTransitionToDeliveringAndCancelled()
        {
            _order.SetStatus(OrderStatus.Preparing);

            _order.SetStatus(OrderStatus.Delivering);
            Assert.AreEqual(OrderStatus.Delivering, _order.Status);
        }

        [Test]
        public void DeliveringState_CanOnlyTransitionToCompleted()
        {
            _order.SetStatus(OrderStatus.Preparing);
            _order.SetStatus(OrderStatus.Delivering);

            _order.SetStatus(OrderStatus.Completed);
            Assert.AreEqual(OrderStatus.Completed, _order.Status);
        }
        
        [Test]
        public void CompletedState_CannotTransitionToAnyOtherState()
        {
            _order.SetStatus(OrderStatus.Preparing);
            _order.SetStatus(OrderStatus.Delivering);
            _order.SetStatus(OrderStatus.Completed);

            _order.SetStatus(OrderStatus.Preparing);
            
            Assert.AreEqual(OrderStatus.Completed, _order.Status); 
        }

        [Test]
        public void InvalidTransition_FromPendingToDelivering_IsBlocked()
        {
            _order.SetStatus(OrderStatus.Delivering);
            
            Assert.AreEqual(OrderStatus.Pending, _order.Status); 
        }
    }
}