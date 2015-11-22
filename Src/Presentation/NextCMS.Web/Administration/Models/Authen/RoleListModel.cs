using FluentValidation.Attributes;
using NextCMS.Admin.Validators.Authen;
using NextCMS.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextCMS.Admin.Models.Authen
{
    public class RoleListModel : BaseNextCMSModel
    {
        public RoleListModel()
        {
            this.Active = true;
        }

        public string Name { get; set; }
        public string SystemName { get; set; }
        public bool Active { get; set; }
    }
}