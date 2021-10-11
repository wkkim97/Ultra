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
    public class ScientificExchangedMeetingMgr : Framework.Database.MgrBase
    {
        public void MergeScientificExchangedMeeting(DTO_EVENT_SCIENTIFIC_MEETING SEMeeting, List<DTO_EVENT_SCIENTIFIC_MEETING_PRODUCT> products)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.ScientificExchangedMeetingDao dao = new Dao.ScientificExchangedMeetingDao())
                    {
                        dao.MergeScientificExchangedMeeting(SEMeeting);
                        dao.DeleteScientificExchangedMeetingProductAll(SEMeeting.PROCESS_ID, SEMeeting.UPDATER_ID);
                        foreach (DTO_EVENT_SCIENTIFIC_MEETING_PRODUCT product in products)
                        {
                            dao.InsertScientificExchangedMeetingProduct(product);
                        }

                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTO_EVENT_SCIENTIFIC_MEETING SelectScientificExchangedMeeting(string processID)
        {
            try
            {
                using (Dao.ScientificExchangedMeetingDao dao = new Dao.ScientificExchangedMeetingDao())
                {
                    return dao.SelectScientificExchangedMeeting(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_EVENT_SCIENTIFIC_MEETING_PRODUCT> SelectScientificExchangedMeetingProducts(string processID)
        {
            try
            {
                using (Dao.ScientificExchangedMeetingDao dao = new Dao.ScientificExchangedMeetingDao())
                {
                    return dao.SelectScientificExchangedMeetingProducts(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
