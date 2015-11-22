using System;
using NextCMS.Core.Domain.Authen;

namespace NextCMS.Core.Domain.Common
{
    /// <summary>
    /// 活动日志
    /// </summary>
    public partial class ActivityLog : BaseEntity
    {
        /// <summary>
        /// 活动日志类型
        /// </summary>
        public int ActivityLogTypeId { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// 内容
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime CreatedOnDate { get; set; }

        /// <summary>
        /// 活动日志类型
        /// </summary>
        public virtual ActivityLogType ActivityLogType { get; set; }

        /// <summary>
        /// 用户
        /// </summary>
        public virtual User User { get; set; }
    }
}
