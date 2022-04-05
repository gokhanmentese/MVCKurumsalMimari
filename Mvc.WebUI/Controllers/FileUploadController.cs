using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Entities.Concrete;
using Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Mvc.WebUI.Controllers
{
    public class FileUploadController : Controller
    {
        private readonly ITaskService _taskService;
        private readonly IHostEnvironment _hostEnvironment;

        public FileUploadController(ITaskService taskService, IHostEnvironment hostEnvironment)
        {
            _taskService = taskService;
            _hostEnvironment = hostEnvironment;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        public async System.Threading.Tasks.Task<IActionResult> LoadFile(List<IFormFile> files)
        {
            long size = files.Sum(f => f.Length);

            var filePaths = new List<string>();
            foreach (var formFile in files)
            {
                if (formFile.Length > 0)
                {
                    // full path to file in temp location
                    var filePath = Path.GetTempFileName(); //we are using Temp file name just for the example. Add your own file path.
                    filePaths.Add(filePath);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await formFile.CopyToAsync(stream);
                    }
                }
            }

            Task task = _taskService.GetById(Guid.Empty);
            List<TaskFile> taskFiles = new List<TaskFile>()
            {
             new TaskFile { FileName="Test1" },
             new TaskFile { FileName="Test2" }
            };

            task.TaskFiles = taskFiles;

            //return Ok(new { count = files.Count, size, filePaths });
            return PartialView("TaskFile", task.TaskFiles);
        }
    }
}
