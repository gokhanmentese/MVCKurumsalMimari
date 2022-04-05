using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Core.Entities.Concrete;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Web.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {

        private readonly IAuthenticationDal _authenticationDal;

        public AuthenticationController(IAuthenticationDal authenticationDal)
        {
            _authenticationDal = authenticationDal;
        }

        public ActionResult<User> Authenticate(User user)
        {
            return _authenticationDal.Get(a => a.IdentityNumber == user.IdentityNumber);
        }

    }
}
