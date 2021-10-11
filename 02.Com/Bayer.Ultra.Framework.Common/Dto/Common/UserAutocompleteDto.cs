using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Common
{
    [DataContract]
    public class UserAutocompleteDto
    {
        [DataMember]
        public string USER_ID { get; set; }

        [DataMember]
        public string FULL_NAME { get; set; }

        [DataMember]
        public string ORG_ACRONYM { get; set; }

        /// <summary>
        /// 2017.12.15 이정주
        /// 직원인 경우 Participant에 중복 등록을 방지하기 위해 추가
        /// </summary>
        [DataMember]
        public string IS_EXIST { get; set; }
    }
}
