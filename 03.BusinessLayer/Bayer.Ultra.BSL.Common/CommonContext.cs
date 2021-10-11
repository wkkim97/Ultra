using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Common
{
    public class CommonContext : Framework.Database.UltraDbContext
    {
        /// <summary>
        /// 로그인 사용자 정보 조회
        /// </summary>
        public const string USP_SELECT_USER = "[eManage].[dbo].[USP_SELECT_USER_ULTRA] @NVCLANGUAGESET, @NVCUSERACCOUNT";

        /// <summary>
        /// 로그인 History 등록
        /// </summary>
        public const string USP_INSERT_LOGIN_HISTORY = "[eManage].[dbo].[USP_INSERT_LOGIN_HISTORY] @USER_ID, @CLIENTIP, @WINDOWUSERNAME, @WINDOWDOMAINNAME";

        /// <summary>
        /// 사용자별 이벤트 목록 조회
        /// </summary>
        public const string USP_SELECT_USER_EVENT_LIST = "[dbo].[USP_SELECT_USER_EVENT_LIST] @USER_ID";

        /// <summary>
        /// 시스템 로그 저장
        /// </summary>
        public const string USP_INSERT_LOG_SYSTEM = "[dbo].[USP_INSERT_LOG_SYSTEM] @TYPE, @EVENT_NAME, @MESSAGE, @CREATER_ID";

        /// <summary>
        /// 의사/약사/간호사 조회
        /// </summary>
        public const string USP_SELECT_HEALTH_CARE_PROVIDER = "[dbo].[USP_SELECT_HEALTH_CARE_PROVIDER] @HCPName, @OrgName, @SpeName, @PROCESS_ID";

        /// <summary>
        /// 병원 조회
        /// </summary>
        public const string USP_SELECT_HEALTH_CARE_OFFICE = "[dbo].[USP_SELECT_HEALTH_CARE_OFFICE] @KEYWORD, @TYPE";

        /// <summary>
        /// 국가 조회
        /// </summary>
        public const string USP_SELECT_COUNTRY_BY_ULTRA = "[eManage].[dbo].[USP_SELECT_COUNTRY_BY_ULTRA]";

        /// <summary>
        /// 코드 조회
        /// </summary>
        public const string USP_SELECT_CODE_SUB = "[dbo].[USP_SELECT_CODE_SUB] @CLASS_CODE";

        /// <summary>
        /// 상품 조회( eManage 에서 조회)
        /// </summary>
        public const string USP_SELECT_EMANAGE_MASTER_PRODUCT = "[dbo].[USP_SELECT_EMANAGE_MASTER_PRODUCT] @KEYWORD";

        /// <summary>
        /// HCP 검색
        /// </summary>
        public const string USP_SELECT_SEARCH_MASTER_DOCTOR = "[dbo].[USP_SELECT_SEARCH_MASTER_DOCTOR] @HCPName,@OrgName,@SpeName";

        /// <summary>
        /// HCP 검색
        /// </summary>
        public const string USP_SELECT_SEARCH_DOCTOR = "[dbo].[USP_SELECT_SEARCH_DOCTOR] @KEYWORD";

        /// <summary>
        /// eManage Master Product 테이블에서 Sample List 검색
        /// </summary>
        public const string USP_SELECT_SAMPLE_LIST = "[dbo].[USP_SELECT_SAMPLE_LIST] @KEYWORD, @TYPE";

        /// <summary>
        /// 학회 리스트 가져오기
        /// </summary>
        public const string USP_SELECT_MEDICAL_SOCIETY = "[dbo].[USP_SELECT_MEDICAL_SOCIETY] @SOCIETY_IDX, @KEYWORD, @STATUS";


        /// <summary>
        /// 학회 리스트 저장
        /// </summary>
        public const string USP_MERGE_MEDICAL_SOCIETY = "[dbo].[USP_MERGE_MEDICAL_SOCIETY] @SOCIETY_IDX, @SOCIETY_NAME, @STATUS, @CREATOR_ID, @UPDATER_ID";

        /// <summary>
        /// 학회 리스트 삭제
        /// </summary>
        public const string USP_DELETE_MEDICAL_SOCIETY = "[dbo].[USP_DELETE_MEDICAL_SOCIETY] @SOCIETY_IDX";

        /// <summary>
        /// 문서 정렬 변경
        /// </summary>
        public const string USP_UPDATE_USER_CONFIG_MENU_SORT = "[dbo].[USP_UPDATE_USER_CONFIG_MENU_SORT] @USER_ID, @EVENT_ID, @SORT";



        /// <summary>
        /// 위임 리스트 가져오기
        /// </summary>
        public const string USP_SELECT_ABSENCE = "[dbo].[USP_SELECT_ABSENCE] @USER_ID, @IDX";

        /// <summary>
        /// 위임 리스트 가져오기
        /// </summary>
        public const string USP_MERGE_ABSENCE = "[dbo].[USP_MERGE_ABSENCE] @IDX, @APPROVER_ID, @FROM_DATE, @TO_DATE, @DESCRIPTION, @USER_ID";

        /// <summary>
        /// 위임 리스트 가져오기
        /// </summary>
        public const string USP_DELETE_ABSENCE = "[dbo].[USP_DELETE_ABSENCE] @USER_ID, @IDX";

        /// <summary>
        /// 위임자 리스트 가져오기
        /// </summary>
        public const string USP_SELECT_USER_LIST_DELEGATION = "[eManage].[dbo].[USP_SELECT_USER_LIST_DELEGATION] @USER_ID";

        /// <summary>
        /// 공통코드 전체 반환
        /// </summary>
        public const string USP_SELECT_CODE_SUB_ALL = "[dbo].[USP_SELECT_CODE_SUB_ALL]";

        /// <summary>
        /// CRM 제품코드 조회
        /// </summary>
        public const string USP_SELECT_CRM_PRODUCT = "[eManage].[dbo].[USP_SELECT_CRM_PRODUCT] @KEYWORD";


        #region USP_SELECT_SENDMAIL_TO_ADDRESS_LIST
        /// <summary>
        /// 메일 발송에 필요한 결재자 목록 조회
        /// </summary>
        public const string USP_SELECT_SENDMAIL_TO_ADDRESS_LIST = "[dbo].[USP_SELECT_SENDMAIL_TO_ADDRESS_LIST] @PROCESS_ID, @MAILSENDTYPE";
        #endregion

        #region USP_UPDATE_PROCESS_APPROVER_SENT_MAIL
        /// <summary>
        /// 메일 발송 상태 업데이트 SP 
        /// </summary>
        public const string USP_UPDATE_PROCESS_APPROVER_SENT_MAIL = "[dbo].[USP_UPDATE_PROCESS_APPROVER_SENT_MAIL] @PROCESS_ID, @APPROVER_ID, @SPLIT";
        #endregion


        #region [ USP_SELECT_PROCESS_REJECT_USER ]

        /// <summary>
        /// 메일발송시 Rejecter 검색
        /// </summary>
        public const string USP_SELECT_PROCESS_REJECT_USER = "[dbo].[USP_SELECT_PROCESS_REJECT_USER] @PROCESS_ID";

        #endregion

        #region [ USP_SELECT_INPUT_COMMENT ]

        /// <summary>
        /// Inputcomment 등록시 메일발송
        /// </summary>
        public const string USP_SELECT_INPUT_COMMENT = "[dbo].[USP_SELECT_INPUT_COMMENT] @PROCESS_ID";
        #endregion
        
        /// <summary>
        /// Send Mail Agent Insert / Update
        /// </summary>
        public const string USP_MERGE_SENDMAIL_QUEUE = "[dbo].[USP_MERGE_SENDMAIL_QUEUE] @IDX, @SEND_MAIL_TYPE, @MAILADDRESS, @SEND_STATUS, @SEND_DATE, @RETRY_CNT, @REMARK";

        /// <summary>
        /// Send Mail Task List 
        /// </summary>
        public const string USP_SELECT_SEND_MAIL_QUEUE = "[dbo].[USP_SELECT_SEND_MAIL_QUEUE]";
         
        public const string USP_UPDATE_PROCESS_APPROVER_SENT_MAIL_COMMENT = "[dbo].[USP_UPDATE_PROCESS_APPROVER_SENT_MAIL_COMMENT] @PROCESS_ID, @STATUS, @APPROVER_ID, @SPLIT";

        public const string USP_SELECT_SEND_NOTICEMAIL_LIST = "[dbo].[USP_SELECT_SEND_NOTICEMAIL_LIST] @BASICDATE";
    }
}
