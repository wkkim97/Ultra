using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Config.Element
{
    public class LogInElement : ConfigurationElement
    {
        [ConfigurationProperty("url", IsRequired = true)]
        public string URL
        {
            get
            {
                return (string)this["url"];
            }
        }

        [ConfigurationProperty("domain", IsRequired = true)]
        public string Domain
        {
            get
            {
                return (string)this["domain"];
            }
        }

        [ConfigurationProperty("useEncryption", IsRequired = true)]
        public bool UseEncryption
        {
            get
            {
                return Convert.ToBoolean(this["useEncryption"]);
            }
        }
    }
}
