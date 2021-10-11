using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Medical
{
    public class DTO_MEDICAL_PRODUCTS
    {
        /// <summary>
        /// 메디컬 일련번호
        /// </summary> 
        public int MEDICAL_IDX { get; set; }

        /// <summary>
        /// 제품코드
        /// </summary> 
        public string PRODUCT_CODE { get; set; }

        /// <summary>
        /// 제품명
        /// </summary> 
        public string PRODUCT_NAME { get; set; }

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
