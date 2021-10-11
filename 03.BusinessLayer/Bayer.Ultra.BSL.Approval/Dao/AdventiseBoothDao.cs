using Bayer.Ultra.Framework.Common.Dto.Approval;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Approval.Dao
{
    internal class AdventiseBoothDao : Framework.Database.DaoBase
    {
        /// <summary>
        /// Merge Adventise Booth
        /// </summary>
        /// <param name="dto"></param>
        public void MergeAdventiseBooth(DTO_EVENT_ADVENTISEMENT dto)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[14];
                    parameters[0] = new SqlParameter("@PROCESS_ID", dto.PROCESS_ID);
                    parameters[1] = new SqlParameter("@SUBJECT", dto.SUBJECT);
                    parameters[2] = new SqlParameter("@EVENT_KEY", dto.EVENT_KEY);
                    parameters[3] = new SqlParameter("@PROCESS_STATUS", dto.PROCESS_STATUS);
                    parameters[4] = new SqlParameter("@REQUESTER_ID", dto.REQUESTER_ID);
                    parameters[5] = new SqlParameter("@COMPANY_CODE", dto.COMPANY_CODE);
                    parameters[6] = new SqlParameter("@ORGANIZATION_NAME", dto.ORGANIZATION_NAME);
                    parameters[7] = new SqlParameter("@LIFE_CYCLE", dto.LIFE_CYCLE);
                    parameters[8] = new SqlParameter("@START_TIME", dto.START_TIME);
                    parameters[9] = new SqlParameter("@END_TIME", dto.END_TIME);
                    parameters[10] = new SqlParameter("@HOST", dto.HOST);
					parameters[11] = new SqlParameter("@IS_DISUSED", dto.IS_DISUSED);
                    parameters[12] = new SqlParameter("@CREATOR_ID", dto.CREATOR_ID);
                    parameters[13] = new SqlParameter("@UPDATER_ID", dto.UPDATER_ID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_MERGE_EVENT_ADVENTISEMENT, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

		/// <summary>
		/// Select Adventise Booth
		/// </summary>
		/// <param name="processID"></param>
		/// <returns></returns>
		public DTO_EVENT_ADVENTISEMENT SelectAdventiseBooth(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_EVENT_ADVENTISEMENT>(ApprovalContext.USP_SELECT_EVENT_ADVENTISEMENT, parameters);
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
