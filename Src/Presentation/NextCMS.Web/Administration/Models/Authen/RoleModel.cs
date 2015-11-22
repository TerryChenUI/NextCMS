using FluentValidation.Attributes;
using NextCMS.Admin.Validators.Authen;
using NextCMS.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextCMS.Admin.Models.Authen
{
    [Validator(typeof(RoleValidator))]
    public class RoleModel : BaseNextCMSModel
    {
        public RoleModel()
        {
            this.Active = true;
        }

        public string Name { get; set; }
        public string SystemName { get; set; }
        public bool Active { get; set; }
    }
}