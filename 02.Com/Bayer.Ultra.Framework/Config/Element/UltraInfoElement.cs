using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Config.Element
{
    public class UltraInfoElement : ConfigurationElement
    {
        [ConfigurationProperty("pages", IsRequired = true)]
        public PagesElement Pages
        {
            get { return (PagesElement)this["pages"]; }
        }

    }

    public class PagesElement : ConfigurationElement
    {
        [ConfigurationProperty("help", IsRequired = false)]
        public string Help
        {
            get { return (string)this["help"]; }
        }

        [ConfigurationProperty("error", IsRequired = false)]
        public string Error
        {
            get { return (string)this["error"]; }
        }
    }
}
