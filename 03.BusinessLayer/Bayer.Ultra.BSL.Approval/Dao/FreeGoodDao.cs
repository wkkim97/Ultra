using Bayer.Ultra.Framework.Common.Dto.Approval;
using Bayer.Ultra.Framework.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Approval.Dao
{
    internal class FreeGoodDao : Framework.Database.DaoBase
    {
        public void InsertFreeGood(DTO_EVENT_FREE_GOOD dto)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_INSERT_EVENT_FREE_GOOD, ParameterMapper.Mapping(dto));
                }
            }
            catch
            {
                throw;
            }
        }

        public void MedifyFreeGoodHCP(DTO_EVENT_FREE_GOOD_HCP dto)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_MERGE_EVENT_FREE_GOOD_HCP, ParameterMapper.Mapping(dto));
                }
            }
            catch
            {
                throw;
            }
        }

        public DTO_EVENT_FREE_GOOD SelectFreeGood(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_EVENT_FREE_GOOD>(ApprovalContext.USP_SELECT_EVENT_FREE_GOOD, parameters);
                    return result.FirstOrDefault();

                }
            }
            catch
            {
                throw;
            }
        }


        public List<EventFreeGoodHcpListDto> SelectFreeGoodHcp(string processID, string type)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@TYPE", type);
                    var result = _context.Database.SqlQuery<EventFreeGoodHcpListDto>(ApprovalContext.USP_SELECT_EVENT_FREE_GOOD_HCP, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// FreeGood Doc에  HCP, Sample 이 존재한지 체크
        /// 단, 반품된 결재문서는 제외한다.
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="hcpcode"></param>
        public string IsExistsFreeGoodHcpItem(string processId, string hcpcode, string hcocode, string sampleCode, string type)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[5];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processId);
                    parameters[1] = new SqlParameter("@HCP_CODE", hcpcode);
                    parameters[2] = new SqlParameter("@HCO_CODE", hcocode);
                    parameters[3] = new SqlParameter("@SAMPLE_CODE", sampleCode);
                    parameters[4] = new SqlParameter("@TYPE", type);

                    var result = _context.Database.SqlQuery<string>(ApprovalContext.USP_SELECT_EXISTS_FREE_GOOD_HCP_SAMPLE, parameters);
                    return result.FirstOrDefault().ToString();
                }
            }
            catch
            {
                throw;
            }
        }


        public void DeleteFreeGoodHCP(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {

                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_DELETE_EVENT_FREE_GOOD_HCP, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public List<FreeGoodExistSampleDto> SelectExistFreeGoodSample(string hcpCode, string hcpName, string hcoCode, string sampleCode)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@HCP_CODE", hcpCode);
                    parameters[1] = new SqlParameter("@HCP_NAME", hcpName);
                    parameters[2] = new SqlParameter("@HCO_CODE", hcoCode);
                    parameters[3] = new SqlParameter("@SAMPLE_CODE", sampleCode);

                    var result = _context.Database.SqlQuery<FreeGoodExistSampleDto>(ApprovalContext.USP_SELECT_EVENT_FREE_GOOD_HCP_EXISTS, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<FreeGoodExistSampleDto> SelectExistFreeGoodSampleRAD(string hcpCode, string hcpName, string hcoCode, string sampleCode)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@HCP_CODE", hcpCode);
                    parameters[1] = new SqlParameter("@HCP_NAME", hcpName);
                    parameters[2] = new SqlParameter("@HCO_CODE", hcoCode);
                    parameters[3] = new SqlParameter("@SAMPLE_CODE", sampleCode);

                    var result = _context.Database.SqlQuery<FreeGoodExistSampleDto>(ApprovalContext.USP_SELECT_EVENT_FREE_GOOD_HCP_EXISTS_RAD, parameters);
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
