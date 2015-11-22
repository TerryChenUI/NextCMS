using System;
using NextCMS.Core.Domain.Authen;

namespace NextCMS.Core.Domain.Common
{
    /// <summary>
    /// 日志记录
    /// </summary>
    public partial class Log : BaseEntity
    {
        /// <summary>
        /// 日志级别
        /// </summary>
        public int LogLevelId { get; set; }

        /// <summary>
        /// 简短信息
        /// </summary>
        public string ShortMessage { get; set; }

        /// <summary>
        /// 详细信息
        /// </summary>
        public string FullMessage { get; set; }

        /// <summary>
        /// Ip
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// 错误页面
        /// </summary>
        public string PageUrl { get; set; }

        /// <summary>
        /// 请求地址
        /// </summary>
        public string ReferrerUrl { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOnDate { get; set; }

        /// <summary>
        /// 日志级别
        /// </summary>
        public LogLevel LogLevel
        {
            get
            {
                return (LogLevel)this.LogLevelId;
            }
            set
            {
                this.LogLevelId = (int)value;
            }
        }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }
    }
}
