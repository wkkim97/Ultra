using Bayer.Ultra.Framework.Common.Dto.Radiology;
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
    public interface IUltraRadiology
    {
        #region Micro Marketing

        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.WrappedRequest,
         UriTemplate = "SelectHospitalList")]
        List<AssignedHospitalListDto> SelectHospitalList(string user_id, string user_type);
        
        //Equipment
        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.WrappedRequest,
         UriTemplate = "SelectEquipment")]
        List<HospitalEquipmentDto> SelectEquipment(string id, string organization_id);

        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "MergeEquipment")]
        string MergeEquipment(HospitalEquipmentDto dto);
        
        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.WrappedRequest,
         UriTemplate = "DeleteEquipment")]
        string DeleteEquipment(string id);

        //Examination
        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.WrappedRequest,
         UriTemplate = "SelectExamination")]
        List<HospitalExaminationDto> SelectExamination(string id, string organization_id, string quarter);

        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "MergeExamination")]
        string MergeExamination(MergeHospitalExaminationDto dto);

        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.WrappedRequest,
         UriTemplate = "DeleteExamination")]
        string DeleteExamination(string id);

        //Market Share
        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.WrappedRequest,
         UriTemplate = "SelectMarketShare")]
        List<HospitalMarketShareDto> SelectMarketShare(string id, string organization_id, string quarter);

        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "MergeMarketShare")]
        string MergeMarketShare(MergeHospitalMarketShareDto dto);

        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.WrappedRequest,
         UriTemplate = "DeleteMarketShare")]
        string DeleteMarketShare(string id);

        #endregion

        #region Master Market Share
        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.WrappedRequest,
         UriTemplate = "SelectMasterMarketShare")]
        List<MasterMarketShare> SelectMasterMarketShare(string id);

        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "MergeMasterMarketShare")]
        string MergeMasterMarketShare(MasterMarketShare mastermarketshare);

        [OperationContract]
        [WebInvoke(Method = "GET",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "DeleteMasterMarketShare/{id}")]
        string DeleteMasterMarketShare(string id);

        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.WrappedRequest,
         UriTemplate = "SelectSearchMasterMarketShare")]
        List<MasterMarketShare> SelectSearchMasterMarketShare(string family, string product);

        #endregion

        #region Non OneKey
        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.WrappedRequest,
         UriTemplate = "SelectNonOnekeyList")]
        List<AssignedNonOneKeyListDto> SelectNonOnekeyList(string user_id, string user_type);

        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.WrappedRequest,
         UriTemplate = "SelectCustomerList")]
        List<CustomerListDto> SelectCustomerList(string customer_type, string customer_name);
        
        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.WrappedRequest,
         UriTemplate = "SelectNonOneKeyHospitalList")]
        List<HospitalListDto> SelectNonOneKeyHospitalList(string keyword);

        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "InsertLog")]
        string InsertLog(InsertLogDto dto);

        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.WrappedRequest,
         UriTemplate = "SelectLog")]
        List<SelectLogDto> SelectLog(string NON_ONEKEY_ID);
        
        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.WrappedRequest,
         UriTemplate = "SelectNonOnekeyAttachFile")]
        List<NonOnekeyAttachDto> SelectNonOnekeyAttachFile(int NON_ONEKEY_ID, string IDXS);

        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.WrappedRequest,
         UriTemplate = "SendMail")]
        string SendMail(List<NonOnekeyIdDto> NON_ONEKEY_ID, string AttachType, string Status, string FromAddress, string ToAddress, string CC);

        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "MergeCustomerData")]
        int MergeCustomerData(MergeCustomerDto customerDto);
        #endregion

        #region HCP Inquiry

        [OperationContract]
        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "SelectInquiryCustomerList")]
        List<HCPInquiryCustomerDto> SelectInquiryCustomerList(string name, string org, string specialty);

        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "MergeCustomerRequest")]
        string MergeCustomerRequest(HCPInquiryMergeDto customerDto);

        [OperationContract]
        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "SelectHCPInquiryList")]
        List<HCPInquiryListDto> SelectHCPInquiryList(string USER_ID, string USER_TYPE);

        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "InsertHCPInquiryLog")]
        string InsertHCPInquiryLog(InsertHCPInquiryLogDto dto);

        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.WrappedRequest,
         UriTemplate = "SelectHCPInquiryLog")]
        List<SelectHCPInquiryLogDto> SelectHCPInquiryLog(string HCP_INQUIRY_REQUEST_ID);

        [OperationContract]
        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.WrappedRequest,
         UriTemplate = "SendHCPInquiryMail")]
        string SendHCPInquiryMail(List<HCPInquiryIdDto> IDs, string sendMailType, string Status, string FromAddress, string CC);

        #endregion
    }
}
