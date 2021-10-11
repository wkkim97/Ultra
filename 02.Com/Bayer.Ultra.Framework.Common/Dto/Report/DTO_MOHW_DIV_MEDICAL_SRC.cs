using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Report
{
    [DataContract]
    public class DTO_MOHW_DIV_MEDICAL_SRC
    {
        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public int MOHW_IDX { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string EVENT_KEY { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string PRODUCT_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string PRODUCT_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string ATTENDEE_STATUS { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string HCO_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string HCO_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string HCO_HIRA_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string HCP_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string HCP_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string COST_CATEGORY { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string COST_CATEGORY_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public decimal? AMOUNT { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string AGENDA_ROLE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string ADDRESS_OF_VENUE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string EVENT_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string EVENT_TYPE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string REQUESTER_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string REQUESTER_ORG { get; set; }


    }
}
