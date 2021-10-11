using Bayer.Ultra.Framework.Common.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Common.Mgr
{
    public class AuthenticationMgr_Nx : Framework.Database.MgrBase
    {
        public UserInfoDto GetUserInfo(string language, string user)
        {
            try
            {
                using (Dao.AuthenticationDao dao = new Dao.AuthenticationDao())
                {
                    return dao.GetUserInfo(language, user);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
