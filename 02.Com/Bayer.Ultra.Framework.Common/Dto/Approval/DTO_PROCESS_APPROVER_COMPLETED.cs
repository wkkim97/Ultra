using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_PROCESS_APPROVER_COMPLETED
    {
        /// <summary>
        /// 프로세스 아이디
        /// </summary> 
        [DataMember]
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 결재 타입
        /// </summary> 
        [DataMember]
        public string APPROVAL_TYPE { get; set; }

        /// <summary>
        /// 결재 일련번호
        /// </summary> 
        [DataMember]
        public int APPROVAL_SEQ { get; set; }

        /// <summary>
        /// 결재자 타입
        /// </summary> 
        [DataMember]
        public string APPROVER_TYPE { get; set; }

        /// <summary>
        /// 결재자
        /// </summary> 
        [DataMember]
        public string APPROVER_ID { get; set; }

        /// <summary>
        /// 결재자 소속
        /// </summary> 
        [DataMember]
        public string APPROVER_ORG_NAME { get; set; }

        /// <summary>
        /// 위임자
        /// </summary> 
        [DataMember]
        public string ABSENCE_APPROVER_ID { get; set; }

        /// <summary>
        /// 위임자 소속
        /// </summary> 
        [DataMember]
        public string ABSENCE_APPROVER_ORG_NAME { get; set; }

        /// <summary>
        /// 상태
        /// </summary> 
        [DataMember]
        public string STATUS { get; set; }

        /// <summary>
        /// 메일 발송 여부
        /// </summary> 
        [DataMember]
        public string SENT_MAIL { get; set; }

        /// <summary>
        /// 메일 발송 코멘트 여부
        /// </summary> 
        [DataMember]
        public string SENT_MAIL_COMMENT { get; set; }

        /// <summary>
        /// 결재일자
        /// </summary> 
        [DataMember]
        public DateTime? APPROVAL_DATE { get; set; }

        /// <summary>
        /// 코멘트
        /// </summary> 
        [DataMember]
        public string COMMENT { get; set; }

        /// <summary>
        /// 수정일자
        /// </summary> 
        [DataMember]
        public DateTime? UPDATE_DATE { get; set; }
    }
}
