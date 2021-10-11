using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Config.Element
{
    public class UserConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("listCount", DefaultValue = 20)]
        public int ListCount
        {
            get
            {
                return Convert.ToInt32(this["listCount"]);
            }
        }

        [ConfigurationProperty("pageCount", DefaultValue = 10)]
        public int PageCount
        {
            get
            {
                return Convert.ToInt32(this["pageCount"]);
            }
        }
    }
}
