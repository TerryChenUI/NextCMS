using FluentValidation.Mvc;
using NextCMS.Core.Infrastructure;
using NextCMS.Services.Configuration;
using NextCMS.Web.Extensions;
using NextCMS.Web.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace NextCMS.Web
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //单例模式初始化
            EngineContext.Initialize(false);

            //自定义视图引擎
            ViewEngines.Engines.Clear();
            //except the themeable razor view engine we use
            ViewEngines.Engines.Add(new ThemeableRazorViewEngine());

            //MVC 组件注册
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            //fluent validation 注册
            DataAnnotationsModelValidatorProvider.AddImplicitRequiredAttributeForValueTypes = false;
            ModelValidatorProviders.Providers.Add(new FluentValidationModelValidatorProvider(new NextCMSValidatorFactory()));

            //计划任务
            TaskManager.Instance.Initialize();
            TaskManager.Instance.Start();
        }
    }
}
