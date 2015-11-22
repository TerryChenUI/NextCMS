using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextCMS.Core.Domain.Configuration
{
    /// <summary>
    /// 邮件账号
    /// </summary>
    public partial class EmailAccount : BaseEntity
    {
        /// <summary>
        /// 邮箱地址
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// 邮箱服务器地址
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// 邮箱用户名
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// 邮箱密码
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// 启用SSL
        /// </summary>
        public bool EnableSsl { get; set; }

        /// <summary>
        /// 默认系统凭据
        /// </summary>
        public bool UseDefaultCredentials { get; set; }

        /// <summary>
        /// 邮箱友好名称
        /// </summary>
        public string FriendlyName
        {
            get
            {
                if (!String.IsNullOrWhiteSpace(this.DisplayName))
                    return this.Email + " (" + this.DisplayName + ")";
                return this.Email;
            }
        }
    }
}
