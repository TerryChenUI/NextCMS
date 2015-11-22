using NextCMS.Core.Infrastructure;

namespace NextCMS.Web.Models.Common
{
    public partial class DataSourceRequest
    {
        private int pageIndex = -2;
        private int pageSize;

        public int PageIndex
        {
            get
            {
                if (this.pageIndex < 0)
                {
                    return 0;
                }
                return this.pageIndex - 1;
            }
            set
            {
                this.pageIndex = value;
            }
        }

        public int PageSize
        {
            get
            {
                return (pageSize <= 0) ? 10 : pageSize;
            }
            set
            {
                pageSize = value;
            }
        }
    }
}