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
    /// 角色
    /// </summary>
    public partial class RoleService : IRoleService
    {
        #region 字段

        private readonly IRepository<Role> _roleRepository;

        #endregion

        #region 构造函数
        public RoleService(IRepository<Role> roleRepository)
        {
            this._roleRepository = roleRepository;
        }

        #endregion

        #region 方法

        #region 角色

        public virtual IPagedList<Role> GetAllRole(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _roleRepository.Table.OrderBy(t => t.Id);
            return new PagedList<Role>(query, pageIndex, pageSize);
        }

        /// <summary>
        /// 根据主键获取角色
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns>角色对象</returns>
        public virtual Role GetRoleById(int id)
        {
            if (id == 0)
                return null;

            return _roleRepository.GetById(id);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="role">角色实体</param>
        public virtual void InsertRole(Role role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            _roleRepository.Insert(role);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="role">角色实体</param>
        public virtual void UpdateRole(Role role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            _roleRepository.Update(role);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="role">角色实体</param>
        public virtual void DeleteRole(Role role)
        {
            if (role == null)
                throw new ArgumentNullException("role");

            _roleRepository.Delete(role);
        }
        #endregion

        #endregion
    }
}