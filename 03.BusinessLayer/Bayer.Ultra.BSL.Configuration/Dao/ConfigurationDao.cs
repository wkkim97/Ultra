using Bayer.Ultra.Framework.Common.Dto.Configuration;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Configuration.Dao
{
    public class ConfigurationDao : Framework.Database.DaoBase
    {
        public DTO_WF_CONFIG GetConfiguration(string eventID)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@EVENT_ID", eventID);

                    var result = _context.Database.SqlQuery<DTO_WF_CONFIG>(ConfigurationContext.USP_SELECT_WF_CONFIG, parameters);
                    return result.FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_WF_CONFIG_COST_CATEGORY> SelectConfigCostCategory(string eventID)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@EVENT_ID", eventID);

                    var result = _context.Database.SqlQuery<DTO_WF_CONFIG_COST_CATEGORY>(ConfigurationContext.USP_SELECT_WF_CONFIG_COST_CATEGORY, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_WF_CONFIG_LIST> SelectConfigurationList()
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    var result = _context.Database.SqlQuery<DTO_WF_CONFIG_LIST>(ConfigurationContext.USP_SELECT_WF_EVENT_LIST);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public void MergeEventConfiguration(DTO_WF_CONFIG config)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[36];
                    parameters[0] = new SqlParameter("@EVENT_ID", config.EVENT_ID);
                    parameters[1] = new SqlParameter("@TABLE_NAME", config.TABLE_NAME);
                    parameters[2] = new SqlParameter("@EVENT_NAME", config.EVENT_NAME);
                    parameters[3] = new SqlParameter("@DATA_OWNER", config.DATA_OWNER);
                    parameters[4] = new SqlParameter("@PREFIX_EVENT_KEY", config.PREFIX_EVENT_KEY);
                    parameters[5] = new SqlParameter("@WEB_PAGE_NAME", config.WEB_PAGE_NAME);
                    parameters[6] = new SqlParameter("@READERS_GROUP_CODE", config.READERS_GROUP_CODE);
                    parameters[7] = new SqlParameter("@RETENTION_PERIOD_CODE", config.RETENTION_PERIOD_CODE);
                    parameters[8] = new SqlParameter("@CLASSIFICATION_INFO_CODE", config.CLASSIFICATION_INFO_CODE);
                    parameters[9] = new SqlParameter("@AFTER_TREATMENT_SERVICE", config.AFTER_TREATMENT_SERVICE);
                    parameters[10] = new SqlParameter("@EVENT_DESC", config.EVENT_DESC);
                    parameters[11] = new SqlParameter("@OPT_FORWARD", config.OPT_FORWARD);
                    parameters[12] = new SqlParameter("@OPT_ADD_REVIEWER", config.OPT_ADD_REVIEWER);
                    parameters[13] = new SqlParameter("@OPT_ADD_REVIEWER_DESC", config.OPT_ADD_REVIEWER_DESC);
                    parameters[14] = new SqlParameter("@OPT_SHOW_EVENT_LIST", config.OPT_SHOW_EVENT_LIST);
                    parameters[15] = new SqlParameter("@OPT_ADD_COST_PLAN", config.OPT_ADD_COST_PLAN);
                    parameters[16] = new SqlParameter("@OPT_ADD_PARTICIPANTS", config.OPT_ADD_PARTICIPANTS);
                    parameters[17] = new SqlParameter("@OPT_ADD_AGENDA", config.OPT_ADD_AGENDA);
                    parameters[18] = new SqlParameter("@OPT_ADD_PAYMENT", config.OPT_ADD_PAYMENT);
                    parameters[19] = new SqlParameter("@OPT_SKIP_APPROVAL", config.OPT_SKIP_APPROVAL);
                    parameters[20] = new SqlParameter("@OPT_ONLY_APPROVAL", config.OPT_ONLY_APPROVAL);
                    parameters[21] = new SqlParameter("@OPT_COUNT_RULE", config.OPT_COUNT_RULE);
                    parameters[22] = new SqlParameter("@OPT_REPORT_MOHW", config.OPT_REPORT_MOHW);
                    parameters[23] = new SqlParameter("@OPT_REPORT_KRPIA", config.OPT_REPORT_KRPIA);
                    parameters[24] = new SqlParameter("@AGENDA_ROLE_TYPE", config.AGENDA_ROLE_TYPE);
                    parameters[25] = new SqlParameter("@APPROVAL_TYPE_CODE", config.APPROVAL_TYPE_CODE);
                    parameters[26] = new SqlParameter("@APPROVAL_LEVEL", config.APPROVAL_LEVEL);
                    parameters[27] = new SqlParameter("@APPROVAL_JOB_TITLE_CODE", config.APPROVAL_JOB_TITLE_CODE);
                    parameters[28] = new SqlParameter("@OPT_EVENT_COMPLETE_COMMENT", config.OPT_EVENT_COMPLETE_COMMENT);
                    parameters[29] = new SqlParameter("@OPT_EVENT_COMPLETE_COMMENT_CATEGORY", config.OPT_EVENT_COMPLETE_COMMENT_CATEGORY);
                    parameters[30] = new SqlParameter("@OPT_EVENT_COMPLETE_ATTACH", config.OPT_EVENT_COMPLETE_ATTACH);
                    parameters[31] = new SqlParameter("@MY_EVENT_KEY", config.MY_EVENT_KEY);
                    parameters[32] = new SqlParameter("@MOHW_REPORT_TYPE", config.MOHW_REPORT_TYPE);
                    parameters[33] = new SqlParameter("@IS_DELETED", config.IS_DELETED);
                    parameters[34] = new SqlParameter("@CREATOR_ID", config.CREATOR_ID);
                    parameters[35] = new SqlParameter("@UPDATER_ID", config.UPDATER_ID);
                    _context.Database.ExecuteSqlCommand(ConfigurationContext.USP_MERGE_WF_CONFIG, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public void InsertEventConfigrationCompany(DTO_WF_CONFIG_COMPANY company)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@EVENT_ID", company.EVENT_ID);
                    parameters[1] = new SqlParameter("@COMPANY_CODE", company.COMPANY_CODE);
                    parameters[2] = new SqlParameter("@CREATOR_ID", company.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ConfigurationContext.USP_INSERT_WF_CONFIG_COMPANY, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteEventConfigurationCompany(string eventID)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@EVENT_ID", eventID);

                    _context.Database.ExecuteSqlCommand(ConfigurationContext.USP_DELETE_WF_CONFIG_COMPANY, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public void InsertEventConfigurationCostCategory(DTO_WF_CONFIG_COST_CATEGORY cost)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@EVENT_ID", cost.EVENT_ID);
                    parameters[1] = new SqlParameter("@CATEGORY_CODE", cost.CATEGORY_CODE);
                    parameters[2] = new SqlParameter("@CREATOR_ID", cost.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ConfigurationContext.USP_INSERT_WF_CONFIG_COST_CATEGORY, parameters);

                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteEventConfigurationCostCategory(string eventID)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@EVENT_ID", eventID);

                    _context.Database.ExecuteSqlCommand(ConfigurationContext.USP_DELETE_WF_CONFIG_COST_CATEGORY, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public List<EventTableColumnDto> SelectEventTableColumn(string tableName)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@TABLE_NAME", tableName);
                    var result = _context.Database.SqlQuery<EventTableColumnDto>(ConfigurationContext.USP_SELECT_TABLE_COLUMN_INFO, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_WF_CONFIG_APPROVER> SelectEventApprover(string eventID, string location)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@EVENT_ID", eventID);
                    parameters[1] = new SqlParameter("@APPROVER_LOCATION", location);

                    var result = _context.Database.SqlQuery<DTO_WF_CONFIG_APPROVER>(ConfigurationContext.USP_SELECT_WF_CONFIG_APPROVER, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_WF_CONFIG_APPROVER_CONDITION> SelectEventApproverCondition(string eventID, string location, int idx)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@EVENT_ID", eventID);
                    parameters[1] = new SqlParameter("@APPROVAL_LOCATION", location);
                    parameters[2] = new SqlParameter("@CONDITION_IDX", idx);

                    var result = _context.Database.SqlQuery<DTO_WF_CONFIG_APPROVER_CONDITION>(ConfigurationContext.USP_SELECT_WF_CONFIG_APPROVER_CONDITION, parameters);
                    return result.ToList();

                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_WF_CONFIG_RECIPIENT> SelectEventRecipient(string eventID)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@EVENT_ID", eventID);

                    var result = _context.Database.SqlQuery<DTO_WF_CONFIG_RECIPIENT>(ConfigurationContext.USP_SELECT_WF_CONFIG_RECIPIENT, parameters);
                    return result.ToList();

                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_WF_CONFIG_RECIPIENT_CONDITION> SelectEventRecipientCondition(string eventID, int idx)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@EVENT_ID", eventID);
                    parameters[1] = new SqlParameter("@CONDITION_INDEX", idx);

                    var result = _context.Database.SqlQuery<DTO_WF_CONFIG_RECIPIENT_CONDITION>(ConfigurationContext.USP_SELECT_WF_CONFIG_RECIPIENT_CONDITION, parameters);
                    return result.ToList();

                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_WF_CONFIG_REVIEWER> SelectEventReviewer(string eventID)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@EVENT_ID", eventID);

                    var result = _context.Database.SqlQuery<DTO_WF_CONFIG_REVIEWER>(ConfigurationContext.USP_SELECT_WF_CONFIG_REVIEWER, parameters);
                    return result.ToList();

                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_WF_CONFIG_REVIEWER_CONDITION> SelectEventReviewerCondition(string eventID, int idx)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@EVENT_ID", eventID);
                    parameters[1] = new SqlParameter("@CONDITION_INDEX", idx);

                    var result = _context.Database.SqlQuery<DTO_WF_CONFIG_REVIEWER_CONDITION>(ConfigurationContext.USP_SELECT_WF_CONFIG_REVIEWER_CONDITION, parameters);
                    return result.ToList();

                }
            }
            catch
            {
                throw;
            }
        }

        public void MergeEventApprover(DTO_WF_CONFIG_APPROVER approver)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[8];
                    parameters[0] = new SqlParameter("@EVENT_ID", approver.EVENT_ID);
                    parameters[1] = new SqlParameter("@APPROVER_LOCATION", approver.APPROVER_LOCATION);
                    parameters[2] = new SqlParameter("@CONDITION_IDX", approver.CONDITION_IDX);
                    parameters[3] = new SqlParameter("@IS_MANDATORY", approver.IS_MANDATORY);
                    parameters[4] = new SqlParameter("@APPROVER_ID", approver.APPROVER_ID);
                    parameters[5] = new SqlParameter("@DISPLAY_CONDITION", approver.DISPLAY_CONDITION);
                    parameters[6] = new SqlParameter("@SQL_CONDITION", approver.SQL_CONDITION);
                    parameters[7] = new SqlParameter("@CREATOR_ID", approver.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ConfigurationContext.USP_MERGE_WF_CONFIG_APPROVER, parameters);

                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 결재문서 Additional Approver의 조건을 전체삭제한다. 등록시 전체삭제 후 저장을 위해
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="location"></param>
        /// <param name="index"></param>
        public void DeleteConfigurationApproverConditionAll(string documentId, string location, int index)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@EVENT_ID", documentId);
                    parameters[1] = new SqlParameter("@APPROVER_LOCATION", location);
                    parameters[2] = new SqlParameter("@CONDITION_IDX", index);

                    _context.Database.ExecuteSqlCommand(ConfigurationContext.USP_DELETE_WF_CONFIG_APPROVER_CONDITION_ALL, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 결재문서 Additional Approver의 조건을 저장한다.
        /// </summary>
        /// <param name="condition"></param>
        public void InsertConfigurationApproverCondition(DTO_WF_CONFIG_APPROVER_CONDITION condition)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[9];
                    parameters[0] = new SqlParameter("@EVENT_ID", condition.EVENT_ID);
                    parameters[1] = new SqlParameter("@APPROVER_LOCATION", condition.APPROVER_LOCATION);
                    parameters[2] = new SqlParameter("@CONDITION_IDX", condition.CONDITION_IDX);
                    parameters[3] = new SqlParameter("@CONDITION_SEQ", condition.CONDITION_SEQ);
                    parameters[4] = new SqlParameter("@FIELD_NAME", condition.FIELD_NAME);
                    parameters[5] = new SqlParameter("@CONDITION", condition.CONDITION);
                    parameters[6] = new SqlParameter("@VALUE", condition.VALUE);
                    parameters[7] = new SqlParameter("@OPTION", condition.OPTION);
                    parameters[8] = new SqlParameter("@CREATOR_ID", condition.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ConfigurationContext.USP_INSERT_WF_CONFIG_APPROVER_CONDITION, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public void MergeEventRecipant(DTO_WF_CONFIG_RECIPIENT recipient)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[7];
                    parameters[0] = new SqlParameter("@EVENT_ID", recipient.EVENT_ID);
                    parameters[1] = new SqlParameter("@CONDITION_INDEX", recipient.CONDITION_IDX);
                    parameters[2] = new SqlParameter("@IS_MANDATORY", recipient.IS_MANDATORY);
                    parameters[3] = new SqlParameter("@RECIPIENT_ID", recipient.RECIPIENT_ID);
                    parameters[4] = new SqlParameter("@DISPLAY_CONDITION", recipient.DISPLAY_CONDITION);
                    parameters[5] = new SqlParameter("@SQL_CONDITION", recipient.SQL_CONDITION);
                    parameters[6] = new SqlParameter("@CREATOR_ID", recipient.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ConfigurationContext.USP_MERGE_WF_CONFIG_RECIPIANT, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteConfigurationRecipientConditionAll(string documentId, int index)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@EVENT_ID", documentId);
                    parameters[1] = new SqlParameter("@CONDITION_IDX", index);

                    _context.Database.ExecuteSqlCommand(ConfigurationContext.USP_DELETE_WF_CONFIG_RECIPIENT_CONDITION_ALL, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 결재문서 Additional Approver의 조건을 저장한다.
        /// </summary>
        /// <param name="condition"></param>
        public void InsertConfigurationRecipientCondition(DTO_WF_CONFIG_RECIPIENT_CONDITION condition)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[8];
                    parameters[0] = new SqlParameter("@EVENT_ID", condition.EVENT_ID);
                    parameters[1] = new SqlParameter("@CONDITION_IDX", condition.CONDITION_IDX);
                    parameters[2] = new SqlParameter("@CONDITION_SEQ", condition.CONDITION_SEQ);
                    parameters[3] = new SqlParameter("@FIELD_NAME", condition.FIELD_NAME);
                    parameters[4] = new SqlParameter("@CONDITION", condition.CONDITION);
                    parameters[5] = new SqlParameter("@VALUE", condition.VALUE);
                    parameters[6] = new SqlParameter("@OPTION", condition.OPTION);
                    parameters[7] = new SqlParameter("@CREATOR_ID", condition.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ConfigurationContext.USP_INSERT_CONFIG_RECIPIENT_CONDITION, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public void MergeEventReviewer(DTO_WF_CONFIG_REVIEWER reviewer)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[7];
                    parameters[0] = new SqlParameter("@EVENT_ID", reviewer.EVENT_ID);
                    parameters[1] = new SqlParameter("@CONDITION_IDX", reviewer.CONDITION_IDX);
                    parameters[2] = new SqlParameter("@IS_MANDATORY", reviewer.IS_MANDATORY);
                    parameters[3] = new SqlParameter("@REVIEWER_ID", reviewer.REVIEWER_ID);
                    parameters[4] = new SqlParameter("@DISPLAY_CONDITION", reviewer.DISPLAY_CONDITION);
                    parameters[5] = new SqlParameter("@SQL_CONDITION", reviewer.SQL_CONDITION);
                    parameters[6] = new SqlParameter("@CREATOR_ID", reviewer.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ConfigurationContext.USP_MERGE_WF_CONFIG_REVIEWER, parameters);

                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteConfigurationReviewerConditionAll(string eventID, int index)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@EVENT_ID", eventID);
                    parameters[1] = new SqlParameter("@CONDITION_IDX", index);

                    _context.Database.ExecuteSqlCommand(ConfigurationContext.USP_DELETE_WF_CONFIG_REVIEWER_CONDITION_ALL, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public void InsertConfigurationReviewerCondition(DTO_WF_CONFIG_REVIEWER_CONDITION condition)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[8];
                    parameters[0] = new SqlParameter("@EVENT_ID", condition.EVENT_ID);
                    parameters[1] = new SqlParameter("@CONDITION_IDX", condition.CONDITION_IDX);
                    parameters[2] = new SqlParameter("@CONDITION_SEQ", condition.CONDITION_SEQ);
                    parameters[3] = new SqlParameter("@FIELD_NAME", condition.FIELD_NAME);
                    parameters[4] = new SqlParameter("@CONDITION", condition.CONDITION);
                    parameters[5] = new SqlParameter("@VALUE", condition.VALUE);
                    parameters[6] = new SqlParameter("@OPTION", condition.OPTION);
                    parameters[7] = new SqlParameter("@CREATOR_ID", condition.CREATOR_ID);

                    _context.Database.ExecuteSqlCommand(ConfigurationContext.USP_INSERT_WF_CONFIG_REVIEWER_CONDITION, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteConfigurationApprover(string eventID, string location, int idx)
        {
            try
            {
                using(_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@EVENT_ID", eventID);
                    parameters[1] = new SqlParameter("@APPROVER_LOCATION", location);
                    parameters[2] = new SqlParameter("@CONDITION_IDX", idx);

                    _context.Database.ExecuteSqlCommand(ConfigurationContext.USP_DELETE_WF_CONFIG_APPROVER, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteConfigurationRecipient(string eventID, int idx)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@EVENT_ID", eventID);
                    parameters[1] = new SqlParameter("@CONDITION_IDX", idx);

                    _context.Database.ExecuteSqlCommand(ConfigurationContext.USP_DELETE_WF_CONFIG_RECIPIENT, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteConfigurationReviewer(string eventID, int idx)
        {
            try
            {
                using (_context = new ConfigurationContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@EVENT_ID", eventID);
                    parameters[1] = new SqlParameter("@CONDITION_IDX", idx);

                    _context.Database.ExecuteSqlCommand(ConfigurationContext.USP_DELETE_WF_CONFIG_REVIEWER, parameters);
                }
            }
            catch
            {
                throw;
            }
        }
    }
}
