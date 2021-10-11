using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_PROCESS_APPROVER
    {
        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string APPROVAL_TYPE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public int APPROVAL_SEQ { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string APPROVER_TYPE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string APPROVER_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string APPROVER_ORG_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string ABSENCE_APPROVER_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string ABSENCE_APPROVER_ORG_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string STATUS { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string SENT_MAIL { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public DateTime? APPROVAL_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string COMMENT { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public DateTime CREATE_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public DateTime? UPDATE_DATE { get; set; }
    }
}
