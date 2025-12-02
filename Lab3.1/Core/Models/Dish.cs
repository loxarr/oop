using Lab3._1.Core.Interfaces;
using System;

namespace Lab3._1.Core.Models
{
    public class Dish : IMenuItem
    {
        public string Name { get; }
        public decimal Price { get; }

        public Dish(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        public void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + " Dish: " + Name + " (" + Price.ToString("C") + ")");
        }
    }
}