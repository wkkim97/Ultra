using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Configuration
{
    [DataContract]
    public class DTO_WF_CONFIG_RECIPIENT_CONDITION
    {
        /// <summary>
        /// 이벤트 아이디
        /// </summary> 
        [DataMember]
        public string EVENT_ID { get; set; }

        /// <summary>
        /// 조건 아이디
        /// </summary> 
        [DataMember]
        public int CONDITION_IDX { get; set; }

        /// <summary>
        /// 조건 일련번호
        /// </summary> 
        [DataMember]
        public int CONDITION_SEQ { get; set; }

        /// <summary>
        /// 필드명
        /// </summary> 
        [DataMember]
        public string FIELD_NAME { get; set; }

        /// <summary>
        /// 필드명
        /// </summary> 
        [DataMember]
        public string FIELD_TYPE { get; set; }

        /// <summary>
        /// 조건
        /// </summary> 
        [DataMember]
        public string CONDITION { get; set; }

        /// <summary>
        /// 값
        /// </summary> 
        [DataMember]
        public string VALUE { get; set; }

        /// <summary>
        /// 옵셩
        /// </summary> 
        [DataMember]
        public string OPTION { get; set; }

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
