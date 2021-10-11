using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Configuration
{
    [DataContract]
    public class DTO_WF_CONFIG_APPROVER
    {
        /// <summary>
        /// 이벤트 아이디
        /// </summary> 
        [DataMember]
        public string EVENT_ID { get; set; }

        /// <summary>
        /// 결재자 위치
        /// </summary> 
        [DataMember]
        public string APPROVER_LOCATION { get; set; }

        /// <summary>
        /// 조건 일련번호
        /// </summary> 
        [DataMember]
        public int CONDITION_IDX { get; set; }

        /// <summary>
        /// 필수여부
        /// </summary> 
        [DataMember]
        public string IS_MANDATORY { get; set; }

        /// <summary>
        /// 결재자
        /// </summary> 
        [DataMember]
        public string APPROVER_ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string APPROVER_NAME { get; set; }
        /// <summary>
        /// Display 조건
        /// </summary> 
        [DataMember]
        public string DISPLAY_CONDITION { get; set; }

        /// <summary>
        /// Sql 조건
        /// </summary> 
        [DataMember]
        public string SQL_CONDITION { get; set; }

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

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int MAX_CONDITION_IDX { get; set; }

    }
}
