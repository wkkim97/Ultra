using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Medical
{
    public class DTO_MEDICAL_HCP_PAYMENT
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
        /// 일련번호
        /// </summary> 
        public int? IDX { get; set; }

        /// <summary>
        /// 비용지급일자
        /// </summary> 
        public string DATE { get; set; }

        /// <summary>
        /// 지급비용
        /// </summary> 
        public int AMOUNT { get; set; }

        /// <summary>
        /// 비용지급방법
        /// </summary> 
        public string METHOD_TYPE { get; set; }

        /// <summary>
        /// Evidence no
        /// </summary> 
        public string EVIDENCE_ID { get; set; }

        /// <summary>
        /// 비고
        /// </summary> 
        public string COMMENT { get; set; }

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

    public class DTO_MEDICAL_IMP
    {
        /// <summary>
        /// 메디컬 일련번호
        /// </summary> 
        public int MEDICAL_IDX { get; set; }

        /// <summary>
        /// hcp code
        /// </summary> 
        public string HCP_CODE { get; set; }

        /// <summary>
        /// 일련번호
        /// </summary> 
        public int? IDX { get; set; }

        /// <summary>
        /// 지급일자
        /// </summary> 
        public string DATE { get; set; }

        /// <summary>
        /// Category
        /// </summary> 
        public string CATEGORY { get; set; }

        /// <summary>
        /// order no
        /// </summary> 
        public string ORDER_NO { get; set; }

        /// <summary>
        /// Airbill no
        /// </summary> 
        public string AIRBILL_NO { get; set; }

        /// <summary>
        /// imp
        /// </summary> 
        public string IMP { get; set; }

        /// <summary>
        /// Dose
        /// </summary> 
        public string DOSE { get; set; }

        /// <summary>
        /// UNIT
        /// </summary> 
        public string UNIT { get; set; }

        /// <summary>
        /// QTY
        /// </summary> 
        public string QTY { get; set; }

        /// <summary>
        /// TYPE
        /// </summary> 
        public string TYPE { get; set; }

        /// <summary>
        /// Comment
        /// </summary> 
        public string COMMENT { get; set; }

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
