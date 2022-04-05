using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using Core.Enums;
using Core.Extensions;
using Entities.Concrete;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.WebUI.Attributes;
using Mvc.WebUI.Model;
using Mvc.WebUI.ViewModel;

namespace Mvc.WebUI.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ITaskService _taskService;

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

        public HomeController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        public IActionResult Index()
        {
            #region Test
            //ViewBag.Categories = _cacheService.GetCacheVal(Enumarations.CacheKey.Kategoriler.ToString());

            var listRoles = User.ClaimRoles();

            if (User.Identity.IsAuthenticated && User.IsInRole("Admin"))
            {

            }

            var loggedInUser = HttpContext.User;
            var loggedInUserName = loggedInUser.Identity.Name; // This is our username we set earlier in the claims. 
            var loggedInUserName2 = loggedInUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value; //Another way to get the name or any other claim we set. 
            #endregion

            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel();

            #region GridWaiting

            GridOptions _gridWaiting = new GridOptions();
            _gridWaiting.GridId = "waitingtasks";
            _gridWaiting.SourceType = typeof(Task);
            _gridWaiting.ValueColumnsName = new string[] { "Subject", "StartDate", "EndDate", "Owner" };
            _gridWaiting.DisplayColumnsName = new string[] { "Konu", "Başlangıç Tarihi", "Bitiş Tarihi", "Sahibi" };
            _gridWaiting.ColumnWidths = new string[] { "30%", "20%", "20%", "20%", "10%;center" };
            _gridWaiting.sourceDefined = true;

            List<CustomGridColumns> command_btn_list = new List<CustomGridColumns>();
            CustomGridColumns command_btn_group = new CustomGridColumns();
            command_btn_group.ActionFunction = new string[] { "btn_Detail_Task", "btn_Delete_Task" };
            command_btn_list.Add(command_btn_group);

            _gridWaiting.commandColumns = command_btn_list;

            _gridWaiting.Source = _taskService.GetWaitingTasksByUserId(CurrentUserId);
            _gridWaiting.IsNEwButton = false;

            _gridWaiting.IsGridHeader = true;
            _gridWaiting.GridHeader = new GridHeader
            {
                Title = "Bekleyen Görevler",
                Button = new Button
                {
                    Name = "Tüm Bekleyen Görevler",
                    Control = "Task",
                    Action = "Waiting"
                }
            };

            homeIndexViewModel.WaitingTasks = _gridWaiting;
            #endregion

            #region GridClosed

            GridOptions _gridClosed = new GridOptions();
            _gridClosed.GridId = "closedtasks";
            _gridClosed.SourceType = typeof(Task);
            _gridClosed.ValueColumnsName = new string[] { "Subject", "StartDate", "EndDate", "Owner" };
            _gridClosed.DisplayColumnsName = new string[] { "Konu", "Başlangıç Tarihi", "Bitiş Tarihi", "Sahibi" };
            _gridClosed.ColumnWidths = new string[] { "30%", "20%", "20%", "20%", "10%;center" };
            _gridClosed.sourceDefined = true;

            List<CustomGridColumns> closed_command_btn_list = new List<CustomGridColumns>();
            CustomGridColumns closed_command_btn_group = new CustomGridColumns();
            closed_command_btn_group.ActionFunction = new string[] { "btn_Detail_Task", "btn_Delete_Task" };
            closed_command_btn_list.Add(command_btn_group);

            _gridClosed.commandColumns = closed_command_btn_list;

            _gridClosed.Source = _taskService.GetClosedTasksByUserId(CurrentUserId);
            _gridClosed.IsNEwButton = false;

            _gridClosed.IsGridHeader = true;
            _gridClosed.GridHeader = new GridHeader
            {
                Title = "Tamamlanan Görevler",
                Button = new Button
                {
                    Name = "Tüm Tamamlanan Görevler",
                    Control = "Task",
                    Action = "Closed"
                }
            };

            homeIndexViewModel.ClosedTasks = _gridClosed;
            #endregion

            return View(homeIndexViewModel);
        }

        public IActionResult Demo()
        {
            return View();
        }
    }
}
