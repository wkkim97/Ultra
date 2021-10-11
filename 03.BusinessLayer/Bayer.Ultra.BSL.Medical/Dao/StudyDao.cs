using Bayer.Ultra.Framework.Common.Dto.Common;
using Bayer.Ultra.Framework.Common.Dto.Medical;
using Bayer.Ultra.Framework.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Medical.Dao
{
    internal class StudyDao : Framework.Database.DaoBase
    {
        /// <summary>
        /// Medical Study List 조회
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public List<DTO_MEDICAL_INFO> SelectMedicalList(string UserName, string isAdmin)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@USER_ID", UserName);
                    parameters[1] = new SqlParameter("@IS_ADMIN", isAdmin);
                    var result = _context.Database.SqlQuery<DTO_MEDICAL_INFO>(StudyContext.USP_SELECT_MEDICAL_INFO_LIST, parameters);

                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Medical Study Detail 조회
        /// </summary>
        /// <param name="medicalIdx"></param>
        /// <returns></returns>
        public MedicalInfoDto SelectMedicalInfo(string medicalIdx)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@MEDICAL_IDX", medicalIdx);
                    var result = _context.Database.SqlQuery<MedicalInfoDto>(StudyContext.USP_SELECT_MEDICAL_INFO_DETAIL, parameters);

                    return result.FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Medical Study Detail Insert / Update
        /// </summary>
        /// <param name="dto"></param>
        public int ModifyMedicalInfo( DTO_MEDICAL_INFO dto)
        {
            int? retValue = -1;
            try
            {
                using (_context = new StudyContext())
                {
                    var result = _context.Database.SqlQuery<DTO_MEDICAL_INFO>(StudyContext.USP_MERGE_MEDICAL_INFO, ParameterMapper.Mapping(dto));

                    retValue = result.FirstOrDefault().MEDICAL_IDX;
                }

                return Convert.ToInt32(retValue);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Impact No가 다른 Medical이도 존재한지 체크
        /// </summary>
        /// <param name="medicalIdx"></param>
        /// <param name="impactNo"></param>
        public string IsExistsMedicalInfo(string medicalIdx, string impactNo)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@MEDICAL_IDX", medicalIdx);
                    parameters[1] = new SqlParameter("@IMPACT_NO", impactNo);
                    var result = _context.Database.SqlQuery<string>(StudyContext.USP_SELECT_EXISTS_MEDICAL_INFO, parameters);

                    return  result.FirstOrDefault().ToString();
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Medical Study Products 조회
        /// </summary>
        /// <param name="medicalIdx"></param>
        /// <returns></returns>
        public List<DTO_MEDICAL_PRODUCTS> SelectMedicalProducts(string medicalIdx)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@MEDICAL_IDX", medicalIdx);
                    var result = _context.Database.SqlQuery<DTO_MEDICAL_PRODUCTS>(StudyContext.USP_SELECT_MEDICAL_PRODUCTS, parameters);

                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Medical Study Editor 조회
        /// </summary>
        /// <param name="medicalIdx"></param>
        /// <returns></returns>
        public List<reviewerDto> SelectMedicalReviewer(string medicalIdx)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@MEDICAL_IDX", medicalIdx);
                    var result = _context.Database.SqlQuery<reviewerDto>(StudyContext.USP_SELECT_MEDICAL_REVIEWER, parameters);

                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Medical Study Products Insert / Update
        /// </summary>
        /// <param name="dto"></param>
        public void ModifyMedicalProducts(DTO_MEDICAL_PRODUCTS dto)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    _context.Database.ExecuteSqlCommand(StudyContext.USP_MERGE_MEDICAL_PRODUCTS, ParameterMapper.Mapping(dto));
                }
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Medical Study Editor Insert / Update
        /// </summary>
        /// <param name="dto"></param>
        public void ModifyMedicalReViewer(DTO_MEDICAL_EDITOR dto)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    _context.Database.ExecuteSqlCommand(StudyContext.USP_MERGE_MEDICAL_REVIEWER, ParameterMapper.Mapping(dto));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Medical Study Reviewer Insert / Update
        /// </summary>
        /// <param name="medicalIdx"></param>
        public void DeleteMedicalReViewer(string medicalIdx)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@MEDICAL_IDX", medicalIdx);
                    _context.Database.ExecuteSqlCommand(StudyContext.USP_DELETE_MEDICAL_REVIEWER, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Medical Study Products Insert / Update
        /// </summary>
        /// <param name="medicalIdx"></param>
        public void DeleteMedicalProducts(string medicalIdx)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@MEDICAL_IDX", medicalIdx);
                    _context.Database.ExecuteSqlCommand(StudyContext.USP_DELETE_MEDICAL_PRODUCTS, parameters);
                }
            }
            catch
            {
                throw;
            }
        }



        /// <summary>
        /// Medical Study Info Delete
        /// </summary>
        /// <param name="medicalIdx"></param>
        public void DeleteMedicalInfo(string medicalIdx)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@MEDICAL_IDX", medicalIdx);
                    _context.Database.ExecuteSqlCommand(StudyContext.USP_DELETE_MEDICAL_INFO, parameters);
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Medical Contract List 조회
        /// </summary>
        /// <param name="medicalIdx"></param>
        /// <returns></returns>
        public List<ContractDto> SelectContractList(string medicalIdx)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@MEDICAL_IDX", medicalIdx);
                    var result = _context.Database.SqlQuery<ContractDto>(StudyContext.USP_SELECT_MEDICAL_CONTRACT_LIST, parameters);

                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Medical Contract 공동연구가 List 조회
        /// </summary>
        /// <param name="medicalIdx"></param>
        /// <returns></returns>
        public List<ContractHCRDto> SelectContractHCRList(string medicalIdx, string hcpCode)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@MEDICAL_IDX", medicalIdx);
                    parameters[1] = new SqlParameter("@HCP_CODE", hcpCode);
                    var result = _context.Database.SqlQuery<ContractHCRDto>(StudyContext.USP_SELECT_MEDICAL_CONTRACT_HCR_LIST, parameters);

                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Medical Contract 상세 조회
        /// </summary>
        /// <param name="medicalIdx"></param>
        /// <returns></returns>
        public DTO_MEDICAL_HCP_CONTRACT SelectContractHCP(string medicalIdx, string hcpCode)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@MEDICAL_IDX", medicalIdx);
                    parameters[1] = new SqlParameter("@HCP_CODE", hcpCode);
                    var result = _context.Database.SqlQuery<DTO_MEDICAL_HCP_CONTRACT>(StudyContext.USP_SELECT_MEDICAL_CONTRACT_HCP, parameters);

                    return result.FirstOrDefault();
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Medical Contract 생성/수정
        /// </summary>
        /// <param name="medicalIdx"></param>
        /// <returns></returns>
        public void ModifyHpcContract(DTO_MEDICAL_HCP_CONTRACT dto)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    _context.Database.ExecuteSqlCommand(StudyContext.USP_MERGE_MEDICAL_HCP_CONTRACT, ParameterMapper.Mapping(dto));
                }
            }
            catch
            {
                throw;
            }
        }



        /// <summary>
        /// Medical Contract 공동연구원 생성/수정
        /// </summary>
        /// <param name="medicalIdx"></param>
        /// <returns></returns>
        public void ModifyHcrContract(DTO_MEDICAL_HCR_CONTRACT dto)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    _context.Database.ExecuteSqlCommand(StudyContext.USP_MERGE_MEDICAL_HCR_CONTRACT, ParameterMapper.Mapping(dto));
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteContractHCR(string medicalIdx, string hcpcode, string hcrcode, string updater)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[5];
                    parameters[0] = new SqlParameter("@MEDICAL_IDX", medicalIdx);
                    parameters[1] = new SqlParameter("@HCP_CODE", hcpcode);
                    parameters[2] = new SqlParameter("@HCR_CODE", hcrcode);
                    parameters[3] = new SqlParameter("@UPDATER_ID", updater);
                    parameters[4] = new SqlParameter("@IS_DELETED", "Y");
                    _context.Database.ExecuteSqlCommand(StudyContext.USP_DELETE_MEDICAL_HCR_CONTRACT, parameters);
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Contract HCP Delete
        /// </summary>
        /// <param name="medicalIdx"></param>
        /// <param name="hcpcode"></param>
        public void DeleteContractHCP(string medicalIdx, string hcpcode)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@MEDICAL_IDX", medicalIdx);
                    parameters[1] = new SqlParameter("@HCP_CODE", hcpcode);
                    _context.Database.ExecuteSqlCommand(StudyContext.USP_DELETE_MEDICAL_HCP_CONTRACT, parameters);
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Contract HCP Delete
        /// </summary>
        /// <param name="medicalIdx"></param>
        /// <param name="hcpcode"></param>
        public string IsExistsHCPContract(string medicalIdx, string hcpcode)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@MEDICAL_IDX", medicalIdx);
                    parameters[1] = new SqlParameter("@HCP_CODE", hcpcode);
 
                    var result = _context.Database.SqlQuery<string>(StudyContext.USP_SELECT_EXISTS_HCP_CONTRACT, parameters);
                    return result.FirstOrDefault().ToString();
                }
            }
            catch
            {
                throw;
            }
        } 

		/// <summary>
		/// Medical List (Study + PMS) 조회
		/// </summary>
		/// <param name="UserName"></param>
		/// <returns></returns>
		public List<DTO_MEDICAL_INFO> SelectMedicalMasterList(string UserName, string ImpactNo, string keyword)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@USER_ID", UserName);
                    parameters[1] = new SqlParameter("@IMPACT_NO", ImpactNo);
                    parameters[2] = new SqlParameter("@KEYWORD", keyword);

                    var result = _context.Database.SqlQuery<DTO_MEDICAL_INFO>(StudyContext.USP_SELECT_MEDICAL_MASTER_LIST, parameters);

                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

         //<!-- Ver 1.0.7 : Go-Direct -->
        /// <summary>
        /// RAD Injector 조회
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public List<DTO_RADINJECTOR_LIST> SelectRADInjectorMasterList(string UserName, string ImpactNo, string keyword)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@USER_ID", UserName);
                    parameters[1] = new SqlParameter("@IMPACT_NO", ImpactNo);
                    parameters[2] = new SqlParameter("@KEYWORD", keyword);

                    var result = _context.Database.SqlQuery<DTO_RADINJECTOR_LIST>(StudyContext.USP_SELECT_RADINJECTOR_MASTER_LIST, parameters);

                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }
         //<!-- Ver 1.0.7 : Go-Direct -->


        public List<HealthCareProviderDto> SelectSearchHCPList(string impactNo, string hcpName, string orgName, string speName)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@IMPACT_NO", impactNo);
                    parameters[1] = new SqlParameter("@HCPName", hcpName);
                    parameters[2] = new SqlParameter("@OrgName", orgName);
                    parameters[3] = new SqlParameter("@SpeName", speName);
                    var result = _context.Database.SqlQuery<HealthCareProviderDto>(StudyContext.USP_SELECT_MEDICAL_INFO_HCP_LIST, parameters);

                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public void InsertMedicalStudyLog(string medicalIdx)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@MEDICAL_IDX", medicalIdx);
                    _context.Database.ExecuteSqlCommand(StudyContext.USP_INSERT_MEDICAL_LOG, parameters );
                }
            }
            catch { }
        }

        public void InsertMedicalHcpContractLog(string medicalIdx, string hcpCode)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@MEDICAL_IDX", medicalIdx);
                    parameters[1] = new SqlParameter("@HCP_CODE", hcpCode);
                    _context.Database.ExecuteSqlCommand(StudyContext.USP_INSERT_MEDICAL_HCP_CONTRACT_LOG, parameters);
                }
            }
            catch { }
        }




        /// <summary>
        /// Medical Contract Payment List 조회
        /// </summary>
        /// <param name="medicalIdx"></param>
        /// <param name="medicalIdx"></param>
        /// <returns></returns>
        public List<DTO_MEDICAL_HCP_PAYMENT> SelectHcpPaymentList(string medicalIdx, string hcpCode)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@MEDICAL_IDX", medicalIdx);
                    parameters[1] = new SqlParameter("@HCP_CODE", hcpCode);
                    var result = _context.Database.SqlQuery<DTO_MEDICAL_HCP_PAYMENT>(StudyContext.USP_SELECT_MEDICAL_HCP_PAYMENT_LIST, parameters);

                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_MEDICAL_IMP> SelectHcpIMPList(string medicalIdx, string hcpCode)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@MEDICAL_IDX", medicalIdx);
                    parameters[1] = new SqlParameter("@HCP_CODE", hcpCode);
                    var result = _context.Database.SqlQuery<DTO_MEDICAL_IMP>(StudyContext.USP_SELECT_MEDICAL_IMP_LIST, parameters);

                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Medical Contract Payment 생성/수정
        /// </summary>
        /// <param name="medicalIdx"></param>
        /// 
        /// <returns></returns>
        public void ModifyHpcPayment(DTO_MEDICAL_HCP_PAYMENT dto)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    _context.Database.ExecuteSqlCommand(StudyContext.USP_MERGE_MEDICAL_HCP_PAYMENT, ParameterMapper.Mapping(dto));
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// Medical Contract IMP 생성/수정
        /// </summary>
        /// <param name="medicalIdx"></param>
        /// 
        /// <returns></returns>
        public void ModifyIMP(DTO_MEDICAL_IMP dto)
        {
            try
            {
                using (_context = new StudyContext())
                {
                    _context.Database.ExecuteSqlCommand(StudyContext.USP_MERGE_MEDICAL_IMP, ParameterMapper.Mapping(dto));
                }
            }
            catch
            {
                throw;
            }
        }

    }
}
