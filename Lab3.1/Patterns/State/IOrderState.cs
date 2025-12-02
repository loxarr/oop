using Lab3._1.Core.Enums;
using Lab3._1.Core.Models;

namespace Lab3._1.Patterns.State
{
    public interface IOrderState
    {
        OrderStatus Status { get; }
        void HandleOrderTransition(BaseOrder order, OrderStatus newStatus);
    }
}