using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_EVENT_DONATION_PRODUCT
    {
        /// <summary>
        /// 프로세스 아이디
        /// </summary> 
        [DataMember]
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 제품 일련번호
        /// </summary> 
        [DataMember]
        public int PRODUCT_IDX { get; set; }

        /// <summary>
        /// 제품 분류
        /// </summary>
        [DataMember]
        public string BU { get; set; }

        /// <summary>
        /// 제품 코드
        /// </summary> 
        [DataMember]
        public string PRODUCT_CODE { get; set; }

        /// <summary>
        /// 제품명
        /// </summary> 
        [DataMember]
        public string PRODUCT_NAME { get; set; }

        /// <summary>
        /// 제품 가격
        /// </summary>
        [DataMember]
        public decimal BASE_PRICE { get; set; }

        /// <summary>
        /// 제품 갯수
        /// </summary>
        [DataMember]
        public int QTY { get; set; }

        /// <summary>
        /// 총 가격
        /// </summary>
        [DataMember]
        public decimal AMOUNT { get; set; }

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
