using lab2.Items;

namespace lab2.Factories
{
    public interface IItemFactory
    {
        Weapon CreateWeapon(string name, decimal damage);
        Armor CreateArmor(string name, decimal defense);
        Potion CreatePotion(string name, int healingAmount);
        QuestItem CreateQuestItem(string name, string description);
    }
}