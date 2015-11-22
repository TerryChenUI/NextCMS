using MvcPaging;
using NextCMS.Core.Domain.Catalog;
using NextCMS.Web.Models.Articles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextCMS.Web.Models.Home
{
    public partial class HomeModel
    {
        public HomeModel()
        {
            this.HotArticles = new List<ArticleModel>();
            this.CommentArticles = new List<ArticleModel>();
        }

        public IPagedList<Article> Articles { get; set; }
        public IEnumerable<ArticleModel> RecommandArticles { get; set; }
        public IEnumerable<ArticleModel> HotArticles { get; set; }
        public IEnumerable<ArticleModel> CommentArticles { get; set; }
    }
}