using Bayer.Ultra.Framework.Common.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Common.Mgr
{
    public class CommonMgr_Nx : Framework.Database.MgrBase
    {
        public List<DTO_USER_CONFIG_MENU_SORT> SelectUserConfigMenuSort(string userID)
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    return dao.SelectUserConfigMenuSort(userID);
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
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    return dao.SelectHealthCareProvider(hcpName, orgName, speName, processID);
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
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    return dao.SelectHealthCareOffice(keyword, hcoType);
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 국가 조회
        /// </summary>
        /// <returns></returns>
        public List<DTO_MASTER_COUNTRY> SelectCountry()
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    return dao.SelectCountry();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 코드 조회
        /// </summary>
        /// <param name="classCode"></param>
        /// <returns></returns>
        public List<DTO_COMMON_CODE_SUB> SelectCommonCode(string classCode)
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    return dao.SelectCommonCode(classCode);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// 상품 키워드 조회
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<MasterProductDto> SelectMasterProduct(string keyword)
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    return dao.SelectMasterProduct(keyword);
                }
            }
            catch
            {
                throw;
            }
        }

        public List<HealthCareProviderDto> SelectMasterDoctor(string hcpName, string orgName, string speName)
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    return dao.SelectMasterDoctor(hcpName, orgName, speName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<HealthCareProviderDto> SelectSearchDoctor(string keyword)
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    return dao.SelectSearchDoctor(keyword);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Sample 코드 조회
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<MasterProductDto> SelectSampleList(string keyword, string type)
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    return dao.SelectSampleList(keyword, type);
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// 학회 리스트 조회
        /// </summary>
        /// <param name="id"></param>
        /// <param name="status">Y, N, A(ALL-검색시에만 사용)</param>
        /// <returns></returns>
        public List<DTO_COMMON_MEDICAL_SOCIETY> SelectMedicalSocietyList(int id, string keyword, string status)
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    return dao.SelectMedicalSocietyList(id, keyword, status);
                }
            }
            catch
            {
                throw;
            }
        }


        public List<DTO_COMMON_ABSENCE> SelectDelegationList(string userid, int idx)
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    return dao.SelectDelegationList(userid, idx);
                }
            }
            catch
            {
                throw;
            }
        }

        public List<UserInfoDto> SelectDelegationToList(string userid)
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    return dao.SelectDelegationToList(userid);
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_COMMON_CODE_SUB> SelectCommonCodeAll()
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    return dao.SelectCommonCodeAll();
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
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    return dao.SelectCRMProduct(keyword);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_SENDMAIL> SelectSendMailQueueList()
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    return dao.SelectSendMailQueueList();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    }
}
