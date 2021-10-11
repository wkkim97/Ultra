﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Configuration
{
    public class ConfigurationContext : Framework.Database.UltraDbContext
    {
        #region Configuration 관련

        #region [ USP_SELECT_WF_CONFIG ]
        public const string USP_SELECT_WF_CONFIG = "[dbo].[USP_SELECT_WF_CONFIG] @EVENT_ID";
        #endregion

        #region [ USP_SELECT_WF_CONFIG_COST_CATEGORY ]
        public const string USP_SELECT_WF_CONFIG_COST_CATEGORY = "[dbo].[USP_SELECT_WF_CONFIG_COST_CATEGORY] @EVENT_ID";
        #endregion

        #region [ USP_SELECT_WF_EVENT_LIST ]
        public const string USP_SELECT_WF_EVENT_LIST = "[dbo].[USP_SELECT_WF_EVENT_LIST]";
        #endregion

        #region [ USP_MERGE_WF_CONFIG ]
        public const string USP_MERGE_WF_CONFIG = "[dbo].[USP_MERGE_WF_CONFIG] @EVENT_ID, @TABLE_NAME, @EVENT_NAME, @DATA_OWNER, @PREFIX_EVENT_KEY, @WEB_PAGE_NAME, @READERS_GROUP_CODE, @RETENTION_PERIOD_CODE, @CLASSIFICATION_INFO_CODE, @AFTER_TREATMENT_SERVICE, @EVENT_DESC, @OPT_FORWARD, @OPT_ADD_REVIEWER, @OPT_ADD_REVIEWER_DESC, @OPT_SHOW_EVENT_LIST, @OPT_ADD_COST_PLAN, @OPT_ADD_PARTICIPANTS, @OPT_ADD_AGENDA, @OPT_ADD_PAYMENT, @OPT_SKIP_APPROVAL, @OPT_ONLY_APPROVAL, @OPT_COUNT_RULE, @OPT_REPORT_MOHW, @OPT_REPORT_KRPIA, @AGENDA_ROLE_TYPE, @APPROVAL_TYPE_CODE, @APPROVAL_LEVEL, @APPROVAL_JOB_TITLE_CODE, @OPT_EVENT_COMPLETE_COMMENT, @OPT_EVENT_COMPLETE_COMMENT_CATEGORY, @OPT_EVENT_COMPLETE_ATTACH, @MY_EVENT_KEY, @MOHW_REPORT_TYPE, @IS_DELETED, @CREATOR_ID, @UPDATER_ID";
        #endregion

        #region [ USP_INSERT_WF_CONFIG_COMPANY ]
        public const string USP_INSERT_WF_CONFIG_COMPANY = "[dbo].[USP_INSERT_WF_CONFIG_COMPANY] @EVENT_ID, @COMPANY_CODE, @CREATOR_ID";
        #endregion

        #region [ USP_DELETE_WF_CONFIG_COMPANY ]
        public const string USP_DELETE_WF_CONFIG_COMPANY = "[dbo].[USP_DELETE_WF_CONFIG_COMPANY] @EVENT_ID";
        #endregion

        #region [ USP_INSERT_WF_CONFIG_COST_CATEGORY ]
        public const string USP_INSERT_WF_CONFIG_COST_CATEGORY = "[dbo].[USP_INSERT_WF_CONFIG_COST_CATEGORY] @EVENT_ID, @CATEGORY_CODE, @CREATOR_ID";
        #endregion

        #region [ USP_DELETE_WF_CONFIG_COST_CATEGORY ]
        public const string USP_DELETE_WF_CONFIG_COST_CATEGORY = "[dbo].[USP_DELETE_WF_CONFIG_COST_CATEGORY] @EVENT_ID";
        #endregion

        #region [ USP_SELECT_TABLE_COLUMN_INFO ]
        public const string USP_SELECT_TABLE_COLUMN_INFO = "[dbo].[USP_SELECT_TABLE_COLUMN_INFO] @TABLE_NAME";
        #endregion

        #region [ USP_SELECT_WF_CONFIG_APPROVER ]
        public const string USP_SELECT_WF_CONFIG_APPROVER = "[dbo].[USP_SELECT_WF_CONFIG_APPROVER] @EVENT_ID, @APPROVER_LOCATION";
        #endregion

        #region [ USP_SELECT_WF_CONFIG_APPROVER_CONDITION ]
        public const string USP_SELECT_WF_CONFIG_APPROVER_CONDITION = "[dbo].[USP_SELECT_WF_CONFIG_APPROVER_CONDITION] @EVENT_ID, @APPROVAL_LOCATION, @CONDITION_IDX";
        #endregion

        #region [ USP_SELECT_WF_CONFIG_RECIPIENT ]
        public const string USP_SELECT_WF_CONFIG_RECIPIENT = "[dbo].[USP_SELECT_WF_CONFIG_RECIPIENT] @EVENT_ID";
        #endregion

        #region [ USP_SELECT_WF_CONFIG_RECIPIENT_CONDITION ]
        public const string USP_SELECT_WF_CONFIG_RECIPIENT_CONDITION = " [dbo].[USP_SELECT_WF_CONFIG_RECIPIENT_CONDITION] @EVENT_ID, @CONDITION_INDEX";
        #endregion

        #region [ USP_SELECT_WF_CONFIG_REVIEWER ]
        public const string USP_SELECT_WF_CONFIG_REVIEWER = "[dbo].[USP_SELECT_WF_CONFIG_REVIEWER] @EVENT_ID";
        #endregion

        #region [ USP_SELECT_WF_CONFIG_REVIEWER_CONDITION ]
        public const string USP_SELECT_WF_CONFIG_REVIEWER_CONDITION = "[dbo].[USP_SELECT_WF_CONFIG_REVIEWER_CONDITION] @EVENT_ID, @CONDITION_IDX";
        #endregion

        #region [ USP_MERGE_WF_CONFIG_APPROVER ]
        public const string USP_MERGE_WF_CONFIG_APPROVER = "[dbo].[USP_MERGE_WF_CONFIG_APPROVER] @EVENT_ID, @APPROVER_LOCATION, @CONDITION_IDX, @IS_MANDATORY, @APPROVER_ID, @DISPLAY_CONDITION, @SQL_CONDITION, @CREATOR_ID";
        #endregion

        #region [ USP_DELETE_WF_CONFIG_APPROVER_CONDITION_ALL ]
        public const string USP_DELETE_WF_CONFIG_APPROVER_CONDITION_ALL = "[dbo].[USP_DELETE_WF_CONFIG_APPROVER_CONDITION_ALL] @EVENT_ID, @APPROVER_LOCATION, @CONDITION_IDX";
        #endregion

        #region [ USP_INSERT_WF_CONFIG_APPROVER_CONDITION ]
        public const string USP_INSERT_WF_CONFIG_APPROVER_CONDITION = "[dbo].[USP_INSERT_WF_CONFIG_APPROVER_CONDITION] @EVENT_ID, @APPROVER_LOCATION, @CONDITION_IDX, @CONDITION_SEQ, @FIELD_NAME, @CONDITION, @VALUE, @OPTION, @CREATOR_ID";
        #endregion

        #region [ USP_DELETE_WF_CONFIG_APPROVER ]
        public const string USP_DELETE_WF_CONFIG_APPROVER = "[dbo].[USP_DELETE_WF_CONFIG_APPROVER] @EVENT_ID, @APPROVER_LOCATION, @CONDITION_IDX"; 
        #endregion

        #region [ USP_MERGE_WF_CONFIG_RECIPIANT ]
        public const string USP_MERGE_WF_CONFIG_RECIPIANT = "[dbo].[USP_MERGE_WF_CONFIG_RECIPIANT] @EVENT_ID, @CONDITION_INDEX, @IS_MANDATORY, @RECIPIENT_ID, @DISPLAY_CONDITION, @SQL_CONDITION, @CREATOR_ID";
        #endregion

        #region [ USP_DELETE_WF_CONFIG_RECIPIENT_CONDITION_ALL ]
        public const string USP_DELETE_WF_CONFIG_RECIPIENT_CONDITION_ALL = "[dbo].[USP_DELETE_WF_CONFIG_RECIPIENT_CONDITION_ALL] @EVENT_ID, @CONDITION_IDX";
        #endregion

        #region [ USP_INSERT_CONFIG_RECIPIENT_CONDITION ]
        public const string USP_INSERT_CONFIG_RECIPIENT_CONDITION = "[dbo].[USP_INSERT_CONFIG_RECIPIENT_CONDITION] @EVENT_ID, @CONDITION_IDX, @CONDITION_SEQ, @FIELD_NAME, @CONDITION, @VALUE, @OPTION, @CREATOR_ID"; 
        #endregion

        #region [ USP_DELETE_WF_CONFIG_RECIPIENT_CONDITION ]
        public const string USP_DELETE_WF_CONFIG_RECIPIENT_CONDITION = "[dbo].[USP_DELETE_WF_CONFIG_RECIPIENT_CONDITION] @EVENT_ID, @CONDITION_IDX, @CONDITION_SEQ";
        #endregion

        #region [ USP_DELETE_WF_CONFIG_RECIPIENT ]
        public const string USP_DELETE_WF_CONFIG_RECIPIENT = "[dbo].[USP_DELETE_WF_CONFIG_RECIPIENT] @EVENT_ID, @CONDITION_IDX"; 
        #endregion

        #region [ USP_MERGE_WF_CONFIG_REVIEWER ]
        public const string USP_MERGE_WF_CONFIG_REVIEWER = "[dbo].[USP_MERGE_WF_CONFIG_REVIEWER] @EVENT_ID, @CONDITION_IDX, @IS_MANDATORY, @REVIEWER_ID, @DISPLAY_CONDITION, @SQL_CONDITION, @CREATOR_ID";
        #endregion

        #region [ USP_DELETE_WF_CONFIG_REVIEWER_CONDITION_ALL ]
        public const string USP_DELETE_WF_CONFIG_REVIEWER_CONDITION_ALL = "[dbo].[USP_DELETE_WF_CONFIG_REVIEWER_CONDITION_ALL] @EVENT_ID, @CONDITION_IDX";
        #endregion

        #region [ USP_INSERT_WF_CONFIG_REVIEWER_CONDITION ]
        public const string USP_INSERT_WF_CONFIG_REVIEWER_CONDITION = "[dbo].[USP_INSERT_WF_CONFIG_REVIEWER_CONDITION] @EVENT_ID, @CONDITION_IDX, @CONDITION_SEQ, @FIELD_NAME, @CONDITION, @VALUE, @OPTION, @CREATOR_ID"; 
        #endregion

        #region [ USP_DELETE_WF_CONFIG_REVIEWER_CONDITION ]
        public const string USP_DELETE_WF_CONFIG_REVIEWER_CONDITION = "[dbo].[USP_DELETE_WF_CONFIG_REVIEWER_CONDITION] @EVENT_ID, @CONDITION_IDX, @CONDITION_SEQ";
        #endregion

        #region [ USP_DELETE_WF_CONFIG_REVIEWER ]
        public const string USP_DELETE_WF_CONFIG_REVIEWER = "[dbo].[USP_DELETE_WF_CONFIG_REVIEWER] @EVENT_ID, @CONDITION_IDX"; 
        #endregion

        #endregion
    }
}