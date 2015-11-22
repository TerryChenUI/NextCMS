using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextCMS.Admin.Models.Authen
{
    public class PermissionRoleModel
    {
        public PermissionRoleModel()
        {
            AvailablePermissions = new List<PermissionModel>();
            AvailableRoles = new List<RoleModel>();
            Allowed = new Dictionary<string, IDictionary<int, bool>>();
        }
        public IList<PermissionModel> AvailablePermissions { get; set; }
        public IList<RoleModel> AvailableRoles { get; set; }

        //[permission] / [role id] / [allowed]
        public IDictionary<string, IDictionary<int, bool>> Allowed { get; set; }
    }
}