using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextCMS.Admin.Models.Authen
{
    public class LoginModel
    {
        public bool CheckoutAsGuest { get; set; }

        [AllowHtml]
        public string Email { get; set; }

        [AllowHtml]
        public string UserNameOrEmail { get; set; }

        [AllowHtml]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}