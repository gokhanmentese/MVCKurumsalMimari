using Core.Entities;
using Entities.Concrete;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Mvc.WebUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.WebUI.ViewComponents
{
    public class GridViewComponent : ViewComponent
    {
        private readonly IAssignTaskService _assignTaskService;

        public GridViewComponent(IAssignTaskService assignTaskService)
        {
            _assignTaskService = assignTaskService;
        }

        public IViewComponentResult Invoke(GridOptions gridOptions)
        {
            if (gridOptions.IsPrivate && gridOptions.TaskId !=Guid.Empty)
            {
                gridOptions.Source = _assignTaskService.GetUsersForAssignedTask(gridOptions.TaskId.Value);
            }
            GridOptionsManager gridOptionsManager = new GridOptionsManager(gridOptions);

            GridControl gridControl = gridOptionsManager.GetClass();
          
            return View(gridControl);
        }
    }
}
