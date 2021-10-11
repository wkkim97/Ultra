using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_PAYMENT_UPLOAD_CONCUR
    {
        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public int IDX { get; set; }

        /// <summary>
        /// 회사코드
        /// </summary> 
        [DataMember]
        public string COMPANY_CODE { get; set; }

        /// <summary>
        /// 보고 ID
        /// </summary> 
        [DataMember]
        public string REPORT_ID { get; set; }

        /// <summary>
        /// 사원 ID
        /// </summary> 
        [DataMember]
        public string EMPLOYEE_ID { get; set; }

        /// <summary>
        /// 사원명
        /// </summary> 
        [DataMember]
        public string EMPLOYEE { get; set; }

        /// <summary>
        /// 보고명
        /// </summary> 
        [DataMember]
        public string REPORT_NAME { get; set; }

        /// <summary>
        /// 거래 ID
        /// </summary> 
        [DataMember]
        public string TRANSACTION_ID { get; set; }

        /// <summary>
        /// 거래 일자
        /// </summary> 
        [DataMember]
        public string TRANSACTION_DATE { get; set; }

        /// <summary>
        /// 지출 타
        /// </summary> 
        [DataMember]
        public string EXPENSE_TYPE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string HCP_EXPENSE_TYPE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string MATERIAL_CODE { get; set; }

        /// <summary>
        /// Event Key
        /// </summary> 
        [DataMember]
        public string CONSULTATION_NUMBER { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string HCP_TYPE_IDENTIFICATION { get; set; }

        /// <summary>
        /// 영업목적
        /// </summary> 
        [DataMember]
        public string BUSINESS_PURPOSE { get; set; }

        /// <summary>
        /// 지출 총금액
        /// </summary> 
        [DataMember]
        public decimal? EXPENSE_AMOUNT { get; set; }

        /// <summary>
        /// 통화
        /// </summary> 
        [DataMember]
        public string REIMBURSEMENT_CURRENCY { get; set; }

        /// <summary>
        /// 참석자별 금액
        /// </summary> 
        [DataMember]
        public decimal? ATTENDEE_AMOUNT { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string EVENT_KEY { get; set; }

        /// <summary>
        /// 참석 코드
        /// </summary> 
        [DataMember]
        public string LOCAL_CODE { get; set; }

        /// <summary>
        /// HCP 코드
        /// </summary>
        [DataMember]
        public string HCP_CODE { get; set; }

        /// <summary>
        /// HCO 코드
        /// </summary>
        [DataMember]
        public string HCO_CODE { get; set; }

        /// <summary>
        /// CRM 이벤트 키
        /// </summary>
        [DataMember]
        public string CRM_EVENT_KEY { get; set; }

        /// <summary>
        /// 참석자 명
        /// </summary> 
        [DataMember]
        public string ATTENDEE_NAME { get; set; }

        /// <summary>
        /// 참석자명_이름
        /// </summary> 
        [DataMember]
        public string ATTENDEE_FIRST_NAME { get; set; }

        /// <summary>
        /// 참석자명_성
        /// </summary> 
        [DataMember]
        public string ATTENDEE_LAST_NAME { get; set; }

        /// <summary>
        /// 제목
        /// </summary> 
        [DataMember]
        public string TITLE { get; set; }

        /// <summary>
        /// 회사
        /// </summary> 
        [DataMember]
        public string COMPANY { get; set; }

        /// <summary>
        /// 출처
        /// </summary> 
        [DataMember]
        public string AFFILIATION { get; set; }

        /// <summary>
        /// 참석 형태
        /// </summary> 
        [DataMember]
        public string ATTENDEE_TYPE { get; set; }

        /// <summary>
        /// IPIN
        /// </summary> 
        [DataMember]
        public string EXTERNAL_ID { get; set; }

        /// <summary>
        /// 판매처
        /// </summary> 
        [DataMember]
        public string VENDOR { get; set; }

        /// <summary>
        /// 도시
        /// </summary> 
        [DataMember]
        public string LOCATION { get; set; }

        /// <summary>
        /// 지역
        /// </summary> 
        [DataMember]
        public string REGION { get; set; }

        /// <summary>
        /// 국가
        /// </summary> 
        [DataMember]
        public string COUNTRY { get; set; }

        /// <summary>
        /// 배분 코
        /// </summary> 
        [DataMember]
        public string ALLOCATION_CODE { get; set; }

        /// <summary>
        /// 배분 넘버
        /// </summary> 
        [DataMember]
        public string ALLOCATION_NUMBER { get; set; }

        /// <summary>
        /// 배분 금액
        /// </summary> 
        [DataMember]
        public decimal? ALLOCATION_AMOUNT { get; set; }

        /// <summary>
        /// 지불방법
        /// </summary> 
        [DataMember]
        public string PAYMENT_TYPE { get; set; }

        /// <summary>
        /// 영수증 타입
        /// </summary> 
        [DataMember]
        public string RECEIPT_TYPE { get; set; }

        /// <summary>
        /// 정책
        /// </summary> 
        [DataMember]
        public string POLICY { get; set; }

        /// <summary>
        /// 코멘트
        /// </summary> 
        [DataMember]
        public string COMMENTS { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string STATUS { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string ERROR_MESSAGE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string IS_DELETED { get; set; }

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
