using Bayer.Ultra.Framework.Common.Dto.Approval;
using Bayer.Ultra.Framework.Common.Dto.Common;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Approval.Dao
{
    internal class _ApprovalDao : Framework.Database.DaoBase
    {
        #region [ GetNewProcessID ]
        public string GetNewProcessID()
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    return _context.Database.SqlQuery<string>(ApprovalContext.USP_CREATE_PROCESS_ID).FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region [ 결재라인 조회]
        public List<DTO_APPROVAL_LINE> SelectApprovalLine(string eventID, string userID, string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@EVENT_ID", eventID);
                    parameters[1] = new SqlParameter("@USER_ID", userID);
                    parameters[2] = new SqlParameter("@PROCESS_ID", processID);
                    var result = _context.Database.SqlQuery<DTO_APPROVAL_LINE>(ApprovalContext.USP_SELECT_APPROVAL_LINE, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region DeleteProcessApprove [ReCall시에 TB_PROCESS_APPROVE를 제거한다]
        /// <summary>
        /// ReCall시 기존 결재선(TB_PROCESS_APPROVE)에 있는 정보를 모두 제거한다.
        /// </summary>
        /// <param name="processid">프로세스ID</param>
        public void DeleteProcessApprove(string processid)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processid);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_DELETE_PROCESS_APPROVER, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region InsertProcessApprove 결재선 추가
        public void InsertProcessApprove(DTO_PROCESS_APPROVER add)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[14];
                    parameters[0] = new SqlParameter("@PROCESS_ID", add.PROCESS_ID);
                    parameters[1] = new SqlParameter("@APPROVAL_TYPE", add.APPROVAL_TYPE);
                    parameters[2] = new SqlParameter("@APPROVAL_SEQ", add.APPROVAL_SEQ);
                    parameters[3] = new SqlParameter("@APPROVER_TYPE", add.APPROVER_TYPE);
                    parameters[4] = new SqlParameter("@APPROVER_ID", add.APPROVER_ID);
                    parameters[5] = new SqlParameter("@APPROVER_ORG_NAME", add.APPROVER_ORG_NAME);
                    parameters[6] = new SqlParameter("@ABSENCE_APPROVER_ID", add.ABSENCE_APPROVER_ID);
                    parameters[7] = new SqlParameter("@ABSENCE_APPROVER_ORG_NAME", add.ABSENCE_APPROVER_ORG_NAME);
                    parameters[8] = new SqlParameter("@STATUS", add.STATUS);
                    parameters[9] = new SqlParameter("@SENT_MAIL", add.SENT_MAIL);
                    parameters[10] = new SqlParameter("@APPROVAL_DATE", (object)add.APPROVAL_DATE ?? DBNull.Value);
                    parameters[11] = new SqlParameter("@COMMENT", add.COMMENT);
                    parameters[12] = new SqlParameter("@CREATE_DATE", (object)add.UPDATE_DATE ?? DBNull.Value);
                    parameters[13] = new SqlParameter("@UPDATE_DATE", (object)add.UPDATE_DATE ?? DBNull.Value);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_INSERT_PROCESS_APPROVER, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region InsertProcessDocument [ TB_PROCESS_DOCUMENT 추가 ]
        /// <summary>
        /// 결재 문서 목록을 추가한다( TB_PROCESS_DOCUMENT )
        /// </summary>
        /// <param name="evt"></param>
        public void InsertProcessEvent(DTO_PROCESS_EVENT evt)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[15];
                    parameters[0] = new SqlParameter("@PROCESS_ID", evt.PROCESS_ID);
                    parameters[1] = new SqlParameter("@EVENT_ID", evt.EVENT_ID);
                    parameters[2] = new SqlParameter("@EVENT_NAME", evt.EVENT_NAME);
                    parameters[3] = new SqlParameter("@SUBJECT", evt.SUBJECT);
                    parameters[4] = new SqlParameter("@START_DATE", evt.START_DATE);
                    parameters[5] = new SqlParameter("@EVENT_KEY", (object)evt.EVENT_KEY ?? DBNull.Value);
                    parameters[6] = new SqlParameter("@PROCESS_STATUS", evt.PROCESS_STATUS);
                    parameters[7] = new SqlParameter("@REQUEST_DATE", (object)evt.REQUEST_DATE ?? DBNull.Value);
                    parameters[8] = new SqlParameter("@COMPANY_CODE", evt.COMPANY_CODE);
                    parameters[9] = new SqlParameter("@REQUESTER_ID", evt.REQUESTER_ID);
                    parameters[10] = new SqlParameter("@CURRENT_APPROVER", evt.CURRENT_APPROVER);
                    parameters[11] = new SqlParameter("@FINAL_APPROVER", evt.FINAL_APPROVER);
                    parameters[12] = new SqlParameter("@REJECTED_PROCESS_ID", evt.REJECTED_PROCESS_ID);
                    parameters[13] = new SqlParameter("@CREATE_DATE", (object)evt.UPDATE_DATE ?? DBNull.Value);
                    parameters[14] = new SqlParameter("@UPDATE_DATE", (object)evt.UPDATE_DATE ?? DBNull.Value);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_INSERT_PROCESS_EVENT, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region InsertProcessApproveAddtional [TB_PROCESS_APPROVER_ADDTIONAL 테이블에 Recipient 를 추가]
        /// <summary>
        /// TB_PROCESS_APPROVER_ADDTIONAL 테이블에 Recipient 를 추가
        /// </summary>
        /// <param name="add">추가할 Recipient Item</param>
        public void InsertProcessApproveAddtional(DTO_PROCESS_APPROVER_ADDTIONAL add)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@PROCESS_ID", add.PROCESS_ID);
                    parameters[1] = new SqlParameter("@IDX", add.IDX);
                    parameters[2] = new SqlParameter("@APPROVAL_TYPE", add.APPROVAL_TYPE);
                    parameters[3] = new SqlParameter("@USER_ID", add.USER_ID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_INSERT_PROCESS_APPROVER_ADDTIONAL, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region DeleteProcessApproveAddtional [TB_PROCESS_APPROVER_ADDTIONAL 테이블에 Additional Approer(A)/Recipient(T) 를 삭제]
        /// <summary>
        /// TB_PROCESS_APPROVER_ADDTIONAL 테이블에 Recipient 를 삭제
        /// </summary>
        /// <param name="add">삭제할 PROCESS ID</param>
        public void DeleteProcessApproveAddtional(string processID, string approvalTYPE)
        {

            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@APPROVAL_TYPE", approvalTYPE);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_DELETE_PROCESS_APPROVER_ADDTIONAL, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }
        #endregion

        #region UpdateDocumentProcessStatus 문서테이블별 문서상태 변경
        /// <summary>
        /// 각 문서테이불에 프로세스ID별로 문서상태를 변경한다.<br/>
        /// 각 테이블에 상태, 변경일자, 변경자 업데이트 처리<br/>
        /// </summary>
        /// <param name="documentid">문서ID</param>
        /// <param name="processid">프로세스ID</param>
        /// <param name="processstatus">문서상태</param>
        /// <param name="userid">결재자ID</param>
        public void UpdateEventProcessStatus(string eventid, string processid, string processstatus, string userid)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@EVENT_ID", eventid);
                    parameters[1] = new SqlParameter("@PROCESS_ID", processid);
                    parameters[2] = new SqlParameter("@PROCESS_STATUS", processstatus);
                    parameters[3] = new SqlParameter("@APPROVER_ID", userid);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_UPDATE_EVENT_PROCESS_STATUS, parameters);
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
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_PROCESS_EVENT>(ApprovalContext.USP_SELECT_PROCESS_EVENT, parameters);
                    return result.FirstOrDefault();

                }
            }
            catch
            {
                throw;
            }
        }

        #region [ 결재 건수 조회 ] 

        public List<HomeCountSummaryDto> SelectHomeEventCountSummary(string requester)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@REQUESTER_ID", requester);

                    var result = _context.Database.SqlQuery<HomeCountSummaryDto>(ApprovalContext.USP_SELECT_HOME_EVENT_COUNT_SUMMARY, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region [ 결재정보 조회 ] 

        public List<HomeNotSubmittedDto> SelectHomeNotSubmitted(string requester)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@REQUESTER_ID", requester);

                    var result = _context.Database.SqlQuery<HomeNotSubmittedDto>(ApprovalContext.USP_SELECT_HOME_NOT_SUBMITTED, parameters);
                    return result.ToList();

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
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@CURRENT_APPROVER", currentApp);

                    var result = _context.Database.SqlQuery<HomeApprovalQueueDto>(ApprovalContext.USP_SELECT_HOME_APPROVAL_QUEUE, parameters);
                    return result.ToList();
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
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@REQUESTER_ID", requester);

                    var result = _context.Database.SqlQuery<HomePendingApprovalDto>(ApprovalContext.USP_SELECT_HOME_PENDING_APPROVAL, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<HomeDelegationDto> SelectHomeDelegation(string userID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@USER_ID", userID);

                    var result = _context.Database.SqlQuery<HomeDelegationDto>(ApprovalContext.USP_SELECT_HOME_DELEGATION, parameters);
                    return result.ToList();
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
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@VISIT_DATE", visitDate);

                    var result = _context.Database.SqlQuery<Home3TimeVisitsDto>(ApprovalContext.USP_SELECT_HOME_VISIT_COUNT, parameters);
                    return result.ToList();
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
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@USER_ID", userID);
                    var result = _context.Database.SqlQuery<HomeFromCRMDataDto>(ApprovalContext.USP_SELECT_HOME_CRM_DATA, parameters);
                    return result.ToList();
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
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@HCP", hcp);
                    parameters[1] = new SqlParameter("@VISIT_DATE", visitDate);

                    var result = _context.Database.SqlQuery<HomeVisitListDto>(ApprovalContext.USP_SELECT_HOME_HCP_INFO, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region [ Home 의사 방문 목록 조회 ]
        public List<DTO_EVENT_BYHCP> SelectEventbyHCP(string hcp, string year)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@HCP_NAME", hcp);
                    //parameters[1] = new SqlParameter("@VISIT_DATE", visitDate);

                    var result = _context.Database.SqlQuery<DTO_EVENT_BYHCP>(ApprovalContext.USP_SELECT_ALL_EVENT_BY_HCP_NAME, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region [ SelectProcessApprovalStatusByUser - 사용자별 결재 상태조회 ]
        public List<DTO_PROCESS_APPROVAL_USER_STATUS> SelectProcessApprovalStatusByUser(string processID, string userID)
        {

            using (_context = new ApprovalContext())
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                parameters[1] = new SqlParameter("@APPROVER_ID", userID);

                var result = _context.Database.SqlQuery<DTO_PROCESS_APPROVAL_USER_STATUS>(ApprovalContext.USP_SELECT_PROCESS_APPROVER_USER, parameters);

                return result.ToList();
            }
        }
        #endregion

        #region [ GetProcessEventStatus ]
        public string GetProcessEventStatus(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<string>(ApprovalContext.USP_SELECT_PROCESS_EVENT_STATUS, parameters);

                    return result.First();
                }
            }
            catch
            {
                return string.Empty;
            }
        }
        #endregion

        #region UpdateDocumentProcessStatus [결재문서목록 테이블(TB_PROCESS_DOCUMENT) 상태변경]
        /// <summary>
        /// 결재문서목록 테이블(TB_PROCESS_DOCUMENT)에 문서상태와 다음결재자 정보를 변경
        /// </summary>
        /// <param name="processid">프로세스아이디</param>
        /// <param name="userid">현결재자아이디</param>
        /// <param name="processstatus">문서상태</param>
        public void UpdateProcessEventStatus(string processid, string userid, string processstatus)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processid);
                    parameters[1] = new SqlParameter("@APPROVER_ID", userid);
                    parameters[2] = new SqlParameter("@PROCESS_STATUS", processstatus);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_UPDATE_PROCESS_EVENT_STATUS, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        public void UpdateProcessEventCompleted(string processid, string userid, string processstatus)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processid);
                    parameters[1] = new SqlParameter("@APPROVER_ID", userid);
                    parameters[2] = new SqlParameter("@PROCESS_STATUS", processstatus);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_UPDATE_PROCESS_EVENT_STATUS_FOR_SKIP_APPROVAL, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region UpdateDocumentProcessStatus [결재선변경(TB_PROCESS_APPROVER) APPROVER STATUS 변경]
        /// <summary>
        /// 결재선에 현결재자 프로세스상태를 변경한다.
        /// </summary>
        /// <param name="processid">프로세스아이디</param>
        /// <param name="userid">현결재자</param>
        /// <param name="status">프로세스상태</param>
        /// <param name="comment">커멘트</param>
        public void UpdateProcessApproverStatus(string processid, string userid, string status, string comment)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processid);
                    parameters[1] = new SqlParameter("@APPROVER_ID", userid);
                    parameters[2] = new SqlParameter("@STATUS", status);
                    parameters[3] = new SqlParameter("@COMMENT", comment);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_UPDATE_PROCESS_APPROVER, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region GetFinalApprovalIDLine [ TB_PROCESS_APPROVER에서 FINAL_APPROVER USERID를 조회한다 ]
        /// <summary>
        ///  TB_PROCESS_APPROVER에서 FINAL_APPROVER USERID를 조회한다
        /// </summary>
        /// <param name="processid">프로세스 ID</param>
        /// <returns>FINAL_APPROVER ID</returns>
        public string GetFinalApprovalIDLine(string processid)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processid);

                    return _context.Database.SqlQuery<string>(ApprovalContext.USP_SELECT_EVENT_FINAL_APPROVER_ID_LINE, parameters).First().ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region UpdateProcessCompleted [최종결재완료시 수행 프로세스]
        /// <summary>
        /// 최종 결재자가 결재완료를 수행했을 경우 업데이트를 수행한다.
        /// </summary>
        /// <param name="eventID">문서ID</param>
        /// <param name="processID">프로세스ID</param>
        /// <param name="processStatus">문서상태</param>
        /// <param name="approverID">결재자ID</param>
        public void UpdateProcessCompleted(string eventID, string processID, string processStatus, string approverID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@EVENT_ID", eventID);
                    parameters[1] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[2] = new SqlParameter("@PROCESS_STATUS", processStatus);
                    parameters[3] = new SqlParameter("@APPROVER_ID", approverID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_UPDATE_PROCESS_COMPLETED, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [ InsertProcessApproverCompleted ]

        public void InsertProcessApproverCompleted(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_INSERT_PROCESS_APPROVER_COMPLETED, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region InsertForwardApprovalAddUser [TB_PROCESS_APPROVER Foward Approver Append]
        /// <summary>
        /// 결재선에 현결재자 다음으로 결재선에 추가
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="currentApproverID"></param>
        /// <param name="forwardUserID"></param>
        public void InsertForwardApprovalAddUser(string processID, string currentApproverID, string forwardUserID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@USER_ID", currentApproverID);
                    parameters[2] = new SqlParameter("@APPROVER_ID", forwardUserID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_INSERT_PROCESS_APPROVER_FORWARDAPPROVAL_USER, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region ChangeProcessDocumentFinalApprover [결재문서목록 테이블(TB_PROCESS_DOCUMENT) Final Approver를 변경한다.]
        /// <summary>
        /// 결재문서목록 테이블(TB_PROCESS_DOCUMENT)에 문서상태와 다음결재자 정보를 변경
        /// </summary>
        /// <param name="processid">프로세스아이디</param>
        /// <param name="newFinalApproverid">현결재자아이디</param>
        public void ChangeProcessEventFinalApprover(string processid, string newFinalApproverid)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processid);
                    parameters[1] = new SqlParameter("@APPROVER_ID", newFinalApproverid);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_UPDATE_PROCESS_EVENT_FINAL_APPROVER, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region SelectApprovalCompletedList
        public List<DTO_PROCESS_COMPLETED_LIST> SelectApprovalCompletedList(string processStatus, string userid, string searchType, string searchText, DateTime? doc_Start, DateTime? doc_End, DateTime? evt_Start, DateTime? evt_End)
        {

            using (_context = new ApprovalContext())
            {
                SqlParameter[] parameters = new SqlParameter[8];
                parameters[0] = new SqlParameter("@STATUS", processStatus);
                parameters[1] = new SqlParameter("@USER_ID", userid);
                parameters[2] = new SqlParameter("@SEARCH_TYPE", searchType);
                parameters[3] = new SqlParameter("@SEARCH_TEXT", searchText);
                parameters[4] = new SqlParameter("@DOC_START", (object)doc_Start ?? DBNull.Value);
                parameters[5] = new SqlParameter("@DOC_END", (object)doc_End ?? DBNull.Value);
                parameters[6] = new SqlParameter("@EVT_START", (object)evt_Start ?? DBNull.Value);
                parameters[7] = new SqlParameter("@EVT_END", (object)evt_End ?? DBNull.Value);

                var result = _context.Database.SqlQuery<DTO_PROCESS_COMPLETED_LIST>(ApprovalContext.USP_SELECT_APPROVAL_COMPLETED_LIST, parameters);

                return result.ToList();
            }
        }
        #endregion

        public void DeleteEventProcess(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_DELETE_EVENT_PROCESS, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_PROCESS_COMPLETED_LIST> SelectApprovalPaymentCompletedList(string processStatus, string userid, string searchType, string searchText, DateTime? doc_Start, DateTime? doc_End, DateTime? evt_Start, DateTime? evt_End)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[8];
                    parameters[0] = new SqlParameter("@STATUS", processStatus);
                    parameters[1] = new SqlParameter("@USER_ID", userid);
                    parameters[2] = new SqlParameter("@SEARCH_TYPE", searchType);
                    parameters[3] = new SqlParameter("@SEARCH_TEXT", searchText);
                    parameters[4] = new SqlParameter("@DOC_START", (object)doc_Start ?? DBNull.Value);
                    parameters[5] = new SqlParameter("@DOC_END", (object)doc_End ?? DBNull.Value);
                    parameters[6] = new SqlParameter("@EVT_START", (object)evt_Start ?? DBNull.Value);
                    parameters[7] = new SqlParameter("@EVT_END", (object)evt_End ?? DBNull.Value);

                    var result = _context.Database.SqlQuery<DTO_PROCESS_COMPLETED_LIST>(ApprovalContext.USP_SELECT_APPROVAL_PAYMENTCOMPLETED_LIST, parameters);

                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        #region SelectApprovalRejectList
        public List<DTO_PROCESS_REJECT_LIST> SelectApprovalRejectList(string reqid, string searchType, string searchText, DateTime? doc_Start, DateTime? doc_End, DateTime? evt_Start, DateTime? evt_End)
        {

            using (_context = new ApprovalContext())
            {
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@REQUESTER_ID", reqid);
                parameters[1] = new SqlParameter("@SEARCH_TYPE", searchType);
                parameters[2] = new SqlParameter("@SEARCH_TEXT", searchText);
                parameters[3] = new SqlParameter("@DOC_START", (object)doc_Start ?? DBNull.Value);
                parameters[4] = new SqlParameter("@DOC_END", (object)doc_End ?? DBNull.Value);
                parameters[5] = new SqlParameter("@EVT_START", (object)evt_Start ?? DBNull.Value);
                parameters[6] = new SqlParameter("@EVT_END", (object)evt_End ?? DBNull.Value);

                var result = _context.Database.SqlQuery<DTO_PROCESS_REJECT_LIST>(ApprovalContext.USP_SELECT_APPROVAL_REJECT_LIST, parameters);

                return result.ToList();
            }
        }
        #endregion

        #region SelectApprovalWithdrawList
        public List<DTO_PROCESS_WITHDRAW_LIST> SelectApprovalWithdrawList(string reqid, string searchType, string searchText, DateTime? doc_Start, DateTime? doc_End, DateTime? evt_Start, DateTime? evt_End)
        {

            using (_context = new ApprovalContext())
            {
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@REQUESTER_ID", reqid);
                parameters[1] = new SqlParameter("@SEARCH_TYPE", searchType);
                parameters[2] = new SqlParameter("@SEARCH_TEXT", searchText);
                parameters[3] = new SqlParameter("@DOC_START", (object)doc_Start ?? DBNull.Value);
                parameters[4] = new SqlParameter("@DOC_END", (object)doc_End ?? DBNull.Value);
                parameters[5] = new SqlParameter("@EVT_START", (object)evt_Start ?? DBNull.Value);
                parameters[6] = new SqlParameter("@EVT_END", (object)evt_End ?? DBNull.Value);

                var result = _context.Database.SqlQuery<DTO_PROCESS_WITHDRAW_LIST>(ApprovalContext.USP_SELECT_APPROVAL_WITHDRAW_LIST, parameters);

                return result.ToList();
            }
        }
        #endregion

        #region [ SelectApprovalCanceledList ]

        public List<DTO_PROCESS_COMPLETED_LIST> SelectApprovalCanceledList(string processStatus, string userid, string searchType, string searchText, DateTime? doc_Start, DateTime? doc_End, DateTime? evt_Start, DateTime? evt_End)
        {

            using (_context = new ApprovalContext())
            {
                SqlParameter[] parameters = new SqlParameter[8];
                parameters[0] = new SqlParameter("@STATUS", processStatus);
                parameters[1] = new SqlParameter("@USER_ID", userid);
                parameters[2] = new SqlParameter("@SEARCH_TYPE", searchType);
                parameters[3] = new SqlParameter("@SEARCH_TEXT", searchText);
                parameters[4] = new SqlParameter("@DOC_START", (object)doc_Start ?? DBNull.Value);
                parameters[5] = new SqlParameter("@DOC_END", (object)doc_End ?? DBNull.Value);
                parameters[6] = new SqlParameter("@EVT_START", (object)evt_Start ?? DBNull.Value);
                parameters[7] = new SqlParameter("@EVT_END", (object)evt_End ?? DBNull.Value);

                var result = _context.Database.SqlQuery<DTO_PROCESS_COMPLETED_LIST>(ApprovalContext.USP_SELECT_APPROVAL_CANCEL_LIST, parameters);

                return result.ToList();
            }
        }
        #endregion

        #region [ SelectApprovalAdminList ]
        public List<DTO_PROCESS_COMPLETED_LIST> SelectApprovalAdminList(string userid, string searchType, string searchText, DateTime? doc_Start, DateTime? doc_End, DateTime? evt_Start, DateTime? evt_End)
        {
            using (_context = new ApprovalContext())
            {
                SqlParameter[] parameters = new SqlParameter[7];
                parameters[0] = new SqlParameter("@USER_ID", userid);
                parameters[1] = new SqlParameter("@SEARCH_TYPE", searchType);
                parameters[2] = new SqlParameter("@SEARCH_TEXT", searchText);
                parameters[3] = new SqlParameter("@DOC_START", (object)doc_Start ?? DBNull.Value);
                parameters[4] = new SqlParameter("@DOC_END", (object)doc_End ?? DBNull.Value);
                parameters[5] = new SqlParameter("@EVT_START", (object)evt_Start ?? DBNull.Value);
                parameters[6] = new SqlParameter("@EVT_END", (object)evt_End ?? DBNull.Value);

                var result = _context.Database.SqlQuery<DTO_PROCESS_COMPLETED_LIST>(ApprovalContext.USP_SELECT_APPROVAL_ADMIN_LIST, parameters);

                return result.ToList();
            }
        } 
        #endregion

        #region [ Insert Event Log ]
        public int InsertEventLog(string processID, string registerID, string logType, string comment)
        {
            return InsertEventLog(processID, registerID, logType, comment, string.Empty);
        }

        public int InsertEventLog(string processID, string registerID, string logType, string comment, string commentCategory)
        {
            int retValue = 0;
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[5];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@REGISTER_ID", registerID);
                    parameters[2] = new SqlParameter("@LOG_TYPE", logType);
                    parameters[3] = new SqlParameter("@LOG_CATEGORY", commentCategory);
                    parameters[4] = new SqlParameter("@COMMENT", comment);

                    retValue = _context.Database.SqlQuery<int>(ApprovalContext.USP_INSERT_LOG_EVENT, parameters).First();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retValue;
        }
        #endregion

        #region [ InsertEventAttachFiles - 이벤트 첨부파일 저장]

        public int InsertEventAttachFiles(DTO_EVENT_ATTACH_FILES file)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[11];
                    parameters[0] = new SqlParameter("@PROCESS_ID", file.PROCESS_ID);
                    parameters[1] = new SqlParameter("@ATTACH_FILE_TYPE", file.ATTACH_FILE_TYPE);
                    parameters[2] = new SqlParameter("@SEQ", file.SEQ);
                    parameters[3] = new SqlParameter("@REFER_IDX", file.REFER_IDX);
                    parameters[4] = new SqlParameter("@DISPLAY_FILE_NAME", file.DISPLAY_FILE_NAME);
                    parameters[5] = new SqlParameter("@SAVED_FILE_NAME", file.SAVED_FILE_NAME);
                    parameters[6] = new SqlParameter("@FILE_SIZE", file.FILE_SIZE);
                    parameters[7] = new SqlParameter("@FILE_PATH", file.FILE_PATH);
                    parameters[8] = new SqlParameter("@FILE_HANDLER_URL", file.FILE_HANDLER_URL);
                    parameters[9] = new SqlParameter("@IS_DELETED", file.IS_DELETED);
                    parameters[10] = new SqlParameter("@CREATOR_ID", file.CREATOR_ID);

                    var result = _context.Database.SqlQuery<int>(ApprovalContext.USP_INSERT_EVENT_ATTACH_FILES, parameters);
                    return result.FirstOrDefault();

                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region [ DeleteEventAttachFiles - 이벤트 첨부파일 삭제 ]

        public void DeleteEventAttachFiles(int idx, string updaterID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@IDX", idx);
                    parameters[1] = new SqlParameter("@UPDATER_ID", updaterID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_DELETE_EVENT_ATTACH_FILES, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region [ SelectEventAttachFile - 이벤트 첨부파일 조회 ]
        public List<DTO_EVENT_ATTACH_FILES> SelectEventAttachFiles(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_EVENT_ATTACH_FILES>(ApprovalContext.USP_SELECT_EVENT_ATTACH_FILES, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region [ SelectEventAttachFile idxs - 이벤트 첨부파일 조회 ]
        public List<DTO_EVENT_ATTACH_FILES> SelectEventAttachFilesIdxs(string processID, string idxs)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@IDX", idxs);

                    var result = _context.Database.SqlQuery<DTO_EVENT_ATTACH_FILES>(ApprovalContext.USP_SELECT_EVENT_ATTACH_FILES_IDXS, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region [ InsertProcessApproveCompletedAddReviewer - 완료된 결재선에 Reviewer 추가 ]
        /// <summary>
        /// 완료된 결재선에 Reviewer 추가
        /// </summary>
        /// <param name="add"></param>
        public void InsertProcessApproveCompletedAddReviewer(DTO_PROCESS_APPROVER_COMPLETED add)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[12];
                    parameters[0] = new SqlParameter("@PROCESS_ID", add.PROCESS_ID);
                    parameters[1] = new SqlParameter("@APPROVAL_TYPE", add.APPROVAL_TYPE);
                    parameters[2] = new SqlParameter("@APPROVAL_SEQ", add.APPROVAL_SEQ);
                    parameters[3] = new SqlParameter("@APPROVER_TYPE", add.APPROVER_TYPE);
                    parameters[4] = new SqlParameter("@APPROVER_ID", add.APPROVER_ID);
                    parameters[5] = new SqlParameter("@APPROVER_ORG_NAME", add.APPROVER_ORG_NAME);
                    parameters[6] = new SqlParameter("@ABSENCE_APPROVER_ID", add.ABSENCE_APPROVER_ID);
                    parameters[7] = new SqlParameter("@ABSENCE_APPROVER_ORG_NAME", add.ABSENCE_APPROVER_ORG_NAME);
                    parameters[8] = new SqlParameter("@STATUS", add.STATUS);
                    parameters[9] = new SqlParameter("@SENT_MAIL", add.SENT_MAIL);
                    parameters[10] = new SqlParameter("@APPROVAL_DATE", (object)add.APPROVAL_DATE ?? DBNull.Value);
                    parameters[11] = new SqlParameter("@COMMENT", add.COMMENT);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_MERGE_PROCESS_APPROVER_COMPLETED_ADD_REVIEWER, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region [ SelectProcessApproverList - 결재라인 조회(Approver,Recipient,Reviewer) ]
        public List<DTO_PROCESS_APPROVAL_LIST> SelectProcessApproverList(string processID)
        {

            using (_context = new ApprovalContext())
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                var result = _context.Database.SqlQuery<DTO_PROCESS_APPROVAL_LIST>(ApprovalContext.USP_SELECT_PROCESS_APPROVER_LIST, parameters);

                return result.ToList();
            }
        }
        #endregion

        #region [ SelectDocLogList - 결재관련 로그(Comment)정보]
        public List<DTO_LOG_EVENT> SelectLogEventList(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_LOG_EVENT>(ApprovalContext.USP_SELECT_LOG_EVENT_LIST, parameters);

                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region [ Cost Plan ]

        public string MergeCostPlan(DTO_MODULE_COST_PLAN dto)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[10];
                    parameters[0] = new SqlParameter("@EVENT_ID", dto.EVENT_ID);
                    parameters[1] = new SqlParameter("@PROCESS_ID", dto.PROCESS_ID);
                    parameters[2] = new SqlParameter("@COST_PLAN_IDX", dto.COST_PLAN_IDX);
                    parameters[3] = new SqlParameter("@CATEGORY_CODE", dto.CATEGORY_CODE);
                    parameters[4] = new SqlParameter("@QTY", dto.QTY);
                    parameters[5] = new SqlParameter("@PRICE", dto.PRICE);
                    parameters[6] = new SqlParameter("@DESC", dto.DESC);
                    parameters[7] = new SqlParameter("@IS_DELETED", dto.IS_DELETED);
                    parameters[8] = new SqlParameter("@CREATOR_ID", dto.CREATOR_ID);
                    parameters[9] = new SqlParameter("@UPDATER_ID", dto.UPDATER_ID);

                    var result = _context.Database.SqlQuery<string>(ApprovalContext.USP_MERGE_MODULE_COST_PLAN, parameters);
                    return result.FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_MODULE_COST_PLAN> SelectCostPlan(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_MODULE_COST_PLAN>(ApprovalContext.USP_SELECT_MODULE_COST_PLAN, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteCostPlan(string processID, int costPlanIDX, string updaterID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@COST_PLAN_IDX", costPlanIDX);
                    parameters[2] = new SqlParameter("@UPDATER_ID", updaterID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_DELETE_MODULE_COST_PLAN, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region [ Participants ]

        public string InsertParticipant(DTO_MODULE_PARTICIPANTS participant)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[18];
                    parameters[0] = new SqlParameter("@EVENT_ID", participant.EVENT_ID);
                    parameters[1] = new SqlParameter("@PROCESS_ID", participant.PROCESS_ID);
                    parameters[2] = new SqlParameter("@PARTICIPANT_IDX", participant.PARTICIPANT_IDX);
                    parameters[3] = new SqlParameter("@PARTICIPANT_TYPE", participant.PARTICIPANT_TYPE);
                    parameters[4] = new SqlParameter("@HCP_TYPE", participant.HCP_TYPE);
                    parameters[5] = new SqlParameter("@HCP_CODE", participant.HCP_CODE);
                    parameters[6] = new SqlParameter("@HCP_NAME", participant.HCP_NAME);
                    parameters[7] = new SqlParameter("@HCO_CODE", participant.HCO_CODE);
                    parameters[8] = new SqlParameter("@HCO_NAME", participant.HCO_NAME);
                    parameters[9] = new SqlParameter("@SPECIALTY_CODE", participant.SPECIALTY_CODE);
                    parameters[10] = new SqlParameter("@SPECIALTY_NAME", participant.SPECIALTY_NAME);
                    parameters[11] = new SqlParameter("@COUNTRY_CODE", participant.COUNTRY_CODE);
                    parameters[12] = new SqlParameter("@COUNTRY_NAME", participant.COUNTRY_NAME);
                    parameters[13] = new SqlParameter("@ROLE", participant.ROLE);
                    parameters[14] = new SqlParameter("@DATA_SOURCE", participant.DATA_SOURCE);
                    parameters[15] = new SqlParameter("@IS_ATTENDED", participant.IS_ATTENDED);
                    parameters[16] = new SqlParameter("@IS_DELETED", participant.IS_DELETED);
                    parameters[17] = new SqlParameter("@CREATOR_ID", participant.CREATOR_ID);

                    var result = _context.Database.SqlQuery<string>(ApprovalContext.USP_INSERT_MODULE_PARTICIPANTS, parameters);
                    return result.FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_MODULE_PARTICIPANTS> SelectParticipants(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_MODULE_PARTICIPANTS>(ApprovalContext.USP_SELECT_MODULE_PARTICIPANTS, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_MODULE_PARTICIPANTS> SelectParticipantsByEventKey(string eventKey)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@EVENT_KEY", eventKey);

                    var result = _context.Database.SqlQuery<DTO_MODULE_PARTICIPANTS>(ApprovalContext.USP_SELECT_MODULE_PARTICIPANTS_BY_EVENTKEY, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public DTO_EVENT_ATTENDEES_LIST_INFO SelectEventAttendeesListInfo(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_EVENT_ATTENDEES_LIST_INFO>(ApprovalContext.USP_SELECT_EVENT_INFO_FOR_ATTENDEES_LIST, parameters);
                    return result.FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }





        public void UpdateParticipantStatus(string processID, int participantIDX, string isAttended, string updaterID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@PARTICIPANT_IDX", participantIDX);
                    parameters[2] = new SqlParameter("@IS_ATTENDED", isAttended);
                    parameters[3] = new SqlParameter("@UPDATER_ID", updaterID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_UPDATE_MODULE_PARTICIPANT_STATUS, parameters);

                }
            }
            catch
            {
                throw;
            }
        }

        // version 1.0.5 Admin Event Page for change by DM Team
        public void UpdateChangeValue(string PROCESS_ID, string CATEGORY, string ADJUSTMENT_AREA, string OLD_VALUE, string NEW_VALUE, string REASON, string UPDATER_ID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[7];
                    parameters[0] = new SqlParameter("@PROCESS_ID", PROCESS_ID);
                    parameters[1] = new SqlParameter("@CATEGORY", CATEGORY);
                    parameters[2] = new SqlParameter("@ADJUSTMENT_AREA", ADJUSTMENT_AREA);
                    parameters[3] = new SqlParameter("@OLD_VALUE", OLD_VALUE);
                    parameters[4] = new SqlParameter("@NEW_VALUE", NEW_VALUE);
                    parameters[5] = new SqlParameter("@REASON", REASON);
                    parameters[6] = new SqlParameter("@UPDATER_ID", UPDATER_ID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_UPDATE_ADMIN_CORRECTION, parameters);

                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteParticipant(string processID, int participantIDX, string updaterID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@PARTICIPANT_IDX", participantIDX);
                    parameters[2] = new SqlParameter("@UPDATER_ID", updaterID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_DELETE_MODULE_PARTICIPANT, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public List<HealthCareProviderDto> SelectParticipantsByContract(string impactNo, string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@IMPACT_NO", impactNo);
                    parameters[1] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<HealthCareProviderDto>(ApprovalContext.USP_SELECT_MODULE_PARTICIPANTS_CONTRACT, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region [ Agenda ]

        public string MergeAgenda(DTO_MODULE_AGENDA agenda)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[10];
                    parameters[0] = new SqlParameter("@EVENT_ID", agenda.EVENT_ID);
                    parameters[1] = new SqlParameter("@PROCESS_ID", agenda.PROCESS_ID);
                    parameters[2] = new SqlParameter("@AGENDA_IDX", agenda.AGENDA_IDX);
                    parameters[3] = new SqlParameter("@DAY", agenda.DAY);
                    parameters[4] = new SqlParameter("@START_TIME", agenda.START_TIME);
                    parameters[5] = new SqlParameter("@DURATION", agenda.DURATION);
                    parameters[6] = new SqlParameter("@SUBJECT", agenda.SUBJECT);
                    parameters[7] = new SqlParameter("@IS_DELETED", agenda.IS_DELETED);
                    parameters[8] = new SqlParameter("@CREATOR_ID", agenda.CREATOR_ID);
                    parameters[9] = new SqlParameter("@UPDATER_ID", agenda.UPDATER_ID);

                    var result = _context.Database.SqlQuery<string>(ApprovalContext.USP_MERGE_MODULE_AGENDA, parameters);
                    return result.FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteAgenda(string processID, int agendaIDX, string updaterID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@AGENDA_IDX", agendaIDX);
                    parameters[2] = new SqlParameter("@UPDATER_ID", updaterID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_DELETE_MODULE_AGENDA, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_MODULE_AGENDA> SelectAgenda(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_MODULE_AGENDA>(ApprovalContext.USP_SELECT_MODULE_AGENDA, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public int MergeAgendaRole(DTO_MODULE_AGENDA_ROLE agendaRole)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[14];
                    parameters[0] = new SqlParameter("@PROCESS_ID", agendaRole.PROCESS_ID);
                    parameters[1] = new SqlParameter("@AGENDA_IDX", agendaRole.AGENDA_IDX);
                    parameters[2] = new SqlParameter("@AGENDA_ROLE_IDX", agendaRole.AGENDA_ROLE_IDX);
                    parameters[3] = new SqlParameter("@PARTICIPANT_IDX", agendaRole.PARTICIPANT_IDX);
                    parameters[4] = new SqlParameter("@PARTICIPANT_CODE", agendaRole.PARTICIPANT_CODE);
                    parameters[5] = new SqlParameter("@ROLE_TYPE", agendaRole.ROLE_TYPE);
                    parameters[6] = new SqlParameter("@ROLE", agendaRole.ROLE);
                    parameters[7] = new SqlParameter("@CRITERIA", agendaRole.CRITERIA);
                    parameters[8] = new SqlParameter("@REASON", agendaRole.REASON);
                    parameters[9] = new SqlParameter("@AMOUNT", agendaRole.AMOUNT);
                    parameters[10] = new SqlParameter("@MATERIAL_CODE", agendaRole.MATERIAL_CODE);
                    parameters[11] = new SqlParameter("@IS_DELETED", agendaRole.IS_DELETED);
                    parameters[12] = new SqlParameter("@CREATOR_ID", agendaRole.CREATOR_ID);
                    parameters[13] = new SqlParameter("@UPDATER_ID", agendaRole.UPDATER_ID);

                    var result = _context.Database.SqlQuery<int>(ApprovalContext.USP_MERGE_MODULE_AGENDA_ROLE, parameters);
                    return result.FirstOrDefault();

                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteAgendaRole(string processID, int agendaIDX, int agendaRoleIDX, string updaterID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@AGENDA_IDX", agendaIDX);
                    parameters[2] = new SqlParameter("@AGENDA_ROLE_IDX", agendaRoleIDX);
                    parameters[3] = new SqlParameter("@UPDATER_ID", updaterID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_DELETE_MODULE_AGENDA_ROLE, parameters);

                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_MODULE_AGENDA_ROLE> SelectAgendaRole(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_MODULE_AGENDA_ROLE>(ApprovalContext.USP_SELECT_MODULE_AGENDA_ROLE, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_MODULE_PARTICIPANTS> SelectAgendaParticipants(string processID, int participantIDX)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@PARTICIPANT_IDX", participantIDX);

                    var result = _context.Database.SqlQuery<DTO_MODULE_PARTICIPANTS>(ApprovalContext.USP_SELECT_MODULE_AGENDA_PARTICIPANTS, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public DTO_MODULE_AGENDA_ROLE SelectAgendaRoleInfo(string processID, int agendaIDX, int agendaRoleIDX)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@AGENDA_IDX", agendaIDX);
                    parameters[2] = new SqlParameter("@AGENDA_ROLE_IDX", agendaRoleIDX);

                    var result = _context.Database.SqlQuery<DTO_MODULE_AGENDA_ROLE>(ApprovalContext.USP_SELECT_MODULE_AGENDA_ROLE_INFO, parameters);
                    return result.FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }

        public ModuleAgendaRuleDto SelectAgendaRoleInfoPrint(string processID, int agendaIDX, int agendaRoleIDX)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@AGENDA_IDX", agendaIDX);
                    parameters[2] = new SqlParameter("@AGENDA_ROLE_IDX", agendaRoleIDX);

                    var result = _context.Database.SqlQuery<ModuleAgendaRuleDto>(ApprovalContext.USP_SELECT_MODULE_AGENDA_ROLE_PRINT, parameters);
                    return result.FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }
        public void UpdateMaterialcode(string processID,string agendaIDX,string mcode,string updateID, string costcenter, string sap_no, string krpia)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[7];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@AGENDA_ROLE_IDX", agendaIDX);
                    parameters[2] = new SqlParameter("@MATERIAL_CODE", mcode);
                    parameters[3] = new SqlParameter("@COST_CENTER", costcenter);
                    parameters[4] = new SqlParameter("@SAP_NO", sap_no);
                    parameters[5] = new SqlParameter("@KRPIA", krpia);
                    parameters[6] = new SqlParameter("@UPDATERID", updateID);
                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_UPDATE_MODULE_AGENDA_MATERIALCODE,parameters);
                    //var result = _context.Database.SqlQuery<ModuleAgendaRuleDto>(ApprovalContext.USP_SELECT_MODULE_AGENDA_ROLE_PRINT, parameters);
                    //return result.FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_MODULE_AGENDA_ROLE> SelectAgendaRoleAmountForHCP(string hcpCode, string processID, string roleType, string role)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@HCP_CODE", hcpCode);
                    parameters[1] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[2] = new SqlParameter("@ROLE_TYPE", roleType);
                    parameters[3] = new SqlParameter("@ROLE", role);

                    var result = _context.Database.SqlQuery<DTO_MODULE_AGENDA_ROLE>(ApprovalContext.USP_SELECT_MODULE_AGENDA_ROLE_AMOUNT, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<EventAgendaSummaryDto> SelectAgendaRoleSummary(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<EventAgendaSummaryDto>(ApprovalContext.USP_SELECT_MODULE_AGENDA_SUMMARY, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region [ Payment ]

        public List<EventPaymentByHCPDto> SelectEventPaymentByHCP(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<EventPaymentByHCPDto>(ApprovalContext.USP_SELECT_MODULE_PAYMENT_BY_HCP, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<EventPaymentPlanActualDto> SelectEventPaymentPlanActual(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<EventPaymentPlanActualDto>(ApprovalContext.USP_SELECT_MODULE_PAYMENT_PLAN_ACTUAL, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public void UpdatePaymentUploadSRM(string processID, string poNumber, string userID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@PO_NUMBER", poNumber);
                    parameters[2] = new SqlParameter("@UPDATER_ID", userID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_UPDATE_PAYMENT_UPLOAD_SRM, parameters);

                }
            }
            catch
            {
                throw;
            }
        }

        public void InsertEventPaymentUploadSRM(DTO_PAYMENT_UPLOAD_SRM srm)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[15];
                    parameters[0] = new SqlParameter("@PROCESS_ID", srm.PROCESS_ID);
                    parameters[1] = new SqlParameter("@SRM_IDX", srm.SRM_IDX);
                    parameters[2] = new SqlParameter("@PARTICIPANT_TYPE", srm.PARTICIPANT_TYPE);
                    parameters[3] = new SqlParameter("@CATEGORY_CODE", srm.CATEGORY_CODE);
                    parameters[4] = new SqlParameter("@PO_NUMBER", srm.PO_NUMBER);
                    parameters[5] = new SqlParameter("@AMOUNT", srm.AMOUNT);
                    parameters[6] = new SqlParameter("@HCP_CODE", srm.HCP_CODE);
                    parameters[7] = new SqlParameter("@HCP_NAME", srm.HCP_NAME);
                    parameters[8] = new SqlParameter("@HCO_CODE", srm.HCO_CODE);
                    parameters[9] = new SqlParameter("@HCO_NAME", srm.HCO_NAME);
                    parameters[10] = new SqlParameter("@COMMENT", srm.COMMENT);
                    parameters[11] = new SqlParameter("@ERROR_MESSAGE", srm.ERROR_MESSAGE);
                    parameters[12] = new SqlParameter("@STATUS", srm.STATUS);
                    parameters[13] = new SqlParameter("@IS_DELETED", srm.IS_DELETED);
                    parameters[14] = new SqlParameter("@CREATOR_ID", srm.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_INSERT_PAYMENT_UPLOAD_SRM, parameters);

                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 업로드된 엑셀 내용을 조회한다.
        /// TB_PAYMENT_UPLOAD_SRM 에서 Status가  Upload인 경우
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        public List<DTO_PAYMENT_UPLOAD_SRM> SelectEventPaymentUploadSRM(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_PAYMENT_UPLOAD_SRM>(ApprovalContext.USP_SELECT_PAYMENT_UPLOAD_SRM, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 이미 등록된 SRM 내용을 조회한다.
        /// TB_PAYMENT_UPLOAD_SRM 에서 Status가 Saved인 경우
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="poNumber"></param>
        /// <returns></returns>
        public List<DTO_PAYMENT_UPLOAD_SRM> SelectEventPaymentSavedSRM(string processID, string poNumber)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@PO_NUMBER", poNumber);

                    var result = _context.Database.SqlQuery<DTO_PAYMENT_UPLOAD_SRM>(ApprovalContext.USP_SELECT_PAYMENT_SAVED_SRM, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteEventPaymentUploadSRM(string processID, string userID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@UPDATER_ID", userID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_DELETE_PAYMENT_UPLOAD_SRM, parameters);

                }
            }
            catch
            {
                throw;
            }
        }

        public DTO_PAYMENT_UPLOAD_SRM_TOTAL GetEventPaymentUploadSRMTotal(string poNumber)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PO_NUMBER", poNumber);

                    var result = _context.Database.SqlQuery<DTO_PAYMENT_UPLOAD_SRM_TOTAL>(ApprovalContext.USP_SELECT_PAYMENT_UPLOAD_SRM_TOTAL, parameters);
                    return result.FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }


        public void InsertEventPaymentUploadSRMHistory(DTO_PAYMENT_UPLOAD_SRM_HISTORY history)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[11];
                    parameters[0] = new SqlParameter("@PROCESS_ID", history.PROCESS_ID);
                    parameters[1] = new SqlParameter("@SRM_HISTORY_IDX", history.SRM_HISTORY_IDX);
                    parameters[2] = new SqlParameter("@PO_NUMBER", history.PO_NUMBER);
                    parameters[3] = new SqlParameter("@AMOUNT", history.AMOUNT);
                    parameters[4] = new SqlParameter("@COMMENT", history.COMMENT);
                    parameters[5] = new SqlParameter("@DISPLAY_FILE_NAME", history.DISPLAY_FILE_NAME);
                    parameters[6] = new SqlParameter("@SAVED_FILE_NAME", history.SAVED_FILE_NAME);
                    parameters[7] = new SqlParameter("@FILE_PATH", history.FILE_PATH);
                    parameters[8] = new SqlParameter("@FILE_HANDLER_URL", history.FILE_HANDLER_URL);
                    parameters[9] = new SqlParameter("@IS_DELETED", history.IS_DELETED);
                    parameters[10] = new SqlParameter("@UPLOADER_ID", history.UPLOADER_ID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_INSERT_PAYMENT_UPLOAD_SRM_HISTORY, parameters);

                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_PAYMENT_UPLOAD_SRM_HISTORY> SelectEventPaymentUploadSRMHistory(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_PAYMENT_UPLOAD_SRM_HISTORY>(ApprovalContext.USP_SELECT_PAYMENT_UPLOAD_SRM_HISTORY, parameters);
                    return result.ToList();
                }

            }
            catch
            {
                throw;
            }
        }

        public void DeleteEventPaymentUploadSRMHistory(string processID, string poNumber, string updaterID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@PO_NUMBER", poNumber);
                    parameters[2] = new SqlParameter("@UPDATER_ID", updaterID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_DELETE_PAYMENT_UPLOAD_SRM_HISTORY, parameters);

                }
            }
            catch
            {
                throw;
            }
        }

        public List<EventPaymentLayerDto> SelectEventPaymentLayer(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<EventPaymentLayerDto>(ApprovalContext.USP_SELECT_MODULE_PAYMENT_LAYER, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<EventPaymentLayerDto> SelectEventPaymentLayerByHCP(string processID, string hcpCode, string dataSource)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@HCP_CODE", hcpCode);
                    parameters[2] = new SqlParameter("@DATA_SOURCE", dataSource);

                    var result = _context.Database.SqlQuery<EventPaymentLayerDto>(ApprovalContext.USP_SELECT_MODULE_PAYMENT_LAYER_BY_HCP, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public void MergeEventPaymentInputSRM(DTO_PAYMENT_INPUT_SRM srm)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[10];
                    parameters[0] = new SqlParameter("@PROCESS_ID", srm.PROCESS_ID);
                    parameters[1] = new SqlParameter("@SRM_IDX", srm.SRM_IDX);
                    parameters[2] = new SqlParameter("@PO_NUMBER", srm.PO_NUMBER);
                    parameters[3] = new SqlParameter("@CATEGORY_CODE", srm.CATEGORY_CODE);
                    parameters[4] = new SqlParameter("@POST_DATE", srm.POST_DATE);
                    parameters[5] = new SqlParameter("@AMOUNT", srm.AMOUNT);
                    parameters[6] = new SqlParameter("@COMMENT", srm.COMMENT);
                    parameters[7] = new SqlParameter("@IS_DELETED", srm.IS_DELETED);
                    parameters[8] = new SqlParameter("@CREATOR_ID", srm.CREATOR_ID);
                    parameters[9] = new SqlParameter("@UPDATER_ID", srm.UPDATER_ID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_MERGE_PAYMENT_INPUT_SRM, parameters);

                }
            }
            catch
            {
                throw;
            }

        }

        public List<DTO_PAYMENT_INPUT_SRM> SelectEventPaymentInputSRM(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_PAYMENT_INPUT_SRM>(ApprovalContext.USP_SELECT_PAYMENT_INPUT_SRM, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteEventPaymentInputSRM(string processID, int srmIDX, string updaterID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@SRM_IDX", srmIDX);
                    parameters[2] = new SqlParameter("@UPDATER_ID", updaterID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_DELETE_PAYMENT_INPUT_SRM, parameters);

                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region [ Payment Your Doces ]
        public void InsertEventPaymentUploadYourDoces(DTO_PAYMENT_UPLOAD_YOURDOCES yourdoces)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[27];
                    parameters[0] = new SqlParameter("@ACCOUNT", yourdoces.ACCOUNT);
                    parameters[1] = new SqlParameter("@NAME_1", yourdoces.NAME_1);
                    parameters[2] = new SqlParameter("@DOCUMENT_NUMBER", yourdoces.DOCUMENT_NUMBER);
                    parameters[3] = new SqlParameter("@DOCUMENT_TYPE", yourdoces.DOCUMENT_TYPE);
                    parameters[4] = new SqlParameter("@PAYMENT_BLOCK", yourdoces.PAYMENT_BLOCK);

                    parameters[5] = new SqlParameter("@DOCUMENT_HEADER_TEXT", yourdoces.DOCUMENT_HEADER_TEXT);
                    parameters[6] = new SqlParameter("@DOCUMENT_DATE", yourdoces.DOCUMENT_DATE);
                    parameters[7] = new SqlParameter("@ENTRY_DATE", yourdoces.ENTRY_DATE);
                    parameters[8] = new SqlParameter("@POSTING_DATE", yourdoces.POSTING_DATE);
                    parameters[9] = new SqlParameter("@NET_DUE_DATE", yourdoces.NET_DUE_DATE);

                    parameters[10] = new SqlParameter("@AMOUNT_IN_DOC_CURR", yourdoces.AMOUNT_IN_DOC_CURR);
                    parameters[11] = new SqlParameter("@DOCUMENT_CURRENCY", yourdoces.DOCUMENT_CURRENCY);
                    parameters[12] = new SqlParameter("@AMOUNT_IN_LOCAL_CURRENCY", yourdoces.AMOUNT_IN_LOCAL_CURRENCY);
                    parameters[13] = new SqlParameter("@LOCAL_CURRENCY", yourdoces.LOCAL_CURRENCY);
                    parameters[14] = new SqlParameter("@TEXT", yourdoces.TEXT);

                    parameters[15] = new SqlParameter("@USER_NAME", yourdoces.USER_NAME);
                    parameters[16] = new SqlParameter("@CLEARING_DATE", yourdoces.CLEARING_DATE);
                    parameters[17] = new SqlParameter("@CLEARING_DOCUMENT", yourdoces.CLEARING_DOCUMENT);
                    parameters[18] = new SqlParameter("@REFERENCE_KEY_1", yourdoces.REFERENCE_KEY_1);
                    parameters[19] = new SqlParameter("@REFERENCE_KEY_2", yourdoces.REFERENCE_KEY_2);

                    parameters[20] = new SqlParameter("@REFERENCE_KEY_3", yourdoces.REFERENCE_KEY_3);
                    parameters[21] = new SqlParameter("@HCO_CODE", yourdoces.HCO_CODE);
                    parameters[22] = new SqlParameter("@COMMENTS", yourdoces.COMMENTS);
                    parameters[23] = new SqlParameter("@STATUS", yourdoces.STATUS);
                    parameters[24] = new SqlParameter("@ERROR_MESSAGE", yourdoces.ERROR_MESSAGE);
                    parameters[25] = new SqlParameter("@IS_DELETED", yourdoces.IS_DELETED);

                    parameters[26] = new SqlParameter("@CREATOR_ID", yourdoces.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_INSERT_PAYMENT_UPLOAD_YOURDOCES, parameters);
                }
            }
            catch
            {
                throw;
            }


        }


        public void DeleteEventPaymentUploadYourDoces(string updaterID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@UPDATER_ID", updaterID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_DELETE_PAYMENT_UPLOAD_YOURDOCES, parameters);
                }
            }
            catch
            {
                throw;
            }
        }


        public List<DTO_PAYMENT_UPLOAD_YOURDOCES> SelectEventPaymentUploadYourDoces(string creatorID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@CREATOR_ID", creatorID);

                    var result = _context.Database.SqlQuery<DTO_PAYMENT_UPLOAD_YOURDOCES>(ApprovalContext.USP_SELECT_PAYMENT_UPLOAD_YOURDOCES, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        

        public void InsertEventModulePaymentFromYourDoces(string creatorID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@CREATOR_ID", creatorID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_INSERT_MODULE_PAYMENT_FROM_YOURDOCES, parameters);
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
        
        #region [ Payment Concur ]
        public void InsertEventPaymentUploadConcur(DTO_PAYMENT_UPLOAD_CONCUR concur)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[44];
                    parameters[0] = new SqlParameter("@COMPANY_CODE", concur.COMPANY_CODE);
                    parameters[1] = new SqlParameter("@REPORT_ID", concur.REPORT_ID);
                    parameters[2] = new SqlParameter("@EMPLOYEE_ID", concur.EMPLOYEE_ID);
                    parameters[3] = new SqlParameter("@EMPLOYEE", concur.EMPLOYEE);
                    parameters[4] = new SqlParameter("@REPORT_NAME", concur.REPORT_NAME);
                    parameters[5] = new SqlParameter("@TRANSACTION_ID", concur.TRANSACTION_ID);
                    parameters[6] = new SqlParameter("@TRANSACTION_DATE", concur.TRANSACTION_DATE);
                    parameters[7] = new SqlParameter("@EXPENSE_TYPE", concur.EXPENSE_TYPE);
                    parameters[8] = new SqlParameter("@HCP_EXPENSE_TYPE", concur.HCP_EXPENSE_TYPE);
                    parameters[9] = new SqlParameter("@MATERIAL_CODE", concur.MATERIAL_CODE);
                    parameters[10] = new SqlParameter("@CONSULTATION_NUMBER", concur.CONSULTATION_NUMBER);
                    parameters[11] = new SqlParameter("@HCP_TYPE_IDENTIFICATION", concur.HCP_TYPE_IDENTIFICATION);
                    parameters[12] = new SqlParameter("@BUSINESS_PURPOSE", concur.BUSINESS_PURPOSE);
                    parameters[13] = new SqlParameter("@EXPENSE_AMOUNT", concur.EXPENSE_AMOUNT);
                    parameters[14] = new SqlParameter("@REIMBURSEMENT_CURRENCY", concur.REIMBURSEMENT_CURRENCY);
                    parameters[15] = new SqlParameter("@ATTENDEE_AMOUNT", concur.ATTENDEE_AMOUNT);
                    parameters[16] = new SqlParameter("@EVENT_KEY", concur.EVENT_KEY);
                    parameters[17] = new SqlParameter("@LOCAL_CODE", concur.LOCAL_CODE);
                    parameters[18] = new SqlParameter("@HCP_CODE", concur.HCP_CODE);
                    parameters[19] = new SqlParameter("@HCO_CODE", concur.HCO_CODE);
                    parameters[20] = new SqlParameter("@CRM_EVENT_KEY", concur.CRM_EVENT_KEY);
                    parameters[21] = new SqlParameter("@ATTENDEE_NAME", concur.ATTENDEE_NAME);
                    parameters[22] = new SqlParameter("@ATTENDEE_FIRST_NAME", concur.ATTENDEE_FIRST_NAME);
                    parameters[23] = new SqlParameter("@ATTENDEE_LAST_NAME", concur.ATTENDEE_LAST_NAME);
                    parameters[24] = new SqlParameter("@TITLE", concur.TITLE);
                    parameters[25] = new SqlParameter("@COMPANY", concur.COMPANY);
                    parameters[26] = new SqlParameter("@AFFILIATION", concur.AFFILIATION);
                    parameters[27] = new SqlParameter("@ATTENDEE_TYPE", concur.ATTENDEE_TYPE);
                    parameters[28] = new SqlParameter("@EXTERNAL_ID", concur.EXTERNAL_ID);
                    parameters[29] = new SqlParameter("@VENDOR", concur.VENDOR);
                    parameters[30] = new SqlParameter("@LOCATION", concur.LOCATION);
                    parameters[31] = new SqlParameter("@REGION", concur.REGION);
                    parameters[32] = new SqlParameter("@COUNTRY", concur.COUNTRY);
                    parameters[33] = new SqlParameter("@ALLOCATION_CODE", concur.ALLOCATION_CODE);
                    parameters[34] = new SqlParameter("@ALLOCATION_NUMBER", concur.ALLOCATION_NUMBER);
                    parameters[35] = new SqlParameter("@ALLOCATION_AMOUNT", concur.ALLOCATION_AMOUNT);
                    parameters[36] = new SqlParameter("@PAYMENT_TYPE", concur.PAYMENT_TYPE);
                    parameters[37] = new SqlParameter("@RECEIPT_TYPE", concur.RECEIPT_TYPE);
                    parameters[38] = new SqlParameter("@POLICY", concur.POLICY);
                    parameters[39] = new SqlParameter("@COMMENTS", concur.COMMENTS);
                    parameters[40] = new SqlParameter("@STATUS", concur.STATUS);
                    parameters[41] = new SqlParameter("@ERROR_MESSAGE", concur.ERROR_MESSAGE);
                    parameters[42] = new SqlParameter("@IS_DELETED", concur.IS_DELETED);
                    parameters[43] = new SqlParameter("@CREATOR_ID", concur.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_INSERT_PAYMENT_UPLOAD_CONCUR, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteEventPaymentUploadConcur(string updaterID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@UPDATER_ID", updaterID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_DELETE_PAYMENT_UPLOAD_CONCUR, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_PAYMENT_UPLOAD_CONCUR> SelectEventPaymentUploadConcur(string creatorID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@CREATOR_ID", creatorID);

                    var result = _context.Database.SqlQuery<DTO_PAYMENT_UPLOAD_CONCUR>(ApprovalContext.USP_SELECT_PAYMENT_UPLOAD_CONCUR, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }


        public void InsertEventModulePaymentFromConcur(string creatorID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@CREATOR_ID", creatorID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_INSERT_MODULE_PAYMENT_FROM_CONCUR, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_MODULE_PAYMENT_CONCUR> SelectPaymentConcurList(string keyword)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@KEYWORD", keyword);

                    var result = _context.Database.SqlQuery<DTO_MODULE_PAYMENT_CONCUR>(ApprovalContext.USP_SELECT_MODULE_PAYMENT_CONCUR, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public void UpdatePaymentConcur(DTO_MODULE_PAYMENT_CONCUR payment)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@PROCESS_ID", payment.PROCESS_ID);
                    parameters[1] = new SqlParameter("@CONCUR_TRANSACTION_ID", payment.CONCUR_TRANSACTION_ID);
                    parameters[2] = new SqlParameter("@CATEGORY_CODE", payment.CATEGORY_CODE);
                    parameters[3] = new SqlParameter("@UPDATER", payment.UPDATER);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_UPDATE_MODULE_PAYMENT_CONCUR, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_MODULE_PAYMENT_CONCUR> SelectConcurHistory(string processId, string concurId)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processId);
                    parameters[1] = new SqlParameter("@CONCUR_TRANSACTION_ID", concurId);

                    var result = _context.Database.SqlQuery<DTO_MODULE_PAYMENT_CONCUR>(ApprovalContext.USP_SELECT_MODULE_PAYMENT_CONCUR_LOG, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion


        #region [ Admin ICC Master ]

        public List<DTO_PAYMENT_ICC_MASTER> SelectICCMasterList(int iccId)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@ICC_ID", iccId);

                    var result = _context.Database.SqlQuery<DTO_PAYMENT_ICC_MASTER>(ApprovalContext.USP_SELECT_PAYMENT_ICC_MASTER, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public int MergeICCMaster(DTO_PAYMENT_ICC_MASTER iccinfo)
        {
            int retValue = 0;

            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[31];
                    parameters[0] = new SqlParameter("@ICC_ID", iccinfo.ICC_ID);
                    parameters[1] = new SqlParameter("@TYPE", iccinfo.TYPE);
                    parameters[2] = new SqlParameter("@YEAR", iccinfo.YEAR);
                    parameters[3] = new SqlParameter("@START_TIME", iccinfo.START_TIME);
                    parameters[4] = new SqlParameter("@END_TIME", iccinfo.END_TIME);
                    parameters[5] = new SqlParameter("@ADDRESS", iccinfo.ADDRESS);
                    parameters[6] = new SqlParameter("@PURPOSE", iccinfo.PURPOSE);
                    parameters[7] = new SqlParameter("@HCP_CODE", iccinfo.HCP_CODE);
                    parameters[8] = new SqlParameter("@HCP_NAME", iccinfo.HCP_NAME);
                    parameters[9] = new SqlParameter("@HCO_CODE", iccinfo.HCO_CODE);
                    parameters[10] = new SqlParameter("@HCO_NAME", iccinfo.HCO_NAME);
                    parameters[11] = new SqlParameter("@SUBJECT", iccinfo.SUBJECT);
                    parameters[12] = new SqlParameter("@HOST_COUNTRY", iccinfo.HOST_COUNTRY);
                    parameters[13] = new SqlParameter("@INVITING_COUNTRY", iccinfo.INVITING_COUNTRY);
                    parameters[14] = new SqlParameter("@PAYMENT_COUNTRY", iccinfo.PAYMENT_COUNTRY);
                    parameters[15] = new SqlParameter("@PAYMENT_DATE", iccinfo.PAYMENT_DATE);
                    parameters[16] = new SqlParameter("@CURRENCY", iccinfo.CURRENCY);
                    parameters[17] = new SqlParameter("@AMOUNT_CURRENCY", iccinfo.AMOUNT_CURRENCY);
                    parameters[18] = new SqlParameter("@AMOUNT_KOR", iccinfo.AMOUNT_KOR);
                    parameters[19] = new SqlParameter("@FLIGHT_CLASS", iccinfo.FLIGHT_CLASS);
                    parameters[20] = new SqlParameter("@FLIGHT_CHECKIN_DATE", iccinfo.FLIGHT_CHECKIN_DATE);
                    parameters[21] = new SqlParameter("@FLIGHT_CHECKOUT_DATE", iccinfo.FLIGHT_CHECKOUT_DATE);
                    parameters[22] = new SqlParameter("@FLIGHT_COMMENT", iccinfo.FLIGHT_COMMENT);
                    parameters[23] = new SqlParameter("@ACCOMMODATION_CHECKIN_DATE", iccinfo.ACCOMMODATION_CHECKIN_DATE);
                    parameters[24] = new SqlParameter("@ACCOMMODATION_CHECKOUT_DATE", iccinfo.ACCOMMODATION_CHECKOUT_DATE);
                    parameters[25] = new SqlParameter("@ACCOMMODATION_COMMENT", iccinfo.ACCOMMODATION_COMMENT);
                    parameters[26] = new SqlParameter("@AMOUNT_MEAL_BEVERAGE", iccinfo.AMOUNT_MEAL_BEVERAGE);
                    parameters[27] = new SqlParameter("@AMOUNT_TRANSPORTAION", iccinfo.AMOUNT_TRANSPORTAION);
                    parameters[28] = new SqlParameter("@COMMENT", iccinfo.COMMENT);
                    parameters[29] = new SqlParameter("@AGREE_KRPIA", iccinfo.AGREE_KRPIA);
                    parameters[30] = new SqlParameter("@CREATOR_ID", iccinfo.CREATOR_ID);

                    retValue = _context.Database.SqlQuery<int>(ApprovalContext.USP_MERGE_PAYMENT_ICC_MASTER, parameters).First();
                }
            }
            catch
            {
                throw;
            }

            return retValue;
        }



        public int InsertICCAttachFiles(DTO_PAYMENT_ICC_ATTACH_FILES file)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[11];
                    parameters[0] = new SqlParameter("@ICC_ID", file.ICC_ID);
                    parameters[1] = new SqlParameter("@ATTACH_FILE_TYPE", file.ATTACH_FILE_TYPE);
                    parameters[2] = new SqlParameter("@SEQ", file.SEQ);
                    parameters[3] = new SqlParameter("@REFER_IDX", file.REFER_IDX);
                    parameters[4] = new SqlParameter("@DISPLAY_FILE_NAME", file.DISPLAY_FILE_NAME);
                    parameters[5] = new SqlParameter("@SAVED_FILE_NAME", file.SAVED_FILE_NAME);
                    parameters[6] = new SqlParameter("@FILE_SIZE", file.FILE_SIZE);
                    parameters[7] = new SqlParameter("@FILE_PATH", file.FILE_PATH);
                    parameters[8] = new SqlParameter("@FILE_HANDLER_URL", file.FILE_HANDLER_URL);
                    parameters[9] = new SqlParameter("@IS_DELETED", file.IS_DELETED);
                    parameters[10] = new SqlParameter("@CREATOR_ID", file.CREATOR_ID);

                    var result = _context.Database.SqlQuery<int>(ApprovalContext.USP_INSERT_PAYMENT_ICC_ATTACH_FILES, parameters);
                    return result.FirstOrDefault();

                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteICCAttachFiles(int idx, string updaterID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@IDX", idx);
                    parameters[1] = new SqlParameter("@UPDATER_ID", updaterID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_DELETE_PAYMENT_ICC_ATTACH_FILES, parameters);
                }
            }
            catch
            {
                throw;
            }
        }


        public List<DTO_PAYMENT_ICC_ATTACH_FILES> SelectICCAttachFiles(int iccID, string idxs)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@ICC_ID", iccID);
                    parameters[1] = new SqlParameter("@IDXS", idxs);

                    var result = _context.Database.SqlQuery<DTO_PAYMENT_ICC_ATTACH_FILES>(ApprovalContext.USP_SELECT_PAYMENT_ICC_ATTACH_FILES, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region [ Delegation ]

        public void MergeDelegation(DTO_MODULE_DELEGATION delegation)
        {
            using (_context = new ApprovalContext())
            {
                try
                {
                    SqlParameter[] parameters = new SqlParameter[6];
                    parameters[0] = new SqlParameter("@PROCESS_ID", delegation.PROCESS_ID);
                    parameters[1] = new SqlParameter("@USER_ID", delegation.USER_ID);
                    parameters[2] = new SqlParameter("@ROLE", delegation.ROLE);
                    parameters[3] = new SqlParameter("@IS_DELETED", delegation.IS_DELETED);
                    parameters[4] = new SqlParameter("@CREATOR_ID", delegation.CREATOR_ID);
                    parameters[5] = new SqlParameter("@UPDATER_ID", delegation.UPDATER_ID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_MERGE_MODULE_DELEGATION, parameters);

                }
                catch
                {
                    throw;
                }
            }
        }
       
        public List<DTO_MODULE_DELEGATION> SelectDelegation(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_MODULE_DELEGATION>(ApprovalContext.USP_SELECT_MODULE_DELEGATION, parameters);
                    return result.ToList();

                }
            }
            catch
            {
                throw;
            }
        }

        

        public void DeleteDelegation(string processID, string userID, string updaterID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@USER_ID", userID);
                    parameters[2] = new SqlParameter("@UPDATER_ID", updaterID);

                    _context.Database.ExecuteSqlCommand(ApprovalContext.USP_DELETE_MODULE_DELEGATION, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_MODULE_SEARCH_USER> SelectDelegationSearchUser(string processID, string keyword)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@KEYWORD", keyword);

                    var result = _context.Database.SqlQuery<DTO_MODULE_SEARCH_USER>(ApprovalContext.USP_SELECT_MODULE_DELEGATION_USER, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region [ChageHistory]
        // version 1.0.5 Add Change History in Event Form
        public List<DTO_MODULE_CHANGE> SelectChange(string processID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<DTO_MODULE_CHANGE>(ApprovalContext.USP_SELECT_MODULE_CHANGE, parameters);
                    return result.ToList();

                }
            }
            catch
            {
                throw;
            }

        }
        #endregion


        #region [ Process Auth User Check ]
        public string IsApprovalProcessUser(string processID, string userID)
        {
            try
            {
                using (_context = new ApprovalContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processID);
                    parameters[1] = new SqlParameter("@USER_ID", userID);

                    var result = _context.Database.SqlQuery<string>(ApprovalContext.USP_SELECT_DOCUMENT_AUTH_USER, parameters);

                    return result.First();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}

