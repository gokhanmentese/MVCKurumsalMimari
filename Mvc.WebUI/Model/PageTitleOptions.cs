using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.WebUI.Model
{
    public class PageTitleOptions
    {
        public PageLink Link1 { get; set; }
        public PageLink Link2 { get; set; }
        public PageLink Link3 { get; set; }

    }

    public class PageLink
    {
        public string DisplayName { get; set; }
        public string Controller { get; set; }

        public string Action { get; set; }
    }
}
