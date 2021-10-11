using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Config.Element
{
    public class WebServerElement : ConfigurationElement
    {
        [ConfigurationProperty("uploadHandler", IsRequired = true)]
        public UploadHandlerElement UploadHandler
        {
            get
            {
                return (UploadHandlerElement)this["uploadHandler"];
            }
        }

        [ConfigurationProperty("uploadFile", IsRequired = true)]
        public UploadFileElement UploadFile
        {
            get
            {
                return (UploadFileElement)this["uploadFile"];
            }
        }
    }

    public class UploadHandlerElement : ConfigurationElement
    {
        [ConfigurationProperty("url", IsRequired = true)]
        public string Url
        {
            get
            {
                return (string)this["url"];
            }
        }

        [ConfigurationProperty("excel", IsRequired = true)]
        public string Excel
        {
            get
            {
                return (string)this["excel"];
            }
        }
    }


    public class UploadFileElement : ConfigurationElement
    {
        [ConfigurationProperty("temp", IsRequired = true)]
        public TempElement Temp
        {
            get
            {
                return (TempElement)this["temp"];
            }
        }

        [ConfigurationProperty("attach", IsRequired = true)]
        public AttachElement Attach
        {
            get
            {
                return (AttachElement)this["attach"];
            }
        }

        [ConfigurationProperty("electronicSign", IsRequired = true)]
        public ElectronicSignElement ElectronicSign
        {
            get
            {
                return (ElectronicSignElement)this["electronicSign"];
            }
        }
    }

    public class TempElement : ConfigurationElement
    {
        [ConfigurationProperty("physicalPath", IsRequired = true)]
        public string PhysicalPath
        {
            get
            {
                return (string)this["physicalPath"];
            }
        }

        [ConfigurationProperty("downloadUrl", IsRequired = false)]
        public string DownloadUrl
        {
            get
            {
                return (string)this["downloadUrl"];
            }
        }
    }

    public class AttachElement : ConfigurationElement
    {
        [ConfigurationProperty("physicalPath", IsRequired = true)]
        public string PhysicalPath
        {
            get
            {
                return (string)this["physicalPath"];
            }
        }

        [ConfigurationProperty("downloadUrl", IsRequired = true)]
        public string DownloadUrl
        {
            get
            {
                return (string)this["downloadUrl"];
            }
        }
    }

    public class ElectronicSignElement : ConfigurationElement
    {
        [ConfigurationProperty("physicalPath", IsRequired = true)]
        public string PhysicalPath
        {
            get
            {
                return (string)this["physicalPath"];
            }
        }

        [ConfigurationProperty("downloadUrl", IsRequired = true)]
        public string DownloadUrl
        {
            get
            {
                return (string)this["downloadUrl"];
            }
        }
    }
}
