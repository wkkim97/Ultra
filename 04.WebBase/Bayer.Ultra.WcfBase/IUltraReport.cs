using Bayer.Ultra.Framework.Common.Dto.Common;
using Bayer.Ultra.Framework.Common.Dto.Report;
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
    public interface IUltraReport
    {
        #region SelectReceiptForFreeGoodList 
        /// <summary>
        /// Receipt For Free Good List 조회
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectReceiptForFreeGoodList/{userid}")]
        List<ReceiptForFreeGoodDto> SelectReceiptForFreeGoodList(string userid);
        #endregion


        #region ExportReportAdmin 
        /// <summary>
        /// Admin report List 조회
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "ExportReportAdmin/{code}/{from}/{to}")]
        List<AdminReportDto> ExportReportAdmin(string code, string from, string to);
        #endregion

        #region ExportXlsReportAdmin 
        /// <summary>
        /// Admin report List 조회
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "ExportXlsReportAdmin/{code}/{from}/{to}")]
        string ExportXlsReportAdmin(string code, string from, string to);
        #endregion



        #region ExportXlsReceiptForFreeGoodList 
        /// <summary>
        /// Receipt For Free Good List 조회
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "ExportXlsReceiptForFreeGoodList/{userid}")]
        string ExportXlsReceiptForFreeGoodList(string userid);
        #endregion

        #region ModifyReceiptFreeGood
        [OperationContract]
        [WebInvoke(Method = "POST",
       RequestFormat = WebMessageFormat.Json,
       ResponseFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "ModifyReceiptFreeGood")]
        string ModifyReceiptFreeGood(DTO_REPORT_RECEIPT_FREE_GOOD dto);

        [OperationContract]
        [WebInvoke(Method = "POST",
       RequestFormat = WebMessageFormat.Json,
       ResponseFormat = WebMessageFormat.Json,
       BodyStyle = WebMessageBodyStyle.Bare,
       UriTemplate = "ModifyReceiptFreeGood_return")]
        string ModifyReceiptFreeGood_return(DTO_REPORT_RECEIPT_FREE_GOOD dto);
        #endregion

        #region SelectReceiptForFreeGoodItem
        /// <summary>
        /// Receipt For Free Good List 조회
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        [OperationContract]
        [WebInvoke(Method = "GET",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.WrappedRequest,
            UriTemplate = "SelectReceiptForFreeGoodItem/{processid}/{idx}")]
         ReceiptForFreeGoodDto SelectReceiptForFreeGoodItem(string processid, string idx);
        #endregion


        [OperationContract]
        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "UpdateReceiptStatus")]
        string UpdateReceiptStatus(DTO_REPORT_RECEIPT_FREE_GOOD dto);


        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "SelectMarketResearchSourceList")]
        List<DTO_MOHW_MARKET_RESEARCH> SelectMarketResearchSourceList(MohwConditionDto condi);

        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "SelectParticipantsSourceList")]
        List<DTO_MOHW_PARTICIPANTS> SelectParticipantsSourceList(MohwConditionDto condi);


        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "InsertMohwReport")]
        string InsertMohwReport(DTO_MOHW_CONDITIONS condi);


        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "CreateXlsMohwReport/{idx}/{mohwType}/{userId}")]
        void CreateXlsMohwReport(string idx, string mohwType, string userId);

        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "SelectMohwList")]
        List<MohwConditionListDto> SelectMohwList(string subject, string mohwType, string startDate, string endDate);


        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Bare,
        UriTemplate = "SelectPluralityMedicalSourceList")]
        List<DTO_MOHW_PLURALITY_MEDICAL> SelectPluralityMedicalSourceList(MohwConditionDto condi);

        [WebInvoke(Method = "POST",
            RequestFormat = WebMessageFormat.Json,
            ResponseFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SelectIndividualMedicalSourceList")]
        List<DTO_MOHW_DIV_MEDICAL_SRC> SelectIndividualMedicalSourceList(MohwConditionDto condition);

        [WebInvoke(Method = "POST",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.Wrapped,
        UriTemplate = "UpdateMohwStatus")]
        string UpdateMohwStatus(string idx, string mohwType, string status, string userId);

        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "CreateMohwCompleteReport/{idx}/{mohwType}/{userId}")]
        void CreateMohwCompleteReport(string idx, string mohwType, string userId);

        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "SelectSampleSourceList")]
        List<DTO_MOHW_FREE_GOODS> SelectSampleSourceList(MohwConditionDto condi);

        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "SelectSampleDeviceSourceList")]
        List<DTO_MOHW_FREE_GOODS_DEVICE> SelectSampleDeviceSourceList(MohwConditionDto condi);

        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "SelectMedicalStudySourceList")]
        List<DTO_MOHW_MEDICAL_STUDY> SelectMedicalStudySourceList(MohwConditionDto condi);


        [WebInvoke(Method = "POST",
         RequestFormat = WebMessageFormat.Json,
         ResponseFormat = WebMessageFormat.Json,
         BodyStyle = WebMessageBodyStyle.Bare,
         UriTemplate = "SelectKRPIASourceList")]
        List<DTO_MOHW_KRPIA> SelectKRPIASourceList(MohwConditionDto condi);


        [OperationContract(IsOneWay = true)]
        [WebInvoke(Method = "GET",
        RequestFormat = WebMessageFormat.Json,
        ResponseFormat = WebMessageFormat.Json,
        BodyStyle = WebMessageBodyStyle.WrappedRequest,
        UriTemplate = "trasfersFTP_Concur/{yyyymm}")]
        void trasfersFTP_Concur(string yyyymm);


    }
}
