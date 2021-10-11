using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Common
{
    public class UserContext : Framework.Database.UltraDbContext
    {
        public const string USP_SELECT_APPROVAL_TARGET_USER_LIST = "[eManage].[dbo].[USP_SELECT_APPROVAL_TARGET_USER_LIST] @KEYWOARD";

        public const string USP_SELECT_USER_AUTOCOMPETE_LIST = "[eManage].[dbo].[USP_SELECT_USER_AUTOCOMPETE_LIST] @KEYWORD";

        public const string USP_SELECT_EMPLOYEE_PARTICIPANTS = "[dbo].[USP_SELECT_EMPLOYEE_PARTICIPANTS] @KEYWORD, @PROCESS_ID";
    }
}
