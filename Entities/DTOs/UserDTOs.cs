using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Core.Attributes;
using Core.Entities;

namespace Entities.Concrete
{
    public class UserDTOs : IEntity
    {
        public string IdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        public string Email { get; set; }
    }
}
