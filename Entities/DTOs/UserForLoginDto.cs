using System;
using System.Collections.Generic;
using System.Text;
using Core.Entities;

namespace Entities.Dtos
{
    public class UserForLoginDto:IDto
    {
        public string IdentityNumber { get; set; }
        public string Password { get; set; }
    }
}
