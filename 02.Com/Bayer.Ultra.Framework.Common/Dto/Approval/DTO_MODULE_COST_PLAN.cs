using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_MODULE_COST_PLAN
    {
        /// <summary>
        /// 이벤트 아디디
        /// </summary>
        [DataMember]
        public string EVENT_ID { get; set; }

        /// <summary>
        /// 프로세스 아이디
        /// </summary> 
        [DataMember]
        public string PROCESS_ID { get; set; }

        /// <summary>
        ///  비용계획 일련번호
        /// </summary> 
        [DataMember]
        public int COST_PLAN_IDX { get; set; }

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
        /// 수량
        /// </summary> 
        [DataMember]
        public int QTY { get; set; }

        /// <summary>
        /// 단가
        /// </summary> 
        [DataMember]
        public decimal PRICE { get; set; }

        /// <summary>
        /// 비용
        /// </summary>
        [DataMember]
        public decimal AMOUNT { get; set; }

        /// <summary>
        /// 설명
        /// </summary> 
        [DataMember]
        public string DESC { get; set; }

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
