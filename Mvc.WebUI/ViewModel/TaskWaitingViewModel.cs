using Mvc.WebUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.WebUI.ViewModel
{
    public class TaskWaitingViewModel
    {
        public PageTitleOptions PageTitleOptions { get; set; }

        public GridOptions WaitingTasks { get; set; }
    }
}
