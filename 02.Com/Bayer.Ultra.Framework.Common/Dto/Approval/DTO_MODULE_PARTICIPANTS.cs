using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_MODULE_PARTICIPANTS
    {
        /// <summary>
        /// 이벤트 아이디
        /// </summary>
        [DataMember]
        public string EVENT_ID { get; set; }

        /// <summary>
        /// 프로세스 아이디
        /// </summary> 
        [DataMember]
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 참석자 일련번호
        /// </summary> 
        [DataMember]
        public int PARTICIPANT_IDX { get; set; }

        /// <summary>
        /// 타입
        /// </summary> 
        [DataMember]
        public string PARTICIPANT_TYPE { get; set; }

        /// <summary>
        /// 참석자 타입
        /// </summary> 
        [DataMember]
        public string HCP_TYPE { get; set; }

        /// <summary>
        /// 참석자 코드
        /// </summary> 
        [DataMember]
        public string HCP_CODE { get; set; }

        /// <summary>
        /// 참석자 명
        /// </summary> 
        [DataMember]
        public string HCP_NAME { get; set; }

        /// <summary>
        /// 병원코드
        /// </summary> 
        [DataMember]
        public string HCO_CODE { get; set; }

        /// <summary>
        /// 병원명
        /// </summary> 
        [DataMember]
        public string HCO_NAME { get; set; }

        /// <summary>
        /// 전공 코드
        /// </summary> 
        [DataMember]
        public string SPECIALTY_CODE { get; set; }

        /// <summary>
        /// 전공
        /// </summary> 
        [DataMember]
        public string SPECIALTY_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string HCO_SECTOR_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string HCO_SECTOR_NAME { get; set; }

        /// <summary>
        /// 국가코드
        /// </summary> 
        [DataMember]
        public string COUNTRY_CODE { get; set; }

        /// <summary>
        /// 국가명
        /// </summary> 
        [DataMember]
        public string COUNTRY_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string ROLE { get; set; }

        /// <summary>
        /// DATASOURCE
        /// </summary> 
        [DataMember]
        public string DATA_SOURCE { get; set; }

        /// <summary>
        /// 참석여부
        /// </summary> 
        [DataMember]
        public string IS_ATTENDED { get; set; }

        /// <summary>
        /// 삭제여부
        /// </summary> 
        [DataMember]
        public string IS_DELETED { get; set; }

        /// <summary>
        /// 방문 횟수
        /// </summary>
        [DataMember]
        public int VISIT_COUNT { get; set; }

        /// <summary>
        /// 상태 Y:참석 N:불참 D:삭제
        /// </summary>
        [DataMember]
        public string STATUS { get; set; }

        [DataMember]
        public string EVENT_DATE { get; set; }

        /// <summary>
        /// 의사 상태 1:Active 0:InActive 9:Delete 8:New
        /// </summary>
        [DataMember]
        public string HCP_STATUS { get; set; }

        /// <summary>
        /// Concur 반영여부
        /// </summary> 
        [DataMember]
        public string CONCUR_UPDATED { get; set; }

        /// <summary>
        /// Consur 반영시간
        /// </summary> 
        [DataMember]
        public DateTime? CONCUR_UPDATE_DATE { get; set; }

        /// <summary>
        /// 생성자
        /// </summary> 
        [DataMember]
        public string CREATOR_ID { get; set; }

        /// <summary>
        /// 생성자 명
        /// </summary>
        [DataMember]
        public string CREATOR_NAME { get; set; }

        /// <summary>
        /// 생성일자
        /// </summary> 
        [DataMember]
        public string CREATE_DATE { get; set; }

        /// <summary>
        /// 수정자
        /// </summary> 
        [DataMember]
        public string UPDATER_ID { get; set; }

        /// <summary>
        /// 수정일자
        /// </summary> 
        [DataMember]
        public string UPDATE_DATE { get; set; }


    }
}
