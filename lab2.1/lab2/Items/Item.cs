using lab2.States;
using lab2.Strategies;
using System.Collections.Generic;

namespace lab2.Items
{
    public abstract class Item
    {
        public string Name { get; set; }
        public IItemState State { get; set; }
        public IUpgradeStrategy UpgradeStrategy { get; set; }

        protected Item(string name)
        {
            Name = name;
            State = new NormalState();
        }

        public abstract Dictionary<string, object> GetProperties();
        
        public string Use() => State.Use(this);
        public string Upgrade() => UpgradeStrategy?.Upgrade(this) ?? "Невозможно улучшить айтем";
    }
}