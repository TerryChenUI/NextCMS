using FluentValidation.Attributes;
using NextCMS.Admin.Validators.Authen;
using NextCMS.Web.Framework;
using NextCMS.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextCMS.Admin.Models.Authen
{
    [Validator(typeof(UserValidator))]
    public partial class UserModel : BaseNextCMSModel
    {
        public UserModel()
        {
            this.Active = true;

            this.Roles = new List<KeyValueModel>();
            this.SelectedRoles = new List<int>();
        }

        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Phone { get; set; }
        public bool Active { get; set; }
        public string LastIpAddress { get; set; }
        public string LastLoginDate { get; set; }
        public string RegisterDate { get; set; }
        public string UpdateDate { get; set; }
        public string LastActivityDate { get; set; }

        public ICollection<KeyValueModel> Roles { get; set; }

        [KeyValue(DisplayProperty = "Roles")]
        public ICollection<int> SelectedRoles { get; set; }
    }
}