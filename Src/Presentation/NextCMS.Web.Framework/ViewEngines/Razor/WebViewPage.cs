using NextCMS.Core;
using NextCMS.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NextCMS.Web.Framework.ViewEngines.Razor
{
    public abstract class WebViewPage<TModel> : System.Web.Mvc.WebViewPage<TModel>
    {
        private IWorkContext _workContext;

        public IWorkContext WorkContext
        {
            get
            {
                return _workContext;
            }
        }

        public override void InitHelpers()
        {
            base.InitHelpers();
           _workContext = EngineContext.Current.Resolve<IWorkContext>();
        }
    }
}
