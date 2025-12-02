using Lab3._1.Core.Enums;
using Lab3._1.Core.Models;
using System;

namespace Lab3._1.Patterns.State
{
    public class DeliveringState : IOrderState
    {
        public OrderStatus Status => OrderStatus.Delivering;

        public void HandleOrderTransition(BaseOrder order, OrderStatus newStatus)
        {
            if (newStatus == OrderStatus.Completed)
            {
                order.ChangeState(new CompletedState());
            }
            else
            {
                Console.WriteLine($"! Ошибка: Недопустимый переход из {Status} в {newStatus}.");
            }
        }
    }
}