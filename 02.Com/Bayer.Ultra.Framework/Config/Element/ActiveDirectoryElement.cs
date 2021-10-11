using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Config.Element
{
    /// <summary>
    /// <activeDirectory domainName="ap.bayer.cnb" fqdn="bhc-ap" netBios="ap.bayer.cnb" ldapPath="LDAP://BHC-AP" />
    /// </summary>
    public class ActiveDirectoryElement : ConfigurationElement
    {
        [ConfigurationProperty("domainName", IsRequired = true)]
        public string DomainName
        {
            get
            {
                return (string)this["domainName"];
            }
        }

        [ConfigurationProperty("fqdn", IsRequired = true)]
        public string FQDN
        {
            get
            {
                return (string)this["fqdn"];
            }
        }

        [ConfigurationProperty("netBios", IsRequired = true)]
        public string NetBIOS
        {
            get
            {
                return (string)this["netBios"];
            }
        }

        [ConfigurationProperty("ldapPath", IsRequired = true)]
        public string LdapPath
        {
            get
            {
                return (string)this["ldapPath"];
            }
        }
    }
}
