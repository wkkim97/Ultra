using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_PROCESS_APPROVAL_USER_STATUS
    {
        [DataMember]
        public string PROCESS_ID { get; set; }

        [DataMember]
        public string DOCUMENT_ID { get; set; }

        [DataMember]
        public string PROCESS_STATUS { get; set; }

        [DataMember]
        public string APPROVAL_TYPE { get; set; }

        [DataMember]
        public string APPROVER_STATUS { get; set; }
    }
}
