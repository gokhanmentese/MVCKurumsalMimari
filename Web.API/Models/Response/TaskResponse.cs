using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Models.Attribute;

namespace Web.API.Models.Response
{
    public class TaskResponse
    {
        public Status Status { get; set; }

        public Guid TaskId { get; set; }
    }
}
