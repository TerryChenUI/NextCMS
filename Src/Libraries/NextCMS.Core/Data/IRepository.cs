using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace NextCMS.Core.Data
{
    /// <summary>
    /// 数据操作仓库
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public partial interface IRepository<T> where T : BaseEntity
    {

        /// <summary>
        /// 获得IQueryable对象
        /// </summary>
        IQueryable<T> Table { get; }

        /// <summary>
        /// No tracking 实体不被EF context跟踪，仅用于只读操作
        /// </summary>
        IQueryable<T> TableNoTracking { get; }

        /// <summary>
        /// 根据主键获取实体
        /// </summary>
        /// <param name="id">主键</param>
        /// <returns>实体</returns>
        T GetById(object id);

        /// <summary>
        /// 插入
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Insert(T entity);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Update(T entity);

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="entity">实体对象</param>
        void Delete(T entity);
    }
}
