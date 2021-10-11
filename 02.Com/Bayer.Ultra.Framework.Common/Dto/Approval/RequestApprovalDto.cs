using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class RequestApprovalDto
    {
        [DataMember]
        public List<DTO_PROCESS_APPROVER> ApproverList { get; set; }

        [DataMember]
        public DTO_PROCESS_EVENT EventProcess { get; set; }

        [DataMember]
        public string EventID { get; set; }

        [DataMember]
        public string ProcessStatus { get; set; }

        [DataMember]
        public string UserID { get; set; }
    }
}
