using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Entities.Concrete;
using Entities.Concrete;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mvc.WebUI.ViewModel;

namespace Mvc.WebUI.Controllers
{
    public class ChoosenController : Controller
    {
        private readonly IUserService _userService;

        public ChoosenController(IUserService userService)
        {
            _userService = userService;
        }

        public IActionResult Index()
        {
            TaskViewModel taskViewModel = new TaskViewModel();
            taskViewModel.Task = new Entities.Concrete.Task();
            taskViewModel.Categories = null;
            taskViewModel.AssignUsers = PopulateAssignUsers();
           
            return View(taskViewModel);
        }

        private List<SelectListItem> PopulateAssignUsers()
        {
            #region AssignUsers
            List<User> userList = _userService.GetAll();

            List<SelectListItem> assignUsers = new List<SelectListItem>();

            userList.ForEach(i =>
            {
               // assignUsers.Add(new SelectListItem { Text = i.UserProfile.FullName, Value = i.UserId.ToString() });
            });

            return assignUsers;
            #endregion
        }
    }
}
