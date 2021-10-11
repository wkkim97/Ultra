using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_PROCESS_EVENT
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
        /// 주제
        /// </summary> 
        [DataMember]
        public string SUBJECT { get; set; }

        /// <summary>
        /// 이벤트 시작일자
        /// </summary>
        [DataMember]
        public string START_DATE { get; set; }

        /// <summary>
        /// 이벤트 번호
        /// </summary> 
        [DataMember]
        public string EVENT_KEY { get; set; }

        /// <summary>
        /// 처리 상태
        /// </summary> 
        [DataMember]
        public string PROCESS_STATUS { get; set; }

        /// <summary>
        /// 요청일자
        /// </summary> 
        [DataMember]
        public DateTime? REQUEST_DATE { get; set; }

        /// <summary>
        /// 회사코드
        /// </summary> 
        [DataMember]
        public string COMPANY_CODE { get; set; }

        /// <summary>
        /// 요청자
        /// </summary> 
        [DataMember]
        public string REQUESTER_ID { get; set; }

        [DataMember]
        public string REQUESTER_NAME { get; set; }

        /// <summary>
        /// 현 결재자
        /// </summary> 
        [DataMember]
        public string CURRENT_APPROVER { get; set; }

        /// <summary>
        /// 최종 결재자
        /// </summary> 
        [DataMember]
        public string FINAL_APPROVER { get; set; }

        /// <summary>
        /// 완료일자
        /// </summary> 
        [DataMember]
        public DateTime? COMPLETED_DATE { get; set; }

        /// <summary>
        /// 거부일자
        /// </summary> 
        [DataMember]
        public string REJECTED_PROCESS_ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string BY_CRM { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string CRM_STATUS { get; set; }

        /// <summary>
        /// 삭제여부
        /// </summary> 
        [DataMember]
        public string IS_DELETED { get; set; }

        /// <summary>
        /// 미사용여부
        /// </summary> 
        [DataMember]
        public string IS_DISUSED { get; set; }

        /// <summary>
        /// 생성일자
        /// </summary> 
        [DataMember]
        public DateTime? CREATE_DATE { get; set; }

        /// <summary>
        /// 수정일자
        /// </summary> 
        [DataMember]
        public DateTime? UPDATE_DATE { get; set; }


    }
}
