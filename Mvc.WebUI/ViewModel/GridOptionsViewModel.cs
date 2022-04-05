using Mvc.WebUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.WebUI.ViewModel
{
    public class GridOptionsViewModel
    {
        public GridOptions WaitingTasks { get; set; }

        public GridOptions ClosedTasks { get; set; }
    }
}
