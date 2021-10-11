using Bayer.Ultra.Framework.Common.Dto.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Common.Dao
{
    internal class UserDao : Framework.Database.DaoBase
    {
        /// <summary>
        /// 사용자 조회
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public List<UserInfoDto> SelectApprovalUserList(string UserName)
        {
            try
            {
                using (_context = new CommonContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@KEYWOARD", UserName);
                    var result = _context.Database.SqlQuery<UserInfoDto>(UserContext.USP_SELECT_APPROVAL_TARGET_USER_LIST, parameters);

                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// AutocompleteBox 사용자 조회
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<UserAutocompleteDto> SelectUserAutocompleteList(string keyword)
        {
            try
            {
                using (_context = new UserContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@KEYWORD", keyword);

                    var result = _context.Database.SqlQuery<UserAutocompleteDto>(UserContext.USP_SELECT_USER_AUTOCOMPETE_LIST, parameters);

                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<UserAutocompleteDto> SelectEmployeeParticipants(string keyword, string processID)
        {
            try
            {
                using (_context = new UserContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@KEYWORD", keyword);
                    parameters[1] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<UserAutocompleteDto>(UserContext.USP_SELECT_EMPLOYEE_PARTICIPANTS, parameters);
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
