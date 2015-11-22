using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextCMS.Core.Domain.Settings
{

    public class GeneralSettings : ISettings
    {
        /// <summary>
        /// 网站名称
        /// </summary>
        public string SiteTitle { get; set; }

        /// <summary>
        /// 标题分隔符
        /// </summary>
        public string Separator { get; set; }

        /// <summary>
        /// SEO标题
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// SEO关键字
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// SEO描述
        /// </summary>
        public string MetaDescription { get; set; }
    }
}
