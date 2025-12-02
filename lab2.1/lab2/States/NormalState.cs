using lab2.Items;

namespace lab2.States
{
    public class NormalState : IItemState
    {
        public string Use(Item item)
        {
            return $"Использует {item.Name}";
        }

        public string Upgrade(Item item)
        {
            item.State = new UpgradedState();
            return $"{item.Name} улучшено";
        }
    }
}