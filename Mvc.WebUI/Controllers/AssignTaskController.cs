using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Business.Constants;
using Core.Enums;
using Core.Utilities.Results;
using Entities.Concrete;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Mvc.WebUI.Model;
using Mvc.WebUI.ViewModel;

namespace Mvc.WebUI.Controllers
{
    public class AssignTaskController : Controller
    {
        private readonly IAssignTaskService _assignTaskService;
        private readonly IDropdownService _dropdownService;
        private Guid CurrentUserId
        {
            get
            {
                if (HttpContext != null && HttpContext.User != null)
                    return new Guid(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                else
                    return Guid.Empty;
            }
        }

        public AssignTaskController(IAssignTaskService assignTaskService, IDropdownService dropdownService)
        {
            _assignTaskService = assignTaskService;
            _dropdownService = dropdownService;
        }

        public IActionResult TaskAddUser()
        {
            TaskAddUserViewModel taskAddUserViewModel = new TaskAddUserViewModel();

            int? directorsgipId = null;
            int? departmentId = null;
            int? unitId = null;

            taskAddUserViewModel.Directorships = _dropdownService.PopulateDirectorships();
            taskAddUserViewModel.Departments = _dropdownService.PopulateDepartments(directorsgipId);
            taskAddUserViewModel.Units = _dropdownService.PopulateUnits(departmentId);
            taskAddUserViewModel.AssignUsers = _dropdownService.PopulateAssignUsers(unitId);

            return PartialView(taskAddUserViewModel);
        }

        [HttpPost]
        public IActionResult TaskAddUser(TaskAddUserViewModel taskAddUserViewModel)
        {
            if (!ModelState.IsValid)
            {
                return Json(new ErrorResult(Messages.TaskAddUserNotValid));
            }

            AssignTask assignTask = new AssignTask();
            assignTask.TaskId = taskAddUserViewModel.TaskId;
            assignTask.StatusCode = (int)Enumarations.AssignTaskStatus.Open;
            assignTask.AssignedUserId = taskAddUserViewModel.AssignUserId.Value;
            assignTask.StartDate = taskAddUserViewModel.StartDate;
            assignTask.EndDate = taskAddUserViewModel.EndDate;
            assignTask.CreatedOn = DateTime.Now;
            assignTask.CreatedBy = CurrentUserId;

            _assignTaskService.Add(assignTask);

            return Json(new SuccessResult());
        }

        public IActionResult GetUsersForAssignedTask(Guid taskid)
        {
            var taskUsers = _assignTaskService.GetUsersForAssignedTask(taskid).OrderBy(a => a.StartDate).ToList();

            return Ok(taskUsers);
        }

        public IActionResult TaskDelete(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var aTask = _assignTaskService.GetById(id);
            if (aTask == null)
                return NotFound();

            _assignTaskService.Delete(aTask);

            return Json(new SuccessResult());
        }

    }
}
