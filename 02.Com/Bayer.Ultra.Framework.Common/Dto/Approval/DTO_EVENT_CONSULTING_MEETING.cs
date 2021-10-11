using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_EVENT_CONSULTING_MEETING
	{
        /// <summary>
        /// 프로세스 아이디
        /// </summary> 
        [DataMember]
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string SUBJECT { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string EVENT_KEY { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string PROCESS_STATUS { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string REQUESTER_ID { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string REQUESTER_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string REQUEST_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string COMPANY_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string ORGANIZATION_NAME { get; set; }

        /// <summary>
        /// 보존기한 코드
        /// </summary> 
        [DataMember]
        public string LIFE_CYCLE { get; set; }

        /// <summary>
        /// 보존기한
        /// </summary>
        [DataMember]
        public string RETENTION_PERIOD { get; set; }

        /// <summary>
        /// version 1.0.6 ABM 문서에 Category  추가
        /// </summary> 
        [DataMember]
        public string CATEGORY { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string START_TIME { get; set; }

		/// <summary>
		/// 
		/// </summary> 
		[DataMember]
		public string END_TIME { get; set; }

		/// <summary>
		/// 
		/// </summary> 
		[DataMember]
        public string ADDRESS_OF_VENUE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string VENUE_SELECTION_REASON { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string VENUE_SELECTION_REASON_MANUAL { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string PURPOSE_OBJECTIVE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string COST_PLAN { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string COMPLIANCE_1 { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string COMPLIANCE_2 { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string COMPLIANCE_REASON_1 { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string COMPLIANCE_REASON_2 { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string IS_DISUSED { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string CREATOR_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public DateTime CREATE_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string UPDATER_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public DateTime? UPDATE_DATE { get; set; }


    }
}
