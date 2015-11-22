using NextCMS.Admin.Models.Authen;
using NextCMS.Admin.Models.Common;
using NextCMS.Core;
using NextCMS.Core.Domain.Authen;
using NextCMS.Services.Authen;
using NextCMS.Web.Framework.Controllers;
using NextCMS.Web.Framework;
using NextCMS.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextCMS.Admin.Controllers
{
    public partial class UserController : BaseAdminController
    {
        #region 私有字段

        private readonly IUserService _userService;
        private readonly IRoleService _roleService;

        #endregion

        #region 构造函数

        public UserController(IUserService userService, IRoleService roleService)
        {
            this._userService = userService;
            this._roleService = roleService;
        }

        #endregion

        #region 公共方法

        [NonAction]
        protected virtual void PrepareAllRolesModel(UserModel model, User user = null)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            //所有角色
            model.Roles = _roleService.GetAllRole().Where(t => t.Active).Select(t => new KeyValueModel
            {
                Text = t.Name,
                Value = t.Id.ToString()
            }).ToList();

            //选中角色
            if (user != null)
            {
                model.SelectedRoles = user.Roles.Select(t => t.Id).ToList();
            }
        }

        #endregion

        #region 用户

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            var model = new UserSearchModel();
            return View(model);
        }

        public ActionResult InitDataTable(UserSearchModel model)
        {
            var query = _userService.GetAllUser(model.UserName, model.Email,
                model.Phone, model.SelectedRoles, model.iDisplayStart / model.iDisplayLength, model.iDisplayLength);

            var filterResult = query.Select(t => new UserListModel
            { 
                Id = t.Id,
                UserName = t.UserName,
                Email = t.Email,
                Phone = t.Phone,
                Active = t.Active,
                RegisterDate = WebHelper.ConvertToUserTime(t.RegisterDate)
            });

            int sortId = model.iDisplayStart + 1;
            var result = from t in filterResult
                         select new[]
                             {
                                 t.Id.ToString(),
                                 sortId++.ToString(),
                                 t.UserName,
                                 t.Email,
                                 t.Phone.ToString(),
                                 t.RegisterDate.ToString(),
                                 t.Active.ToString(),
                                 t.Id.ToString()
                             };

            return DataTableJsonResult(model.sEcho, model.iDisplayStart, query.TotalCount, query.TotalCount, result);
        }

        public ActionResult Create()
        {
            var model = new UserModel();
            PrepareAllRolesModel(model);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public ActionResult Create(UserModel model, bool continueEditing)
        {
            if (!String.IsNullOrWhiteSpace(model.UserName))
            {
                var user = _userService.GetUserByUserName(model.UserName);
                if (user != null)
                    ModelState.AddModelError("UserName", "用户名已经注册了");
            }

            if (!String.IsNullOrWhiteSpace(model.Email))
            {
                var user = _userService.GetUserByEmail(model.Email);
                if (user != null)
                    ModelState.AddModelError("Email", "电子邮箱已经注册了");
            }

            if (ModelState.IsValid)
            {
                var user = new User()
                {
                    UserName = model.UserName,
                    Password = Encryption.EncryptText(model.Password),
                    Email = model.Email,
                    Phone = model.Phone,
                    RegisterDate = DateTime.Now,
                    UpdateDate = DateTime.Now
                };

                //角色
                foreach (var id in model.SelectedRoles)
                {
                    user.Roles.Add(_roleService.GetRoleById(id));
                }

                _userService.InsertUser(user);

                SuccessNotification("添加成功");
                return continueEditing ? RedirectToAction("Edit", new { id = user.Id }) : RedirectToAction("List");
            }
            PrepareAllRolesModel(model);

            return View(model);
        }

        public ActionResult Edit(int id) 
        {
            var user = _userService.GetUserById(id);
            var model = new UserModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Password = Encryption.DecryptText(user.Password), 
                Email = user.Email,
                Phone = user.Phone,
                RegisterDate = WebHelper.ConvertToUserTime(user.RegisterDate),
                UpdateDate = WebHelper.ConvertToUserTime(user.UpdateDate),
                LastIpAddress = user.LastIpAddress,
            };

            if (user.LastLoginDate != null)
                model.LastLoginDate = WebHelper.ConvertToUserTime(user.LastLoginDate.Value);

            if (user.LastActivityDate != null)
                model.LastActivityDate = WebHelper.ConvertToUserTime(user.LastActivityDate.Value);

            PrepareAllRolesModel(model, user);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        [FormValueRequired("save", "save-continue")]
        public ActionResult Edit(UserModel model, bool continueEditing)
        {
            var user = _userService.GetUserById(model.Id);

            if (ModelState.IsValid)
            {
                user.Phone = model.Phone;
                user.Active = model.Active;
                user.UpdateDate = DateTime.Now;

                //Role
                var allRoles = _roleService.GetAllRole().ToList();
                foreach (var role in allRoles)
                {
                    //ensure that the current user cannot add/remove to/from "Administrators" system role
                    //if he's not an admin himself
                    //if (userRole.SystemName == SystemUserRoleNames.Administrators &&
                    //    !_workContext.CurrentUser.IsAdmin())
                    //    continue;

                    //所有 { 1, 2, 3, 4}
                    //现有 { 1, 4 }
                    //选中 { 1, 2, 3 }
                    //添加 {2,3}, 删除{4}

                    // 判断选中的角色是否在所有角色中
                    if (model.SelectedRoles != null && model.SelectedRoles.Contains(role.Id))
                    {
                        // add role
                        // 数据库中用户所属角色中判断是否存在该角色
                        if (user.Roles.Count(t => t.Id == role.Id) == 0)
                        {
                            user.Roles.Add(role);
                        }
                    }
                    else
                    {
                        //remove role
                        if (user.Roles.Count(t => t.Id == role.Id) > 0)
                        {
                            user.Roles.Remove(role);
                        }
                    }
                }
                _userService.UpdateUser(user);

                SuccessNotification("更新成功");
                return continueEditing ? RedirectToAction("Edit", new { id = user.Id }) : RedirectToAction("List");
            }

            PrepareAllRolesModel(model, user);
            return View(model);
        }

        [HttpPost, ActionName("Edit")]
        [FormValueRequired("changepassword")]
        public ActionResult ChangePassword(UserModel model)
        {
            var user = _userService.GetUserById(model.Id);
            if (user == null)
                return RedirectToAction("List");

            if (ModelState.IsValid)
            {
                user.Password = Encryption.EncryptText(model.Password);

                _userService.UpdateUser(user);

                SuccessNotification("修改密码成功");

                return RedirectToAction("Edit", new { id = user.Id });
            }

            PrepareAllRolesModel(model, user);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var user = _userService.GetUserById(id);
            _userService.DeleteUser(user);

            return Json(new { success = true });
        }
        #endregion
    }
}