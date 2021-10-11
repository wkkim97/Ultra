using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_MODULE_AGENDA
    {
        /// <summary>
        /// 이벤트 아이디
        /// </summary>
        [DataMember]
        public string EVENT_ID { get; set; }

        /// <summary>
        /// 프로세스 아이디
        /// </summary> 
        [DataMember]
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 아젠다 일련번호
        /// </summary> 
        [DataMember]
        public int AGENDA_IDX { get; set; }

        /// <summary>
        /// 날자
        /// </summary> 
        [DataMember]
        public int DAY { get; set; }

        /// <summary>
        /// 시작 시간
        /// </summary> 
        [DataMember]
        public string START_TIME { get; set; }

        /// <summary>
        /// 진행 시간
        /// </summary> 
        [DataMember]
        public int DURATION { get; set; }

        /// <summary>
        /// 주제
        /// </summary> 
        [DataMember]
        public string SUBJECT { get; set; }

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
