using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Mvc.WebUI.ViewComponents
{
    [ViewComponent]
    public class CategoryViewComponent : ViewComponent
    {
        private readonly IUserService _userService;

        public CategoryViewComponent(IUserService userService)
        {
            _userService = userService;
        }

        public IViewComponentResult Invoke()
        {
            return View(_userService.GetAll());
        }
    }
}
