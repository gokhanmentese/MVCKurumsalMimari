using Entities.Concrete;
using Interfaces;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class DropdownManager : IDropdownService
    {
        private readonly IDirectorshipService _directorshipService;
        private readonly IDepartmentService _departmentService;
        private readonly IUnitService _unitService;
        private readonly IUserProfileService _userProfileService;
        private readonly ICategoryService _categoryService;


        public DropdownManager(IDirectorshipService directorshipService, IDepartmentService departmentService, IUnitService unitService, IUserProfileService userProfileService, ICategoryService categoryService)
        {
            _directorshipService = directorshipService;
            _departmentService = departmentService;
            _unitService = unitService;
            _userProfileService = userProfileService;
            _categoryService = categoryService;
        }

        public List<SelectListItem> PopulateDirectorships()
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

        public List<SelectListItem> PopulateDepartments(int? directorshipId = null)
        {
            #region Departments
            List<Department> userList = new List<Department>();

            if (directorshipId == null)
                userList = _departmentService.GetAll();
            else
                userList = _departmentService.GetByDirectorhipId(directorshipId.Value);

            List<SelectListItem> assignUsers = new List<SelectListItem>();

            userList.ForEach(i =>
            {
                assignUsers.Add(new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
            });

            return assignUsers;
            #endregion
        }

        public List<SelectListItem> PopulateUnits(int? departmentId = null)
        {
            #region Units
            List<Unit> userList = new List<Unit>();

            if (departmentId == null)
                userList = _unitService.GetAll();
            else
                userList = _unitService.GetByDepartmentId(departmentId.Value);

            List<SelectListItem> assignUsers = new List<SelectListItem>();

            userList.ForEach(i =>
            {
                assignUsers.Add(new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
            });

            return assignUsers;
            #endregion
        }

        public List<SelectListItem> PopulateAssignUsers(int? unitId = null)
        {
            #region AssignUsers
            List<UserProfile> userList = new List<UserProfile>();

            if (unitId == null)
                userList = _userProfileService.GetAll();
            else
                userList = _userProfileService.GetByUnitId(unitId.Value);

            List<SelectListItem> assignUsers = new List<SelectListItem>();

            userList.ForEach(i =>
            {
                assignUsers.Add(new SelectListItem { Text = i.FullName, Value = i.UserId.ToString() });
            });

            return assignUsers;
            #endregion
        }

        public List<SelectListItem> PopulateCategories()
        {
            #region Categories
            List<Category> categoryList = _categoryService.GetAll();

            List<SelectListItem> categories = new List<SelectListItem>();

            categoryList.ForEach(i =>
            {
                categories.Add(new SelectListItem { Text = i.Name, Value = i.Id.ToString() });
            });

            return categories;
            #endregion
        }

      
    }
}
