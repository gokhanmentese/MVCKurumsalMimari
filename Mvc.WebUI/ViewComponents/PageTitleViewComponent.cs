using Microsoft.AspNetCore.Mvc;
using Mvc.WebUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.WebUI.ViewComponents
{
    public class PageTitleViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(PageTitleOptions options)
        {
            return View(options);
        }
    }
}
