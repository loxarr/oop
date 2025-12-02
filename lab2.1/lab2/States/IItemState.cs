using lab2.Items;

namespace lab2.States
{
    public interface IItemState
    {
        string Use(Item item);
        string Upgrade(Item item);
    }
}