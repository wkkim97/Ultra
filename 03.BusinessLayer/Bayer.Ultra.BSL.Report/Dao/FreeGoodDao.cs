using Bayer.Ultra.Framework.Common.Dto.Report;
using Bayer.Ultra.Framework.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Report.Dao
{
    internal class FreeGoodDao : Framework.Database.DaoBase
    {
        /// <summary>
        /// Report Receipt Free Good  List 조회
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public List<ReceiptForFreeGoodDto> SelectReceiptList(string userid)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@USER_ID", userid);

                    var result = _context.Database.SqlQuery<ReceiptForFreeGoodDto>(ReportContext.USP_SELECT_REPORT_RESEIPT_FREE_GOOD_LIST, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Report Receipt Free Good Save
        /// </summary> 
        /// <returns></returns>
        public void ModifyReceiptFreeGood(DTO_REPORT_RECEIPT_FREE_GOOD dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    _context.Database.ExecuteSqlCommand(ReportContext.USP_MERGE_REPORT_RECEIPT_FREE_GOOD, ParameterMapper.Mapping(dto));
                }
            }
            catch
            {
                throw;
            }
        }

        public void ModifyReceiptFreeGood_Return(DTO_REPORT_RECEIPT_FREE_GOOD dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    _context.Database.ExecuteSqlCommand(ReportContext.USP_MERGE_REPORT_RECEIPT_FREE_GOOD_RETURN, ParameterMapper.Mapping(dto));
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Report Receipt Free Good  List 조회
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public ReceiptForFreeGoodDto  SelectReceiptItem(string processid, string idx)
        {
            try
            {
                using (_context = new ReportContext())
                {
                   
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processid);
                    parameters[1] = new SqlParameter("@IDX", idx);
                    var result = _context.Database.SqlQuery<ReceiptForFreeGoodDto>(ReportContext.USP_SELECT_REPORT_RESEIPT_FREE_GOOD_ITEM, parameters);
                    return result.FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Report Receipt Free Good  List 조회
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public void UpdateReceiptFreeGoodFileIdx(string processid, string idx, string fileIdxs)
        {
            try
            {
                using (_context = new ReportContext())
                {

                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processid);
                    parameters[1] = new SqlParameter("@IDX", idx);
                    parameters[2] = new SqlParameter("@EVENT_FILE_IDX", fileIdxs);
                    _context.Database.ExecuteSqlCommand(ReportContext.USP_UPDATE_REPORT_RECEIPT_FREE_GOOD_FILE_IDX, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public void UpdateReceiptFreeGoodFileReturnIdx(string processid, string idx, string fileIdxs)
        {
            try
            {
                using (_context = new ReportContext())
                {

                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processid);
                    parameters[1] = new SqlParameter("@IDX", idx);
                    parameters[2] = new SqlParameter("@EVENT_FILE_IDX", fileIdxs);
                    _context.Database.ExecuteSqlCommand(ReportContext.USP_UPDATE_REPORT_RECEIPT_FREE_GOOD_RETURN_FILE_IDX, parameters);
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// freegood product 반품 상태 변경 
        /// </summary>
        /// <param name="processid"></param>
        /// <param name="idx"></param>
        /// <param name="status"></param>
        /// <param name="returndate"></param>
        /// <param name="userid"></param>
        public void UpdateReceiptStatus(string processid, string idx, string eventkey, string status, string sapOrder, string userid)
        { 
            try
            {
                using (_context = new ReportContext())
                { 
                    SqlParameter[] parameters = new SqlParameter[6];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processid);
                    parameters[1] = new SqlParameter("@IDX", idx);
                    parameters[2] = new SqlParameter("@EVENT_KEY", eventkey);
                    parameters[3] = new SqlParameter("@STATUS", status);
                    parameters[4] = new SqlParameter("@SAP_ORDER", sapOrder);
                    parameters[5] = new SqlParameter("@UPDATER_ID", userid);
 
                    _context.Database.ExecuteSqlCommand(ReportContext.USP_UPDATE_REPORT_RECEIPT_RETURN_STATUS, parameters);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
