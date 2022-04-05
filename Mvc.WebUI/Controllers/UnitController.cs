using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Mvc.WebUI.Controllers
{
    public class UnitController : Controller
    {
        private readonly IUnitService _unitService;

        public UnitController(IUnitService unitService)
        {
            _unitService = unitService;
        }

        public JsonResult GetUnits(int id)
        {
            var unitList = _unitService.GetByDepartmentId(id);

            List<SelectListItem> units = new List<SelectListItem>();

            unitList.ForEach(i =>
            {
                units.Add(new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
            });


            return Json(new SelectList(units, "Value", "Text"));
        }
    }
}
