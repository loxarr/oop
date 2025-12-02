// Файл: Patterns/State/CancelledState.cs (Новый файл)

using Lab3._1.Core.Enums;
using Lab3._1.Core.Models;
using System;

namespace Lab3._1.Patterns.State
{
    public class CancelledState : IOrderState
    {
        public OrderStatus Status => OrderStatus.Cancelled;

        public void HandleOrderTransition(BaseOrder order, OrderStatus newStatus)
        {
            Console.WriteLine($"! Ошибка: Отмененный заказ не может изменить статус.");
        }
    }
}