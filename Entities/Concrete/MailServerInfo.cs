using Core.Attributes;
using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class EmailServerInfo : IEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Host { get; set; }

        public int? Port { get; set; }

        public bool? EnableSsl { get; set; }

        [Required]
        public string SenderEmail { get; set; }

        [Required]
        public string SenderEmailPassword { get; set; }

        public string SenderDisplayName { get; set; }
    }
}
