using Bayer.Ultra.Framework;
using Bayer.Ultra.Framework.Common;
using Bayer.Ultra.Framework.Common.Dto.Common;
using Bayer.Ultra.Framework.Config;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace Bayer.Ultra.BSL.Common.Mgr
{
    public class CommonMgr_Tx : Framework.Database.MgrBase
    {
        /// <summary>
        /// 로그인 정보 등록
        /// </summary>
        /// <param name="userID"></param>
        /// <param name="clientIP"></param>
        /// <param name="loginUserName"></param>
        /// <param name="machineName"></param>
        public void InsertLoginHistory(string userID, string clientIP, string loginUserName, string machineName)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.CommonDao dao = new Dao.CommonDao())
                    {
                        dao.InsertLoginHistory(userID, clientIP, loginUserName, machineName);

                    }
                    scope.Complete();
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
        public static void InsertSystemLog(string type, string eventName, string message, string creater)
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    dao.InsertSystemLog(type, eventName, message, creater);
                }
            }
            catch
            {
                //TO-DO:
            }
        }

        /// <summary>
        /// 문서 업데이트
        /// </summary>
        /// <param name="documents"></param>
        public void UpdateDocumentList(List<DTO_USER_CONFIG_MENU_SORT> documents)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    using (Dao.CommonDao dao = new Dao.CommonDao())
                    {
                        foreach (DTO_USER_CONFIG_MENU_SORT document in documents)
                        {
                            dao.UpdateDocumentList(document);
                        }
                    }
                    scope.Complete();
                }
            }
            catch
            {
                throw;
            }
        }

        public void MergeDelegation(DTO_COMMON_ABSENCE delegation)
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    dao.MergeDelegation(delegation);
                }
            }
            catch
            {
                throw;
            }
        }

        public void DeleteDelegation(string userId, int idx)
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    dao.DeleteDelegation(userId, idx);
                }
            }
            catch
            {
                throw;
            }
        }

        public void UpdateApproverSentMailComment(string processId, string status, string approver, string split)
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    dao.UpdateApproverSentMailComment(processId, status, approver, split);
                }
            }
            catch
            {
                throw;
            }
        }

        public void MailSendApprover(string processId, string sendMailType, string approvers)
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    dao.UpdateApproverSentMailComment(processId, "R", approvers, ",");
                }
                MailSend(processId, sendMailType, Bayer.Ultra.Framework.Config.WebSiteConfigHandler.SmtpManager.Sender);
            }
            catch
            {
                throw;
            }
        }

        public void MailSend(string processID, string sendMailType, string senderAddress)
        {
            MailFormat format = null;
            string retValue = string.Empty;
            try
            {
                if (!senderAddress.Contains("@"))
                {
                    using (Dao.AuthenticationDao dao = new Dao.AuthenticationDao())
                    {
                        UserInfoDto dto = dao.GetUserInfo("ko-kr", senderAddress);
                        if (dto != null)
                            senderAddress = dto.MAIL_ADDRESS;
                    }
                }

                format = GetMailFormat(sendMailType);
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {

                    List<SendmailToAddressListDto> list = dao.SelectSendMailTargetList(processID, sendMailType);
                    retValue = SendSmtpMail(processID, format, senderAddress, sendMailType, list);
                    if (retValue == "OK")
                    {
                        dao.UpdateApproverSentMail(processID, format.ApproverList, ",");
                    }
                }

            }
            catch (Exception ex)
            {
                CommonMgr_Tx.InsertSystemLog("Error", string.Format("{0}.{1}", this.GetType().Name, System.Reflection.MethodInfo.GetCurrentMethod().Name), ex.ToString(), string.Empty);
            }
            finally
            {
                if (format != null)
                {
                    format = null;
                }
            }
        }

        public void SendNoticeMail(string sendMailType, string searchdata, string senderAddress)
        {
            MailFormat format = null;
            try
            {
                List<SendmailToAddressListDto> sendlist = null;
              
                if (sendMailType.Equals("DelayApproval"))
                {
                    DateTime dtbasicdate = Convert.ToDateTime(searchdata);
                    using (Dao.CommonDao dao = new Dao.CommonDao())
                    {
                        sendlist = dao.GetNoticeMailList(dtbasicdate);
                     
                        if(sendlist.Count > 0)
                        {
                            format = GetMailFormat(sendMailType);
                            SendSmtpMail(format, sendMailType, sendlist, senderAddress);
                        }
                    }   
                }  
            }
            catch (Exception ex)
            {
            }
            finally
            {
                if (format != null)
                {
                    format = null;
                }
            }
        }
        public static void WriteLog(string sMessage)
        {
            string strLogDir = @"C:\temp\upload\";
            string strLogWriteYN = "Y";

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

        private string SendSmtpMail(MailFormat format, string sendMailType, List<SendmailToAddressListDto> list, string senderAddress)
        {
            string retValue = string.Empty;
            string documentid = string.Empty;
            string documentName = string.Empty;
            string formName = string.Empty;
            string comment = string.Empty;
            string DearName = string.Empty;
            string processId = string.Empty;
            string url = string.Empty;
            string documentUri = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.SmtpManager.EventUrl;
            string sender = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.SmtpManager.Sender;
            const string DEAR_NAME = "{DEAR_NAME}", DOC_SUBJECT = "{DOC_SUBJECT}", DOC_HREF = "{DOC_HREF}", REQUESTER = "{REQUESTER}", EVENT_NAME = "{EVENT_NAME}", REQUEST_DATE = "{REQUEST_DATE}";
            StringBuilder sbUrl = new StringBuilder(128);
            MailFormat format_e = GetMailFormat("DelayEventComplete");
            MailFormat format_p = GetMailFormat("DelayPaymentComplete");
            // version 1.0.6 : Veeva 동의서 수집 관련 자동메일 추가 
            MailFormat format_c = GetMailFormat("VeevaConsent");
            MailFormat r_format = null;
            try
            {
                SmtpClient SmtpServer = SmtpManager.CreateSmtpClientObj();
                MailMessage mail = null;
                

                foreach (SendmailToAddressListDto approver in list)
                { 
                    if (approver.MAIL_ADDRESS.IsNullOrEmptyEx()) continue;
                    r_format = format;
                    if (approver.MAIL_TYPE == "E") r_format = format_e;
                    if (approver.MAIL_TYPE == "P") r_format = format_p;
                    // version 1.0.6 : Veeva 동의서 수집 관련 자동메일 추가 
                    if (approver.MAIL_TYPE == "C") r_format = format_c;
                    processId = approver.PROCESS_ID;
                    documentid = approver.EVENT_ID;
                    documentName = approver.EVENT_NAME;
                    formName = approver.WEB_PAGE_NAME;
                    comment = approver.COMMENT;
                    DearName = approver.APPROVER_NAME;

                    sbUrl = new StringBuilder(128);
                    

                    mail = new MailMessage();
                    mail.From = new MailAddress(senderAddress);

                    mail.To.Add(approver.MAIL_ADDRESS);

                    // version 1.0.6 : Veeva 동의서 수집 관련 자동메일 추가 
                    if (approver.MAIL_TYPE == "C")
                    {
                        mail.CC.Add(approver.SENDER_MAIL_ADDRESS+"@cwid.bayer.com");
                        mail.Bcc.Add("kr_sfe@bayer.com");
                        
                    }
                    else
                    {
                        string eventUrl = string.Format(@"{0}{1}?processid={2}&eventid={3}&title={4}", documentUri, formName, processId, documentid, documentName);
                        sbUrl.AppendFormat("http://{0}/Ultra/Pages/Authentication/Gate/GatewayAuth.aspx?ReturnURL=", Bayer.Ultra.Framework.Config.WebSiteConfigHandler.Login.Domain);
                        sbUrl.AppendFormat(System.Web.HttpUtility.UrlEncode("/ultra/Pages/Main.aspx?maillink=Y&tabpage=" + System.Web.HttpUtility.UrlEncode(eventUrl)));
                    }

                    mail.To.Clear();
                    mail.CC.Clear();
                    mail.To.Add("wookyung.kim@bayer.com,youngwoo.lee@bayer.com");



                    mail.Subject = r_format.Subject.Replace(DOC_SUBJECT, string.Format("{0} : {1}", approver.EVENT_NAME, approver.SUBJECT));
                    mail.IsBodyHtml = true;
                     
                    string requester = approver.SENDER_NAME;
                    string requestdate = approver.REQUEST_DATE.ToString();

                    string body = r_format.Body.Replace(DEAR_NAME, DearName);
                    body = body.Replace(DOC_HREF, sbUrl.ToString());
                    body = body.Replace(REQUESTER, requester);
                    body = body.Replace(REQUEST_DATE, requestdate);
                    body = body.Replace(EVENT_NAME, documentName);

                    mail.Body = body;
                    WriteLog(approver.MAIL_ADDRESS);
                    SmtpServer.Send(mail);

                    using (Dao.CommonDao dao = new Dao.CommonDao())
                    {
                        if (approver.MAIL_TYPE != "C")
                        {
                            dao.UpdateApproverSentMail(processId, approver.APPROVER_ID, "");
                        }
                            
                    }
                }

            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                retValue = ex.Message;
            }
            finally
            {
                if(sbUrl != null)
                {
                    sbUrl.Clear();
                    sbUrl = null;
                }
            }

            return retValue;
        }

        /// <summary>
        /// 2014.12.26 기존 SendMailSMTP에서 추가로 변경
        /// Dear가 각 메일본문에 누적 안되도록
        /// </summary>
        /// <param name="processId"></param>
        /// <param name="format"></param>
        /// <param name="senderAddress"></param>
        /// <param name="sendMailType"></param>
        /// <returns></returns>
        private string SendSmtpMail(string processId, MailFormat format, string senderAddress, string sendMailType, List<SendmailToAddressListDto> list)
        {
            string retValue = "OK";
            string documentUri = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.SmtpManager.EventUrl;
            string sender = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.SmtpManager.Sender;

            string DearName = string.Empty, body = format.Body;
            StringBuilder sbComment = new StringBuilder(128);
            StringBuilder sbUrl = new StringBuilder(128);
            const string DEAR_NAME = "{DEAR_NAME}", DOC_SUBJECT = "{DOC_SUBJECT}", COMMENT = "{COMMENT}", DOC_HREF = "{DOC_HREF}";
            try
            {
                if (list.Count <= 0)
                {
                    return "LIST_NO_COUNT";
                }
                SmtpClient SmtpServer = Bayer.Ultra.Framework.Common.SmtpManager.CreateSmtpClientObj();

                MailMessage mail = null;
                using (Bayer.Ultra.BSL.Common.Dao.CommonDao dao = new Bayer.Ultra.BSL.Common.Dao.CommonDao())
                {
                    SendmailToAddressListDto firstItem = list.FirstOrDefault();
                    format.ApproverList = string.Empty;
                    format.ApproverList = String.Join(",", list.Select(t => t.APPROVER_ID).ToList());

                    DearName = firstItem.APPROVER_NAME;
                    string eventUrl = string.Format(@"{0}{1}?processid={2}&eventid={3}&title={4}", documentUri, firstItem.WEB_PAGE_NAME, processId, firstItem.EVENT_ID, firstItem.EVENT_NAME);
                    sbUrl.AppendFormat("http://{0}/Ultra/Pages/Authentication/Gate/GatewayAuth.aspx?ReturnURL=" , Bayer.Ultra.Framework.Config.WebSiteConfigHandler.Login.Domain);
                    sbUrl.AppendFormat(System.Web.HttpUtility.UrlEncode("/ultra/Pages/Main.aspx?maillink=Y&tabpage=" + System.Web.HttpUtility.UrlEncode(eventUrl)));
         
                   //sbUrl.AppendFormat("http://{0}/ultra/Pages/Main.aspx?maillink=Y", Bayer.Ultra.Framework.Config.WebSiteConfigHandler.Login.Domain);
                   //sbUrl.Append("&tabpage=");
                   //sbUrl.Append(System.Web.HttpUtility.UrlEncode(string.Format(@"{0}{1}?processid={2}&eventid={3}&title={4}", documentUri, firstItem.WEB_PAGE_NAME, processId, firstItem.EVENT_ID, firstItem.EVENT_NAME)));
                   mail = new MailMessage();
                    mail.Subject = format.Subject.Replace(DOC_SUBJECT, string.Format("{0} : {1}", firstItem.EVENT_NAME, firstItem.SUBJECT));

                    mail.From = new MailAddress(senderAddress);

                    // To : Requester Or Aprover
                    mail.To.Add(String.Join(",", list.Select(x => x.MAIL_ADDRESS).ToList())); //
                     
                    body = body.Replace(DEAR_NAME, DearName);
                    body = body.Replace(DOC_HREF, sbUrl.ToString());

                    if (sendMailType.Equals("CurrentApprover"))
                    {
                        sbComment.AppendFormat("Requester : {0}<br/>", firstItem.REQUEST_NAME);
                        sbComment.AppendFormat("Document Name : {0}<br/>", firstItem.EVENT_NAME);
                        sbComment.AppendFormat("Request Date  : {0}<br/>", firstItem.REQUEST_DATE.ToString("yyyy-MM-dd HH:mm"));

                        //만약 Rejected ProcessI가 존재하면 본문 하단에 description을 추가한다. 
                        /*
                        if (firstItem.REJECTED_PROCESS_ID.DefaultIfEmpty().ToString().Trim().Length > 0)
                        {  
                            string history = "History:<br/>This document was rejected with below comment by {0}<br/>Comment:{1}"; 
                            RejectDocumentMailCommentDto dto = dao.SelectProcessRejectUser(processId);
                            history = string.Format(history, dto.REJECTER, dto.COMMENT);
                            sbComment.Append(history);
                        }*/
                        body = body.Replace(COMMENT, sbComment.ToString());
                    }
                    else if (sendMailType.Equals("FinalApproval"))
                    {
                        // 수신자 변경 => To : Requester, Cc : Reviewer, Recipient 
                        mail.To.Clear();
                        mail.CC.Clear();
                        mail.To.Add(String.Join(",", list.FindAll(x => x.APPROVAL_TYPE != "R").Select(x => x.MAIL_ADDRESS).ToList()));
                        if (list.FindAll(x => x.APPROVAL_TYPE == "R").Count > 0)
                        {
                            mail.CC.Add(String.Join(",", list.FindAll(x => x.APPROVAL_TYPE == "R").Select(x => x.MAIL_ADDRESS).ToList()));
                        }

                        if (!(firstItem.APPROVAL_TYPE.Equals("D") || firstItem.APPROVAL_TYPE.Equals("A")))  //Recipient Or Reviewer
                        {
                            MailFormat recipientFormat = GetMailFormat("FinalApprovalRecipient");
                            body = recipientFormat.Body.Replace(DEAR_NAME, DearName);
                            body = body.Replace(DOC_HREF, sbUrl.ToString());
                        }
                    }
                    else if (sendMailType.Equals("Reject"))
                    {
                        sbComment.Append(firstItem.COMMENT);
                        body = body.Replace(COMMENT, sbComment.ToString());
                    }
                    else if (sendMailType.Equals("Withdraw"))
                    {
                        // 수신자 변경 => To : Requester, Cc : 결제자, Reviewer
                        mail.To.Clear();
                        mail.CC.Clear();
                        mail.To.Add(String.Join(",", list.FindAll(x => x.APPROVAL_TYPE == "D").Select(x => x.MAIL_ADDRESS).ToList()));
                        if (list.FindAll(x => x.APPROVAL_TYPE != "D").Count > 0)
                        {
                            mail.CC.Add(String.Join(",", list.FindAll(x => x.APPROVAL_TYPE != "D").Select(x => x.MAIL_ADDRESS).ToList()));
                        }

                        sbComment.Append(firstItem.COMMENT);
                        body = body.Replace(COMMENT, sbComment.ToString());
                    }
                    else if (sendMailType.Equals("Delegation"))
                    {
                        // 수신자 변경 => To : Requester, Cc : 결제자, Reviewer
                        mail.To.Clear();
                        mail.CC.Clear();
                        mail.To.Add(String.Join(",", list.FindAll(x => x.APPROVAL_TYPE == "R").Select(x => x.MAIL_ADDRESS).ToList())); // Delegater list
                        if (list.FindAll(x => x.APPROVAL_TYPE == "D").Count > 0)
                        {
                            mail.CC.Add(String.Join(",", list.FindAll(x => x.APPROVAL_TYPE == "D").Select(x => x.MAIL_ADDRESS).ToList())); // Requester
                        }
                    }
                    else if (sendMailType.Equals("InputComment"))
                    {
                        string inputComment = dao.SelectInputComment(processId);
                        sbComment.Append(inputComment);
                        body = body.Replace(COMMENT, sbComment.ToString());
                    }

                    // 메일 발송 테스트 계정 추후 제거 시작 
                    {
                        mail.To.Clear();
                        mail.CC.Clear();
                        mail.To.Add("wookyung.kim@bayer.com,loki_park@naver.com"); //
                    }
                    // 메일 발송 테스트 계정 추후 제거 끝
                    mail.To.Clear();
                    mail.CC.Clear();
                    mail.To.Add("wookyung.kim@bayer.com,youngwoo.lee@bayer.com");
                    
                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    SmtpServer.Send(mail);
                }
            }
            catch (Exception ex)
            {
                WriteLog(ex.Message);
                retValue = ex.Message;
                throw ex;
            }
            finally
            {
                if (sbComment != null)
                {
                    sbComment.Clear();
                    sbComment = null;
                }
                if (sbUrl != null)
                {
                    sbUrl.Clear();
                    sbUrl = null;
                }
            }

            return retValue;
        }

        public MailFormat GetMailFormat(string sendMailType)
        {
            System.Xml.XmlDocument doc;
            System.Xml.XmlElement root;
            System.Xml.XmlNode n;

            string mailFormatPath = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.SmtpManager.MailFormat;
            MailFormat format = new MailFormat();
         //   mailFormatPath = "C:\Temp\Ultra\10.Web\ultra\Config\MailFormat.xml";
            try
            {
                doc = new System.Xml.XmlDocument();

                doc.Load(mailFormatPath);
                root = doc.DocumentElement;
                n = root.SelectSingleNode(string.Format("//Items/Item[@MailSendType='{0}']", sendMailType));

                format.Subject = n.SelectSingleNode("Subject").InnerText;
                format.Body = n.SelectSingleNode("Body").InnerText;
                format.DocumentUrl = @"{0}/{1}?processid={2}&documentid={3}";
                return format;
            }
            catch
            {
                throw;
            }
        }




        public void MergeMedicalSociety(DTO_COMMON_MEDICAL_SOCIETY society)
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    dao.MergeMedicalSociety(society);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void DeleteMedicalSociety(int id)
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    dao.DeleteMedicalSociety(id);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void MeageSendMailQueue(DTO_SENDMAIL dto)
        {
            try
            {
                using (Dao.CommonDao dao = new Dao.CommonDao())
                {
                    dao.MeageSendMailQueue(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
