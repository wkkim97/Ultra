using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_MODULE_AGENDA_ROLE
    {
        /// <summary>
        /// 프로세스 아이디
        /// </summary> 
        [DataMember]
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 프로세스 제목
        /// </summary>
        [DataMember]
        public string SUBJECT { get; set; }

        /// <summary>
        /// 프로세스 시작일
        /// </summary>
        [DataMember]
        public string START_DATE { get; set; }

        /// <summary>
        /// 아젠다 일련번호
        /// </summary> 
        [DataMember]
        public int AGENDA_IDX { get; set; }

        /// <summary>
        /// 아젠다 역할 일련버호
        /// </summary> 
        [DataMember]
        public int AGENDA_ROLE_IDX { get; set; }

        /// <summary>
        /// 참석자 일련번호
        /// </summary> 
        [DataMember]
        public int PARTICIPANT_IDX { get; set; }

        /// <summary>
        /// 참석자 코드
        /// </summary>
        [DataMember]
        public string PARTICIPANT_CODE { get; set; }

        /// <summary>
        /// 참석자 명
        /// </summary>
        [DataMember]
        public string PARTICIPANT_NAME { get; set; }

        /// <summary>
        /// 병원 명
        /// </summary>
        [DataMember]
        public string HCO_NAME { get; set; }

        /// <summary>
        /// 전공
        /// </summary>
        [DataMember]
        public string SPECIALTY_NAME { get; set; }

        /// <summary>
        /// Role Type
        /// </summary> 
        [DataMember]
        public string ROLE_TYPE { get; set; }

        /// <summary>
        /// 역할 코드
        /// </summary> 
        [DataMember]
        public string ROLE { get; set; }

        /// <summary>
        /// Criteria
        /// </summary> 
        [DataMember]
        public string CRITERIA { get; set; }

        /// <summary>
        /// Reason
        /// </summary> 
        [DataMember]
        public string REASON { get; set; }

        /// <summary>
        /// 금액
        /// </summary> 
        [DataMember]
        public decimal? AMOUNT { get; set; }

        /// <summary>
        /// Material Code
        /// </summary> 
        [DataMember]
        public string MATERIAL_CODE { get; set; }
        [DataMember]
        public string COST_CENTER { get; set; }
        [DataMember]
        public string SAP_NO { get; set; }
        [DataMember]
        public string KRPIA { get; set; }

        /// <summary>
        /// 삭제여부
        /// </summary> 
        [DataMember]
        public string IS_DELETED { get; set; }

        /// <summary>
        /// 다운로드 경로
        /// </summary>
        [DataMember]
        public string DOWNLOAD_URL { get; set; }

        /// <summary>
        /// 첨부파일 인덱스
        /// </summary>
        [DataMember]
        public int? ATTACH_IDX { get; set; }

        /// <summary>
        /// 첨부파일 타입(AgendaRole)
        /// </summary>
        [DataMember]
        public string ATTACH_FILE_TYPE { get; set; }

        /// <summary>
        /// 첨부파일 표시명
        /// </summary>
        [DataMember]
        public string DISPLAY_FILE_NAME { get; set; }

        /// <summary>
        /// 첨부파일 저장명
        /// </summary>
        [DataMember]
        public string SAVED_FILE_NAME { get; set; }

        /// <summary>
        /// 첨부파일 사이즈
        /// </summary>
        [DataMember]
        public int? FILE_SIZE { get; set; }

        /// <summary>
        /// 첨부파일 핸들러 URL(삭제시 필요)
        /// </summary>
        [DataMember]
        public string FILE_HANDLER_URL { get; set; }

        /// <summary>
        /// 첨부파일 물리적 저장 경록
        /// </summary>
        [DataMember]
        public string FILE_PATH { get; set; }

        /// <summary>
        /// 생성자
        /// </summary> 
        [DataMember]
        public string CREATOR_ID { get; set; }

        /// <summary>
        /// 생성일자
        /// </summary> 
        [DataMember]
        public DateTime CREATE_DATE { get; set; }

        /// <summary>
        /// 수정자
        /// </summary> 
        [DataMember]
        public string UPDATER_ID { get; set; }

        /// <summary>
        /// 수정일자
        /// </summary> 
        [DataMember]
        public DateTime? UPDATE_DATE { get; set; }
    }
}
