using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextCMS.Admin.Models.Settings
{
    public class ArticleSettingsModel
    {
        public int ArticlePageSize { get; set; }

        public int CommentPageSize { get; set; }

        public bool AllowComment { get; set; }

        public int LatestCommentPageSize { get; set; }

        public int HotCommentPageSize { get; set; }

        public int HotArticlePageSize { get; set; }
    }
}