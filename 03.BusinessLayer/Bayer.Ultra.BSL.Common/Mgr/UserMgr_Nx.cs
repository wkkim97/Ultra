using Bayer.Ultra.Framework.Common.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Common.Mgr
{
    public class UserMgr_Nx : Framework.Database.MgrBase
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
                using (Dao.UserDao dao = new Dao.UserDao())
                {
                    return dao.SelectApprovalUserList(UserName);
                }
            }
            catch
            {
                throw;
            }
        }

        public List<UserAutocompleteDto> SelectUserAutocompleteList(string keyword)
        {
            try
            {
                using (Dao.UserDao dao = new Dao.UserDao())
                {
                    return dao.SelectUserAutocompleteList(keyword);
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
                using (Dao.UserDao dao = new Dao.UserDao())
                {
                    return dao.SelectEmployeeParticipants(keyword, processID);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
