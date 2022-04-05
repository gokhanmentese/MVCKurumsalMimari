using Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.WebUI.Controllers
{

    public class ErrorController : Controller
    {
        [Route("/error")]
        [AllowAnonymous]
        public IActionResult Error()
        {
            // Retrieve error information in case of internal errors
            var exceptionDetails = HttpContext.Features.Get<IExceptionHandlerPathFeature>();

            if (exceptionDetails !=null)
            {

                // Use the information about the exception 
                ViewBag.Stacktrace = exceptionDetails.Error.StackTrace;
                ViewBag.ExceptionMessage = exceptionDetails.Error.Message;
                ViewBag.ExceptionPath = exceptionDetails.Path;
            }


            return View("Error");
        }

        [Route("Error/{statusCode}")]
        public IActionResult HttpStatusCodeHandler(int statusCode)
        {
            var statusCodeResult = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();

            switch (statusCode)
            {
                case 404:
                    ViewBag.ErrorMessage = "";
                    ViewBag.Path = "";
                    ViewBag.QS = statusCodeResult.OriginalQueryString;
                    break;
                default:
                    break;
            }

            return View("NotFound");
        }

        [Route("/error-local-development")]
        public IActionResult ErrorLocalDevelopment([FromServices] IWebHostEnvironment webHostEnvironment)
        {
            if (webHostEnvironment.EnvironmentName != "Development")
            {
                throw new InvalidOperationException(
                    "This shouldn't be invoked in non-development environments.");
            }

            var context = HttpContext.Features.Get<IExceptionHandlerFeature>();

            return Problem(
                detail: context.Error.StackTrace,
                title: context.Error.Message);
        }
    }
}
