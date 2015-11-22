using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextCMS.Core.Domain.Media
{
    /// <summary>
    /// 图片
    /// </summary>
    public partial class Picture : BaseEntity
    {
        /// <summary>
        /// 图片后缀
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// 搜索引擎友好名称
        /// </summary>
        public string SeoFilename { get; set; }

        /// <summary>
        /// 是否新图
        /// </summary>
        public bool IsNew { get; set; }

    }
}
