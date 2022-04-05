using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.WebUI.ViewModel
{
    public class TaskFileViewModel
    {
        public Guid TaskId { get; set; }

        public List<TaskFile> TaskFiles { get; set; }

        [Display(Name ="Dosya Seç")]
        public IFormFile FormFile { get; set; }
    }
}
