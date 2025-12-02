using lab2.Items;

namespace lab2.Strategies
{
    public interface UpgradeStrategy
    {
        string Upgrade(Item item);
    }
}