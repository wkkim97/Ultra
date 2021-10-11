using Bayer.Ultra.Framework.Common.Dto.Approval;
using Bayer.Ultra.Framework.Common.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Approval.Mgr
{
    public class _ApprovalMgr_Nx : Framework.Database.MgrBase
    {
        public string GetNewProcessID()
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.GetNewProcessID();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region [ SelectApprovalLine - 결재라인 ]
        public List<DTO_APPROVAL_LINE> SelectApprovalLine(string eventID, string userID, string processID)
        {
            try
            {
                String username_id = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectApprovalLine(eventID, userID, processID);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Home 결재 건수 조회
        public List<HomeCountSummaryDto> SelectHomeEventCountSummary(string requester)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectHomeEventCountSummary(requester);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region Home 결재 정보 조회
        public List<HomeNotSubmittedDto> SelectHomeNotSubmitted(string requester)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectHomeNotSubmitted(requester);
                }
            }
            catch
            {
                throw;
            }
        }

        public List<HomeApprovalQueueDto> SelectHomeApprovalQueue(string currentApp)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectHomeApprovalQueue(currentApp);
                }
            }
            catch
            {
                throw;
            }
        }

        public List<HomePendingApprovalDto> SelectHomePendingApproval(string requester)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectHomePendingApproval(requester);
                }
            }
            catch
            {
                throw;
            }
        }

        public List<Home3TimeVisitsDto> SelectHome3TimeVisits(object reference_Date)
        {
            throw new NotImplementedException();
        }

        public List<HomeDelegationDto> SelectHomeDelegation(string userID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectHomeDelegation(userID);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region [ SelectHome3TimeVisits ]
        public List<Home3TimeVisitsDto> SelectHome3TimeVisits(string visitDate)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectHome3TimeVisits(visitDate);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region [ Home 화면 From CRM Data]
        public List<HomeFromCRMDataDto> SelectHomeFromCRMData(string userID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectHomeFromCRMData(userID);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region [ Home 의사 방문 목록 조회 ]
        public List<HomeVisitListDto> SelectHomeVisitList(string hcp, string visitDate)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectHomeVisitList(hcp, visitDate);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [ Admin 의사 방문 목록 조회 ]
        public List<DTO_EVENT_BYHCP> SelectEventbyHCP(string hcp, string year)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectEventbyHCP(hcp, year);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [ SelectProcessApprovalStatusByUser - 사용자별 결재 상태조회 ]
        public List<DTO_PROCESS_APPROVAL_USER_STATUS> SelectProcessApprovalStatusByUser(string processID, string userID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectProcessApprovalStatusByUser(processID, userID);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region [ GetProcessEventStatus ]
        public string GetProcessEventStatus(string processID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.GetProcessEventStatus(processID);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region [ SelectApprovalCompletedList ]
        public List<DTO_PROCESS_COMPLETED_LIST> SelectApprovalCompletedList(string processStatus, string userid, string searchType, string searchText, DateTime? doc_Start, DateTime? doc_End, DateTime? evt_Start, DateTime? evt_End)
        {

            using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
            {
                if (processStatus.Equals("PaymentCompleted"))
                    return dao.SelectApprovalPaymentCompletedList(processStatus, userid, searchType, searchText, doc_Start, doc_End, evt_Start, evt_End);
                else
                    return dao.SelectApprovalCompletedList(processStatus, userid, searchType, searchText, doc_Start, doc_End, evt_Start, evt_End);
            }
        }
        #endregion

        #region [ SelectApprovalRejectList ]
        public List<DTO_PROCESS_REJECT_LIST> SelectApprovalRejectList(string reqid, string searchType, string searchText, DateTime? doc_Start, DateTime? doc_End, DateTime? evt_Start, DateTime? evt_End)
        {

            using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
            {
                return dao.SelectApprovalRejectList(reqid, searchType, searchText, doc_Start, doc_End, evt_Start, evt_End);
            }
        }
        #endregion

        #region [ SelectApprovalWithdrawList ]
        public List<DTO_PROCESS_WITHDRAW_LIST> SelectApprovalWithdrawList(string reqid, string searchType, string searchText, DateTime? doc_Start, DateTime? doc_End, DateTime? evt_Start, DateTime? evt_End)
        {

            using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
            {
                return dao.SelectApprovalWithdrawList(reqid, searchType, searchText, doc_Start, doc_End, evt_Start, evt_End);
            }
        }
        #endregion

        #region [ SelectApprovalCanceledList ]

        public List<DTO_PROCESS_COMPLETED_LIST> SelectApprovalCanceledList(string processStatus, string userid, string searchType, string searchText, DateTime? doc_Start, DateTime? doc_End, DateTime? evt_Start, DateTime? evt_End)
        {

            using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
            {
                return dao.SelectApprovalCanceledList(processStatus, userid, searchType, searchText, doc_Start, doc_End, evt_Start, evt_End);
            }
        }

        #endregion


        #region [  SelectApprovalAdminList ]
        public List<DTO_PROCESS_COMPLETED_LIST> SelectApprovalAdminList(string userid, string searchType, string searchText, DateTime? doc_Start, DateTime? doc_End, DateTime? evt_Start, DateTime? evt_End)
        {

            using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
            {
                return dao.SelectApprovalAdminList(userid, searchType, searchText, doc_Start, doc_End, evt_Start, evt_End);
            }
        } 
        #endregion


        #region [ SelectProcessApproverList - 결재라인 조회(Approver,Recipient,Reviewer) ]
        public List<DTO_PROCESS_APPROVAL_LIST> SelectProcessApproverList(string processID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectProcessApproverList(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public DTO_PROCESS_EVENT SelectProcessEvent(string processID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectProcessEvent(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region [ Process Auth User Check ]
        public bool IsApprovalProcessUser(string processId, string userid)
        {
            string retValue = string.Empty;
            bool bretValue = false;
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    retValue = dao.IsApprovalProcessUser(processId, userid);
                    if (retValue.ToUpper().Equals("TRUE"))
                        bretValue = true;
                    else
                        bretValue = false;
                }
                return bretValue;
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region [ SelectDocLogList - 결재관련 로그(Comment)정보]
        public List<DTO_LOG_EVENT> SelectLogEventList(string processID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectLogEventList(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        #endregion

        #region [ SelectEventAttachFile - 이벤트 첨부파일 조회 ]
        public List<DTO_EVENT_ATTACH_FILES> SelectEventAttachFiles(string processID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectEventAttachFiles(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [ SelectEventAttachFile - 이벤트 IDX 별 첨부파일 조회 ]
        public List<DTO_EVENT_ATTACH_FILES> SelectEventAttachFilesIdxs(string processID, string idxs)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectEventAttachFilesIdxs(processID, idxs);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [ Cost Plan ]

        public List<DTO_MODULE_COST_PLAN> SelectCostPlan(string processID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectCostPlan(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ Praticipants ]

        public List<DTO_MODULE_PARTICIPANTS> SelectParticipants(string processID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectParticipants(processID);
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
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectParticipantsByEventKey(eventKey);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTO_EVENT_ATTENDEES_LIST_INFO SelectEventAttendeesListInfo(string processID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectEventAttendeesListInfo(processID);
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
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectParticipantsByContract(impactNo, processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ Agenda ]

        public List<DTO_MODULE_AGENDA> SelectAgenda(string processID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectAgenda(processID);
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
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectAgendaRole(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MODULE_PARTICIPANTS> SelectAgendaParticipants(string processID, int participantIDX)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectAgendaParticipants(processID, participantIDX);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }


        public DTO_MODULE_AGENDA_ROLE SelectAgendaRoleInfo(string processID, int agendaIDX, int agendaRoleIDX)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectAgendaRoleInfo(processID, agendaIDX, agendaRoleIDX);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ModuleAgendaRuleDto SelectAgendaRolePrint(string processID, int agendaIDX, int agendaRoleIDX)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectAgendaRoleInfoPrint(processID, agendaIDX, agendaRoleIDX);
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
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectAgendaRoleAmountForHCP(hcpCode, processID, roleType, role);
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
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectAgendaRoleSummary(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [ Payment] 

        public List<EventPaymentByHCPDto> SelectEventPaymentByHCP(string processID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectEventPaymentByHCP(processID);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<EventPaymentPlanActualDto> SelectEventPaymentPlanActual(string processID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectEventPaymentPlanActual(processID);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTO_PAYMENT_UPLOAD_SRM_TOTAL GetEventPaymentUploadSRMTotal(string poNumber)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.GetEventPaymentUploadSRMTotal(poNumber);
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
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectEventPaymentUploadSRMHistory(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_PAYMENT_UPLOAD_SRM> SelectEventPaymentSavedSRM(string processID, string poNumber)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectEventPaymentSavedSRM(processID, poNumber);
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
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectEventPaymentLayer(processID);
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
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectEventPaymentLayerByHCP(processID, hcpCode, dataSource);
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
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectEventPaymentInputSRM(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_PAYMENT_UPLOAD_CONCUR> SelectEventPaymentUploadConcur(string userID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectEventPaymentUploadConcur(userID);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_PAYMENT_UPLOAD_YOURDOCES> SelectEventPaymentUploadYourDoces(string userID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectEventPaymentUploadYourDoces(userID);

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
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectPaymentConcurList(keyword);

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
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectConcurHistory(processId, concurId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_PAYMENT_ICC_MASTER> SelectICCMasterList(int iccId)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectICCMasterList(iccId);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<DTO_PAYMENT_ICC_ATTACH_FILES> SelectICCAttachFiles(int iccId, string idxs)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectICCAttachFiles(iccId, idxs);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region [ Delegation ]

        public List<DTO_MODULE_DELEGATION> SelectDelegation(string processID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectDelegation(processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        // version 1.0.5 Add Change History in Event Form
        public List<DTO_MODULE_CHANGE> SelectChange(string processID)
        {
            try
            {
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectChange(processID);
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
                using (Dao._ApprovalDao dao = new Dao._ApprovalDao())
                {
                    return dao.SelectDelegationSearchUser(processID, keyword);
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
