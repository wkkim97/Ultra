using Bayer.Ultra.Framework.Common.Dto.Medical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bayer.Ultra.BSL.Medical.Mgr
{
    public class PmsMgr_Tx : Framework.Database.MgrBase
    { 
        public string ModifyPms(DTO_MEDICAL_PMS dto)
        {
            string retValue = string.Empty;
            string idx = string.Empty;
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.PmsDao dao = new Dao.PmsDao())
                    {
                        idx = dao.ModifyPMS(dto);
                        retValue = "OK";
                    }
                    scope.Complete();
                }
                InsertLog(idx);
            }
            catch 
            {
                retValue = "FAIL";
                throw;
            }

            return retValue;
        }

        public void InsertLog(string idx)
        { 
            try
            { 
                using (Dao.PmsDao dao = new Dao.PmsDao())
                {
                    dao.InsertpmsLog(idx); 
                }
                
            }
            catch{ }
        }
    }
}
