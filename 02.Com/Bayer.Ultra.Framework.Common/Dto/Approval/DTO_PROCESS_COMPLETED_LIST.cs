using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_PROCESS_COMPLETED_LIST
    {
        [DataMember]
        public string PROCESS_ID { get; set; }

        [DataMember]
        public string EVENT_ID { get; set; }

        [DataMember]
        public string EVENT_NAME { get; set; }

        [DataMember]
        public string SUBJECT { get; set; }

        [DataMember]
        public string EVENT_KEY { get; set; }

        [DataMember]
        public string PROCESS_STATUS { get; set; }

        [DataMember]
        public string REQUESTER_ID { get; set; }

        [DataMember]
        public string REQUESTER { get; set; }

        [DataMember]
        public string FINAL_APPROVER { get; set; }

        [DataMember]
        public string FINAL_APPROVER_NAME { get; set; }

        [DataMember]
        public string START_DATE { get; set; }

        [DataMember]
        public string REQUEST_DATE { get; set; }

        [DataMember]
        public string CREATE_DATE { get; set; }

        [DataMember]
        public string COMMENT { get; set; }

        [DataMember]
        public string ATTACHFILEYN { get; set; }

        [DataMember]
        public string WEB_PAGE_NAME { get; set; }
    }
}
