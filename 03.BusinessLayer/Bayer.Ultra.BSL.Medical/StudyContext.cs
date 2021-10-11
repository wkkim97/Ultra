using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Medical
{
    public class StudyContext : Framework.Database.UltraDbContext
    {
        /// <summary>
        /// Medical Study List SP
        /// </summary>
        public const string USP_SELECT_MEDICAL_INFO_LIST = "[dbo].[USP_SELECT_MEDICAL_INFO_LIST] @USER_ID, @IS_ADMIN";

        /// <summary>
        /// Medical study Detail SP
        /// </summary>
        public const string USP_SELECT_MEDICAL_INFO_DETAIL = "[dbo].[USP_SELECT_MEDICAL_INFO_DETAIL] @MEDICAL_IDX";

        /// <summary>
        /// Medical Study Detail Insert/Update SP
        /// </summary>
        public const string USP_MERGE_MEDICAL_INFO = "[dbo].[USP_MERGE_MEDICAL_INFO] @MEDICAL_IDX, @CATEGORY, @TYPE, @STATUS, @TEAM, @IMPACT_NO, @TITLE, @METERIAL_NO, @APPROVAL_NO, @APPROVAL_DATE, @COST_INFORMATION, @AUTHOR, @IS_DELETED, @CREATOR_ID, @CREATE_DATE, @UPDATER_ID, @UPDATE_DATE";

        /// <summary>
        /// Medical study Impact No Exists Check SP
        /// </summary>
        public const string USP_SELECT_EXISTS_MEDICAL_INFO = "[dbo].[USP_SELECT_EXISTS_MEDICAL_INFO] @MEDICAL_IDX, @IMPACT_NO";

        /// <summary>
        /// Medical Study Products List SP
        /// </summary>
        public const string USP_SELECT_MEDICAL_PRODUCTS = "[dbo].[USP_SELECT_MEDICAL_PRODUCTS] @MEDICAL_IDX";

        /// <summary>
        /// Medical Study Editor List SP
        /// </summary>
        public const string USP_SELECT_MEDICAL_REVIEWER = "[dbo].[USP_SELECT_MEDICAL_REVIEWER] @MEDICAL_IDX";
         
        /// <summary>
        /// Medical study Products Delete SP
        /// </summary>
        public const string USP_DELETE_MEDICAL_PRODUCTS = "[dbo].[USP_DELETE_MEDICAL_PRODUCTS] @MEDICAL_IDX";

        /// <summary>
        /// Medical study Editor Delete SP
        /// </summary>
        public const string USP_DELETE_MEDICAL_REVIEWER = "[dbo].[USP_DELETE_MEDICAL_REVIEWER] @MEDICAL_IDX";

        /// <summary>
        /// Medical study Editor Insert/Update SP
        /// </summary>
        public const string USP_MERGE_MEDICAL_REVIEWER = "[dbo].[USP_MERGE_MEDICAL_REVIEWER] @MEDICAL_IDX, @REVIEWER_ID, @IS_DELETED, @CREATOR_ID, @CREATE_DATE, @UPDATER_ID, @UPDATE_DATE";

        /// <summary>
        /// MEdical study Products Insert/Update SP
        /// </summary>
        public const string USP_MERGE_MEDICAL_PRODUCTS = "[dbo].[USP_MERGE_MEDICAL_PRODUCTS] @MEDICAL_IDX, @PRODUCT_CODE, @PRODUCT_NAME, @IS_DELETED, @CREATOR_ID, @CREATE_DATE, @UPDATER_ID, @UPDATE_DATE";

        /// <summary>
        /// Medical Study Info delete SP
        /// </summary>
        public const string USP_DELETE_MEDICAL_INFO = "[dbo].[USP_DELETE_MEDICAL_INFO] @MEDICAL_IDX";

        /// <summary>
        /// Medical Contract 리스트 SP
        /// </summary>
        public const string USP_SELECT_MEDICAL_CONTRACT_LIST = "[dbo].[USP_SELECT_MEDICAL_CONTRACT_LIST] @MEDICAL_IDX";

        /// <summary>
        /// Medical Contract별 공동연구가 목록리스트 SP
        /// </summary>
        public const string USP_SELECT_MEDICAL_CONTRACT_HCR_LIST = "[dbo].[USP_SELECT_MEDICAL_CONTRACT_HCR_LIST] @MEDICAL_IDX, @HCP_CODE";
 
        /// <summary>
        /// Contract(HCP) 상세 추가/수정 SP
        /// </summary>
        public const string USP_MERGE_MEDICAL_HCP_CONTRACT = "[dbo].[USP_MERGE_MEDICAL_HCP_CONTRACT] @MEDICAL_IDX, @HCP_CODE, @HCP_NAME, @HCO_CODE, @HCO_NAME, @SPECIALTY_CODE, @SPECIALTY_NAME, @CONTRACT_NO, @CONTRACT_STATUS, @CONTRACT_DATE, @CONTRACT_CHANGE_DATE, @RESEARCH_FUND, @ROLE, @REMARK, @IS_DELETED, @CREATOR_ID, @CREATE_DATE, @UPDATER_ID, @UPDATE_DATE";

        /// <summary>
        /// Contract(HCR) 공동연구자 추가/수정 SP
        /// </summary>
        public const string USP_MERGE_MEDICAL_HCR_CONTRACT = "[dbo].[USP_MERGE_MEDICAL_HCR_CONTRACT] @MEDICAL_IDX, @HCP_CODE, @HCR_CODE, @HCR_NAME, @HCO_CODE, @HCO_NAME, @SPECIALTY_CODE, @SPECIALTY_NAME, @REMARK, @IS_DELETED, @CREATOR_ID, @CREATE_DATE, @UPDATER_ID, @UPDATE_DATE";

        /// <summary>
        /// Medical Contract HCP 상세 SP
        /// </summary>
        public const string USP_SELECT_MEDICAL_CONTRACT_HCP = "[dbo].[USP_SELECT_MEDICAL_CONTRACT_HCP] @MEDICAL_IDX, @HCP_CODE";

        /// <summary>
        /// Medical Contract HCR 삭제 SP
        /// </summary>
        public const string USP_DELETE_MEDICAL_HCR_CONTRACT = "[dbo].[USP_DELETE_MEDICAL_HCR_CONTRACT] @MEDICAL_IDX, @HCP_CODE, @HCR_CODE, @UPDATER_ID, @IS_DELETED";

        /// <summary>
        /// Contract HCP delete SP
        /// </summary>
        public const string USP_DELETE_MEDICAL_HCP_CONTRACT = "[dbo].[USP_DELETE_MEDICAL_HCP_CONTRACT] @MEDICAL_IDX, @HCP_CODE";


        /// <summary>
        /// Contract HCP 같은 Medical Info에 동일한 HCP가 존재하는지 체크 SP
        /// </summary>
        public const string USP_SELECT_EXISTS_HCP_CONTRACT = "[dbo].[USP_SELECT_EXISTS_HCP_CONTRACT] @MEDICAL_IDX, @HCP_CODE";
         
        /// <summary>
        /// Medical List (Study + PMS) SP
        /// </summary>
        public const string USP_SELECT_MEDICAL_MASTER_LIST = "[dbo].[USP_SELECT_MEDICAL_MASTER_LIST] @USER_ID, @IMPACT_NO, @KEYWORD";

        // <!-- Ver 1.0.7 : Go-Direct -->
        /// <summary>
        /// RAD Injector 조회
        /// </summary>
        public const string USP_SELECT_RADINJECTOR_MASTER_LIST = "[dbo].[USP_SELECT_RADINJECTOR_MASTER_LIST] @USER_ID, @IMPACT_NO, @KEYWORD";
        // <!-- Ver 1.0.7 : Go-Direct -->

        /// <summary>
        /// Medical Info에 Impact No기준으로 HCP, HCR 조회 SP
        /// </summary>
        public const string USP_SELECT_MEDICAL_INFO_HCP_LIST = "[dbo].[USP_SELECT_MEDICAL_INFO_HCP_LIST] @IMPACT_NO, @HCPName, @OrgName, @SpeName";

        /// <summary>
        /// Medical study log 추가
        /// </summary>
        public const string USP_INSERT_MEDICAL_LOG = "[dbo].[USP_INSERT_MEDICAL_LOG] @MEDICAL_IDX";


        /// <summary>
        /// Medical Hcp log 추가
        /// </summary>
        public const string USP_INSERT_MEDICAL_HCP_CONTRACT_LOG = "[dbo].[USP_INSERT_MEDICAL_HCP_CONTRACT_LOG] @MEDICAL_IDX, @HCP_CODE";

        /// <summary>
        /// Medical Hcp Payment list 조회
        /// </summary>
        public const string USP_SELECT_MEDICAL_HCP_PAYMENT_LIST = "[dbo].[USP_SELECT_MEDICAL_HCP_PAYMENT_LIST] @MEDICAL_IDX, @HCP_CODE";

        /// <summary>
        /// Medical IMP list 조회
        /// </summary>
        public const string USP_SELECT_MEDICAL_IMP_LIST = "[dbo].[USP_SELECT_MEDICAL_IMP_LIST] @MEDICAL_IDX, @HCP_CODE";


        /// <summary>
        /// Medical Hcp Payment Insert / Update SP
        /// </summary>
        public const string USP_MERGE_MEDICAL_HCP_PAYMENT = "[dbo].[USP_MERGE_MEDICAL_HCP_PAYMENT] @MEDICAL_IDX, @HCP_CODE, @IDX, @DATE, @AMOUNT, @METHOD_TYPE, @EVIDENCE_ID, @COMMENT, @IS_DELETED, @CREATOR_ID, @CREATE_DATE, @UPDATER_ID, @UPDATE_DATE";

        /// <summary>
        /// Medical IMP Insert / Update SP
        /// </summary>
        public const string USP_MERGE_MEDICAL_IMP = "[dbo].[USP_MERGE_MEDICAL_IMP] @MEDICAL_IDX,@HCP_CODE,@IDX,@DATE,@CATEGORY,@ORDER_NO,@AIRBILL_NO,@IMP,@DOSE,@UNIT,@QTY,@TYPE,@COMMENT,@IS_DELETED,@CREATOR_ID,@CREATE_DATE,@UPDATER_ID,@UPDATE_DATE";
    }
}
