using Bayer.Ultra.Framework.Common.Dto.Approval;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Approval.Dao
{
    internal class ScientificExchangedMeetingDao : Framework.Database.DaoBase
    {
        /// <summary>
        /// Merge Scientific Exchanged Meeting
        /// </summary>
        /// <param name="dto"></param>
        public void MergeScientificExchangedMeeting(DTO_EVENT_SCIENTIFIC_MEETING dto)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[12];
                    parameters[0] = new SqlParameter("@PROCESS_ID", dto.PROCESS_ID);
                    parameters[1] = new SqlParameter("@SUBJECT", dto.SUBJECT);
                    parameters[2] = new SqlParameter("@EVENT_KEY", dto.EVENT_KEY);
                    parameters[3] = new SqlParameter("@PROCESS_STATUS", dto.PROCESS_STATUS);
                    parameters[4] = new SqlParameter("@REQUESTER_ID", dto.REQUESTER_ID);
                    parameters[5] = new SqlParameter("@COMPANY_CODE", dto.COMPANY_CODE);
                    parameters[6] = new SqlParameter("@ORGANIZATION_NAME", dto.ORGANIZATION_NAME);
                    parameters[7] = new SqlParameter("@LIFE_CYCLE", dto.LIFE_CYCLE);
                    parameters[8] = new SqlParameter("@START_TIME", dto.START_TIME);
                    parameters[9] = new SqlParameter("@IS_DISUSED", dto.IS_DISUSED);
                    parameters[10] = new SqlParameter("@CREATOR_ID", dto.CREATOR_ID);
                    parameters[11] = new SqlParameter("@UPDATER_ID", dto.UPDATER_ID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_MERGE_EVENT_SCIENTIFIC_MEETING, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

		/// <summary>
		/// Select Scientific Exchanged Meeting
		/// </summary>
		/// <param name="processID"></param>
		/// <returns></returns>
		public DTO_EVENT_SCIENTIFIC_MEETING SelectScientificExchangedMeeting(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_EVENT_SCIENTIFIC_MEETING>(ApprovalContext.USP_SELECT_EVENT_SCIENTIFIC_MEETING, parameters);
                    return result.FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }

		/// <summary>
		/// Insert Scientific Exchanged Meeting Product
		/// </summary>
		/// <param name="product"></param>
		public void InsertScientificExchangedMeetingProduct(DTO_EVENT_SCIENTIFIC_MEETING_PRODUCT product)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[7];
                    parameters[0] = new SqlParameter("@PROCESS_ID", product.PROCESS_ID);
                    parameters[1] = new SqlParameter("@PRODUCT_IDX", product.PRODUCT_IDX);
                    parameters[2] = new SqlParameter("@PRODUCT_CODE", product.PRODUCT_CODE);
                    parameters[3] = new SqlParameter("@PRODUCT_NAME", product.PRODUCT_NAME);
                    parameters[4] = new SqlParameter("@PRODUCT_TYPE", product.PRODUCT_TYPE);
                    parameters[5] = new SqlParameter("@IS_DELETED", product.IS_DELETED);
                    parameters[6] = new SqlParameter("@CREATOR_ID", product.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_INSERT_EVENT_SCIENTIFIC_MEETING_PRODUCT, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

		/// <summary>
		/// Delete Scientific Exchanged Meeting Product All
		/// </summary>
		/// <param name="processID"></param>
		/// <param name="updaterID"></param>
		public void DeleteScientificExchangedMeetingProductAll(string processID, string updaterID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@UPDATER_ID", updaterID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_DELETE_EVENT_SCIENTIFIC_MEETING_PRODUCT_ALL, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

		/// <summary>
		/// Select Scientific Exchanged Meeting Product
		/// </summary>
		/// <param name="processID"></param>
		/// <returns></returns>
		public List<DTO_EVENT_SCIENTIFIC_MEETING_PRODUCT> SelectScientificExchangedMeetingProducts(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_EVENT_SCIENTIFIC_MEETING_PRODUCT>(ApprovalContext.USP_SELECT_EVENT_SCIENTIFIC_MEETING_PRODUCT, parameters);
                    return result.ToList();

                }
            }
            catch
            {
                throw;
            }
        }
    }
}
