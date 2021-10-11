using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Common
{
    [DataContract]
    public class DTO_MASTER_COUNTRY
    {
        [DataMember]
        public string COUNTRY_CODE { get; set; }

        [DataMember]
        public string COUNTRY_NAME { get; set; }
    }

}
