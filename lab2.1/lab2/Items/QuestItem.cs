using System.Collections.Generic;

namespace lab2.Items
{
    public class QuestItem : Item
    {
        public string Description { get; }

        public QuestItem(string name, string description) : base(name)
        {
            Description = description;
            UpgradeStrategy = null;
        }

        public override Dictionary<string, object> GetProperties()
        {
            return new Dictionary<string, object>
            {
                ["Description"] = Description,
                ["Type"] = "Quest Item"
            };
        }
    }
}