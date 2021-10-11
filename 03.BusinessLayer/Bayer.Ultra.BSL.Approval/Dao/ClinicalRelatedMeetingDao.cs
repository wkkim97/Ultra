using Bayer.Ultra.Framework.Common.Dto.Approval;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Approval.Dao
{
    internal class ClinicalRelatedMeetingDao : Framework.Database.DaoBase
    {
        /// <summary>
        /// Merge Clinical Related Metting
        /// </summary>
        /// <param name="dto"></param>
        public void MergeClinicalRelatedMeeting(DTO_EVENT_CLINICAL_MEETING dto)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[17];
                    parameters[0] = new SqlParameter("@PROCESS_ID", dto.PROCESS_ID);
                    parameters[1] = new SqlParameter("@SUBJECT", dto.SUBJECT);
                    parameters[2] = new SqlParameter("@EVENT_KEY", dto.EVENT_KEY);
                    parameters[3] = new SqlParameter("@PROCESS_STATUS", dto.PROCESS_STATUS);
                    parameters[4] = new SqlParameter("@REQUESTER_ID", dto.REQUESTER_ID);
                    parameters[5] = new SqlParameter("@COMPANY_CODE", dto.COMPANY_CODE);
                    parameters[6] = new SqlParameter("@ORGANIZATION_NAME", dto.ORGANIZATION_NAME);
                    parameters[7] = new SqlParameter("@LIFE_CYCLE", dto.LIFE_CYCLE);
					parameters[8] = new SqlParameter("@CATEGORY", dto.CATEGORY);
					parameters[9] = new SqlParameter("@IMPACT_NO", dto.IMPACT_NO);
                    parameters[10] = new SqlParameter("@START_TIME", dto.START_TIME);
					parameters[11] = new SqlParameter("@ADDRESS_OF_VENUE", dto.ADDRESS_OF_VENUE);
					parameters[12] = new SqlParameter("@VENUE_SELECTION_REASON", dto.VENUE_SELECTION_REASON);
					parameters[13] = new SqlParameter("@VENUE_SELECTION_REASON_MANUAL", dto.VENUE_SELECTION_REASON_MANUAL);
					parameters[14] = new SqlParameter("@IS_DISUSED", dto.IS_DISUSED);
                    parameters[15] = new SqlParameter("@CREATOR_ID", dto.CREATOR_ID);
                    parameters[16] = new SqlParameter("@UPDATER_ID", dto.UPDATER_ID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_MERGE_EVENT_CLINICAL_MEETING, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

		/// <summary>
		/// Select Clinical Related Metting
		/// </summary>
		/// <param name="processID"></param>
		/// <returns></returns>
		public DTO_EVENT_CLINICAL_MEETING SelectClinicalRelatedMeeting(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_EVENT_CLINICAL_MEETING>(ApprovalContext.USP_SELECT_EVENT_CLINICAL_MEETING, parameters);
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
