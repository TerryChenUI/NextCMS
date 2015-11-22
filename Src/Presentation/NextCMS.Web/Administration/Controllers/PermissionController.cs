using NextCMS.Admin.Models.Authen;
using NextCMS.Admin.Models.Common;
using NextCMS.Services.Authen;
using NextCMS.Web.Framework.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NextCMS.Admin.Controllers
{
    public class PermissionController : BaseAdminController
    {
        #region 私有字段

        private readonly IPermissionService _permissionService;
        private readonly IRoleService _roleService;

        #endregion

        #region 构造函数

        public PermissionController(IPermissionService permissionService, IRoleService roleService)
        {
            this._permissionService = permissionService;
            this._roleService = roleService;
        }

        #endregion

        #region 公用方法

        [NonAction]
        protected virtual void PrepareAllPermissionsModel(PermissionModel model)
        {
            if (model == null)
                throw new ArgumentNullException("model");

            model.AvailablePermissions.Add(new SelectListItem()
            {
                Text = "父权限",
                Value = "0"
            });
            var permissions = _permissionService.GetAllPermission();
            foreach (var p in permissions)
            {
                model.AvailablePermissions.Add(new SelectListItem()
                {
                    Text = _permissionService.GetFormattedBreadCrumb(p),
                    Value = p.Id.ToString()
                });
            }
        }

        public List<PermissionModel> SortPermissionForTree(int parentId, string indexKey = "1")
        {
            var model = new List<PermissionModel>();
            foreach (var p in _permissionService.GetAllPermissionsByParentId(parentId))
            {
                var pm = new PermissionModel
                {
                    Id = p.Id,
                    Name = _permissionService.GetFormattedBreadCrumb(p),
                    ParentId = p.ParentId
                };
                if (parentId == 0)
                {
                    pm.IndexKey = p.Id.ToString();
                }
                else
                {
                    pm.IndexKey = indexKey + "_" + p.Id.ToString();
                }

                pm.ChildPermissions.AddRange(SortPermissionForTree(p.Id, pm.IndexKey));
                model.Add(pm);
            }
            return model;
        }

        public List<PermissionModel> SortPermissionForTree(List<PermissionModel> permissions)
        {
            var result = new List<PermissionModel>();

            foreach (var p in permissions)
            {
                //循环每次找出父节点，放到List集合中
                result.Add(p);

                //递归调用source，根据ParentId往下找子节点
                result.AddRange(SortPermissionForTree(p.ChildPermissions));
            }
            return result;
        }

        #endregion

        #region 权限
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
            var query = _permissionService.GetAllPermission(param.iDisplayStart / param.iDisplayLength, param.iDisplayLength);

            var filterResult = query.Select(t =>
            {
                var permission = new PermissionListModel
                {
                    Id = t.Id,
                    Icon = !string.IsNullOrEmpty(t.Icon) ? string.Format("<i class=\"fa {0}\"></i>", t.Icon) : "",
                    Name = t.Name,
                    BreadCrumb = _permissionService.GetFormattedBreadCrumb(t),
                    LinkUrl = string.IsNullOrEmpty(t.Controller) ? "#" : string.Format("{0}/{1}/{2}", t.Area, t.Controller, t.Action),
                    DisplayOrder = _permissionService.GetFormattedDisplayOrder(t),
                    Active = t.Active
                };
                return permission;
            });

            int sortId = param.iDisplayStart + 1;
            var result = from t in filterResult
                         select new[]
                             {
                                 sortId++.ToString(),
                                 t.Icon,
                                 t.Name,
                                 t.BreadCrumb,
                                 t.LinkUrl,
                                 t.DisplayOrder,
                                 t.Active.ToString(),
                                 t.Id.ToString()
                             };

            return DataTableJsonResult(param.sEcho, param.iDisplayStart, query.TotalCount, query.TotalCount, result);
        }

        public ActionResult Create()
        {
            var model = new PermissionModel();
            PrepareAllPermissionsModel(model);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Create(PermissionModel model, bool continueEditing)
        {
            if (ModelState.IsValid)
            {
                var permission = model.ToEntity();
                _permissionService.InsertPermission(permission);

                SuccessNotification("添加成功");
                return continueEditing ? RedirectToAction("Edit", new { id = permission.Id }) : RedirectToAction("List");
            }
            PrepareAllPermissionsModel(model);
            return View(model);
        }

        public ActionResult Edit(int id)
        {
            var permission = _permissionService.GetPermissionById(id);
            var model = permission.ToModel();
            PrepareAllPermissionsModel(model);
            return View(model);
        }

        [HttpPost, ParameterBasedOnFormName("save-continue", "continueEditing")]
        public ActionResult Edit(PermissionModel model, bool continueEditing)
        {
            var permission = _permissionService.GetPermissionById(model.Id);

            if (ModelState.IsValid)
            {
                permission = model.ToEntity(permission);
                _permissionService.UpdatePermission(permission);

                SuccessNotification("保存成功");
                return continueEditing ? RedirectToAction("Edit", new { id = permission.Id }) : RedirectToAction("List");
            }
            PrepareAllPermissionsModel(model);
            return View(model);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var permission = _permissionService.GetPermissionById(id);
            _permissionService.DeletePermission(permission);

            return Json(new { success = true });
        }
        #endregion

        #region 访问控制

        public ActionResult Access() 
        {
            var permissions = _permissionService.GetAllPermission();
            var roles = _roleService.GetAllRole();
            
            var model = new PermissionRoleModel();

            var permissionTree = SortPermissionForTree(0);
            model.AvailablePermissions = SortPermissionForTree(permissionTree);

            model.AvailableRoles = roles.Select(t => new RoleModel()
            {
                Id = t.Id,
                Name = t.Name
            }).ToList();

            //数据库中选中的项目
            foreach (var ps in permissions)
            {
                //checkbox 保存 permission Id
                var key = ps.Id.ToString();
                foreach (var rs in roles)
                {
                    bool allowed = ps.Roles.Count(x => x.Id == rs.Id) > 0;
                    if (!model.Allowed.ContainsKey(key))
                    {
                        model.Allowed[key] = new Dictionary<int, bool>();
                    }

                    model.Allowed[key][rs.Id] = allowed;
                }
            }

            return View(model);
        }

        [HttpPost]
        public ActionResult Access(FormCollection form)
        {
            var permissions = _permissionService.GetAllPermission();
            var roles = _roleService.GetAllRole().ToList();

            foreach (var rs in roles)
            {
                string formKey = "allow_" + rs.Id;

                var permissionKeyToRestrict = form[formKey] != null ? form[formKey].Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList() : new List<string>();

                var permissionToRestrict = permissionKeyToRestrict.Select(t => Convert.ToInt32(t)).ToList();

                foreach (var ps in permissions)
                {
                    bool allow = permissionToRestrict.Count(t => t == ps.Id) > 0;
                    if (allow)
                    {
                        if (ps.Roles.FirstOrDefault(x => x.Id == rs.Id) == null)
                        {
                            ps.Roles.Add(rs);
                            _permissionService.UpdatePermission(ps);
                        }
                    }
                    else
                    {
                        if (ps.Roles.FirstOrDefault(x => x.Id == rs.Id) != null)
                        {
                            ps.Roles.Remove(rs);
                            _permissionService.UpdatePermission(ps);
                        }
                    }
                }
            }

            SuccessNotification("保存成功");

            return RedirectToAction("Access");
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
        #endregion
    }
}