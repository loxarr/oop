using lab2;
using lab2.Builders;
using lab2.Factories;
using lab2.Items;
using lab2.States;
using lab2.Strategies;
using System;
using Xunit;

namespace lab2.Tests
{
    public class InventorySystemTests
    {
        [Fact]
        public void WeaponCreation_WithMedievalFactory_ShouldCreateCorrectWeapon()
        {
            var factory = new MedievalItemFactory();
            var builder = new ItemBuilder(factory);

            var weapon = builder.BuildWeapon("Меч", 15.0m);

            Assert.Equal("Меч", weapon.Name);
            Assert.Equal(15.0m, weapon.Damage);
            Assert.IsType<WeaponUpgradeStrategy>(weapon.UpgradeStrategy);
        }

        [Fact]
        public void ArmorEquip_WhenUsed_ShouldBeEquipped()
        {
            var factory = new MedievalItemFactory();
            var builder = new ItemBuilder(factory);
            var inventory = new Inventory();

            var armor = builder.BuildArmor("Щит", 10.0m);
            inventory.AddItem(armor);
            inventory.UseItem("Щит");

            Assert.Equal(armor, inventory.Equipped[typeof(Armor)]);
        }

        [Fact]
        public void UpgradeWeapon_ShouldIncreaseDamageAndChangeState()
        {
            var factory = new MedievalItemFactory();
            var builder = new ItemBuilder(factory);
            var inventory = new Inventory();

            var weapon = builder.BuildWeapon("Топор", 20.0m);
            inventory.AddItem(weapon);
            var upgradeResult = inventory.UpgradeItem("Топор");

            Assert.Equal(30.0m, weapon.Damage);
            Assert.IsType<UpgradedState>(weapon.State);
            Assert.Contains("повысило урон до 30", upgradeResult);
        }

        [Fact]
        public void QuestItem_ShouldNotBeUpgradable()
        {
            var factory = new MedievalItemFactory();
            var builder = new ItemBuilder(factory);
            var inventory = new Inventory();

            var questItem = builder.BuildQuestItem("Ключ", "Открывает древние двери");
            inventory.AddItem(questItem);
            var upgradeResult = inventory.UpgradeItem("Ключ");

            Assert.Equal("Этот элемент не может быть улучшен", upgradeResult);
        }

        [Fact]
        public void FantasyFactory_ShouldCreateEnhancedItems()
        {
            var factory = new FantasyItemFactory();
            var builder = new ItemBuilder(factory);

            var weapon = builder.BuildWeapon("Меч", 10.0m);
            var armor = builder.BuildArmor("Щит", 5.0m);

            Assert.Equal("Меч магии", weapon.Name);
            Assert.Equal(12.0m, weapon.Damage); 
            Assert.Equal("Щит защиты", armor.Name);
            Assert.Equal(5.5m, armor.Defense);
        }

        [Fact]
        public void InventoryStatus_ShouldDisplayCorrectInformation()
        {
            var factory = new MedievalItemFactory();
            var builder = new ItemBuilder(factory);
            var inventory = new Inventory();

            var weapon = builder.BuildWeapon("Лук", 12.0m);
            inventory.AddItem(weapon);
            inventory.UseItem("Лук");
            var status = inventory.GetInventoryStatus();

            Assert.Contains("Лук", status);
            Assert.Contains("Damage: 12.0", status);
            Assert.Contains("Оборудован:", status);
        }

        [Fact]
        public void ItemCombination_ShouldCreateNewItem()
        {
            var factory = new MedievalItemFactory();
            var builder = new ItemBuilder(factory);
            var inventory = new Inventory();

            var potion1 = builder.BuildPotion("Здоровье", 20);
            var potion2 = builder.BuildPotion("Здоровье", 20);
            
            inventory.AddItem(potion1);
            inventory.AddItem(potion2);

            inventory.CombineItems("Здоровье", "Здоровье", (item1, item2) => 
                builder.BuildPotion("Вау исцеление", 50));

            Assert.Single(inventory.Items);
            Assert.Equal("Вау исцеление", inventory.Items[0].Name);
        }
    }
}