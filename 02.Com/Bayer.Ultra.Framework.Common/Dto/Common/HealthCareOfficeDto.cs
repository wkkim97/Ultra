using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Common
{
    [DataContract]
    public class HealthCareOfficeDto
    {
        [DataMember]
        public string HCO_CODE { get; set; }

        [DataMember]
        public string HCO_NAME { get; set; }

        [DataMember]
        public string ADDRESS { get; set; }
    }
}
