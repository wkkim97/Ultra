using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_MODULE_SEARCH_USER
    {
        [DataMember]
        public string USER_ID { get; set; }

        [DataMember]
        public string USER_NAME { get; set; }

        [DataMember]
        public string ORGANIZATION { get; set; }

        [DataMember]
        public string IS_EXIST { get; set; }
    }
}
