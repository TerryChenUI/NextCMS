using System.Collections.Generic;

namespace NextCMS.Core.Domain.Authen
{
    /// <summary>
    /// 角色
    /// </summary>
    public partial class Role : BaseEntity
    {
        /// <summary>
        /// 角色名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 激活状态
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// 系统角色
        /// </summary>
        public bool IsSystemRole { get; set; }

        /// <summary>
        /// 角色名称（英文）
        /// </summary>
        public string SystemName { get; set; }

        #region 导航属性

        /// <summary>
        /// 用户
        /// </summary>
        private ICollection<User> _users;
        public virtual ICollection<User> Users
        {
            get { return _users ?? (_users = new List<User>()); }
            protected set { _users = value; }
        }

        /// <summary>
        /// 权限
        /// </summary>
        private ICollection<Permission> _permissions;
        public virtual ICollection<Permission> Permissions
        {
            get { return _permissions ?? (_permissions = new List<Permission>()); }
            protected set { _permissions = value; }
        }

        #endregion
    }
}
