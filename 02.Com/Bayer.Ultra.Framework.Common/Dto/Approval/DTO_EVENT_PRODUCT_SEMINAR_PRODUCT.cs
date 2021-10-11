using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_EVENT_PRODUCT_SEMINAR_PRODUCT
    {
        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string PROCESS_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public int PRODUCT_IDX { get; set; }

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
        public string PRODUCT_TYPE { get; set; }

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
