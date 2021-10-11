using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Medical
{
    public class DTO_MEDICAL_PMS
    {
        /// <summary>
        /// 키값
        /// </summary> 
        public int? IDX { get; set; }
        /// <summary>
        /// 생성자
        /// </summary> 
        public string CREATOR_ID { get; set; }

        /// <summary>
        /// PMS 의사코드
        /// </summary> 
        public string PMS_HCP_CODE { get; set; }

        /// <summary>
        /// PMS 의사
        /// </summary> 
        public string HCP_NAME { get; set; }

        /// <summary>
        /// 병원코드
        /// </summary> 
        public string HCO_CODE { get; set; }

        /// <summary>
        /// 병원명
        /// </summary> 
        public string HCO_NAME { get; set; }

        /// <summary>
        /// 재심사여부
        /// </summary> 
        public string REVIEW_YN { get; set; }

        /// <summary>
        /// 비지급일자
        /// </summary> 
        public string DATE { get; set; }

        /// <summary>
        /// 제품코드
        /// </summary> 
        public string PRODUCT_CODE { get; set; }

        /// <summary>
        /// 제품명
        /// </summary> 
        public string PRODUCT_NAME { get; set; }

        /// <summary>
        /// 단가
        /// </summary> 
        public int COST { get; set; }

        /// <summary>
        /// 건수
        /// </summary> 
        public int NUMBER { get; set; }

        /// <summary>
        /// 지급비용
        /// </summary> 
        public int AMOUNT { get; set; }

        /// <summary>
        /// 지급비용방법
        /// </summary> 
        public string METHOD_TYPE { get; set; }

        /// <summary>
        /// Contract No
        /// </summary> 
        public string CONTRACT_ID { get; set; }

        /// <summary>
        /// Evidence No
        /// </summary> 
        public string EVIDENCE_ID { get; set; }

        /// <summary>
        /// 체크여부
        /// </summary> 
        public string VALIDATEYN { get; set; }

        /// <summary>
        /// 비고
        /// </summary> 
        public string REMARK { get; set; }

        /// <summary>
        /// 삭제여부
        /// </summary> 
        public string IS_DELETED { get; set; }

        /// <summary>
        /// 생성일자
        /// </summary> 
        public DateTime? CREATE_DATE { get; set; }

        /// <summary>
        /// 수정자
        /// </summary> 
        public string UPDATER_ID { get; set; }

        /// <summary>
        /// 수정일자
        /// </summary> 
        public DateTime? UPDATE_DATE { get; set; }


    }
}
