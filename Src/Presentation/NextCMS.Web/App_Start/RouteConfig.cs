using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace NextCMS.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            //routes.MapRoute("HomeArticleList",
            //                "article/list",
            //                new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            //                new[] { "NextCMS.Web.Controllers" });

            routes.MapRoute("ArticleList",
                            "article/list/{id}",
                            new { controller = "Article", action = "List", id = UrlParameter.Optional },
                            new[] { "NextCMS.Web.Controllers" });

            routes.MapRoute("Article",
                            "article/{id}",
                            new { controller = "Article", action = "Detail", id = UrlParameter.Optional },
                            new[] { "NextCMS.Web.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional },
                namespaces: new[] { "NextCMS.Web.Controllers" }
            );
        }
    }
}
