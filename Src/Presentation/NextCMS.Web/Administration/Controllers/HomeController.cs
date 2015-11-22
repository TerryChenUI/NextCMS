using NextCMS.Admin.Models.Authen;
using NextCMS.Core.Domain.Authen;
using NextCMS.Services.Authen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextCMS.Admin.Controllers
{
    public class HomeController : BaseAdminController
    {
        #region 私有字段

        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;

        #endregion

        #region 构造函数

        public HomeController(IUserService userService,
            IPermissionService permissionService)
        {
            this._userService = userService;
            this._permissionService = permissionService;
        }

        #endregion

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Login()
        {
            var model = new LoginModel();
            return View(model);
        }

        [HttpPost]
        public ActionResult Login(LoginModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var loginResult = _userService.ValidateUser(model.UserNameOrEmail, model.Password);
                switch (loginResult)
                {
                    case UserLoginResult.Successful:
                        {
                            var user = _userService.GetUserByUserNameOrEmail(model.UserNameOrEmail);

                            //sign in new user
                            _userService.SignIn(user, model.RememberMe);

                            //activity log
                            //_userActivityService.InsertActivity("PublicStore.Login", _localizationService.GetResource("ActivityLog.PublicStore.Login"), user);

                            if (!String.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                                return Redirect(returnUrl);
                            else
                                return RedirectToAction("Index");
                        }
                    case UserLoginResult.UserNotExist:
                        ModelState.AddModelError("", "该用户不存在");
                        break;
                    case UserLoginResult.Deleted:
                        ModelState.AddModelError("", "该用户已被删除");
                        break;
                    case UserLoginResult.NotActive:
                        ModelState.AddModelError("", "该用户没有被激活");
                        break;
                    case UserLoginResult.NotRegistered:
                        ModelState.AddModelError("", "该用户不存在");
                        break;
                    case UserLoginResult.WrongPassword:
                    default:
                        ModelState.AddModelError("", "密码错误");
                        break;
                }
            }
            return View(model);
        }


        //[NonAction]
        //protected virtual IList<SideBarMenuModel> SortMenuForTree(IList<Permission> permissions)
        //{
        //    var sideBarMenuModel = new List<SideBarMenuModel>();
        //    foreach (var permission in permissions)
        //    {

        //        sideBarMenuModel.Add(new SideBarMenuModel { 
        //            Name = permission.Name,
        //            Controller = permission.Controller,
        //            Action = permission.Action,
        //            Icon = permission.Icon,
        //            DisplayOrder = permission.DisplayOrder
        //        });

        //        foreach(var )

        //    }
        //}
    }
}