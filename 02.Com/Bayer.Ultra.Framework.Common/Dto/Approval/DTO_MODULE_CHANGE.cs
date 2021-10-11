using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    
    public class DTO_MODULE_CHANGE
    {
        [DataMember]
        public int ID { get; set; }

        [DataMember]
        public string PROCESS_ID { get; set; }

        [DataMember]
        public string EVENT_KEY { get; set; }

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
        public string EVIDENCE { get; set; }

        [DataMember]
        public string COMMENT { get; set; }

        [DataMember]
        public string RESULT { get; set; }

        [DataMember]
        public string UPDATER_ID { get; set; }

        [DataMember]
        public string UPDATE_DATE { get; set; }



    }
}
