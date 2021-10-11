using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Configuration;
using Bayer.Ultra.Framework.Database;
using System.Data.Entity;
using Bayer.Ultra.Framework.Common.Dto.Common;
using System.Runtime.InteropServices;
using System.Security.Principal;
using System.ServiceModel.Activation;

namespace Bayer.Ultra.Agent.SendMailAgent
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public class Common :  Framework.Database.DaoBase
    {
        string userName = "MWBRD";
        string domainName = "AD-BAYER-CNB";
        string password = "Il0veSQL1029!";
        public Common()
		{ 
		}

        public void MeageSendMailQueue(DTO_SENDMAIL dto)
        {
            try
            {
                using (_context = new CommonContract())
                {
                    _context.Database.ExecuteSqlCommand(CommonContract.USP_MERGE_SENDMAIL_QUEUE, ParameterMapper.Mapping(dto));
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DTO_SENDMAIL> SelectSendMailQueueList()
        {
            using (_context = new CommonContract())
            {
                var result = _context.Database.SqlQuery<DTO_SENDMAIL>(CommonContract.USP_SELECT_SEND_MAIL_QUEUE);

                return result.ToList();
            }
        }

        public List<DTO_SENDMAIL_VIOLATION> SelectSendMailViolation(int idx)
        {
            using (_context = new CommonContract())
            {
                SqlParameter[] parameters = new SqlParameter[1];
                parameters[0] = new SqlParameter("@IDX", idx); 
                var result = _context.Database.SqlQuery<DTO_SENDMAIL_VIOLATION>(CommonContract.USP_SELECT_SEND_MAIL_VIOLATION, parameters);

                return result.ToList();
            }
        }


        public List<DTO_SENDMAIL_INTERFACE> SelectSendMailInterface()
        {
            using (_context = new CommonContract())
            {
                var result = _context.Database.SqlQuery<DTO_SENDMAIL_INTERFACE>(CommonContract.USP_SELECT_SEND_MAIL_INTERFACE);

                return result.ToList();
            }
        }

        //IDX INTERFACE_DATE  WRITE_TIME MESSAGE
        //5711	2018-01-15 06:50	2018-01-15 06:50	Ultra Interface Execution Report  - ExecutionDate : 2018-01-15 06:50 - Fetch Count : 1111 - Error Count : 0

        public void test_person()
        {
            try
            {
                Common.WriteLog("name1 : " + System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                
                //using (var imp = new Impersonation(domainName,userName,  password))
                //{
                    using (_context = new CommonContract())
                    {
                        var result = _context.Database.SqlQuery<string>(CommonContract.USP_SELECT_PARTICIPANTS_CONCUR_TRANSFER);

                        Common.WriteLog(result.ToList()[1]);
                    }

                    Common.WriteLog("name1 : " + System.Security.Principal.WindowsIdentity.GetCurrent().Name);
                //}
                
            }
            catch (Exception ex)
            {
                Common.WriteLog("ServiseStart - error!!!!!!");
                Common.WriteLog("----------------------------------------------");
                Common.WriteLog("ex - " + ex.ToString());
                Common.WriteLog("LogTrace - " + SendMailAgent.LogTrace);
                Common.WriteLog("----------------------------------------------");
            }

        }



        public List<string> SelectConcurTransfer()
        {



            //using (var imp = new Impersonation(domainName, userName, password)) { 
                using (_context = new CommonContract())
                {
                    var result = _context.Database.SqlQuery<string>(CommonContract.USP_SELECT_PARTICIPANTS_CONCUR_TRANSFER);

                    return result.ToList();
                }
            //}


        }

        public void UpdateConcurTransfer()
        {
            using (_context = new CommonContract())
            {
                _context.Database.ExecuteSqlCommand(CommonContract.USP_UPDATE_PARTICIPANTS_CONCUR_TRANSFER);
            }
        }

        #region WriteLog
        /// <summary>
        /// <b>Log Write</b><br/>
        /// - 작  성  자 : 닷넷소프트 김학진<br/>
        /// - 최초작성일 : 2010.04.15<br/>
        /// - 최종수정자 : <br/>
        /// - 최종수정일 : <br/>
        /// - 주요변경로그<br/>
        /// 2010.04.15 생성<br/>
        /// </summary>
        /// <param name="sMessage">Log Message</param>
        public static void WriteLog(string sMessage)
		{
			string strLogDir = ConfigurationManager.AppSettings["LogFilePath"] + @"\" + DateTime.Now.ToString("yyyy-MM-dd");
			string strLogWriteYN = ConfigurationManager.AppSettings["LogWriteYN"];

			string strFullPath = string.Format(@"{0}\SendMailAgent.txt", strLogDir);

			FileStream oFs = null;
			StreamWriter oWriter = null;

			try
			{
				// 디렉토리 존재 여부 확인
				if (!Directory.Exists(strLogDir))
				{
					//디렉토리가 없는 경우에 폴더 생성 여부 확인
					if (strLogWriteYN.Equals("Y"))
					{
						System.IO.Directory.CreateDirectory(strLogDir);
						//return;
					}
				}

				oFs = new FileStream(strFullPath, FileMode.Append, FileAccess.Write, FileShare.Write);
				oWriter = new StreamWriter(oFs, System.Text.Encoding.Default);
				oWriter.WriteLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " - " + sMessage);
			}
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
			finally
			{
				if (oWriter != null) oWriter.Close();
				if (oFs != null) oFs.Close();
			}
		}
		#endregion
         
	}

    public class CommonContract : UltraDbContext
    {
        public CommonContract() 
            : this(ConfigurationManager.ConnectionStrings["Ultra"].ConnectionString)
        {

        }

        public CommonContract(string connectionString) : base(connectionString) { }

        /// <summary>
        /// Send Mail Agent Insert / Update
        /// </summary>
        public const string USP_MERGE_SENDMAIL_QUEUE = "[dbo].[USP_MERGE_SENDMAIL_QUEUE] @IDX, @SEND_MAIL_TYPE, @MAILADDRESS, @SEND_STATUS, @SEND_DATE, @RETRY_CNT, @REMARK";

        /// <summary>
        /// Send Mail Task List 
        /// </summary>
        public const string USP_SELECT_SEND_MAIL_QUEUE = "[dbo].[USP_SELECT_SEND_MAIL_QUEUE]";

        /// <summary>
        /// Send Mail Violation Item
        /// </summary>
        public const string USP_SELECT_SEND_MAIL_VIOLATION = "[dbo].[USP_SELECT_SEND_MAIL_VIOLATION] @IDX";

        /// <summary>
        /// Interface SP Result Message
        /// </summary>
        public const string USP_SELECT_SEND_MAIL_INTERFACE = "[dbo].[USP_SELECT_SEND_MAIL_INTERFACE]";

        /// <summary>
        /// Select Concur Transfer List
        /// </summary>
        public const string USP_SELECT_PARTICIPANTS_CONCUR_TRANSFER = "[dbo].[USP_SELECT_PARTICIPANTS_CONCUR_TRANSFER]";

        /// <summary>
        /// Update Concur Transfer List
        /// </summary>
        public const string USP_UPDATE_PARTICIPANTS_CONCUR_TRANSFER = "[dbo].[USP_UPDATE_PARTICIPANTS_CONCUR_TRANSFER]";
    }
}
