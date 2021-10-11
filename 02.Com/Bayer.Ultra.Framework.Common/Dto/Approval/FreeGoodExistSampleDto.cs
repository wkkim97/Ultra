using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class FreeGoodExistSampleDto
    {
        [DataMember]
        public string PROCESS_ID { get; set; }

        [DataMember]
        public string SUBJECT { get; set; }

        [DataMember]
        public string EVENT_KEY { get; set; }

        [DataMember]
        public string PROCESS_STATUS { get; set; }

        [DataMember]
        public string REQUESTER_ID { get; set; }

        [DataMember]
        public string REQUESTER_NAME { get; set; }

        [DataMember]
        public string REQUEST_DATE { get; set; }

        [DataMember]
        public string ORGANIZATION_NAME { get; set; }

        [DataMember]
        public string SAMPLE_CODE { get; set; }

        [DataMember]
        public string PRODUCT_NAME { get; set; }

        [DataMember]
        public string HCP_NAME { get; set; }

        [DataMember]
        public string HCO_NAME { get; set; }
    }
}
