using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Report
{
    public class DTO_MOHW_PARTICIPANTS
    {
        /// <summary>
        /// 
        /// </summary> 
        public int MOHW_IDX { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string EVENT_KEY { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string HOST { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string HOST_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string SUBJECT { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string VENUE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        //public DateTime? START_TIME { get; set; }
        public string START_TIME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        //public DateTime? END_TIME { get; set; }
        public string END_TIME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string COST_CATEGORY { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public decimal AMOUNT { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string CONGREES_TYPE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public int HCP_NO { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string KRPIA { get; set; }

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
