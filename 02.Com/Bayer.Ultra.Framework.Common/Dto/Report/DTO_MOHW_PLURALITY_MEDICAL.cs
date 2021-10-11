using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Report
{
    public class DTO_MOHW_PLURALITY_MEDICAL
    {
        /// <summary>
        /// 
        /// </summary> 
        public int MOHW_IDX { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string CONTRACT_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string PRODUCT_STANDARD_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string PRODUCT_STANDARD_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string HCO_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string HCO_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string HCP_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string HCP_CODE { get; set; }
        
        /// <summary>
        /// 
        /// </summary> 
        public string COST_CATEGORY { get; set; }
        
        /// <summary>
        /// 
        /// </summary> 
        public string COST_CATEGORY_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public decimal AMOUNT { get; set; }
        
        /// <summary>
        /// 
        /// </summary> 
        public string EVENT_TYPE { get; set; }
        
        /// <summary>
        /// 
        /// </summary> 
        public string EVENT_KEY { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string ADDRESS_OF_VENUE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string START_TIME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string END_TIME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string KRPIA_NUMBER { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string REQUESTER_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string REQUESTER_ORG { get; set; }
    }

    public class DTO_MOHW_PLURALITY_MEDICAL_REPORT
    {
        //연번에 event Key 추가
        //[DataMember]
        //public int ROW_NUM { get; set; }
        //[DataMember]
        public string ROW_NUM { get; set; }
        /// <summary>
        /// 
        /// </summary> 
        public int MOHW_IDX { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string CONTRACT_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string PRODUCT_STANDARD_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string PRODUCT_STANDARD_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string HCO_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string HCO_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string HCP_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string HCP_CODE { get; set; }
        
        /// <summary>
        /// 
        /// </summary> 
        public decimal? ACCOMMODATION_AMOUNT { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public decimal? GIMMICKSOUVENIR_AMOUNT { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public decimal? MEALBEVERAGE_AMOUNT { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public decimal? TRANSPORTATION_AMOUNT { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string EVENT_TYPE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string EVENT_KEY { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string ADDRESS_OF_VENUE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string DATE_TIME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string KRPIA_NUMBER { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string REQUESTER_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string REQUESTER_ORG { get; set; }
    }
}
