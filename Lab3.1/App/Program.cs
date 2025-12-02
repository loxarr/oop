using Lab3._1.Core.Enums;
using Lab3._1.Core.Interfaces;
using Lab3._1.Core.Services;
using Lab3._1.Core.Models;
using Lab3._1.Patterns.AbstractFactory;
using Lab3._1.Patterns.Composite;
using Lab3._1.Patterns.Decorator;
using System;

namespace Lab3._1.App
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Console.WriteLine("=== Система Управления Заказами Службы Доставки ===");
            Console.WriteLine("----------------------------------------------------\n");

            // 1. Создание Меню (Паттерн Компоновщик)
            Console.WriteLine("1. Инициализация Меню (Компоновщик)");
            IMenuItem pizza = new Dish("Пицца 'Маргарита'", 12.50m);
            IMenuItem pasta = new Dish("Паста 'Карбонара'", 9.80m);
            MenuCategory mainCourses = new MenuCategory("Горячие Блюда");
            mainCourses.Add(pizza);
            mainCourses.Add(pasta);

            IMenuItem coke = new Dish("Кока-Кола (0.5л)", 2.00m);
            IMenuItem juice = new Dish("Апельсиновый Сок", 3.00m);
            MenuCategory drinks = new MenuCategory("Напитки");
            drinks.Add(coke);
            drinks.Add(juice);

            MenuCategory fullMenu = new MenuCategory("Полное Меню");
            fullMenu.Add(mainCourses);
            fullMenu.Add(drinks);

            fullMenu.Display(0);
            Console.WriteLine("----------------------------------------------------\n");

            // 2. Создание Заказов (Паттерн Абстрактная Фабрика)
            Console.WriteLine("2. Создание Заказов (Абстрактная Фабрика и Стратегия)");
            IOrderFactory standardFactory = new StandardOrderFactory();
            IOrderFactory specialFactory = new SpecialOrderFactory();

            // Создание стандартного заказа
            OrderService standardService = new OrderService(standardFactory);
            IOrder order1 = standardService.CreateNewOrder();
            order1.AddItem(pizza, 2);
            order1.AddItem(coke, 3);
            Console.WriteLine($"Создан Заказ #{order1.OrderId} ({order1.Type}): {order1.Description}");

            // Создание специального заказа (с автоматической скидкой - Стратегия)
            OrderService specialService = new OrderService(specialFactory);
            IOrder order2 = specialService.CreateNewOrder();
            order2.AddItem(pasta, 1);
            order2.AddItem(juice, 2);
            Console.WriteLine($"Создан Заказ #{order2.OrderId} ({order2.Type}): {order2.Description}");
            Console.WriteLine("----------------------------------------------------\n");


            // 3. Расчет Стоимости (Паттерн Стратегия)
            Console.WriteLine("3. Расчет стоимости заказа (Стратегия)");
            
            Console.WriteLine($"Расчет для Заказа #{order1.OrderId} (Standard):");
            Console.WriteLine($"Итоговая стоимость: {order1.GetCost():C}");

            Console.WriteLine($"\nРасчет для Заказа #{order2.OrderId} (Special - с 10% скидкой):");
            Console.WriteLine($"Итоговая стоимость: {order2.GetCost():C}");
            Console.WriteLine("----------------------------------------------------\n");


            // 4. Расширение Заказа (Паттерн Декоратор)
            Console.WriteLine("4. Добавление опций к заказу (Декоратор)");
            
            // Оборачиваем Заказ 1 в Декоратор
            IOrder decoratedOrder1 = new PriorityDeliveryDecorator(order1);
            decoratedOrder1 = new SpecialInstructionDecorator(decoratedOrder1, "Положить побольше салфеток.");

            Console.WriteLine($"Описание Заказа #{decoratedOrder1.OrderId}: {decoratedOrder1.Description}");
            Console.WriteLine($"Итоговая стоимость с приоритетной доставкой: {decoratedOrder1.GetCost():C}");
            Console.WriteLine("----------------------------------------------------\n");

            // 5. Отслеживание Состояния (Паттерн Состояние)
            Console.WriteLine("5. Управление состоянием заказа (Состояние)");

            Console.WriteLine($"Текущий статус Заказа #{order1.OrderId}: {order1.Status}");

            // Корректный переход: Pending -> Preparing
            standardService.UpdateOrderStatus(order1.OrderId, OrderStatus.Preparing);

            // Корректный переход: Preparing -> Delivering
            standardService.UpdateOrderStatus(order1.OrderId, OrderStatus.Delivering);

            // Некорректный переход: Delivering -> Pending (предотвращается Состоянием)
            standardService.UpdateOrderStatus(order1.OrderId, OrderStatus.Pending);

            // Корректный переход: Delivering -> Completed
            standardService.UpdateOrderStatus(order1.OrderId, OrderStatus.Completed);

            // Попытка изменить выполненный заказ (предотвращается Состоянием)
            standardService.UpdateOrderStatus(order1.OrderId, OrderStatus.Delivering);

            Console.WriteLine($"\nФинальный статус Заказа #{order1.OrderId}: {order1.Status}");
        }
    }
}