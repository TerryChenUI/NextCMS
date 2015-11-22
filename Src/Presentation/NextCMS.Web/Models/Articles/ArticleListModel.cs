using MvcPaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NextCMS.Core.Domain.Catalog;

namespace NextCMS.Web.Models.Articles
{
    public class ArticleListModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Description { get; set; }

        public IPagedList<Article> Articles { get; set; }
    }
}