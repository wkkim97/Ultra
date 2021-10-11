using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common.Dto.Approval
{
    public class ModuleAgendaRuleDto
    {
        public string PROCESS_ID { get; set; }
        public string EVENT_KEY { get; set; }
        public string EVENT_NAME { get; set; }
        public string PREFIX_EVENT_KEY { get; set; }
        public string MATERIAL_CODE { get; set; }
        public string COST_CENTER { get; set; }
        public string SAP_NO { get; set; }
        public string KRPIA { get; set; }

        public decimal AMOUNT { get; set; }
        public string HCP_CODE { get; set; }
        public string HCP_NAME { get; set; }
        public int AGENDA_IDX { get; set; }
        public int AGENDA_ROLE_IDX { get; set; }

        public string REQUESTER_ID { get; set; }

        public string REQUESTER_NAME { get; set; }
    }
}
