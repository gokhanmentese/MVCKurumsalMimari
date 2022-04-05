using System;
using System.Runtime.Serialization;

namespace Core.Entities
{
    //public class BaseEntity : IEntity
    //{
    //}

    public class Reference : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public string LogicalName { get; set; }

        public Reference() { }
        public Reference(string logicalName, Guid id, string name)
        {
            Id = id;
            LogicalName = logicalName;
            Name = name;
        }

        public Reference(string logicalName, Guid id)
        {
            Id = id;
            LogicalName = logicalName;
        }

    }

    public class Option : IEntity
    {
        public int Value { get; set; }

        public string Label { get; set; }

        public Option()
        {

        }
        public Option(int value)
        {
            Value = value;
        }

        public Option(int value, string label)
        {
            Value = value;
            Label = label;
        }
    }
}
