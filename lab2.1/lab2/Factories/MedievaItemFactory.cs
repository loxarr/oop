using lab2.Items;
using lab2.Strategies;

namespace lab2.Factories
{
    public class MedievalItemFactory : IItemFactory
    {
        public Weapon CreateWeapon(string name, decimal damage)
        {
            return new Weapon(name, damage, new WeaponUpgradeStrategy());
        }

        public Armor CreateArmor(string name, decimal defense)
        {
            return new Armor(name, defense, new ArmorUpgradeStrategy());
        }

        public Potion CreatePotion(string name, int healingAmount)
        {
            return new Potion(name, healingAmount, new PotionUpgradeStrategy());
        }

        public QuestItem CreateQuestItem(string name, string description)
        {
            return new QuestItem(name, description);
        }
    }
}