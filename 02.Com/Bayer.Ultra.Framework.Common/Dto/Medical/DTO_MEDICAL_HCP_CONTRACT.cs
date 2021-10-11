using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Medical
{
    public class DTO_MEDICAL_HCP_CONTRACT
    {
        /// <summary>
        /// 메디컬 일련번호
        /// </summary> 
        public int MEDICAL_IDX { get; set; }

        /// <summary>
        /// 의사코드
        /// </summary> 
        public string HCP_CODE { get; set; }

        /// <summary>
        /// 의사명
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
         
        public string SPECIALTY_CODE { get; set; }

        public string SPECIALTY_NAME { get; set; }

        /// <summary>
        /// 계약상태
        /// </summary> 
        public string CONTRACT_STATUS { get; set; }

        /// <summary>
        /// 최초계약체결일
        /// </summary> 
        public string CONTRACT_DATE { get; set; }

        /// <summary>
        /// 계약만료일
        /// </summary> 
        public string CONTRACT_CHANGE_DATE { get; set; }

        /// <summary>
        /// 계약번호
        /// </summary> 
        public string CONTRACT_NO { get; set; }

        /// <summary>
        /// 연구비
        /// </summary> 
        public decimal? RESEARCH_FUND { get; set; }

        /// <summary>
        /// 역할
        /// </summary> 
        public string ROLE { get; set; }

        /// <summary>
        /// 비고
        /// </summary> 
        public string REMARK { get; set; }

        /// <summary>
        /// 삭제여부
        /// </summary> 
        public string IS_DELETED { get; set; }

        /// <summary>
        /// 생성자
        /// </summary> 
        public string CREATOR_ID { get; set; }

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
