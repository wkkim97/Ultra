using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_EVENT_DONATION
	{
        /// <summary>
        /// 프로세스 아이디
        /// </summary> 
        [DataMember]
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 제목
        /// </summary> 
        [DataMember]
        public string SUBJECT { get; set; }

        /// <summary>
        /// 이벤트 키
        /// </summary> 
        [DataMember]
        public string EVENT_KEY { get; set; }

        /// <summary>
        /// 프로세스 상태
        /// </summary> 
        [DataMember]
        public string PROCESS_STATUS { get; set; }

        /// <summary>
        /// 기안자 아이디
        /// </summary> 
        [DataMember]
        public string REQUESTER_ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string REQUESTER_NAME { get; set; }


        /// <summary>
        /// 기안일자
        /// </summary> 
        [DataMember]
        public string REQUEST_DATE { get; set; }

        /// <summary>
        /// 회사코드
        /// </summary> 
        [DataMember]
        public string COMPANY_CODE { get; set; }

        /// <summary>
        /// 조직명
        /// </summary> 
        [DataMember]
        public string ORGANIZATION_NAME { get; set; }

        /// <summary>
        /// 보존기한
        /// </summary> 
        [DataMember]
        public string LIFE_CYCLE { get; set; }

        /// <summary>
        /// 시작시간
        /// </summary> 
        [DataMember]
        public string START_TIME { get; set; }
		
		/// <summary>
		/// 타입
		/// </summary>
		[DataMember]
		public string TYPE{get;set;}
		
		/// <summary>
		/// 기부금액
		/// </summary>
		[DataMember]
		public string VALUE{get;set;}

		/// <summary>
		/// 목적
		/// </summary>
		[DataMember]
		public string PURPOSE{get;set;}

		/// <summary>
		/// 설명
		/// </summary>
		[DataMember]
		public string EXPLANATION{get;set;}

		/// <summary>
		/// 기부단체
		/// </summary>
		[DataMember]
		public string RECIPIENT{get;set;}

		/// <summary>
		/// 주소
		/// </summary>
		[DataMember]
		public string ADDRESS{get;set;}

		/// <summary>
		/// 전화번호
		/// </summary>
		[DataMember]
		public string TEL{get;set;}

		/// <summary>
		/// 이메일주소
		/// </summary>
		[DataMember]
		public string EMAIL { get; set; }

		/// <summary>
		/// 기부단체의 적격성 여부
		/// </summary>
		[DataMember]
		public string IS_ELIGIBILITY{get;set;}

		/// <summary>
		/// 기부 이후 영수증 타입
		/// </summary>
		[DataMember]
		public string AFTER_RECEIPT{get;set;}

		/// <summary>
		/// 설명
		/// </summary>
		[DataMember]
		public string COMMENT{get;set;}

		/// <summary>
		/// 분류
		/// </summary>
		[DataMember]
		public string CATEGORY{get;set;}

		/// <summary>
		/// 사용여부
		/// </summary> 
		[DataMember]
        public string IS_DISUSED { get; set; }

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
