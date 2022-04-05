using Entities.Concrete;
using Mvc.WebUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.WebUI.ViewModel
{
    public class PostOutgoingViewModel
    {
        public PageTitleOptions PageTitleOptions { get; set; }

        public List<Email> OutgoingEmails { get; set; }

        public Email SelectedEmail { get; set; }

        public PagingInfo PagingInfo { get; set; }
    }
}
