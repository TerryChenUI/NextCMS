using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NextCMS.Admin.Models.Common
{
    public class DataTableParameter
    {
        /// <summary>
        /// DataTable请求服务器端次数
        /// </summary>       
        public string sEcho { get; set; }

        /// <summary>
        /// 过滤文本
        /// </summary>
        //public string sSearch { get; set; }

        /// <summary>
        /// 每页显示的数量
        /// </summary>
        public int iDisplayLength { get; set; }

        /// <summary>
        /// 分页时每页跨度数量
        /// </summary>
        public int iDisplayStart { get; set; }

        /// <summary>
        /// 列数
        /// </summary>
        //public int iColumns { get; set; }

        /// <summary>
        /// 排序列的数量
        /// </summary>
        //public int iSortingCols { get; set; }

        /// <summary>
        /// 逗号分割所有的列
        /// </summary>
        //public string sColumns { get; set; }
    }
}