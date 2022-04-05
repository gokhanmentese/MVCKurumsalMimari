using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Entities.Concrete
{
    public class Role : IEntity
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
    }
}
