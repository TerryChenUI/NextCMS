using NextCMS.Core;
using NextCMS.Core.Domain.Authen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextCMS.Services.Authen
{
    /// <summary>
    /// 权限
    /// </summary>
    public partial interface IPermissionService
    {
        #region 权限

        /// <summary>
        /// 获取所有权限
        /// </summary>
        /// <returns></returns>
        IPagedList<Permission> GetAllPermission(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 根据主键获取权限
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns>权限对象</returns>
        Permission GetPermissionById(int id);

        /// <summary>
        /// 根据父权限获取所有权限
        /// </summary>
        /// <param name="parentId"></param>
        /// <returns></returns>
        IList<Permission> GetAllPermissionsByParentId(int parentId);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="role">权限实体</param>
        void InsertPermission(Permission role);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="role">权限实体</param>
        void UpdatePermission(Permission role);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="role">权限实体</param>
        void DeletePermission(Permission role);

        #endregion

        #region 公用

        /// <summary>
        /// 权限面包屑格式-权限名称
        /// </summary>
        /// <param name="permission">权限</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>面包屑格式</returns>
        string GetFormattedBreadCrumb(Permission permission, string separator = ">>");

        /// <summary>
        /// 权限面包屑格式-排序
        /// </summary>
        /// <param name="permission">权限</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>面包屑格式</returns>
        string GetFormattedDisplayOrder(Permission permission, string separator = "-");

        /// <summary>
        /// 验证权限
        /// </summary>
        /// <param name="controller">控制器</param>
        /// <param name="user">当前用户</param>
        /// <returns></returns>
        bool Authorize(string controller, User currentUser);

        #endregion

    }
}
