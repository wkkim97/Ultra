using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Config.Element
{
    [ConfigurationCollection(typeof(ClientScriptVariableElement))]
    public class ClientScriptVariableCollection : ConfigurationElementCollection
    {

        internal const string PropertyName = "add";
        protected override ConfigurationElement CreateNewElement()
        {
            return new ClientScriptVariableElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element");

            return ((ClientScriptVariableElement)element).Key;
        }

        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get
            {
                return PropertyName;
            }
        }
        public ClientScriptVariableElement this[int idx]
        {
            get { return (ClientScriptVariableElement)BaseGet(idx); }
        }

        new public ClientScriptVariableElement this[string key]
        {
            get
            {
                return (ClientScriptVariableElement)BaseGet(key);
            }
        }
    }

    public class ClientScriptVariableElement : ConfigurationElement
    {
        [ConfigurationProperty("key", IsRequired = true)]
        public string Key
        {
            get
            {
                return (string)this["key"];
            }
        }

        [ConfigurationProperty("value")]
        public string Value
        {
            get
            {
                return (string)this["value"];
            }
        }

        [ConfigurationProperty("path")]
        public string Path
        {
            get
            {
                return (string)this["path"];
            }
        }

        [ConfigurationProperty("attr")]
        public string Attr
        {
            get
            {
                return (string)this["attr"];
            }
        }
    }
}
