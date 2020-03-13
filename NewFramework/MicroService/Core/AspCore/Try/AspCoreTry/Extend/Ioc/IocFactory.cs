using System;
using System.Configuration;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Practices.Unity.Configuration;
using Unity;
using Unity.Interception;
/// <summary>
/// 此类作用：容器实体，使用App_Data\unity.config文件配置,根据XML配置初始化IOC Created by  ZhangQC  2016.08.08 
/// </summary>
public partial class IocFactory
    {
        /// <summary>
        /// 依赖注入容器
        /// </summary>
        private static IUnityContainer _container;

        /// <summary>
        /// 初始化容器
        /// </summary>
        private static IUnityContainer Init(IConfiguration configuration)
        {
            //实例化容器
            _container = new UnityContainer();

            _container.AddNewExtension<Interception>();

            // Unity只会调用标识了InjectionConstructor特性的构造函数，这样就很好的解决了多构造函数的情况下，Unity调用哪个构造函数。

            var unityConfig = configuration.GetSection("Unity:ConfigFilePath").Value;

            if (!File.Exists(unityConfig))
            {
                throw new Exception("小朋友，我没有发现配置文件，你是不是跟我开玩笑呢？");
            }

            var fileMap = new ExeConfigurationFileMap { ExeConfigFilename = unityConfig };
            var configurationManager = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
            var unitySection = (UnityConfigurationSection)configuration.GetSection("unity");
            _container.LoadConfiguration(unitySection);
            return _container;
        }
    }