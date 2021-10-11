using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Config
{
    public class WebSiteSection : ConfigurationSection
    {
        /// <summary>
        /// Login 관련 정보 설정
        /// </summary>
        [ConfigurationProperty("login", IsRequired = false)]
        public Element.LogInElement Login
        {
            get
            {
                return (Element.LogInElement)this["login"];
            }
            set
            {
                this["login"] = value;
            }
        }

        /// <summary>
        /// 기본 ConnectionString
        /// </summary>
        [ConfigurationProperty("defaultDbConnection", IsRequired = false)]
        public Element.DefaultDbConnectionElement DefaultDbConnection
        {
            get
            {
                return (Element.DefaultDbConnectionElement)base["defaultDbConnection"];
            }
        }

        /// <summary>
        /// ConnectionString 목록
        /// </summary>
        [ConfigurationProperty("dbConnectionStrings", IsRequired = false)]
        public Element.DBServerCollection ConnectionStrings
        {
            get
            {
                return (Element.DBServerCollection)base["dbConnectionStrings"];
            }
        }

        /// <summary>
        /// 웹서비스 관련 정보 목록
        /// </summary>
        [ConfigurationProperty("wcfServices", IsRequired = false)]
        public Element.WcfServiceElement WcfServices
        {
            get
            {
                return (Element.WcfServiceElement)this["wcfServices"];
            }
            set
            {
                this["wcfServices"] = value;
            }
        }

        /// <summary>
        /// 로컬 개발 여부 설정
        /// </summary>
        [ConfigurationProperty("localDevelop", IsRequired = false)]
        public Element.LocalDevelopElement LocalDevelop
        {
            get
            {
                return (Element.LocalDevelopElement)this["localDevelop"];
            }
        }

        [ConfigurationProperty("webServer", IsRequired = false)]
        public Element.WebServerElement WebServer
        {
            get
            {
                return (Element.WebServerElement)this["webServer"];
            }
        }

        /// <summary>
        /// 사용자 설정 관련
        /// </summary>
        [ConfigurationProperty("userConfiguration", IsRequired = false)]
        public Element.UserConfigurationElement UserConfiguration
        {
            get
            {
                return (Element.UserConfigurationElement)this["userConfiguration"];
            }
        }

        [ConfigurationProperty("clientScriptVariables", IsRequired = false)]
        public Element.ClientScriptVariableCollection ClientScriptVariables
        {
            get
            {
                return (Element.ClientScriptVariableCollection)base["clientScriptVariables"];
            }
        }

        [ConfigurationProperty("activeDirectory", IsRequired = false)]
        public Element.ActiveDirectoryElement ActiveDirectory
        {
            get
            {
                return (Element.ActiveDirectoryElement)base["activeDirectory"];
            }
        }

        [ConfigurationProperty("ultraInfo", IsRequired = false)]
        public Element.UltraInfoElement UltraInfo
        {
            get
            {
                return (Element.UltraInfoElement)base["ultraInfo"];
            }
        }

        [ConfigurationProperty("smtpManager", IsRequired = false)]
        public Element.SmtpManagerElement SmtpManager
        {
            get
            {
                return (Element.SmtpManagerElement)base["smtpManager"];
            }
        }
    }
}
