using Core.Attributes;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using Mvc.WebUI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;


namespace Mvc.WebUI.ViewModel
{
    public class TaskViewModel
    {
        public PageTitleOptions PageTitleOptions { get; set; }
        public Task Task { get; set; }

        public string UserDescription { get; set; }

        public List<SelectListItem> Categories { get; set; }

        public List<SelectListItem> AssignUsers { get; set; }

        public List<SelectListItem> Directorships { get; set; }

        public List<SelectListItem> Departments { get; set; }

        public List<SelectListItem> Units { get; set; }

        //public int? CategoryId { get; set; }

        public int? DirectorshipId { get; set; }

        public int? DepartmentId { get; set; }

        public int? UnitId { get; set; }

        //[Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".doc", ".docx", ".xls", ".xlsx",".pdf", ".ppt", ".jpg", ".png", ".rar" })]
        [MaxFileSize(25 * 1024 * 1024)]
        public List<IFormFile> TaskFiles { get; set; }

        //[Required(ErrorMessage = "Please select a file.")]
        [DataType(DataType.Upload)]
        [AllowedExtensions(new string[] { ".doc", ".docx", ".xls", ".xlsx", ".pdf", ".ppt", ".jpg", ".png", ".rar" })]
        [MaxFileSize(25 * 1024 * 1024)]
        public List<IFormFile> UserTaskFiles { get; set; }

        public bool IsOwner { get; set; }

        public GridOptions TaskUsers { get; set; }
    }
}
