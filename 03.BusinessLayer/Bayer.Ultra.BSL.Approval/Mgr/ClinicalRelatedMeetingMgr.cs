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
    public class ClinicalRelatedMeetingMgr : Framework.Database.MgrBase
    {
        public void MergeClinicalRelatedMeeting(DTO_EVENT_CLINICAL_MEETING SEMeeting)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.ClinicalRelatedMeetingDao dao = new Dao.ClinicalRelatedMeetingDao())
                    {
                        dao.MergeClinicalRelatedMeeting(SEMeeting);
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTO_EVENT_CLINICAL_MEETING SelectClinicalRelatedMeeting(string processID)
        {
            try
            {
                using (Dao.ClinicalRelatedMeetingDao dao = new Dao.ClinicalRelatedMeetingDao())
                {
                    return dao.SelectClinicalRelatedMeeting(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
