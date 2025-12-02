using lab2.Items;

namespace lab2.Strategies
{
    public interface IUpgradeStrategy
    {
        string Upgrade(Item item);
    }
}