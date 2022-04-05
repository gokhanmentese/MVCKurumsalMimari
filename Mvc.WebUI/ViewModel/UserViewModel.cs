using Microsoft.AspNetCore.Mvc;
using Mvc.WebUI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Mvc.WebUI.ViewModel
{
    public class UserViewModel
    {

        public PageTitleOptions PageTitleOptions { get; set; }

        [Required(ErrorMessage = "Bu Alan Boş Geçilemez")]
        [StringLength(11)]
        [Display(Name="TC Kimlik Numarası")]
        public string IdentityNumber { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Bu Alan Boş Geçilemez")]
        [StringLength(20, MinimumLength = 5)]
        [Display(Name = "Kullanıcı Şifre  ")]
        public string Password { get; set; }

        public string Email { get; set; }
    }

}