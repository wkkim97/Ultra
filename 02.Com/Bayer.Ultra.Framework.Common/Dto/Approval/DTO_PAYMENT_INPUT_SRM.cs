using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_PAYMENT_INPUT_SRM
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
        /// 카테고리 코드
        /// </summary>
        [DataMember]
        public string CATEGORY_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string CATEGORY_NAME { get; set; }

        /// <summary>
        /// 영수일자
        /// </summary> 
        [DataMember]
        public string POST_DATE { get; set; }

        /// <summary>
        /// 금액
        /// </summary> 
        [DataMember]
        public decimal AMOUNT { get; set; }

        /// <summary>
        /// 코맨트
        /// </summary> 
        [DataMember]
        public string COMMENT { get; set; }

        /// <summary>
        /// 삭제여부
        /// </summary> 
        [DataMember]
        public string IS_DELETED { get; set; }

        /// <summary>
        /// 생정자
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
