using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_APPROVAL_LINE
    {
        [DataMember]
        public Int64 IDX { get; set; }

        [DataMember]
        public string APPROVAL_TYPE { get; set; }

        [DataMember]
        public string USER_ID { get; set; }

        [DataMember]
        public string USER_NAME { get; set; }

        [DataMember]
        public string ABSENCE_USER_ID { get; set; }

        [DataMember]
        public string ABSENCE_USER_NAME { get; set; }

        [DataMember]
        public string SUPERVISOR_USER_ID { get; set; }

        [DataMember]
        public int APPROVAL_LEVEL { get; set; }

        [DataMember]
        public int APPROVAL_SEQ { get; set; }

        [DataMember]
        public string APPROVER_TYPE { get; set; }

        [DataMember]
        public string ORG_NAME { get; set; }

        [DataMember]
        public string ABSENCE_ORG_NAME { get; set; }

        [DataMember]
        public string JOB_TITLE { get; set; }

        [DataMember]
        public int? JOB_TITLE_NUM { get; set; }

        [DataMember]
        public decimal? LIMIT_AMOUNT { get; set; }

        [DataMember]
        public string PHONE { get; set; }

        [DataMember]
        public string MAIL_ADDRESS { get; set; }
    }
}
