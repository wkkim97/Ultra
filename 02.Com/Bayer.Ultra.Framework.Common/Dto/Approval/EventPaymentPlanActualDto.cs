using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class EventPaymentPlanActualDto
    {
        [DataMember]
        public string CATEGORY_CODE { get; set; }

        [DataMember]
        public string CATEGORY_NAME { get; set; }

        [DataMember]
        public string DESC { get; set; }

        [DataMember]
        public decimal AMOUNT_PLAN { get; set; }

        [DataMember]
        public decimal AMOUNT_ACTUAL { get; set; }

        // version 1.0.5 Payment Complete 시, Plan vs Actual 30% Gap 발생시 comment 입력
        [DataMember]
        public string GAP_COMMENT { get; set; }
    }
}
