using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Report
{
    public class ReceiptForFreeGoodDto
    {
        public string PROCESS_ID    { get; set;}
	    public int IDX           { get; set;} 
        public string EVENT_ID { get; set; }
	    public string EVENT_KEY     { get; set;}
	    public string REQUESTER_ID  { get; set;}

        public string REQUESTER_NAME { get; set; }

        public string REQUEST_DATE  { get; set;}
	    public string HCP_CODE      { get; set;}
	    public string HCP_NAME      { get; set;}
	    public string HCO_CODE      { get; set;}
	    public string HCO_NAME      { get; set;}
	    public string PRODUCT_CODE  { get; set;}
	    public string PRODUCT_NAME  { get; set;}
	    public int QTY           { get; set;}

        public string PURPOSE { get; set; }
         //<!-- Ver 1.0.7 : Go-Direct -->
        public string RECEIPT_CATEGORY { get; set; }
        
        public string RETURN_COMMENT { get; set; }
        public string RECEIPT_DATE  { get; set;}

        public string RETURN_RECEIPT_DATE { get; set; }
        public string RECEIPT_TYPE  { get; set;}

        public string SIGN_IMG_URL { get; set; }

        public string FILE_URL { get; set; }
        public string RETURN_FILE_URL { get; set; }

        public string RECEIPT_FILENAME { get; set; }
        public string RETURN_RECEIPT_FILENAME { get; set; }

        public string STATUS { get; set; }

        public string EVENT_FILE_IDX { get; set; }
        public string RETURN_EVENT_FILE_IDX { get; set; }

        public string RETURN_DATE { get; set; }

        public string SAP_ORDER { get; set; }

        public string BU { get; set; }

        public string STD_CODE { get; set; }
        public string LAST_APPROVER { get; set; }
        public string LOG { get; set; }
    }
}
