using NextCMS.Admin.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextCMS.Admin.Models.Catalog
{
    public class ArticleSearchModel : DataTableParameter
    {
        public ArticleSearchModel()
        {
            this.SearchCategories = new List<SelectListItem> { 
                new SelectListItem { Text = "请选择", Value = "0"}
            };
        }

        public string SearchTitle { get; set; }

        public int SearchCategoryId { get; set; }

        public List<SelectListItem> SearchCategories { get; set; }
    }
}