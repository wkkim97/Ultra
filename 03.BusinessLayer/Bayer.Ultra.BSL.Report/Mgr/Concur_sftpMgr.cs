using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Report.Mgr
{
    public class Concur_sftpMgr : Framework.Database.MgrBase
    {
        public void trasfersFTP_Concur(string yyyymm)
        {
            try
            {
                using (Dao.Concur_sftpDao dao = new Dao.Concur_sftpDao())
                {
                    dao.trasfersFTP_Concur(yyyymm);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}
