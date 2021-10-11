using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_PAYMENT_UPLOAD_YOURDOCES
    {

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public int IDX { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string ACCOUNT { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string NAME_1 { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string DOCUMENT_NUMBER { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string DOCUMENT_TYPE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string PAYMENT_BLOCK { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string DOCUMENT_HEADER_TEXT { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string DOCUMENT_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string ENTRY_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string POSTING_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string NET_DUE_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public decimal? AMOUNT_IN_DOC_CURR { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string DOCUMENT_CURRENCY { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public decimal? AMOUNT_IN_LOCAL_CURRENCY { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string LOCAL_CURRENCY { get; set; }

        public decimal? WITHHOLDING_TAX_AMOUNT { get; set; }
        public decimal? WITHHOLDING_TAX_BASE_AMOUNT { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string TEXT { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string USER_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string CLEARING_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string CLEARING_DOCUMENT { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string REFERENCE_KEY_1 { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string REFERENCE_KEY_2 { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string REFERENCE_KEY_3 { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string HCO_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string COMMENTS { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string STATUS { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string ERROR_MESSAGE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string IS_DELETED { get; set; }

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
