using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextCMS.Core.Domain.Authen
{
    /// <summary>
    /// 权限
    /// </summary>
    public partial class Permission : BaseEntity
    {
        /// <summary>
        /// 父权限
        /// </summary>
        public int ParentId { get; set; }

        /// <summary>
        /// 权限名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 区域
        /// </summary>
        public string Area { get; set; }

        /// <summary>
        /// 控制器
        /// </summary>
        public string Controller { get; set; }

        /// <summary>
        /// 动作
        /// </summary>
        public string Action { get; set; }

        /// <summary>
        /// 图标
        /// </summary>
        public string Icon { get; set; }

        /// <summary>
        /// 显示顺序
        /// </summary>
        public int DisplayOrder { get; set; }

        /// <summary>
        /// 已启用
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// 已删除
        /// </summary>
        public bool Deleted { get; set; }  

        /// <summary>
        /// 角色权限
        /// </summary>
        private ICollection<Role> _roles;
        public virtual ICollection<Role> Roles
        {
            get { return _roles ?? (_roles = new List<Role>()); }
            protected set { _roles = value; }
        }
    }
}
