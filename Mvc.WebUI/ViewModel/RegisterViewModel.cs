using Mvc.WebUI.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Mvc.WebUI.ViewModel
{
    public class RegisterViewModel
    {
        public PageTitleOptions PageTitleOptions { get; set; }

        [Required(ErrorMessage = "TC Kimlik Numarası alanı dolu olmalıdır")]
        [StringLength(11)]
        [Display(Name = "TC Kimlik Numarası")]
        public string IdentityNumber { get; set; }

        [Required(ErrorMessage = "Ad alanı dolu olmalıdır")]
        [MaxLength(50,ErrorMessage ="Maximum 50 karakter giriş yapılabilir")]
        [Display(Name ="Ad")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Soyad alanı dolu olmalıdır")]
        [MaxLength(50, ErrorMessage = "Maximum 50 karakter giriş yapılabilir")]
        [Display(Name = "Soyad")]
        public string LastName { get; set; }


        [DataType(DataType.EmailAddress, ErrorMessage = "Lütfen e-posta adresinizi geçerli bir formatta giriniz.")]
        [Required(ErrorMessage = "E-posta alanı dolu olmalıdır")]
        [Display(Name = "E-posta")]
        public string Email { get; set; }
    }
}
