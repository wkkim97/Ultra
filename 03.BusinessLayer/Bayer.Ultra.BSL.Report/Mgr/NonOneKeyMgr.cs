using Bayer.Ultra.Framework.Common;
using Bayer.Ultra.Framework.Common.Dto.Radiology;
using Bayer.Ultra.Framework.Common.Dto.Common;
using Bayer.Ultra.Framework.Config;
using System.Net.Mail;
using HiQPdf;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Web;

namespace Bayer.Ultra.BSL.Report.Mgr
{
    public class NonOneKeyMgr : Framework.Database.MgrBase
    {
        //List 조회
        public List<AssignedNonOneKeyListDto> SelectAssignedNonOneKeyList(string user_id, string user_type)
        {
            try
            {
                using (Dao.NonOneKeyDao dao = new Dao.NonOneKeyDao())
                {
                    return dao.SelectAssignedNonOneKeyList(user_id, user_type);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //검색
        public List<CustomerListDto> SelectCustomerList(string customer_type, string customer_name)
        {
            try
            {
                using (Dao.NonOneKeyDao dao = new Dao.NonOneKeyDao())
                {
                    return dao.SelectCustomerList(customer_type, customer_name);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int MergeCustomerData(MergeCustomerDto customerDto)
        {
            try
            {
                int NON_ONEKEY_ID = 0;
                using (Dao.NonOneKeyDao dao = new Dao.NonOneKeyDao())
                {
                    NON_ONEKEY_ID = dao.MergeCustomerData(customerDto);
                    
                }
                return NON_ONEKEY_ID;
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
                using (Dao.NonOneKeyDao dao = new Dao.NonOneKeyDao())
                {
                    return dao.SelectNonOneKeyHospitalList(keyword);
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
                using (Dao.NonOneKeyDao dao = new Dao.NonOneKeyDao())
                {
                    foreach (NonOnekeyIdDto idDto in dto.NON_ONEKEY_ID)
                    {                        
                        dao.InsertLog(idDto.NON_ONEKEY_ID, dto.REGISTER_ID, dto.LOG_TYPE, dto.LOG_CATEGORY, dto.COMMENT);
                    }
                }
                return "ok";
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
                using (Dao.NonOneKeyDao dao = new Dao.NonOneKeyDao())
                {
                    return dao.SelectLog(NON_ONEKEY_ID);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string MergeAttach(List<NonOnekeyAttachDto> mergeFiles)
        {
            try
            {
                using (TransactionScope scope = new TransactionScope())
                {
                    if (mergeFiles != null && mergeFiles.Count > 0) //업데이트 할 첨부파일이 존재하면
                    {
                        foreach (NonOnekeyAttachDto attachFile in mergeFiles)
                        { 
                            using (Dao.NonOneKeyDao dao = new Dao.NonOneKeyDao())
                            {
                                dao.InsertNonOnekeyAttachFiles(new NonOnekeyAttachDto()
                                {
                                    NON_ONEKEY_ID = attachFile.NON_ONEKEY_ID,
                                    ATTACH_FILE_TYPE = attachFile.ATTACH_FILE_TYPE,
                                    SEQ = attachFile.SEQ,
                                    DISPLAY_FILE_NAME = HttpUtility.UrlDecode(attachFile.DISPLAY_FILE_NAME),
                                    SAVED_FILE_NAME = attachFile.SAVED_FILE_NAME,
                                    FILE_SIZE = (int)attachFile.FILE_SIZE,
                                    FILE_PATH = HttpUtility.UrlDecode(attachFile.FILE_PATH),
                                    FILE_HANDLER_URL = attachFile.FILE_HANDLER_URL,
                                    REFER_IDX = attachFile.REFER_IDX,
                                    CREATOR_ID = attachFile.CREATOR_ID,
                                    IS_DELETED = "N"
                                });
                            }
                        }
                    }
                    
                    scope.Complete();
                    return "ok";

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public int InsertAttach(NonOnekeyAttachDto dto)
        {
            try
            {
                using (Dao.NonOneKeyDao dao = new Dao.NonOneKeyDao())
                {
                    return dao.InsertNonOnekeyAttachFiles(dto);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void DeleteAttach(int IDX, string UPDATER_ID)
        {
            try
            {
                using (Dao.NonOneKeyDao dao = new Dao.NonOneKeyDao())
                {
                    dao.DeleteNonOnekeyAttachFiles(IDX, UPDATER_ID);
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
                using (Dao.NonOneKeyDao dao = new Dao.NonOneKeyDao())
                {
                    return dao.SelectNonOnekeyAttachFile(NON_ONEKEY_ID, IDXS);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string MailSend(List<NonOnekeyIdDto> NON_ONEKEY_IDs, string sendMailType, string NON_ONEKEY_STATUS, string fromAddress, string toAddress, string CC)
        {
            string retValue;
            StringBuilder sbUrl = new StringBuilder(128);
            const string STATUS = "{STATUS}", DOC_HREF = "{DOC_HREF}", COMMENT = "{COMMENT}", DEAR_NAME = "{DEAR_NAME}";
            MailFormat format;
            using (BSL.Common.Mgr.CommonMgr_Tx mgr = new BSL.Common.Mgr.CommonMgr_Tx())
            {
                format = mgr.GetMailFormat(sendMailType);
            }

            try
            {
                SmtpClient SmtpServer = SmtpManager.CreateSmtpClientObj();
                MailMessage mail = null;

                //IQVIA 발송 메일 작성
                if (NON_ONEKEY_STATUS == "REGISTERING")
                {
                    string body = format.Body;
                    mail = new MailMessage();
                    AssignedNonOneKeyListDto nonOnekeyData;
                    int i = 0;
                    //Table 생성
                    string tableStr = "<table style='border: 1px solid black;border-collapse:collapse;witdh:100%'>";
                    tableStr += "<tr><th style='border: 1px solid black;border-collapse:collapse;padding:5px;'> No. </th>";
                    tableStr += "<th style='border: 1px solid black;border-collapse:collapse;padding:5px;'> Request Type </th>";
                    tableStr += "<th style='border: 1px solid black;border-collapse:collapse;padding:5px;'> Customer Type </th>";
                    tableStr += "<th style='border: 1px solid black;border-collapse:collapse;padding:5px;'> Name </th>";
                    tableStr += "<th style='border: 1px solid black;border-collapse:collapse;padding:5px;'> Gender </th>";
                    tableStr += "<th style='border: 1px solid black;border-collapse:collapse;padding:5px;'> Organization Name </th>";
                    tableStr += "<th style='border: 1px solid black;border-collapse:collapse;padding:5px;'> Organization ID </th>";
                    tableStr += "<th style='border: 1px solid black;border-collapse:collapse;padding:5px;'> Individual ID </th>";
                    tableStr += "<th style='border: 1px solid black;border-collapse:collapse;padding:5px;'> Remark </th>";
                    tableStr += "<th style='border: 1px solid black;border-collapse:collapse;padding:5px;'> Comment </th></tr>";

                    foreach (NonOnekeyIdDto idDto in NON_ONEKEY_IDs)
                    {
                        using (Dao.NonOneKeyDao dao = new Dao.NonOneKeyDao())
                        {
                            string IND_ID = "";
                            i++;
                            nonOnekeyData = dao.SelectNonOneKeyData(idDto.NON_ONEKEY_ID);
                            if (nonOnekeyData.CUSTOMER_TYPE == "인턴") nonOnekeyData.CUSTOMER_TYPE = "의사";
                            if (nonOnekeyData.REMARK.IndexOf('|') >= 0)
                            {
                                string[] arr = nonOnekeyData.REMARK.Split('|');
                                IND_ID = arr[0];
                                nonOnekeyData.REMARK = arr[1];
                            }
                            tableStr += "<tr><td style='border: 1px solid black;border-collapse:collapse;padding:5px;'> "+ i + " </td>";
                            tableStr += "<td style='border: 1px solid black;border-collapse:collapse;padding:5px;'> " + nonOnekeyData.REQUEST_TYPE + " </td>";
                            tableStr += "<td style='border: 1px solid black;border-collapse:collapse;padding:5px;'> " + nonOnekeyData.CUSTOMER_TYPE + " </td>";
                            tableStr += "<td style='border: 1px solid black;border-collapse:collapse;padding:5px;'> " + nonOnekeyData.CUSTOMER_NAME + " </td>";
                            tableStr += "<td style='border: 1px solid black;border-collapse:collapse;padding:5px;'> " + nonOnekeyData.GENDER + " </td>";
                            tableStr += "<td style='border: 1px solid black;border-collapse:collapse;padding:5px;'> " + nonOnekeyData.ORGANIZATION_NAME + " </td>";
                            tableStr += "<td style='border: 1px solid black;border-collapse:collapse;padding:5px;'> " + nonOnekeyData.ORGANIZATION_ID + " </td>";
                            tableStr += "<td style='border: 1px solid black;border-collapse:collapse;padding:5px;'> " + IND_ID + " </td>";
                            tableStr += "<td style='border: 1px solid black;border-collapse:collapse;padding:5px;'> " + nonOnekeyData.REMARK.Replace("\n","<br>") + " </td>";
                            tableStr += "<td style='border: 1px solid black;border-collapse:collapse;padding:5px;'></td></tr>";
                        }
                    }
                    tableStr += "</table>";
                    mail.Subject = format.Subject.Replace(STATUS, "업로드 요청");
                    mail.From = new MailAddress(fromAddress);
                    mail.To.Clear();
                    mail.CC.Clear();
                    mail.To.Add(toAddress.Replace("|", ","));
                    if(CC != "") mail.CC.Add(CC.Replace("|", ","));

                    //테스트 start
                    //mail.To.Clear();
                    //mail.To.Add("wookyung.kim@bayer.com,youngwoo.lee@bayer.com,bumyoung.kim@bayer.com");
                    //테스트 end

                    body = body.Replace(COMMENT, tableStr);
                    body = body.Replace(DEAR_NAME, "IQVIA team");

                    mail.Body = body;
                    mail.IsBodyHtml = true;
                    SmtpServer.Send(mail);
                }
                else
                {

                    foreach (NonOnekeyIdDto idDto in NON_ONEKEY_IDs) {
                        string body = format.Body;
                        mail = new MailMessage();
                        AssignedNonOneKeyListDto nonOnekeyData;
                        string sender = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.SmtpManager.Sender;
                        string documentUri = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.SmtpManager.EventUrl.Replace("Event", "Report");
                        string reportUrl = string.Format(@"{0}{1}?processid={2}&eventid={3}&title={4}", documentUri, "NonOneKey.aspx", "", "", "Non Onekey");

                        mail.From = new MailAddress(sender);
                        mail.To.Clear();
                        mail.CC.Clear();
                        mail.To.Add(toAddress.Replace("|", ","));

                        //등록 요청 메일 작성
                        if (NON_ONEKEY_STATUS == "REQUESTING")
                        {
                            string[] requesterNameArr = fromAddress.Split('@')[0].Split('.');
                            string comment = "Please find enclosed a link to a document in <strong>UlTra</strong>";
                            comment += " and be so kind to verify(Approval or Reject) it as quickly as possible.<br><br>";
                            comment += "Requester : " + Capitalize(requesterNameArr[0]) + ' ' + Capitalize(requesterNameArr[1]);
                            comment += "<br>";
                            comment += "Status : " + Capitalize(NON_ONEKEY_STATUS);

                            mail.Subject = format.Subject.Replace(STATUS, "등록 요청");
                            reportUrl = reportUrl.Replace("processid=", "processid=requesting");
                            body = body.Replace(COMMENT, comment);
                            body = body.Replace(DEAR_NAME, "Ultra Non-Onekey Administrator");
                        }
                        //반려 확인 메일 작성
                        else if (NON_ONEKEY_STATUS == "REJECTED")
                        {
                            List<SelectLogDto> logsData;
                            string logComment = "";

                            using (Dao.NonOneKeyDao dao = new Dao.NonOneKeyDao())
                            {
                                nonOnekeyData = dao.SelectNonOneKeyData(idDto.NON_ONEKEY_ID);
                                logsData = dao.SelectLog(idDto.NON_ONEKEY_ID.ToString());
                            }

                            foreach(SelectLogDto logData in logsData)
                            {
                                if (logData.LOG_CATEGORY == "REJECTED")
                                {
                                    logComment = logData.COMMENT;
                                    break;
                                }
                            }

                            string dearName = nonOnekeyData.FULL_NAME;
                            string comment = "아래의 링크를 통해 <strong>UlTra</strong>시스템에서 관련 문서를 확인해 주세요.<br><br>";
                            comment += " 등록 요청하신 정보가 관리자의 검토 결과 반려되었습니다.<br><br> ";
                            comment += "Customer Name : " + nonOnekeyData.CUSTOMER_NAME;
                            comment += "<br>";
                            comment += "Organization : " + nonOnekeyData.ORGANIZATION_NAME;
                            comment += "<br>";
                            comment += "Request Date : " + nonOnekeyData.CREATE_DATE.ToString("yyyy-MM-dd HH:mm:ss");
                            comment += "<br>";
                            comment += "Status : " + Capitalize(nonOnekeyData.NON_ONEKEY_STATUS);
                            comment += "<br>";
                            comment += "Comment : " + logComment;

                            mail.Subject = format.Subject.Replace(STATUS, "반려");
                            reportUrl = reportUrl.Replace("processid=", "processid=rejected");
                            body = body.Replace(COMMENT, comment);
                            body = body.Replace(DEAR_NAME, dearName);
                            mail.To.Clear();
                            mail.To.Add(nonOnekeyData.MAIL_ADDRESS);
                        }
                        //재등록 확인 요청 메일 작성
                        else if (NON_ONEKEY_STATUS == "RE-REQUESTING")
                        {
                            using (Dao.NonOneKeyDao dao = new Dao.NonOneKeyDao())
                            {
                                nonOnekeyData = dao.SelectNonOneKeyData(idDto.NON_ONEKEY_ID);
                            }

                            string comment = "Please find enclosed a link to a document in <strong>UlTra</strong>";
                            comment += " and be so kind to verify(Approval or Reject) it as quickly as possible.<br><br>";
                            comment += "Requester : " + nonOnekeyData.FULL_NAME;
                            comment += "<br>";
                            comment += "Customer Name : " + nonOnekeyData.CUSTOMER_NAME;
                            comment += "<br>";
                            comment += "Organization : " + nonOnekeyData.ORGANIZATION_NAME;
                            comment += "<br>";
                            comment += "Request Date : " + nonOnekeyData.CREATE_DATE.ToString("yyyy-MM-dd HH:mm:ss");
                            comment += "<br>";
                            comment += "Status : Re-" + Capitalize(nonOnekeyData.NON_ONEKEY_STATUS);

                            mail.Subject = format.Subject.Replace(STATUS, "등록 요청");
                            reportUrl = reportUrl.Replace("processid=", "processid=requesting");
                            body = body.Replace(COMMENT, comment);
                            body = body.Replace(DEAR_NAME, "Ultra Non-Onekey Administrator");
                        }
                        //완료 메일 발송
                        else if (NON_ONEKEY_STATUS == "COMPLETE")
                        {
                            using (Dao.NonOneKeyDao dao = new Dao.NonOneKeyDao())
                            {
                                nonOnekeyData = dao.SelectNonOneKeyData(idDto.NON_ONEKEY_ID);
                            }

                            string dearName = nonOnekeyData.FULL_NAME;
                            string comment = "입력하신 Non-onekey Data의 등록이 완료되었습니다.";
                            comment += "<br><br>";
                            comment += "Customer Name : " + nonOnekeyData.CUSTOMER_NAME;
                            comment += "<br>";
                            comment += "Organization : " + nonOnekeyData.ORGANIZATION_NAME;
                            comment += "<br>";
                            comment += "Request Date : " + nonOnekeyData.CREATE_DATE.ToString("yyyy-MM-dd HH:mm:ss");
                            comment += "<br>";
                            comment += "Status : " + Capitalize(nonOnekeyData.NON_ONEKEY_STATUS);

                            mail.Subject = format.Subject.Replace(STATUS, "등록 완료");
                            reportUrl = reportUrl.Replace("processid=", "processid=complete");
                            body = body.Replace(COMMENT, comment);
                            body = body.Replace(DEAR_NAME, dearName);
                            mail.To.Clear();
                            mail.To.Add(nonOnekeyData.MAIL_ADDRESS);
                        }

                        sbUrl.AppendFormat("http://{0}/ultra/pages/authentication/logon.aspx?ReturnURL=", Bayer.Ultra.Framework.Config.WebSiteConfigHandler.Login.Domain);
                        sbUrl.AppendFormat(System.Web.HttpUtility.UrlEncode("/ultra/Pages/Main.aspx?maillink=Y&tabpage=" + System.Web.HttpUtility.UrlEncode(reportUrl)));

                        body = body.Replace(DOC_HREF, sbUrl.ToString());

                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        SmtpServer.Send(mail);
                    }
                }

                retValue = "OK";
            }
            catch (Exception ex)
            {
                retValue = ex.Message;
                throw ex;
            }
            finally
            {
                if (format != null)
                {
                    format = null;
                }
                if (sbUrl != null)
                {
                    sbUrl.Clear();
                    sbUrl = null;
                }
            }
            return retValue;
        }        
        private string Capitalize(string word)
        {
            return word.Substring(0, 1).ToUpper() + word.Substring(1).ToLower();
        }
    }
}
