using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Configuration
{
    [DataContract]
    public class DTO_WF_CONFIG
    {
        /// <summary>
        /// 이벤트 아이디
        /// </summary> 
        [DataMember]
        public string EVENT_ID { get; set; }

        /// <summary>
        /// 테이블명
        /// </summary> 
        [DataMember]
        public string TABLE_NAME { get; set; }

        /// <summary>
        /// 이벤트명
        /// </summary> 
        [DataMember]
        public string EVENT_NAME { get; set; }

        /// <summary>
        /// Owner
        /// </summary> 
        [DataMember]
        public string DATA_OWNER { get; set; }

        /// <summary>
        /// Owner Name
        /// </summary>
        [DataMember]
        public string DATA_OWNER_NAME { get; set; }

        /// <summary>
        /// 번호 접두사
        /// </summary> 
        [DataMember]
        public string PREFIX_EVENT_KEY { get; set; }

        /// <summary>
        /// 웹페이지
        /// </summary> 
        [DataMember]
        public string WEB_PAGE_NAME { get; set; }

        /// <summary>
        /// 열람대상
        /// </summary> 
        [DataMember]
        public string READERS_GROUP_CODE { get; set; }

        /// <summary>
        /// 보관주기
        /// </summary> 
        [DataMember]
        public string RETENTION_PERIOD_CODE { get; set; }

        /// <summary>
        /// 보관주기
        /// </summary>
        [DataMember]
        public string RETENTION_PERIOD_TEXT { get; set; }

        /// <summary>
        /// 정보공개범위
        /// </summary> 
        [DataMember]
        public string CLASSIFICATION_INFO_CODE { get; set; }

        /// <summary>
        /// 완료 후 실행 서비스
        /// </summary> 
        [DataMember]
        public string AFTER_TREATMENT_SERVICE { get; set; }

        /// <summary>
        /// 설명
        /// </summary> 
        [DataMember]
        public string EVENT_DESC { get; set; }

        /// <summary>
        /// 전달 가능 여부
        /// </summary> 
        [DataMember]
        public string OPT_FORWARD { get; set; }

        /// <summary>
        /// Reviewer 추가 여부
        /// </summary> 
        [DataMember]
        public string OPT_ADD_REVIEWER { get; set; }

        /// <summary>
        /// Reviewer 추가시 설명
        /// </summary> 
        [DataMember]
        public string OPT_ADD_REVIEWER_DESC { get; set; }

        /// <summary>
        /// 메인 화면 표시 여부
        /// </summary> 
        [DataMember]
        public string OPT_SHOW_EVENT_LIST { get; set; }

        /// <summary>
        /// 경비 설정 여부
        /// </summary> 
        [DataMember]
        public string OPT_ADD_COST_PLAN { get; set; }

        /// <summary>
        /// 참석자 설정 여부
        /// </summary> 
        [DataMember]
        public string OPT_ADD_PARTICIPANTS { get; set; }

        /// <summary>
        /// Agenda  설정 여부
        /// </summary> 
        [DataMember]
        public string OPT_ADD_AGENDA { get; set; }

        /// <summary>
        /// 비용 설정 여부
        /// </summary> 
        [DataMember]
        public string OPT_ADD_PAYMENT { get; set; }

        /// <summary>
        /// 결재 생략 여부
        /// </summary> 
        [DataMember]
        public string OPT_SKIP_APPROVAL { get; set; }

        /// <summary>
        /// 결재 만
        /// </summary>
        [DataMember]
        public string OPT_ONLY_APPROVAL { get; set; }

        /// <summary>
        /// CountRule 체크 여부
        /// </summary> 
        [DataMember]
        public string OPT_COUNT_RULE { get; set; }

        /// <summary>
        /// 보건복지부 보고 여부
        /// </summary> 
        [DataMember]
        public string OPT_REPORT_MOHW { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string OPT_REPORT_KRPIA { get; set; }

        /// <summary>
        /// 역할 타입
        /// 강연 : Lecture 자문 : Consultation
        /// </summary>
        [DataMember]
        public string AGENDA_ROLE_TYPE { get; set; }

        /// <summary>
        /// 결재 타입
        /// </summary> 
        [DataMember]
        public string APPROVAL_TYPE_CODE { get; set; }

        /// <summary>
        /// 상위 결재자 Level
        /// </summary> 
        [DataMember]
        public int APPROVAL_LEVEL { get; set; }

        /// <summary>
        /// 상위 결재자 Job
        /// </summary> 
        [DataMember]
        public string APPROVAL_JOB_TITLE_CODE { get; set; }

        /// <summary>
        /// EventComplete시 Comment
        /// </summary>
        [DataMember]
        public string OPT_EVENT_COMPLETE_COMMENT { get; set; }

        /// <summary>
        /// EventComplete시 Comment 카페고리
        /// </summary>
        [DataMember]
        public string OPT_EVENT_COMPLETE_COMMENT_CATEGORY { get; set; }

        /// <summary>
        /// EventComplete시 첨부파일
        /// </summary>
        [DataMember]
        public string OPT_EVENT_COMPLETE_ATTACH { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        /// version 1.0.5 Event complete 시 briefing material(LMR) number check
        public string OPT_EVENT_CHECK_DETAIL_MATERIAL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]

        /// version 1.0.5 Consulting Event complete 시 medical compliance 문구 추가
        public string OPT_EVENT_CHECK_MEDICAL { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
      


        public string MY_EVENT_KEY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string MOHW_REPORT_TYPE { get; set; }

        /// <summary>
        /// 삭제여부
        /// </summary>
        [DataMember]
        public string IS_DELETED { get; set; }

        /// <summary>
        /// 참석자 방문 제한 횟수
        /// </summary>
        [DataMember]
        public int VISIT_LIMIT_COUNT { get; set; }

        /// <summary>
        /// 의사 금액 제한
        /// </summary>
        [DataMember]
        public decimal HCP_LIMIT_AMOUNT { get; set; }

        /// <summary>
        /// 생성자
        /// </summary> 
        [DataMember]
        public string CREATOR_ID { get; set; }

        /// <summary>
        /// 생성일자
        /// </summary> 
        [DataMember]
        public DateTime? CREATE_DATE { get; set; }

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
