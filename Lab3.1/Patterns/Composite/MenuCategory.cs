using Lab3._1.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Lab3._1.Patterns.Composite
{
    public class MenuCategory : IMenuItem
    {
        public string Name { get; }
        public decimal Price => _children.Sum(c => c.Price); 

        private readonly List<IMenuItem> _children = new List<IMenuItem>();

        public MenuCategory(string name)
        {
            Name = name;
        }

        public void Add(IMenuItem component)
        {
            _children.Add(component);
        }

        public void Display(int depth)
        {
            Console.WriteLine(new string('-', depth) + " Category: " + Name);

            foreach (var component in _children)
            {
                component.Display(depth + 2);
            }
        }
    }
}