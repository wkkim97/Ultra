using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Medical
{
    public class PmsContext : Framework.Database.UltraDbContext
    { 
        /// <summary>
        /// PMS 조회 SP
        /// </summary>
        public const string USP_SELECT_MEDICAL_PMS_LIST = "[dbo].[USP_SELECT_MEDICAL_PMS_LIST] @USER_ID, @IS_ADMIN";

        /// <summary>
        /// PMS 추가/수정 SP
        /// </summary>
        public const string USP_MERGE_MEDICAL_PMS = "[dbo].[USP_MERGE_MEDICAL_PMS] @IDX, @CREATOR_ID, @PMS_HCP_CODE, @HCP_NAME, @HCO_CODE, @HCO_NAME, @REVIEW_YN, @DATE, @PRODUCT_CODE, @PRODUCT_NAME, @COST, @NUMBER, @AMOUNT, @METHOD_TYPE, @CONTRACT_ID, @EVIDENCE_ID, @VALIDATEYN, @REMARK, @IS_DELETED, @CREATE_DATE, @UPDATER_ID, @UPDATE_DATE";

        /// <summary>
        /// PMS 상세조회 SP
        /// </summary>
        public const string USP_SELECT_MEDICAL_PMS = "[dbo].[USP_SELECT_MEDICAL_PMS] @IDX";

        /// <summary>
        /// PMS INSERT LOG SP
        /// </summary>
        public const string USP_INSERT_MEDICAL_PMS_LOG = "[dbo].[USP_INSERT_MEDICAL_PMS_LOG] @IDX";
    }
}
