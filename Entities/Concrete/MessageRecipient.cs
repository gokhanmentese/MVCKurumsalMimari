using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
   public class MessageRecipient : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid UserId { get; set; }

        public Guid MessageId { get; set; }

        public bool? IsArchieve { get; set; } 

        public bool? IsRecycle { get; set; }

        public bool? IsRead { get; set; } 
    }
}
