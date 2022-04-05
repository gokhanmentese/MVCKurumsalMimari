using Microsoft.AspNetCore.Mvc;
using Mvc.WebUI.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.WebUI.ViewComponents
{
    public class CKEditorViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(CKEditorViewModel ck)
        {
            return View(ck);
        }
    }
}
