using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Common
{ 
    #region DTO_SENDMAIL_TO_ADDRESS_LIST
    public class SendmailToAddressListDto
    {
        public string PROCESS_ID { get; set; }
        public string EVENT_ID { get; set; }
        public string EVENT_NAME { get; set; }
        public string APPROVER_ID { get; set; }
        public string APPROVER_NAME { get; set; }
        public string MAIL_ADDRESS { get; set; }
        public string WEB_PAGE_NAME { get; set; }
        public string COMMENT { get; set; }
        public string APPROVAL_TYPE { get; set; }
        public string REJECTED_PROCESS_ID { get; set; }
        public string SENDER_ID { get; set; }
        public string SENDER_NAME { get; set; }
        public string SENDER_MAIL_ADDRESS { get; set; }
        public DateTime REQUEST_DATE { get; set; }

        public string REQUEST_NAME { get; set; }

        public string SUBJECT { get; set; }
        public string EVENT_KEY { get; set; }
        public string MAIL_TYPE { get; set; }
    }
    #endregion
}
