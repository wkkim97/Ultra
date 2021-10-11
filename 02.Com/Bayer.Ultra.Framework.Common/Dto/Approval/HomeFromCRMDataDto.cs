using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class HomeFromCRMDataDto
    {
        [DataMember]
        public string EVENT_DATE { get; set; }

        [DataMember]
        public string HCP_CODE { get; set; }

        [DataMember]
        public string HCP_NAME { get; set; }

        [DataMember]
        public string HCO_CODE { get; set; }

        [DataMember]
        public string HCO_NAME { get; set;}

        [DataMember]
        public string SPECIALTY_CODE { get; set; }

        [DataMember]
        public string SPECIALTY_NAME { get; set; }

        [DataMember]
        public string HCP_TYPE { get; set; }

        [DataMember]
        public string CRM_STATUS { get; set; }

        [DataMember]
        public string IS_VIOLATED { get; set; }
    }
}
