using Lab3._1.Core.Enums;
using Lab3._1.Core.Models;
using System;

namespace Lab3._1.Patterns.State
{
    public class PreparingState : IOrderState
    {
        public OrderStatus Status => OrderStatus.Preparing;

        public void HandleOrderTransition(BaseOrder order, OrderStatus newStatus)
        {
            if (newStatus == OrderStatus.Delivering)
            {
                order.ChangeState(new DeliveringState());
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