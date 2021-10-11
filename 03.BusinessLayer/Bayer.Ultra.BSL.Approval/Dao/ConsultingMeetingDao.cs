using Bayer.Ultra.Framework.Common.Dto.Approval;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Approval.Dao
{
    internal class ConsultingMeetingDao : Framework.Database.DaoBase
    {
        public void MergeConsultingMeeting(DTO_EVENT_CONSULTING_MEETING dto)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[23];
                    parameters[0] = new SqlParameter("@PROCESS_ID", dto.PROCESS_ID);
                    parameters[1] = new SqlParameter("@SUBJECT", dto.SUBJECT);
                    parameters[2] = new SqlParameter("@EVENT_KEY", dto.EVENT_KEY);
                    parameters[3] = new SqlParameter("@PROCESS_STATUS", dto.PROCESS_STATUS);
                    parameters[4] = new SqlParameter("@REQUESTER_ID", dto.REQUESTER_ID);
                    parameters[5] = new SqlParameter("@COMPANY_CODE", dto.COMPANY_CODE);
                    parameters[6] = new SqlParameter("@ORGANIZATION_NAME", dto.ORGANIZATION_NAME);
                    parameters[7] = new SqlParameter("@LIFE_CYCLE", dto.LIFE_CYCLE);
                    //version 1.0.6 ABM 문서에 Category  추가
                    parameters[8] = new SqlParameter("@CATEGORY", dto.CATEGORY);
                    parameters[9] = new SqlParameter("@START_TIME", dto.START_TIME);
					parameters[10] = new SqlParameter("@END_TIME", dto.END_TIME);
					parameters[11] = new SqlParameter("@ADDRESS_OF_VENUE", dto.ADDRESS_OF_VENUE);
                    parameters[12] = new SqlParameter("@VENUE_SELECTION_REASON", dto.VENUE_SELECTION_REASON);
                    parameters[13] = new SqlParameter("@VENUE_SELECTION_REASON_MANUAL", dto.VENUE_SELECTION_REASON_MANUAL);
                    parameters[14] = new SqlParameter("@PURPOSE_OBJECTIVE", dto.PURPOSE_OBJECTIVE);
                    parameters[15] = new SqlParameter("@COST_PLAN", dto.COST_PLAN);
                    //version 1.0.4 consulting meeting 에 complience option 추가
                    parameters[16] = new SqlParameter("@COMPLIANCE_1", dto.COMPLIANCE_1);
                    parameters[17] = new SqlParameter("@COMPLIANCE_2", dto.COMPLIANCE_2);
                    parameters[18] = new SqlParameter("@COMPLIANCE_REASON_1", dto.COMPLIANCE_REASON_1);
                    parameters[19] = new SqlParameter("@COMPLIANCE_REASON_2", dto.COMPLIANCE_REASON_2);

                    parameters[20] = new SqlParameter("@IS_DISUSED", dto.IS_DISUSED);
                    parameters[21] = new SqlParameter("@CREATOR_ID", dto.CREATOR_ID);
                    parameters[22] = new SqlParameter("@UPDATER_ID", dto.UPDATER_ID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_MERGE_EVENT_CONSULTING_MEETING, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public DTO_EVENT_CONSULTING_MEETING SelectConsultingMeeting(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_EVENT_CONSULTING_MEETING>(ApprovalContext.USP_SELECT_EVENT_CONSULTING_MEETING, parameters);
                    return result.FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
