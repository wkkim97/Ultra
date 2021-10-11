using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class EventPaymentLayerDto
    {
        [DataMember]
        public string DataSource { get; set; }

        [DataMember]
        public string Date { get; set; }

        [DataMember]
        public string Line1 { get; set; }

        [DataMember]
        public string Line2 { get; set; }

        [DataMember]
        public decimal Amount { get; set; }

        [DataMember]
        public string Comment { get; set; }

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public string SavedName { get; set; }

        [DataMember]
        public string AttachType { get; set; }

        [DataMember]
        public string FilePath { get; set; }

        [DataMember]
        public string FileHandlerUrl { get; set; }

        [DataMember]
        public string CATEGORY_NAME { get; set; }

        [DataMember]
        public string USER_NAME { get; set; }
    }
}
