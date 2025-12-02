using System;
using System.Collections.Generic;
using System.Linq;

namespace lab2
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("=== Система управления инвентарем ===");
            
            // Создаем инвентарь
            var inventory = new Inventory();
            
            // Создаем предметы
            var sword = new Weapon("Меч", 15.0m, new BasicUpgradeStrategy());
            var armor = new Armor("Броня", 10.0m, new BasicUpgradeStrategy());
            var shield = new Armor("Щит", 5.0m, new BasicUpgradeStrategy());
            
            // Добавляем предметы в инвентарь
            inventory.AddItem(sword);
            inventory.AddItem(armor);
            inventory.AddItem(shield);
            
            // Демонстрация работы инвентаря
            Console.WriteLine("\n1. Статус инвентаря:");
            Console.WriteLine(inventory.GetInventoryStatus());
            
            Console.WriteLine("\n2. Используем предметы:");
            Console.WriteLine(inventory.UseItem("Меч"));
            Console.WriteLine(inventory.UseItem("Броня"));
            
            Console.WriteLine("\n3. Статус после использования:");
            Console.WriteLine(inventory.GetInventoryStatus());
            
            Console.WriteLine("\n4. Улучшаем предмет:");
            Console.WriteLine(inventory.UpgradeItem("Меч"));
            
            Console.WriteLine("\n5. Финальный статус:");
            Console.WriteLine(inventory.GetInventoryStatus());
            
            // Демонстрация комбинирования предметов
            Console.WriteLine("\n6. Комбинирование предметов:");
            var anotherSword = new Weapon("Меч", 12.0m, new BasicUpgradeStrategy());
            inventory.AddItem(anotherSword);
            
            inventory.CombineItems("Меч", "Меч", (item1, item2) => 
                new Weapon("Улучшенный меч", 25.0m, new BasicUpgradeStrategy()));
                
            Console.WriteLine(inventory.GetInventoryStatus());
        }
    }

    public class Inventory
    {
        private readonly List<Item> _items;
        private readonly Dictionary<Type, Item> _equipped;

        public IReadOnlyList<Item> Items => _items.AsReadOnly();
        public IReadOnlyDictionary<Type, Item> Equipped => _equipped;

        public Inventory()
        {
            _items = new List<Item>();
            _equipped = new Dictionary<Type, Item>();
        }

        public void AddItem(Item item)
        {
            _items.Add(item);
        }

        public string UseItem(string itemName)
        {
            var item = _items.FirstOrDefault(i => i.Name == itemName);
            if (item == null) return "Айтем не найден";

            var result = item.Use();
            
            if (item is Weapon weapon)
            {
                _equipped[typeof(Weapon)] = weapon;
            }
            else if (item is Armor armor)
            {
                _equipped[typeof(Armor)] = armor;
            }

            return result;
        }

        public string UpgradeItem(string itemName)
        {
            var item = _items.FirstOrDefault(i => i.Name == itemName);
            if (item == null) return "Айтем не найден";

            if (item.UpgradeStrategy == null)
                return "Этот элемент не может быть улучшен";

            var upgradeResult = item.Upgrade();
            var stateResult = item.State.Upgrade(item);
            
            return $"{upgradeResult}. {stateResult}";
        }

        public string GetInventoryStatus()
        {
            var status = new List<string> { "Инвентарь:" };
            
            foreach (var item in _items)
            {
                var stateType = item.State is States.UpgradedState ? "Улучшен" : "Нормально";
                var properties = string.Join(", ", item.GetProperties()
                    .Select(p => $"{p.Key}: {p.Value}"));
                
                status.Add($"- {item.Name} ({stateType}): {properties}");
            }
            
            status.Add("Оборудован:");
            foreach (var equipped in _equipped)
            {
                status.Add($"- {equipped.Key.Name}: {equipped.Value.Name}");
            }
            
            return string.Join("\n", status);
        }

        public void CombineItems(string item1Name, string item2Name, Func<Item, Item, Item> combineFunc)
        {
            var item1 = _items.FirstOrDefault(i => i.Name == item1Name);
            var item2 = _items.FirstOrDefault(i => i.Name == item2Name && i != item1); 
            
            if (item1 != null && item2 != null)
            {
                var combinedItem = combineFunc(item1, item2);
                _items.Remove(item1);
                _items.Remove(item2);
                _items.Add(combinedItem);
            }
            else
            {
                var items = _items.Where(i => i.Name == item1Name).Take(2).ToList();
                if (items.Count == 2)
                {
                    var combinedItem = combineFunc(items[0], items[1]);
                    _items.Remove(items[0]);
                    _items.Remove(items[1]);
                    _items.Add(combinedItem);
                }
            }
        }
    }

    // Базовые классы и интерфейсы для демонстрации
    public abstract class Item
    {
        public string Name { get; }
        public IUpgradeStrategy UpgradeStrategy { get; protected set; }
        public IItemState State { get; set; }

        protected Item(string name)
        {
            Name = name;
            State = new States.NormalState();
        }

        public abstract string Use();
        public abstract Dictionary<string, object> GetProperties();

        public string Upgrade()
        {
            return UpgradeStrategy?.Upgrade(this) ?? "Нельзя улучшить";
        }
    }

    public class Weapon : Item
    {
        public decimal Damage { get; set; }

        public Weapon(string name, decimal damage, IUpgradeStrategy upgradeStrategy) 
            : base(name)
        {
            Damage = damage;
            UpgradeStrategy = upgradeStrategy;
        }

        public override string Use()
        {
            return $"Используется оружие {Name} с уроном {Damage}";
        }

        public override Dictionary<string, object> GetProperties()
        {
            return new Dictionary<string, object> { ["Урон"] = Damage };
        }
    }

    public class Armor : Item
    {
        public decimal Defense { get; set; }

        public Armor(string name, decimal defense, IUpgradeStrategy upgradeStrategy) 
            : base(name)
        {
            Defense = defense;
            UpgradeStrategy = upgradeStrategy;
        }

        public override string Use()
        {
            return $"Надевается броня {Name} с защитой {Defense}";
        }

        public override Dictionary<string, object> GetProperties()
        {
            return new Dictionary<string, object> { ["Защита"] = Defense };
        }
    }

    public interface IUpgradeStrategy
    {
        string Upgrade(Item item);
    }

    public class BasicUpgradeStrategy : IUpgradeStrategy
    {
        public string Upgrade(Item item)
        {
            if (item is Weapon weapon)
            {
                weapon.Damage *= 1.5m;
                return $"Урон увеличен до {weapon.Damage}";
            }
            else if (item is Armor armor)
            {
                armor.Defense *= 1.5m;
                return $"Защита увеличена до {armor.Defense}";
            }
            return "Неизвестный тип предмета";
        }
    }

    public interface IItemState
    {
        string Upgrade(Item item);
    }

    public static class States
    {
        public class NormalState : IItemState
        {
            public string Upgrade(Item item)
            {
                item.State = new UpgradedState();
                return "Состояние изменено на Улучшенное";
            }
        }

        public class UpgradedState : IItemState
        {
            public string Upgrade(Item item)
            {
                return "Предмет уже улучшен";
            }
        }
    }
}