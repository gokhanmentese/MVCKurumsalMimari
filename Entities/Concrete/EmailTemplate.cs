using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace Entities.Concrete
{
   public  class EmailTemplate : IEntity
    {
        [Key]
        public int Id { get; set; }

        public string Path { get; set; }

        public string DefaultPath { get; set; }

        public string FileName { get; set; }

        public string Subject { get; set; }

        [NotMapped]
        public string ContentRootPath { get; set; }= Directory.GetCurrentDirectory();

    }
}
