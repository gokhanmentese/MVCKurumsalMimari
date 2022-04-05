using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IDropdownService
    {
        List<SelectListItem> PopulateDirectorships();

        List<SelectListItem> PopulateDepartments(int? directorshipId = null);

        List<SelectListItem> PopulateUnits(int? departmentId = null);

        List<SelectListItem> PopulateAssignUsers(int? unitId = null);

        List<SelectListItem> PopulateCategories();
    }
}
