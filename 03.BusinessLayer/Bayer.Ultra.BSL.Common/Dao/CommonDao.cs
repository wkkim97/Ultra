using Bayer.Ultra.Framework.Common.Dto.Common;
using Bayer.Ultra.Framework.Database;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Common.Dao
{
    internal class CommonDao : Framework.Database.DaoBase
    {
        /// <summary>
        /// 로그인 History 등록
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="clientIP"></param>
        /// <param name="loginUserName"></param>
        /// <param name="machineName"></param>
        public void InsertLoginHistory(string userID, string clientIP, string loginUserName, string machineName)
        {
            try
            {
                using (_context = new CommonContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@USER_ID", userID);
                    parameters[1] = new SqlParameter("@CLIENTIP", clientIP);
                    parameters[2] = new SqlParameter("@WINDOWUSERNAME", loginUserName);
                    parameters[3] = new SqlParameter("@WINDOWDOMAINNAME", machineName);

                    _context.Database.ExecuteSqlCommand(CommonContext.USP_INSERT_LOGIN_HISTORY, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 사용자별 이벤트 목록 조회
        /// </summary>
        /// <param name="userID"></param>
        /// <returns></returns>
        public List<DTO_USER_CONFIG_MENU_SORT> SelectUserConfigMenuSort(string userID)
        {
            try
            {
                using (_context = new CommonContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@USER_ID", userID);

                    var result = _context.Database.SqlQuery<DTO_USER_CONFIG_MENU_SORT>(CommonContext.USP_SELECT_USER_EVENT_LIST, parameters);
                    return result.ToList();
                }
            }

            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 시스템 로그 저장
        /// </summary>
        /// <param name="type"></param>
        /// <param name="eventName"></param>
        /// <param name="message"></param>
        /// <param name="creater"></param>
        public void InsertSystemLog(string type, string eventName, string message, string creater)
        {
            try
            {
                using(_context = new CommonContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@TYPE", type);
                    parameters[1] = new SqlParameter("@EVENT_NAME", eventName);
                    parameters[2] = new SqlParameter("@MESSAGE", message);
                    parameters[3] = new SqlParameter("@CREATER_ID", creater);

                    _context.Database.ExecuteSqlCommand(CommonContext.USP_INSERT_LOG_SYSTEM, parameters);
                }
            }
            catch
            {
                throw;
            }
        }

        /// <summary>
        /// 의사/약사/간호사 조회
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public List<HealthCareProviderDto> SelectHealthCareProvider(string hcpName, string orgName, string speName, string processID)
        {
            try
            {
                using (_context = new CommonContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@HCPName", hcpName);
                    parameters[1] = new SqlParameter("@OrgName", orgName);
                    parameters[2] = new SqlParameter("@SpeName", speName);
                    parameters[3] = new SqlParameter("@PROCESS_ID", processID);

                    var result = _context.Database.SqlQuery<HealthCareProviderDto>(CommonContext.USP_SELECT_HEALTH_CARE_PROVIDER, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<HealthCareOfficeDto> SelectHealthCareOffice(string keyword, string hcoType)
        {
            try
            {
                using (_context = new CommonContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@KEYWORD", keyword);
                    parameters[1] = new SqlParameter("@TYPE", hcoType);

                    var result = _context.Database.SqlQuery<HealthCareOfficeDto>(CommonContext.USP_SELECT_HEALTH_CARE_OFFICE, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
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
                using (_context = new CommonContext())
                {
                    var result = _context.Database.SqlQuery<DTO_MASTER_COUNTRY>(CommonContext.USP_SELECT_COUNTRY_BY_ULTRA);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
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
                using (_context = new CommonContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@CLASS_CODE", classCode);

                    var result = _context.Database.SqlQuery<DTO_COMMON_CODE_SUB>(CommonContext.USP_SELECT_CODE_SUB, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }


        public List<MasterProductDto> SelectMasterProduct(string keyword)
        {
            try
            {
                using (_context = new CommonContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@KEYWORD", keyword);

                    var result = _context.Database.SqlQuery<MasterProductDto>(CommonContext.USP_SELECT_EMANAGE_MASTER_PRODUCT, parameters);
                    return result.ToList();
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
                using (_context = new CommonContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@HCPName", hcpName);
                    parameters[1] = new SqlParameter("@OrgName", orgName);
                    parameters[2] = new SqlParameter("@SpeName", speName);

                    var result = _context.Database.SqlQuery<HealthCareProviderDto>(CommonContext.USP_SELECT_SEARCH_MASTER_DOCTOR, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<HealthCareProviderDto> SelectSearchDoctor(string keyword)
        {
            try
            {
                using (_context = new CommonContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@KEYWORD", keyword);

                    var result = _context.Database.SqlQuery<HealthCareProviderDto>(CommonContext.USP_SELECT_SEARCH_DOCTOR, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }


        public List<MasterProductDto> SelectSampleList(string keyword, string searchType)
        {
            try
            {
                using (_context = new CommonContext())
                {
                    SqlParameter[] parameters = new SqlParameter[2];
                    parameters[0] = new SqlParameter("@KEYWORD", keyword);
                    parameters[1] = new SqlParameter("@TYPE", searchType);
                    var result = _context.Database.SqlQuery<MasterProductDto>(CommonContext.USP_SELECT_SAMPLE_LIST, parameters);
                    return result.ToList();
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
                using (_context = new CommonContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@SOCIETY_IDX", id);
                    parameters[1] = new SqlParameter("@KEYWORD", keyword);
                    parameters[2] = new SqlParameter("@STATUS", status);

                    var result = _context.Database.SqlQuery<DTO_COMMON_MEDICAL_SOCIETY>(CommonContext.USP_SELECT_MEDICAL_SOCIETY, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }


		/// <summary>
		/// 문서 정렬 순서 변경
		/// </summary>
		/// <param name="document"></param>
		public void UpdateDocumentList(DTO_USER_CONFIG_MENU_SORT document)
		{
			using (_context = new CommonContext())
			{
				SqlParameter[] parameters = new SqlParameter[3];
				parameters[0] = new SqlParameter("@USER_ID", document.USER_ID);
				parameters[1] = new SqlParameter("@EVENT_ID", document.EVENT_ID);
				parameters[2] = new SqlParameter("@SORT", document.SORT);

				_context.Database.ExecuteSqlCommand(CommonContext.USP_UPDATE_USER_CONFIG_MENU_SORT, parameters);
			}
		}

		/// <summary>
		/// 위임 리스트 가져오기
		/// </summary>
		/// <param name="userId">사용자 ID</param>
		/// <param name="idx">위임 ID</param>
		public List<DTO_COMMON_ABSENCE> SelectDelegationList(string userId, int idx)
		{
			using (_context = new CommonContext())
			{
				SqlParameter[] parameters = new SqlParameter[2];
				parameters[0] = new SqlParameter("@USER_ID", userId);
				parameters[1] = new SqlParameter("@IDX", idx);
				
				var result = _context.Database.SqlQuery<DTO_COMMON_ABSENCE>(CommonContext.USP_SELECT_ABSENCE, parameters);
				return result.ToList();
			}
		}


		/// <summary>
		/// 위임자 리스트 가져오기
		/// </summary>
		/// <param name="userId">사용자 ID</param>
		public List<UserInfoDto> SelectDelegationToList(string userId)
		{
			using (_context = new CommonContext())
			{
				SqlParameter[] parameters = new SqlParameter[1];
				parameters[0] = new SqlParameter("@USER_ID", userId);
				
				var result = _context.Database.SqlQuery<UserInfoDto>(CommonContext.USP_SELECT_USER_LIST_DELEGATION, parameters);
				return result.ToList();
			}
		}

		
		public void MergeDelegation(DTO_COMMON_ABSENCE delegation)
		{
			using (_context = new CommonContext())
			{
				SqlParameter[] parameters = new SqlParameter[6];
				parameters[0] = new SqlParameter("@IDX", Convert.ToInt32(delegation.IDX));
				parameters[1] = new SqlParameter("@APPROVER_ID", delegation.APPROVER_ID);
				parameters[2] = new SqlParameter("@FROM_DATE", delegation.FROM_DATE);
				parameters[3] = new SqlParameter("@TO_DATE", delegation.TO_DATE);
				parameters[4] = new SqlParameter("@DESCRIPTION", delegation.DESCRIPTION);
				parameters[5] = new SqlParameter("@USER_ID", delegation.USER_ID);

				_context.Database.ExecuteSqlCommand(CommonContext.USP_MERGE_ABSENCE, parameters);
			}
		}

		public void DeleteDelegation(string userId, int idx)
		{
			using (_context = new CommonContext())
			{
				SqlParameter[] parameters = new SqlParameter[2];
				parameters[0] = new SqlParameter("@USER_ID", userId);
				parameters[1] = new SqlParameter("@IDX", idx);

				_context.Database.ExecuteSqlCommand(CommonContext.USP_DELETE_ABSENCE, parameters);
			}
		}

        public List<DTO_COMMON_CODE_SUB> SelectCommonCodeAll()
        {
            try
            {
                using (_context = new CommonContext())
                {
                    var result = _context.Database.SqlQuery<DTO_COMMON_CODE_SUB>(CommonContext.USP_SELECT_CODE_SUB_ALL);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public List<DTO_MASTER_CRM_PRODUCT> SelectCRMProduct(string keyword)
        {
            try
            {
                using (_context = new CommonContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@KEYWORD", keyword);

                    var result = _context.Database.SqlQuery<DTO_MASTER_CRM_PRODUCT>(CommonContext.USP_SELECT_CRM_PRODUCT, parameters);
                    return result.ToList();
                }
            }
            catch
            {
                throw;
            }
        }

        public void MergeMedicalSociety(DTO_COMMON_MEDICAL_SOCIETY society)
        {
            using (_context = new CommonContext())
            {
                SqlParameter[] parameters = new SqlParameter[5];
                parameters[0] = new SqlParameter("@SOCIETY_IDX", society.SOCIETY_IDX);
                parameters[1] = new SqlParameter("@SOCIETY_NAME", society.SOCIETY_NAME);
                parameters[2] = new SqlParameter("@STATUS", society.STATUS);
                parameters[3] = new SqlParameter("@CREATOR_ID", society.CREATOR_ID);
                parameters[4] = new SqlParameter("@UPDATER_ID", society.UPDATER_ID);

                _context.Database.ExecuteSqlCommand(CommonContext.USP_MERGE_MEDICAL_SOCIETY, parameters);
            }
        }

        public void DeleteMedicalSociety(int id)
        {
            using (_context = new CommonContext())
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@SOCIETY_IDX", id);

                _context.Database.ExecuteSqlCommand(CommonContext.USP_DELETE_MEDICAL_SOCIETY, parameters);
            }
        }


        public List<SendmailToAddressListDto> SelectSendMailTargetList(string processid, string mailsendType)
        {

            using (_context = new CommonContext())
            {
                SqlParameter[] parameters = new SqlParameter[2];
                parameters[0] = new SqlParameter("@PROCESS_ID", processid);
                parameters[1] = new SqlParameter("@MAILSENDTYPE", mailsendType);


                var result = _context.Database.SqlQuery<SendmailToAddressListDto>(CommonContext.USP_SELECT_SENDMAIL_TO_ADDRESS_LIST, parameters);

                return result.ToList();
            }
        }

        public void UpdateApproverSentMail(string processid, string approver, string split)
        {
            try
            {
                using (_context = new CommonContext())
                {
                    SqlParameter[] parameters = new SqlParameter[3];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processid);
                    parameters[1] = new SqlParameter("@APPROVER_ID", approver);
                    parameters[2] = new SqlParameter("@SPLIT", split);
                    _context.Database.ExecuteSqlCommand(CommonContext.USP_UPDATE_PROCESS_APPROVER_SENT_MAIL, parameters);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #region [ SelectProcessRejectUser ]
        public RejectDocumentMailCommentDto SelectProcessRejectUser(string processId)
        {
            try
            {
                using (_context = new CommonContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processId);

                    var result = _context.Database.SqlQuery<RejectDocumentMailCommentDto>(CommonContext.USP_SELECT_PROCESS_REJECT_USER, parameters);

                    return result.First();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

        #region [ SelectInputComment ]
        public string SelectInputComment(string processId)
        {
            try
            {
                using (_context = new CommonContext())
                {
                    SqlParameter[] parameters = new SqlParameter[1];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processId);

                    var result = _context.Database.SqlQuery<string>(CommonContext.USP_SELECT_INPUT_COMMENT, parameters);

                    return result.First();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion

         
        public void MeageSendMailQueue(DTO_SENDMAIL dto)
        {
            try
            {
                using (_context = new CommonContext())
                { 
                    _context.Database.ExecuteSqlCommand(CommonContext.USP_MERGE_SENDMAIL_QUEUE, ParameterMapper.Mapping(dto));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_SENDMAIL> SelectSendMailQueueList()
        {
            using (_context = new CommonContext())
            { 
                var result = _context.Database.SqlQuery<DTO_SENDMAIL>(CommonContext.USP_SELECT_SEND_MAIL_QUEUE);

                return result.ToList();
            }
        }

        public void UpdateApproverSentMailComment(string processId, string status, string approver, string split)
        {
            try
            {
                using (_context = new CommonContext())
                {
                    SqlParameter[] parameters = new SqlParameter[4];
                    parameters[0] = new SqlParameter("@PROCESS_ID", processId);
                    parameters[1] = new SqlParameter("@STATUS", status);
                    parameters[2] = new SqlParameter("@APPROVER_ID", approver);
                    parameters[3] = new SqlParameter("@SPLIT", split);
                    _context.Database.ExecuteSqlCommand(CommonContext.USP_UPDATE_PROCESS_APPROVER_SENT_MAIL_COMMENT, parameters);
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public List<SendmailToAddressListDto> GetNoticeMailList(DateTime dtbasicdate)
        {

            using (_context = new CommonContext())
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@BASICDATE", dtbasicdate);
  
                var result = _context.Database.SqlQuery<SendmailToAddressListDto>(CommonContext.USP_SELECT_SEND_NOTICEMAIL_LIST, parameters);

                return result.ToList();
            }
        }
    }
}
