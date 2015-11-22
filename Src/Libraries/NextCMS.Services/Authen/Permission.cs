using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using NextCMS.Core;
using NextCMS.Core.Data;
using NextCMS.Core.Domain.Authen;

namespace NextCMS.Services.Authen
{
    /// <summary>
    /// 权限
    /// </summary>
    public partial class PermissionService : IPermissionService
    {
        #region 字段

        private readonly IRepository<Permission> _permissionRepository;

        #endregion

        #region 构造函数
        public PermissionService(IRepository<Permission> permissionRepository)
        {
            this._permissionRepository = permissionRepository;
        }

        #endregion

        #region 方法

        #region 权限

        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        public virtual IPagedList<Permission> GetAllPermission(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _permissionRepository.Table.Where(t => !t.Deleted)
                .OrderBy(t => t.ParentId).ThenBy(t => t.DisplayOrder);

            var unsortedPermissions = query.ToList();
            var sortedPermissions = SortPermissionsForTree(unsortedPermissions);

            return new PagedList<Permission>(sortedPermissions, pageIndex, pageSize);
        }

        /// <summary>
        /// 递归列出权限的树节点
        /// </summary>
        /// <param name="source">所有权限</param>
        /// <param name="parentId">父节点</param>
        /// <returns></returns>
        private List<Permission> SortPermissionsForTree(List<Permission> source, int parentId = 0)
        {
            if (source == null)
                throw new ArgumentNullException("source");

            var result = new List<Permission>();

            var parentList = source.ToList().FindAll(c => c.ParentId == parentId);

            foreach (var parent in parentList)
            {
                //循环每次找出父节点，放到List集合中
                result.Add(parent);

                //递归调用source，根据ParentId往下找子节点
                result.AddRange(SortPermissionsForTree(source, parent.Id));
            }
            return result.ToList();
        }

        /// <summary>
        /// 根据主键获取权限
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns>权限对象</returns>
        public virtual Permission GetPermissionById(int id)
        {
            if (id == 0)
                return null;

            return _permissionRepository.GetById(id);
        }

        public virtual IList<Permission> GetAllPermissionsByParentId(int parentId)
        {
            var query = _permissionRepository.Table
                .Where(t => !t.Deleted && t.Active && t.ParentId == parentId)
                .OrderBy(t => t.DisplayOrder);

            return query.ToList();
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="permission">权限实体</param>
        public virtual void InsertPermission(Permission permission)
        {
            if (permission == null)
                throw new ArgumentNullException("permission");

            _permissionRepository.Insert(permission);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="permission">权限实体</param>
        public virtual void UpdatePermission(Permission permission)
        {
            if (permission == null)
                throw new ArgumentNullException("permission");

            _permissionRepository.Update(permission);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="permission">权限实体</param>
        public virtual void DeletePermission(Permission permission)
        {
            if (permission == null)
                throw new ArgumentNullException("permission");

            _permissionRepository.Delete(permission);
        }
        #endregion

        #region 公用

        /// <summary>
        /// 权限面包屑格式-权限名称
        /// </summary>
        /// <param name="permission">权限</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>面包屑格式</returns>
        public virtual string GetFormattedBreadCrumb(Permission permission, string separator = ">>")
        {
            if (permission == null)
                throw new ArgumentNullException("permission");

            string result = string.Empty;

            var alreadyProcessedPermissionIds = new List<int>() { };

            while (permission != null &&  //not null
                !permission.Deleted &&  //not deleted
                !alreadyProcessedPermissionIds.Contains(permission.Id)) //prevent circular references
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = permission.Name;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", permission.Name, separator, result);
                }

                alreadyProcessedPermissionIds.Add(permission.Id);

                permission = _permissionRepository.GetById(permission.ParentId);

            }

            return result;
        }

        /// <summary>
        /// 权限面包屑格式-排序
        /// </summary>
        /// <param name="permission">权限</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>面包屑格式</returns>
        public virtual string GetFormattedDisplayOrder(Permission permission, string separator = "-")
        {
            if (permission == null)
                throw new ArgumentNullException("permission");

            string result = string.Empty;

            var alreadyProcessedPermissionIds = new List<int>() { };

            while (permission != null &&  //not null
                !permission.Deleted &&  //not deleted
                !alreadyProcessedPermissionIds.Contains(permission.Id)) //prevent circular references
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = permission.DisplayOrder.ToString();
                }
                else
                {
                    result = string.Format("{0} {1} {2}", permission.DisplayOrder, separator, result);
                }

                alreadyProcessedPermissionIds.Add(permission.Id);

                permission = _permissionRepository.GetById(permission.ParentId);

            }

            return result;
        }

        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <param name="user">当前用户</param>
        /// <returns></returns>
        public virtual bool Authorize(string controller, User currentUser)
        {
            if (currentUser == null)
                return false;

            bool allow = false;
            var roles = currentUser.Roles.Where(t => t.Active);
            foreach (var rs in roles)
            {
                if (rs.Permissions.Count(t => t.Controller.ToLower() == controller.ToLower()) > 0)
                {
                    allow = true;
                    break;
                }
            }
            return allow;
        }

        #endregion

        #endregion
    }
}