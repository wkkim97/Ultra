using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class EventPaymentByHCPDto
    {
        [DataMember]
        public string SECTOR_NAME { get; set; }

        [DataMember]
        public string HCP_CODE { get; set; }

        [DataMember]
        public string IS_ATTENDED { get; set; }

        [DataMember]
        public string DOC_NAME { get; set; }

        [DataMember]
        public string HCO_NAME { get; set; }

        [DataMember]
        public string SPECIALITY { get; set; }

        [DataMember]
        public string ROLE { get; set; }

        [DataMember]
        public decimal AMOUNT_1 { get; set; }

        [DataMember]
        public decimal AMOUNT_2 { get; set; }

        [DataMember]
        public decimal AMOUNT_3 { get; set; }

        [DataMember]
        public decimal AMOUNT_4 { get; set; }

        [DataMember]
        public decimal AMOUNT_5 { get; set; }

        [DataMember]
        public decimal AMOUNT_TOTAL { get; set; }

        [DataMember]
        public int COUNT_CONCOUR { get; set; }

        [DataMember]
        public int COUNT_SRM { get; set; }

        [DataMember]
        public int COUNT_YOURDOCES { get; set; }

    }
}
