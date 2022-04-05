using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Web.API.Models.Attribute;

namespace Web.API.Models.Request
{
    public class TaskListRequest
    {
        public Auth   Auth { get; set; }

        public Guid AssingUserId { get; set; }
    }
}
