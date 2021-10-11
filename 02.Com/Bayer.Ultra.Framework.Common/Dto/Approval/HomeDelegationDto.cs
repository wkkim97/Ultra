using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class HomeDelegationDto
    {
        /// <summary>
        /// 프로세스 아이디
        /// </summary>
        [DataMember]
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 이벤트 아이디
        /// </summary>
        [DataMember]
        public string EVENT_ID { get; set; }

        /// <summary>
        /// 이벤트 명
        /// </summary>
        [DataMember]
        public string EVENT_NAME { get; set; }

        /// <summary>
        /// 요청일자
        /// </summary>
        [DataMember]
        public string REQUEST_DATE { get; set; }

        /// <summary>
        /// Subject
        /// </summary>
        [DataMember]
        public string SUBJECT { get; set; }

        /// <summary>
        /// 기안자 아이디
        /// </summary>
        [DataMember]
        public string REQUESTER_ID { get; set; }

        /// <summary>
        /// 기안자 명
        /// </summary>
        [DataMember]
        public string REQUESTER_NAME { get; set; }

        /// <summary>
        /// 웹페이지명
        /// </summary>
        [DataMember]
        public string WEB_PAGE_NAME { get; set; }

        /// <summary>
        /// 프로세스 상태
        /// </summary>
        [DataMember]
        public string PROCESS_STATUS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string DELEGATION_ROLE { get; set; }
    }
}
