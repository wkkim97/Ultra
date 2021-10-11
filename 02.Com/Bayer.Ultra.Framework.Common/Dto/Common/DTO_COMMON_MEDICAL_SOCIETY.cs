using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Common
{
    [DataContract]
    public class DTO_COMMON_MEDICAL_SOCIETY
	{
        /// <summary>
        /// 학회 ID
        /// </summary> 
        [DataMember]
        public int SOCIETY_IDX { get; set; }

        /// <summary>
        /// 학회 명
        /// </summary> 
        [DataMember]
        public string SOCIETY_NAME { get; set; }

        /// <summary>
        /// 상태값 (Y, N)
        /// </summary> 
        [DataMember]
        public string STATUS { get; set; }

        /// <summary>
        /// 삭제여부 (Y, N)
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
