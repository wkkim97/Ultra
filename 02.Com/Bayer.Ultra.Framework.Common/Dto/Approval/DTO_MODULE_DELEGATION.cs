using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_MODULE_DELEGATION
    {
        /// <summary>
        /// 프로세스 아이디
        /// </summary> 
        [DataMember]
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 사용자 아이디
        /// </summary> 
        [DataMember]
        public string USER_ID { get; set; }

        /// <summary>
        /// 사용자명
        /// </summary>
        [DataMember]
        public string USER_NAME { get; set; }

        /// <summary>
        /// 사용자 소속
        /// </summary>
        [DataMember]
        public string ORGANIZATION { get; set; }

        /// <summary>
        /// 역할
        /// </summary> 
        [DataMember]
        public string ROLE { get; set; }

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
        public string CREATE_DATE { get; set; }

        /// <summary>
        /// 수정자
        /// </summary> 
        [DataMember]
        public string UPDATER_ID { get; set; }

        /// <summary>
        /// 수정일자
        /// </summary> 
        [DataMember]
        public string UPDATE_DATE { get; set; }


    }
}
