using lab2.Factories;
using lab2.Items;

namespace lab2.Builders
{
    public class ItemBuilder
    {
        private readonly IItemFactory _factory;

        public ItemBuilder(IItemFactory factory)
        {
            _factory = factory;
        }

        public Weapon BuildWeapon(string name, decimal damage)
        {
            return _factory.CreateWeapon(name, damage);
        }

        public Armor BuildArmor(string name, decimal defense)
        {
            return _factory.CreateArmor(name, defense);
        }

        public Potion BuildPotion(string name, int healingAmount)
        {
            return _factory.CreatePotion(name, healingAmount);
        }

        public QuestItem BuildQuestItem(string name, string description)
        {
            return _factory.CreateQuestItem(name, description);
        }
    }
}