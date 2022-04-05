using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Entities.Concrete;
using Interfaces;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailTemplatesController : ControllerBase
    {
        private readonly IEmailTemplateService _emailTemplateService;
        private readonly IHostEnvironment _hostEnvironment;
        public EmailTemplatesController(IEmailTemplateService emailTemplateService , IHostEnvironment hostEnvironment)
        {
            _emailTemplateService = emailTemplateService;
            _hostEnvironment = hostEnvironment;
        }
        public ActionResult<List<EmailTemplate>> GetAll()
        {
            var filePath = Path.Combine(_hostEnvironment.ContentRootPath, "Download", $"{""}.ext");

            return _emailTemplateService.GetAll();
        }
    }
}
