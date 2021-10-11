using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Config.Element
{

    [ConfigurationCollection(typeof(DBServerElement))]
    public class DBServerCollection : ConfigurationElementCollection
    {
        internal const string PropertyName = "dbServer";
        protected override ConfigurationElement CreateNewElement()
        {
            return new DBServerElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            if (element == null)
                throw new ArgumentNullException("element");

            return ((DBServerElement)element).Name;
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
        public DBServerElement this[int idx]
        {
            get { return (DBServerElement)BaseGet(idx); }
        }

        new public DBServerElement this[string Name]
        {
            get
            {
                return (DBServerElement)BaseGet(Name);
            }
        }
    }

    public class DBServerElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true, IsKey = true)]
        public string Name
        {
            get
            {
                return (string)this["name"];
            }
        }

        [ConfigurationProperty("connectionString", IsRequired = true)]
        public string ConnectionString
        {
            get
            {
                return (string)this["connectionString"];
            }
        }
        //windows authentification
        [ConfigurationProperty("providerName", IsRequired = true)]
        public string providerName
        {
            get
            {
                return (string)this["providerName"];
            }
        }
    }
}
