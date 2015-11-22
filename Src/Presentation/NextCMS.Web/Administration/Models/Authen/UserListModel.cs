using NextCMS.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextCMS.Admin.Models.Authen
{
    public partial class UserListModel : BaseNextCMSModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public string RegisterDate { get; set; }

        public ICollection<int> SelectedRoles { get; set; }
    }
}