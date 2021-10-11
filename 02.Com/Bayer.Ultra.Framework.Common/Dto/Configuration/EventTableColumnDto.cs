using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Configuration
{
    [DataContract]
    public class EventTableColumnDto
    {
        [DataMember]
        public string ColumnId { get; set; }

        [DataMember]
        public string ColumnName { get; set; }

        [DataMember]
        public string DataType { get; set; }
    }
}
