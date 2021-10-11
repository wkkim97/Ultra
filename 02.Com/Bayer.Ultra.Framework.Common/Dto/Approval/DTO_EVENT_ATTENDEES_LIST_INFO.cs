using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_EVENT_ATTENDEES_LIST_INFO
    {
        [DataMember]
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string SUBJECT { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string START_TIME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string ADDRESS_OF_VENUE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string REQUESTER_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string REQUESTER_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string REQUESTER_DEPT { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string PRODUCT_NAME { get; set; }
    }
}
