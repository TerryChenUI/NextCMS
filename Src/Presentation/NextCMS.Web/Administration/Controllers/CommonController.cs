using NextCMS.Admin.Models.Common;
using NextCMS.Core;
using NextCMS.Core.Domain.Authen;
using NextCMS.Services.Authen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextCMS.Admin.Controllers
{
    public class CommonController : BaseAdminController
    {
        #region 私有字段

        private readonly IUserService _userService;
        private readonly IPermissionService _permissionService;
        private readonly IWorkContext _workContextService;

        #endregion

        #region 构造函数

        public CommonController(IUserService userService, IPermissionService permissionService,
            IWorkContext workContextService)
        {
            this._userService = userService;
            this._permissionService = permissionService;
            this._workContextService = workContextService;
        }

        #endregion

        [ChildActionOnly]
        public ActionResult SidebarMenu()
        {
            //获取第一层菜单
            //var model = new List<SideBarMenuModel>();

            //var parentMenus = _permissionService.GetAllPermissionsByParentId(0).Select(t =>
            //        new SideBarMenuModel
            //        {
            //            Name = t.Name,
            //            Area = t.Area,
            //            Controller = t.Controller,
            //            Action = t.Action
            //        }
            //    ).ToList();

            //model.AddRange(parentMenus);

            //SortMenuForTree(model, parentMenus);
            //return PartialView(model);

            //var currentUser = _workContextService.CurrentUser;
            var currentUser = _userService.GetUserById(1);

            var permissions = new List<Permission> { };

            var rolePermissions = currentUser.Roles.Select(t => t.Permissions).ToList();

            foreach (var rp in rolePermissions)
            {
                permissions = permissions.Union(rp).ToList();
            }

            var model = SortMenuForTree(0, permissions);
            return PartialView(model);
        }

        /// <summary>
        /// 菜单节点
        /// </summary>
        /// <param name="parentId">父节点</param>
        /// <returns></returns>
        public List<SideBarMenuModel> SortMenuForTree(int parentId, IEnumerable<Permission> rolePermissions)
        {
            var model = new List<SideBarMenuModel>();
            foreach (var p in rolePermissions.Where(t => !t.Deleted && t.Active && t.ParentId == parentId)
                .OrderBy(t => t.DisplayOrder))
            {
                var menu = new SideBarMenuModel
                {
                    Name = p.Name,
                    Controller = p.Controller,
                    Action = p.Action,
                    Icon = p.Icon
                };
                menu.ChildMenus.AddRange(SortMenuForTree(p.Id, rolePermissions));
                model.Add(menu);
            }
            return model;
        }
    }
}