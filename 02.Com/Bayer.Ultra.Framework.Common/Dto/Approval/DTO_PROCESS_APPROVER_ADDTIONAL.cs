using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_PROCESS_APPROVER_ADDTIONAL
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
        public int IDX { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string APPROVAL_TYPE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string USER_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public DateTime? CREATE_DATE { get; set; }
    }
}
