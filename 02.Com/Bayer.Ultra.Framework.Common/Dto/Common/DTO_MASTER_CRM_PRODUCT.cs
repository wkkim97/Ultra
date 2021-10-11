using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Common
{
    [DataContract]
    public class DTO_MASTER_CRM_PRODUCT
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string PRODUCT_TYPE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string PRODUCT_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string PRODUCT_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string SHORT_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string Product_Lines { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string STATUS { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string ProductSegments { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string Promoted { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string Competitor { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string Manufacturer { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string PRODUCT_FAMILY_NO { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string Contact_Online { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string CREATOR_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public DateTime? CREATE_DATE { get; set; }

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
