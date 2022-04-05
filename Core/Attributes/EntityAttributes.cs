using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Attributes
{
    public interface IEntityAttributes
    {

    }

    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = false)]
    public class EntityAttributes : Attribute 
    {
        public EntityAttributes()
        {
            this.allowNull = false;
            this.isPrimaryKey = false;
            this.isForeignKey = false;
            this.isOptionSet = false;
        }
        public string PropertyName { get; set; }
        public bool isPrimaryKey { get; set; }
        public bool isForeignKey { get; set; }
        public bool allowNull { get; set; }
        public int MaxLength { get; set; }
        public bool isRequired { get; set; }

        public bool isAutoNumber { get; set; }

        public bool isOptionSet { get; set; }

        public bool isEntityReference { get; set; }

        public string EntityName { get; set; }
    }
}
