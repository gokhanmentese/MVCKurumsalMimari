using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.WebUI.ViewModel
{
    public class TaskAddUserViewModel
    {
        public Guid? AssignUserId { get; set; }

        public Guid TaskId { get; set; }

        public List<SelectListItem> AssignUsers { get; set; }

        public List<SelectListItem> Directorships { get; set; }

        public List<SelectListItem> Departments { get; set; }

        public List<SelectListItem> Units { get; set; }


        public int? DirectorshipId { get; set; }

        public int? DepartmentId { get; set; }

        public int? UnitId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }



    }
}
