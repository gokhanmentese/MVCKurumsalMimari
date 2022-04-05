using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Entities.Concrete
{
    public class File : IEntity
    {
        public Guid Id { get; set; }

        public string FileName { get; set; }

        public string MimeType { get; set; }

        public string FilePath { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.Now;

    }
}
