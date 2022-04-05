using Core.Utilities.IoC;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using Microsoft.AspNetCore.WebUtilities;
using Core.Extensions;

namespace Mvc.WebUI.Attributes
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class EncryptedActionParameterAttribute : ActionFilterAttribute
    {
        private IHttpContextAccessor _httpContextAccessor;
        private Type _type;

        public EncryptedActionParameterAttribute(Type type)
        {
            _httpContextAccessor = ServiceTool.ServiceProvider.GetService<IHttpContextAccessor>();
            _type = type;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            Dictionary<string, object> decryptedParameters = new Dictionary<string, object>();
            if (_httpContextAccessor.HttpContext.Request.QueryString.HasValue)
            {
                //var slug = context.RouteData.Values["slug"] as string;
                //if (slug != null)
                //{
                //    int id;
                //    ////Slugs.TryGetValue(slug, out id);
                //    context.ActionArguments["id"] = id;
                //}

                var queryString = QueryHelpers.ParseQuery(_httpContextAccessor.HttpContext.Request.QueryString.Value);

                var queryStringVal = _httpContextAccessor.HttpContext.Request.QueryString.Value;
                string encryptedQueryString = queryStringVal.Split("?q=")[1];
                string decrptedString = SecurityExtensions.DecryptText(encryptedQueryString.ToString());

                string[] paramsArrs = decrptedString.Split('?');

                for (int i = 0; i < paramsArrs.Length; i++)
                {
                    string[] paramArr = paramsArrs[i].Split('=');

                    if (_type == typeof(string))
                        decryptedParameters.Add(paramArr[0], paramArr[1]);
                    else if (_type == typeof(Guid))
                        decryptedParameters.Add(paramArr[0], new Guid(paramArr[1]));
                    else if (_type == typeof(int))
                        decryptedParameters.Add(paramArr[0], Convert.ToInt32(paramArr[1]));
                }
            }

            for (int i = 0; i < decryptedParameters.Count; i++)
            {
                context.ActionArguments[decryptedParameters.Keys.ElementAt(i)] = decryptedParameters.Values.ElementAt(i);
            }

            base.OnActionExecuting(context);
        }

        
    }
}
