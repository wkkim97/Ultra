using System;
using System.Runtime.Serialization;

namespace Bayer.Ultra.Framework.Common.Dto.Common
{
    [DataContract]
    public class DTO_LOGIN_HISTORY
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string FULL_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string USER_ID { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string MAILADDRESS { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string ORG_ACRONYM { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string CLIENTIP { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        public DateTime? CREATE_DATE { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string WINDOWUSERNAME { get; set; }

        /// <summary>
        /// 
        /// </summary> 
        [DataMember]
        public string WINDOWDOMAINNAME { get; set; }


    }
}
