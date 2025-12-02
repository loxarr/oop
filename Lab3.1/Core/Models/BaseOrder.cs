using Lab3._1.Core.Enums;
using Lab3._1.Core.Interfaces;
using Lab3._1.Patterns.State;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3._1.Core.Models
{
    public class BaseOrder : IOrder
    {
        private static int _nextId = 1000;
        public int OrderId { get; }
        public OrderType Type { get; }
        public List<OrderItem> Items { get; } = new List<OrderItem>();
        public IOrderCostCalculator Calculator { get; set; } 

        public string Description { get; protected set; } 

        private IOrderState _currentState;
        public OrderStatus Status 
        { 
            get => _currentState.Status;
            set => throw new InvalidOperationException("Use SetStatus method to transition states.");
        }

        public BaseOrder(OrderType type, IOrderCostCalculator calculator)
        {
            OrderId = _nextId++;
            Type = type;
            Calculator = calculator;
            Description = $"Базовый заказ типа {type}.";
            _currentState = new PendingState();
        }

        public decimal GetCost()
        {
            if (Calculator == null) throw new InvalidOperationException("Калькулятор стоимости не установлен.");
            return Calculator.CalculateTotal(this);
        }

        public void AddItem(IMenuItem item, int quantity)
        {
            if (Status != OrderStatus.Pending && Status != OrderStatus.Preparing)
            {
                Console.WriteLine($"Нельзя добавлять позиции в заказ со статусом {Status}.");
                return;
            }
            Items.Add(new OrderItem(item, quantity));
        }

        public void SetStatus(OrderStatus newStatus)
        {
            _currentState.HandleOrderTransition(this, newStatus);
        }

        public void ChangeState(IOrderState newState)
        {
            Console.WriteLine($"Заказ #{OrderId}: Статус изменен с {_currentState.Status} на {newState.Status}.");
            _currentState = newState;
        }

        public decimal GetItemsSubtotal() => Items.Sum(i => i.TotalPrice);
    }
}