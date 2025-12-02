using lab2.Items;

namespace lab2.Strategies
{
    public class WeaponUpgradeStrategy : IUpgradeStrategy
    {
        public string Upgrade(Item item)
        {
            var weapon = item as Weapon;
            weapon!.Damage *= 1.5m;
            return $"Оружие {weapon.Name} повысило урон до {weapon.Damage}";
        }
    }
}