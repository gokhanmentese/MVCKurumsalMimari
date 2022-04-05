using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Core.Entities.Concrete
{
    public class UserRole : IEntity
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public Guid RoleId { get; set; }
    }
}
