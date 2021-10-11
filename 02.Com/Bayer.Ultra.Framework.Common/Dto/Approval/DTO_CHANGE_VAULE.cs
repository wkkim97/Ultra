using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_CHANGE_VAULE
    {        
        
        [DataMember]
        public string PROCESS_ID { get; set; }

        [DataMember]
        public string CATEGORY { get; set; }

        [DataMember]
        public string ADJUSTMENT_AREA { get; set; }

        [DataMember]
        public string OLD_VALUE { get; set; }

        [DataMember]
        public string NEW_VALUE { get; set; }

        [DataMember]
        public string REASON { get; set; }

        [DataMember]
        public string UPDATER_ID { get; set; }


    }
}
