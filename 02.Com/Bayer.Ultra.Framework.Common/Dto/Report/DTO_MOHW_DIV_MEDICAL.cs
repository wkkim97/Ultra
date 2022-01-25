using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Report
{
    [DataContract]
    public class DTO_MOHW_DIV_MEDICAL
    {


        //연번에 event Key 추가
        //[DataMember]
        //public int ROW_NUM { get; set; }
        [DataMember]
        public string ROW_NUM { get; set; }

        [DataMember]
        public string PRODUCT_NAME { get; set; }

        [DataMember]
        public string HCO_NAME { get; set; }

        [DataMember]
        public string HCO_HIRA_CODE { get; set; }

        [DataMember]
        public string HCP_NAMES { get; set; }

        [DataMember]
        public decimal AMOUNT { get; set; }

        [DataMember]
        public string ADDRESS_OF_VENUE { get; set; }

        [DataMember]
        public string EVENT_DATE { get; set; }
    }
}
