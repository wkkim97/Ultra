using Bayer.Ultra.Framework.Common.Dto.Common;
using Bayer.Ultra.Framework.Common.Dto.Medical;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Medical.Mgr
{
    public class StudyMgr_Nx : Framework.Database.MgrBase
    {
        public List<DTO_MEDICAL_INFO> SelectMedicalList(string userID, string isAdmin)
        {
            try
            {
                using (Dao.StudyDao dao = new Dao.StudyDao())
                {
                    return dao.SelectMedicalList(userID, isAdmin);
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
                using (Dao.StudyDao dao = new Dao.StudyDao())
                {
                    return dao.SelectMedicalInfo(medicalIdx);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Impact No 가 존재하는 체크
        /// </summary>
        /// <param name="medicalIdx"></param>
        /// <param name="impactNo"></param>
        /// <returns></returns>
        public bool IsMedicalInfoWidthImpactNo(string medicalIdx, string impactNo)
        {
            bool retValue = false;
            try
            {
                using (Dao.StudyDao dao = new Dao.StudyDao())
                {
                     string val = dao.IsExistsMedicalInfo(medicalIdx, impactNo);
                    if( val.ToUpper().Equals("EXISTS") )
                    {
                        retValue = true;
                    }
                    else
                    {
                        retValue = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retValue;
        }
         
        public List<DTO_MEDICAL_PRODUCTS> SelectMedicalProducts(string medicalIdx)
        {
            try
            {
                using (Dao.StudyDao dao = new Dao.StudyDao())
                {
                    return dao.SelectMedicalProducts(medicalIdx);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<reviewerDto> SelectMedicalViewer(string medicalIdx)
        {
            try
            {
                using (Dao.StudyDao dao = new Dao.StudyDao())
                {
                    return dao.SelectMedicalReviewer(medicalIdx);
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
                using (Dao.StudyDao dao = new Dao.StudyDao())
                {
                    return dao.SelectContractList(medicalIdx);
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
                using (Dao.StudyDao dao = new Dao.StudyDao())
                {
                    return dao.SelectContractHCRList(medicalIdx, hcpCode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
         
        public DTO_MEDICAL_HCP_CONTRACT SelectContractHCP(string medicalIdx, string hcpCode)
        {
            try
            {
                using (Dao.StudyDao dao = new Dao.StudyDao())
                {
                    return dao.SelectContractHCP(medicalIdx, hcpCode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Medical Info에 동일한 HCP가 존재하는 체크
        /// </summary>
        /// <param name="medicalIdx"></param>
        /// <param name="impactNo"></param>
        /// <returns></returns>
        public bool IsExistsHCPContract(string medicalIdx, string hcpcode)
        {
            bool retValue = false;
            try
            {
                using (Dao.StudyDao dao = new Dao.StudyDao())
                {
                    string val = dao.IsExistsHCPContract(medicalIdx, hcpcode);
                    if (val.ToUpper().Equals("EXISTS"))
                    {
                        retValue = true;
                    }
                    else
                    {
                        retValue = false;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return retValue;
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
				using (Dao.StudyDao dao = new Dao.StudyDao())
				{
					return dao.SelectMedicalMasterList(userID, impactNo, keyword);
				}
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}

        // <!-- Ver 1.0.7 : Go-Direct -->
        /// <summary>
		/// RAD Injector 조회
		/// </summary>
		/// <param name="userID"></param>
		/// <returns></returns>
		public List<DTO_RADINJECTOR_LIST> SelectRADInjectorMasterList(string userID, string impactNo, string keyword)
        {
            try
            {
                using (Dao.StudyDao dao = new Dao.StudyDao())
                {
                    return dao.SelectRADInjectorMasterList(userID, impactNo, keyword);
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
                using (Dao.StudyDao dao = new Dao.StudyDao())
                {
                    return dao.SelectSearchHCPList(impactNo, hcpName, orgName, speName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_MEDICAL_HCP_PAYMENT> SelectHcpPaymentList(string medicalIdx, string hcpcode)
        {
            try
            {
                using (Dao.StudyDao dao = new Dao.StudyDao())
                {
                    return dao.SelectHcpPaymentList(medicalIdx, hcpcode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public List<DTO_MEDICAL_IMP> SelectHcpIMPList(string medicalIdx, string hcpcode)
        {
            try
            {
                using (Dao.StudyDao dao = new Dao.StudyDao())
                {
                    return dao.SelectHcpIMPList(medicalIdx, hcpcode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
