using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
   public class Role : IEntity
    {
        public Guid  Id { get; set; }

        public string Name { get; set; }
    }
}
