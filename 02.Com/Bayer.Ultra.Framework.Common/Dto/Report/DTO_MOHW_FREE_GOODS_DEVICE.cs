using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Report
{
    public class DTO_MOHW_FREE_GOODS_DEVICE
    {
        /// <summary>
        /// 
        /// </summary> 
        public int? NO { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public int? MOHW_IDX { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string EVENT_KEY { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string PRODUCT_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string PRODUCT_CODE { get; set; }

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
        public int PRODUCT_QTY { get; set; }

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
        public string HCO_HIRA_CODE { get; set; }

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
        public int QTY { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public decimal? TOTAL_QTY { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string DELIVERY_DATE { get; set; }
        public string RETURN_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string PURPOSE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string REQUESTER { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string REQUESTER_ORG { get; set; }


    }

    public class DTO_MOHW_FREE_GOODS_DEVICE_REPORT
    {  
        public string EVENT_KEY { get; set; } 
        public string HCO_NAME { get; set; }
        public string HCO_HIRA_CODE { get; set; }
        public string PRODUCT_STANDARD_NAME { get; set; }
        public string PRODUCT_STANDARD_CODE { get; set; }
        public int? PRODUCT_QTY { get; set; }
        public int? QTY { get; set; }
        public decimal? TOTAL_QTY { get; set; }
        public string DELIVERY_DATE { get; set; }
        public string RETURN_DATE { get; set; }
        public string PURPOSE { get; set; }
        public string REQUESTER { get; set; }
        public string REQUESTER_ORG { get; set; }

    }
}
