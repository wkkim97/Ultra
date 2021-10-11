using Bayer.Ultra.Framework.Common.Dto.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bayer.Ultra.BSL.Configuration.Mgr
{
    public class ConfigurationMgr : Framework.Database.MgrBase
    {
        public DTO_WF_CONFIG GetConfiguration(string eventID)
        {
            try
            {
                using (Dao.ConfigurationDao dao = new Dao.ConfigurationDao())
                {
                    return dao.GetConfiguration(eventID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_WF_CONFIG_COST_CATEGORY> SelectConfigCostCategory(string eventID)
        {
            try
            {
                using (Dao.ConfigurationDao dao = new Dao.ConfigurationDao())
                {
                    return dao.SelectConfigCostCategory(eventID);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public List<DTO_WF_CONFIG_LIST> SelectConfigurationList()
        {
            try
            {
                using (Dao.ConfigurationDao dao = new Dao.ConfigurationDao())
                {
                    return dao.SelectConfigurationList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MergeEventConfiguration(DTO_WF_CONFIG config, List<DTO_WF_CONFIG_COMPANY> companies, List<DTO_WF_CONFIG_COST_CATEGORY> categories)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.ConfigurationDao dao = new Dao.ConfigurationDao())
                    {
                        string eventID = config.EVENT_ID;
                        dao.MergeEventConfiguration(config);

                        dao.DeleteEventConfigurationCompany(eventID);
                        foreach (DTO_WF_CONFIG_COMPANY company in companies)
                        {
                            dao.InsertEventConfigrationCompany(company);
                        }

                        dao.DeleteEventConfigurationCostCategory(eventID);
                        foreach (DTO_WF_CONFIG_COST_CATEGORY category in categories)
                        {
                            dao.InsertEventConfigurationCostCategory(category);
                        }
                    }
                    scope.Complete();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<EventTableColumnDto> SelectEventTableColumn(string tableName)
        {
            try
            {
                using (Dao.ConfigurationDao dao = new Dao.ConfigurationDao())
                {
                    return dao.SelectEventTableColumn(tableName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_WF_CONFIG_APPROVER> SelectEventApprover(string eventID, string location)
        {
            try
            {
                using (Dao.ConfigurationDao dao = new Dao.ConfigurationDao())
                {
                    return dao.SelectEventApprover(eventID, location);
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
                using (Dao.ConfigurationDao dao = new Dao.ConfigurationDao())
                {
                    return dao.SelectEventApproverCondition(eventID, location, idx);
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
                using (Dao.ConfigurationDao dao = new Dao.ConfigurationDao())
                {
                    return dao.SelectEventRecipient(eventID);
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
                using (Dao.ConfigurationDao dao = new Dao.ConfigurationDao())
                {
                    return dao.SelectEventRecipientCondition(eventID, idx);
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
                using (Dao.ConfigurationDao dao = new Dao.ConfigurationDao())
                {
                    return dao.SelectEventReviewer(eventID);
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
                using (Dao.ConfigurationDao dao = new Dao.ConfigurationDao())
                {
                    return dao.SelectEventReviewerCondition(eventID, idx);
                }
            }
            catch
            {
                throw;
            }
        }

        public enum ConfigCondition
        {
            Equals,
            GreaterThan,
            LessThan,
            StartWith,
            NotStartWith,
            Include,
            NotInclude,
            IsNull,
            IsNotNull
        }

        private string AppendConditionString(string strSqlTemp, ConfigurationConditionDto data)
        {

            ConfigCondition configCodition = (ConfigCondition)Enum.Parse(typeof(ConfigCondition), data.CONDITION);
            string fieldName = data.FIELD_NAME;
            string strValue = string.Empty;
            if (data.FIELD_TYPE.Contains("char")) strValue = "'" + data.VALUE.Trim() + "'";
            else strValue = data.VALUE.Trim();
            switch (configCodition)
            {
                case ConfigCondition.Equals:
                    strSqlTemp += data.FIELD_NAME + "=" + strValue;
                    break;
                case ConfigCondition.GreaterThan:
                    strSqlTemp += data.FIELD_NAME + ">=" + strValue;
                    break;
                case ConfigCondition.LessThan:
                    strSqlTemp += data.FIELD_NAME + "<" + strValue;
                    break;
                case ConfigCondition.StartWith:
                    strSqlTemp += "CHARINDEX(" + strValue + ", " + data.FIELD_NAME + ") = 1";
                    break;
                case ConfigCondition.NotStartWith:
                    strSqlTemp += "CHARINDEX(" + strValue + ", " + data.FIELD_NAME + ") = 0";
                    break;
                case ConfigCondition.Include:
                    strSqlTemp += "CHARINDEX(" + strValue + ", " + data.FIELD_NAME + ") >= 1";
                    break;
                case ConfigCondition.NotInclude:
                    strSqlTemp += "CHARINDEX(" + strValue + ", " + data.FIELD_NAME + ") = 0";
                    break;
                case ConfigCondition.IsNull:
                    strSqlTemp += data.FIELD_NAME + " IS NULL";
                    break;
                case ConfigCondition.IsNotNull:
                    strSqlTemp += data.FIELD_NAME + " IS NOT NULL";
                    break;
            }

            return strSqlTemp;
        }

        private string[] CheckDisplayAndSqlCondition(string userId, List<ConfigurationConditionDto> list)
        {
            string[] values = new string[2];
            //values[0] -> Display Condition
            //values[1] -> Sql Condition;
            if (list.Count < 1) return values;
            string beforeOption = string.Empty;


            string strSql = string.Empty;
            string strSqlTemp = string.Empty;
            strSqlTemp = AppendConditionString(strSqlTemp, list[0]);
            if (list.Count == 1)
                strSql = "(" + strSqlTemp + ") ";
            list.RemoveAt(0); //첫번째 조건 처리후 삭제
            if (list.Count > 0)
            {
                beforeOption = list[0].OPTION.Trim();
            }
            int index = 0;
            foreach (ConfigurationConditionDto data in list)
            {

                if (data.OPTION.Trim().Equals(beforeOption))
                {

                    if (list.Count - 1 == index) //Option이 변하지 않은경우
                    {
                        strSqlTemp += (" " + data.OPTION + " ");
                        strSqlTemp = AppendConditionString(strSqlTemp, data);
                        strSql += "(" + strSqlTemp + ") ";
                    }
                    else
                    {
                        strSqlTemp += (" " + data.OPTION + " ");
                        strSqlTemp = AppendConditionString(strSqlTemp, data);

                    }

                }
                else
                {
                    strSql += "(" + strSqlTemp + ") ";
                    strSql += (" " + data.OPTION + " ");
                    strSqlTemp = string.Empty;
                    strSqlTemp = AppendConditionString(strSqlTemp, data);
                    if (list.Count - 1 == index)
                        strSql += "(" + strSqlTemp + ") ";
                }
                beforeOption = data.OPTION.Trim();
                index++;
            }

            values[0] = "if (" + strSql + ") then '" + userId + "'";
            values[1] = "CASE WHEN " + strSql + " THEN '" + userId + "' END";

            return values;
        }

        public void MergeEventApprover(DTO_WF_CONFIG_APPROVER approver, List<ConfigurationConditionDto> conditions)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.ConfigurationDao dao = new Dao.ConfigurationDao())
                    {
                        dao.DeleteConfigurationApproverConditionAll(approver.EVENT_ID, approver.APPROVER_LOCATION, approver.CONDITION_IDX);

                        foreach (ConfigurationConditionDto cond in conditions)
                        {
                            DTO_WF_CONFIG_APPROVER_CONDITION condition = new DTO_WF_CONFIG_APPROVER_CONDITION()
                            {
                                APPROVER_LOCATION = cond.APPROVER_LOCATION,
                                CONDITION = cond.CONDITION,
                                CONDITION_IDX = approver.CONDITION_IDX,
                                CONDITION_SEQ = cond.CONDITION_SEQ,
                                EVENT_ID = approver.EVENT_ID,
                                FIELD_NAME = cond.FIELD_NAME,
                                OPTION = cond.OPTION,
                                VALUE = cond.VALUE,
                                CREATOR_ID = approver.CREATOR_ID
                            };

                            dao.InsertConfigurationApproverCondition(condition);
                        }

                        if (approver.IS_MANDATORY.Equals("N"))
                        {
                            string[] results = CheckDisplayAndSqlCondition(approver.APPROVER_ID, conditions);
                            approver.DISPLAY_CONDITION = results[0];
                            approver.SQL_CONDITION = results[1];
                        }

                        dao.MergeEventApprover(approver);

                        scope.Complete();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void MergeEventRecipient(DTO_WF_CONFIG_RECIPIENT recipient, List<ConfigurationConditionDto> conditions)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.ConfigurationDao dao = new Dao.ConfigurationDao())
                    {
                        dao.DeleteConfigurationRecipientConditionAll(recipient.EVENT_ID, recipient.CONDITION_IDX);

                        foreach (ConfigurationConditionDto cond in conditions)
                        {
                            DTO_WF_CONFIG_RECIPIENT_CONDITION condition = new DTO_WF_CONFIG_RECIPIENT_CONDITION()
                            {
                                CONDITION = cond.CONDITION,
                                CONDITION_IDX = recipient.CONDITION_IDX,
                                CONDITION_SEQ = cond.CONDITION_SEQ,
                                EVENT_ID = recipient.EVENT_ID,
                                FIELD_NAME = cond.FIELD_NAME,
                                OPTION = cond.OPTION,
                                VALUE = cond.VALUE,
                                CREATOR_ID = recipient.CREATOR_ID
                            };

                            dao.InsertConfigurationRecipientCondition(condition);
                        }

                        if (recipient.IS_MANDATORY.Equals("N"))
                        {
                            string[] results = CheckDisplayAndSqlCondition(recipient.RECIPIENT_ID, conditions);
                            recipient.DISPLAY_CONDITION = results[0];
                            recipient.SQL_CONDITION = results[1];
                        }

                        dao.MergeEventRecipant(recipient);

                        scope.Complete();
                    }
                }
            }
            catch
            {
                throw;
            }
        }

        public void MergeEventReviewer(DTO_WF_CONFIG_REVIEWER reviewer, List<ConfigurationConditionDto> conditions)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.ConfigurationDao dao = new Dao.ConfigurationDao())
                    {
                        dao.DeleteConfigurationReviewerConditionAll(reviewer.EVENT_ID, reviewer.CONDITION_IDX);

                        foreach (ConfigurationConditionDto cond in conditions)
                        {
                            DTO_WF_CONFIG_REVIEWER_CONDITION condition = new DTO_WF_CONFIG_REVIEWER_CONDITION()
                            {
                                CONDITION = cond.CONDITION,
                                CONDITION_IDX = reviewer.CONDITION_IDX,
                                CONDITION_SEQ = cond.CONDITION_SEQ,
                                EVENT_ID = reviewer.EVENT_ID,
                                FIELD_NAME = cond.FIELD_NAME,
                                OPTION = cond.OPTION,
                                VALUE = cond.VALUE,
                                CREATOR_ID = reviewer.CREATOR_ID
                            };

                            dao.InsertConfigurationReviewerCondition(condition);
                        }

                        if (reviewer.IS_MANDATORY.Equals("N"))
                        {
                            string[] results = CheckDisplayAndSqlCondition(reviewer.REVIEWER_ID, conditions);
                            reviewer.DISPLAY_CONDITION = results[0];
                            reviewer.SQL_CONDITION = results[1];
                        }

                        dao.MergeEventReviewer(reviewer);

                        scope.Complete();
                    }
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
                using (Dao.ConfigurationDao dao = new Dao.ConfigurationDao())
                {
                    dao.DeleteConfigurationApprover(eventID, location, idx);
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
                using (Dao.ConfigurationDao dao = new Dao.ConfigurationDao())
                {
                    dao.DeleteConfigurationRecipient(eventID, idx);
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
                using (Dao.ConfigurationDao dao = new Dao.ConfigurationDao())
                {
                    dao.DeleteConfigurationReviewer(eventID, idx);
                }

            }
            catch
            {
                throw;
            }
        }
    }
}
