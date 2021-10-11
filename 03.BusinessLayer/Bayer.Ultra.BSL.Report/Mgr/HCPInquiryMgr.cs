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
    public class HCPInquiryMgr : Framework.Database.MgrBase
    {
        //List 조회
        //검색
        public List<HCPInquiryCustomerDto> SelectInquiryCustomerList(string name, string org, string specialty)
        {
            try
            {
                using (Dao.HCPInquiryDao dao = new Dao.HCPInquiryDao())
                {
                    return dao.SelectInquiryCustomerList(name, org, specialty);
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
                int HCP_INQUIRY_REQUEST_ID = 0;
                using (Dao.HCPInquiryDao dao = new Dao.HCPInquiryDao())
                {
                    HCP_INQUIRY_REQUEST_ID = dao.MergeCustomerRequest(customerDto);

                }
                if (HCP_INQUIRY_REQUEST_ID != 0) return "Ok";
                else return "Fail";
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
                using (Dao.HCPInquiryDao dao = new Dao.HCPInquiryDao())
                {
                    return dao.SelectHCPInquiryList(USER_ID, USER_TYPE);
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
                using (Dao.HCPInquiryDao dao = new Dao.HCPInquiryDao())
                {
                    return dao.SelectHCPInquiryLog(HCP_INQUIRY_REQUEST_ID);
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
                using (Dao.HCPInquiryDao dao = new Dao.HCPInquiryDao())
                {
                    foreach (HCPInquiryIdDto idDto in dto.IDs)
                    {
                        dao.InsertHCPInquiryLog(idDto.HCP_INQUIRY_REQUEST_ID, dto.REGISTER_ID, dto.LOG_TYPE, dto.LOG_CATEGORY, dto.COMMENT);
                    }
                }
                return "ok";
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string SendHCPInquiryMail(List<HCPInquiryIdDto> IDs, string sendMailType, string status, string fromAddress, string cc)
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

                foreach (HCPInquiryIdDto idDto in IDs) {
                        string body = format.Body;
                        mail = new MailMessage();
                        HCPInquiryListDto dto;
                        string sender = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.SmtpManager.Sender;
                        string documentUri = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.SmtpManager.EventUrl.Replace("Event", "Report");
                        string reportUrl = string.Format(@"{0}{1}?processid={2}&eventid={3}&title={4}", documentUri, "HCPInquiry.aspx", "", "", "HCP Inquiry");

                        mail.From = new MailAddress(sender);
                        mail.To.Clear();
                        mail.CC.Clear();
                        mail.To.Add(idDto.MAIL_ADDRESS.Replace("|", ","));

                        //등록 요청 메일 작성
                        if (status == "REQUESTING")
                        {
                            string[] requesterNameArr = fromAddress.Split('@')[0].Split('.');
                            string comment = "Please find enclosed a link to a document in <strong>UlTra - HCP document request</strong>";
                            comment += " and be so kind to confirm it as quickly as possible.<br><br>";
                            comment += "Requester : " + Capitalize(requesterNameArr[0]) + ' ' + Capitalize(requesterNameArr[1]);
                            comment += "<br>";
                            comment += "Status : " + Capitalize(status);

                            mail.Subject = format.Subject.Replace(STATUS, "요청중");
                            reportUrl = reportUrl.Replace("processid=", "processid=requesting");
                            body = body.Replace(COMMENT, comment);
                            body = body.Replace(DEAR_NAME, "Ultra HCP-document Administrator");
                        }
                        //확인 메일 작성
                        else if (status == "PROCESSING")
                        {
                            using (Dao.HCPInquiryDao dao = new Dao.HCPInquiryDao())
                            {
                                dto = dao.SelectHCPInquiryData(idDto.HCP_INQUIRY_REQUEST_ID);
                            }
                            string dearName = dto.FULL_NAME;
                            string comment = "아래의 링크를 통해 <strong>UlTra</strong>시스템에서 관련 문서를 확인할 수 있습니다.<br><br>";
                            comment += " 요청하신 정보로 관리자가 자료를 생성하고 있습니다.<br><br> ";
                            comment += "Customer Name : " + dto.CUSTOMER_NAME;
                            comment += "<br>";
                            comment += "Organization : " + dto.ORGANIZATION_NAME;
                            comment += "<br>";
                            comment += "Request Date : " + dto.CREATE_DATE.ToString("yyyy-MM-dd HH:mm:ss");
                            comment += "<br>";
                            comment += "Status : " + Capitalize(dto.INQUIRY_STATUS);

                            mail.Subject = format.Subject.Replace(STATUS, "처리중");
                            reportUrl = reportUrl.Replace("processid=", "processid=processing");
                            body = body.Replace(COMMENT, comment);
                            body = body.Replace(DEAR_NAME, dearName);
                            mail.To.Clear();
                            mail.To.Add(dto.MAIL_ADDRESS);
                        }
                        //완료 메일 발송
                        else if (status == "COMPLETE")
                        {
                            using (Dao.HCPInquiryDao dao = new Dao.HCPInquiryDao())
                            {
                                dto = dao.SelectHCPInquiryData(idDto.HCP_INQUIRY_REQUEST_ID);
                            }

                            string dearName = dto.FULL_NAME;
                            string comment = "요청하신 HCP document 발송이 완료되었습니다.";
                            comment += "<br><br>";
                            comment += "Customer Name : " + dto.CUSTOMER_NAME;
                            comment += "<br>";
                            comment += "Organization : " + dto.ORGANIZATION_NAME;
                            comment += "<br>";
                            comment += "Request Date : " + dto.CREATE_DATE.ToString("yyyy-MM-dd HH:mm:ss");
                            comment += "<br>";
                            comment += "Status : " + Capitalize(dto.INQUIRY_STATUS);

                            mail.Subject = format.Subject.Replace(STATUS, "발송 완료");
                            reportUrl = reportUrl.Replace("processid=", "processid=complete");
                            body = body.Replace(COMMENT, comment);
                            body = body.Replace(DEAR_NAME, dearName);
                            mail.To.Clear();
                            mail.To.Add(dto.MAIL_ADDRESS);
                        }

                        sbUrl.AppendFormat("http://{0}/ultra/pages/authentication/logon.aspx?ReturnURL=", Bayer.Ultra.Framework.Config.WebSiteConfigHandler.Login.Domain);
                        sbUrl.AppendFormat(System.Web.HttpUtility.UrlEncode("/ultra/Pages/Main.aspx?maillink=Y&tabpage=" + System.Web.HttpUtility.UrlEncode(reportUrl)));

                        body = body.Replace(DOC_HREF, sbUrl.ToString());

                        mail.Body = body;
                        mail.IsBodyHtml = true;
                        SmtpServer.Send(mail);
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
