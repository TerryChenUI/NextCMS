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
    /// 类别
    /// </summary>
    public partial class CategoryService : ICategoryService
    {
        #region 字段

        private readonly IRepository<Category> _categoryRepository;

        #endregion

        #region 构造函数
        public CategoryService(IRepository<Category> categoryRepository)
        {
            this._categoryRepository = categoryRepository;
        }

        #endregion

        #region 方法

        #region 类别

        public virtual IPagedList<Category> GetAllCategory(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var query = _categoryRepository.Table.OrderBy(t => t.Id);
            return new PagedList<Category>(query, pageIndex, pageSize);
        }

        /// <summary>
        /// 根据主键获取类别
        /// </summary>
        /// <param name="id">主键id</param>
        /// <returns>类别对象</returns>
        public virtual Category GetCategoryById(int id)
        {
            if (id == 0)
                return null;

            return _categoryRepository.GetById(id);
        }

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="category">类别实体</param>
        public virtual void InsertCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            _categoryRepository.Insert(category);
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="category">类别实体</param>
        public virtual void UpdateCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            _categoryRepository.Update(category);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="category">类别实体</param>
        public virtual void DeleteCategory(Category category)
        {
            if (category == null)
                throw new ArgumentNullException("category");

            _categoryRepository.Delete(category);
        }
        #endregion

        #region 公用

        /// <summary>
        /// 类别面包屑格式-类别名称
        /// </summary>
        /// <param name="category">类别</param>
        /// <param name="separator">分隔符号</param>
        /// <returns>面包屑格式</returns>
        public virtual string GetFormattedBreadCrumb(Category category, string separator = ">>")
        {
            if (category == null)
                throw new ArgumentNullException("category");

            string result = string.Empty;

            var alreadyProcessedCategoryIds = new List<int>() { };

            while (category != null &&  //not null
                !category.Deleted &&  //not deleted
                !alreadyProcessedCategoryIds.Contains(category.Id)) //prevent circular references
            {
                if (String.IsNullOrEmpty(result))
                {
                    result = category.Name;
                }
                else
                {
                    result = string.Format("{0} {1} {2}", category.Name, separator, result);
                }

                alreadyProcessedCategoryIds.Add(category.Id);

                category = _categoryRepository.GetById(category.ParentId);

            }

            return result;
        }

        #endregion

        #endregion
    }
}
