using lab2.Strategies;
using System.Collections.Generic;

namespace lab2.Items
{
    public class Armor : Item
    {
        public decimal Defense { get; set; }

        public Armor(string name, decimal defense, IUpgradeStrategy upgradeStrategy)
            : base(name)
        {
            Defense = defense;
            UpgradeStrategy = upgradeStrategy;
        }

        public override Dictionary<string, object> GetProperties()
        {
            return new Dictionary<string, object>
            {
                ["Defense"] = Defense,
                ["Type"] = "Armor"
            };
        }
    }
}