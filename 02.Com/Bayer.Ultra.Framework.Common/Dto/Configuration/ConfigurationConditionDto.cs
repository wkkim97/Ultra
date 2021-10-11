using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Configuration
{
    [DataContract]
    public class ConfigurationConditionDto
    {
        [DataMember]
        public string APPROVER_LOCATION { get; set; }

        [DataMember]
        public string IS_MANDATORY { get; set; }

        [DataMember]
        public int CONDITION_SEQ { get; set; }

        [DataMember]
        public string FIELD_NAME { get; set; }

        [DataMember]
        public string FIELD_TYPE { get; set; }

        [DataMember]
        public string CONDITION { get; set; }

        [DataMember]
        public string VALUE { get; set; }

        [DataMember]
        public string OPTION { get; set; }
    }
}
