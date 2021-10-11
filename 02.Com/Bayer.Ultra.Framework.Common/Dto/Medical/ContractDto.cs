using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Medical
{

    public class ContractDto
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
        /// 공동연구가 인원
        /// </summary>
        public   int? HCR_CNT { get; set; }

        public string REMARK { get; set; }
    }


    public class SearchHcpDto
    { 
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

    }
}
