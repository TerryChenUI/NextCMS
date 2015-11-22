using NextCMS.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextCMS.Admin.Models.Catalog
{
    public class CategoryListModel : BaseNextCMSModel
    {
        public string Name { get; set; }

        public bool Published { get; set; }

        public int DisplayOrder { get; set; }

        public DateTime CreatedOnDate { get; set; }

    }
}