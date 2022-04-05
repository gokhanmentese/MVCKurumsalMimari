using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using Business.Constants;
using Core.Common;
using Core.Enums;
using Core.Utilities.Results;
using Entities.Concrete;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Hosting;
using Mvc.WebUI.Model;
using Mvc.WebUI.ViewModel;
using Core.Extensions;
using Entities.DTOs;
using Mvc.WebUI.Attributes;

namespace Mvc.WebUI.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        #region Global Parameters
        private readonly IHostEnvironment _hostEnvironment;
        private readonly ICategoryService _categoryService;
        //private readonly IUserService _userService;
        private readonly ITaskService _taskService;
        private readonly IUserProfileService _userProfileService;
        private readonly IDirectorshipService _directorshipService;
        private readonly IDepartmentService _departmentService;
        private readonly IUnitService _unitService;
        private readonly ITaskFileService _taskFileService;
        private readonly IFileService _fileService;
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
        #endregion

        #region Construector

        public TaskController(ICategoryService categoryService, IUserProfileService userProfileService, ITaskService taskService,
            IDirectorshipService directorshipService, IDepartmentService departmentService, IUnitService unitService, IHostEnvironment hostEnvironment,
            ITaskFileService taskFileService, IFileService fileService, IAssignTaskService assignTaskService, IDropdownService dropdownService)
        {
            _categoryService = categoryService;
            _userProfileService = userProfileService;
            _taskService = taskService;
            _directorshipService = directorshipService;
            _departmentService = departmentService;
            _unitService = unitService;
            _hostEnvironment = hostEnvironment;
            _taskFileService = taskFileService;
            _fileService = fileService;
            _assignTaskService = assignTaskService;
            _dropdownService = dropdownService;
        }
        #endregion

        #region Actions
        public IActionResult Index()
        {
            TaskIndexViewModel taskIndexViewModel = new TaskIndexViewModel();

            taskIndexViewModel.PageTitleOptions = new PageTitleOptions
            {
                Link1 = new PageLink { DisplayName = "AnaSayfa", Controller = "Home", Action = "Index" },
                Link2 = new PageLink { DisplayName = "Görevler", Controller = "Task", Action = "Index" }
            };

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

            taskIndexViewModel.WaitingTasks = _gridWaiting;
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

            taskIndexViewModel.ClosedTasks = _gridClosed;
            #endregion

            return View(taskIndexViewModel);
        }

        [Authorize]
        public IActionResult Create()
        {
            TaskViewModel taskViewModel = new TaskViewModel();

            taskViewModel.PageTitleOptions = new PageTitleOptions
            {
                Link1 = new PageLink { DisplayName = "AnaSayfa", Controller = "Home", Action = "Index" },
                Link2 = new PageLink { DisplayName = "Görevler", Controller = "Task", Action = "Index" },
                Link3 = new PageLink { DisplayName = "Yeni Görev", Controller = "Task", Action = "Create" }
            };

            taskViewModel.Task = new Entities.Concrete.Task();
            taskViewModel.Categories = _dropdownService.PopulateCategories();
            taskViewModel.AssignUsers = new List<SelectListItem>();// PopulateAssignUsers();
            //taskViewModel.Categories = new SelectList(categoryList, "Id", "Name");
            //taskViewModel.AssignUsers = new SelectList(userList, "Id", "FullName");

            taskViewModel.Directorships = _dropdownService.PopulateDirectorships();
            taskViewModel.Departments = new List<SelectListItem>();// PopulateDepartments();
            taskViewModel.Units = new List<SelectListItem>(); //  PopulateUnits();

            return View(taskViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(TaskViewModel taskViewModel)
        {
            try
            {
                Guid currentUserId = new Guid(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
                string currentUserName = HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;

                taskViewModel.Categories = _dropdownService.PopulateCategories();
                taskViewModel.AssignUsers = _dropdownService.PopulateAssignUsers();

                //if (!ModelState.IsValid)
                //    return View(taskViewModel);

                //var selectedUserItem = taskViewModel.AssignUsers.Find(p => p.Value == taskViewModel.AssignUserId.ToString());
                //if (selectedUserItem != null)
                //{
                //    selectedUserItem.Selected = true;
                //}

                Task newTask = new Task();
                newTask.Subject = taskViewModel.Task.Subject;
                newTask.CategoryId = taskViewModel.Task.CategoryId;
                newTask.AssignUserId = taskViewModel.Task.AssignUserId;
                newTask.Description = taskViewModel.Task.Description;
                newTask.StartDate = taskViewModel.Task.StartDate;
                newTask.EndDate = taskViewModel.Task.EndDate;
                newTask.StateCode = (int)Enumarations.TaskStates.Active;
                newTask.StatusCode = (int)Enumarations.TaskStatus.Open;
                newTask.OwnerId = currentUserId;
                newTask.Owner = currentUserName;
                newTask.CreatedOn = DateTime.Now;
                newTask.CreatedBy = currentUserId;
                newTask.ModifiedOn = DateTime.Now;
                newTask.ModifiedBy = currentUserId;

                Task task = _taskService.Add(newTask);

              //  var queryString = SecurityExtensions.EncryptText("id="+task.Id.ToStringFromGuid());

                return RedirectToAction("Detail", "Task", new { id = task.Id });

            }
            catch (Exception ex)
            {
                return View(taskViewModel);
            }
        }

        [Authorize]
        [EncryptedActionParameter(typeof(Guid))]
        public IActionResult Detail(Guid id)
        {
            TaskViewModel taskViewModel = new TaskViewModel();

            taskViewModel.PageTitleOptions = new PageTitleOptions
            {
                Link1 = new PageLink { DisplayName = "AnaSayfa", Controller = "Home", Action = "Index" },
                Link2 = new PageLink { DisplayName = "Görevler", Controller = "Task", Action = "Index" },
                Link3 = new PageLink { DisplayName = "Detay", Controller = "Task", Action = "Detail" }
            };

            int? directorsgipId = null;
            int? departmentId = null;
            int? unitId = null;

            Entities.Concrete.Task task = _taskService.GetById(id);

            if (task.AssignUserId != null)
            {
                var userProfile = _userProfileService.GetByUserId(task.AssignUserId.Value);
                if (userProfile != null && userProfile.UserId != Guid.Empty)
                {
                    taskViewModel.DirectorshipId = userProfile.DirectorshipId;
                    taskViewModel.DepartmentId = userProfile.DepartmentId;
                    taskViewModel.UnitId = userProfile.UnitId;

                    directorsgipId = userProfile.DirectorshipId;
                    departmentId = userProfile.DepartmentId;
                    unitId = userProfile.UnitId;
                }
            }

            taskViewModel.Categories = _dropdownService.PopulateCategories();
            taskViewModel.Directorships = _dropdownService.PopulateDirectorships();
            taskViewModel.Departments = _dropdownService.PopulateDepartments(directorsgipId);
            taskViewModel.Units = _dropdownService.PopulateUnits(departmentId);
            taskViewModel.AssignUsers = _dropdownService.PopulateAssignUsers(unitId);

            task.TaskFiles = _taskFileService.GetByTaskId(id);
            //  task.UserTaskFiles= _taskFileService.GetByTaskId(id);

            taskViewModel.Task = task;
            taskViewModel.IsOwner = CheckIfTaskOwner(task.Id).Success;

            #region GridOptions
            GridOptions _gridOptions = new GridOptions();
            _gridOptions = new GridOptions();
            _gridOptions.GridId = "taskusers";
            _gridOptions.SourceType = typeof(AssignTaskDTO);
            _gridOptions.ValueColumnsName = new string[] { "FullName", "StartDate", "EndDate", "AssignDate" };
            _gridOptions.DisplayColumnsName = new string[] { "Ad Soyad", "Başlangıç Tarihi", "Bitiş Tarihi", "Atama Tarihi" };
            _gridOptions.ColumnWidths = new string[] { "30%", "20%", "20%", "20%", "10%;center" };
            _gridOptions.sourceDefined = true;

            List<CustomGridColumns> command_btn_list = new List<CustomGridColumns>();
            CustomGridColumns command_btn_group = new CustomGridColumns();
            command_btn_group.ActionFunction = new string[] { "btn_Detail_Kullanici", "btn_Delete_Kullanici" };
            command_btn_list.Add(command_btn_group);

            _gridOptions.commandColumns = command_btn_list;

            _gridOptions.Source = _assignTaskService.GetUsersForAssignedTask(id).OrderBy(a => a.StartDate).ToList();
            _gridOptions.IsNEwButton = false;

            taskViewModel.TaskUsers = _gridOptions;
            #endregion

            return View(taskViewModel);
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(TaskViewModel taskViewModel)
        {
            try
            {
                Guid currentUserId = new Guid(HttpContext.User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);

                taskViewModel.Categories = _dropdownService.PopulateCategories();
                taskViewModel.AssignUsers = _dropdownService.PopulateAssignUsers();

                Task newTask = _taskService.GetById(taskViewModel.Task.Id);
                newTask.Subject = taskViewModel.Task.Subject;
                newTask.CategoryId = taskViewModel.Task.CategoryId;
                newTask.AssignUserId = taskViewModel.Task.AssignUserId;
                newTask.Description = taskViewModel.Task.Description;
                newTask.StartDate = taskViewModel.Task.StartDate;
                newTask.EndDate = taskViewModel.Task.EndDate;
                newTask.ModifiedBy = currentUserId;
                newTask.ModifiedOn = DateTime.Now;

                _taskService.Update(newTask);

                return RedirectToAction("Detail", "Task", new { id = taskViewModel.Task.Id });
            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public IActionResult UpdateTaskUsers(Guid id)
        {
            #region GridOptions
            GridOptions _gridOptions = new GridOptions();
            _gridOptions = new GridOptions();
            _gridOptions.GridId = "taskusers";
            _gridOptions.SourceType = typeof(AssignTaskDTO);
            _gridOptions.ValueColumnsName = new string[] { "FullName", "StartDate", "EndDate", "AssignDate" };
            _gridOptions.DisplayColumnsName = new string[] { "Ad Soyad", "Başlangıç Tarihi", "Bitiş Tarihi", "Atama Tarihi" };
            _gridOptions.ColumnWidths = new string[] { "30%", "20%", "20%", "20%", "10%;center" };
            _gridOptions.sourceDefined = true;

            List<CustomGridColumns> command_btn_list = new List<CustomGridColumns>();
            CustomGridColumns command_btn_group = new CustomGridColumns();
            command_btn_group.ActionFunction = new string[] { "btn_Detail_Kullanici", "btn_Delete_Kullanici" };
            command_btn_list.Add(command_btn_group);

            _gridOptions.commandColumns = command_btn_list;

            _gridOptions.Source = _assignTaskService.GetUsersForAssignedTask(id).OrderBy(a => a.StartDate).ToList();
            _gridOptions.IsNEwButton = false;

            #endregion

            return ViewComponent("Grid", _gridOptions);
        }

        [Authorize]
        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> LoadFiles(TaskViewModel model)
        {
            if (ModelState.IsValid)
            {
                string uniqueFileName = string.Empty;
                string filePath = string.Empty;

                var addedFiles = new List<IFormFile>();
                var isOwner = CheckIfTaskOwner(model.Task.Id).Success;

                if (isOwner)
                    addedFiles = model.TaskFiles;
                else
                    addedFiles = model.UserTaskFiles;

                if (addedFiles != null)
                {
                    var task = _taskService.GetById(model.Task.Id);
                    if (task != null && task.Id != Guid.Empty)
                    {
                        string uploadsFolder = GetFilePath(task.Id, CurrentUserId).Message;

                        if (!Directory.Exists(uploadsFolder))
                            Directory.CreateDirectory(uploadsFolder);

                        foreach (var itemFile in addedFiles)
                        {
                            Guid taskFileId = Guid.NewGuid();

                            uniqueFileName = taskFileId.ToString() + "_" + itemFile.FileName;
                            filePath = Path.Combine(uploadsFolder, uniqueFileName);

                            using (var fileStream = new FileStream(filePath, FileMode.Create))
                            {
                                await itemFile.CopyToAsync(fileStream);
                            }


                            Entities.Concrete.File newFile = new Entities.Concrete.File
                            {
                                FileName = itemFile.FileName,
                                FilePath = filePath,
                                MimeType = itemFile.ContentType,
                            };

                            Entities.Concrete.File file = _fileService.Add(newFile);

                            TaskFile taskFile = new TaskFile
                            {
                                Id = taskFileId,
                                TaskId = model.Task.Id,
                                FileId = file.Id,
                                FileName = file.FileName,
                                UserId = CurrentUserId,
                                OwnerShip = isOwner ? (int)Enumarations.FileOwnerShip.Owner : (int)Enumarations.FileOwnerShip.User
                            };

                            _taskFileService.Add(taskFile);
                        }
                    }
                    else
                        throw new Exception(Messages.TaskNotFound);
                }
            }
            else
            {
                return Json(new ErrorResult(Messages.FileIsNotValid));
            }

            return Json(new SuccessResult(Messages.FileAdded));
        }


        [HttpPost]
        public IActionResult LoadTaskFileView(List<IFormFile> files)
        {

            List<TaskFile> taskFiles = new List<TaskFile>()
            {
             new TaskFile { FileName="Test1" },
             new TaskFile { FileName="Test2" }
            };

            //Some Opeartion
            return PartialView("TaskFile", taskFiles);

        }

        public IActionResult Waiting()
        {
            TaskWaitingViewModel taskWaitingViewModel = new TaskWaitingViewModel();

            taskWaitingViewModel.PageTitleOptions = new PageTitleOptions
            {
                Link1 = new PageLink { DisplayName = "AnaSayfa", Controller = "Home", Action = "Index" },
                Link2 = new PageLink { DisplayName = "Görevler", Controller = "Task", Action = "Index" },
                Link3 = new PageLink { DisplayName = "Bekleyen Görevler", Controller = "Task", Action = "Waiting" }
            };

            #region GridOptions
            GridOptions _gridOptions = new GridOptions();
            _gridOptions.GridId = "waitingtasks";
            _gridOptions.SourceType = typeof(Task);
            _gridOptions.ValueColumnsName = new string[] { "Subject", "StartDateStr", "EndDateStr", "Owner", "Status" };
            _gridOptions.DisplayColumnsName = new string[] { "Konu", "Başlangıç Tarihi", "Bitiş Tarihi", "Sahibi", "Durum" };
            _gridOptions.ColumnWidths = new string[] { "30%", "15%", "15%", "15%", "15%", "10%;center" };
            _gridOptions.sourceDefined = true;

            List<CustomGridColumns> command_btn_list = new List<CustomGridColumns>();
            CustomGridColumns command_btn_group = new CustomGridColumns();
            command_btn_group.ActionFunction = new string[] { "btn_Detail_Task", "btn_Delete_Task" };
            command_btn_list.Add(command_btn_group);

            _gridOptions.commandColumns = command_btn_list;
            _gridOptions.Source = _taskService.GetWaitingTasksByUserId(CurrentUserId);
            _gridOptions.IsNEwButton = false;


            #endregion

            taskWaitingViewModel.WaitingTasks = _gridOptions;

            return View(taskWaitingViewModel);
        }

        public IActionResult Closed()
        {
            TaskClosedViewModel taskClosedViewModel = new TaskClosedViewModel();

            taskClosedViewModel.PageTitleOptions = new PageTitleOptions
            {
                Link1 = new PageLink { DisplayName = "AnaSayfa", Controller = "Home", Action = "Index" },
                Link2 = new PageLink { DisplayName = "Görevler", Controller = "Task", Action = "Index" },
                Link3 = new PageLink { DisplayName = "Tamamlanan Görevler", Controller = "Task", Action = "Closed" }
            };

            #region GridOptions
            GridOptions _gridOptions = new GridOptions();
            _gridOptions.GridId = "closedtasks";
            _gridOptions.SourceType = typeof(Task);
            _gridOptions.ValueColumnsName = new string[] { "Subject", "StartDateStr", "EndDateStr", "Owner", "Status" };
            _gridOptions.DisplayColumnsName = new string[] { "Konu", "Başlangıç Tarihi", "Bitiş Tarihi", "Sahibi", "Durum" };
            _gridOptions.ColumnWidths = new string[] { "30%", "15%", "15%", "15%", "15%", "10%;center" };
            _gridOptions.sourceDefined = true;

            List<CustomGridColumns> command_btn_list = new List<CustomGridColumns>();
            CustomGridColumns command_btn_group = new CustomGridColumns();
            command_btn_group.ActionFunction = new string[] { "btn_Detail_Task", "btn_Delete_Task" };
            command_btn_list.Add(command_btn_group);

            _gridOptions.commandColumns = command_btn_list;
            _gridOptions.Source = _taskService.GetClosedTasksByUserId(CurrentUserId);
            _gridOptions.IsNEwButton = false;
            #endregion

            taskClosedViewModel.ClosedTasks = _gridOptions;

            return View(taskClosedViewModel);
        }

        public IActionResult AssignList()
        {
            AssignListViewModel assignListViewModel = new AssignListViewModel();

            assignListViewModel.PageTitleOptions = new PageTitleOptions
            {
                Link1 = new PageLink { DisplayName = "AnaSayfa", Controller = "Home", Action = "Index" },
                Link2 = new PageLink { DisplayName = "Görevler", Controller = "Task", Action = "AssignList" }
            };

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
            command_btn_group.ActionFunction = new string[] { "btn_Assign_Task" };
            command_btn_list.Add(command_btn_group);

            _gridWaiting.commandColumns = command_btn_list;

            _gridWaiting.Source = _taskService.GetWaitingTasksByUserId(CurrentUserId);
            _gridWaiting.IsNEwButton = false;
            _gridWaiting.IsGridHeader = true;
            _gridWaiting.GridHeader = new GridHeader
            {
                Title = "Bekleyen Görevler",
            };

            assignListViewModel.WaitingTasks = _gridWaiting;
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
            closed_command_btn_group.ActionFunction = new string[] { "btn_Assign_Task" };
            closed_command_btn_list.Add(command_btn_group);

            _gridClosed.commandColumns = closed_command_btn_list;

            _gridClosed.Source = _taskService.GetClosedTasksByUserId(CurrentUserId);
            _gridClosed.IsNEwButton = false;
            _gridClosed.IsGridHeader = true;
            _gridClosed.GridHeader = new GridHeader
            {
                Title = "Tamamlanan Görevler",
            };

            assignListViewModel.ClosedTasks = _gridClosed;
            #endregion

            return View(assignListViewModel);
        }

        [EncryptedActionParameter(typeof(Guid))]
        public IActionResult Assignment(Guid id)
        {
            AssignTaskViewModel assignTaskViewModel = new AssignTaskViewModel();

            assignTaskViewModel.PageTitleOptions = new PageTitleOptions
            {
                Link1 = new PageLink { DisplayName = "AnaSayfa", Controller = "Home", Action = "Index" },
                Link2 = new PageLink { DisplayName = "Görevler", Controller = "Task", Action = "Index" },
                Link3 = new PageLink { DisplayName = "Görev Devret", Controller = "Task", Action = "Assignment" }
            };

            Task task = _taskService.GetById(id);

            assignTaskViewModel.AssignTask = new AssignTask { Id = id, TaskId = task.Id };
            assignTaskViewModel.AssignUsers = new List<SelectListItem>();// PopulateAssignUsers();
            assignTaskViewModel.Directorships = _dropdownService.PopulateDirectorships();
            assignTaskViewModel.Departments = new List<SelectListItem>();// PopulateDepartments();
            assignTaskViewModel.Units = new List<SelectListItem>(); //  PopulateUnits();

            return View(assignTaskViewModel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Assign(TaskViewModel taskViewModel)
        {
            var taskUsers = _assignTaskService.GetUsersForAssignedTask(taskViewModel.Task.Id);
            if (taskUsers != null && taskUsers.Count != 0)
            {
                foreach (var item in taskUsers)
                {
                    var assignTask = _assignTaskService.GetById(item.Id);
                    if (assignTask != null)
                    {
                        assignTask.StatusCode = (int)Enumarations.AssignTaskStatus.Assigned;
                        assignTask.AssignDate = DateTime.Now;
                        assignTask.AssignUserId = CurrentUserId;
                        _assignTaskService.Update(assignTask);
                    }
                }
            }

            var task = _taskService.GetById(taskViewModel.Task.Id);
            task.AssignUserId = CurrentUserId;
            task.StateCode = (int)Enumarations.TaskStates.Active;
            task.StatusCode = (int)Enumarations.TaskStatus.Assigned;
            _taskService.Update(task);

            return RedirectToAction("Index", "Task");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Completed(TaskViewModel taskViewModel)
        {
            var task = _taskService.GetById(taskViewModel.Task.Id);
            task.StateCode = (int)Core.Enums.Enumarations.TaskStates.InActive;
            task.StatusCode = (int)Core.Enums.Enumarations.TaskStatus.Closed;

            _taskService.Update(task);

            return RedirectToAction("Detail", "Task", new { id = taskViewModel.Task.Id });

        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Transfer(AssignTaskViewModel taskViewModel)
        {
            AssignTask assignTask = new AssignTask();
            assignTask.TaskId = taskViewModel.AssignTask.TaskId;
            assignTask.AssignUserId = CurrentUserId;
            assignTask.AssignedUserId = taskViewModel.AssignTask.AssignUserId;
            assignTask.StatusCode = (int)Enumarations.AssignTaskStatus.Assigned;
            assignTask.Description = taskViewModel.AssignTask.Description;
            assignTask.AssignDate = DateTime.Now;
            assignTask.StartDate = taskViewModel.AssignTask.StartDate;
            assignTask.EndDate = taskViewModel.AssignTask.EndDate;
            assignTask.CreatedOn = DateTime.Now;
            assignTask.CreatedBy = CurrentUserId;

            _assignTaskService.Add(assignTask);

            var existingAssignTask = _assignTaskService.GetByTaskIdAndUserId(taskViewModel.AssignTask.TaskId, CurrentUserId, DateTime.Now);

            if (existingAssignTask != null)
            {
                existingAssignTask.StatusCode = (int)Enumarations.AssignTaskStatus.OtherAssigned;
                _assignTaskService.Update(existingAssignTask);
            }

            return RedirectToAction("Index", "Task");
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult ClosedTransfer(AssignTaskViewModel taskViewModel)
        {
            if (ModelState.IsValid)
            {
                return NotFound();
            }

            var existingAssignTask = _assignTaskService.GetByTaskIdAndUserId(taskViewModel.AssignTask.TaskId, CurrentUserId, DateTime.Now);
            if (existingAssignTask != null)
            {
                existingAssignTask.StatusCode = (int)Enumarations.AssignTaskStatus.Closed;
                _assignTaskService.Update(existingAssignTask);
            }

            AssignTask assignTask = new AssignTask();
            assignTask.TaskId = taskViewModel.AssignTask.TaskId;
            assignTask.AssignUserId = CurrentUserId;
            assignTask.AssignedUserId = taskViewModel.AssignTask.AssignUserId;
            assignTask.StatusCode = (int)Enumarations.AssignTaskStatus.Assigned;
            assignTask.Description = taskViewModel.AssignTask.Description;
            assignTask.AssignDate = DateTime.Now;
            assignTask.StartDate = taskViewModel.AssignTask.StartDate;
            assignTask.EndDate = taskViewModel.AssignTask.EndDate;
            assignTask.CreatedOn = DateTime.Now;
            assignTask.CreatedBy = CurrentUserId;

            _assignTaskService.Add(assignTask);

            return RedirectToAction("Index", "Task");
        }

        [EncryptedActionParameter(typeof(Guid))]
        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var task = _taskService.GetById(id);
            if (task == null)
                return NotFound();

            _taskService.Delete(task);

            return RedirectToAction("Index", "Task");
        }

        #endregion

        #region Methods
        private IResult CheckIfTaskOwner(Guid taskId)
        {
            if (_taskService.GetById(taskId).OwnerId.ToStringFromGuid() == CurrentUserId.ToStringFromGuid())
            {
                return new SuccessResult();
            }

            return new ErrorResult();
        }

        private IResult GetFilePath(Guid taskId, Guid currentUserId)
        {
            if (CheckIfTaskOwner(taskId).Success)
                return new SuccessResult(Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot//taskfiles//tasks//" + taskId.ToStringFromGuid()));
            else
                return new SuccessResult(Path.Combine(_hostEnvironment.ContentRootPath, "wwwroot//taskfiles//users//" + currentUserId.ToStringFromGuid() + "//" + taskId.ToStringFromGuid()));
        }

        //private List<SelectListItem> PopulateCategories()
        //{
        //    #region Categories
        //    List<Category> categoryList = _categoryService.GetAll();

        //    List<SelectListItem> categories = new List<SelectListItem>();

        //    categoryList.ForEach(i =>
        //    {
        //        categories.Add(new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
        //    });

        //    return categories;
        //    #endregion
        //}



        //private List<SelectListItem> PopulateDirectorships()
        //{
        //    #region AssignUsers
        //    var userList = _directorshipService.GetAll();

        //    List<SelectListItem> assignUsers = new List<SelectListItem>();

        //    userList.ForEach(i =>
        //    {
        //        assignUsers.Add(new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
        //    });

        //    return assignUsers;
        //    #endregion
        //}

        //private List<SelectListItem> PopulateDepartments(int? directorshipId = null)
        //{
        //    #region Departments
        //    List<Department> userList = new List<Department>();

        //    if (directorshipId == null)
        //        userList = _departmentService.GetAll();
        //    else
        //        userList = _departmentService.GetByDirectorhipId(directorshipId.Value);

        //    List<SelectListItem> assignUsers = new List<SelectListItem>();

        //    userList.ForEach(i =>
        //    {
        //        assignUsers.Add(new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
        //    });

        //    return assignUsers;
        //    #endregion
        //}

        //private List<SelectListItem> PopulateUnits(int? departmentId = null)
        //{
        //    #region Units
        //    List<Unit> userList = new List<Unit>();

        //    if (departmentId == null)
        //        userList = _unitService.GetAll();
        //    else
        //        userList = _unitService.GetByDepartmentId(departmentId.Value);

        //    List<SelectListItem> assignUsers = new List<SelectListItem>();

        //    userList.ForEach(i =>
        //    {
        //        assignUsers.Add(new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
        //    });

        //    return assignUsers;
        //    #endregion
        //}
        #endregion
    }
}
