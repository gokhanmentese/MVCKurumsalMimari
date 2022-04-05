using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class Message : IEntity
    {
        [Key]
        public Guid Id { get; set; }

        public Guid? ParentMessageId { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public DateTime SendDate { get; set; } = DateTime.Now;

        public bool? IsArchieve { get; set; }

        public bool? IsRecycle { get; set; } 

        public Guid Sender { get; set; }

        public bool? IsSended { get; set; }

        public bool? IsRead { get; set; } 

        public int StateCode { get; set; }

        public int StatusCode { get; set; }

        public Guid OwnerId { get; set; }

        public Guid CreatedBy { get; set; }

        public Guid ModifiedBy { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime ModifiedOn { get; set; }

        public List<MessageRecipient> ToRecipients { get; set; }
    }
}
