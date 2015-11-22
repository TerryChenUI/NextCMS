using NextCMS.Admin.Models.Authen;
using NextCMS.Admin.Models.Common;
using NextCMS.Core.Domain.Authen;
using NextCMS.Services.Authen;
using NextCMS.Web.Framework.Controllers;
using NextCMS.Web.Framework.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextCMS.Admin.Controllers
{
    public partial class RoleController : BaseAdminController
    {
        #region 私有字段

        private readonly IRoleService _roleService;
        private readonly IPermissionService _permissionService;

        #endregion

        #region 构造函数

        public RoleController(IRoleService roleService,
            IPermissionService permissionService)
        {
            this._roleService = roleService;
            this._permissionService = permissionService;
        }

        #endregion

        #region 角色

        public ActionResult Index()
        {
            return RedirectToAction("List");
        }

        public ActionResult List()
        {
            return View();
        }

        public ActionResult InitDataTable(DataTableParameter param)
        {
            var query = _roleService.GetAllRole(param.iDisplayStart / param.iDisplayLength, param.iDisplayLength);

            var filterResult = query.Select(t => new RoleListModel
            {
                Id = t.Id,
                Name = t.Name,
                SystemName = t.SystemName,
                Active = t.Active
            });

            int sortId = param.iDisplayStart + 1;
            var result = from t in filterResult
                         select new[]
                             {
                                 sortId++.ToString(),
                                 t.Name,
                                 t.SystemName,
                                 t.Active.ToString(),
                                 t.Id.ToString()
                             };

            return DataTableJsonResult(param.sEcho, param.iDisplayStart, query.TotalCount, query.TotalCount, result);
        }

        public ActionResult Create()
        {
            var model = new RoleModel();
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(RoleModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var role = model.ToEntity();
                _roleService.InsertRole(role);

                SuccessNotification("添加成功");

                return continueEditing ? RedirectToAction("Edit", new { id = role.Id }) : RedirectToAction("List");
            }
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var role = _roleService.GetRoleById(id);
            var model = role.ToModel();

            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(RoleModel model, bool continueEditing)
        {
            var role = _roleService.GetRoleById(model.Id);

            if (ModelState.IsValid)
            {
                role = model.ToEntity(role);
                _roleService.UpdateRole(role);

                SuccessNotification("更新成功");
                return continueEditing ? RedirectToAction("Edit", new { id = role.Id }) : RedirectToAction("List");
            }
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var role = _roleService.GetRoleById(id);
            _roleService.DeleteRole(role);

            return Json(new { success = true });
        }

        #endregion
    }
}