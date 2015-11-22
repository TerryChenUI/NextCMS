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
    /// 角色
    /// </summary>
    public partial interface IRoleService
    {
        IPagedList<Role> GetAllRole(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 根据主键获取角色
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns>角色对象</returns>
        Role GetRoleById(int id);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="role">角色实体</param>
        void InsertRole(Role role);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="role">角色实体</param>
        void UpdateRole(Role role);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="role">角色实体</param>
        void DeleteRole(Role role);
    }
}
