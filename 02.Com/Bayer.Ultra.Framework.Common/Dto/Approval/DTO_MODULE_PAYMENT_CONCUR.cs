using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_MODULE_PAYMENT_CONCUR
    {
        /// <summary>
        /// 프로세스 아이디
        /// </summary> 
        [DataMember]
        public string PROCESS_ID { get; set; }
        
        /// <summary>
        /// 프로세스 타입
        /// </summary> 
        [DataMember]
        public string PROCESS_STATUS { get; set; }

        /// <summary>
        /// 이벤트 키
        /// </summary> 
        [DataMember]
        public string EVENT_KEY { get; set; }

        /// <summary>
        /// 이벤트 제목
        /// </summary>
        [DataMember]
        public string SUBJECT { get; set; }

        /// <summary>
        /// 이벤트 시작 시간
        /// </summary>
        [DataMember]
        public string START_DATE { get; set; }

        /// <summary>
        /// CONCUR TRANSACTION ID
        /// </summary>
        [DataMember]
        public string CONCUR_TRANSACTION_ID { get; set; }

        /// <summary>
        /// 데이터 소스
        /// </summary>
        [DataMember]
        public string DATA_SOURCE { get; set; }

        /// <summary>
        /// 카테고리 코드
        /// </summary>
        [DataMember]
        public string CATEGORY_CODE { get; set; }

        /// <summary>
        /// 카테고리 명
        /// </summary>
        [DataMember]
        public string CODE_NAME { get; set; }
        
        /// <summary>
        /// Consur 반영시간
        /// </summary> 
        [DataMember]
        public string UPDATER { get; set; }

        [DataMember]
        public string UPDATER_NAME { get; set; }

        /// <summary>
        /// Consur 반영시간
        /// </summary> 
        [DataMember]
        public string UPDATE_DATE { get; set; }
        
    }
}
