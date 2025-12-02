using Lab3._1.Core.Interfaces;

namespace Lab3._1.Patterns.Decorator
{
    public class SpecialInstructionDecorator : OrderDecorator
    {
        private readonly string _instruction;

        public SpecialInstructionDecorator(IOrder order, string instruction) : base(order) 
        {
            _instruction = instruction;
        }

        public override string Description => _order.Description + $" + Особые инструкции: '{_instruction}'.";

        public override decimal GetCost()
        {
            return _order.GetCost();
        }
    }
}