using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_EVENT_SCIENTIFIC_MEETING_PRODUCT
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
        /// 
        /// </summary>
        [DataMember]
        public string PRODUCT_TYPE { get; set; }

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
