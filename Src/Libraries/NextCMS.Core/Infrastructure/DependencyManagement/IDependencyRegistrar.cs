using Autofac;

namespace NextCMS.Core.Infrastructure.DependencyManagement
{
    public interface IDependencyRegistrar
    {
        /// <summary>  
        /// 此方法在通过ContainerBuilder注册依赖关系。  
        /// </summary>  
        /// <param name="builder">容器管理者类</param>  
        /// <param name="typeFinder">类型查找者接口</param>
        void Register(ContainerBuilder builder, ITypeFinder typeFinder);

        /// <summary>  
        /// 注册排序序号  
        /// </summary>
        int Order { get; }
    }
}
