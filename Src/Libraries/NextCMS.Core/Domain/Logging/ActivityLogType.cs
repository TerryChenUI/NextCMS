namespace NextCMS.Core.Domain.Common
{
    /// <summary>
    /// 活动日志类型
    /// </summary>
    public partial class ActivityLogType : BaseEntity
    {
        /// <summary>
        /// 关键字
        /// </summary>
        public string SystemKeyword { get; set; }

        /// <summary>
        /// 显示名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 可用
        /// </summary>
        public bool Enabled { get; set; }
    }
}
