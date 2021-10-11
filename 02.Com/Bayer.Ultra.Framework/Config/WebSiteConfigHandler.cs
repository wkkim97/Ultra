using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Config
{
    /// <summary>
    /// 설정정보 관련
    /// https://msdn.microsoft.com/en-US/library/system.configuration.sectioninformation.configsource(v=vs.100).aspx
    /// https://msdn.microsoft.com/ko-kr/library/windows/desktop/2tw134k3(v=vs.85).aspx
    /// </summary>
    public class WebSiteConfigHandler
    {
        static Configuration _config;
        static WebSiteSection _webSiteSection;

        static WebSiteConfigHandler()
        {
            Initialize();
        }

        /// <summary>
        /// 설정 초기화
        /// </summary>
        private static void Initialize()
        {
            _config = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration("~");
            //_config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            ConfigurationSectionGroup sectionGroup = _config.SectionGroups[Core.Consts.ConfigSectionGroupName];
            _webSiteSection = (WebSiteSection)sectionGroup.Sections[Core.Consts.ConfigSectionWebSiteName];
        }

        public static WebSiteSection RootSection
        {
            get
            {
                return _webSiteSection;
            }
        }

        static Element.ActiveDirectoryElement _activeDirectory;
        public static Element.ActiveDirectoryElement ActiveDirectory
        {
            get
            {
                if (_activeDirectory == null)
                    _activeDirectory = _webSiteSection.ActiveDirectory;
                return _activeDirectory;
            }
        }

        static Element.SmtpManagerElement _smtpManager;
        public static Element.SmtpManagerElement SmtpManager
        {
            get
            {
                if (_smtpManager == null)
                    _smtpManager = _webSiteSection.SmtpManager;  
                return _smtpManager;
            }
        }

        static Element.LogInElement _loginElement;
        /// <summary>
        /// LogIn 정보
        /// </summary>
        public static Element.LogInElement Login
        {
            get
            {
                if (_loginElement == null)
                    _loginElement = _webSiteSection.Login;
                return _loginElement;
            }
        }

        static Element.DefaultDbConnectionElement _defaultDbConnection;
        /// <summary>
        /// 기본 ConnectionString
        /// </summary>
        public static Element.DefaultDbConnectionElement DefaultDbConnection
        {
            get
            {
                if (_defaultDbConnection == null)
                    _defaultDbConnection = _webSiteSection.DefaultDbConnection;
                return _defaultDbConnection;
            }
        }

        static Element.DBServerCollection _dbServerCollction;
        /// <summary>
        /// ConnectionString 목록
        /// </summary>
        public static Element.DBServerCollection ConnectionStrings
        {
            get
            {
                if (_dbServerCollction == null)
                    _dbServerCollction = _webSiteSection.ConnectionStrings;
                return _dbServerCollction;
            }
        }

        static Element.WcfServiceElement _wcfServicesElement;
        /// <summary>
        /// 웹서비스 목록
        /// </summary>
        public static Element.WcfServiceElement WcfServices
        {
            get
            {
                if (_wcfServicesElement == null)
                    _wcfServicesElement = _webSiteSection.WcfServices;
                return _wcfServicesElement;
            }
        }

        /// <summary>
        /// 로컬 개발관련 정보
        /// </summary>
        static Element.LocalDevelopElement _localDevelop;
        public static Element.LocalDevelopElement LocalDevelop
        {
            get
            {
                if (_localDevelop == null)
                    _localDevelop = _webSiteSection.LocalDevelop;
                return _localDevelop;
            }
        }

        /// <summary>
        /// 사용자 환경 설정
        /// </summary>
        static Element.UserConfigurationElement _userConfiguration;
        public static Element.UserConfigurationElement UserConfiguration
        {
            get
            {
                if (_userConfiguration == null)
                    _userConfiguration = _webSiteSection.UserConfiguration;
                return _userConfiguration;
            }
        }

        /// <summary>
        /// WebServer 설정
        /// </summary>
        static Element.WebServerElement _webServer;
        public static Element.WebServerElement WebServer
        {
            get
            {
                if (_webServer == null)
                    _webServer = _webSiteSection.WebServer;
                return _webServer;
            }
        }

        /// <summary>
        /// Client Side Script 멤버변수 목록
        /// </summary>
        static Element.ClientScriptVariableCollection _clientScriptVariables;
        public static Element.ClientScriptVariableCollection ClientScriptVariables
        {
            get
            {
                if (_clientScriptVariables == null)
                    _clientScriptVariables = _webSiteSection.ClientScriptVariables;
                return _clientScriptVariables;
            }
        }

        static Element.UltraInfoElement _ultraInfo;

        public static Element.UltraInfoElement UltraInfo
        {
            get
            {
                if (_ultraInfo == null)
                    _ultraInfo = _webSiteSection.UltraInfo;
                return _ultraInfo;
            }
        }
    }
}
