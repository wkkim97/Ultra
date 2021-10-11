using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Report
{
    public class AdminReportDto
    {
        public string EVENT_ID { get; set; }
        public string PROCESS_ID { get; set; }
        public string PROCESS_STATUS { get; set; }
        public string EVENT_KEY { get; set; }
        public string REQUESTER { get; set; }
        public string ORGANIZATION { get; set; }
        public string EVENT_NAME { get; set; }
        public string SUBJECT { get; set; }
        public int INTERNAL { get; set; }
        public int EXTERANL { get; set; }
        public decimal PLAN_TOTAL { get; set; }
        public decimal ACTUAL_TOTAL { get; set; }
        public decimal AMOUNT_GAP { get; set; }
        public string RATIO { get; set; }
        public string EVENT_SEGMENTATION { get; set; }

    }
    public class AdminReport_AmountGapDto
    {
        public string EVENT_NAME { get; set; }
        public string EVENT_KEY { get; set; }
        public decimal AMOUNT_PLAN { get; set; }
        public decimal AMOUNT_ACTUAL { get; set; }
        public decimal AMOUNT_GAP { get; set; }
        public decimal RATIO { get; set; }


    }
    public class AdminReport_actionplan
    {
        public string EVENT_ID { get; set; }
        public string PROCESS_ID { get; set; }
        public string EVENT_KEY { get; set; }
        public string CATEGORY_NAME { get; set; }
        public decimal AMOUNT_PLAN { get; set; }
        public decimal AMOUNT_ACTUAL { get; set; }
        public decimal YOUR_DOCS_AMT { get; set; }
        public decimal SRM_AMT { get; set; }
        public decimal CUNCUR_AMT { get; set; }
        public decimal AMOUNT_GAP { get; set; }
        public string RATIO_NEW { get; set; }
        public string COST_WARNING { get; set; }



    }
}
