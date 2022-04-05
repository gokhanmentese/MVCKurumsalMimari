using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mvc.WebUI.Controllers
{
    public class DepartmanController : Controller
    {
        private readonly IDepartmentService _departmentService;

        public DepartmanController(IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }

        public JsonResult GetDepartments(int id)
        {
            var departmentList = _departmentService.GetByDirectorhipId(id);

            List<SelectListItem> departments = new List<SelectListItem>();

            departmentList.ForEach(i =>
            {
                departments.Add(new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
            });

            return Json(new SelectList(departments, "Value", "Text"));
        }
    }
}
