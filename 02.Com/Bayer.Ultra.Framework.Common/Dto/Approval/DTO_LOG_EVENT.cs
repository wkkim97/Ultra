using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class DTO_LOG_EVENT
    {
        [DataMember]
        public int IDX { get; set; }

        [DataMember]
        public string PROCESS_ID { get; set; }

        [DataMember]
        public string REGISTER_ID { get; set; }

        [DataMember]
        public string REGISTER { get; set; }

        [DataMember]
        public string LOG_TYPE { get; set; }

        [DataMember]
        public string LOG_CATEGORY { get; set; }

        [DataMember]
        public string LOG_CATEGORY_NAME { get; set; }

        [DataMember]
        public string COMMENT { get; set; }

        [DataMember]
        public DateTime? CREATE_DATE { get; set; }

        [DataMember]
        public string FILE_PATH { get; set; }

    }
}
