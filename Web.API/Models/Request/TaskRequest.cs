using System;
using System.Collections.Generic;
using System.Linq;
using Web.API.Models.Attribute;

namespace Web.API.Models.Request
{
    public class TaskRequest
    {
        public Auth Auth { get; set; }

        public Task Task { get; set; }

    }
}
