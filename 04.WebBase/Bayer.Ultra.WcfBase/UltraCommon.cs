using Bayer.Ultra.Framework.Common.Dto.Common;
using Bayer.Ultra.Framework.Common.Dto.Configuration;
using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.ServiceModel.Web;


namespace Bayer.Ultra.WcfBase
{


    public class UltraCommon : IUltraCommon
    {
        public bool IsAuthentication(string account, string password)
        {
            try
            {
                using (Framework.Common.Activedirectory.LdapQuery ldap = new Framework.Common.Activedirectory.LdapQuery())
                {
                    //return ldap.IsAuthentication(account, password);
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserInfoDto GetUserInfo(string language, string user)
        {
            try
            {
                using (BSL.Common.Mgr.AuthenticationMgr_Nx mgr = new BSL.Common.Mgr.AuthenticationMgr_Nx())
                {
                    return mgr.GetUserInfo(language, user);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void InsertLoginHistory(DTO_LOGIN_HISTORY login)
        {
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Tx mgr = new BSL.Common.Mgr.CommonMgr_Tx())
                {
                    mgr.InsertLoginHistory(login.USER_ID, login.CLIENTIP, login.WINDOWUSERNAME, login.WINDOWDOMAINNAME);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserInfoDto> SelectApprovalUserList(string keyword)
        {
            try
            {
                using (BSL.Common.Mgr.UserMgr_Nx mgr = new BSL.Common.Mgr.UserMgr_Nx())
                {
                    if (string.IsNullOrEmpty(keyword)) keyword = string.Empty;
                    return mgr.SelectApprovalUserList(keyword);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserAutocompleteDto> SelectUserAutocompleteList(string keyword)
        {
            try
            {
                using (BSL.Common.Mgr.UserMgr_Nx mgr = new BSL.Common.Mgr.UserMgr_Nx())
                {
                    if (string.IsNullOrEmpty(keyword)) keyword = string.Empty;
                    return mgr.SelectUserAutocompleteList(keyword);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserAutocompleteDto> SelectUserAutocompleteList2(string keyword)
        {
            try
            {
                using (BSL.Common.Mgr.UserMgr_Nx mgr = new BSL.Common.Mgr.UserMgr_Nx())
                {
                    if (string.IsNullOrEmpty(keyword)) keyword = string.Empty;
                    return mgr.SelectUserAutocompleteList(keyword);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HealthCareProviderDto> SelectHealthCareProvider(string hcpName, string orgName, string speName, string processID)
        {
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Nx mgr = new BSL.Common.Mgr.CommonMgr_Nx())
                {
                    if (string.IsNullOrEmpty(hcpName)) hcpName = string.Empty;
                    if (string.IsNullOrEmpty(orgName)) orgName = string.Empty;
                    if (string.IsNullOrEmpty(speName)) speName = string.Empty;
                    return mgr.SelectHealthCareProvider(hcpName, orgName, speName, processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HealthCareOfficeDto> SelectHealthCareOffice(string keyword, string hcoType)
        {
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Nx mgr = new BSL.Common.Mgr.CommonMgr_Nx())
                {
                    if (string.IsNullOrEmpty(keyword)) keyword = string.Empty;
                    return mgr.SelectHealthCareOffice(keyword, hcoType);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<MasterProductDto> SelectMasterProductList(string keyword)
        {
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Nx mgr = new BSL.Common.Mgr.CommonMgr_Nx())
                {
                    if (string.IsNullOrEmpty(keyword)) keyword = string.Empty;
                    return mgr.SelectMasterProduct(keyword);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HealthCareProviderDto> SelectSearchMasterDoctor(string hcpName, string orgName, string speName)
        {
            List<HealthCareProviderDto> retValue = null;
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Nx mgr = new BSL.Common.Mgr.CommonMgr_Nx())
                {
                    if (string.IsNullOrEmpty(hcpName)) hcpName = string.Empty;
                    if (string.IsNullOrEmpty(orgName)) orgName = string.Empty;
                    if (string.IsNullOrEmpty(speName)) speName = string.Empty;
                    retValue = mgr.SelectMasterDoctor(hcpName, orgName, speName);

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retValue;
        }

        public List<HealthCareProviderDto> SelectSearchDoctor(string keyword)
        {
            List<HealthCareProviderDto> retValue = null;
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Nx mgr = new BSL.Common.Mgr.CommonMgr_Nx())
                {
                    if (string.IsNullOrEmpty(keyword)) keyword = "xxxx";
                    retValue = mgr.SelectSearchDoctor(keyword);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return retValue;
        }

        public List<MasterProductDto> SelectSampleList(string keyword, string sampletype)
        {
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Nx mgr = new BSL.Common.Mgr.CommonMgr_Nx())
                {
                    if (string.IsNullOrEmpty(keyword)) keyword = "%";
                    if (string.IsNullOrEmpty(sampletype)) sampletype = string.Empty;
                    return mgr.SelectSampleList(keyword, sampletype);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public List<MasterProductDto> SelectSampleTypeList(string keyword)
        {
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Nx mgr = new BSL.Common.Mgr.CommonMgr_Nx())
                {
                    if (string.IsNullOrEmpty(keyword)) keyword = string.Empty;
                    return mgr.SelectSampleList(keyword, "sampletype");
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public List<DTO_COMMON_MEDICAL_SOCIETY> SearchMedicalSocietyList(string status, string keyword)
        {
            try
            {
                return SelectMedicalSocietyList("0", keyword, status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_COMMON_MEDICAL_SOCIETY> SelectMedicalSocietyList(string id, string status)
        {
            try
            {
                return SelectMedicalSocietyList(id, string.Empty, status);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_COMMON_MEDICAL_SOCIETY> SelectMedicalSocietyList(string id, string keyword, string status)
        {
            int nID = -1;
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Nx mgr = new BSL.Common.Mgr.CommonMgr_Nx())
                {
                    if (string.IsNullOrEmpty(status)) status = "A";
                    if (string.IsNullOrEmpty(keyword)) keyword = string.Empty;
                    try
                    {
                        nID = Convert.ToInt32(id);
                    }
                    catch
                    {
                        nID = 0;
                    }
                    return mgr.SelectMedicalSocietyList(nID, keyword, status);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MergeMedicalSociety(DTO_COMMON_MEDICAL_SOCIETY society)
        {
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Tx mgr = new BSL.Common.Mgr.CommonMgr_Tx())
                {
                    mgr.MergeMedicalSociety(society);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteMedicalSociety(string id)
        {
            int nID = -1;
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Tx mgr = new BSL.Common.Mgr.CommonMgr_Tx())
                {
                    try
                    {
                        nID = Convert.ToInt32(id);
                    }
                    catch
                    {
                        nID = 0;
                    }
                    mgr.DeleteMedicalSociety(nID);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void UpdateDocumentList(List<DTO_USER_CONFIG_MENU_SORT> documents)
        {
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Tx mgr = new BSL.Common.Mgr.CommonMgr_Tx())
                {
                    mgr.UpdateDocumentList(documents);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_COMMON_ABSENCE> SelectDelegationList(string userid, string idx)
        {
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Nx mgr = new BSL.Common.Mgr.CommonMgr_Nx())
                {
                    if (string.IsNullOrEmpty(idx)) idx = "0";
                    return mgr.SelectDelegationList(userid, Convert.ToInt32(idx));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserInfoDto> SelectDelegationToList(string userid)
        {
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Nx mgr = new BSL.Common.Mgr.CommonMgr_Nx())
                {
                    return mgr.SelectDelegationToList(userid);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MergeDelegation(DTO_COMMON_ABSENCE delegation)
        {
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Tx mgr = new BSL.Common.Mgr.CommonMgr_Tx())
                {
                    if (string.IsNullOrEmpty(delegation.IDX)) delegation.IDX = "0";
                    mgr.MergeDelegation(delegation);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteDelegation(string userid, string idx)
        {
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Tx mgr = new BSL.Common.Mgr.CommonMgr_Tx())
                {
                    mgr.DeleteDelegation(userid, Convert.ToInt32(idx));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_COMMON_CODE_SUB> SelectCommonCode(string classCode)
        {
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Nx mgr = new BSL.Common.Mgr.CommonMgr_Nx())
                {
                    return mgr.SelectCommonCode(classCode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_COMMON_CODE_SUB> SelectCommonCodeAll()
        {
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Nx mgr = new BSL.Common.Mgr.CommonMgr_Nx())
                {
                    return mgr.SelectCommonCodeAll();
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
                using (BSL.Configuration.Mgr.ConfigurationMgr mgr = new BSL.Configuration.Mgr.ConfigurationMgr())
                {
                    return mgr.SelectConfigurationList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTO_WF_CONFIG GetConfiguration(string eventID)
        {
            try
            {
                using (BSL.Configuration.Mgr.ConfigurationMgr mgr = new BSL.Configuration.Mgr.ConfigurationMgr())
                {
                    return mgr.GetConfiguration(eventID);
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
                using (BSL.Configuration.Mgr.ConfigurationMgr mgr = new BSL.Configuration.Mgr.ConfigurationMgr())
                {
                    mgr.MergeEventConfiguration(config, companies, categories);
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
                using (BSL.Configuration.Mgr.ConfigurationMgr mgr = new BSL.Configuration.Mgr.ConfigurationMgr())
                {
                    return mgr.SelectConfigCostCategory(eventID);
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
                using (BSL.Configuration.Mgr.ConfigurationMgr mgr = new BSL.Configuration.Mgr.ConfigurationMgr())
                {
                    return mgr.SelectEventTableColumn(tableName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MergeEventApprover(DTO_WF_CONFIG_APPROVER approver, List<ConfigurationConditionDto> conditions)
        {
            try
            {
                using (BSL.Configuration.Mgr.ConfigurationMgr mgr = new BSL.Configuration.Mgr.ConfigurationMgr())
                {
                    mgr.MergeEventApprover(approver, conditions);
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
                using (BSL.Configuration.Mgr.ConfigurationMgr mgr = new BSL.Configuration.Mgr.ConfigurationMgr())
                {
                    return mgr.SelectEventApprover(eventID, location);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_WF_CONFIG_APPROVER_CONDITION> SelectEventApproverCondition(string eventID, string location, string idx)
        {
            try
            {
                using (BSL.Configuration.Mgr.ConfigurationMgr mgr = new BSL.Configuration.Mgr.ConfigurationMgr())
                {
                    return mgr.SelectEventApproverCondition(eventID, location, Convert.ToInt32(idx));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteEventApprover(string eventID, string location, string idx)
        {
            try
            {
                using (BSL.Configuration.Mgr.ConfigurationMgr mgr = new BSL.Configuration.Mgr.ConfigurationMgr())
                {
                    mgr.DeleteConfigurationApprover(eventID, location, Convert.ToInt32(idx));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MergeEventRecipient(DTO_WF_CONFIG_RECIPIENT recipient, List<ConfigurationConditionDto> conditions)
        {
            try
            {
                using (BSL.Configuration.Mgr.ConfigurationMgr mgr = new BSL.Configuration.Mgr.ConfigurationMgr())
                {
                    mgr.MergeEventRecipient(recipient, conditions);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_WF_CONFIG_RECIPIENT> SelectEventRecipient(string eventID)
        {
            try
            {
                using (BSL.Configuration.Mgr.ConfigurationMgr mgr = new BSL.Configuration.Mgr.ConfigurationMgr())
                {
                    return mgr.SelectEventRecipient(eventID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_WF_CONFIG_RECIPIENT_CONDITION> SelectEventRecipentCondition(string eventID, string idx)
        {
            try
            {
                using (BSL.Configuration.Mgr.ConfigurationMgr mgr = new BSL.Configuration.Mgr.ConfigurationMgr())
                {
                    return mgr.SelectEventRecipientCondition(eventID, Convert.ToInt32(idx));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void DeleteEventRecipient(string eventID, string idx)
        {
            try
            {
                using (BSL.Configuration.Mgr.ConfigurationMgr mgr = new BSL.Configuration.Mgr.ConfigurationMgr())
                {
                    mgr.DeleteConfigurationRecipient(eventID, Convert.ToInt32(idx));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MergeEventReviewer(DTO_WF_CONFIG_REVIEWER reviewer, List<ConfigurationConditionDto> conditions)
        {
            try
            {
                using (BSL.Configuration.Mgr.ConfigurationMgr mgr = new BSL.Configuration.Mgr.ConfigurationMgr())
                {
                    mgr.MergeEventReviewer(reviewer, conditions);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_WF_CONFIG_REVIEWER> SelectEventReviewer(string eventID)
        {
            try
            {
                using (BSL.Configuration.Mgr.ConfigurationMgr mgr = new BSL.Configuration.Mgr.ConfigurationMgr())
                {
                    return mgr.SelectEventReviewer(eventID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_WF_CONFIG_REVIEWER_CONDITION> SelectEventReviewerCondition(string eventID, string idx)
        {
            try
            {
                using (BSL.Configuration.Mgr.ConfigurationMgr mgr = new BSL.Configuration.Mgr.ConfigurationMgr())
                {
                    return mgr.SelectEventReviewerCondition(eventID, Convert.ToInt32(idx));
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteEventReviewer(string eventID, string idx)
        {
            try
            {
                using (BSL.Configuration.Mgr.ConfigurationMgr mgr = new BSL.Configuration.Mgr.ConfigurationMgr())
                {
                    mgr.DeleteConfigurationReviewer(eventID, Convert.ToInt32(idx));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MASTER_CRM_PRODUCT> SelectCRMProduct(string keyword)
        {
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Nx mgr = new BSL.Common.Mgr.CommonMgr_Nx())
                {
                    if (string.IsNullOrEmpty(keyword)) keyword = string.Empty;
                    return mgr.SelectCRMProduct(keyword);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<UserAutocompleteDto> SelectEmployeeParticipants(string processID, string keyword)
        {
            try
            {
                using (BSL.Common.Mgr.UserMgr_Nx mgr = new BSL.Common.Mgr.UserMgr_Nx())
                {
                    return mgr.SelectEmployeeParticipants(keyword, processID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MailSend(string processID, string sendMailType, string senderAddress)
        {
            try
            {
                using (BSL.Common.Mgr.CommonMgr_Tx mgr = new BSL.Common.Mgr.CommonMgr_Tx())
                {
                    mgr.MailSend(processID, sendMailType, senderAddress);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public void SendNoticeMail(string sendMailType, string searchdata)
        {
            try
            {
                string senderAddress = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.SmtpManager.Sender;
                using (Bayer.Ultra.BSL.Common.Mgr.CommonMgr_Tx mgr = new BSL.Common.Mgr.CommonMgr_Tx())
                {
                    mgr.SendNoticeMail(sendMailType, searchdata, senderAddress);
                }
            }
            catch (Exception ex)
            {
                
                throw ex;
            }
            finally
            {
            }
        }

        public void TestExceptionResponse()
        {
            throw new Exception("Exception...........");
        }
    }
}
