using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Approval
{
    public partial class ApprovalContext : Framework.Database.UltraDbContext
    {
        #region USP_CREATE_PROCESS_ID
        public const string USP_CREATE_PROCESS_ID = "[dbo].[USP_CREATE_PROCESS_ID]";
        #endregion

        public const string USP_SELECT_APPROVAL_LINE = "[dbo].[USP_SELECT_APPROVAL_LINE] @EVENT_ID, @USER_ID, @PROCESS_ID";

        public const string USP_DELETE_PROCESS_APPROVER = "[dbo].[USP_DELETE_PROCESS_APPROVER] @PROCESS_ID";

        public const string USP_INSERT_PROCESS_APPROVER = "[dbo].[USP_INSERT_PROCESS_APPROVER] @PROCESS_ID, @APPROVAL_TYPE, @APPROVAL_SEQ, @APPROVER_TYPE, @APPROVER_ID, @APPROVER_ORG_NAME, @ABSENCE_APPROVER_ID, @ABSENCE_APPROVER_ORG_NAME, @STATUS, @SENT_MAIL, @APPROVAL_DATE, @COMMENT, @CREATE_DATE, @UPDATE_DATE";

        public const string USP_INSERT_PROCESS_EVENT = "[dbo].[USP_INSERT_PROCESS_EVENT] @PROCESS_ID, @EVENT_ID, @EVENT_NAME, @SUBJECT, @START_DATE, @EVENT_KEY, @PROCESS_STATUS, @REQUEST_DATE, @COMPANY_CODE, @REQUESTER_ID, @CURRENT_APPROVER, @FINAL_APPROVER, @REJECTED_PROCESS_ID, @CREATE_DATE, @UPDATE_DATE";

        public const string USP_INSERT_PROCESS_APPROVER_ADDTIONAL = "[dbo].[USP_INSERT_PROCESS_APPROVER_ADDTIONAL] @PROCESS_ID, @IDX, @APPROVAL_TYPE, @USER_ID";

        public const string USP_DELETE_PROCESS_APPROVER_ADDTIONAL = "[dbo].[USP_DELETE_PROCESS_APPROVER_ADDTIONAL] @PROCESS_ID, @APPROVAL_TYPE";

        public const string USP_SELECT_PROCESS_EVENT = "[dbo].[USP_SELECT_PROCESS_EVENT] @PROCESS_ID";

        #region [ USP_UPDATE_EVENT_PROCESS_STATUS ]
        public const string USP_UPDATE_EVENT_PROCESS_STATUS = "[DBO].[USP_UPDATE_EVENT_PROCESS_STATUS] @EVENT_ID, @PROCESS_ID, @PROCESS_STATUS, @APPROVER_ID";
        #endregion

        #region [ Home 화면 결재 건수 조회 ]
        /// <summary>
        /// 결재 건수 (NOT_SUBMITTED, APPROVAL_QUEUE, PENDING_APPROVAL, DELEGATION)
        /// </summary>
        public const string USP_SELECT_HOME_EVENT_COUNT_SUMMARY = "[dbo].[USP_SELECT_HOME_EVENT_COUNT_SUMMARY] @REQUESTER_ID";
        #endregion

        #region [ Home 화면 결재 정보 조회 ]
        /// <summary>
        /// 저장된 결재(Request 하지 않음)
        /// </summary>
        public const string USP_SELECT_HOME_NOT_SUBMITTED = "[dbo].[USP_SELECT_HOME_NOT_SUBMITTED] @REQUESTER_ID";

        /// <summary>
        /// 내가 결재 해야 할 결재
        /// </summary>
        public const string USP_SELECT_HOME_APPROVAL_QUEUE = "[dbo].[USP_SELECT_HOME_APPROVAL_QUEUE] @CURRENT_APPROVER";

        /// <summary>
        /// 내가 요청한 결재
        /// </summary>
        public const string USP_SELECT_HOME_PENDING_APPROVAL = "[dbo].[USP_SELECT_HOME_PENDING_APPROVAL] @REQUESTER_ID";

        /// <summary>
        /// 내가 위임받은 결재(참석자를 입력한다.)
        /// </summary>
        public const string USP_SELECT_HOME_DELEGATION = "[dbo].[USP_SELECT_HOME_DELEGATION] @USER_ID";
        #endregion

        #region [ USP_SELECT_HOME_3TIME_VISITS ]
        public const string USP_SELECT_HOME_VISIT_COUNT = "[dbo].[USP_SELECT_HOME_VISIT_COUNT] @VISIT_DATE";
        #endregion

        #region [ Home 화면 From CRM Data - USP_SELECT_HOME_CRM_DATA ]
        public const string USP_SELECT_HOME_CRM_DATA = "[dbo].[USP_SELECT_HOME_CRM_DATA] @USER_ID";
        #endregion

        #region [ USP_SELECT_HOME_HCP_INFO - 의사별 방문횟수 조회]
        public const string USP_SELECT_HOME_HCP_INFO = "[dbo].[USP_SELECT_HOME_HCP_INFO] @HCP, @VISIT_DATE";
        #endregion

        #region [ USP_SELECT_ALL_EVENT_BY_HCP_NAME - 의사별 방문 조회]
        public const string USP_SELECT_ALL_EVENT_BY_HCP_NAME = "[dbo].[USP_SELECT_ALL_EVENT_BY_HCP_NAME] @HCP_NAME";
        #endregion

        #region [ USP_SELECT_PROCESS_APPROVER_USER ]
        public const string USP_SELECT_PROCESS_APPROVER_USER = "[dbo].[USP_SELECT_PROCESS_APPROVER_USER] @PROCESS_ID, @APPROVER_ID";
        #endregion

        #region [ USP_SELECT_PROCESS_EVENT_STATUS ]
        public const string USP_SELECT_PROCESS_EVENT_STATUS = "[dbo].[USP_SELECT_PROCESS_EVENT_STATUS] @PROCESS_ID";
        #endregion

        #region [ USP_UPDATE_PROCESS_EVENT_STATUS ]
        public const string USP_UPDATE_PROCESS_EVENT_STATUS = "[dbo].[USP_UPDATE_PROCESS_EVENT_STATUS] @PROCESS_ID, @APPROVER_ID, @PROCESS_STATUS";
        #endregion

        public const string USP_UPDATE_PROCESS_EVENT_STATUS_FOR_SKIP_APPROVAL = "[dbo].[USP_UPDATE_PROCESS_EVENT_STATUS_FOR_SKIP_APPROVAL] @PROCESS_ID, @APPROVER_ID, @PROCESS_STATUS";

        #region [ USP_UPDATE_PROCESS_APPROVER ]
        public const string USP_UPDATE_PROCESS_APPROVER = "[dbo].[USP_UPDATE_PROCESS_APPROVER] @PROCESS_ID, @APPROVER_ID, @STATUS, @COMMENT";
        #endregion

        #region [ USP_SELECT_DOCUMENT_FINAL_APPROVER_ID_LINE ]
        public const string USP_SELECT_EVENT_FINAL_APPROVER_ID_LINE = "[dbo].[USP_SELECT_EVENT_FINAL_APPROVER_ID_LINE] @PROCESS_ID";
        #endregion

        #region [ USP_UPDATE_PROCESS_COMPLETED ]
        public const string USP_UPDATE_PROCESS_COMPLETED = "[dbo].[USP_UPDATE_PROCESS_COMPLETED] @EVENT_ID, @PROCESS_ID, @PROCESS_STATUS, @APPROVER_ID";
        #endregion

        #region [ USP_INSERT_PROCESS_APPROVER_COMPLETED ]
        public const string USP_INSERT_PROCESS_APPROVER_COMPLETED = "[dbo].[USP_INSERT_PROCESS_APPROVER_COMPLETED] @PROCESS_ID";
        #endregion

        #region [ USP_INSERT_PROCESS_APPROVER_FORWARDAPPROVAL_USER ]
        public const string USP_INSERT_PROCESS_APPROVER_FORWARDAPPROVAL_USER = "[dbo].[USP_INSERT_PROCESS_APPROVER_FORWARDAPPROVAL_USER] @PROCESS_ID, @USER_ID, @APPROVER_ID";
        #endregion

        #region [ USP_UPDATE_PROCESS_EVENT_FINAL_APPROVER ]
        public const string USP_UPDATE_PROCESS_EVENT_FINAL_APPROVER = "[dbo].[USP_UPDATE_PROCESS_EVENT_FINAL_APPROVER] @PROCESS_ID, @APPROVER_ID";
        #endregion

        #region [ USP_SELECT_APPROVAL_COMPLETED_LIST ]
        public const string USP_SELECT_APPROVAL_COMPLETED_LIST = "[dbo].[USP_SELECT_APPROVAL_COMPLETED_LIST] @STATUS, @USER_ID, @SEARCH_TYPE, @SEARCH_TEXT, @DOC_START, @DOC_END, @EVT_START, @EVT_END";
        #endregion

        #region [ USP_SELECT_APPROVAL_PAYMENTCOMPLETED_LIST ]
        public const string USP_SELECT_APPROVAL_PAYMENTCOMPLETED_LIST = "[dbo].[USP_SELECT_APPROVAL_PAYMENTCOMPLETED_LIST] @STATUS, @USER_ID, @SEARCH_TYPE, @SEARCH_TEXT, @DOC_START, @DOC_END, @EVT_START, @EVT_END"; 
        #endregion

        #region [ USP_SELECT_APPROVAL_REJECT_LIST ]
        public const string USP_SELECT_APPROVAL_REJECT_LIST = "[dbo].[USP_SELECT_APPROVAL_REJECT_LIST] @REQUESTER_ID, @SEARCH_TYPE, @SEARCH_TEXT, @DOC_START, @DOC_END, @EVT_START, @EVT_END";
        #endregion

        #region [ USP_SELECT_APPROVAL_WITHDRAW_LIST ]
        public const string USP_SELECT_APPROVAL_WITHDRAW_LIST = "[dbo].[USP_SELECT_APPROVAL_WITHDRAW_LIST] @REQUESTER_ID, @SEARCH_TYPE, @SEARCH_TEXT, @DOC_START, @DOC_END, @EVT_START, @EVT_END";
        #endregion

        #region [ USP_SELECT_APPROVAL_CANCEL_LIST ]
        public const string USP_SELECT_APPROVAL_CANCEL_LIST = "[dbo].[USP_SELECT_APPROVAL_CANCEL_LIST] @STATUS, @USER_ID, @SEARCH_TYPE, @SEARCH_TEXT, @DOC_START, @DOC_END, @EVT_START, @EVT_END";
        #endregion

        #region  USP_SELECT_APPROVAL_ADMIN_LIST ]
        public const string USP_SELECT_APPROVAL_ADMIN_LIST = "[dbo].[USP_SELECT_APPROVAL_ADMIN_LIST] @USER_ID, @SEARCH_TYPE, @SEARCH_TEXT, @DOC_START, @DOC_END, @EVT_START, @EVT_END"; 
        #endregion

        #region [ USP_INSERT_LOG_EVENT ]
        public const string USP_INSERT_LOG_EVENT = "[dbo].[USP_INSERT_LOG_EVENT] @PROCESS_ID, @REGISTER_ID, @LOG_TYPE, @LOG_CATEGORY, @COMMENT";
        #endregion

        #region [ USP_MERGE_PROCESS_APPROVER_COMPLETED_ADD_REVIEWER ]
        public const string USP_MERGE_PROCESS_APPROVER_COMPLETED_ADD_REVIEWER = "[dbo].[USP_MERGE_PROCESS_APPROVER_COMPLETED_ADD_REVIEWER] @PROCESS_ID, @APPROVAL_TYPE, @APPROVAL_SEQ, @APPROVER_TYPE, @APPROVER_ID, @APPROVER_ORG_NAME, @ABSENCE_APPROVER_ID, @ABSENCE_APPROVER_ORG_NAME, @STATUS, @SENT_MAIL, @APPROVAL_DATE, @COMMENT";
        #endregion

        #region [ USP_SELECT_PROCESS_APPROVER_LIST ]
        public const string USP_SELECT_PROCESS_APPROVER_LIST = "[dbo].[USP_SELECT_PROCESS_APPROVER_LIST] @PROCESS_ID";
        #endregion

        public const string USP_SELECT_DOCUMENT_AUTH_USER = "[dbo].[USP_SELECT_DOCUMENT_AUTH_USER] @PROCESS_ID, @USER_ID";

        #region [ USP_SELECT_LOG_EVENT_LIST ]
        public const string USP_SELECT_LOG_EVENT_LIST = "[dbo].[USP_SELECT_LOG_EVENT_LIST] @PROCESS_ID";
        #endregion

        #region [ USP_DELETE_EVENT_PROCESS ]
        public const string USP_DELETE_EVENT_PROCESS = "[dbo].[USP_DELETE_EVENT_PROCESS] @PROCESS_ID"; 
        #endregion

        #region [ Cost Plan ]

        #region [ USP_MERGE_MODULE_COST_PLAN ] 
        public const string USP_MERGE_MODULE_COST_PLAN = "[dbo].[USP_MERGE_MODULE_COST_PLAN] @EVENT_ID, @PROCESS_ID, @COST_PLAN_IDX, @CATEGORY_CODE, @QTY, @PRICE, @DESC, @IS_DELETED, @CREATOR_ID, @UPDATER_ID";
        #endregion

        #region [ USP_SELECT_MODULE_COST_PLAN ] 
        public const string USP_SELECT_MODULE_COST_PLAN = "[dbo].[USP_SELECT_MODULE_COST_PLAN] @PROCESS_ID";
        #endregion

        #region [ USP_DELETE_MODULE_COST_PLAN ]
        public const string USP_DELETE_MODULE_COST_PLAN = "[dbo].[USP_DELETE_MODULE_COST_PLAN] @PROCESS_ID, @COST_PLAN_IDX, @UPDATER_ID";
        #endregion

        #endregion

        #region [ Participants ]

        #region [ USP_INSERT_MODULE_PARTICIPANTS ]
        public const string USP_INSERT_MODULE_PARTICIPANTS = "[dbo].[USP_INSERT_MODULE_PARTICIPANTS] @EVENT_ID, @PROCESS_ID, @PARTICIPANT_IDX, @PARTICIPANT_TYPE, @HCP_TYPE, @HCP_CODE, @HCP_NAME, @HCO_CODE, @HCO_NAME, @SPECIALTY_CODE, @SPECIALTY_NAME, @COUNTRY_CODE, @COUNTRY_NAME, @ROLE, @DATA_SOURCE, @IS_ATTENDED, @IS_DELETED, @CREATOR_ID";
        #endregion

        #region [ USP_SELECT_MODULE_PARTICIPANTS ]
        public const string USP_SELECT_MODULE_PARTICIPANTS = "[dbo].[USP_SELECT_MODULE_PARTICIPANTS] @PROCESS_ID";
        #endregion

        #region [ USP_SELECT_MODULE_PARTICIPANTS_BY_EVENTKEY ]
        public const string USP_SELECT_MODULE_PARTICIPANTS_BY_EVENTKEY = "[dbo].[USP_SELECT_MODULE_PARTICIPANTS_BY_EVENTKEY] @EVENT_KEY";
        #endregion

        #region [ USP_SELECT_EVENT_INFO_FOR_ATTENDEES_LIST ] 
        public const string USP_SELECT_EVENT_INFO_FOR_ATTENDEES_LIST = "[dbo].[USP_SELECT_EVENT_INFO_FOR_ATTENDEES_LIST] @PROCESS_ID";
        #endregion


        #region [ USP_UPDATE_MODULE_PARTICIPANT_STATUS ]
        public const string USP_UPDATE_MODULE_PARTICIPANT_STATUS = "[dbo].[USP_UPDATE_MODULE_PARTICIPANT_STATUS] @PROCESS_ID, @PARTICIPANT_IDX, @IS_ATTENDED, @UPDATER_ID";
        #endregion

        #region [ USP_DELETE_MODULE_PARTICIPANT ]
        public const string USP_DELETE_MODULE_PARTICIPANT = "[dbo].[USP_DELETE_MODULE_PARTICIPANT] @PROCESS_ID, @PARTICIPANT_IDX, @UPDATER_ID";
        #endregion

        #region [ USP_SELECT_MODULE_PARTICIPANTS_CONTRACT ]
        public const string USP_SELECT_MODULE_PARTICIPANTS_CONTRACT = "[dbo].[USP_SELECT_MODULE_PARTICIPANTS_CONTRACT] @IMPACT_NO, @PROCESS_ID"; 
        #endregion

        #endregion

        #region [ Agenda ]

        #region [ USP_MERGE_MODULE_AGENDA ]
        public const string USP_MERGE_MODULE_AGENDA = "[dbo].[USP_MERGE_MODULE_AGENDA] @EVENT_ID, @PROCESS_ID, @AGENDA_IDX, @DAY, @START_TIME, @DURATION, @SUBJECT, @IS_DELETED, @CREATOR_ID, @UPDATER_ID";
        #endregion

        #region [ USP_DELETE_MODULE_AGENDA ] 
        public const string USP_DELETE_MODULE_AGENDA = "[dbo].[USP_DELETE_MODULE_AGENDA] @PROCESS_ID, @AGENDA_IDX, @UPDATER_ID";
        #endregion

        #region [ USP_SELECT_MODULE_AGENDA ] 
        public const string USP_SELECT_MODULE_AGENDA = "[dbo].[USP_SELECT_MODULE_AGENDA] @PROCESS_ID";
        #endregion

        #region [ USP_MERGE_MODULE_AGENDA_ROLE ]
        public const string USP_MERGE_MODULE_AGENDA_ROLE = "[dbo].[USP_MERGE_MODULE_AGENDA_ROLE] @PROCESS_ID, @AGENDA_IDX, @AGENDA_ROLE_IDX, @PARTICIPANT_IDX, @PARTICIPANT_CODE, @ROLE_TYPE, @ROLE, @CRITERIA, @REASON, @AMOUNT, @MATERIAL_CODE, @IS_DELETED, @CREATOR_ID, @UPDATER_ID";
        #endregion

        #region [ USP_DELETE_MODULE_AGENDA_ROLE ]
        public const string USP_DELETE_MODULE_AGENDA_ROLE = "[dbo].[USP_DELETE_MODULE_AGENDA_ROLE] @PROCESS_ID, @AGENDA_IDX, @AGENDA_ROLE_IDX, @UPDATER_ID";
        #endregion

        #region [ USP_SELECT_MODULE_AGENDA_ROLE ]
        public const string USP_SELECT_MODULE_AGENDA_ROLE = "[dbo].[USP_SELECT_MODULE_AGENDA_ROLE] @PROCESS_ID";
        #endregion

        #region [ USP_SELECT_MODULE_AGENDA_PARTICIPANTS ]
        public const string USP_SELECT_MODULE_AGENDA_PARTICIPANTS = "[dbo].[USP_SELECT_MODULE_AGENDA_PARTICIPANTS] @PROCESS_ID, @PARTICIPANT_IDX";
        #endregion

        #region [ USP_SELECT_MODULE_AGENDA_ROLE_INFO ]
        public const string USP_SELECT_MODULE_AGENDA_ROLE_INFO = "[dbo].[USP_SELECT_MODULE_AGENDA_ROLE_INFO] @PROCESS_ID, @AGENDA_IDX, @AGENDA_ROLE_IDX";
        #endregion

        #region [ USP_SELECT_MODULE_AGENDA_ROLE_PRINT ]
        public const string USP_SELECT_MODULE_AGENDA_ROLE_PRINT = "[dbo].[USP_SELECT_MODULE_AGENDA_ROLE_PRINT] @PROCESS_ID, @AGENDA_IDX, @AGENDA_ROLE_IDX";
        #endregion
        #region [ USP_UPDATE_MODULE_AGENDA_MATERIALCODE ]
        public const string USP_UPDATE_MODULE_AGENDA_MATERIALCODE = "[dbo].[USP_UPDATE_MODULE_AGENDA] @PROCESS_ID, @AGENDA_ROLE_IDX, @MATERIAL_CODE,@COST_CENTER, @SAP_NO, @KRPIA,@UPDATERID";
        #endregion

        #region [ USP_SELECT_MODULE_AGENDA_ROLE_AMOUNT ]
        public const string USP_SELECT_MODULE_AGENDA_ROLE_AMOUNT = "[dbo].[USP_SELECT_MODULE_AGENDA_ROLE_AMOUNT] @HCP_CODE, @PROCESS_ID, @ROLE_TYPE, @ROLE";
        #endregion

        #region [ USP_SELECT_MODULE_AGENDA_SUMMARY ]
        public const string USP_SELECT_MODULE_AGENDA_SUMMARY = "[dbo].[USP_SELECT_MODULE_AGENDA_SUMMARY] @PROCESS_ID"; 
        #endregion

        #endregion

        #region [ Payment ]

        #region [ USP_SELECT_MODULE_PAYMENT ]
        public const string USP_SELECT_MODULE_PAYMENT_BY_HCP = "[dbo].[USP_SELECT_MODULE_PAYMENT_BY_HCP] @PROCESS_ID";
        #endregion

        #region [ USP_SELECT_MODULE_PAYMENT_PLAN_ACTUAL ]
        public const string USP_SELECT_MODULE_PAYMENT_PLAN_ACTUAL = "[dbo].[USP_SELECT_MODULE_PAYMENT_PLAN_ACTUAL] @PROCESS_ID";
        #endregion

        #region [ USP_UPDATE_PAYMENT_UPLOAD_SRM ]
        public const string USP_UPDATE_PAYMENT_UPLOAD_SRM = "[dbo].[USP_UPDATE_PAYMENT_UPLOAD_SRM] @PROCESS_ID, @PO_NUMBER, @UPDATER_ID";
        #endregion

        #region [ USP_INSERT_PAYMENT_UPLOAD_SRM ]
        public const string USP_INSERT_PAYMENT_UPLOAD_SRM = "[dbo].[USP_INSERT_PAYMENT_UPLOAD_SRM] @PROCESS_ID, @SRM_IDX, @PO_NUMBER, @PARTICIPANT_TYPE, @CATEGORY_CODE, @AMOUNT, @HCP_CODE, @HCP_NAME, @HCO_CODE, @HCO_NAME, @COMMENT, @ERROR_MESSAGE, @STATUS, @IS_DELETED, @CREATOR_ID";
        #endregion

        #region [ USP_DELETE_PAYMENT_UPLOAD_SRM ]
        public const string USP_DELETE_PAYMENT_UPLOAD_SRM = "[dbo].[USP_DELETE_PAYMENT_UPLOAD_SRM] @PROCESS_ID, @UPDATER_ID";
        #endregion

        #region [ USP_SELECT_PAYMENT_UPLOAD_SRM ]
        public const string USP_SELECT_PAYMENT_UPLOAD_SRM = "[dbo].[USP_SELECT_PAYMENT_UPLOAD_SRM] @PROCESS_ID";
        #endregion

        #region [ USP_SELECT_PAYMENT_SAVED_SRM ]
        public const string USP_SELECT_PAYMENT_SAVED_SRM = "[dbo].[USP_SELECT_PAYMENT_SAVED_SRM] @PROCESS_ID, @PO_NUMBER";
        #endregion

        #region [ USP_INSERT_PAYMENT_UPLOAD_SRM_HISTORY ]
        public const string USP_INSERT_PAYMENT_UPLOAD_SRM_HISTORY = "[dbo].[USP_INSERT_PAYMENT_UPLOAD_SRM_HISTORY] @PROCESS_ID, @SRM_HISTORY_IDX, @PO_NUMBER, @AMOUNT, @COMMENT, @DISPLAY_FILE_NAME, @SAVED_FILE_NAME, @FILE_PATH, @FILE_HANDLER_URL, @IS_DELETED, @UPLOADER_ID";
        #endregion

        #region [ USP_SELECT_PAYMENT_UPLOAD_SRM_HISTORY ]
        public const string USP_SELECT_PAYMENT_UPLOAD_SRM_HISTORY = "[dbo].[USP_SELECT_PAYMENT_UPLOAD_SRM_HISTORY] @PROCESS_ID";
        #endregion

        #region [ USP_DELETE_PAYMENT_UPLOAD_SRM_HISTORY ]
        public const string USP_DELETE_PAYMENT_UPLOAD_SRM_HISTORY = "[dbo].[USP_DELETE_PAYMENT_UPLOAD_SRM_HISTORY] @PROCESS_ID, @PO_NUMBER, @UPDATER_ID";
        #endregion

        #region [ USP_SELECT_PAYMENT_UPLOAD_SRM_TOTAL ]
        public const string USP_SELECT_PAYMENT_UPLOAD_SRM_TOTAL = "[dbo].[USP_SELECT_PAYMENT_UPLOAD_SRM_TOTAL] @PO_NUMBER";
        #endregion

        #region [ USP_SELECT_MODULE_PAYMENT_LAYER ]
        public const string USP_SELECT_MODULE_PAYMENT_LAYER = "[dbo].[USP_SELECT_MODULE_PAYMENT_LAYER] @PROCESS_ID";
        #endregion

        #region [ USP_MERGE_PAYMENT_INPUT_SRM ]
        public const string USP_MERGE_PAYMENT_INPUT_SRM = "[dbo].[USP_MERGE_PAYMENT_INPUT_SRM] @PROCESS_ID, @SRM_IDX, @PO_NUMBER, @CATEGORY_CODE, @POST_DATE, @AMOUNT, @COMMENT, @IS_DELETED, @CREATOR_ID, @UPDATER_ID";
        #endregion

        #region [ USP_SELECT_PAYMENT_INPUT_SRM ]
        public const string USP_SELECT_PAYMENT_INPUT_SRM = "[dbo].[USP_SELECT_PAYMENT_INPUT_SRM] @PROCESS_ID";
        #endregion

        #region [ USP_DELETE_PAYMENT_INPUT_SRM ]
        public const string USP_DELETE_PAYMENT_INPUT_SRM = "[dbo].[USP_DELETE_PAYMENT_INPUT_SRM] @PROCESS_ID, @SRM_IDX, @UPDATER_ID";
        #endregion

        #region [ USP_SELECT_MODULE_PAYMENT_LAYER_BY_HCP ]
        public const string USP_SELECT_MODULE_PAYMENT_LAYER_BY_HCP = "[dbo].[USP_SELECT_MODULE_PAYMENT_LAYER_BY_HCP] @PROCESS_ID, @HCP_CODE, @DATA_SOURCE";
        #endregion

        #region [ USP_INSERT_PAYMENT_UPLOAD_CONCUR ]
        public const string USP_INSERT_PAYMENT_UPLOAD_CONCUR = "[dbo].[USP_INSERT_PAYMENT_UPLOAD_CONCUR] @COMPANY_CODE, @REPORT_ID, @EMPLOYEE_ID, @EMPLOYEE, @REPORT_NAME, @TRANSACTION_ID, @TRANSACTION_DATE, @EXPENSE_TYPE, @HCP_EXPENSE_TYPE, @MATERIAL_CODE, @CONSULTATION_NUMBER, @HCP_TYPE_IDENTIFICATION, @BUSINESS_PURPOSE, @EXPENSE_AMOUNT, @REIMBURSEMENT_CURRENCY, @ATTENDEE_AMOUNT, @EVENT_KEY, @LOCAL_CODE, @HCP_CODE, @HCO_CODE, @CRM_EVENT_KEY, @ATTENDEE_NAME, @ATTENDEE_FIRST_NAME, @ATTENDEE_LAST_NAME, @TITLE, @COMPANY, @AFFILIATION, @ATTENDEE_TYPE, @EXTERNAL_ID, @VENDOR, @LOCATION, @REGION, @COUNTRY, @ALLOCATION_CODE, @ALLOCATION_NUMBER, @ALLOCATION_AMOUNT, @PAYMENT_TYPE, @RECEIPT_TYPE, @POLICY, @COMMENTS, @STATUS, @ERROR_MESSAGE, @IS_DELETED, @CREATOR_ID";
        #endregion

        #region [ USP_INSERT_PAYMENT_UPLOAD_YOURDOCES ]
        public const string USP_INSERT_PAYMENT_UPLOAD_YOURDOCES = "[dbo].[USP_INSERT_PAYMENT_UPLOAD_YOURDOCES] @ACCOUNT, @NAME_1, @DOCUMENT_NUMBER, @DOCUMENT_TYPE, @PAYMENT_BLOCK, @DOCUMENT_HEADER_TEXT, @DOCUMENT_DATE, @ENTRY_DATE, @POSTING_DATE, @NET_DUE_DATE, @AMOUNT_IN_DOC_CURR, @DOCUMENT_CURRENCY, @AMOUNT_IN_LOCAL_CURRENCY, @LOCAL_CURRENCY, @TEXT, @USER_NAME, @CLEARING_DATE, @CLEARING_DOCUMENT, @REFERENCE_KEY_1, @REFERENCE_KEY_2, @REFERENCE_KEY_3, @HCO_CODE, @COMMENTS, @STATUS, @ERROR_MESSAGE, @IS_DELETED, @CREATOR_ID";


        #endregion

        #region [ USP_DELETE_PAYMENT_UPLOAD_CONCUR ]
        public const string USP_DELETE_PAYMENT_UPLOAD_CONCUR = "[dbo].[USP_DELETE_PAYMENT_UPLOAD_CONCUR] @UPDATER_ID";
        #endregion

        #region [ USP_DELETE_PAYMENT_UPLOAD_YOURDOCES ]
        public const string USP_DELETE_PAYMENT_UPLOAD_YOURDOCES = "[dbo].[USP_DELETE_PAYMENT_UPLOAD_YOURDOCES] @UPDATER_ID";
        #endregion

        #region [ USP_SELECT_PAYMENT_UPLOAD_CONCUR ]
        public const string USP_SELECT_PAYMENT_UPLOAD_CONCUR = "[dbo].[USP_SELECT_PAYMENT_UPLOAD_CONCUR] @CREATOR_ID";
        #endregion

        #region [ USP_SELECT_PAYMENT_UPLOAD_CONCUR ]
        public const string USP_SELECT_PAYMENT_UPLOAD_YOURDOCES = "[dbo].[USP_SELECT_PAYMENT_UPLOAD_YOURDOCES] @CREATOR_ID";
        #endregion

        #region [ USP_INSERT_MODULE_PAYMENT_FROM_CONCUR ]
        public const string USP_INSERT_MODULE_PAYMENT_FROM_CONCUR = "[dbo].[USP_INSERT_MODULE_PAYMENT_FROM_CONCUR] @CREATOR_ID";
        #endregion

        #region [ USP_INSERT_MODULE_PAYMENT_FROM_YOURDOCES ]
        public const string USP_INSERT_MODULE_PAYMENT_FROM_YOURDOCES = "[dbo].[USP_INSERT_MODULE_PAYMENT_FROM_YOURDOCES] @CREATOR_ID";
        #endregion

        #region [ USP_SELECT_MODULE_PAYMENT_CONCUR ]
        public const string USP_SELECT_MODULE_PAYMENT_CONCUR = "[dbo].[USP_SELECT_MODULE_PAYMENT_CONCUR] @KEYWORD";
        #endregion

        #region [ USP_UPDATE_MODULE_PAYMENT_CONCUR ]
        public const string USP_UPDATE_MODULE_PAYMENT_CONCUR = "[dbo].[USP_UPDATE_MODULE_PAYMENT_CONCUR] @PROCESS_ID, @CONCUR_TRANSACTION_ID, @CATEGORY_CODE, @UPDATER";
        #endregion

        #region [ USP_SELECT_MODULE_PAYMENT_CONCUR_LOG ]
        public const string USP_SELECT_MODULE_PAYMENT_CONCUR_LOG = "[dbo].[USP_SELECT_MODULE_PAYMENT_CONCUR_LOG] @PROCESS_ID, @CONCUR_TRANSACTION_ID";
        #endregion

        #region [ USP_SELECT_PAYMENT_ICC_MASTER ]
        public const string USP_SELECT_PAYMENT_ICC_MASTER = "[dbo].[USP_SELECT_PAYMENT_ICC_MASTER] @ICC_ID";
        #endregion

        #region [ USP_MERGE_PAYMENT_ICC_MASTER ]
        public const string USP_MERGE_PAYMENT_ICC_MASTER = "[dbo].[USP_MERGE_PAYMENT_ICC_MASTER] @ICC_ID, @TYPE, @YEAR, @START_TIME, @END_TIME, @ADDRESS, @PURPOSE, @HCP_CODE, @HCP_NAME, @HCO_CODE, @HCO_NAME, @SUBJECT, @HOST_COUNTRY, @INVITING_COUNTRY, @PAYMENT_COUNTRY, @PAYMENT_DATE, @CURRENCY, @AMOUNT_CURRENCY, @AMOUNT_KOR, @FLIGHT_CLASS, @FLIGHT_CHECKIN_DATE, @FLIGHT_CHECKOUT_DATE, @FLIGHT_COMMENT, @ACCOMMODATION_CHECKIN_DATE, @ACCOMMODATION_CHECKOUT_DATE, @ACCOMMODATION_COMMENT, @AMOUNT_MEAL_BEVERAGE, @AMOUNT_TRANSPORTAION, @COMMENT, @AGREE_KRPIA, @CREATOR_ID";
        #endregion

        #region [ USP_INSERT_PAYMENT_ICC_ATTACH_FILES ]
        public const string USP_INSERT_PAYMENT_ICC_ATTACH_FILES = "[dbo].[USP_INSERT_PAYMENT_ICC_ATTACH_FILES] @ICC_ID, @ATTACH_FILE_TYPE, @SEQ, @REFER_IDX, @DISPLAY_FILE_NAME, @SAVED_FILE_NAME, @FILE_SIZE, @FILE_PATH, @FILE_HANDLER_URL, @IS_DELETED, @CREATOR_ID";
        #endregion

        #region [ USP_SELECT_PAYMENT_ICC_ATTACH_FILES ]
        public const string USP_SELECT_PAYMENT_ICC_ATTACH_FILES = "[dbo].[USP_SELECT_PAYMENT_ICC_ATTACH_FILES] @ICC_ID, @IDXS";
        #endregion

        #region [ USP_DELETE_PAYMENT_ICC_ATTACH_FILES ]
        public const string USP_DELETE_PAYMENT_ICC_ATTACH_FILES = "[dbo].[USP_DELETE_PAYMENT_ICC_ATTACH_FILES] @IDX, @UPDATER_ID";
        #endregion

        #endregion


        #region [ Delegation ]

        #region [ USP_MERGE_MODULE_DELEGATION ]
        public const string USP_MERGE_MODULE_DELEGATION = "[dbo].[USP_MERGE_MODULE_DELEGATION] @PROCESS_ID, @USER_ID, @ROLE, @IS_DELETED, @CREATOR_ID, @UPDATER_ID";
        #endregion

        #region [ USP_SELECT_MODULE_DELEGATION ]
        public const string USP_SELECT_MODULE_DELEGATION = "[dbo].[USP_SELECT_MODULE_DELEGATION] @PROCESS_ID";
        #endregion

        // version 1.0.5 Add Change History in Event Form
        #region [ USP_SELECT_MODULE_CHANGE ]
        public const string USP_SELECT_MODULE_CHANGE = "[dbo].[USP_SELECT_MODULE_CHANGE] @PROCESS_ID";
        #endregion

        #region [ USP_DELETE_MODULE_DELEGATION ]
        public const string USP_DELETE_MODULE_DELEGATION = "[dbo].[USP_DELETE_MODULE_DELEGATION] @PROCESS_ID, @USER_ID, @UPDATER_ID";
        #endregion

        #region [ USP_SELECT_MODULE_DELEGATION_USER ]
        public const string USP_SELECT_MODULE_DELEGATION_USER = "[dbo].[USP_SELECT_MODULE_DELEGATION_USER] @PROCESS_ID, @KEYWORD";
        #endregion

        #endregion

        #region [Change_Value]
        // version 1.0.5 Admin Event Page for change by DM Team

        public const string USP_UPDATE_ADMIN_CORRECTION = "[dbo].[USP_UPDATE_ADMIN_CORRECTION] @PROCESS_ID,@CATEGORY,@ADJUSTMENT_AREA,@OLD_VALUE,@NEW_VALUE,@REASON,@UPDATER_ID";

        #endregion
    }                                                                                                         
}                                                                                                             
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              
                                                                                                              