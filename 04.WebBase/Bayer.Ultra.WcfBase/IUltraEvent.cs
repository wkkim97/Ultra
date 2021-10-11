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
    
    [ServiceContract]
    
    public partial interface IUltraEvent
    {
       
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectHomeNotSubmitted/{requester}")]
        List<HomeNotSubmittedDto> SelectHomeNotSubmitted(string requester);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectHomeApprovalQueue/{currentapp}")]
        List<HomeApprovalQueueDto> SelectHomeApprovalQueue(string currentApp);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectHomePendingApproval/{requester}")]
        List<HomePendingApprovalDto> SelectHomePendingApproval(string requester);

        /// <summary>
        /// Home 결재 건수 조회
        /// </summary>
        /// <param name="requester"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectHomeEventCountSummary/{requester}")]
        List<HomeCountSummaryDto> SelectHomeEventCountSummary(string requester);

        /// <summary>
        /// Home 3 Time Visits 조회
        /// </summary>
        /// <param name="requester"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectHome3TimeVisits/{requester}/{visitDate}")]
        List<Home3TimeVisitsDto> SelectHome3TimeVisits(string requester, string visitDate);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectHomeFromCRMData/{userID}")]
        List<HomeFromCRMDataDto> SelectHomeFromCRMData(string userID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectHomeDelegation/{userID}")]
        List<HomeDelegationDto> SelectHomeDelegation(string userID);


        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectHomeVisitList/{hcp}/{visitDate}")]
        List<HomeVisitListDto> SelectHomeVisitList(string hcp, string visitDate);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectEventbyHCP/{hcp}/{year}")]
        List<DTO_EVENT_BYHCP> SelectEventbyHCP(string hcp, string year);

        /// <summary>
        /// Completed 이벤트 조회 조회 (PROCESS_STATUS : Completed, EventComplete, PaymentComplete)
        /// </summary>
        /// <param name="processStatus"></param>
        /// <param name="userID"></param>
        /// <param name="searchType"></param>
        /// <param name="searchText"></param>
        /// <param name="doc_Start"></param>
        /// <param name="doc_End"></param>
        /// <param name="evt_Start"></param>
        /// <param name="evt_End"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectApprovalCompletedList")]
        List<DTO_PROCESS_COMPLETED_LIST> SelectApprovalCompletedList(string processStatus, string userID, string searchType, string searchText, string doc_Start, string doc_End, string evt_Start, string evt_End);

        /// <summary>
        /// Reject 이벤트 조회 조회 (PROCESS_STATUS : Reject -> EVENT_KEY 존재 안함)
        /// </summary>
        /// <param name="reqID"></param>
        /// <param name="searchType"></param>
        /// <param name="searchText"></param>
        /// <param name="doc_Start"></param>
        /// <param name="doc_End"></param>
        /// <param name="evt_Start"></param>
        /// <param name="evt_End"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectApprovalRejectList")]
        List<DTO_PROCESS_REJECT_LIST> SelectApprovalRejectList(string reqID, string searchType, string searchText, string doc_Start, string doc_End, string evt_Start, string evt_End);

        /// <summary>
        /// Withdraw 이벤트 조회 조회 (PROCESS_STATUS : Withdraw)
        /// </summary>
        /// <param name="reqID"></param>
        /// <param name="searchType"></param>
        /// <param name="searchText"></param>
        /// <param name="doc_Start"></param>
        /// <param name="doc_End"></param>
        /// <param name="evt_Start"></param>
        /// <param name="evt_End"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectApprovalWithdrawList")]
        List<DTO_PROCESS_WITHDRAW_LIST> SelectApprovalWithdrawList(string reqID, string searchType, string searchText, string doc_Start, string doc_End, string evt_Start, string evt_End);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectApprovalCanceledList")]
        List<DTO_PROCESS_COMPLETED_LIST> SelectApprovalCanceledList(string processStatus, string userID, string searchType, string searchText, string doc_Start, string doc_End, string evt_Start, string evt_End);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectApprovalAdminList")]
        List<DTO_PROCESS_COMPLETED_LIST> SelectApprovalAdminList(string userID, string searchType, string searchText, string doc_Start, string doc_End, string evt_Start, string evt_End);

        /// <summary>
        /// 결재자 목록 조회
        /// </summary>
        /// <param name="processID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectApproverList/{processID=null}")]
        List<DTO_PROCESS_APPROVAL_LIST> SelectApproverList(string processID);


        #region [ Excel File Up/Down ] 

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "ExcelFileDown")]
        string ExcelFileDown(string fileName, string userID, string processID);


        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "ExcelFileDownAttendeeList")]
        string ExcelFileDownAttendeeList(string fileName, string userID, string processID);
        #endregion


        #region [ Cost Plan ] 

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "MergeCostplan")]
        string MergeCostPlan(DTO_MODULE_COST_PLAN dto);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "MergeCostPlanList")]
        void MergeCostPlanList(List<DTO_MODULE_COST_PLAN> list);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectCostplan/{processID=null}")]
        List<DTO_MODULE_COST_PLAN> SelectCostPlan(string processID);


        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "DeleteCostplan")]
        void DeleteCostPlan(string processID, int costPlanIDX, string updaterID);

        #endregion

        #region [ Participants ]

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "InsertParticipants")]
        string InsertParticipants(List<DTO_MODULE_PARTICIPANTS> participants);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectParticipants/{processID=null}")]
        List<DTO_MODULE_PARTICIPANTS> SelectParticipants(string processID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectParticipantsByEventKey/{eventKey=null}")]
        List<DTO_MODULE_PARTICIPANTS> SelectParticipantsByEventKey(string eventKey);


        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "UpdateParticipantStatus")]
        void UpdateParticipantStatus(string processID, int[] indexes, string isAttended, string updaterID);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "DeleteParticipant")]
        void DeleteParticipant(string processID, int[] indexes, string updaterID);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectParticipantsByContract")]
        List<HealthCareProviderDto> SelectParticipantsByContract(string impactNo, string processID);

        #endregion

        #region [ Approval ]

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectApprovalLine/{eventID}/{userID}/{processID}")]
        List<DTO_APPROVAL_LINE> SelectApprovalLine(string eventID, string userID, string processID);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "InsertAdditionalReviewer")]
        void InsertAdditionalReviewer(List<DTO_PROCESS_APPROVER_ADDTIONAL> additional);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "InsertProcessApprove")]
        void InsertProcessApprove(RequestApprovalDto requestApproval);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "UpdateProcessStatus")]
        void UpdateProcessStatus(string eventID, string processID, string comment, string processStatus, string userID, string approverStatus);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "UpdateProcessCompleted")]
        void UpdateProcessCompleted(string eventID, string processID, string processStatus, string userID);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "InsertProcessEvent")]
        void InsertProcessEvent(DTO_PROCESS_EVENT processEvent);

        /// <summary>
        /// 결재 Forward
        /// </summary>
        /// <param name="reviewers"></param>
        /// <param name="comment"></param>
        /// <param name="userID"></param>
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "InsertForward")]
        void InsertForward(List<DTO_PROCESS_APPROVER_COMPLETED> receivers, string comment, string userID);

        /// <summary>
        /// Forwad Approval 
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="processID"></param>
        /// <param name="userID"></param>
        /// <param name="approverID"></param>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "InsertForwardApproval/{eventID}/{processID}/{userID}/{approverID}")]
        void InsertForwardApproval(string eventID, string processID, string userID, string approverID);

        /// <summary>
        /// Input Comment
        /// </summary>
        /// <param name="processID"></param>
        /// <param name="comment"></param>
        /// <param name="userID"></param>
        /// <param name="logType"></param>
        /// <param name="uploadTempPath"></param>
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "InsertInputComment")]
        void InsertInputComment(string processID, string commentCategory, string comment, string userID, string logType, EventAttachFileResponseDto attachFile, string sendMailApproverId);

        /// <summary>
        /// Withdraw 처리
        /// </summary>
        /// <param name="eventID"></param>
        /// <param name="processID"></param>
        /// <param name="comment"></param>
        /// <param name="processStatus"></param>
        /// <param name="userID"></param>
        /// <param name="approverStatus"></param>
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "DoWithdraw")]
        void DoWithdraw(string eventID, string processID, string comment, string processStatus, string userID, string approverStatus);
        #endregion

        #region [ Event Complete]

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "DoEventComplete")]
        void DoEventComplete(string eventID, string processID, string commentCategory, string comment, string processStatus, string userID, EventAttachFileResponseDto attachFile);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "DeleteEventProcess/{processID}")]
        void DeleteEventProcess(string processID);

        #endregion

        #region [ Payment Complete ]

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "DoPaymentComplete")]
        void DoPaymentComplete(string eventID, string processID, string processStatus, string userID);
        #endregion

        #region [ Recall ]

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "UpdateRecall")]
        void UpdateRecall(string eventID, string processID, string comment, string userID, string logType, string processStatus);
        #endregion

        #region [ Event Attach ]

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectEventAttachFiles/{processID}")]
        List<DTO_EVENT_ATTACH_FILES> SelectEventAttachFiles(string processID);

        #endregion


        #region [ Event Attach IDXS ]

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectEventAttachFilesIdxs/{processID}/{idxs}")]
        List<DTO_EVENT_ATTACH_FILES> SelectEventAttachFilesIdxs(string processID, string idxs);

        #endregion

        #region [ Agenda ]

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "MergeAgenda")]
        string MergeAgenda(DTO_MODULE_AGENDA agenda);


        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "DeleteAgenda/{processID}/{agendaIDX}/{updaterID}")]
        void DeleteAgenda(string processID, string agendaIDX, string updaterID);


        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectAgenda/{processID}")]
        List<DTO_MODULE_AGENDA> SelectAgenda(string processID);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "MergeAgendaRole")]
        int MergeAgendaRole(DTO_MODULE_AGENDA_ROLE agendaRole);


        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "DeleteAgendaRole/{processID}/{agendaIDX}/{agendaRoleIDX}/{updaterID}")]
        void DeleteAgendaRole(string processID, string agendaIDX, string agendaRoleIDX, string updaterID);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "UpdateMaterialCode")]
        void UpdateMaterialCode(string processID, string agenda_role_idx, string mcode, string userId, string costcenter,string sap_no,string krpia);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectAgendaRole/{processID}")]
        List<DTO_MODULE_AGENDA_ROLE> SelectAgendaRole(string processID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectAgendaParticipants/{processID}/{participantIDX}")]
        List<DTO_MODULE_PARTICIPANTS> SelectAgendaParticipants(string processID, string participantIDX);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectAgendaRoleInfo/{processID}/{agendaIDX}/{agendaRoleIDX}")]
        DTO_MODULE_AGENDA_ROLE SelectAgendaRoleInfo(string processID, string agendaIDX, string agendaRoleIDX);


        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "SelectAgendaRoleInfoPrint/{processID}/{agendaIDX}/{agendaRoleIDX}")]
        ModuleAgendaRuleDto SelectAgendaRoleInfoPrint(string processID, string agendaIDX, string agendaRoleIDX);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectAgendaRoleAmountForHCP")]
        List<DTO_MODULE_AGENDA_ROLE> SelectAgendaRoleAmountForHCP(string hcpCode, string processID, string roleType, string role);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectAgendaRoleSummary/{processID}")]
        List<EventAgendaSummaryDto> SelectAgendaRoleSummary(string processID);

        #endregion

        #region [ Payment ]

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectEventPaymentByHCP/{processID}")]
        List<EventPaymentByHCPDto> SelectEventPaymentByHCP(string processID);


        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectEventPaymentPlanActual/{processID}")]
        List<EventPaymentPlanActualDto> SelectEventPaymentPlanActual(string processID);


        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "ReadEventPaymentUploadExcel")]
        List<DTO_PAYMENT_UPLOAD_SRM> ReadEventPaymentUploadExcel(string processID, string userID, string filePath);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectEventPaymentSavedSRM")]
        List<DTO_PAYMENT_UPLOAD_SRM> SelectEventPaymentSavedSRM(string processID, string poNumber);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "GetEventPaymentUploadSRMTotal/{poNumber}")]
        DTO_PAYMENT_UPLOAD_SRM_TOTAL GetEventPaymentUploadSRMTotal(string poNumber);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "InsertEventpaymentUploadSRMHistory")]
        void InsertEventpaymentUploadSRMHistory(string processID, string userID, DTO_PAYMENT_UPLOAD_SRM_HISTORY history);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectEventPaymentUploadSRMHistory/{processID}")]
        List<DTO_PAYMENT_UPLOAD_SRM_HISTORY> SelectEventPaymentUploadSRMHistory(string processID);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "DeleteEventPaymentUploadSRMHistory")]
        void DeleteEventPaymentUploadSRMHistory(string processID, string poNumber, string updaterID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectEventPaymentLayer/{processID}")]
        List<EventPaymentLayerDto> SelectEventPaymentLayer(string processID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectEventPaymentLayerByHCP/{processID}/{hcpCode}/{dataSource}")]
        List<EventPaymentLayerDto> SelectEventPaymentLayerByHCP(string processID, string hcpCode, string dataSource);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "MergeEventPaymentInputSRM")]
        void MergeEventPaymentInputSRM(DTO_PAYMENT_INPUT_SRM srm);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectEventPaymentInputSRM/{processID}")]
        List<DTO_PAYMENT_INPUT_SRM> SelectEventPaymentInputSRM(string processID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "DeleteEventPaymentInputSRM/{processID}/{srmIDX}/{updaterID}")]
        void DeleteEventPaymentInputSRM(string processID, string srmIDX, string updaterID);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "InsertEventPaymentUploadConcur")]
        string InsertEventPaymentUploadConcur(string userID, string filePath);


        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "InsertEventPaymentUploadYourDoces")]
        string InsertEventPaymentUploadYourDoces(string userID, string filePath);


        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "ReadEventPaymentUploadConcur/{userID}")]
        List<DTO_PAYMENT_UPLOAD_CONCUR> ReadEventPaymentUploadConcur(string userID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "ReadEventPaymentUploadYourDoces/{userID}")]
        List<DTO_PAYMENT_UPLOAD_YOURDOCES> ReadEventPaymentUploadYourDoces(string userID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "InsertEventModulePaymentFromConcur/{creatorID}")]
        void InsertEventModulePaymentFromConcur(string creatorID);


        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "InsertEventModulePaymentFromYourDoces/{creatorID}")]
        void InsertEventModulePaymentFromYourDoces(string creatorID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectPaymentConcurList/{keyword=null}")]
        List<DTO_MODULE_PAYMENT_CONCUR> SelectPaymentConcurList(string keyword);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "UpdatePaymentConcur")]
        void UpdatePaymentConcur(DTO_MODULE_PAYMENT_CONCUR concur);


        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectConcurHistory/{processId}/{concurId}")]
        List<DTO_MODULE_PAYMENT_CONCUR> SelectConcurHistory(string processId, string concurId);


        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectICCMasterList/{iccId=null}")]
        List<DTO_PAYMENT_ICC_MASTER> SelectICCMasterList(string iccId);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "MergeICCMaster")]
        void MergeICCMaster(DTO_PAYMENT_ICC_MASTER iccinfo, List<EventAttachFileResponseDto> attachFiles);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectICCAttachFiles/{iccId}/{idxs=null}")]
        List<DTO_PAYMENT_ICC_ATTACH_FILES> SelectICCAttachFiles(string iccId, string idxs);
        #endregion

        #region [ Delegation ]

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "MergeDelegation")]
        void MergeDelegation(List<DTO_MODULE_DELEGATION> delegation);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "DeleteDelegation")]
        void DeleteDelegation(string processID, string[] userIDs, string updaterID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectDelegation/{processID}")]
        List<DTO_MODULE_DELEGATION> SelectDelegation(string processID);

        // version 1.0.5 Add Change History in Event Form
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectChange/{processID}")]
        List<DTO_MODULE_CHANGE> SelectChange(string processID);



        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectDelegationSearchUser/{processID}/{keyword=null}")]
        List<DTO_MODULE_SEARCH_USER> SelectDelegationSearchUser(string processID, string keyword);

        #endregion

        #region [ Product Briefing ]

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "MergeProductBriefing")]
        void MergeProductBriefing(DTO_EVENT_PRODUCT_BRIEFING briefing, List<DTO_EVENT_PRODUCT_BRIEFING_PRODUCT> products);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectProductBriefing/{processID}")]
        DTO_EVENT_PRODUCT_BRIEFING SelectProductBriefing(string processID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectProductBriefingProducts/{processID}")]
        List<DTO_EVENT_PRODUCT_BRIEFING_PRODUCT> SelectProductBriefingProducts(string processID);

        #endregion

        #region [ Product Seminar ]

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "MergeProductSeminar")]
        void MergeProductSeminar(DTO_EVENT_PRODUCT_SEMINAR seminar, List<DTO_EVENT_PRODUCT_SEMINAR_PRODUCT> products);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectProductSeminar/{processID}")]
        DTO_EVENT_PRODUCT_SEMINAR SelectProductSeminar(string processID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectProductSeminarProducts/{processID}")]
        List<DTO_EVENT_PRODUCT_SEMINAR_PRODUCT> SelectProductSeminarProducts(string processID);

        #endregion

        #region [ Product Presentation Meeting ]

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "MergeProductPresentation")]
        void MergeProductPresentation(DTO_EVENT_PRODUCT_PRESENTATION presentation, List<DTO_EVENT_PRODUCT_PRESENTATION_PRODUCT> products);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectProductPresentation/{processID}")]
        DTO_EVENT_PRODUCT_PRESENTATION SelectProductPresentation(string processID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectProductPresentationProducts/{processID}")]
        List<DTO_EVENT_PRODUCT_PRESENTATION_PRODUCT> SelectProductPresentationProducts(string processID);

        #endregion

        #region [ Scientific Exchanged Meeting ]

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "MergeScientificExchangedMeeting")]
        void MergeScientificExchangedMeeting(DTO_EVENT_SCIENTIFIC_MEETING SEMeeting, List<DTO_EVENT_SCIENTIFIC_MEETING_PRODUCT> products);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectScientificExchangedMeeting/{processID}")]
        DTO_EVENT_SCIENTIFIC_MEETING SelectScientificExchangedMeeting(string processID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectScientificExchangedMeetingProducts/{processID}")]
        List<DTO_EVENT_SCIENTIFIC_MEETING_PRODUCT> SelectScientificExchangedMeetingProducts(string processID);
        #endregion


        #region [ FreeGood ]

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "InsertFreeGoodHCP")]
        string InsertFreeGoodHCP(EventFreeGoodDto dto);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "DeleteFreeGoodHCP/{processId}")]
        void DeleteFreeGoodHCP(string processId);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectFreeGood/{processID}")]
        DTO_EVENT_FREE_GOOD SelectFreeGood(string processID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectFreeGoodHCP/{type}/{processID}")]
        List<EventFreeGoodHcpListDto> SelectFreeGoodHCP(string type, string processID);


        //  <!-- Ver 1.0.7 : Go-Direct --> : type 파라미터 추가 : HCP 또는 HCO
        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "IsExistsFreeGoodHcpItem/{processID}/{hcpCode}/{hcoCode}/{sampleCode}/{type}")]
        bool IsExistsFreeGoodHcpItem(string processID, string hcpCode, string hcoCode, string sampleCode, string type);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectExistFreeGoodSample")]
        List<FreeGoodExistSampleDto> SelectExistFreeGoodSample(List<DTO_EVENT_FREE_GOOD_HCP> checkList);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectExistFreeGoodSampleRAD")]
        List<FreeGoodExistSampleDto> SelectExistFreeGoodSampleRAD(List<DTO_EVENT_FREE_GOOD_HCP> checkList);
        #endregion


        #region [ Clinical Related Meeting ]

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "MergeClinicalRelatedMeeting")]
        void MergeClinicalRelatedMeeting(DTO_EVENT_CLINICAL_MEETING CRMeeting);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectClinicalRelatedMeeting/{processID}")]
        DTO_EVENT_CLINICAL_MEETING SelectClinicalRelatedMeeting(string processID);

        #endregion

        #region [ Congress Events ]

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "MergeCongressEvent")]
        void MergeCongressEvent(DTO_EVENT_CONGRESS Congress);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectCongressEvent/{processID}")]
        DTO_EVENT_CONGRESS SelectCongressEvent(string processID);

        #endregion

        #region [ Adventisement / Booth ]

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "MergeAdventiseBooth")]
        void MergeAdventiseBooth(DTO_EVENT_ADVENTISEMENT Adventise);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectAdventiseBooth/{processID}")]
        DTO_EVENT_ADVENTISEMENT SelectAdventiseBooth(string processID);

        #endregion

        #region [ Employee  ]

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "MergeEmployeeTraining")]
        void MergeEmployeeTraining(DTO_EVENT_EMPLOYEE_TRAINING employee);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectEmployeeTraining/{processID}")]
        DTO_EVENT_EMPLOYEE_TRAINING SelectEmployeeTraining(string processID);

        #endregion

        #region [ Consulting/ABM(Medical MSL) Meeting ]

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "MergeConsultingMeeting")]
        void MergeConsultingMeeting(DTO_EVENT_CONSULTING_MEETING consulting);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectConsultingMeeting/{processID}")]
        DTO_EVENT_CONSULTING_MEETING SelectConsultingMeeting(string processID);

        #endregion


        #region [ Donation ]

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "MergeDonation")]
        void MergeDonation(DTO_EVENT_DONATION donation, List<DTO_EVENT_DONATION_PRODUCT> products);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectDonation/{processID}")]
        DTO_EVENT_DONATION SelectDonation(string processID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "selectDonationProducts/{processID}")]
        List<DTO_EVENT_DONATION_PRODUCT> selectDonationProducts(string processID);

        #endregion


        #region [Change_Value]
        // version 1.0.5 Admin Event Page for change by DM Team
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "UpdateChangeValue")]
        void UpdateChangeValue(string PROCESS_ID, string CATEGORY, string ADJUSTMENT_AREA, string OLD_VALUE,string NEW_VALUE, string REASON, string UPDATER_ID);

        #endregion

    }
}
