using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Configuration;
using System.IO;
using Bayer.Ultra.Framework.Common.Dto.Common;
using System.Net.Mail;
using System.Net;
using System.ServiceModel.Activation;

namespace Bayer.Ultra.Agent.SendMailAgent
{
    [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
    public partial class SendMailAgent : ServiceBase
	{
		public static string LogTrace = string.Empty;
		
		public SendMailAgent()
		{
			InitializeComponent();
		}

		protected override void OnStart(string[] args)
		{
			this.ServiseStart();
		}

		protected override void OnStop()
		{
            
		}


		/// <summary>
		/// 서비스 시작
		/// </summary>
		public void ServiseStart()
		{
			try
			{
                SendMailAgent.LogTrace = "SendMailAgent.ServiseStart.DisplayConfig";
                DisplayConfig();

                SendMailAgent.LogTrace = "SendMailAgent.ServiseStart.SelectSendMailQueueList";
                //2019-07-22 :delete below function due to no need for starting (WK Kim)
                //SelectSendMailQueueList();

                SendMailAgent.LogTrace = "SendMailAgent.ServiseStart.timer1.Start";
                this.timer1.Interval = Convert.ToDouble(ConfigurationManager.AppSettings["Interval"]) * 1000 * 60;
                this.timer1.Start();
                this.dtDaily.Interval = 60 * 1000;
                this.dtDaily.Start();
            }
			catch (Exception ex)
			{
				Common.WriteLog("ServiseStart - error!!!!!!");
				Common.WriteLog("----------------------------------------------");
				Common.WriteLog("ex - " + ex.ToString());
				Common.WriteLog("LogTrace - " + SendMailAgent.LogTrace);
				Common.WriteLog("----------------------------------------------");
			}
			finally
			{
			}
		}

		/// <summary>
		/// 초기화 로그를 찍는다.
		/// </summary>
		public void DisplayConfig()
		{
			try
			{
				Common.WriteLog("=================================================");
				Common.WriteLog("=================================================");
				for (int i = 0; i < ConfigurationManager.AppSettings.Count; i++)
				{
					Common.WriteLog(ConfigurationManager.AppSettings.Keys[i] + " : " + ConfigurationManager.AppSettings[i]);
				}
				Common.WriteLog("=================================================");
				Common.WriteLog("=================================================");				
			}
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
			finally
			{
			}
		}


		/// <summary>
		/// 메일 발송할 목록를 불러온다.
		/// </summary>
		private void SelectSendMailQueueList()
		{
            List<DTO_SENDMAIL> dto = null;
            List<DTO_SENDMAIL_VIOLATION> vioDto = null;
            List<DTO_SENDMAIL_INTERFACE> interDto = null;
			try
			{  
				SendMailAgent.LogTrace = "SendMailAgent.SelectSendMailQueueList";
                // 데이터를 가저온다.
                using (Common mgr  = new Common())
                {
                    dto = mgr.SelectSendMailQueueList();
                    foreach (DTO_SENDMAIL tesk in dto)
                    {
                        if (tesk.SEND_MAIL_TYPE.Equals(Bayer.Ultra.Framework.Common.ApprovalUtil.SendMailType.Violation.ToString()))
                        {
                            vioDto = mgr.SelectSendMailViolation(tesk.IDX);
                            ProcessViolationSendMail(tesk, vioDto);
                        }

                        if (tesk.SEND_MAIL_TYPE.Equals(Bayer.Ultra.Framework.Common.ApprovalUtil.SendMailType.Interface.ToString()))
                        {
                            interDto = mgr.SelectSendMailInterface();
                            ProcessInterfaceSendMail(tesk, interDto);
                        }
                    }
                }

			}
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
			finally
			{
			 
			}
		}
          

		/// <summary>
		/// 
		/// </summary> 
        private void ProcessViolationSendMail(DTO_SENDMAIL tesk, List<DTO_SENDMAIL_VIOLATION> violation)
		{
            StringBuilder sbHtml = new StringBuilder(512);
            MailFormat format = null;

            const string VIOLATION_ITEMS = "{VIOLATION_ITEMS}";
            string body = string.Empty;
            try
			{
                if( violation.Count <= 0 )
                {
                    Common.WriteLog( string.Format("해당 IDX는 Violation (HCP) 목록에 존재하지 않습니다. {0}주소로 발송하지 못하였습니다.", tesk.MAILADDRESS));
                    return;
                }
                format = SendMailMgr.Instance.GetMailFormat(Bayer.Ultra.Framework.Common.ApprovalUtil.SendMailType.Violation.ToString());
                 
                string thCSS = "border-width: 1px;padding: 8px;border-style: solid;border-color: #517994;background-color: #B2CFD8;";
                string tdCSS = "border-width: 1px;padding: 8px;border-style: solid;border-color: #517994;background-color: #ffffff;";

                sbHtml.Append("<table style=\"font-family: verdana, arial, sans-serif;font-size: 11px;color: #333333;border-width: 1px;border-color: #3A3A3A;border-collapse: collapse; \" > ");
                sbHtml.Append("<thead>");
                sbHtml.AppendFormat("<tr><th style='{0}'>Subject</th><th style='{0}'>Type</th><th style='{0}'>Status</th><th style='{0}'>HCP Name</th></tr>", thCSS);
                sbHtml.Append("</thead>");
                sbHtml.Append("<tbody>");
                foreach(DTO_SENDMAIL_VIOLATION vItem in violation)
                {
                    sbHtml.Append("<tr>");
                    sbHtml.AppendFormat("<td style='{0}'>{1}</td>", tdCSS, vItem.SUBJECT);
                    sbHtml.AppendFormat("<td style='{0}'>{1}</td>", tdCSS, vItem.TYPE);
                    sbHtml.AppendFormat("<td style='{0}'>{1}</td>", tdCSS, vItem.STATUS);
                    sbHtml.AppendFormat("<td style='{0}'>{1}</td>", tdCSS, vItem.HCP_NAME);
                    sbHtml.Append("</tr>");
                }
        
                sbHtml.Append("</tbody>");
                sbHtml.Append("</table>");
                 
                body = format.Body.Replace(VIOLATION_ITEMS, sbHtml.ToString());
                try
                { 
                    bool sendYN = SendMailMgr.Instance.SendMail(format.Subject, tesk.MAILADDRESS, body);
                    if (sendYN)
                    {
                        tesk.SEND_STATUS = Bayer.Ultra.Framework.Common.ApprovalUtil.SendMailStatus.Complete.ToString();
                        tesk.SEND_DATE = DateTime.Now;
                        UpdateSendMailResult(tesk);
                    }
                    else
                    {
                        tesk.SEND_STATUS = Bayer.Ultra.Framework.Common.ApprovalUtil.SendMailStatus.Fail.ToString();
                        tesk.RETRY_CNT = tesk.RETRY_CNT + 1;
                        UpdateSendMailResult(tesk);
                    }
                }
                catch (Exception ex)
                {
                    tesk.SEND_STATUS = Bayer.Ultra.Framework.Common.ApprovalUtil.SendMailStatus.Fail.ToString();
                    tesk.RETRY_CNT = tesk.RETRY_CNT + 1;
                    tesk.REMARK = ex.ToString();
                    UpdateSendMailResult(tesk);
                }
                Common.WriteLog(string.Format("{0}주소로 Violation 메일을 발송하였습니다.", tesk.MAILADDRESS));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
			finally
			{
                if(sbHtml != null)
                {
                    sbHtml.Clear();
                    sbHtml = null;
                }
			}
		}


        private void ProcessInterfaceSendMail(DTO_SENDMAIL tesk, List<DTO_SENDMAIL_INTERFACE> interfaceResult)
        {

            StringBuilder sbHtml = new StringBuilder(512);
            MailFormat format = null;
            


            const string INTERFACE_ITEMS = "{INTERFACE_ITEMS}";
            string body = string.Empty;
            try
            {
                if (interfaceResult.Count <= 0)
                {
                    Common.WriteLog(string.Format("해당 INTERFACE LOG 가 존재하지 않습니다. {0}주소로 발송하지 못하였습니다.", tesk.MAILADDRESS));
                    return;
                }

                //int intParticipantCount = dtoParticipantsList.Where(P => P.LOG_TYPE == "Complete").Where(P => P.IS_ATTENDED == "Y").Where(P => P.PARTICIPANT_TYPE != "Employee").Count();

                DTO_SENDMAIL_INTERFACE interfaceInfo = interfaceResult.Where(L => L.LOG_TYPE == "Complete").FirstOrDefault();

                format = SendMailMgr.Instance.GetMailFormat(Bayer.Ultra.Framework.Common.ApprovalUtil.SendMailType.Interface.ToString());

                string thCSS = "border-width: 1px;padding: 8px;border-style: solid;border-color: #517994;background-color: #B2CFD8;";
                string tdCSS = "border-width: 1px;padding: 8px;border-style: solid;border-color: #517994;background-color: #ffffff;";
                
                sbHtml.Append("<table style=\"font-family: verdana, arial, sans-serif;font-size: 11px;color: #333333;border-width: 1px;border-color: #3A3A3A;border-collapse: collapse; \" > ");
                sbHtml.Append("<thead>");
                sbHtml.AppendFormat("<tr><th style='{0}'>Message</th><th style='{0}'>ExecutionDate</th><th style='{0}'>Fetch</th><th style='{0}'>Error</th></tr>", thCSS);
                sbHtml.Append("</thead>");
                sbHtml.Append("<tbody>");
                sbHtml.Append("<tr>");
                
                string[] arrMsg = interfaceInfo.MESSAGE.Split('|');

                sbHtml.AppendFormat("<td style='{0}'>{1}</td>", tdCSS, arrMsg[0].ToString().Trim());
                sbHtml.AppendFormat("<td style='{0}'>{1}</td>", tdCSS, arrMsg[1].ToString().Split(':')[1].ToString().Trim());
                sbHtml.AppendFormat("<td style='{0}'>{1}</td>", tdCSS, arrMsg[2].ToString().Split(':')[1].ToString().Trim());
                sbHtml.AppendFormat("<td style='{0}'>{1}</td>", tdCSS, arrMsg[3].ToString().Split(':')[1].ToString().Trim());
                
                sbHtml.Append("</tr>");
                sbHtml.Append("</tbody>");
                sbHtml.Append("</table>");

                sbHtml.Append("<br>");
                sbHtml.Append("<table style=\"font-family: verdana, arial, sans-serif;font-size: 11px;color: #333333;border-width: 1px;border-color: #3A3A3A;border-collapse: collapse; \" > ");
                sbHtml.Append("<thead>");
                sbHtml.AppendFormat("<tr><th style='{0}'>MESSAGE</th><th style='{0}'>FETCH_SEQ</th><th style='{0}'>ACTIVITY_ID</th><th style='{0}'>USER_ID</th>", thCSS);
                sbHtml.AppendFormat("<th style='{0}'>HCO_NAME</th><th style='{0}'>HCP_NAME</th><th style='{0}'>HCP_CODE</th><th style='{0}'>ACTIVITY_TYPE</th><th style='{0}'>EVENT_STATUS</th></tr>", thCSS);
                sbHtml.Append("</thead>");
                sbHtml.Append("<tbody>");
                sbHtml.Append("<tr>");


                foreach (DTO_SENDMAIL_INTERFACE IRitem in interfaceResult)
                {
                    sbHtml.Append("<tr>");
                    sbHtml.AppendFormat("<td style='{0}'>{1}</td>", tdCSS, IRitem.MESSAGE);
                    sbHtml.AppendFormat("<td style='{0}'>{1}</td>", tdCSS, IRitem.FETCH_SEQ);
                    sbHtml.AppendFormat("<td style='{0}'>{1}</td>", tdCSS, IRitem.ACTIVITY_ID);
                    sbHtml.AppendFormat("<td style='{0}'>{1}</td>", tdCSS, IRitem.USER_ID);
                    sbHtml.AppendFormat("<td style='{0}'>{1}</td>", tdCSS, IRitem.HCO_NAME);
                    sbHtml.AppendFormat("<td style='{0}'>{1}</td>", tdCSS, IRitem.HCP_CODE);
                    sbHtml.AppendFormat("<td style='{0}'>{1}</td>", tdCSS, IRitem.HCP_NAME);
                    sbHtml.AppendFormat("<td style='{0}'>{1}</td>", tdCSS, IRitem.ACTIVITY_TYPE);
                    sbHtml.AppendFormat("<td style='{0}'>{1}</td>", tdCSS, IRitem.EVENT_STATUS);
                    sbHtml.Append("</tr>");
                }

                sbHtml.Append("</tr>");
                sbHtml.Append("</tbody>");
                sbHtml.Append("</table>");

                body = format.Body.Replace(INTERFACE_ITEMS, sbHtml.ToString());
                try
                {
                    bool sendYN = SendMailMgr.Instance.SendMail(format.Subject, tesk.MAILADDRESS, body);
                    if (sendYN)
                    {
                        tesk.SEND_STATUS = Bayer.Ultra.Framework.Common.ApprovalUtil.SendMailStatus.Complete.ToString();
                        tesk.SEND_DATE = DateTime.Now;
                        UpdateSendMailResult(tesk);
                    }
                    else
                    {
                        tesk.SEND_STATUS = Bayer.Ultra.Framework.Common.ApprovalUtil.SendMailStatus.Fail.ToString();
                        tesk.RETRY_CNT = tesk.RETRY_CNT + 1;
                        UpdateSendMailResult(tesk);
                    }
                }
                catch (Exception ex)
                {
                    tesk.SEND_STATUS = Bayer.Ultra.Framework.Common.ApprovalUtil.SendMailStatus.Fail.ToString();
                    tesk.RETRY_CNT = tesk.RETRY_CNT + 1;
                    tesk.REMARK = ex.ToString();
                    UpdateSendMailResult(tesk);
                }
                Common.WriteLog(string.Format("{0}주소로 INTERFACE LOG 메일을 발송하였습니다.", tesk.MAILADDRESS));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            finally
            {
                if (sbHtml != null)
                {
                    sbHtml.Clear();
                    sbHtml = null;
                }
            }
        }


        /// <summary>
        /// 결과를 DB에 업데이트 한다. 
        /// </summary>
        /// <param name="rID"></param>
        /// <param name="ds"></param>
        private void UpdateSendMailResult(DTO_SENDMAIL tesk)
		{ 
			try
			{
				SendMailAgent.LogTrace = "SendMailAgent.UpdateSendMailResult";
                using (Common mgr = new Common())
                {
                    mgr.MeageSendMailQueue(tesk);
                }
			}
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
			finally
			{ 
			}
		}


		/// <summary>
		/// 타이머 티커 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			this.timer1.Enabled = false;
        
            try
			{
				SendMailAgent.LogTrace = "SendMailAgent.UpdateWithdrawResult.SelectSendMailQueueList";
                SelectSendMailQueueList();
 
            }
            catch (Exception ex)
			{
				Common.WriteLog("timer1_Elapsed - 오류 발생!!!!!!");
				Common.WriteLog("----------------------------------------------");
				Common.WriteLog("ex - " + ex.ToString());
				Common.WriteLog("LogTrace - " + SendMailAgent.LogTrace);
				Common.WriteLog("----------------------------------------------");
			}
			finally
			{
				this.timer1.Enabled = true;
			}
		}

        private void dtDaily_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.dtDaily.Enabled = false;

            try
            {
                TimeSpan tSpan = new TimeSpan(e.SignalTime.Hour, e.SignalTime.Minute, 00);
                TimeSpan optSpan = TimeSpan.Parse(ConfigurationManager.AppSettings["DailyTime"]);
                string concurYN = ConfigurationManager.AppSettings["concurYN"];
                TimeSpan concurSpan = TimeSpan.Parse(ConfigurationManager.AppSettings["ConcurTime"]);

                if (tSpan.Equals(optSpan))
                {
                    SendNoticeMail();
                }

                if (concurYN.Equals("Y") && tSpan.Equals(concurSpan))
                {
                    // txt 첨부파일 메일 발송
                    SendConcurTransferMail();
                }

            }
            catch (Exception ex)
            {
                Common.WriteLog("timer1_Elapsed - 오류 발생!!!!!!");
                Common.WriteLog("----------------------------------------------");
                Common.WriteLog("ex - " + ex.ToString());
                Common.WriteLog("LogTrace - " + SendMailAgent.LogTrace);
                Common.WriteLog("----------------------------------------------");
            }
            finally
            {
                this.dtDaily.Enabled = true;
            }
        }


        #region SendNoticeMail
        /// <summary>
        /// 장기간 미 결재자에 메일 발송
        /// </summary>
        public void SendNoticeMail()
        {
            try
            {
                Common.WriteLog("");
                Common.WriteLog("SendNoticeMail - 기준 날짜 계산....");
                SendMailAgent.LogTrace = "SendNoticeMail.GetBasicDate";
                double dbasedate = Convert.ToDouble(ConfigurationManager.AppSettings["basicdate"]) * -1;

                if (DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
                {
                    if (DateTime.Now.DayOfWeek == DayOfWeek.Monday || DateTime.Now.DayOfWeek == DayOfWeek.Tuesday)
                        dbasedate = dbasedate - 2;

                    string strBasicDate = DateTime.Now.AddDays(dbasedate).ToString("yyyy-MM-dd");

                    Common.WriteLog("SendNoticeMail - 웹서비스 실행..../BasicDate = " + strBasicDate);
                    SendMailAgent.LogTrace = "SendNoticeMail.GetNoticeMailList";

                    SendNoticeMail(strBasicDate);

                    Common.WriteLog("SendNoticeMail - 장기간 미 결재자 목록 완료..../DayOfWeek = " + DateTime.Now.DayOfWeek.ToString());
                }
                else
                {
                    Common.WriteLog("SendNoticeMail - 휴일 발송 안함..../DayOfWeek = " + DateTime.Now.DayOfWeek.ToString());
                }
            }
            catch
            {
                throw;
            }
        }

        private void SendNoticeMail(string basicdate)
        {
            try
            {
                SendMailAgent.LogTrace = "SendNoticeMail";

                string sendMailType = "DelayApproval";
                string wcfUrl = ConfigurationManager.AppSettings["serviceUrl"];
                string serviceUrl = string.Format("{0}/SendNoticeMail/{1}/{2}", wcfUrl, sendMailType, basicdate);
                Common.WriteLog(string.Format("SendNoticeMail - 발송항목 정보 = basicdate : {0} / url : {1}", basicdate, serviceUrl));

                SendMailAgent.LogTrace = "SendNoticeMail.WcfSvc";
                HttpWebRequest request = WebRequest.Create(serviceUrl) as HttpWebRequest;
                using (HttpWebResponse response = request.GetResponse() as HttpWebResponse)
                {
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        Common.WriteLog("SendNoticeMail.WcfSvc - 메일 발송 완료....");
                    }
                    else
                    {
                        Common.WriteLog("SendNoticeMail.WcfSvc - 메일 발송 실패!!!!");
                        Common.WriteLog("----------------------------------------------");
                        Common.WriteLog("ex - " + String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));
                        Common.WriteLog("LogTrace - " + SendMailAgent.LogTrace);
                        Common.WriteLog("----------------------------------------------");
                    }
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion



        #region SendConcurTransferMail
        /// <summary>
        /// Concur Transfer 메일 발송
        /// </summary>
        public void SendConcurTransferMail()
        {
            try
            {
                Common.WriteLog("");
                List<string> ConcurTransferList = null;
                
                if (DateTime.Now.DayOfWeek != DayOfWeek.Saturday && DateTime.Now.DayOfWeek != DayOfWeek.Sunday)
                {
                    Common.WriteLog("SendConcurTransferMail - config getting ....");
                    SendMailAgent.LogTrace = "SendConcurTransferMail.GetFrom";
                    string from = ConfigurationManager.AppSettings["concurFrom"];
                    SendMailAgent.LogTrace = "SendConcurTransferMail.GetTo";
                    string to = ConfigurationManager.AppSettings["concurTo"];
                    SendMailAgent.LogTrace = "SendConcurTransferMail.GetCc";
                    string cc = ConfigurationManager.AppSettings["concurCc"];
                    SendMailAgent.LogTrace = "SendConcurTransferMail.subject";
                    string subject = string.Format("CRM_Attendee_list, {0}, Korea", DateTime.Now.ToString("yyyy-MM-dd"));
                    SendMailAgent.LogTrace = "SendConcurTransferMail.body";
                    //string body = @"Dear TnE_BBS,<br/>
                    //            <br/>
                    //            Please upload the CRM Attendee list.<br/>
                    //            <br/>
                    //            Thanks.";
                    string strFilePath = string.Format("attendee_p00036763jya_HCPKR_{0}.txt", DateTime.Now.ToString("yyyyMMdd"));
                    string body = @"Dear TnE_BBS,<br/>
                                <br/>
                                The below CRM Attendee list is pushed to sFTP server.<br/>
                                File Name : " + strFilePath + @"
                                < br/>< br/>
                                Thanks.";

                    SendMailAgent.LogTrace = "SaveMailAttachFile.SelectConcurTransfer";
                    // 데이터를 가저온다.
                    using (Common mgr = new Common())
                    {
                        ConcurTransferList = mgr.SelectConcurTransfer();
                    }

                    if (ConcurTransferList == null || ConcurTransferList.Count == 0)
                    {
                        Common.WriteLog("SendConcurTransferMail - 메일 발송 안함..../ListCount=0");
                        return;
                    }

                    SendMailAgent.LogTrace = "SendConcurTransferMail.SaveMailAttachFile";
                    string filePath = SaveMailAttachFile(ConcurTransferList);

                    SendMailAgent.LogTrace = "SendConcurTransferMail.UpdateConcurTransfer";

                    //File Transfer to sFTP : Strat
                    string source = filePath;
                    string destination = @"users";
                    string host = "10.190.195.68";
                    string username = "concur@3FTP68";
                    string password = "Newpass33##";
                    int port = 2222;  //Port 22 is defaulted for SFTP upload


                    sftp_solution.UploadSFTPFile(host, username, password, source, destination, port);
                    //File Transfer to sFTP : End



                    // 데이터를 가저온다.
                    using (Common mgr = new Common())
                    {
                        mgr.UpdateConcurTransfer();
                    }

                    SendMailAgent.LogTrace = "SendConcurTransferMail.SendMail";
                    bool sendYN = SendMailMgr.Instance.SendMail(subject, from, to, cc, body, filePath);

                    Common.WriteLog("SendConcurTransferMail - 메일 발송 완료....");
                }
                else
                {
                    Common.WriteLog("SendConcurTransferMail - 휴일 발송 안함..../DayOfWeek = " + DateTime.Now.DayOfWeek.ToString());
                }
            }
            catch
            {
                throw;
            }
        }

        private string SaveMailAttachFile(List<string> ConcurTransferList)
        {
            string strFilePath = string.Empty;

            try
            {
                SendMailAgent.LogTrace = "SaveMailAttachFile.GetUploadPath";
                string uploadPath = ConfigurationManager.AppSettings["concurTxtPath"];

                strFilePath = string.Format("{0}\\attendee_p00036763jya_HCPKR_{1}.txt", uploadPath, DateTime.Now.ToString("yyyyMMdd"));

                if (!Directory.Exists(uploadPath))
                {
                    Directory.CreateDirectory(uploadPath);
                }

                SendMailAgent.LogTrace = "SaveMailAttachFile.SaveFile";
                File.WriteAllLines(strFilePath, ConcurTransferList, Encoding.UTF8);

            }
            catch
            {
                throw;
            }


            return strFilePath;
        }
        
        #endregion
    }
}
