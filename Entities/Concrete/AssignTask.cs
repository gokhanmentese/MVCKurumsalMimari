using Core.Entities;
using Core.Enums;
using System;


namespace Entities.Concrete
{
    public class AssignTask : IEntity
    {
        public Guid Id { get; set; }

        public Guid TaskId { get; set; }

        public Guid AssignUserId { get; set; }

        public Guid AssignedUserId { get; set; }

        public DateTime?  AssignDate{ get; set; }

        public string Description { get; set; }

        public int StatusCode { get; set; } = (int)Enumarations.AssignTaskStatus.Open;

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? CreatedOn { get; set; } = DateTime.Now;

        public Guid CreatedBy { get; set; }
    }
}
