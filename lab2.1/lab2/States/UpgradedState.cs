using lab2.Items;

namespace lab2.States
{
    public class UpgradedState : IItemState
    {
        public string Use(Item item)
        {
            return $"Улучшилось {item.Name}";
        }

        public string Upgrade(Item item)
        {
            return $"{item.Name} уже улучшен";
        }
    }
}