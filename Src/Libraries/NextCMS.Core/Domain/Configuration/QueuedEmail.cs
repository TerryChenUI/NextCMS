using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextCMS.Core.Domain.Configuration
{
    /// <summary>
    /// 邮件队列
    /// </summary>
    public partial class QueuedEmail : BaseEntity
    {
        /// <summary>
        /// 优先级
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// 发件人
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// 发件人名称
        /// </summary>
        public string FromName { get; set; }

        /// <summary>
        /// 收件人
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// 收件人名称
        /// </summary>
        public string ToName { get; set; }

        /// <summary>
        /// 回复
        /// </summary>
        public string ReplyTo { get; set; }

        /// <summary>
        /// 回复名称
        /// </summary>
        public string ReplyToName { get; set; }

        /// <summary>
        /// 抄送
        /// </summary>
        public string CC { get; set; }

        /// <summary>
        /// 密件抄送的收件人地址
        /// </summary>
        public string Bcc { get; set; }

        /// <summary>
        /// 主题
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 征文
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// 附件地址
        /// </summary>
        public string AttachmentFilePath { get; set; }

        /// <summary>
        /// 附件名称
        /// </summary>
        public string AttachmentFileName { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreatedOnDate { get; set; }

        /// <summary>
        /// 发送尝试次数
        /// </summary>
        public int SentTries { get; set; }

        /// <summary>
        /// 发送日期
        /// </summary>
        public DateTime? SentOnDate { get; set; }

        /// <summary>
        /// 邮件账户
        /// </summary>
        public int EmailAccountId { get; set; }

        public virtual EmailAccount EmailAccount { get; set; }
    }
}
