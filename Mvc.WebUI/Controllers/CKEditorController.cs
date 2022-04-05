using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Mvc.WebUI.Model;
using Mvc.WebUI.ViewModel;

namespace Mvc.WebUI.Controllers
{
    public class CKEditorController : Controller
    {
        public IActionResult Demo()
        {
            return View();
        }

        public IActionResult ShowEditor()
        {
            CKEditorViewModel ck = new CKEditorViewModel {Body= "test html" };
            return View(ck);
        }

        //[Authorize(Roles = "Admin,User")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CKEditor cKEditor)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View();

                if (string.IsNullOrEmpty(cKEditor.Text))
                    NotFound();

                var str = cKEditor.Text.HtmlRemove();

                str = str.Replace("\n", String.Empty);
                str = str.Replace("\r", String.Empty);
                str = str.Replace("\t", String.Empty);

                str = Regex.Replace(str, @"\t\n\r", "");

                return RedirectToAction("Details", "Task", new { id =str });

            }
            catch (Exception ex)
            {
                return View();
            }
        }

        public static string StripHTML(string htmlString)
        {

            string pattern = @"<(.|\n)*?>";

            return Regex.Replace(htmlString, pattern, string.Empty);
        }
    }
}
