using lab2.Strategies;
using System.Collections.Generic;

namespace lab2.Items
{
    public class Potion : Item
    {
        public int HealingAmount { get; set; }

        public Potion(string name, int healingAmount, IUpgradeStrategy upgradeStrategy)
            : base(name)
        {
            HealingAmount = healingAmount;
            UpgradeStrategy = upgradeStrategy;
        }

        public override Dictionary<string, object> GetProperties()
        {
            return new Dictionary<string, object>
            {
                ["Healing"] = HealingAmount,
                ["Type"] = "Potion"
            };
        }
    }
}