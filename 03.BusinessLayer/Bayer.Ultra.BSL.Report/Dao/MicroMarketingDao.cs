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
    internal class MicroMarketingDao : Framework.Database.DaoBase
    {
        public List<AssignedHospitalListDto> SelectAssignedHospitalList(string user_id, string user_type)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@USER_ID", user_id);
                    parameters[1] = new SqlParameter("@USER_TYPE", user_type);

                    var result = _context.Database.SqlQuery<AssignedHospitalListDto>(ReportContext.USP_SELECT_RAD_HOSPITAL_LIST, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        #region Equipment
        public List<HospitalEquipmentDto> SelectEquipment(string id, string organization_id)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@ID", id);
                    parameters[1] = new SqlParameter("@ORGANIZATION_ID", organization_id);

                    var result = _context.Database.SqlQuery<HospitalEquipmentDto>(ReportContext.USP_SELECT_MICRO_MARKETING_EQUIPMENT, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public void MergeEquipment(HospitalEquipmentDto dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    _context.Database.ExecuteSqlCommand(ReportContext.USP_MERGE_MICRO_MARKETING_EQUIPMENT, ParameterMapper.Mapping(dto));
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteEquipment(string id)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@ID", id);
                    _context.Database.ExecuteSqlCommand(ReportContext.USP_DELETE_MICRO_MARKETING_EQUIPMENT, parameters);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region Examination
        public List<HospitalExaminationDto> SelectExamination(string id, string organization_id, string quarter)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@ID", id);
                    parameters[1] = new SqlParameter("@ORGANIZATION_ID", organization_id);
                    parameters[2] = new SqlParameter("@QUARTER", quarter);

                    var result = _context.Database.SqlQuery<HospitalExaminationDto>(ReportContext.USP_SELECT_MICRO_MARKETING_EXAMINATION, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public void MergeExamination(MergeHospitalExaminationDto dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    _context.Database.ExecuteSqlCommand(ReportContext.USP_MERGE_MICRO_MARKETING_EXAMINATION, ParameterMapper.Mapping(dto));
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteExamination(string id)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@ID", id);
                    _context.Database.ExecuteSqlCommand(ReportContext.USP_DELETE_MICRO_MARKETING_EXAMINATION, parameters);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
        #region Market Share
        public List<HospitalMarketShareDto> SelectMarketShare(string id, string organization_id, string quarter)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@ID", id);
                    parameters[1] = new SqlParameter("@ORGANIZATION_ID", organization_id);
                    parameters[2] = new SqlParameter("@QUARTER", quarter);

                    var result = _context.Database.SqlQuery<HospitalMarketShareDto>(ReportContext.USP_SELECT_MICRO_MARKETING_MARKETSHARE, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public void MergeMarketShare(MergeHospitalMarketShareDto dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    _context.Database.ExecuteSqlCommand(ReportContext.USP_MERGE_MICRO_MARKETING_MARKETSHARE, ParameterMapper.Mapping(dto));
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteMarketShare(string id)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@ID", id);
                    _context.Database.ExecuteSqlCommand(ReportContext.USP_DELETE_MICRO_MARKETING_MARKETSHARE, parameters);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        //Micromarketing function
        #region Master_MarketShare
        public List<MasterMarketShare> SelectMasterMarketShare(string id)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@ID", id);
                    

                    var result = _context.Database.SqlQuery<MasterMarketShare>(ReportContext.USP_SELECT_MASTER_RAD_MARKETSHARE, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public void MergeMasterMarketShare(MasterMarketShare dto)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[7];
                    parameters[0] = new SqlParameter("@ID", dto.ID);
                    parameters[1] = new SqlParameter("@MANUFACTURE", dto.MANUFACTURE);
                    parameters[2] = new SqlParameter("@SEGMENT", dto.SEGMENT);
                    parameters[3] = new SqlParameter("@PRODUCT_FAMILY", dto.PRODUCT_FAMILY);
                    parameters[4] = new SqlParameter("@PRODUCT", dto.PRODUCT);
                    parameters[5] = new SqlParameter("@PRICE", dto.PRICE);
                    parameters[6] = new SqlParameter("@CREATOR_ID", dto.CREATOR_ID);
                    

                    _context.Database.ExecuteSqlCommand(ReportContext.USP_MERGE_MASTER_RAD_MARKETSHARE, parameters);
                }
            }
            catch
            {
                throw;
            }
        }
        public void DeleteMasterMarketShare(string id)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@ID", id);


                    _context.Database.ExecuteSqlCommand(ReportContext.USP_DELETE_MASTER_RAD_MARKETSHARE, parameters);
                }
            }
            catch
            {
                throw;
            }
        }
        
        public List<MasterMarketShare> SelectSearchMasterMarketShare(string family, string product)
        {
            try
            {
                using (_context = new ReportContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@PRODUCT_FAMILY", family);
                    parameters[1] = new SqlParameter("@PRODUCT", product);


                    var result = _context.Database.SqlQuery<MasterMarketShare>(ReportContext.USP_SELECT_SEARCH_MASTER_RAD_MARKETSHARE, parameters);
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
