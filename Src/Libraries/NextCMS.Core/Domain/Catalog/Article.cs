using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextCMS.Core.Domain.Catalog
{
    /// <summary>
    /// 文章
    /// </summary>
    public partial class Article : BaseEntity
    {

        /// <summary>
        /// 标题
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string ShortDescription { get; set; }

        /// <summary>
        /// 正文
        /// </summary>
        public string FullDescription { get; set; }

        /// <summary>
        /// SEO关键字
        /// </summary>
        public string MetaKeywords { get; set; }

        /// <summary>
        /// SEO描述
        /// </summary>
        public string MetaDescription { get; set; }

        /// <summary>
        /// SEO标题
        /// </summary>
        public string MetaTitle { get; set; }

        /// <summary>
        /// 封面图
        /// </summary>
        public int PictureId { get; set; }

        /// <summary>
        /// 访问量
        /// </summary>
        public int Views { get; set; }

        /// <summary>
        /// 评论个数
        /// </summary>
        public int CommentCount { get; set; }

        /// <summary>
        /// 推荐
        /// </summary>
        public int Favor { get; set; }

        /// <summary>
        /// 踩
        /// </summary>
        public int Hate { get; set; }

        /// <summary>
        /// 允许评论
        /// </summary>
        public bool AllowComment { get; set; }

        /// <summary>
        /// 置顶
        /// </summary>
        public bool ShowOnTop { get; set; }

        /// <summary>
        /// 已发布
        /// </summary>
        public bool Published { get; set; }

        /// <summary>
        /// 已删除
        /// </summary>
        public bool Deleted { get; set; }

        /// <summary>
        /// 创建日期
        /// </summary>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// 更新日期
        /// </summary>
        public DateTime UpdateDate { get; set; }

        /// <summary>
        /// 类别
        /// </summary>
        public int? CategoryId { get; set; }

        public virtual Category Category { get; set; }

        /// <summary>
        /// 评论
        /// </summary>
        private ICollection<Comment> _comments;
        public virtual ICollection<Comment> Comments
        {
            get { return _comments ?? (_comments = new List<Comment>()); }
            protected set { _comments = value; }
        }

        /// <summary>
        /// 标签
        /// </summary>
        private ICollection<Tag> _tags;
        public virtual ICollection<Tag> Tags
        {
            get { return _tags ?? (_tags = new List<Tag>()); }
            protected set { _tags = value; }
        }
    }
}
