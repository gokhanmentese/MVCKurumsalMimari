using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Entities.Concrete;
using Microsoft.AspNetCore.Mvc;
using Mvc.WebUI.Infastructure;

namespace Mvc.WebUI.Controllers
{
    public class WebApiClientController : Controller
    {
        public IActionResult Index()
        {
            IEnumerable<Department> departmentList;

            HttpResponseMessage responseMessage = APIProxy.webApiClient.GetAsync("departments").Result;

            string json =  responseMessage.Content.ReadAsStringAsync().Result;


            return View();
        }
    }
}
