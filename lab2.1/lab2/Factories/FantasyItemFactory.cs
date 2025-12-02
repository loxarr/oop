using lab2.Items;
using lab2.Strategies;

namespace lab2.Factories
{
    public class FantasyItemFactory : IItemFactory
    {
        public Weapon CreateWeapon(string name, decimal damage)
        {
            return new Weapon($"{name} магии", damage * 1.2m, new WeaponUpgradeStrategy());
        }

        public Armor CreateArmor(string name, decimal defense)
        {
            return new Armor($"{name} защиты", defense * 1.1m, new ArmorUpgradeStrategy());
        }

        public Potion CreatePotion(string name, int healingAmount)
        {
            return new Potion($"{name} зелье", healingAmount + 10, new PotionUpgradeStrategy());
        }

        public QuestItem CreateQuestItem(string name, string description)
        {
            return new QuestItem($"{name}", description);
        }
    }
}