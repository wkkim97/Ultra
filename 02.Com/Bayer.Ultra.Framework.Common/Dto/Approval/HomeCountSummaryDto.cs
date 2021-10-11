using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class HomeCountSummaryDto
    {
        /// <summary>
        /// Not Submitted 개수
        /// </summary>
        [DataMember]
        public Int32 NOT_SUBMITTED { get; set; }

        /// <summary>
        /// Approval Queue 개수
        /// </summary>
        [DataMember]
        public Int32 APPROVAL_QUEUE { get; set; }

        /// <summary>
        /// Pending Approval 개수
        /// </summary>
        [DataMember]
        public Int32 PENDING_APPROVAL { get; set; }

        /// <summary>
        /// Delegation 개수
        /// </summary>
        [DataMember]
        public Int32 DELEGATION { get; set; }
    }
}
