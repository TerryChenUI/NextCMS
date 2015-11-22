using NextCMS.Core.Domain.Authen;
using NextCMS.Core.Domain.Settings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextCMS.Core
{
    /// <summary>
    /// 网站上下文接口
    /// </summary>
    public interface IWorkContext
    {
        string UserName { get; set; }

        User CurrentUser { get; set; }

        GeneralSettings GeneralSettings { get; }
    }
}
