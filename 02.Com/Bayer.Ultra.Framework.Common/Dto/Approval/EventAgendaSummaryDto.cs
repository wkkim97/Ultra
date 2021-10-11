using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class EventAgendaSummaryDto
    {
        [DataMember]
        public string START_DATE { get; set; }

        [DataMember]
        public string START_TIME { get; set; }

        [DataMember]
        public string SUBJECT { get; set; }

        [DataMember]
        public string ROLES { get; set; }
    }
}
