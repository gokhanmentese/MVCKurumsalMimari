using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Aspects.Autofac.Transaction;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.WebUI.ViewModel;

namespace Mvc.WebUI.Controllers
{
    public class TaskFileController : Controller
    {
        private readonly ITaskFileService _taskFileService;
        private readonly IFileService _fileService;

        public TaskFileController(ITaskFileService taskFileService, IFileService fileService)
        {
            _taskFileService = taskFileService;
            _fileService = fileService;
        }

        public IActionResult Index()
        {
            return View();
        }

        

        public IActionResult Delete(Guid id)
        {
            if (id == Guid.Empty)
                return BadRequest();

            var taskFile = _taskFileService.GetById(id);
            if (taskFile != null)
            {
                _taskFileService.Delete(taskFile);

                //if (taskFile.FileId != Guid.Empty)
                //{
                //    var file = _fileService.GetById(taskFile.FileId);
                //    if (file != null)
                //    {
                //        if (System.IO.File.Exists(file.FilePath))
                //            System.IO.File.Delete(file.FilePath);

                //        _fileService.Delete(file);
                //    }
                //}
               
            }

            return Ok();
        }

        public IActionResult GetTaskFilesByTaskId(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var task = _taskFileService.GetByTaskId(id);
            if (task == null)
                return NotFound();

            return Json(task);
        }

        public IActionResult GetOwnerTaskFilesByTaskId(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var task = _taskFileService.GetOwnerTaskFilesByTaskId(id);
            if (task == null)
                return NotFound();

            return Json(task);
        }

        public IActionResult GetUserTaskFilesByTaskId(Guid id)
        {
            if (id == Guid.Empty)
                return NotFound();

            var task = _taskFileService.GetUserTaskFilesByTaskId(id);
            if (task == null)
                return NotFound();

            return Json(task);
        }
    }
}
