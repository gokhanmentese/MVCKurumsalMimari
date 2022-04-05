using Core.Extensions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Mvc.WebUI.HtmlHelpers
{
    public static class EncodedHelpers
    {
        public static IHtmlContent EncodedActionLink(this IHtmlHelper htmlHelper, string linkText, string actionName, string controllerName, object routeValues, object htmlAttributes)
        {
            string queryString = string.Empty;
            string htmlAttributesString = string.Empty;
            if (routeValues != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(routeValues);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    if (i > 0)
                    {
                        queryString += "?";
                    }
                    queryString += d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            if (htmlAttributes != null)
            {
                RouteValueDictionary d = new RouteValueDictionary(htmlAttributes);
                for (int i = 0; i < d.Keys.Count; i++)
                {
                    htmlAttributesString += " " + d.Keys.ElementAt(i) + "=" + d.Values.ElementAt(i);
                }
            }

            var aTag = new TagBuilder("a");
            aTag.MergeAttribute("href", string.Format("/{0}/{1}/?q={2}", controllerName, actionName, SecurityExtensions.EncryptText(queryString)));
            aTag.InnerHtml.Append(linkText);
           // aTag.AddCssClass("page-link");

            //<a href="/Answer?questionId=14">What is Entity Framework??</a>
            //StringBuilder ancor = new StringBuilder();
            //ancor.Append("<a ");
            //if (htmlAttributesString != string.Empty)
            //{
            //    ancor.Append(htmlAttributesString);
            //}

            //ancor.Append(" href='");
            //if (controllerName != string.Empty)
            //{
            //    ancor.Append("/" + controllerName);
            //}

            //if (actionName != "Index")
            //{
            //    ancor.Append("/" + actionName);
            //}
            //if (queryString != string.Empty)
            //{
            //    ancor.Append("?q=" + Encrypt(queryString));
            //}
            //ancor.Append("'");
            //ancor.Append(">");
            //ancor.Append(linkText);
            //ancor.Append("</a>");

            return aTag;
        }
    }
}
