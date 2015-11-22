using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextCMS.Web.Models.Articles
{
    public partial class ArticleDetailModel
    {
        public ArticleDetailModel()
        {
            this.HotArticles = new List<ArticleModel>();
            this.CommentArticles = new List<ArticleModel>();
        }

        public ArticleModel Article { get; set; }
        public IEnumerable<ArticleModel> HotArticles { get; set; }
        public IEnumerable<ArticleModel> CommentArticles { get; set; }
    }
}