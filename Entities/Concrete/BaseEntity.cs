using Core.Entities;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace Entities.Concrete
{
    [DataContract]
    public class BaseEntity : IEntity
    {
        public string Owner { get; set; }

        public Guid OwnerId { get; set; }
        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        public DateTime ModifiedOn { get; set; } = DateTime.Now;

     
    }
}
