using Bayer.Ultra.Framework.Common.Dto.Radiology;
using Bayer.Ultra.Framework.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Report.Dao
{
    internal class NonOneKeyDao : Framework.Database.DaoBase
    {
        public AssignedNonOneKeyListDto SelectNonOneKeyData(int NON_ONEKEY_ID)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@NON_ONEKEY_ID", NON_ONEKEY_ID);

                    var result = _context.Database.SqlQuery<AssignedNonOneKeyListDto>(ReportContext.USP_SELECT_NON_ONEKEY, parameters);
                    return result.FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<AssignedNonOneKeyListDto> SelectAssignedNonOneKeyList(string user_id, string user_type)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@USER_ID", user_id);
                    parameters[1] = new SqlParameter("@USER_TYPE", user_type);

                    var result = _context.Database.SqlQuery<AssignedNonOneKeyListDto>(ReportContext.USP_SELECT_NON_ONEKEY_LIST, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        public List<CustomerListDto> SelectCustomerList(string customer_type, string customer_name)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@CUSTOMER_TYPE", customer_type);
                    parameters[1] = new SqlParameter("@CUSTOMER_NAME", customer_name);

                    var result = _context.Database.SqlQuery<CustomerListDto>(ReportContext.USP_SELECT_SEARCH_NON_ONEKEY, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        public int MergeCustomerData(MergeCustomerDto dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[11];
                    parameters[0] = new SqlParameter("@NON_ONEKEY_ID", dto.NON_ONEKEY_ID);
                    parameters[1] = new SqlParameter("@REQUESTER_ID", dto.REQUESTER_ID);
                    parameters[2] = new SqlParameter("@REQUEST_TYPE", dto.REQUEST_TYPE);
                    parameters[3] = new SqlParameter("@CUSTOMER_TYPE", dto.CUSTOMER_TYPE);
                    parameters[4] = new SqlParameter("@CUSTOMER_NAME", dto.CUSTOMER_NAME);
                    parameters[5] = new SqlParameter("@GENDER", dto.GENDER);
                    parameters[6] = new SqlParameter("@ORGANIZATION_ID", dto.ORGANIZATION_ID);
                    parameters[7] = new SqlParameter("@ORGANIZATION_NAME", dto.ORGANIZATION_NAME);
                    parameters[8] = new SqlParameter("@NON_ONEKEY_STATUS", dto.NON_ONEKEY_STATUS);
                    parameters[9] = new SqlParameter("@REMARK", dto.REMARK);
                    parameters[10] = new SqlParameter("@CREATOR_ID", dto.CREATOR_ID);

                    var result = _context.Database.SqlQuery<int>(ReportContext.USP_MERGE_NON_ONEKEY, parameters);
                    return Convert.ToInt32(result.FirstOrDefault().ToString());
                }
            }
            catch
            {
                throw;
            }
        }
        public List<HospitalListDto> SelectNonOneKeyHospitalList(string keyword)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@KEYWORD", keyword);

                    var result = _context.Database.SqlQuery<HospitalListDto>(ReportContext.USP_SELECT_SEARCH_HOSPITAL, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        public void InsertLog(int NON_ONEKEY_ID, string REGISTER_ID, string LOG_TYPE, string LOG_CATEGORY, string COMMENT)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@NON_ONEKEY_ID", NON_ONEKEY_ID);
                parameters[1] = new SqlParameter("@REGISTER_ID", REGISTER_ID);
                parameters[2] = new SqlParameter("@LOG_TYPE", LOG_TYPE);
                parameters[3] = new SqlParameter("@LOG_CATEGORY", LOG_CATEGORY);
                parameters[4] = new SqlParameter("@COMMENT", COMMENT);
                using (_context = new ReportContext())
                {
                    _context.Database.ExecuteSqlCommand(ReportContext.USP_INSERT_NON_ONEKEY_LOG, parameters);
                }
            }
            catch
            {
                throw;
            }
        }
        public List<SelectLogDto> SelectLog(string NON_ONEKEY_ID)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@NON_ONEKEY_ID", NON_ONEKEY_ID);

                    var result = _context.Database.SqlQuery<SelectLogDto>(ReportContext.USP_SELECT_NON_ONEKEY_LOG_LIST, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        #region [ InsertNonOnekeyAttachFiles - Non onekey 첨부파일 저장]

        public int InsertNonOnekeyAttachFiles(NonOnekeyAttachDto file)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[11];
                    parameters[0] = new SqlParameter("@NON_ONEKEY_ID", file.NON_ONEKEY_ID);
                    parameters[1] = new SqlParameter("@ATTACH_FILE_TYPE", file.ATTACH_FILE_TYPE);
                    parameters[2] = new SqlParameter("@SEQ", file.SEQ);
                    parameters[3] = new SqlParameter("@REFER_IDX", file.REFER_IDX);
                    parameters[4] = new SqlParameter("@DISPLAY_FILE_NAME", file.DISPLAY_FILE_NAME);
                    parameters[5] = new SqlParameter("@SAVED_FILE_NAME", file.SAVED_FILE_NAME);
                    parameters[6] = new SqlParameter("@FILE_SIZE", file.FILE_SIZE);
                    parameters[7] = new SqlParameter("@FILE_PATH", file.FILE_PATH);
                    parameters[8] = new SqlParameter("@FILE_HANDLER_URL", file.FILE_HANDLER_URL);
                    parameters[9] = new SqlParameter("@IS_DELETED", file.IS_DELETED);
                    parameters[10] = new SqlParameter("@CREATOR_ID", file.CREATOR_ID);

                    var result = _context.Database.SqlQuery<int>(ReportContext.USP_INSERT_NON_ONEKEY_ATTACH_FILES, parameters);
                    return Convert.ToInt32(result.FirstOrDefault().ToString());

                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region [ DeleteNonOnekeyAttachFiles - Non onekey 첨부파일 삭제 ]

        public void DeleteNonOnekeyAttachFiles(int IDX, string UPDATER_ID)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@IDX", IDX);
                    parameters[1] = new SqlParameter("@UPDATER_ID", UPDATER_ID);

                    _context.Database.ExecuteSqlCommand(ReportContext.USP_DELETE_NON_ONEKEY_ATTACH_FILES, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region [ SelectNonOnekeyAttachFile - Non onekey 첨부파일 조회 ]
        public List<NonOnekeyAttachDto> SelectNonOnekeyAttachFile(int NON_ONEKEY_ID, string IDXS)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@NON_ONEKEY_ID", NON_ONEKEY_ID);
                    parameters[1] = new SqlParameter("@IDXS", IDXS);

                    var result = _context.Database.SqlQuery<NonOnekeyAttachDto>(ReportContext.USP_SELECT_NON_ONEKEY_ATTACH_FILES, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}