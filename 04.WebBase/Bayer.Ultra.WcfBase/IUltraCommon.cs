using Bayer.Ultra.Framework.Common.Dto.Common;
using Bayer.Ultra.Framework.Common.Dto.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.WcfBase
{
    
    [ServiceContract]
    
    public interface IUltraCommon
    {        
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "checkad")]
        bool IsAuthentication(string account, string password);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "getuserinfo")]
        UserInfoDto GetUserInfo(string language, string user);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "insertloginhistory")]
        void InsertLoginHistory(DTO_LOGIN_HISTORY login);


        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "selectapprovaluserlist/{keyword=null}")]
        List<UserInfoDto> SelectApprovalUserList(string keyword);

        /// <summary>
        /// AutocompleteBox 직원조회
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "selectuserautocompletelist/{keyword=null}")]
        List<UserAutocompleteDto> SelectUserAutocompleteList(string keyword);

        /// <summary>
        /// AutocompleteBox 직원조회
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "selectuserautocompletelist2")]
        List<UserAutocompleteDto> SelectUserAutocompleteList2(string keyword);

        /// <summary>
        /// 의사/약사/간호사 조회
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectHCP")]
        List<HealthCareProviderDto> SelectHealthCareProvider(string hcpName, string orgName, string speName, string processID);


        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectHCO")]
        List<HealthCareOfficeDto> SelectHealthCareOffice(string keyword, string hcoType);


        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "SelectMasterProductList/{keyword=null}")]
        List<MasterProductDto> SelectMasterProductList(string keyword);


        [OperationContract]
        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "SelectSearchMasterDoctor")]
        List<HealthCareProviderDto> SelectSearchMasterDoctor(string hcpName, string orgName, string speName);

        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "SelectSearchDoctor/?q={keyword}")]
        List<HealthCareProviderDto> SelectSearchDoctor(string keyword);

        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "SelectSampleList/{keyword=null}/{sampletype=null}")]
        List<MasterProductDto> SelectSampleList(string keyword, string sampletype);

        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "SelectMedicalSocietyList/{id}/{status=null}")]
        List<DTO_COMMON_MEDICAL_SOCIETY> SelectMedicalSocietyList(string id, string status);

        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "SearchMedicalSocietyList/{status}/{keyword=null}")]
        List<DTO_COMMON_MEDICAL_SOCIETY> SearchMedicalSocietyList(string status, string keyword);

        [OperationContract]
        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "MergeMedicalSociety")]
        void MergeMedicalSociety(DTO_COMMON_MEDICAL_SOCIETY society);

        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "DeleteMedicalSociety/{id}")]
        void DeleteMedicalSociety(string id);


        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "SelectSampleTypeList/{keyword=null}")]
        List<MasterProductDto> SelectSampleTypeList(string keyword);


        [OperationContract]
        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "UpdateDocumentList")]
        void UpdateDocumentList(List<DTO_USER_CONFIG_MENU_SORT> documents);

        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "SelectDelegationList/{userid}/{idx=null}")]
        List<DTO_COMMON_ABSENCE> SelectDelegationList(string userid, string idx);

        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "SelectDelegationToList/{userid}")]
        List<UserInfoDto> SelectDelegationToList(string userid);

        [OperationContract]
        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "MergeDelegation")]
        void MergeDelegation(DTO_COMMON_ABSENCE delegation);

        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "DeleteDelegation/{userid}/{idx}")]
        void DeleteDelegation(string userid, string idx);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectCommonCode/{classCode}")]
        List<DTO_COMMON_CODE_SUB> SelectCommonCode(string classCode);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectCommonCodeAll")]
        List<DTO_COMMON_CODE_SUB> SelectCommonCodeAll();

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectConfigurationList")]
        List<DTO_WF_CONFIG_LIST> SelectConfigurationList();

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "GetConfiguration/{eventID}")]
        DTO_WF_CONFIG GetConfiguration(string eventID);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "MergeEventConfiguration")]
        void MergeEventConfiguration(DTO_WF_CONFIG config, List<DTO_WF_CONFIG_COMPANY> companies, List<DTO_WF_CONFIG_COST_CATEGORY> categories);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectConfigCostCategory/{eventID}")]
        List<DTO_WF_CONFIG_COST_CATEGORY> SelectConfigCostCategory(string eventID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectEventTableColumn/{tableName}")]
        List<EventTableColumnDto> SelectEventTableColumn(string tableName);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "MergeEventApprover")]
        void MergeEventApprover(DTO_WF_CONFIG_APPROVER approver, List<ConfigurationConditionDto> conditions);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectEventApprover/{eventID}/{location}")]
        List<DTO_WF_CONFIG_APPROVER> SelectEventApprover(string eventID, string location);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectEventApproverCondition/{eventID}/{location}/{idx}")]
        List<DTO_WF_CONFIG_APPROVER_CONDITION> SelectEventApproverCondition(string eventID, string location, string idx);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "DeleteEventApprover/{eventID}/{location}/{idx}")]
        void DeleteEventApprover(string eventID, string location, string idx);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "MergeEventRecipient")]
        void MergeEventRecipient(DTO_WF_CONFIG_RECIPIENT recipient, List<ConfigurationConditionDto> conditions);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectEventRecipiant/{eventID}")]
        List<DTO_WF_CONFIG_RECIPIENT> SelectEventRecipient(string eventID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectEventRecipantCondition/{eventID}/{idx}")]
        List<DTO_WF_CONFIG_RECIPIENT_CONDITION> SelectEventRecipentCondition(string eventID, string idx);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "DeleteEventRecipient/{eventID}/{idx}")]
        void DeleteEventRecipient(string eventID, string idx);

        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "MergeEventReviewer")]
        void MergeEventReviewer(DTO_WF_CONFIG_REVIEWER reviewer, List<ConfigurationConditionDto> conditions);


        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectEventReviewer/{eventID}")]
        List<DTO_WF_CONFIG_REVIEWER> SelectEventReviewer(string eventID);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectEventReviewerCondition/{eventID}/{idx}")]
        List<DTO_WF_CONFIG_REVIEWER_CONDITION> SelectEventReviewerCondition(string eventID, string idx);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "DeleteEventReviewer/{eventID}/{idx}")]
        void DeleteEventReviewer(string eventID, string idx);


        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectCRMProduct/{keyword=null}")]
        List<DTO_MASTER_CRM_PRODUCT> SelectCRMProduct(string keyword);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectEmployeeParticipants/{processID}/{keyword=null}")]
        List<UserAutocompleteDto> SelectEmployeeParticipants(string processID, string keyword);

        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "MailSend/{processID}/{sendMailType}/{senderAddress}")]
        void MailSend(string processID, string sendMailType, string senderAddress);

        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "SendNoticeMail/{sendMailType}/{searchdata}")]
        void SendNoticeMail(string sendMailType, string searchdata);

        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "TestExceptionResponse")]
        void TestExceptionResponse();
    }
}
