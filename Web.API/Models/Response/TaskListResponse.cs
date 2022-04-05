using System;
using System.Collections.Generic;
using System.Linq;
using Web.API.Models.Attribute;

namespace Web.API.Models.Response
{
    public class TaskListResponse
    {
        public Status Status { get; set; }

        public List<Task> Tasks { get; set; }
    }
}
