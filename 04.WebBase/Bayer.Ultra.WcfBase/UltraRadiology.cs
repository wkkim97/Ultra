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
    public partial class UltraRadiology : IUltraRadiology
    {

        #region Micro Marketing
        public List<AssignedHospitalListDto> SelectHospitalList(string user_id, string user_type)
        {
            try
            {
                if (string.IsNullOrEmpty(user_type)) user_type = "END_USER";
                
                using (BSL.Report.Mgr.MicroMarketingMgr mgr = new BSL.Report.Mgr.MicroMarketingMgr())
                {   
                    return mgr.SelectAssignedHospitalList(user_id, user_type);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Equipment
        public List<HospitalEquipmentDto> SelectEquipment(string id, string organization_id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) id = "";

                using (BSL.Report.Mgr.MicroMarketingMgr mgr = new BSL.Report.Mgr.MicroMarketingMgr())
                {
                    return mgr.SelectEquipment(id, organization_id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string MergeEquipment(HospitalEquipmentDto dto)
        {
            try
            {
                using (BSL.Report.Mgr.MicroMarketingMgr mgr = new BSL.Report.Mgr.MicroMarketingMgr())
                {
                    return mgr.MergeEquipment(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteEquipment(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new Exception("No id");
                using (BSL.Report.Mgr.MicroMarketingMgr mgr = new BSL.Report.Mgr.MicroMarketingMgr())
                {
                    return mgr.DeleteEquipment(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Examination
        public List<HospitalExaminationDto> SelectExamination(string id, string organization_id, string quarter)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) id = "";

                using (BSL.Report.Mgr.MicroMarketingMgr mgr = new BSL.Report.Mgr.MicroMarketingMgr())
                {
                    return mgr.SelectExamination(id, organization_id, quarter);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string MergeExamination(MergeHospitalExaminationDto dto)
        {
            try
            {
                using (BSL.Report.Mgr.MicroMarketingMgr mgr = new BSL.Report.Mgr.MicroMarketingMgr())
                {
                    return mgr.MergeExamination(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteExamination(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new Exception("No id");
                using (BSL.Report.Mgr.MicroMarketingMgr mgr = new BSL.Report.Mgr.MicroMarketingMgr())
                {
                    return mgr.DeleteExamination(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //Market Share
        public List<HospitalMarketShareDto> SelectMarketShare(string id, string organization_id, string quarter)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) id = "";

                using (BSL.Report.Mgr.MicroMarketingMgr mgr = new BSL.Report.Mgr.MicroMarketingMgr())
                {
                    return mgr.SelectMarketShare(id, organization_id, quarter);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string MergeMarketShare(MergeHospitalMarketShareDto dto)
        {
            try
            {
                using (BSL.Report.Mgr.MicroMarketingMgr mgr = new BSL.Report.Mgr.MicroMarketingMgr())
                {
                    return mgr.MergeMarketShare(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteMarketShare(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) throw new Exception("No id");
                using (BSL.Report.Mgr.MicroMarketingMgr mgr = new BSL.Report.Mgr.MicroMarketingMgr())
                {
                    return mgr.DeleteMarketShare(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region Master Market Share
        public List<MasterMarketShare> SelectMasterMarketShare(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id)) id = "";

                using (BSL.Report.Mgr.MicroMarketingMgr mgr = new BSL.Report.Mgr.MicroMarketingMgr())
                {
                    return mgr.SelectMasterMarketShare(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string MergeMasterMarketShare(MasterMarketShare mastermarketshare)
        {
            try
            {
                using (BSL.Report.Mgr.MicroMarketingMgr mgr = new BSL.Report.Mgr.MicroMarketingMgr())
                {
                    return mgr.MergeMasterMarketShare(mastermarketshare);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string DeleteMasterMarketShare(string id)
        {
            try
            {
                using (BSL.Report.Mgr.MicroMarketingMgr mgr = new BSL.Report.Mgr.MicroMarketingMgr())
                {
                    return mgr.DeleteMasterMarketShare(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<MasterMarketShare> SelectSearchMasterMarketShare(string family, string product)
        {
            try
            {
                if (string.IsNullOrEmpty(family)) family = String.Empty;
                if (string.IsNullOrEmpty(product)) product = String.Empty;

                using (BSL.Report.Mgr.MicroMarketingMgr mgr = new BSL.Report.Mgr.MicroMarketingMgr())
                {
                    return mgr.SelectSearchMasterMarketShare(family, product);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region Non Onekey
        public List<AssignedNonOneKeyListDto> SelectNonOnekeyList(string user_id, string user_type)
        {
            try
            {
                if (string.IsNullOrEmpty(user_id)) user_id = "";
                if (string.IsNullOrEmpty(user_type)) user_type = "END_USER";

                using (BSL.Report.Mgr.NonOneKeyMgr mgr = new BSL.Report.Mgr.NonOneKeyMgr())
                {
                    return mgr.SelectAssignedNonOneKeyList(user_id, user_type);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<CustomerListDto> SelectCustomerList(string customer_type, string customer_name)
        {
            try
            {
                using (BSL.Report.Mgr.NonOneKeyMgr mgr = new BSL.Report.Mgr.NonOneKeyMgr())
                {
                    return mgr.SelectCustomerList(customer_type, customer_name);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<HospitalListDto> SelectNonOneKeyHospitalList(string keyword)
        {
            try
            {
                using (BSL.Report.Mgr.NonOneKeyMgr mgr = new BSL.Report.Mgr.NonOneKeyMgr())
                {
                    return mgr.SelectNonOneKeyHospitalList(keyword);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string InsertLog(InsertLogDto dto)
        {
            try
            {
                using (BSL.Report.Mgr.NonOneKeyMgr mgr = new BSL.Report.Mgr.NonOneKeyMgr())
                {
                    if (string.IsNullOrEmpty(dto.COMMENT)) dto.COMMENT = string.Empty;
                    if (string.IsNullOrEmpty(dto.LOG_TYPE)) dto.LOG_TYPE = string.Empty;
                    return mgr.InsertLog(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SelectLogDto> SelectLog(string NON_ONEKEY_ID)
        {
            try
            {
                using (BSL.Report.Mgr.NonOneKeyMgr mgr = new BSL.Report.Mgr.NonOneKeyMgr())
                {
                    return mgr.SelectLog(NON_ONEKEY_ID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<NonOnekeyAttachDto> SelectNonOnekeyAttachFile(int NON_ONEKEY_ID, string IDXS)
        {
            try
            {
                using (BSL.Report.Mgr.NonOneKeyMgr mgr = new BSL.Report.Mgr.NonOneKeyMgr())
                {
                    if (string.IsNullOrEmpty(IDXS)) IDXS = string.Empty;
                    return mgr.SelectNonOnekeyAttachFile(NON_ONEKEY_ID, IDXS);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string SendMail (List<NonOnekeyIdDto> NON_ONEKEY_ID, string AttachType, string Status, string FromAddress, string ToAddress, string CC)
        {
            try
            {   
                using (BSL.Report.Mgr.NonOneKeyMgr mgr = new BSL.Report.Mgr.NonOneKeyMgr())
                {
                    return mgr.MailSend(NON_ONEKEY_ID, AttachType, Status, FromAddress, ToAddress, CC);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int MergeCustomerData (MergeCustomerDto customerDto)
        {
            try
            {
                using (BSL.Report.Mgr.NonOneKeyMgr mgr = new BSL.Report.Mgr.NonOneKeyMgr())
                {
                    return mgr.MergeCustomerData(customerDto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion

        #region HCP Inquiry
        public List<HCPInquiryCustomerDto> SelectInquiryCustomerList(string name, string org, string specialty)
        {
            try
            {
                using (BSL.Report.Mgr.HCPInquiryMgr mgr = new BSL.Report.Mgr.HCPInquiryMgr())
                {
                    if (string.IsNullOrEmpty(org)) org = string.Empty;
                    if (string.IsNullOrEmpty(specialty)) specialty = string.Empty;
                    return mgr.SelectInquiryCustomerList(name, org, specialty);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string MergeCustomerRequest(HCPInquiryMergeDto customerDto)
        {
            try
            {
                using (BSL.Report.Mgr.HCPInquiryMgr mgr = new BSL.Report.Mgr.HCPInquiryMgr())
                {
                    if (!customerDto.HCP_INQUIRY_REQUEST_ID.HasValue) customerDto.HCP_INQUIRY_REQUEST_ID = 0;
                    if (string.IsNullOrEmpty(customerDto.REMARK)) customerDto.REMARK = string.Empty;
                    if (string.IsNullOrEmpty(customerDto.DELIVERED_TO_HCP)) customerDto.DELIVERED_TO_HCP = string.Empty;
                    return mgr.MergeCustomerRequest(customerDto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<HCPInquiryListDto> SelectHCPInquiryList(string USER_ID, string USER_TYPE)
        {
            try
            {
                using (BSL.Report.Mgr.HCPInquiryMgr mgr = new BSL.Report.Mgr.HCPInquiryMgr())
                {
                    if (string.IsNullOrEmpty(USER_ID)) USER_ID = "";
                    if (string.IsNullOrEmpty(USER_TYPE)) USER_TYPE = "END_USER";
                    return mgr.SelectHCPInquiryList(USER_ID, USER_TYPE);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<SelectHCPInquiryLogDto> SelectHCPInquiryLog(string HCP_INQUIRY_REQUEST_ID)
        {
            try
            {
                using (BSL.Report.Mgr.HCPInquiryMgr mgr = new BSL.Report.Mgr.HCPInquiryMgr())
                {
                    return mgr.SelectHCPInquiryLog(HCP_INQUIRY_REQUEST_ID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string InsertHCPInquiryLog(InsertHCPInquiryLogDto dto)
        {
            try
            {
                using (BSL.Report.Mgr.HCPInquiryMgr mgr = new BSL.Report.Mgr.HCPInquiryMgr())
                {
                    if (string.IsNullOrEmpty(dto.COMMENT)) dto.COMMENT = string.Empty;
                    if (string.IsNullOrEmpty(dto.LOG_TYPE)) dto.LOG_TYPE = string.Empty;
                    return mgr.InsertHCPInquiryLog(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string SendHCPInquiryMail(List<HCPInquiryIdDto> IDs, string sendMailType, string Status, string FromAddress, string CC)
        {
            try
            {
                using (BSL.Report.Mgr.HCPInquiryMgr mgr = new BSL.Report.Mgr.HCPInquiryMgr())
                {
                    return mgr.SendHCPInquiryMail(IDs, sendMailType, Status, FromAddress, CC);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
