using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Core.Entities;
using Core.Entities.Concrete;
using Entities.Concrete;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.DependencyInjection;
using Mvc.WebUI.Attributes;
using Mvc.WebUI.Model;
using Mvc.WebUI.ViewModel;

namespace Mvc.WebUI.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IUserProfileService _userProfileService;

        private readonly IDirectorshipService _directorshipService;
        private readonly IDepartmentService _departmentService;
        private readonly IUnitService _unitService;

        private readonly GridOptions _gridOptions;
        public UserController(IUserService userService, IUserProfileService userProfileService, IDirectorshipService directorshipService, IDepartmentService departmentService, IUnitService unitService)
        {
            _userService = userService;
            _userProfileService = userProfileService;
            _directorshipService = directorshipService;
            _departmentService = departmentService;
            _unitService = unitService;

            #region GridOptions
            _gridOptions = new GridOptions();
            _gridOptions.GridId = "allusers";
            _gridOptions.SourceType = typeof(UserProfile);
            _gridOptions.ValueColumnsName = new string[] { "IdentityNumber", "FirstName", "LastName", "Email", "PhoneNumber" };
            _gridOptions.DisplayColumnsName = new string[] { "TcKimlikNo", "Ad", "Soyad", "Email", "Telefon" };
            _gridOptions.ColumnWidths = new string[] { "20%", "20%", "20%", "15%", "15%", "10%;center" };
            _gridOptions.sourceDefined = true;
            _gridOptions.NewButtonUrl = new PageLink { DisplayName = "Yeni", Controller = "Auth", Action = "Register" };
            _gridOptions.IsNEwButton = true;

            List<CustomGridColumns> command_btn_list = new List<CustomGridColumns>();
            CustomGridColumns command_btn_group = new CustomGridColumns();
            command_btn_group.ActionFunction = new string[] { "btn_Detail_Kullanici", "btn_Delete_Kullanici" };
            command_btn_list.Add(command_btn_group);

            _gridOptions.commandColumns = command_btn_list;

            #endregion
        }

        [Authorize(Roles = "Admin")]
        public IActionResult ShowUsers()
        {
            UsersViewModel usersViewModel = new UsersViewModel();

            usersViewModel.PageTitleOptions = new PageTitleOptions
            {
                Link1 = new PageLink { DisplayName = "AnaSayfa", Controller = "Home", Action = "Index" },
                Link2 = new PageLink { DisplayName = "Kullanıcılar", Controller = "User", Action = "ShowUsers" }
            };

            #region GridOptions
            GridOptions _gridOptions = new GridOptions();
            _gridOptions = new GridOptions();
            _gridOptions.GridId = "allusers";
            _gridOptions.SourceType = typeof(UserProfile);
            _gridOptions.ValueColumnsName = new string[] { "IdentityNumber", "FirstName", "LastName", "Email", "PhoneNumber" };
            _gridOptions.DisplayColumnsName = new string[] { "TcKimlikNo", "Ad", "Soyad", "Email", "Telefon" };
            _gridOptions.ColumnWidths = new string[] { "20%", "20%", "20%", "15%", "15%", "10%;center" };
            _gridOptions.sourceDefined = true;

            List<CustomGridColumns> command_btn_list = new List<CustomGridColumns>();
            CustomGridColumns command_btn_group = new CustomGridColumns();
            command_btn_group.ActionFunction = new string[] { "btn_Detail_Kullanici", "btn_Delete_Kullanici" };
            command_btn_list.Add(command_btn_group);

            _gridOptions.commandColumns = command_btn_list;

            #endregion

            _gridOptions.Source = _userProfileService.GetAll();
            _gridOptions.IsNEwButton = true;
            _gridOptions.NewButtonUrl = new PageLink { DisplayName = "Yeni", Controller = "Auth", Action = "Register" };

            usersViewModel.Users = _gridOptions;

            return View(usersViewModel);
        }

        public IActionResult ShowUsersGridComponent()
        {
            _gridOptions.Source = _userService.GetAll();
            _gridOptions.IsNEwButton = true;

            return View(_gridOptions);
        }

        [EncryptedActionParameter(typeof(Guid))]
        public IActionResult Detail(Guid? id = null)
        {
            UserProfileViewModel userProfileViewModel = new UserProfileViewModel();

            userProfileViewModel.PageTitleOptions = new PageTitleOptions
            {
                Link1 = new PageLink { DisplayName = "AnaSayfa", Controller = "Home", Action = "Index" },
                Link2 = new PageLink { DisplayName = "Kullanıcılar", Controller = "User", Action = "ShowUsers" },
                Link3 = new PageLink { DisplayName = "Profil", Controller = "User", Action = "Detail" }
            };

            if (id == null && User.Identity.IsAuthenticated)
            {
                var loggedInUser = HttpContext.User;
                var loggedInUserName = loggedInUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name).Value;
                id = new Guid(loggedInUser.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier).Value);
            }

            var userProfile = new UserProfile();

            userProfile = _userProfileService.GetByUserId(id.Value);


            userProfileViewModel.UserProfile = userProfile;

            userProfileViewModel.Directorships = PopulateDirectorships();
            userProfileViewModel.Departments = PopulateDepartments();
            userProfileViewModel.Units = PopulateUnits();

            userProfileViewModel.DirectorshipId = userProfile.DirectorshipId;
            userProfileViewModel.DepartmentId = userProfile.DepartmentId;
            userProfileViewModel.UnitId = userProfile.UnitId;

            return View(userProfileViewModel);

        }

        [HttpPost]
        public IActionResult Update(UserProfileViewModel user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (user == null)
                        NotFound();

                    var userProfile = _userProfileService.GetByUserId(user.UserProfile.UserId);

                    userProfile.FirstName = user.UserProfile.FirstName;
                    userProfile.LastName = user.UserProfile.LastName;
                    userProfile.Email = user.UserProfile.Email;
                    userProfile.PhoneNumber = user.UserProfile.PhoneNumber;

                    userProfile.DepartmentId = user.DepartmentId;
                    userProfile.DirectorshipId = user.DirectorshipId;
                    userProfile.UnitId = user.UnitId;

                    _userProfileService.Update(userProfile);
                }
                else
                    return View(user);
              
                return RedirectToAction("Detail","User", new { id = user.UserProfile.UserId });
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [EncryptedActionParameter(typeof(Guid))]
        public IActionResult Delete(Guid id)
        {
            try
            {
                if (id == null)
                    return NotFound();

                var userProfile = _userProfileService.GetByUserId(id);
                if (userProfile == null)
                    return NotFound();

                _userProfileService.Delete(userProfile);
                _userService.Delete(new User() { UserId = userProfile.UserId });

                return RedirectToAction("ShowUsers", "User");
            }
            catch (Exception)
            {
                throw;
            }
        }

        public ActionResult Index()
        {
            return View();
        }

        private List<SelectListItem> PopulateDirectorships()
        {
            #region AssignUsers
            var userList = _directorshipService.GetAll();

            List<SelectListItem> assignUsers = new List<SelectListItem>();

            userList.ForEach(i =>
            {
                assignUsers.Add(new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
            });

            return assignUsers;
            #endregion
        }

        private List<SelectListItem> PopulateDepartments()
        {
            #region AssignUsers
            var userList = _departmentService.GetAll();

            List<SelectListItem> assignUsers = new List<SelectListItem>();

            userList.ForEach(i =>
            {
                assignUsers.Add(new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
            });

            return assignUsers;
            #endregion
        }

        private List<SelectListItem> PopulateUnits()
        {
            #region AssignUsers
            var userList = _unitService.GetAll();

            List<SelectListItem> assignUsers = new List<SelectListItem>();

            userList.ForEach(i =>
            {
                assignUsers.Add(new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
            });

            return assignUsers;
            #endregion
        }

        public JsonResult GetUsers(string id)
        {
            var userList = _userProfileService.GetByUnitId(Convert.ToInt32(id));

            List<SelectListItem> assignUsers = new List<SelectListItem>();

            userList.ForEach(i =>
            {
                assignUsers.Add(new SelectListItem { Text = i.FullName, Value = i.UserId.ToString() });
            });


            return Json(new SelectList(assignUsers, "Value", "Text"));
        }
    }
}
