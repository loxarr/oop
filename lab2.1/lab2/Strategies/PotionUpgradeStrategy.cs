using lab2.Items;

namespace lab2.Strategies
{
    public class PotionUpgradeStrategy : IUpgradeStrategy
    {
        public string Upgrade(Item item)
        {
            var potion = item as Potion;
            potion!.HealingAmount *= 2;
            return $"Исцеление зелья {potion.Name} повысилось до {potion.HealingAmount}";
        }
    }
}