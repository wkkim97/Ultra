using Bayer.Ultra.Framework.Common.Dto.Common;
using System.Data.SqlClient;
using System.Linq;

namespace Bayer.Ultra.BSL.Common.Dao
{
    internal class AuthenticationDao : Framework.Database.DaoBase
    {
        public UserInfoDto GetUserInfo(string language, string user)
        {
            try
            {
                using (_context = new CommonContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@NVCLANGUAGESET", language);
                    parameters[1] = new SqlParameter("@NVCUSERACCOUNT", user);

                    var result = _context.Database.SqlQuery<UserInfoDto>(CommonContext.USP_SELECT_USER, parameters);
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
