using Lab3._1.Core.Interfaces;
using Lab3._1.Core.Models;
using Lab3._1.Patterns.Composite;
using System.Linq;
using NUnit.Framework;
using static NUnit.Framework.Assert; 


namespace Lab3._1.Tests.PatternTests
{
    [TestFixture]
    public class CompositeTests
    {
        [Test]
        public void Dish_Leaf_HasCorrectPrice()
        {
            // Arrange
            IMenuItem dish = new Dish("Суп", 5.00m);

            // Assert
            Assert.AreEqual("Суп", dish.Name);
            Assert.AreEqual(5.00m, dish.Price);
        }

        [Test]
        public void MenuCategory_Composite_CalculatesSumOfChildrenPrices()
        {
            IMenuItem pizza = new Dish("Пицца", 10.00m);
            IMenuItem coke = new Dish("Кола", 2.00m);
            
            MenuCategory mainCategory = new MenuCategory("Main");
            mainCategory.Add(pizza);
            mainCategory.Add(coke);

            Assert.AreEqual("Main", mainCategory.Name);
            Assert.AreEqual(12.00m, mainCategory.Price);
        }

        [Test]
        public void NestedCategories_CalculatesPriceRecursively()
        {
            IMenuItem coffee = new Dish("Кофе", 3.00m);
            MenuCategory hotDrinks = new MenuCategory("Hot Drinks");
            hotDrinks.Add(coffee); 

            IMenuItem burger = new Dish("Бургер", 8.00m);
            MenuCategory lunch = new MenuCategory("Lunch");
            lunch.Add(burger);

            MenuCategory fullMenu = new MenuCategory("Full Menu");
            fullMenu.Add(hotDrinks); 
            fullMenu.Add(lunch); 

            Assert.AreEqual(11.00m, fullMenu.Price);
        }
    }
}