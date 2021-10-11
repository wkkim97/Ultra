using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Bayer.Ultra.Framework.Config.Element
{
    public class SmtpManagerElement : ConfigurationElement
    {
        [ConfigurationProperty("smtpServer", IsRequired = true)]
        public string SmtpServer
        {
            get
            {
                return (string)this["smtpServer"];
            }
        }

        [ConfigurationProperty("mailDomain", IsRequired = true)]
        public string MailDomain
        {
            get
            {
                return (string)this["mailDomain"];
            }
        }

        [ConfigurationProperty("serder", IsRequired = true)]
        public string Sender
        {
            get
            {
                return (string)this["serder"];
            }
        }

        [ConfigurationProperty("ps", IsRequired = true)]
        public string Password
        {
            get
            {
                return (string)this["ps"];
            }
        }

        [ConfigurationProperty("mailFormat", IsRequired = true)]
        public string MailFormat
        {
            get
            {
                return (string)this["mailFormat"];
            }
        }

        [ConfigurationProperty("eventUrl", IsRequired = true)]
        public string EventUrl
        {
            get
            {
                return (string)this["eventUrl"];
            }
        }
    }
}
