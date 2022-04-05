using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Web.API.Models.Attribute
{
    public class Task
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Subject { get; set; }

        public string Description { get; set; }

        public int? PriorityCode { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public Guid? AssignUserId { get; set; }
    }
}
