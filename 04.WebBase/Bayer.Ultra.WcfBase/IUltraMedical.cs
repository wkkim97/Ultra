using Bayer.Ultra.Framework.Common.Dto.Common;
using Bayer.Ultra.Framework.Common.Dto.Medical;
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
    public interface IUltraMedical
    {
        #region Study List
        /// <summary>
        /// Study List 조회
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectMedicalStudyList/{userID}/{isAdmin}")]
        List<DTO_MEDICAL_INFO> SelectMedicalStudyList(string userID, string isAdmin);
        #endregion

        #region Select Medical Study Detail
        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "SelectMedicalInfo/{medicalIdx}")]
        MedicalInfoDto SelectMedicalInfo(string medicalIdx);
        #endregion

        #region ModifyMedicalInfo
        [OperationContract]
        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "ModifyMedicalInfo")]
        string ModifyMedicalInfo(DTO_MEDICAL_INFO dto);
        #endregion

        #region IsMedicalInfoImpactNo
        [OperationContract]
        [WebInvoke(Method = "GET",
RequestFormat = WebMessageFormat.Json,
ResponseFormat = WebMessageFormat.Json,
BodyStyle = WebMessageBodyStyle.WrappedRequest,
UriTemplate = "IsMedicalInfoImpactNo/{medicalIdx}/{impactNo}")]
        string IsMedicalInfoImpactNo(string medicalIdx, string impactNo);
        #endregion

        #region SelectMedicalProducts
        /// <summary>
        /// Study Products 조회
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectMedicalProducts/{medicalIdx}")]
        List<DTO_MEDICAL_PRODUCTS> SelectMedicalProducts(string medicalIdx);
        #endregion

        #region SelectMedicalReviewer
        /// <summary>
        /// Study Editor(Reviewer) 조회
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectMedicalReviewer/{medicalIdx}")]
        List<reviewerDto> SelectMedicalReviewer(string medicalIdx);
        #endregion

        #region ModifyMedical
        [OperationContract]
        [WebInvoke(Method = "POST",
       RequestFormat = WebMessageFormat.Json,
       ResponseFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "ModifyMedical")]
        string ModifyMedical(MedicalDto dto);
        #endregion

        #region DeleteMedicalInfo
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "DeleteMedicalInfo/{medicalIdx}")]
        string DeleteMedicalInfo(string medicalIdx);
        #endregion

        #region SelectContractList
        /// <summary>
        /// COntract HCP List 조회
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectContractList/{medicalIdx}")]
        List<ContractDto> SelectContractList(string medicalIdx);
        #endregion

        #region SelectContractHCRList
        /// <summary>
        /// Contract HCP(공동연구가) List 조회
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectContractHCRList/{medicalIdx}/{hcpCode}")]
        List<ContractHCRDto> SelectContractHCRList(string medicalIdx, string hcpCode);
        #endregion
         
        #region SelectContractDetail
        /// <summary>
        /// COntract HCP 상세 조회
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectContractDetail/{medicalIdx}/{hcpCode=null}")]
        DTO_MEDICAL_HCP_CONTRACT SelectContractDetail(string medicalIdx, string hcpCode);
        #endregion
         
        #region ModifyHcpContract
        [OperationContract]
        [WebInvoke(Method = "POST",
       RequestFormat = WebMessageFormat.Json,
       ResponseFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "ModifyHcpContract")]
        string ModifyHcpContract(DTO_MEDICAL_HCP_CONTRACT dto);
        #endregion

        #region ModifyHcrContract
        [OperationContract]
        [WebInvoke(Method = "POST",
       RequestFormat = WebMessageFormat.Json,
       ResponseFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "ModifyHcrContract")]
        string ModifyHcrContract(List<DTO_MEDICAL_HCR_CONTRACT> dto);
        #endregion

        #region DeleteContractHCP
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "DeleteContractHCP/{medicalIdx}/{hcpcode}")]
        string DeleteContractHCP(string medicalIdx, string hcpcode);
        #endregion

        #region IsExistsHCPContract
        [OperationContract]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "IsExistsHCPContract/{medicalIdx}/{hcpcode}")]
        string IsExistsHCPContract(string medicalIdx, string hcpcode);
        #endregion

        #region PMS List
        /// <summary>
        /// Study List 조회
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectMedicalPmsList/{userID}/{isAdmin}")]
        List<pmslistDto> SelectMedicalPmsList(string userID, string isAdmin);
        #endregion

        #region PMS download
        /// <summary>
        /// Study List 조회
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectXlsMedicalPmsList/{userID}/{isAdmin}")]
        string SelectXlsMedicalPmsList(string userID, string isAdmin);
        #endregion

        #region ModifyPMS
        [OperationContract]
        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "ModifyPms")]
        string ModifyPms(DTO_MEDICAL_PMS dto);
        #endregion
         
        #region PMS Detail
        /// <summary>
        /// Study List 조회
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectMedicalPms/{idx}")]
        pmslistDto SelectMedicalPms(string idx);
		#endregion


		#region Medical List
		/// <summary>
		/// Medical List 조회 ( Study + PMS ) - ClinicalRelatedMeeting 페이지에서 Impact No, Title 사용
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		[OperationContract]
		[WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectMedicalMasterList/{userID}/{impactNo=null}/{keyword=null}")]
        List<DTO_MEDICAL_INFO> SelectMedicalMasterList(string userID, string impactNo, string keyword);
        #endregion

        // <!-- Ver 1.0.7 : Go-Direct -->
        #region Medical List
        /// <summary>
        /// RAD Injector 조회
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectRADInjectorMasterList/{userID}/{impactNo=null}/{keyword=null}")]
        List<DTO_RADINJECTOR_LIST> SelectRADInjectorMasterList(string userID, string impactNo, string keyword);
        #endregion
        // <!-- Ver 1.0.7 : Go-Direct -->

        #region SelectSearchHCPList
        /// <summary>
        /// Medical Info(Study)에서 ImpactNo 기준으로 HCP, 공동연구가 검색
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectSearchHCPList")]
        List<HealthCareProviderDto> SelectSearchHCPList(string impactNo, string hcpName, string orgName, string speName);
        #endregion

        #region SelectHcpPaymentList
        /// <summary>
        /// Contract HCP Payment List 조회
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectHcpPaymentList/{medicalIdx}/{hcpCode}")]
        List<DTO_MEDICAL_HCP_PAYMENT> SelectHcpPaymentList(string medicalIdx, string hcpCode);
        #endregion
        #region SelectHcpIMPList
        /// <summary>
        /// Contract HCP Payment List 조회
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectHcpIMPList/{medicalIdx}/{hcpCode}")]
        List<DTO_MEDICAL_IMP> SelectHcpIMPList(string medicalIdx, string hcpCode);
        #endregion

        #region ModifyHcpPament
        [OperationContract]
        [WebInvoke(Method = "POST",
       RequestFormat = WebMessageFormat.Json,
       ResponseFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "ModifyHcpPayment")]
        string ModifyHcpPayment(DTO_MEDICAL_HCP_PAYMENT dto);
        #endregion

        #region ModifyIMP
        [OperationContract]
        [WebInvoke(Method = "POST",
       RequestFormat = WebMessageFormat.Json,
       ResponseFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "ModifyIMP")]
        string ModifyIMP(DTO_MEDICAL_IMP dto);
        #endregion
    }
}
