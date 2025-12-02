// Файл: Patterns/State/CompletedState.cs (Новый вид)

using Lab3._1.Core.Enums;
using Lab3._1.Core.Models;
using System;

namespace Lab3._1.Patterns.State
{
    public class CompletedState : IOrderState
    {
        public OrderStatus Status => OrderStatus.Completed;

        public void HandleOrderTransition(BaseOrder order, OrderStatus newStatus)
        {
            Console.WriteLine($"! Ошибка: Заказ уже выполнен ({Status}). Переход в {newStatus} невозможен.");
        }
    }
}