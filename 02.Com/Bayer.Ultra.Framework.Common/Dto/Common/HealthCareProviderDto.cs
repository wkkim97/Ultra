using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Common
{
    [DataContract]
    public class HealthCareProviderDto
    {
        /// <summary>
        /// 타입(의사/간호사/약사)
        /// </summary>
        [DataMember]
        public string Type { get; set; }

        /// <summary>
        /// HCP 코드
        /// </summary>
        [DataMember]
        public string HCPCode { get; set; }

        /// <summary>
        /// HCP 명
        /// </summary>
        [DataMember]
        public string HCPName { get; set; }

        /// <summary>
        /// 조직 코드(병원/약국)
        /// </summary>
        [DataMember]
        public string OrganizationCode { get; set; }

        /// <summary>
        /// 조직 명
        /// </summary>
        [DataMember]
        public string OrganizationName { get; set; }

        /// <summary>
        /// 전공 코드
        /// </summary>
        [DataMember]
        public string SpecialtyCode { get; set; }

        /// <summary>
        /// 전공 명
        /// </summary>
        [DataMember]
        public string SpecialtyName { get; set; }

        [DataMember]
        public string IsExist { get; set; }

        [DataMember]
        public int VisitCount { get; set; }
    }
}
