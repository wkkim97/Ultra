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
    public class AdventiseBoothMgr : Framework.Database.MgrBase
    {
        public void MergeAdventiseBooth(DTO_EVENT_ADVENTISEMENT Adventise)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.AdventiseBoothDao dao = new Dao.AdventiseBoothDao())
                    {
                        dao.MergeAdventiseBooth(Adventise);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTO_EVENT_ADVENTISEMENT SelectAdventiseBooth(string processID)
        {
            try
            {
                using (Dao.AdventiseBoothDao dao = new Dao.AdventiseBoothDao())
                {
                    return dao.SelectAdventiseBooth(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
