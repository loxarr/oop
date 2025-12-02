using Lab3._1.Core.Models;

namespace Lab3._1.Core.Interfaces
{
    public interface IOrderCostCalculator
    {
        decimal CalculateTotal(BaseOrder order);
    }
}
