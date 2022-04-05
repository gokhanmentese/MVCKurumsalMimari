using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.WebUI.Model
{
    public class PagingInfo
    {
        public int ItemsPerPage { get; set; }

        public int TotalItems { get; set; }

        public int CurrentPage { get; set; }
    }
}
