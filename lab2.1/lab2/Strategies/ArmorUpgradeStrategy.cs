using lab2.Items;

namespace lab2.Strategies
{
    public class ArmorUpgradeStrategy : IUpgradeStrategy
    {
        public string Upgrade(Item item)
        {
            var armor = item as Armor;
            armor!.Defense *= 1.3m;
            return $"Защита брони {armor.Name} повысилось до {armor.Defense}";
        }
    }
}