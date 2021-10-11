using Bayer.Ultra.Framework.Common.Dto.Approval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using Bayer.Ultra.Framework;

namespace Bayer.Ultra.BSL.Approval.Mgr
{
    public class CongressEventMgr : Framework.Database.MgrBase
    {
        public void MergeCongressEvent(DTO_EVENT_CONGRESS Congress)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.CongressEventDao dao = new Dao.CongressEventDao())
                    {
                        dao.MergeCongressEvent(Congress);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTO_EVENT_CONGRESS SelectCongressEvent(string processID)
        {
            try
            {
                using (Dao.CongressEventDao dao = new Dao.CongressEventDao())
                {
                    return dao.SelectCongressEvent(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
