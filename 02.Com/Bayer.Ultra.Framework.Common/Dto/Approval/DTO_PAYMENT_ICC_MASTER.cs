using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_PAYMENT_ICC_MASTER
    {
        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public int ICC_ID { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string TYPE { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string YEAR { get; set; }

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
        public string ADDRESS { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string PURPOSE { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string HCP_CODE { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string HCP_NAME { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string HCO_CODE { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string HCO_NAME { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string SUBJECT { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string HOST_COUNTRY { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string INVITING_COUNTRY { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string PAYMENT_COUNTRY { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string PAYMENT_DATE { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string CURRENCY { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public decimal AMOUNT_CURRENCY { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public decimal AMOUNT_KOR { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string FLIGHT_CLASS { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string FLIGHT_CHECKIN_DATE { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string FLIGHT_CHECKOUT_DATE { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string FLIGHT_COMMENT { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string ACCOMMODATION_CHECKIN_DATE { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string ACCOMMODATION_CHECKOUT_DATE { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string ACCOMMODATION_COMMENT { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public decimal AMOUNT_MEAL_BEVERAGE { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public decimal AMOUNT_TRANSPORTAION { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string COMMENT { get; set; }

        /// <summary>  
        /// 
        /// </summary>
        [DataMember]
        public string AGREE_KRPIA { get; set; }

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
