using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;

namespace NextCMS.Web.Framework.UI
{
    public static class HtmlHelperExtension
    {
        private const string SCRIPTBLOCK_BUILDER = "ScriptBlockBuilder";

        /// <summary>
        /// From Partial View， register script into Context, then will put them at the end of Page HTML.
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <param name="template"></param>
        /// <returns></returns>
        public static MvcHtmlString WriteScriptBlock(this HtmlHelper htmlHelper, Func<dynamic, HelperResult> template)
        {
            var context = htmlHelper.ViewContext.HttpContext;
            if (!context.Request.IsAjaxRequest())
            {
                var scriptBuilder = context.Items[SCRIPTBLOCK_BUILDER]
                     as StringBuilder ?? new StringBuilder();

                scriptBuilder.Append(template(null).ToHtmlString());

                context.Items[SCRIPTBLOCK_BUILDER] = scriptBuilder;

                return new MvcHtmlString(string.Empty);
            }
            return new MvcHtmlString(template(null).ToHtmlString());
        }

        /// <summary>
        /// Write the Scripts that register from Some Partial Views, then output them to Page Bottom
        /// </summary>
        /// <param name="htmlHelper"></param>
        /// <returns></returns>
        public static MvcHtmlString RenderScriptBlocks(this HtmlHelper htmlHelper)
        {
            var scriptBuilder = htmlHelper.ViewContext.HttpContext.Items[SCRIPTBLOCK_BUILDER]
                   as StringBuilder ?? new StringBuilder();

            return new MvcHtmlString(scriptBuilder.ToString());
        }
    }
}