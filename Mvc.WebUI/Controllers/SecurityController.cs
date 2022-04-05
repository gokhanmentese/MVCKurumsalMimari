using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Mvc.WebUI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SecurityController : ControllerBase
    {
        [HttpGet("{key}")]
        public ActionResult<IEnumerable<string>> Get(string key)
        {
            var SCollection = new ServiceCollection();

            //add protection services
            SCollection.AddDataProtection();
            var LockerKey = SCollection.BuildServiceProvider();

            var locker = ActivatorUtilities.CreateInstance<Security>(LockerKey);
            string encryptKey = locker.Encrypt(key);
            string deencryptKey = locker.Decrypt(encryptKey);
            return new string[] { encryptKey, deencryptKey };
        }
    }
}
