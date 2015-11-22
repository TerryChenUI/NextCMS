using System;
using System.Configuration;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using NextCMS.Core.Configuration;
using NextCMS.Core.Infrastructure;

namespace NextCMS.Core.Infrastructure
{
    /// <summary>
    /// 使用单例模式的引擎 Provides access to the singleton instance of the NextCMS engine.
    /// </summary>
    public class EngineContext
    {
        #region Utilities

        /// <summary>
        /// Creates a factory instance and adds a http application injecting facility.
        /// </summary>
        /// <param name="config">Config</param>
        /// <returns>New engine instance</returns>
        protected static IEngine CreateEngineInstance(NextCMSConfig config)
        {
            if (config != null && !string.IsNullOrEmpty(config.EngineType))
            {
                var engineType = Type.GetType(config.EngineType);
                if (engineType == null)
                    throw new ConfigurationErrorsException("The type '" + config.EngineType + "' could not be found. Please check the configuration at /configuration/NextCMS/engine[@engineType] or check for missing assemblies.");
                if (!typeof(IEngine).IsAssignableFrom(engineType))
                    throw new ConfigurationErrorsException("The type '" + engineType + "' doesn't implement 'NextCMS.Core.Infrastructure.IEngine' and cannot be configured in /configuration/NextCMS/engine[@engineType] for that purpose.");
                return Activator.CreateInstance(engineType) as IEngine;
            }

            return new NextCMSEngine();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Initializes a static instance of the NextCMS factory.
        /// </summary>
        /// <param name="forceRecreate">Creates a new factory instance even though the factory has been previously initialized.</param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public static IEngine Initialize(bool forceRecreate)
        {
            if (Singleton<IEngine>.Instance == null || forceRecreate)
            {
                var config = ConfigurationManager.GetSection("NextCMSConfig") as NextCMSConfig;
                Singleton<IEngine>.Instance = CreateEngineInstance(config);
                //初始化配置，注册系统中的组件和插件
                Singleton<IEngine>.Instance.Initialize(config);
            }
            return Singleton<IEngine>.Instance;
        }

        /// <summary>
        /// Sets the static engine instance to the supplied engine. Use this method to supply your own engine implementation.
        /// </summary>
        /// <param name="engine">The engine to use.</param>
        /// <remarks>Only use this method if you know what you're doing.</remarks>
        public static void Replace(IEngine engine)
        {
            Singleton<IEngine>.Instance = engine;
        }

        #endregion

        #region Properties

        /// <summary>
        /// Gets the singleton NextCMS engine used to access NextCMS services.
        /// </summary>
        public static IEngine Current
        {
            get
            {
                if (Singleton<IEngine>.Instance == null)
                {
                    Initialize(false);
                }
                return Singleton<IEngine>.Instance;
            }
        }

        #endregion
    }
}
