using System;
using System.Collections.Generic;
using lab2.Items;
using lab2.Factories;
using lab2.Builders;
using lab2.Strategies;

namespace lab2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Система управления инвентарем RPG ===");
            Console.WriteLine("=========================================\n");

            // Создаем инвентарь
            var inventory = new Inventory();

            // Используем фабрику для создания предметов
            IItemFactory factory = new MedievalItemFactory();
            
            Console.WriteLine("1. Создание предметов через фабрику:");
            var sword = factory.CreateWeapon("Стальной меч", 25.0m);
            var armor = factory.CreateArmor("Кольчуга", 15.0m);
            var potion = factory.CreatePotion("Зелье здоровья", "Восстанавливает 50 HP");
            
            inventory.AddItem(sword);
            inventory.AddItem(armor);
            inventory.AddItem(potion);
            
            Console.WriteLine($"- Создан: {sword.Name}");
            Console.WriteLine($"- Создан: {armor.Name}");
            Console.WriteLine($"- Создан: {potion.Name}");
            Console.WriteLine();

            // Используем билдер для сложного предмета
            Console.WriteLine("2. Создание предмета через билдер:");
            var epicSword = new ItemBuilder()
                .SetName("Эпический меч дракона")
                .SetWeapon(35.0m, new WeaponUpgradeStrategy())
                .Build();
                
            inventory.AddItem(epicSword);
            Console.WriteLine($"- Создан через билдер: {epicSword.Name}");
            Console.WriteLine();

            // Демонстрация работы инвентаря
            Console.WriteLine("3. Статус инвентаря:");
            Console.WriteLine(inventory.GetInventoryStatus());
            Console.WriteLine();

            Console.WriteLine("4. Использование предметов:");
            Console.WriteLine(inventory.UseItem("Стальной меч"));
            Console.WriteLine(inventory.UseItem("Кольчуга"));
            Console.WriteLine(inventory.UseItem("Зелье здоровья"));
            Console.WriteLine();

            Console.WriteLine("5. Улучшение предметов:");
            Console.WriteLine(inventory.UpgradeItem("Стальной меч"));
            Console.WriteLine(inventory.UpgradeItem("Кольчуга"));
            Console.WriteLine();

            Console.WriteLine("6. Финальный статус инвентаря:");
            Console.WriteLine(inventory.GetInventoryStatus());
            Console.WriteLine();

            // Демонстрация комбинирования
            Console.WriteLine("7. Комбинирование предметов:");
            var anotherPotion = factory.CreatePotion("Зелье маны", "Восстанавливает 30 MP");
            inventory.AddItem(anotherPotion);
            
            inventory.CombineItems("Зелье здоровья", "Зелье маны", (item1, item2) =>
                factory.CreatePotion("Комбинированное зелье", "Восстанавливает 40 HP и 20 MP"));
                
            Console.WriteLine(inventory.GetInventoryStatus());

            Console.WriteLine("\nНажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
    }
}