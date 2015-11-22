using System;
using System.Collections.Generic;

namespace NextCMS.Core.Domain.Authen
{
    /// <summary>
    /// 用户
    /// </summary>
    public partial class User : BaseEntity
    {
        private ICollection<Role> _roles;

        /// <summary>
        /// 用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 电子邮件
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 电话号码
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// 激活状态
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// 删除
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// 注册时间
        /// </summary>
        public DateTime RegisterDate { get; set; }

        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 最后登录时间
        /// </summary>
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// 最后登陆Ip地址
        /// </summary>
        public string LastIpAddress { get; set; }

        /// <summary>
        /// 最后激活时间
        /// </summary>
        public DateTime? LastActivityDate { get; set; }

        #region 导航属性

        /// <summary>
        /// 角色
        /// </summary>
        public virtual ICollection<Role> Roles
        {
            get { return _roles ?? (_roles = new List<Role>()); }
            protected set { _roles = value; }
        }

        #endregion
    }
}