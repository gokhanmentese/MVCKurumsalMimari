using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Business.Constants;
using Core.Utilities.Results;
using Entities.Concrete;
using Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Mvc.WebUI.Controllers
{
    public class DownloadController : Controller
    {
        private readonly IDownloadService _downloadService;

        public DownloadController(IDownloadService downloadService)
        {
            _downloadService = downloadService;
        }


        public IActionResult DownloadFile(Guid id)
        {
            IDataResult<DownloadFile> result = _downloadService.FileDownload(id);

            if (result.Success)
            {
                return File(result.Data.MemoryStream, result.Data.ContentType,result.Data.DownloadName);
            }
            else
            {
                return NotFound(result.Message);
            }
        }
    }
}
