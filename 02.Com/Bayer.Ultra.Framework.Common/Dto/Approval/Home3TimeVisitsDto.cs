using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class Home3TimeVisitsDto
    {
        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string HCP_CODE { get; set; }

        /// <summary>
        /// 이름
        /// </summary>
        [DataMember]
        public string HCP_NAME { get; set; }

        /// <summary>
        /// 병원코드
        /// </summary>
        [DataMember]
        public string HCO_CODE { get; set; }
        /// <summary>
        /// 소속 병원
        /// </summary>
        [DataMember]
        public string HCO_NAME { get; set; }

        /// <summary>
        /// 전공
        /// </summary>
        [DataMember]
        public string SPECIALTY_CODE { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public string SPECIALTY_NAME { get; set; }

        /// <summary>
        /// 
        /// </summary>
        [DataMember]
        public int VISIT_COUNT { get; set; }

        /// <summary>
        /// Doctor/Nurse/Pharmacist/방사선사
        /// </summary>
        [DataMember]
        public string HCP_TYPE { get; set; }
    }
}
