using Core.Attributes;
using Core.Entities;
using Core.Entities.Concrete;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    public class Email : IEntity
    {
       [Key]
        public Guid Id { get; set; }

        public Guid? ParentEmailId { get; set; }

        public Guid? TaskId { get; set; }

        public Guid? SenderId { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string Cc { get; set; }

        public string Bcc { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public DateTime? SendDate { get; set; } = DateTime.Now;

        public DateTime? ReadDate { get; set; }

        public int? Status { get; set; }

        public int? TransferStatus { get; set; }

        public string StatusReason { get; set; }

        public bool IsHtml { get; set; }

        public Guid CreatedBy { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

        [NotMapped]
        public string FileType { get; set; }

        [NotMapped]
        public byte[] FileStream { get; set; }


    }
}
