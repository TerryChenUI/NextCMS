using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextCMS.Web.Extensions
{
    public class ThemeableRazorViewEngine : RazorViewEngine
    {
        public ThemeableRazorViewEngine()
        {
            AreaViewLocationFormats = new[]
                                          {
                                              //admin
                                              "~/Administration/Views/{1}/{0}.cshtml", 
                                              "~/Administration/Views/Shared/{0}.cshtml"
                                          };

            AreaMasterLocationFormats = new[]
                                            {
                                                //Admin
                                                "~/Administration/Views/{1}/{0}.cshtml", 
                                                "~/Administration/Views/Shared/{0}.cshtml"
                                            };

            ViewLocationFormats = new[]
                                      {
                                            //default
                                            "~/Views/{1}/{0}.cshtml", 
                                            "~/Views/Shared/{0}.cshtml",

                                            //Admin
                                            "~/Administration/Views/{1}/{0}.cshtml",
                                            "~/Administration/Views/Shared/{0}.cshtml"
                                      };

            MasterLocationFormats = new[]
                                        {
                                            //default
                                            "~/Views/{1}/{0}.cshtml", 
                                            "~/Views/Shared/{0}.cshtml"
                                        };

            AreaPartialViewLocationFormats = AreaViewLocationFormats;
            PartialViewLocationFormats = ViewLocationFormats;
        }
    }
}