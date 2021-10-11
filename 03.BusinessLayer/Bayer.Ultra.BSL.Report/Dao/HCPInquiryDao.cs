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
    internal class HCPInquiryDao : Framework.Database.DaoBase
    {
        public List<HCPInquiryCustomerDto> SelectInquiryCustomerList(string name, string org, string specialty)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@HCPName", name);
                    parameters[1] = new SqlParameter("@OrgName", org);
                    parameters[2] = new SqlParameter("@SpeName", specialty);

                    var result = _context.Database.SqlQuery<HCPInquiryCustomerDto>(ReportContext.USP_SELECT_SEARCH_MASTER_DOCTOR, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        public int MergeCustomerRequest(HCPInquiryMergeDto customerDto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[15];
                    parameters[0] = new SqlParameter("@HCP_INQUIRY_REQUEST_ID", customerDto.HCP_INQUIRY_REQUEST_ID);
                    parameters[1] = new SqlParameter("@REQUESTER_ID", customerDto.REQUESTER_ID);
                    parameters[2] = new SqlParameter("@REQUEST_TYPE", customerDto.REQUEST_TYPE);
                    parameters[3] = new SqlParameter("@CUSTOMER_ID", customerDto.CUSTOMER_ID);
                    parameters[4] = new SqlParameter("@CUSTOMER_NAME", customerDto.CUSTOMER_NAME);
                    parameters[5] = new SqlParameter("@ORGANIZATION_ID", customerDto.ORGANIZATION_ID);
                    parameters[6] = new SqlParameter("@ORGANIZATION_NAME", customerDto.ORGANIZATION_NAME);
                    parameters[7] = new SqlParameter("@SPECIALTY_ID", customerDto.SPECIALTY_ID);
                    parameters[8] = new SqlParameter("@SPECIALTY_NAME", customerDto.SPECIALTY_NAME);
                    parameters[9] = new SqlParameter("@YEAR_FROM", customerDto.YEAR_FROM);
                    parameters[10] = new SqlParameter("@YEAR_TO", customerDto.YEAR_TO);
                    parameters[11] = new SqlParameter("@INQUIRY_STATUS", customerDto.INQUIRY_STATUS);
                    parameters[12] = new SqlParameter("@REMARK", customerDto.REMARK);
                    parameters[13] = new SqlParameter("@DELIVERED_TO_HCP", customerDto.DELIVERED_TO_HCP);
                    parameters[14] = new SqlParameter("@CREATOR_ID", customerDto.CREATOR_ID);

                    var result = _context.Database.SqlQuery<int>(ReportContext.USP_MERGE_HCP_INQUIRY_REQUEST, parameters);
                    return Convert.ToInt32(result.FirstOrDefault().ToString());
                }
            }
            catch
            {
                throw;
            }
        }
        public List<HCPInquiryListDto> SelectHCPInquiryList(string USER_ID, string USER_TYPE)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@USER_ID", USER_ID);
                    parameters[1] = new SqlParameter("@USER_TYPE", USER_TYPE);

                    var result = _context.Database.SqlQuery<HCPInquiryListDto>(ReportContext.USP_SELECT_HCP_INQUIRY_REQUEST_LIST, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        public List<SelectHCPInquiryLogDto> SelectHCPInquiryLog(string HCP_INQUIRY_REQUEST_ID)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@HCP_INQUIRY_REQUEST_ID", HCP_INQUIRY_REQUEST_ID);

                    var result = _context.Database.SqlQuery<SelectHCPInquiryLogDto>(ReportContext.USP_SELECT_HCP_INQUIRY_REQUEST_LOG_LIST, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        public void InsertHCPInquiryLog(int HCP_INQUIRY_REQUEST_ID, string REGISTER_ID, string LOG_TYPE, string LOG_CATEGORY, string COMMENT)
        {
            try
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@HCP_INQUIRY_REQUEST_ID", HCP_INQUIRY_REQUEST_ID);
                parameters[1] = new SqlParameter("@REGISTER_ID", REGISTER_ID);
                parameters[2] = new SqlParameter("@LOG_TYPE", LOG_TYPE);
                parameters[3] = new SqlParameter("@LOG_CATEGORY", LOG_CATEGORY);
                parameters[4] = new SqlParameter("@COMMENT", COMMENT);
                using (_context = new ReportContext())
                {
                    _context.Database.ExecuteSqlCommand(ReportContext.USP_INSERT_HCP_INQUIRY_REQUEST_LOG, parameters);
                }
            }
            catch
            {
                throw;
            }
        }
        public HCPInquiryListDto SelectHCPInquiryData(int HCP_INQUIRY_REQUEST_ID)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@HCP_INQUIRY_REQUEST_ID", HCP_INQUIRY_REQUEST_ID);

                    var result = _context.Database.SqlQuery<HCPInquiryListDto>(ReportContext.USP_SELECT_HCP_INQUIRY_REQUEST, parameters);
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