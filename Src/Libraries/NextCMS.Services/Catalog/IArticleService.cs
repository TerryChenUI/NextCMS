using NextCMS.Core;
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
    public partial interface IArticleService
    {
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
        /// <returns></returns>
        IPagedList<Article> GetAllArticle(string title = null, int categoryId = 0, int tagId = 0, bool showHidden = false, int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="title">标题</param>
        /// <param name="categoryId">类别</param>
        /// <param name="tagId">标签</param>
        /// <returns></returns>
        IQueryable<Article> GetAllArticle(string title = null, int categoryId = 0, int tagId = 0);
        
        /// <summary>
        /// 根据主键获取文章
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns>文章对象</returns>
        Article GetArticleById(int id);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="article">文章实体</param>
        void InsertArticle(Article article);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="article">文章实体</param>
        void UpdateArticle(Article article);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="article">文章实体</param>
        void DeleteArticle(Article article);

        #endregion

        #region 标签

        IPagedList<Tag> GetAllTag(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 根据主键获取标签
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns>标签对象</returns>
        Tag GetTagById(int id);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="tag">标签实体</param>
        void InsertTag(Tag tag);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="tag">标签实体</param>
        void UpdateTag(Tag tag);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="tag">标签实体</param>
        void DeleteTag(Tag tag);

        #endregion

    }
}
