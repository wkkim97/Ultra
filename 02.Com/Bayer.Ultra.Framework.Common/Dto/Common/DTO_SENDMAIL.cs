using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Common
{
    public class DTO_SENDMAIL
    {
        /// <summary>
        /// 
        /// </summary> 
        public int IDX { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string SEND_MAIL_TYPE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string MAILADDRESS { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string SEND_STATUS { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public DateTime? SEND_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public int RETRY_CNT { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string REMARK { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public DateTime? CREATE_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public DateTime? UPDATE_DATE { get; set; }


    }

    public class DTO_SENDMAIL_VIOLATION
    {
        /// <summary>
        /// 
        /// </summary> 
        public int IDX { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string HCP_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string SUBJECT { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string TYPE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string STATUS { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public string HCP_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public DateTime? CREATE_DATE { get; set; }


    }


    public class DTO_SENDMAIL_INTERFACE
    {

        public int IDX { get; set; }

        public string INTERFACE_DATE { get; set; }

        public string WRITE_TIME { get; set; }

        public string LOG_TYPE { get; set; }

        public string MESSAGE { get; set; }

        public string FETCH_SEQ { get; set; }

        public string ACTIVITY_ID { get; set; }

        public string USER_ID { get; set; }

        public string HCO_NAME { get; set; }

        public string HCP_NAME { get; set; }

        public string HCP_CODE { get; set; }

        public string ACTIVITY_TYPE { get; set; }

        public string EVENT_STATUS { get; set; }


    }


}
