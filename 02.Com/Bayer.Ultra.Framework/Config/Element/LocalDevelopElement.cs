using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Config.Element
{
    /// <summary>
    /// 로컬개발관련 항목 설정
    /// </summary>
    public class LocalDevelopElement : ConfigurationElement
    {
        [ConfigurationProperty("used", DefaultValue = false, IsRequired = true)]
        public bool Used
        {
            get
            {
                return Convert.ToBoolean(this["used"]);
            }
        }


        [ConfigurationProperty("developUser", IsRequired = true)]
        public DevelopUserElement DevelopUser
        {
            get
            {
                return (DevelopUserElement)this["developUser"];
            }
        }
    }

    public class DevelopUserElement : ConfigurationElement
    {
        [ConfigurationProperty("id", DefaultValue = "", IsRequired = true)]
        public string ID
        {
            get { return (string)this["id"]; }
        }

        [ConfigurationProperty("password", DefaultValue = "", IsRequired = true)]
        public string Password
        {
            get { return (string)this["password"]; }
        }
    }
}
