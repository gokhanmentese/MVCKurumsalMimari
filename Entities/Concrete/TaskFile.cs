using Core.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Entities.Concrete
{
    public class TaskFile : IEntity
    {
        public Guid Id { get; set; }

        public Guid TaskId { get; set; }

        public Guid FileId { get; set; }

        public string FileName { get; set; }

        public Guid UserId { get; set; }

        public int? OwnerShip { get; set; }

        public DateTime? CreatedOn { get; set; } = DateTime.Now;


    }
}
