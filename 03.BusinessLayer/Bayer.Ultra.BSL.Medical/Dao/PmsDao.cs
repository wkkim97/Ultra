using Bayer.Ultra.Framework.Common.Dto.Medical;
using Bayer.Ultra.Framework.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Medical.Dao
{
    internal class PmsDao : Framework.Database.DaoBase
    { 
        /// <summary>
        /// Medical Study List 조회
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public List<pmslistDto> SelectMedicalPMSList(string UserId, string isAdmin)
        {
            try
            {
                using (_context = new PmsContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@USER_ID", UserId);
                    parameters[1] = new SqlParameter("@IS_ADMIN", isAdmin);
                    var result = _context.Database.SqlQuery<pmslistDto>(PmsContext.USP_SELECT_MEDICAL_PMS_LIST, parameters);

                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// PMS 저장 수정
        /// </summary>
        /// <param name="dto"></param>
        public string ModifyPMS(DTO_MEDICAL_PMS dto)
        {
            try
            {
                using (_context = new PmsContext())
                {
                    var result = _context.Database.SqlQuery<string>(PmsContext.USP_MERGE_MEDICAL_PMS, ParameterMapper.Mapping(dto));
                    return result.FirstOrDefault().ToString();
                }
            }
            catch
            {
                throw;
            }
        }

        public pmslistDto SelectMedicalPMS(string idx)
        {
            try
            {
                using (_context = new PmsContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@IDX", idx);
                    var result = _context.Database.SqlQuery<pmslistDto>(PmsContext.USP_SELECT_MEDICAL_PMS, parameters);

                    return result.FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// PMS log 추가
        /// </summary>
        /// <param name="idx"></param>
        public void InsertpmsLog(string idx)
        {
            try
            {
                using (_context = new PmsContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@IDX", idx);
                    _context.Database.ExecuteSqlCommand(PmsContext.USP_INSERT_MEDICAL_PMS_LOG, parameters);
                }
            }
            catch
            { 
            }
        }
    }
}
