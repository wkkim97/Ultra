using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_PAYMENT_UPLOAD_SRM_TOTAL
    {
        /// <summary>
        /// REQ Tracking
        /// </summary> 
        [DataMember]
        public string REQ_TRACK_NUMBER { get; set; }

        /// <summary>
        /// PO 넘버
        /// </summary> 
        [DataMember]
        public string PO_NUMBER { get; set; }

        /// <summary>
        /// 문서 번호
        /// </summary> 
        [DataMember]
        public string DOC_NUMBER { get; set; }

        /// <summary>
        /// 밴더
        /// </summary> 
        [DataMember]
        public string VENDOR_CODE { get; set; }

        /// <summary>
        /// 장소
        /// </summary> 
        [DataMember]
        public string PLACE { get; set; }

        /// <summary>
        /// 문서 일자
        /// </summary> 
        [DataMember]
        public DateTime? DOC_DATE { get; set; }

        /// <summary>
        /// 인보이스 일자
        /// </summary> 
        [DataMember]
        public DateTime? INV_POST_DATE { get; set; }

        /// <summary>
        /// 지출총액
        /// </summary> 
        [DataMember]
        public decimal? SPEND_TOTAL_AMOUNT { get; set; }

        /// <summary>
        /// 요청자
        /// </summary> 
        [DataMember]
        public string CREATOR_ID { get; set; }

        /// <summary>
        /// 수정자
        /// </summary> 
        [DataMember]
        public string UPDATER_ID { get; set; }

        /// <summary>
        /// 생성일자
        /// </summary> 
        [DataMember]
        public DateTime? CREATE_DATE { get; set; }

        /// <summary>
        /// 수정일자
        /// </summary> 
        [DataMember]
        public DateTime? UPDATE_DATE { get; set; }

    }
}
