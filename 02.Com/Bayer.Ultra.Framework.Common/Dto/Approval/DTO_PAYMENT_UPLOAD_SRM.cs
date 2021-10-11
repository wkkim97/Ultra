using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_PAYMENT_UPLOAD_SRM
    {
        /// <summary>
        /// 프로세스 아이디
        /// </summary> 
        [DataMember]
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 일련번호
        /// </summary> 
        [DataMember]
        public int SRM_IDX { get; set; }

        /// <summary>
        /// P/O Number
        /// </summary> 
        [DataMember]
        public string PO_NUMBER { get; set; }

        /// <summary>
        /// 참석자 타입(Total/KoreaHCP/Employee)
        /// </summary>
        [DataMember]
        public string PARTICIPANT_TYPE { get; set; }

        /// <summary>
        /// 카테고리 코드
        /// </summary> 
        [DataMember]
        public string CATEGORY_CODE { get; set; }

        /// <summary>
        /// 카테고리 명
        /// </summary>
        [DataMember]
        public string CATEGORY_NAME { get; set; }


        /// <summary>
        /// 금액
        /// </summary> 
        [DataMember]
        public decimal AMOUNT { get; set; }


        /// <summary>
        /// HCP 코드
        /// </summary> 
        [DataMember]
        public string HCP_CODE { get; set; }

        /// <summary>
        /// HCP 명
        /// </summary>
        [DataMember]
        public string HCP_NAME { get; set; }

        /// <summary>
        /// 병원 코드
        /// </summary>
        [DataMember]
        public string HCO_CODE { get; set; }

        /// <summary>
        /// 병원 명
        /// </summary>
        [DataMember]
        public string HCO_NAME { get; set; }

        /// <summary>
        /// 참석여부
        /// </summary>
        [DataMember]
        public string IS_ATTENDED { get; set; }

        /// <summary>
        /// 역할
        /// </summary>
        [DataMember]
        public string ROLE { get; set; }

        /// <summary>
        /// 코맨트
        /// </summary>
        [DataMember]
        public string COMMENT { get; set; }

        [DataMember]
        public string HISTORY_COMMENT { get; set; }

        /// <summary>
        /// 오류 메시지
        /// </summary>
        [DataMember]
        public string ERROR_MESSAGE { get; set; }

        /// <summary>
        /// 업로드 상태
        /// </summary>
        [DataMember]
        public string STATUS { get; set; }
        /// <summary>
        /// 삭제여부
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
