using NextCMS.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextCMS.Web.Models.Articles
{
    public partial class ArticleModel : BaseNextCMSModel
    {
        public string Title { get; set; }

        public string PictureCoverImg { get; set; }

        public string Author { get; set; }

        public string CreateDate { get; set; }

        public int Views { get; set; }

        public int CommentCount{ get;set; }

        public string ShortDescription { get; set; }

        public string FullDescription { get; set; }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }
    }
}