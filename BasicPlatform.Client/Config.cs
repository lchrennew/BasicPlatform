using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using System.IO;
using System.Web.Configuration;

namespace BasicPlatform.Client
{
    internal static class Config
    {
        static Config()
        {
            LoadConfig(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"bin\bpo.config"));

            authApi = WebConfigurationManager.AppSettings["bpo.api"];
            apiKey = WebConfigurationManager.AppSettings["bpo.key"];
            apiSecret = WebConfigurationManager.AppSettings["bpo.secret"];
            iPermission = WebConfigurationManager.AppSettings["bpo.permission"] ?? "BasicPlatform.Client.Permission, BasicPlatform.Client";
            iUserProcessor = WebConfigurationManager.AppSettings["bpo.userprocessor"] ?? "BasicPlatform.Client.UserProcessor, BasicPlatform.Client";
            iGroupProcessor = WebConfigurationManager.AppSettings["bpo.groupprocessor"] ?? "BasicPlatform.Client.GroupProcessor, BasicPlatform.Client";
            var app = Bpc.GetApp();
            if (app != null)
            {
                clientIdentifier = app.Label;
                connectUrl = app.ConnectUrl;
                connected = !string.IsNullOrEmpty(app.ConnectUrl);
                selfConnectable = connected && app.SelfConnectable;
            }
            else throw new ConfigurationErrorsException("app not exists");
        }

        internal static void Initialize() { }

        /// <summary>
        /// 加载config
        /// </summary>
        /// <param name="configFilePath"></param>
        static void LoadConfig(string configFilePath)
        {
            try
            {
                ExeConfigurationFileMap fileMap = new ExeConfigurationFileMap
                {
                    ExeConfigFilename = configFilePath
                };
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(fileMap, ConfigurationUserLevel.None);
                foreach (KeyValueConfigurationElement element in config.AppSettings.Settings)
                {
                    ConfigurationManager.AppSettings[element.Key] = element.Value;
                }
            }
            catch (Exception exception)
            {
                throw new Exception("config文件不存在或配置错误." + exception.Message);
            }
        }

        internal static string authApi;
        internal static string apiKey;
        internal static string apiSecret;
        internal static string clientIdentifier;
        internal static string connectUrl;
        internal static bool connected;
        internal static bool selfConnectable;
        internal static string iPermission;
        internal static string iUserProcessor;
        internal static string iGroupProcessor;
    }
}
