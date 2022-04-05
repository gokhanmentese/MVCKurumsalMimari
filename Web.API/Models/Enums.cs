using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace Web.API.Models
{
    public class Enums
    {
        public enum MessageCode
        {
            [Description("İşlem başarılı")]
            Succeessful=100,

            [Description("İşlem başarısız")]
            UnSucceessful = 101,

            [Description("İşlem hatalı")]
            Error =102,

            [Description("Gelen TC Kimlik no uyumsuz.")]
            TcError =103,

            [Description("Kullanıcı adı veya parola hatalı.")]
            Authorization = 400
        }
    }
}
