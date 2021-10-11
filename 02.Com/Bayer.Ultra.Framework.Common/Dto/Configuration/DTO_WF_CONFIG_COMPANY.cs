using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Configuration
{
    [DataContract]
    public class DTO_WF_CONFIG_COMPANY
    {
        [DataMember]
        public string EVENT_ID { get; set; }

        [DataMember]
        public string COMPANY_CODE { get; set; }

        [DataMember]
        public string CREATOR_ID { get; set; }

        [DataMember]
        public DateTime CREATE_DATE { get; set; }
    }
}
