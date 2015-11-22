using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Builder;
using Autofac.Core;
using Autofac.Integration.Mvc;
using NextCMS.Core.Infrastructure;
using NextCMS.Core.Infrastructure.DependencyManagement;
using NextCMS.Core.Data;
using NextCMS.Data;
using NextCMS.Services.Authen;
using NextCMS.Services.Configuration;
using System.Data.Entity;
using NextCMS.Data.Initializers;
using NextCMS.Core;
using NextCMS.Services.Catalog;
using NextCMS.Services.Media;
using NextCMS.Core.Domain.Settings;

namespace NextCMS.Web.Framework
{
    /// <summary>
    /// 注册
    /// </summary>
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public virtual void Register(ContainerBuilder builder, ITypeFinder typeFinder)
        {
            #region 注册HTTP上下文 => HTTP context and other related stuff

            //builder.Register(c =>
            //register FakeHttpContext when HttpContext is not available
            //HttpContext.Current != null ?
            //(new HttpContextWrapper(HttpContext.Current) as HttpContextBase) :
            //(new FakeHttpContext("~/") as HttpContextBase))
            //.As<HttpContextBase>()
            //.InstancePerLifetimeScope();
            builder.Register(c => new HttpContextWrapper(HttpContext.Current) as HttpContextBase)
                .As<HttpContextBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>()
                .InstancePerLifetimeScope();
            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>()
                .InstancePerLifetimeScope();

            #endregion

            //Controllers
            var assemblies = typeFinder.GetAssemblies().ToArray();
            builder.RegisterControllers(assemblies);

            //开发者模式
            //Database.SetInitializer(new NextCMSDropCreateDatabaseIfModelChanges());

            var connectionString = ConfigurationManager.ConnectionStrings["nameOrConnectionString"].ConnectionString;

            //data layer
            builder.Register<IDbContext>(c => new NextCMSObjectContext(connectionString)).InstancePerLifetimeScope();

            builder.RegisterType<WebHelper>().As<IWebHelper>().InstancePerLifetimeScope();

            //work context
            builder.RegisterType<WebWorkContext>().As<IWorkContext>().InstancePerLifetimeScope();

            //repository
            builder.RegisterGeneric(typeof(EfRepository<>)).As(typeof(IRepository<>)).InstancePerLifetimeScope();

            //service
            builder.RegisterType<ScheduleTaskService>().As<IScheduleTaskService>().InstancePerLifetimeScope();

            builder.RegisterType<UserService>().As<IUserService>().InstancePerLifetimeScope();
            builder.RegisterType<RoleService>().As<IRoleService>().InstancePerLifetimeScope();
            builder.RegisterType<PermissionService>().As<IPermissionService>().InstancePerLifetimeScope();

            builder.RegisterType<ArticleService>().As<IArticleService>().InstancePerLifetimeScope();
            builder.RegisterType<CategoryService>().As<ICategoryService>().InstancePerLifetimeScope();

            builder.RegisterType<PictureService>().As<IPictureService>().InstancePerLifetimeScope();

            builder.RegisterType<SettingService>().As<ISettingService>().InstancePerLifetimeScope();
            builder.RegisterSource(new SettingsSource());

            //var dataSettingsManager = new DataSettingsManager();
            //var dataProviderSettings = dataSettingsManager.LoadSettings();
            //builder.Register(c => dataSettingsManager.LoadSettings()).As<DataSettings>();
            //builder.Register(x => new EfDataProviderManager(x.Resolve<DataSettings>())).As<BaseDataProviderManager>().InstancePerDependency();

            //builder.Register(x => x.Resolve<BaseDataProviderManager>().LoadDataProvider()).As<IDataProvider>().InstancePerDependency();

            //if (dataProviderSettings != null && dataProviderSettings.IsValid())
            //{
            //    var efDataProviderManager = new EfDataProviderManager(dataSettingsManager.LoadSettings());
            //    var dataProvider = efDataProviderManager.LoadDataProvider();
            //    dataProvider.InitConnectionFactory();

            //    builder.Register<IDbContext>(c => new NextCMSObjectContext(@"nameOrConnectionString")).InstancePerLifetimeScope();
            //}
            //else
            //{
            //    builder.Register<IDbContext>(c => new NextCMSObjectContext(@"nameOrConnectionString")).InstancePerLifetimeScope();
            //}

            //WebApi
            //builder.RegisterApiControllers(assemblies);

            //插件
            //builder.RegisterType<PluginFinder>().As<IPluginFinder>().InstancePerHttpRequest();

            //缓存管理
            //builder.RegisterType<MemoryCacheManager>().As<ICacheManager>().Named<ICacheManager>("nextcms_cache_static").SingleInstance();
            //builder.RegisterType<PerRequestCacheManager>().As<ICacheManager>().Named<ICacheManager>("nextcms_cache_per_request").InstancePerHttpRequest();


        }

        public int Order
        {
            get { return 0; }
        }

        public class SettingsSource : IRegistrationSource
        {
            static readonly MethodInfo BuildMethod = typeof(SettingsSource).GetMethod(
                "BuildRegistration",
                BindingFlags.Static | BindingFlags.NonPublic);

            public IEnumerable<IComponentRegistration> RegistrationsFor(
                    Service service,
                    Func<Service, IEnumerable<IComponentRegistration>> registrations)
            {
                var ts = service as TypedService;
                if (ts != null && typeof(ISettings).IsAssignableFrom(ts.ServiceType))
                {
                    var buildMethod = BuildMethod.MakeGenericMethod(ts.ServiceType);
                    yield return (IComponentRegistration)buildMethod.Invoke(null, null);
                }
            }

            static IComponentRegistration BuildRegistration<TSettings>() where TSettings : ISettings, new()
            {
                return RegistrationBuilder
                    .ForDelegate((c, p) =>
                    {
                        return c.Resolve<ISettingService>().LoadSetting<TSettings>();
                    })
                    .InstancePerLifetimeScope()
                    .CreateRegistration();
            }

            public bool IsAdapterForIndividualComponents { get { return false; } }
        }
    }
}
