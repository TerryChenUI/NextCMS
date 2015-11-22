
using System.Collections.Generic;

namespace NextCMS.Core
{
    /// <summary>
    /// 分页
    /// </summary>
    public interface IPagedList<T> : IList<T>
    {
        /// <summary>
        /// 页码
        /// </summary>
        int PageIndex { get; }

        /// <summary>
        /// 每页个数
        /// </summary>
        int PageSize { get; }

        /// <summary>
        /// 总个数
        /// </summary>
        int TotalCount { get; }

        /// <summary>
        /// 总页数
        /// </summary>
        int TotalPages { get; }

        /// <summary>
        /// 前一页
        /// </summary>
        bool HasPreviousPage { get; }

        /// <summary>
        /// 后一页
        /// </summary>
        bool HasNextPage { get; }
    }
}
