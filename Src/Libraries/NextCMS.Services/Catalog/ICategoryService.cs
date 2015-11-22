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
    /// 类别
    /// </summary>
    public partial interface ICategoryService
    {
        #region 类别

        IPagedList<Category> GetAllCategory(int pageIndex = 0, int pageSize = int.MaxValue);

        /// <summary>
        /// 根据主键获取类别
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns>类别对象</returns>
        Category GetCategoryById(int id);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="category">类别实体</param>
        void InsertCategory(Category category);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="category">类别实体</param>
        void UpdateCategory(Category category);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="category">类别实体</param>
        void DeleteCategory(Category category);

        #endregion

        #region 公用

        /// <summary>
        /// 类别面包屑格式-类别名称
        /// </summary>
        /// <param name="category">类别</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>面包屑格式</returns>
        string GetFormattedBreadCrumb(Category category, string separator = ">>");

        #endregion
    }
}
