using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.DependencyInjection;
using Mvc.WebUI.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Mvc.WebUI.HtmlHelpers
{
    public static class PagingHelpers
    {
        public static IUrlHelper GetUrlHelper(this IHtmlHelper html)
        {
            var urlFactory = html.ViewContext.HttpContext.RequestServices.GetRequiredService<IUrlHelperFactory>();
            var actionAccessor = html.ViewContext.HttpContext.RequestServices.GetRequiredService<IActionContextAccessor>();
            return urlFactory.GetUrlHelper(actionAccessor.ActionContext);
        }

        public static IHtmlContent Pager(this IHtmlHelper html, PagingInfo pagingInfo, string controllername, string actionname)
        {
            var stringBuilder = new StringBuilder();
            int totalPage = (int)Math.Ceiling((decimal)pagingInfo.TotalItems / pagingInfo.ItemsPerPage);


            var ulTag = new TagBuilder("ul");
            ulTag.AddCssClass("pagination pagination-sm justify-content-end");

            if (totalPage > 1)
            {
                #region Add LiTag
                TagBuilder previusLiTag = new TagBuilder("li");

                var previousAnchor = new TagBuilder("a");
                previousAnchor.MergeAttribute("tabindex", "-1");
                previousAnchor.AddCssClass("page-link");
                previousAnchor.InnerHtml.Append("<<");

                if (pagingInfo.CurrentPage > 1)
                    previousAnchor.MergeAttribute("href", string.Format("/{0}/{1}/?page={2}",controllername,actionname, pagingInfo.CurrentPage - 1));
                else
                    previusLiTag.AddCssClass("page-item disabled");

                previusLiTag.InnerHtml.AppendHtml(previousAnchor);

                ulTag.InnerHtml.AppendHtml(previusLiTag);

                for (int i = 1; i <= totalPage; i++)
                {
                    var anchor = new TagBuilder("a");
                    anchor.MergeAttribute("href", string.Format("/{0}/{1}/?page={2}", controllername, actionname, i));
                    anchor.InnerHtml.Append(i.ToString());
                    anchor.Attributes.Add("title", i.ToString());
                    anchor.AddCssClass("page-link");

                    TagBuilder liTag = new TagBuilder("li");
                    liTag.InnerHtml.AppendHtml(anchor);
                    liTag.AddCssClass("page-item");

                    if (pagingInfo.CurrentPage == i)
                    {
                        liTag.AddCssClass("active");
                    }

                    ulTag.InnerHtml.AppendHtml(liTag);

                    stringBuilder.Append(anchor);
                }

                TagBuilder nextLiTag = new TagBuilder("li");
                nextLiTag.AddCssClass("page-item");


                var nextAnchor = new TagBuilder("a");
                nextAnchor.MergeAttribute("tabindex", "-1");
                nextAnchor.AddCssClass("page-link");
                nextAnchor.InnerHtml.Append(">>");

                nextLiTag.InnerHtml.AppendHtml(nextAnchor);

                if (pagingInfo.CurrentPage != totalPage)
                    nextAnchor.MergeAttribute("href", string.Format("/{0}/{1}/?page={2}", controllername, actionname, pagingInfo.CurrentPage + 1));
                else
                    nextLiTag.AddCssClass("page-item disabled");

                ulTag.InnerHtml.AppendHtml(nextLiTag);
                #endregion
            }

            return ulTag; // new HtmlString(stringBuilder.ToString());
        }

    }
}
