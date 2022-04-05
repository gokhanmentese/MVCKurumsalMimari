using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text;

namespace Entities.Concrete
{
    public class User : IEntity
    {
        public Guid UserId { get; set; }

        public string IdentityNumber { get; set; }

        public byte[] Password { get; set; }

        public byte[] PasswordSalt { get; set; }

        public bool Confirmed { get; set; } = false;

        public bool Status { get; set; } = false;
    }
}
