using FluentValidation.Attributes;
using NextCMS.Admin.Validators.Catalog;
using NextCMS.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextCMS.Admin.Models.Catalog
{
    [Validator(typeof(TagValidator))]
    public partial class TagModel : BaseNextCMSModel
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }
}