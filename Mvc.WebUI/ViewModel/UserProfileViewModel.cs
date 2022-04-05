using Entities.Concrete;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mvc.WebUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.WebUI.ViewModel
{
    public class UserProfileViewModel
    {
        public PageTitleOptions PageTitleOptions { get; set; }

        public UserProfile UserProfile { get; set; }

        public List<SelectListItem> Directorships { get; set; }

        public List<SelectListItem> Departments { get; set; }

        public List<SelectListItem> Units { get; set; }

        public int? DirectorshipId { get; set; }

        public int? DepartmentId { get; set; }

        public int? UnitId { get; set; }
    }
}
