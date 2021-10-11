using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Common
{
    [DataContract]
    public class DTO_COMMON_CODE_SUB
    {
        /// <summary>
        /// 분류코드
        /// </summary> 
        [DataMember]
        public string CLASS_CODE { get; set; }

        /// <summary>
        /// 세부코드
        /// </summary> 
        [DataMember]
        public string SUB_CODE { get; set; }

        /// <summary>
        /// 코드명
        /// </summary> 
        [DataMember]
        public string CODE_NAME { get; set; }

        /// <summary>
        /// 정렬순서
        /// </summary> 
        [DataMember]
        public int SORT_SEQ { get; set; }

        /// <summary>
        /// 사용여부
        /// </summary> 
        [DataMember]
        public string USABLE { get; set; }

        /// <summary>
        /// 생성자
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
