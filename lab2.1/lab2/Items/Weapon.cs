using lab2.Strategies;
using System.Collections.Generic;

namespace lab2.Items
{
    public class Weapon : Item
    {
        public decimal Damage { get; set; }

        public Weapon(string name, decimal damage, IUpgradeStrategy upgradeStrategy)
            : base(name)
        {
            Damage = damage;
            UpgradeStrategy = upgradeStrategy;
        }

        public override Dictionary<string, object> GetProperties()
        {
            return new Dictionary<string, object>
            {
                ["Damage"] = Damage,
                ["Type"] = "Weapon"
            };
        }
    }
}