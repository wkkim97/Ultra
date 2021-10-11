using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Medical
{

    public class ContractHCRDto
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
        /// 공동연구코드
        /// </summary> 
        public string HCR_CODE { get; set; }

        /// <summary>
        /// 공동연구원
        /// </summary> 
        public string HCR_NAME { get; set; }

        /// <summary>
        /// 병원코드
        /// </summary> 
        public string HCO_CODE { get; set; }

        /// <summary>
        /// 병원명
        /// </summary> 
        public string HCO_NAME { get; set; }

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
        public string CREATE_DATE { get; set; }

        /// <summary>
        /// 수정자
        /// </summary> 
        public string UPDATER_ID { get; set; }

        /// <summary>
        /// 수정일자
        /// </summary> 
        public string UPDATE_DATE { get; set; }
    }
}
