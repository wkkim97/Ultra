using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Bayer.Ultra.Framework.Common.Dto.Approval;

namespace Bayer.Ultra.BSL.Excel.Mgr
{
    public class FileDownMgr
    {

        public string GetDownloadFileFullPathNameSRM(string fileName, string userId, string strProcessID, List<DTO_MODULE_PARTICIPANTS> dtoParticipantsList)
        {
            try
            {
                using (Dao.FileHandler dao = new Dao.FileHandler())
                {
                    return dao.CreateExcelFileSRM(fileName, userId, strProcessID, dtoParticipantsList);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string GetDownloadFileFullPathNameAttendeesList(string fileName, string userId, string strProcessID, List<EventAgendaSummaryDto> dtoAgendaRoleSummary, List<DTO_MODULE_PARTICIPANTS> dtoParticipantsList, DTO_EVENT_ATTENDEES_LIST_INFO dtoEventAttendeesListInfo)
        {
            try
            {
                String username_id = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                using (Dao.FileHandler dao = new Dao.FileHandler())
                {
                    return dao.CreateExcelFileAttendeesList(fileName, userId, strProcessID, dtoAgendaRoleSummary, dtoParticipantsList, dtoEventAttendeesListInfo);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
