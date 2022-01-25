using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Report
{
    public class ReportContext : Framework.Database.UltraDbContext
    {
        #region AdminReport
        public const string USP_SELECT_DOWNLOAD_EXCEL_TEMPLATE = "[dbo].[USP_SELECT_DOWNLOAD_EXCEL_TEMPLATE] @CATEGORY,@FROM_DATE,@TO_DATE";
        #endregion
        #region Free Good
        /// <summary>
        /// Receipt for free Good List SP
        /// </summary>
        public const string USP_SELECT_REPORT_RESEIPT_FREE_GOOD_LIST = "[dbo].[USP_SELECT_REPORT_RESEIPT_FREE_GOOD_LIST] @USER_ID";

        /// <summary>
        /// Receipt for free Good  저장 SP
        /// </summary>
        public const string USP_MERGE_REPORT_RECEIPT_FREE_GOOD = "[dbo].[USP_MERGE_REPORT_RECEIPT_FREE_GOOD] @PROCESS_ID, @IDX, @EVENT_KEY, @RECEIPT_TYPE, @RECEIPT_DATE, @RECEIPT_FILENAME,@EVENT_FILE_IDX, @FILE_PATH, @FILE_URL, @SIGN_IMG_PATH, @SIGN_IMG_URL, @RETURN_DATE, @SAP_ORDER, @STATUS, @CREATOR_ID, @CREATE_DATE, @UPDATER_ID, @UPDATE_DATE";
        public const string USP_MERGE_REPORT_RECEIPT_FREE_GOOD_RETURN = "[dbo].[USP_MERGE_REPORT_RECEIPT_FREE_GOOD_RETURN] @PROCESS_ID, @IDX, @EVENT_KEY, @RECEIPT_TYPE, @RECEIPT_DATE, @RECEIPT_FILENAME,@EVENT_FILE_IDX, @FILE_PATH, @FILE_URL, @SIGN_IMG_PATH, @SIGN_IMG_URL, @RETURN_DATE, @SAP_ORDER, @STATUS, @CREATOR_ID, @CREATE_DATE, @UPDATER_ID, @UPDATE_DATE,@RETURN_COMMENT";

        /// <summary>
        /// Receipt for free Good ITEM 조회 SP
        /// </summary>
        public const string USP_SELECT_REPORT_RESEIPT_FREE_GOOD_ITEM = "[dbo].[USP_SELECT_REPORT_RESEIPT_FREE_GOOD_ITEM] @PROCESS_ID, @IDX";
         
        /// <summary>
        /// Receipt for free Good  저장 SP
        /// </summary>
        public const string USP_UPDATE_REPORT_RECEIPT_FREE_GOOD_FILE_IDX = "[dbo].[USP_UPDATE_REPORT_RECEIPT_FREE_GOOD_FILE_IDX] @PROCESS_ID, @IDX, @EVENT_FILE_IDX";
        public const string USP_UPDATE_REPORT_RECEIPT_FREE_GOOD_RETURN_FILE_IDX = "[dbo].[USP_UPDATE_REPORT_RECEIPT_FREE_GOOD_RETURN_FILE_IDX] @PROCESS_ID, @IDX, @EVENT_FILE_IDX";

        /// <summary>
        /// Receipt for free Good  status SP
        /// </summary>
        public const string USP_UPDATE_REPORT_RECEIPT_RETURN_STATUS = "[dbo].[USP_UPDATE_REPORT_RECEIPT_RETURN_STATUS] @PROCESS_ID, @IDX, @EVENT_KEY, @STATUS, @SAP_ORDER, @UPDATER_ID";

        #endregion

        #region MOHW Conditions
        /// <summary>
        /// MOHW Conditions 추가
        /// </summary>
        public const string USP_INSERT_REPORT_MOHW_CONDITION = "[dbo].[USP_INSERT_REPORT_MOHW_CONDITION] @IDX, @MOHW_TYPE, @SUBJECT, @START_DATE, @END_DATE, @IS_FOOD_COST_YN, @BELOW_ONE_WON_YN, @EXCEPT_IECTURER_FOOD_COST_YN, @ONLY_ATTEND_YN, @ONLY_MEDICINE_YN, @ONLY_MEDICAL_EQUIPMENT_YN, @EXCEPT_BAYER_EMPLOYEEE_YN, @CONFIRM_YN, @FILE_PATH, @STATUS, @CREATOR_ID, @CREATE_DATE";

        /// <summary>
        /// MOHW Conditions File Path Update
        /// </summary>
        public const string USP_MERGE_REPORT_MOHW_FILES = "[dbo].[USP_MERGE_REPORT_MOHW_FILES] @IDX, @TYPE, @PATH, @STATUS, @USER_ID";

        /// <summary>
        /// MOHW Conditions List 조회
        /// </summary>
        public const string USP_SELECT_REPORT_MOHW_LIST = "[dbo].[USP_SELECT_REPORT_MOHW_LIST] @SUBJECT, @MOHW_TYPE, @START_DATE, @END_DATE";

        /// <summary>
        /// MOHW Conditions Status 업데이트
        /// </summary>
        public const string USP_UPDATE_REPORT_MOHW_STATUS = "[dbo].[USP_UPDATE_REPORT_MOHW_STATUS] @IDX, @MOHW_TYPE, @STATUS, @USER_ID";

        /// <summary>
        /// 각각의 Report Source Table에 Confirm 업데이트
        /// </summary>
        public const string USP_UPDATE_CONFIRM_MOHW = "[dbo].[USP_UPDATE_CONFIRM_MOHW] @IDX, @MOHW_TYPE, @USER_ID";

        #endregion

        #region  Market Research(시판 후 조사)
        /// <summary>
        /// Market Research Source list
        /// </summary>
        public const string USP_SELECT_REPORT_MARKET_RESEARCH_SOURCE_LIST = "[dbo].[USP_SELECT_REPORT_MARKET_RESEARCH_SOURCE_LIST] @START_DATE, @END_DATE, @USER_ID";

        /// <summary>
        /// Market Research Report 생성
        /// </summary>
        public const string USP_INSERT_REPORT_MARKET_RESEARCH = "[dbo].[USP_INSERT_REPORT_MARKET_RESEARCH] @MOHW_IDX, @START_DATE, @END_DATE, @USER_ID";

        /// <summary>
        /// Market Research Report 조회(Excel 조회용)
        /// </summary>
        public const string USP_SELECT_REPORT_MOHW_MARKET_RESEARCH = "[dbo].[USP_SELECT_REPORT_MOHW_MARKET_RESEARCH] @IDX"; 
        #endregion

        #region  Participants (학술대회)
        /// <summary>
        /// Market Research Source list
        /// </summary>
        public const string USP_SELECT_REPORT_PARTICIPANTS_SOURCE_LIST = "[dbo].[USP_SELECT_REPORT_PARTICIPANTS_SOURCE_LIST] @START_DATE, @END_DATE, @USER_ID";

        /// <summary>
        /// Market Research Report 생성
        /// </summary>
        public const string USP_INSERT_REPORT_PARTICIPANTS = "[dbo].[USP_INSERT_REPORT_PARTICIPANTS] @MOHW_IDX, @START_DATE, @END_DATE, @USER_ID";

        /// <summary>
        /// Market Research Report 조회(Excel 조회용)
        /// </summary>
        public const string USP_SELECT_REPORT_MOHW_PARTICIPANTS = "[dbo].[USP_SELECT_REPORT_MOHW_PARTICIPANTS] @IDX";


        public const string USP_SELECT_REPORT_MOHW_PARTICIPANTS_XLS = "[dbo].[USP_SELECT_REPORT_MOHW_PARTICIPANTS_XLS] @IDX";

        #endregion

        #region  Plurality Medical (복수 요양 기관)
        /// <summary>
        /// Plurality Medical list
        /// </summary>
        public const string USP_SELECT_REPORT_PLURALITY_MEDICAL_SOURCE_LIST = "[dbo].[USP_SELECT_REPORT_PLURALITY_MEDICAL_SOURCE_LIST] @START_DATE, @END_DATE, @BELOW_ONE_WON_YN, @EXCEPT_IECTURER_FOOD_COST_YN, @ONLY_ATTEND_YN, @ONLY_MEDICINE_YN, @ONLY_MEDICAL_EQUIPMENT_YN, @EXCEPT_BAYER_EMPLOYEEE_YN, @USER_ID";

        /// <summary>
        /// Plurality Medical Report 생성
        /// </summary>
        public const string USP_INSERT_REPORT_PLURALITY_MEDICAL = "[dbo].[USP_INSERT_REPORT_PLURALITY_MEDICAL] @MOHW_IDX, @START_DATE, @END_DATE, @BELOW_ONE_WON_YN, @EXCEPT_IECTURER_FOOD_COST_YN, @ONLY_ATTEND_YN, @ONLY_MEDICINE_YN, @ONLY_MEDICAL_EQUIPMENT_YN, @EXCEPT_BAYER_EMPLOYEEE_YN, @USER_ID";

        /// <summary>
        /// Plurality Medical Report 조회(Excel 조회용)
        /// </summary>
        public const string USP_SELECT_REPORT_MOHW_PLURALITY_MEDICAL = "[dbo].[USP_SELECT_REPORT_MOHW_PLURALITY_MEDICAL] @IDX";
        #endregion

        #region [ Individual Medical (개별 요양 기관) ]

        /// <summary>
        /// 조건별 조회
        /// </summary>
        public const string USP_SELECT_REPORT_DIV_MEDICAL_SOURCE_LIST = "[dbo].[USP_SELECT_REPORT_DIV_MEDICAL_SOURCE_LIST] @START_DATE, @END_DATE, @BELOW_ONE_WON_YN, @EXCEPT_IECTURER_FOOD_COST_YN, @ONLY_ATTEND_YN, @ONLY_MEDICINE_YN, @ONLY_MEDICAL_EQUIPMENT_YN, @EXCEPT_BAYER_EMPLOYEEE_YN";

        /// <summary>
        /// 조회후 Insert
        /// </summary>
        public const string USP_INSERT_REPORT_DIV_MEDICAL = "[dbo].[USP_INSERT_REPORT_DIV_MEDICAL] @MOHW_IDX, @START_DATE, @END_DATE, @BELOW_ONE_WON_YN, @EXCEPT_IECTURER_FOOD_COST_YN, @ONLY_ATTEND_YN, @ONLY_MEDICINE_YN, @ONLY_MEDICAL_EQUIPMENT_YN, @EXCEPT_BAYER_EMPLOYEEE_YN";

        /// <summary>
        /// 엑셀(Source) 생성용 조회
        /// </summary>
        public const string USP_SELECT_REPORT_MOHW_DIV_MEDICAL_SRC = "[dbo].[USP_SELECT_REPORT_MOHW_DIV_MEDICAL_SRC] @IDX";

        /// <summary>
        /// 엑셀(MOHW) 생성용 조회
        /// </summary>
        public const string USP_SELECT_REPORT_MOHW_DIV_MEDICAL = "[dbo].[USP_SELECT_REPORT_MOHW_DIV_MEDICAL] @IDX";

        #endregion

        #region  Free Goods (견본품 제공)
        /// <summary>
        /// Free Goods(견본품) Source list
        /// </summary>
        public const string USP_SELECT_REPORT_FREE_GOODS_SOURCE_LIST = "[dbo].[USP_SELECT_REPORT_FREE_GOODS_SOURCE_LIST] @START_DATE, @END_DATE, @USER_ID";

        /// <summary>
        /// Free Goods(견본품) Report 생성
        /// </summary>
        public const string USP_INSERT_REPORT_MOHW_FREE_GOODS = "[dbo].[USP_INSERT_REPORT_MOHW_FREE_GOODS] @MOHW_IDX, @START_DATE, @END_DATE, @USER_ID";

        /// <summary>
        /// MOHW Free Goods(견본품) Report 조회(Excel 조회용)
        /// </summary>
        public const string USP_SELECT_REPORT_MOHW_FREE_GOODS = "[dbo].[USP_SELECT_REPORT_MOHW_FREE_GOODS] @IDX";

        /// <summary>
        /// MOHW Free Goods(견본품) Report 조회(MOHW 조회용)
        /// </summary>
        public const string USP_SELECT_REPORT_MOHW_FREE_GOODS_XLS = "[dbo].[USP_SELECT_REPORT_MOHW_FREE_GOODS_XLS] @IDX";

        #endregion

        #region  Free Goods (구매전 의료기기 제공)
        /// <summary>
        /// Free Goods(견본품) Source list
        /// </summary>
        public const string USP_SELECT_REPORT_FREE_GOODS_DEVICE_SOURCE_LIST = "[dbo].[USP_SELECT_REPORT_FREE_GOODS_DEVICE_SOURCE_LIST] @START_DATE, @END_DATE, @USER_ID";

        /// <summary>
        /// Free Goods(견본품) Report 생성
        /// </summary>
        public const string USP_INSERT_REPORT_MOHW_FREE_GOODS_DEVICE = "[dbo].[USP_INSERT_REPORT_MOHW_FREE_GOODS_DEVICE] @MOHW_IDX, @START_DATE, @END_DATE, @USER_ID";

        /// <summary>
        /// MOHW Free Goods(견본품) Report 조회(Excel 조회용)
        /// </summary>
        public const string USP_SELECT_REPORT_MOHW_FREE_GOODS_DEVICE = "[dbo].[USP_SELECT_REPORT_MOHW_FREE_GOODS_DEVICE] @IDX";

        /// <summary>
        /// MOHW Free Goods(견본품) Report 조회(MOHW 조회용)
        /// </summary>
        public const string USP_SELECT_REPORT_MOHW_FREE_GOODS_DEVICE_XLS = "[dbo].[USP_SELECT_REPORT_MOHW_FREE_GOODS_DEVICE_XLS] @IDX";

        #endregion

        #region  Medical Study (임상시험지원)
        /// <summary>
        /// Medical Study (임상시험지원) Source list
        /// </summary>
        public const string USP_SELECT_REPORT_MOHW_MEDICAL_STUDY_SOURCE_LIST = "[dbo].[USP_SELECT_REPORT_MOHW_MEDICAL_STUDY_SOURCE_LIST] @START_DATE, @END_DATE, @USER_ID";

        /// <summary>
        /// Medical Study (임상시험지원) Report 생성
        /// </summary>
        public const string USP_INSERT_REPORT_MOHW_MEDICAL_STUDY = "[dbo].[USP_INSERT_REPORT_MOHW_MEDICAL_STUDY] @MOHW_IDX, @START_DATE, @END_DATE, @USER_ID";

        /// <summary>
        /// MOHW Medical Study (임상시험지원) Report 조회(Excel 조회용)
        /// </summary>
        public const string USP_SELECT_REPORT_MOHW_MEDICAL_STUDY = "[dbo].[USP_SELECT_REPORT_MOHW_MEDICAL_STUDY] @IDX";

        /// <summary>
        /// MOHW Medical Study (임상시험지원) Report 조회(MOHW 조회용)
        /// </summary>
        public const string USP_SELECT_REPORT_MOHW_MEDICAL_STUDY_XLS = "[dbo].[USP_SELECT_REPORT_MOHW_MEDICAL_STUDY_XLS] @IDX";

        #endregion

        #region  KRPIA (협회보고)
        /// <summary>
        ///  KRPIA (협회보고) Source list
        /// </summary>
        public const string USP_SELECT_REPORT_MOHW_KRPIA_SOURCE_LIST = "[dbo].[USP_SELECT_REPORT_MOHW_KRPIA_SOURCE_LIST] @START_DATE, @END_DATE, @TYPE, @USER_ID";

        /// <summary>
        ///  KRPIA (협회보고) Report 생성
        /// </summary>
        public const string USP_INSERT_REPORT_MOHW_KRPIA = "[dbo].[USP_INSERT_REPORT_MOHW_KRPIA] @MOHW_IDX, @START_DATE, @END_DATE, @USER_ID,@TYPE";

        /// <summary>
        /// MOHW  KRPIA (협회보고) Report 조회(Excel 조회용)
        /// </summary>
        public const string USP_SELECT_REPORT_MOHW_KRPIA = "[dbo].[USP_SELECT_REPORT_MOHW_KRPIA] @IDX";

        /// <summary>
        /// MOHW  KRPIA (협회보고) Report 조회(MOHW 조회용)
        /// </summary>
        public const string USP_SELECT_REPORT_MOHW_KRPIA_XLS = "[dbo].[USP_SELECT_REPORT_MOHW_KRPIA_XLS] @IDX";

        #endregion

        #region  MicroMarketing (마이크로 마케팅)
        /// <summary>
        ///  RAD 유저에 할당된 병원 리스트 조회
        /// </summary>
        public const string USP_SELECT_RAD_HOSPITAL_LIST = "[dbo].[USP_SELECT_RAD_HOSPITAL_LIST] @USER_ID, @USER_TYPE";

        /// <summary>
        ///  Examination 리스트 조회
        /// </summary>
        public const string USP_SELECT_MICRO_MARKETING_EXAMINATION = "[dbo].[USP_SELECT_MICRO_MARKETING_EXAMINATION] @ID, @ORGANIZATION_ID, @QUARTER";

        /// <summary>
        ///  Examination 생성, 저장
        /// </summary>
        public const string USP_MERGE_MICRO_MARKETING_EXAMINATION = "[dbo].[USP_MERGE_MICRO_MARKETING_EXAMINATION] @ID, @CURRENT_QUARTER, @USER_ID, @USER_NAME,	@ORGANIZATION_ID, @ORGANIZATION_NAME, @EXAMINATION_TYPE, @SCAN_TYPE, @NUMBER_OF_CASES, @NUMBER_OF_ENHANCED_CASES, @CASE_RELATION, @COMMENT, @NEW_QUATER, @CREATOR_ID";
        
        /// <summary>
        ///  Examination 삭제
        /// </summary>
        public const string USP_DELETE_MICRO_MARKETING_EXAMINATION = "[dbo].[USP_DELETE_MICRO_MARKETING_EXAMINATION] @ID";

        /// <summary>
        ///  Equipment 리스트 조회
        /// </summary>
        public const string USP_SELECT_MICRO_MARKETING_EQUIPMENT = "[dbo].[USP_SELECT_MICRO_MARKETING_EQUIPMENT] @ID, @ORGANIZATION_ID";

        /// <summary>
        ///  Equipment 생성, 저장
        /// </summary>
        public const string USP_MERGE_MICRO_MARKETING_EQUIPMENT = "[dbo].[USP_MERGE_MICRO_MARKETING_EQUIPMENT] @ID, @USER_ID, @USER_NAME, @ORGANIZATION_ID, @ORGANIZATION_NAME, @CATEGORY, @SCANNER_MODEL_TYPE, @MANUFACTURER, @STATUS, @INJECTOR_MANUFACTURER, @EQUIPMENT_TYPE, @DATE_OF_INSTALLATION, @AMOUNT, @MEMO, @CREATOR_ID";

        /// <summary>
        ///  Equipment 삭제
        /// </summary>
        public const string USP_DELETE_MICRO_MARKETING_EQUIPMENT = "[dbo].[USP_DELETE_MICRO_MARKETING_EQUIPMENT] @ID";

        /// <summary>
        ///  Market Share 리스트 조회
        /// </summary>
        public const string USP_SELECT_MICRO_MARKETING_MARKETSHARE = "[dbo].[USP_SELECT_MICRO_MARKETING_MARKETSHARE] @ID, @ORGANIZATION_ID, @QUARTER";

        /// <summary>
        ///  Market Share 생성, 저장
        /// </summary>
        public const string USP_MERGE_MICRO_MARKETING_MARKETSHARE = "[dbo].[USP_MERGE_MICRO_MARKETING_MARKETSHARE] @ID, @CURRENT_QUARTER, @USER_ID, @USER_NAME, @ORGANIZATION_ID, @ORGANIZATION_NAME, @SEGMENT, @SUB_CATEGORY, @MANUFACTURER, @PRODUCT_CODE, @PRODUCT_FAMAILY, @PRODUCT, @QUANTITY, @PRICE, @SALES, @COMMENT, @NEW_QUATER, @CREATOR_ID";

        /// <summary>
        ///  Market Share 삭제
        /// </summary>
        public const string USP_DELETE_MICRO_MARKETING_MARKETSHARE = "[dbo].[USP_DELETE_MICRO_MARKETING_MARKETSHARE] @ID";

        //Micromarketing function
        public const string USP_SELECT_MASTER_RAD_MARKETSHARE = "[dbo].[USP_SELECT_MASTER_RAD_MARKETSHARE] @ID";
        public const string USP_MERGE_MASTER_RAD_MARKETSHARE = "[dbo].[USP_MERGE_MASTER_RAD_MARKETSHARE] @ID, @MANUFACTURE, @SEGMENT, @PRODUCT_FAMILY, @PRODUCT, @PRICE, @CREATOR_ID";
        public const string USP_DELETE_MASTER_RAD_MARKETSHARE = "[dbo].[USP_DELETE_MASTER_RAD_MARKETSHARE] @ID";
        public const string USP_SELECT_SEARCH_MASTER_RAD_MARKETSHARE = "[dbo].[USP_SELECT_SEARCH_MASTER_RAD_MARKETSHARE] @PRODUCT_FAMILY, @PRODUCT";
        #endregion

        #region Non onekey
        /// <summary>
        ///  Onekey Request 데이터 조회
        /// </summary>
        public const string USP_SELECT_NON_ONEKEY = "[dbo].[USP_SELECT_NON_ONEKEY] @NON_ONEKEY_ID";
        /// <summary>
        ///  Onekey Request 리스트 조회
        /// </summary>
        public const string USP_SELECT_NON_ONEKEY_LIST = "[dbo].[USP_SELECT_NON_ONEKEY_LIST] @USER_ID, @USER_TYPE";
        
        /// <summary>
        ///  Onekey Customer 리스트 조회
        /// </summary>
        public const string USP_SELECT_SEARCH_NON_ONEKEY = "[dbo].[USP_SELECT_SEARCH_NON_ONEKEY] @CUSTOMER_TYPE, @CUSTOMER_NAME";
        
        /// <summary>
        ///  Onekey Customer 생성, 저장
        /// </summary>
        public const string USP_MERGE_NON_ONEKEY = "[dbo].[USP_MERGE_NON_ONEKEY] @NON_ONEKEY_ID, @REQUESTER_ID, @REQUEST_TYPE, @CUSTOMER_TYPE, @CUSTOMER_NAME, @GENDER, @ORGANIZATION_ID, @ORGANIZATION_NAME, @NON_ONEKEY_STATUS, @REMARK, @CREATOR_ID";
        
        /// <summary>
        ///  Onekey 병원 리스트 조회
        /// </summary>
        public const string USP_SELECT_SEARCH_HOSPITAL = "[dbo].[USP_SELECT_SEARCH_HOSPITAL] @KEYWORD";

        /// <summary>
        ///  Onekey Log 생성
        /// </summary>
        public const string USP_INSERT_NON_ONEKEY_LOG = "[dbo].[USP_INSERT_NON_ONEKEY_LOG] @NON_ONEKEY_ID, @REGISTER_ID, @LOG_TYPE, @LOG_CATEGORY, @COMMENT";

        /// <summary>
        ///  Onekey Log 생성
        /// </summary>
        public const string USP_SELECT_NON_ONEKEY_LOG_LIST = "[dbo].[USP_SELECT_NON_ONEKEY_LOG_LIST] @NON_ONEKEY_ID";

        /// <summary>
        ///  Onekey Attachment 생성
        /// </summary>
        public const string USP_INSERT_NON_ONEKEY_ATTACH_FILES = "[dbo].[USP_INSERT_NON_ONEKEY_ATTACH_FILES] @NON_ONEKEY_ID, @ATTACH_FILE_TYPE, @SEQ, @REFER_IDX, @DISPLAY_FILE_NAME, @SAVED_FILE_NAME, @FILE_SIZE, @FILE_PATH, @FILE_HANDLER_URL, @IS_DELETED, @CREATOR_ID";

        /// <summary>
        ///  Onekey Attachment 조회
        /// </summary>
        public const string USP_SELECT_NON_ONEKEY_ATTACH_FILES = "[dbo].[USP_SELECT_NON_ONEKEY_ATTACH_FILES] @NON_ONEKEY_ID, @IDXS";

        /// <summary>
        ///  Onekey Attachment 삭제
        /// </summary>
        public const string USP_DELETE_NON_ONEKEY_ATTACH_FILES = "[dbo].[USP_DELETE_NON_ONEKEY_ATTACH_FILES] @IDX, @UPDATER_ID";
        #endregion

        #region HCP Inquiry

        /// <summary>
        ///  HCP Inquiry Request 고객 검색
        /// </summary>
        public const string USP_SELECT_SEARCH_MASTER_DOCTOR = "[dbo].[USP_SELECT_SEARCH_MASTER_DOCTOR] @HCPName, @OrgName, @SpeName";

        /// <summary>
        ///  HCP Inquiry Request 신규 / 수정
        /// </summary>
        public const string USP_MERGE_HCP_INQUIRY_REQUEST = "[dbo].[USP_MERGE_HCP_INQUIRY_REQUEST] @HCP_INQUIRY_REQUEST_ID, @REQUESTER_ID, @REQUEST_TYPE, @CUSTOMER_ID, @CUSTOMER_NAME, @ORGANIZATION_ID, @ORGANIZATION_NAME, @SPECIALTY_ID, @SPECIALTY_NAME, @YEAR_FROM, @YEAR_TO, @INQUIRY_STATUS, @REMARK,@DELIVERED_TO_HCP, @CREATOR_ID";

        /// <summary>
        ///  HCP Inquiry Request 리스트 조회
        /// </summary>
        public const string USP_SELECT_HCP_INQUIRY_REQUEST_LIST = "[dbo].[USP_SELECT_HCP_INQUIRY_REQUEST_LIST] @USER_ID, @USER_TYPE";

        /// <summary>
        ///  HCP Inquiry Request 로그 조회
        /// </summary>
        public const string USP_SELECT_HCP_INQUIRY_REQUEST_LOG_LIST = "[dbo].[USP_SELECT_HCP_INQUIRY_REQUEST_LOG_LIST] @HCP_INQUIRY_REQUEST_ID";

        /// <summary>
        ///  HCP Inquiry Request 로그 조회
        /// </summary>
        public const string USP_INSERT_HCP_INQUIRY_REQUEST_LOG = "[dbo].[USP_INSERT_HCP_INQUIRY_REQUEST_LOG] @HCP_INQUIRY_REQUEST_ID, @REGISTER_ID, @LOG_TYPE, @LOG_CATEGORY, @COMMENT";

        /// <summary>
        ///  HCP Inquiry Request 로그 조회
        /// </summary>
        public const string USP_SELECT_HCP_INQUIRY_REQUEST = "[dbo].[USP_SELECT_HCP_INQUIRY_REQUEST] @HCP_INQUIRY_REQUEST_ID";
        
        #endregion
    }
}
 