using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    [DataContract]
    public class EventAttachFileResponseDto
    {
        [DataMember]
        public int Index;

        [DataMember]
        public string DisplayName;

        [DataMember]
        public string SavedName;

        [DataMember]
        public long FileSize;

        [DataMember]
        public string AttachType;

        [DataMember]
        public string FilePath;

        [DataMember]
        public string FileHandlerUrl;

        [DataMember]
        public string ErrorMessage;
    }
}
