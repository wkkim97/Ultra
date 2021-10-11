using Bayer.Ultra.Framework.Common.Dto.Common;
using Bayer.Ultra.Framework.Common.Dto.Medical;
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


    public partial class UltraMedical : IUltraMedical
    { 
        public List<DTO_MEDICAL_INFO> SelectMedicalStudyList(string userID, string isAdmin)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Nx mgr = new BSL.Medical.Mgr.StudyMgr_Nx())
                {
                    if (string.IsNullOrEmpty(userID)) userID = string.Empty;
                    return mgr.SelectMedicalList(userID, isAdmin);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public MedicalInfoDto SelectMedicalInfo(string medicalIdx)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Nx mgr = new BSL.Medical.Mgr.StudyMgr_Nx())
                { 
                    return mgr.SelectMedicalInfo(medicalIdx);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string ModifyMedicalInfo(DTO_MEDICAL_INFO dto)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Tx mgr = new BSL.Medical.Mgr.StudyMgr_Tx())
                {
                    return mgr.ModifyMedicalInfo(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string IsMedicalInfoImpactNo(string medicalIdx, string impactNo)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Nx mgr = new BSL.Medical.Mgr.StudyMgr_Nx())
                {
                    return mgr.IsMedicalInfoWidthImpactNo(medicalIdx, impactNo).ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         
        public List<DTO_MEDICAL_PRODUCTS> SelectMedicalProducts(string medicalIdx)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Nx mgr = new BSL.Medical.Mgr.StudyMgr_Nx())
                { 
                    return mgr.SelectMedicalProducts(medicalIdx);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<reviewerDto> SelectMedicalReviewer(string medicalIdx)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Nx mgr = new BSL.Medical.Mgr.StudyMgr_Nx())
                {
                    return mgr.SelectMedicalViewer(medicalIdx);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string ModifyMedical(MedicalDto dto)    
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Tx mgr = new BSL.Medical.Mgr.StudyMgr_Tx())
                {
                    return mgr.ModifyMedical(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string DeleteMedicalInfo(string medicalIdx)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Tx mgr = new BSL.Medical.Mgr.StudyMgr_Tx())
                {
                    return mgr.DeleteMedicalInfo(medicalIdx);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ContractDto> SelectContractList(string medicalIdx)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Nx mgr = new BSL.Medical.Mgr.StudyMgr_Nx())
                {
                    return mgr.SelectContractList(medicalIdx);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<ContractHCRDto> SelectContractHCRList(string medicalIdx, string hcpCode)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Nx mgr = new BSL.Medical.Mgr.StudyMgr_Nx())
                {
                    return mgr.SelectContractHCRList(medicalIdx, hcpCode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public DTO_MEDICAL_HCP_CONTRACT SelectContractDetail(string medicalIdx, string hcpCode)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Nx mgr = new BSL.Medical.Mgr.StudyMgr_Nx())
                {
                    return mgr.SelectContractHCP(medicalIdx, hcpCode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ModifyHcpContract(DTO_MEDICAL_HCP_CONTRACT dto)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Tx mgr = new BSL.Medical.Mgr.StudyMgr_Tx())
                {
                    return mgr.ModifyHcpContract(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ModifyHcrContract(List<DTO_MEDICAL_HCR_CONTRACT> dto)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Tx mgr = new BSL.Medical.Mgr.StudyMgr_Tx())
                {
                    return mgr.ModifyHcrContract(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public string DeleteContractHCP(string medicalIdx, string hcpcode)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Tx mgr = new BSL.Medical.Mgr.StudyMgr_Tx())
                {
                    return mgr.DeleteContractHCP(medicalIdx, hcpcode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string IsExistsHCPContract(string medicalIdx, string hcpcode)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Nx mgr = new BSL.Medical.Mgr.StudyMgr_Nx())
                {
                    return mgr.IsExistsHCPContract(medicalIdx, hcpcode).ToString();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<pmslistDto> SelectMedicalPmsList(string userID, string isAdmin)
        {
            try
            {
                using (BSL.Medical.Mgr.PmsMgr_Nx mgr = new BSL.Medical.Mgr.PmsMgr_Nx())
                {
                    if (string.IsNullOrEmpty(userID)) userID = string.Empty;
                    return mgr.SelectMedicalPmsList(userID, isAdmin);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public string SelectXlsMedicalPmsList(string userID, string isAdmin)
        {
            try
            {
                using (BSL.Medical.Mgr.PmsMgr_Nx mgr = new BSL.Medical.Mgr.PmsMgr_Nx())
                {
                    if (string.IsNullOrEmpty(userID)) userID = string.Empty;
                    return mgr.SelectXlsMedicalPmsList(userID, isAdmin);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public string ModifyPms(DTO_MEDICAL_PMS dto)
        {
            try
            {
                using (BSL.Medical.Mgr.PmsMgr_Tx mgr = new BSL.Medical.Mgr.PmsMgr_Tx())
                {
                    return mgr.ModifyPms(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public pmslistDto SelectMedicalPms(string idx)
        {
            try
            {
                using (BSL.Medical.Mgr.PmsMgr_Nx mgr = new BSL.Medical.Mgr.PmsMgr_Nx())
                {
                    if (string.IsNullOrEmpty(idx)) idx = string.Empty;
                    return mgr.SelectMedicalPms(idx);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		/// <summary>
		/// Medical List (Study + PMS) List
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public List<DTO_MEDICAL_INFO> SelectMedicalMasterList(string userID, string impactNo, string keyword)
		{
			try
			{
				using (BSL.Medical.Mgr.StudyMgr_Nx mgr = new BSL.Medical.Mgr.StudyMgr_Nx())
				{
					if (string.IsNullOrEmpty(impactNo)) impactNo = "ALL";
                    if (string.IsNullOrEmpty(keyword)) keyword = "";
                    return mgr.SelectMedicalMasterList(userID, impactNo, keyword);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

        // <!-- Ver 1.0.7 : Go-Direct -->
        public List<DTO_RADINJECTOR_LIST> SelectRADInjectorMasterList(string userID, string impactNo, string keyword)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Nx mgr = new BSL.Medical.Mgr.StudyMgr_Nx())
                {
                    if (string.IsNullOrEmpty(impactNo)) impactNo = "ALL";
                    if (string.IsNullOrEmpty(keyword)) keyword = "";
                    return mgr.SelectRADInjectorMasterList(userID, impactNo, keyword);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        // <!-- Ver 1.0.7 : Go-Direct -->

        public List<HealthCareProviderDto> SelectSearchHCPList(string impactNo, string hcpName, string orgName, string speName)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Nx mgr = new BSL.Medical.Mgr.StudyMgr_Nx())
                {
                    if (string.IsNullOrEmpty(impactNo)) impactNo = string.Empty;
                    if (string.IsNullOrEmpty(hcpName)) hcpName = string.Empty;
                    if (string.IsNullOrEmpty(orgName)) orgName = string.Empty;
                    if (string.IsNullOrEmpty(speName)) speName = string.Empty;
                    return mgr.SelectSearchHCPList(impactNo, hcpName, orgName, speName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        public List<DTO_MEDICAL_HCP_PAYMENT> SelectHcpPaymentList(string medicalIdx, string hcpCode)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Nx mgr = new BSL.Medical.Mgr.StudyMgr_Nx())
                {
                    return mgr.SelectHcpPaymentList(medicalIdx, hcpCode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DTO_MEDICAL_IMP> SelectHcpIMPList(string medicalIdx, string hcpCode)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Nx mgr = new BSL.Medical.Mgr.StudyMgr_Nx())
                {
                    return mgr.SelectHcpIMPList(medicalIdx, hcpCode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ModifyHcpPayment(DTO_MEDICAL_HCP_PAYMENT dto)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Tx mgr = new BSL.Medical.Mgr.StudyMgr_Tx())
                {
                    return mgr.ModifyHcpPayment(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public string ModifyIMP(DTO_MEDICAL_IMP dto)
        {
            try
            {
                using (BSL.Medical.Mgr.StudyMgr_Tx mgr = new BSL.Medical.Mgr.StudyMgr_Tx())
                {
                    return mgr.ModifyIMP(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
