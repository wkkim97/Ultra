using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_EVENT_BYHCP
    {
        [DataMember]
        public string EVENT_NAME { get; set; }

        [DataMember]
        public string EVENT_KEY { get; set; }

        [DataMember]
        public string HCP_NAME { get; set; }

        [DataMember]

        public string HCP_CODE { get; set; }
        [DataMember]

        public string HCO_NAME { get; set; }
        [DataMember]

        public string HCO_CODE { get; set; }

        [DataMember]
        public string START_DATE { get; set; }

        [DataMember]
        public string SUBJECT { get; set; }

        [DataMember]
        public string PROCESS_STATUS { get; set; }

        [DataMember]
        public string REQUEST_DATE { get; set; }

        [DataMember]
        public string FULL_NAME { get; set; }

        [DataMember]
        public string USER_ID { get; set; }

        [DataMember]
        public string ORG_ACRONYM { get; set; }

        [DataMember]
        public string PROCESS_ID { get; set; }

        [DataMember]
        public string HCP_TYPE { get; set; }
        [DataMember]
        public string IS_ATTENDED { get; set; }
        [DataMember]
        public string IS_DELETED { get; set; }
        

    }
}
