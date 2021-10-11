using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    public class EventFreeGoodDto
    {
        public DTO_EVENT_FREE_GOOD dtoFreeGood { get; set; }
        public List<DTO_EVENT_FREE_GOOD_HCP> dtoHcp { get; set; }
    }
}
