using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextCMS.Admin.Models.Settings
{
    public class GeneralSettingsModel
    {
        public string SiteTitle { get; set; }

        public string Separator { get; set; }

        public string MetaTitle { get; set; }

        public string MetaKeywords { get; set; }

        public string MetaDescription { get; set; }
    }
}