using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Configuration
{
    [DataContract]
    public class DTO_WF_CONFIG_COST_CATEGORY
    {
        /// <summary>
        /// 이벤트 아이디
        /// </summary> 
        [DataMember]
        public string EVENT_ID { get; set; }

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
        /// 생성자
        /// </summary> 
        [DataMember]
        public string CREATOR_ID { get; set; }

        /// <summary>
        /// 생성일자
        /// </summary> 
        [DataMember]
        public DateTime? CREATE_DATE { get; set; }
    }
}
