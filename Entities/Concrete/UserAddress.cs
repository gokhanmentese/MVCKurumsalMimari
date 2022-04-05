using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class UserAddress : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid CountryId { get; set; }
    }
}
