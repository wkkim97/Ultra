using Bayer.Ultra.Framework.Common.Dto.Approval;
using Bayer.Ultra.Framework.Common.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;


namespace Bayer.Ultra.WcfBase
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public partial class UltraEvent : IUltraEvent
    {
        
        public List<HomeNotSubmittedDto> SelectHomeNotSubmitted(string requester)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectHomeNotSubmitted(requester);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HomeApprovalQueueDto> SelectHomeApprovalQueue(string currentApp)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectHomeApprovalQueue(currentApp);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HomePendingApprovalDto> SelectHomePendingApproval(string requester)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectHomePendingApproval(requester);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HomeCountSummaryDto> SelectHomeEventCountSummary(string requester)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectHomeEventCountSummary(requester);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<Home3TimeVisitsDto> SelectHome3TimeVisits(string requester, string visitDate)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectHome3TimeVisits(visitDate);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HomeFromCRMDataDto> SelectHomeFromCRMData(string userID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectHomeFromCRMData(userID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HomeVisitListDto> SelectHomeVisitList(string hcp, string visitDate)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectHomeVisitList(hcp, visitDate);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_EVENT_BYHCP> SelectEventbyHCP(string hcp, string year)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectEventbyHCP(hcp, year);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HomeDelegationDto> SelectHomeDelegation(string userID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectHomeDelegation(userID);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_PROCESS_COMPLETED_LIST> SelectApprovalCompletedList(string processStatus, string userID, string searchType, string searchText, string doc_Start, string doc_End, string evt_Start, string evt_End)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    string strSearchType = string.Empty;
                    string strSearchText = string.Empty;
                    DateTime? dtDocStart = null;
                    DateTime? dtDocEnd = null;
                    DateTime? dtEvtStart = null;
                    DateTime? dtEvtEnd = null;

                    if (!string.IsNullOrEmpty(searchType)) strSearchType = searchType;
                    if (!string.IsNullOrEmpty(searchText)) strSearchText = searchText;
                    if (!string.IsNullOrEmpty(doc_Start)) dtDocStart = Convert.ToDateTime(doc_Start);
                    if (!string.IsNullOrEmpty(doc_End)) dtDocEnd = Convert.ToDateTime(doc_End).AddDays(1);
                    if (!string.IsNullOrEmpty(evt_Start)) dtEvtStart = Convert.ToDateTime(evt_Start);
                    if (!string.IsNullOrEmpty(evt_End)) dtEvtEnd = Convert.ToDateTime(evt_End).AddDays(1);

                    return mgr.SelectApprovalCompletedList(processStatus, userID, strSearchType, strSearchText, dtDocStart, dtDocEnd, dtEvtStart, dtEvtEnd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_PROCESS_REJECT_LIST> SelectApprovalRejectList(string reqid, string searchType, string searchText, string doc_Start, string doc_End, string evt_Start, string evt_End)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    string strSearchType = string.Empty;
                    string strSearchText = string.Empty;
                    DateTime? dtDocStart = null;
                    DateTime? dtDocEnd = null;
                    DateTime? dtEvtStart = null;
                    DateTime? dtEvtEnd = null;

                    if (!string.IsNullOrEmpty(searchType)) strSearchType = searchType;
                    if (!string.IsNullOrEmpty(searchText)) strSearchText = searchText;
                    if (!string.IsNullOrEmpty(doc_Start)) dtDocStart = Convert.ToDateTime(doc_Start);
                    if (!string.IsNullOrEmpty(doc_End)) dtDocEnd = Convert.ToDateTime(doc_End).AddDays(1);
                    if (!string.IsNullOrEmpty(evt_Start)) dtEvtStart = Convert.ToDateTime(evt_Start);
                    if (!string.IsNullOrEmpty(evt_End)) dtEvtEnd = Convert.ToDateTime(evt_End).AddDays(1);

                    return mgr.SelectApprovalRejectList(reqid, strSearchType, strSearchText, dtDocStart, dtDocEnd, dtEvtStart, dtEvtEnd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_PROCESS_WITHDRAW_LIST> SelectApprovalWithdrawList(string reqid, string searchType, string searchText, string doc_Start, string doc_End, string evt_Start, string evt_End)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    string strSearchType = string.Empty;
                    string strSearchText = string.Empty;
                    DateTime? dtDocStart = null;
                    DateTime? dtDocEnd = null;
                    DateTime? dtEvtStart = null;
                    DateTime? dtEvtEnd = null;

                    if (!string.IsNullOrEmpty(searchType)) strSearchType = searchType;
                    if (!string.IsNullOrEmpty(searchText)) strSearchText = searchText;
                    if (!string.IsNullOrEmpty(doc_Start)) dtDocStart = Convert.ToDateTime(doc_Start);
                    if (!string.IsNullOrEmpty(doc_End)) dtDocEnd = Convert.ToDateTime(doc_End).AddDays(1);
                    if (!string.IsNullOrEmpty(evt_Start)) dtEvtStart = Convert.ToDateTime(evt_Start);
                    if (!string.IsNullOrEmpty(evt_End)) dtEvtEnd = Convert.ToDateTime(evt_End).AddDays(1);

                    return mgr.SelectApprovalWithdrawList(reqid, strSearchType, strSearchText, dtDocStart, dtDocEnd, dtEvtStart, dtEvtEnd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_PROCESS_COMPLETED_LIST> SelectApprovalCanceledList(string processStatus, string userID, string searchType, string searchText, string doc_Start, string doc_End, string evt_Start, string evt_End)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    string strSearchType = string.Empty;
                    string strSearchText = string.Empty;
                    DateTime? dtDocStart = null;
                    DateTime? dtDocEnd = null;
                    DateTime? dtEvtStart = null;
                    DateTime? dtEvtEnd = null;

                    if (!string.IsNullOrEmpty(searchType)) strSearchType = searchType;
                    if (!string.IsNullOrEmpty(searchText)) strSearchText = searchText;
                    if (!string.IsNullOrEmpty(doc_Start)) dtDocStart = Convert.ToDateTime(doc_Start);
                    if (!string.IsNullOrEmpty(doc_End)) dtDocEnd = Convert.ToDateTime(doc_End).AddDays(1);
                    if (!string.IsNullOrEmpty(evt_Start)) dtEvtStart = Convert.ToDateTime(evt_Start);
                    if (!string.IsNullOrEmpty(evt_End)) dtEvtEnd = Convert.ToDateTime(evt_End).AddDays(1);

                    return mgr.SelectApprovalCanceledList(processStatus, userID, strSearchType, strSearchText, dtDocStart, dtDocEnd, dtEvtStart, dtEvtEnd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_PROCESS_COMPLETED_LIST> SelectApprovalAdminList(string userID, string searchType, string searchText, string doc_Start, string doc_End, string evt_Start, string evt_End)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    string strSearchType = string.Empty;
                    string strSearchText = string.Empty;
                    DateTime? dtDocStart = null;
                    DateTime? dtDocEnd = null;
                    DateTime? dtEvtStart = null;
                    DateTime? dtEvtEnd = null;

                    if (!string.IsNullOrEmpty(searchType)) strSearchType = searchType;
                    if (!string.IsNullOrEmpty(searchText)) strSearchText = searchText;
                    if (!string.IsNullOrEmpty(doc_Start)) dtDocStart = Convert.ToDateTime(doc_Start);
                    if (!string.IsNullOrEmpty(doc_End)) dtDocEnd = Convert.ToDateTime(doc_End).AddDays(1);
                    if (!string.IsNullOrEmpty(evt_Start)) dtEvtStart = Convert.ToDateTime(evt_Start);
                    if (!string.IsNullOrEmpty(evt_End)) dtEvtEnd = Convert.ToDateTime(evt_End).AddDays(1);

                    return mgr.SelectApprovalAdminList(userID, strSearchType, strSearchText, dtDocStart, dtDocEnd, dtEvtStart, dtEvtEnd);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_PROCESS_APPROVAL_LIST> SelectApproverList(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    if (string.IsNullOrEmpty(processID)) processID = string.Empty;
                    return mgr.SelectProcessApproverList(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        #region [ Excel File Down/Up ]
        public string ExcelFileDown(string fileName, string userId, string strProcessID)
        {
            string strReturnDestFileName = string.Empty;

            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    // ParticipantsList 를 조회하여 Excel 파일에 리스트로 입력
                    List<DTO_MODULE_PARTICIPANTS> dtoParticipantsList = null;

                    dtoParticipantsList = mgr.SelectParticipants(strProcessID);

                    // 참여자가 존재하는 경우 Excel 파일에 참여자 정보 SAVE
                    if (dtoParticipantsList.Count > 0)
                    {
                        BSL.Excel.Mgr.FileDownMgr Mgr = new BSL.Excel.Mgr.FileDownMgr();
                        strReturnDestFileName = Mgr.GetDownloadFileFullPathNameSRM(fileName, userId, strProcessID, dtoParticipantsList);
                    }
                    else  // 참여자가 존재하지 않는 경우
                    {
                        strReturnDestFileName = string.Empty;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

            return strReturnDestFileName;
        }


        public string ExcelFileDownAttendeeList(string fileName, string userId, string strProcessID)
        {
            string strReturnDestFileName = string.Empty;

            // TEST
            //strProcessID = "P000000707"; // "P000000033";

            try
            {


                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    List<EventAgendaSummaryDto> dtoAgendaRoleSummary = null;
                    List<DTO_MODULE_PARTICIPANTS> dtoParticipantsList = null;
                    DTO_EVENT_ATTENDEES_LIST_INFO dtoEventAttendeesListInfo = null;

                    dtoAgendaRoleSummary = mgr.SelectAgendaRoleSummary(strProcessID);
                    dtoParticipantsList = mgr.SelectParticipants(strProcessID);
                    dtoEventAttendeesListInfo = mgr.SelectEventAttendeesListInfo(strProcessID);

                    // 참여자가 존재하는 경우 Excel 파일에 SAVE
                    if (dtoParticipantsList.Count > 0)
                    {
                        BSL.Excel.Mgr.FileDownMgr Mgr = new BSL.Excel.Mgr.FileDownMgr();
                        strReturnDestFileName = Mgr.GetDownloadFileFullPathNameAttendeesList(fileName, userId, strProcessID, dtoAgendaRoleSummary, dtoParticipantsList, dtoEventAttendeesListInfo);
                    }
                    else  // 참여자가 존재하지 않는 경우
                    {
                        strReturnDestFileName = string.Empty;
                    }

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
            }

            return strReturnDestFileName;
        }

        #endregion



        #region [ Cost Plan ] 

        public string MergeCostPlan(DTO_MODULE_COST_PLAN dto)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    return mgr.MergeCostPlan(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MergeCostPlanList(List<DTO_MODULE_COST_PLAN> list)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.MergeCostPlanList(list);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MODULE_COST_PLAN> SelectCostPlan(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    if (string.IsNullOrEmpty(processID)) processID = string.Empty;
                    return mgr.SelectCostPlan(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteCostPlan(string processID, int costPlanIDX, string updaterID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.DeleteCostPlan(processID, costPlanIDX, updaterID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ Participants ]

        public string InsertParticipants(List<DTO_MODULE_PARTICIPANTS> participants)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    return mgr.InsertParticipant(participants);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MODULE_PARTICIPANTS> SelectParticipants(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    if (string.IsNullOrEmpty(processID)) processID = string.Empty;
                    return mgr.SelectParticipants(processID);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MODULE_PARTICIPANTS> SelectParticipantsByEventKey(string eventKey)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    if (string.IsNullOrEmpty(eventKey)) return null;
                    return mgr.SelectParticipantsByEventKey(eventKey);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateParticipantStatus(string processID, int[] indexes, string isAttended, string updaterID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.UpdateParticipantStatus(processID, indexes, isAttended, updaterID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteParticipant(string processID, int[] indexes, string updaterID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.DeleteParticipant(processID, indexes, updaterID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HealthCareProviderDto> SelectParticipantsByContract(string impactNo, string processID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectParticipantsByContract(impactNo, processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ Agenda ]

        public string MergeAgenda(DTO_MODULE_AGENDA agenda)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    return mgr.MergeAgenda(agenda);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAgenda(string processID, string agendaIDX, string updaterID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {

                    mgr.DeleteAgenda(processID, Convert.ToInt32(agendaIDX), updaterID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MODULE_AGENDA> SelectAgenda(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectAgenda(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public int MergeAgendaRole(DTO_MODULE_AGENDA_ROLE agendaRole)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    return mgr.MergeAgendaRole(agendaRole);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteAgendaRole(string processID, string agendaIDX, string agendaRoleIDX, string updaterID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.DeleteAgendaRole(processID, Convert.ToInt32(agendaIDX), Convert.ToInt32(agendaRoleIDX), updaterID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UpdateMaterialCode(string processID, string agendaIDX, string mCode, string updaterID, string costcenter, string sap_no, string krpia)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.UpdateMaterialcode(processID, agendaIDX, mCode, updaterID, costcenter, sap_no, krpia);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<DTO_MODULE_AGENDA_ROLE> SelectAgendaRole(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectAgendaRole(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MODULE_PARTICIPANTS> SelectAgendaParticipants(string processID, string participantIDX)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    int idx = Convert.ToInt32(participantIDX);
                    return mgr.SelectAgendaParticipants(processID, idx);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTO_MODULE_AGENDA_ROLE SelectAgendaRoleInfo(string processID, string agendaIDX, string agendaRoleIDX)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectAgendaRoleInfo(processID, Convert.ToInt32(agendaIDX), Convert.ToInt32(agendaRoleIDX));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ModuleAgendaRuleDto SelectAgendaRoleInfoPrint(string processID, string agendaIDX, string agendaRoleIDX)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectAgendaRolePrint(processID, Convert.ToInt32(agendaIDX), Convert.ToInt32(agendaRoleIDX));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MODULE_AGENDA_ROLE> SelectAgendaRoleAmountForHCP(string hcpCode, string processID, string roleType, string role)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectAgendaRoleAmountForHCP(hcpCode, processID, roleType, role);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventAgendaSummaryDto> SelectAgendaRoleSummary(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectAgendaRoleSummary(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [ Payment ]

        public List<EventPaymentByHCPDto> SelectEventPaymentByHCP(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectEventPaymentByHCP(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventPaymentPlanActualDto> SelectEventPaymentPlanActual(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectEventPaymentPlanActual(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_PAYMENT_UPLOAD_SRM> ReadEventPaymentUploadExcel(string processID, string userID, string filePath)
        {
            try
            {
                List<DTO_PAYMENT_UPLOAD_SRM> list;
                using (BSL.Excel.Dao.SRMReader reader = new BSL.Excel.Dao.SRMReader())
                {
                    list = reader.ReadPaymentSRM(processID, userID, filePath);

                }


                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    return mgr.InsertEventPaymentUploadSRM(processID, userID, list);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_PAYMENT_UPLOAD_SRM> SelectEventPaymentSavedSRM(string processID, string poNumber)
        {
            using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
            {
                return mgr.SelectEventPaymentSavedSRM(processID, poNumber);
            }
        }

        public DTO_PAYMENT_UPLOAD_SRM_TOTAL GetEventPaymentUploadSRMTotal(string poNumber)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.GetEventPaymentUploadSRMTotal(poNumber);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertEventpaymentUploadSRMHistory(string processID, string userID, DTO_PAYMENT_UPLOAD_SRM_HISTORY history)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.InsertEventPaymentUploadSRMHistory(processID, userID, history);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_PAYMENT_UPLOAD_SRM_HISTORY> SelectEventPaymentUploadSRMHistory(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectEventPaymentUploadSRMHistory(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteEventPaymentUploadSRMHistory(string processID, string poNumber, string updaterID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.DeleteEventPaymentUploadSRMHistory(processID, poNumber, updaterID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventPaymentLayerDto> SelectEventPaymentLayer(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectEventPaymentLayer(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventPaymentLayerDto> SelectEventPaymentLayerByHCP(string processID, string hcpCode, string dataSource)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectEventPaymentLayerByHCP(processID, hcpCode, dataSource);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MergeEventPaymentInputSRM(DTO_PAYMENT_INPUT_SRM srm)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.MergeEventPaymentInputSRM(srm);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_PAYMENT_INPUT_SRM> SelectEventPaymentInputSRM(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectEventPaymentInputSRM(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteEventPaymentInputSRM(string processID, string srmIDX, string updaterID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.DeleteEventPaymentInputSRM(processID, Convert.ToInt32(srmIDX), updaterID);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string InsertEventPaymentUploadConcur(string userID, string filePath)
        {
            string excellistcount = "";
            try
            {
                List<DTO_PAYMENT_UPLOAD_CONCUR> list;
                using (BSL.Excel.Eao.ConcurReader reader = new BSL.Excel.Eao.ConcurReader())
                {
                    list = reader.ReadPaymentConcur(userID, filePath);
                }
                excellistcount = list.Count.ToString();
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.InsertEventPaymentUploadConcur(userID, list);
                }
                return excellistcount;

            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                
            }
           // return excellistcount;
        }

        public string InsertEventPaymentUploadYourDoces(string userID, string filePath)
        {
            string excellistcount = "";
            try
            {
                List<DTO_PAYMENT_UPLOAD_YOURDOCES> list;
                using (BSL.Excel.Eao.YourDocesReader reader = new BSL.Excel.Eao.YourDocesReader())
                {
                    list = reader.ReadPaymentYourdoces(userID, filePath);
                }
                excellistcount = list.Count.ToString();

                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.InsertEventPaymentUploadYourDoces(userID, list);
                }
                return excellistcount;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DTO_PAYMENT_UPLOAD_CONCUR> ReadEventPaymentUploadConcur(string userID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectEventPaymentUploadConcur(userID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_PAYMENT_UPLOAD_YOURDOCES> ReadEventPaymentUploadYourDoces(string userID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectEventPaymentUploadYourDoces(userID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertEventModulePaymentFromConcur(string creatorID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.InsertEventModulePaymentFromConcur(creatorID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void InsertEventModulePaymentFromYourDoces(string creatorID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.InsertEventModulePaymentFromYourDoces(creatorID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DTO_MODULE_PAYMENT_CONCUR> SelectPaymentConcurList(string keyword)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    if (string.IsNullOrEmpty(keyword)) keyword = string.Empty;
                    return mgr.SelectPaymentConcurList(keyword);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdatePaymentConcur(DTO_MODULE_PAYMENT_CONCUR concur)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.UpdatePaymentConcur(concur);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MODULE_PAYMENT_CONCUR> SelectConcurHistory(string processId, string concurId)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectConcurHistory(processId, concurId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_PAYMENT_ICC_MASTER> SelectICCMasterList(string iccId)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    if (string.IsNullOrEmpty(iccId)) iccId = "0";
                    return mgr.SelectICCMasterList(Convert.ToInt32(iccId));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MergeICCMaster(DTO_PAYMENT_ICC_MASTER iccinfo, List<EventAttachFileResponseDto> attachFiles)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.MergeICCMaster(iccinfo, attachFiles);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_PAYMENT_ICC_ATTACH_FILES> SelectICCAttachFiles(string iccId, string idxs)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    if (string.IsNullOrEmpty(idxs)) idxs = "";
                    return mgr.SelectICCAttachFiles(Convert.ToInt32(iccId), idxs);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        // version 1.0.5 Add Change History in Event Form
        #region [Change]

        public List<DTO_MODULE_CHANGE> SelectChange(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectChange(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ Delegation ]

        public void MergeDelegation(List<DTO_MODULE_DELEGATION> delegation)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.MergeDelegation(delegation);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteDelegation(string processID, string[] userIDs, string updaterID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.DeleteDelegation(processID, userIDs, updaterID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MODULE_DELEGATION> SelectDelegation(string processID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectDelegation(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MODULE_SEARCH_USER> SelectDelegationSearchUser(string processID, string keyword)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Nx mgr = new BSL.Approval.Mgr._ApprovalMgr_Nx())
                {
                    return mgr.SelectDelegationSearchUser(processID, keyword);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [Change_Value]
        // version 1.0.5 Admin Event Page for change by DM Team

        public void UpdateChangeValue(string PROCESS_ID, string CATEGORY, string ADJUSTMENT_AREA, string OLD_VALUE, string NEW_VALUE, string REASON, string UPDATER_ID)
        {
            try
            {
                using (BSL.Approval.Mgr._ApprovalMgr_Tx mgr = new BSL.Approval.Mgr._ApprovalMgr_Tx())
                {
                    mgr.UpdateChangeValue(PROCESS_ID, CATEGORY, ADJUSTMENT_AREA, OLD_VALUE, NEW_VALUE, REASON, UPDATER_ID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

    }
}
