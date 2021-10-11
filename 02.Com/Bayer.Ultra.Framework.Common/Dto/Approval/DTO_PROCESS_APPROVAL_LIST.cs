using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_PROCESS_APPROVAL_LIST
    {

        [DataMember]
        public string PROCESS_ID { get; set; }

        [DataMember]
        public Int64 IDX { get; set; }

        [DataMember]
        public string APPROVAL_TYPE { get; set; }

        [DataMember]
        public string APPROVER_ID { get; set; }

        [DataMember]
        public string APPROVER { get; set; }

        [DataMember]
        public string MAIL_ADDRESS { get; set; }

        [DataMember]
        public string COMPANY_NAME { get; set; }

        [DataMember]
        public string APPROVER_ORG_NAME { get; set; }

        [DataMember]
        public string STATUS { get; set; }

        [DataMember]
        public string APPROVER_STATUS { get; set; }

        [DataMember]
        public DateTime? APPROVAL_DATE { get; set; }

        [DataMember]
        public string COMMENT { get; set; }

        [DataMember]
        public DateTime? UPDATE_DATE { get; set; }
    }
}
