using NextCMS.Core;
using NextCMS.Core.Data;
using NextCMS.Core.Domain.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextCMS.Services.Catalog
{
    /// <summary>
    /// 文章
    /// </summary>
    public partial class ArticleService : IArticleService
    {
        #region 字段

        private readonly IRepository<Article> _articleRepository;
        private readonly IRepository<Tag> _tagRepository;

        #endregion

        #region 构造函数
        public ArticleService(IRepository<Article> articleRepository, IRepository<Tag> tagRepository)
        {
            this._articleRepository = articleRepository;
            this._tagRepository = tagRepository;
        }

        #endregion

        #region 方法

        #region 文章

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="categoryId">类别</param>
        /// <param name="tagId">标签</param>
        /// <param name="showHidden">是否隐藏未发布项</param>
        /// <param name="pageIndex">页码</param>
        /// <param name="pageSize">每页个数</param>
        public virtual IPagedList<Article> GetAllArticle(string title = null, int categoryId = 0, int tagId = 0, bool showHidden = false, int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _articleRepository.Table;

            if (!string.IsNullOrEmpty(title))
                query = query.Where(t => t.Title.ToLower().Contains(title.ToLower()));

            if(categoryId != 0)
                query = query.Where(t => t.CategoryId == categoryId);

            if (tagId != 0)
                query = query.Where(t => t.Tags.Select(m => m.Id).Contains(tagId));               
            
            if (!showHidden)
                query = query.Where(t => t.Published);

            query = query.OrderByDescending(t => t.Id);

            return new PagedList<Article>(query, pageIndex, pageSize);
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="categoryId">类别</param>
        /// <param name="tagId">标签</param>
        public virtual IQueryable<Article> GetAllArticle(string title = null, int categoryId = 0, int tagId = 0)
        {
            var query = _articleRepository.Table.Where(t => t.Published);

            if (!string.IsNullOrEmpty(title))
                query = query.Where(t => t.Title.ToLower().Contains(title.ToLower()));

            if (categoryId != 0)
                query = query.Where(t => t.CategoryId == categoryId);

            if (tagId != 0)
                query = query.Where(t => t.Tags.Select(m => m.Id).Contains(tagId));

            query = query.OrderByDescending(t => t.Id);

            return query;
        }

        /// <summary>
        /// 根据主键获取文章
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns>文章对象</returns>
        public virtual Article GetArticleById(int id)
        {
            if (id == 0)
                return null;

            return _articleRepository.GetById(id);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="article">文章实体</param>
        public virtual void InsertArticle(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            _articleRepository.Insert(article);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="article">文章实体</param>
        public virtual void UpdateArticle(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            _articleRepository.Update(article);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="article">文章实体</param>
        public virtual void DeleteArticle(Article article)
        {
            if (article == null)
                throw new ArgumentNullException("article");

            _articleRepository.Delete(article);
        }
        #endregion

        #region 标签

        public virtual IPagedList<Tag> GetAllTag(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _tagRepository.Table.OrderBy(t => t.Id);
            return new PagedList<Tag>(query, pageIndex, pageSize);
        }

        /// <summary>
        /// 根据主键获取标签
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns>标签对象</returns>
        public virtual Tag GetTagById(int id)
        {
            if (id == 0)
                return null;

            return _tagRepository.GetById(id);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="tag">标签实体</param>
        public virtual void InsertTag(Tag tag)
        {
            if (tag == null)
                throw new ArgumentNullException("tag");

            _tagRepository.Insert(tag);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="tag">标签实体</param>
        public virtual void UpdateTag(Tag tag)
        {
            if (tag == null)
                throw new ArgumentNullException("tag");

            _tagRepository.Update(tag);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="tag">标签实体</param>
        public virtual void DeleteTag(Tag tag)
        {
            if (tag == null)
                throw new ArgumentNullException("tag");

            _tagRepository.Delete(tag);
        }
        #endregion

        #endregion
    }
}
