using System.Runtime.Serialization;

namespace Bayer.Ultra.Framework.Common.Dto.Common
{
    [DataContract]
    public class AuthenticationDto
    {
        /// <summary>
        /// 사용자 ID
        /// </summary>
        [DataMember]
        public string USER_ID { get; set; }

        /// <summary>
        /// 상태
        /// </summary>
        [DataMember]
        public string IT_STATUS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string FULL_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string ORG_ACRONYM { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string COMPANY_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string COMPANY_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string MAIL_ADDRESS { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string PHONE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string MOBILE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string JOB_TITLE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string TITLE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string LEADING_SUBGROUP { get; set; }

        /// <summary>
        /// 트레이닝 완료 여부 (YES/NO)
        /// </summary>
        [DataMember]
        public string TRAINING_COMPLETED { get; set; }

        /// <summary>
        /// CRM 사용자 여부(YES/NO)
        /// </summary>
        [DataMember]
        public string CRM_USER_YN { get; set; }

    }
}
