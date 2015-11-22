using NextCMS.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextCMS.Admin.Models.Authen
{
    public class PermissionModel : BaseNextCMSModel
    {
        public PermissionModel()
        {
            this.Active = true;
            this.AvailablePermissions = new List<SelectListItem>();
            this.ChildPermissions = new List<PermissionModel>();
        }

        public int ParentId { get; set; }

        public string Name { get; set; }

        public string IndexKey { get; set; }

        public string Area 
        {
            get { return "Admin"; }
            set { value = "Admin"; }
        }

        public string Controller { get; set; }

        public string Action { get; set; }

        public string Icon { get; set; }

        public string DisplayOrder { get; set; }

        public bool Active { get; set; }

        public bool Deleted { get; set; }

        public List<PermissionModel> ChildPermissions { get; set; }

        public List<SelectListItem> AvailablePermissions { get; set; }
    }
}