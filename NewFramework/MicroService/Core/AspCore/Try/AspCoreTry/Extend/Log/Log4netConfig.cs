using System;
using System.IO;
using log4net;
using log4net.Config;
using Microsoft.Extensions.Configuration;

public class Log4NetConfig
    {
        public static string RepositoryName { get; set; }

        public static void Init(IConfiguration configuration)
        {
            var repositoryName = configuration.GetSection("Log4Net:RepositoryName").Value;
            if (string.IsNullOrWhiteSpace(repositoryName))
            {
                throw new Exception("必须在配置文件中添加 Log4Net > RepositoryName 节点");
            }

            RepositoryName = repositoryName;

            var configFilePath = configuration.GetSection("Log4Net:ConfigFilePath").Value;
            if (string.IsNullOrWhiteSpace(configFilePath))
            {
                configFilePath = "log4net.config";
            }

            var file = new FileInfo(configFilePath);
            var repository = LogManager.CreateRepository(repositoryName);
            XmlConfigurator.Configure(repository, file);
        }
    }