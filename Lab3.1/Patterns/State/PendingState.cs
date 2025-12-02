using Lab3._1.Core.Enums;
using Lab3._1.Core.Models;
using System;

namespace Lab3._1.Patterns.State
{
    public class PendingState : IOrderState
    {
        public OrderStatus Status => OrderStatus.Pending;

        public void HandleOrderTransition(BaseOrder order, OrderStatus newStatus)
        {
            if (newStatus == OrderStatus.Preparing)
            {
                order.ChangeState(new PreparingState());
            }
            else if (newStatus == OrderStatus.Cancelled)
            {
                order.ChangeState(new CancelledState());
            }
            else
            {
                Console.WriteLine($"! Ошибка: Недопустимый переход из {Status} в {newStatus}.");
            }
        }
    }
}