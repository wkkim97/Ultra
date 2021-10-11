using Bayer.Ultra.Framework.Common.Dto.Approval;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bayer.Ultra.BSL.Approval.Mgr
{
    public class ConsultingMeetingMgr : Framework.Database.MgrBase
    {
        public void MergeConsultingMeeting(DTO_EVENT_CONSULTING_MEETING consulting)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.ConsultingMeetingDao dao = new Dao.ConsultingMeetingDao())
                    {
                        dao.MergeConsultingMeeting(consulting);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTO_EVENT_CONSULTING_MEETING SelectConsultingMeeting(string processID)
        {
            try
            {
                using (Dao.ConsultingMeetingDao dao = new Dao.ConsultingMeetingDao())
                {
                    return dao.SelectConsultingMeeting(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
