﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using webTemplate.Global.Config;

namespace webTemplate.UnitTest.Tools
{
    public class TestConfig : IConfig
    {
        private Configuration configuration;

        public TestConfig(string configPath)
        {
            var configFileMap = new ExeConfigurationFileMap();
            configFileMap.ExeConfigFilename = configPath;
            configuration = ConfigurationManager.OpenMappedExeConfiguration(configFileMap, ConfigurationUserLevel.None);
        }
      
        #region IConfig Members

        public string ConnectionStrings(string connectionString)
        {
            return configuration.ConnectionStrings.ConnectionStrings[connectionString].ConnectionString;
        }

        public string CultureCode
        {
            get
            {
                var culture = configuration.AppSettings.Settings["Culture"].Value;
                if (culture != null)
                {
                    return culture;
                }
                return "en";
            }
        }

        public CultureInfo Culture
        {
            get
            {
                return new CultureInfo(CultureCode);
            }
        }
        public bool DebugMode
        {
            get
            {
                return bool.Parse(configuration.AppSettings.Settings["DebugMode"].Value);
            }
        }

        public string AdminEmail
        {
            get
            {
                return configuration.AppSettings.Settings["AdminEmail"].Value;
            }
        }

        public bool EnableMail
        {
            get
            {
                return bool.Parse(configuration.AppSettings.Settings["EnableMail"].Value);
            }
        }

        public IQueryable<MimeType> MimeTypes
        {
            get
            {
                MimeTypesConfigSection configInfo = (MimeTypesConfigSection)configuration.GetSection("mimeConfig");
                return configInfo.mimeTypes.OfType<MimeType>().AsQueryable<MimeType>();
            }
        }

        public MailSetting MailSetting
        {
            get
            {
                return (MailSetting)configuration.GetSection("mailConfig");
            }
        }


        public IQueryable<MailTemplate> MailTemplates
        {
            get
            {
                MailTemplateConfig configInfo = (MailTemplateConfig)configuration.GetSection("mailTemplatesConfig");
                return configInfo.mailTemplates.OfType<MailTemplate>().AsQueryable<MailTemplate>();
            }
        }

        public IQueryable<IconSize> IconSizes
        {
            get
            {
                IconSizesConfigSection configInfo = (IconSizesConfigSection)configuration.GetSection("iconConfig");
                if (configInfo != null)
                {
                    return configInfo.iconSizes.OfType<IconSize>().AsQueryable<IconSize>();
                }
                return null;
            }
        }

        #endregion
    }
}
