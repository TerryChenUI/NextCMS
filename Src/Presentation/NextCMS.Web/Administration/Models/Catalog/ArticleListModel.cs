using NextCMS.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextCMS.Admin.Models.Catalog
{
    public class ArticleListModel : BaseNextCMSModel
    {
        public string Title { get; set; }

        public int Views { get; set; }

        public int CommentCount { get; set; }

        public bool Published { get; set; }

        public string CreateDate { get; set; }
    }
}