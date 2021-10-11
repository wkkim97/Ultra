using Bayer.Ultra.Framework.Common.Dto.Common;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Agent.SendMailAgent
{
    public class SendMailMgr
    {
        private static SendMailMgr instance;

        private SendMailMgr() { }

        public static SendMailMgr Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new SendMailMgr();
                }
                return instance;
            }
        }

        private SmtpClient CreateSmtpClientObj()
        {
            string smtpServer = ConfigurationManager.AppSettings.GetValues("smtpServer").FirstOrDefault();
            SmtpClient client = new SmtpClient(smtpServer);
            client.UseDefaultCredentials = true;

            return client;

            ///////////////* 기본 인증으로 처리하기위해 주석처리하고 아래 코드로 변경  시작 *//////////////////
            //string sender = ConfigurationManager.AppSettings.GetValues("sender").FirstOrDefault();
            //string senderPs = Bayer.Ultra.Framework.EncryptionUtils.eWDecrypt(ConfigurationManager.AppSettings.GetValues("senderps").FirstOrDefault());
            //int port = 587;

            //SmtpClient SmtpServer = new SmtpClient(smtpServer);

            //SmtpServer.Port = port;
            //SmtpServer.Credentials = new System.Net.NetworkCredential(sender, senderPs);
            //SmtpServer.EnableSsl = true;
            ////SmtpServer.DeliveryMethod = SmtpDeliveryMethod.Network;
            ////SmtpServer.UseDefaultCredentials = false;
            //return SmtpServer;
            ////////////////////////////// 기본인증 주석 종료부분///////////////////////////////////////

        }

        public bool SendMail(string subject, string to, string body)
        {
            SmtpClient SmtpServer = CreateSmtpClientObj();
            MailMessage mail = null;
            bool retvalue = false;
            try
            {
                mail = new MailMessage();
                mail.Subject = subject;
                mail.From = new MailAddress(ConfigurationManager.AppSettings["sender"]);
                
                mail.To.Add(to);
                //테스트 계정
                //mail.To.Add("loki_park@naver.com");

                mail.Body = body;
           
                mail.IsBodyHtml = true;
                SmtpServer.Send(mail);
                retvalue = true;
            }
            catch(Exception ex) 
            {
                retvalue = false;
                throw ex;
            }

            return retvalue;
        }

        public bool SendMail(string subject, string from, string tolist, string cclist, string body, string filePath)
        {
            SmtpClient SmtpServer = CreateSmtpClientObj();
            MailMessage mail = null;
            bool retvalue = false;
            try
            {
                mail = new MailMessage();
                mail.Subject = subject;
                mail.From = new MailAddress(from);

                foreach (string to in tolist.Split(';'))
                {
                    if (to.Equals(string.Empty)) continue;
                    mail.To.Add(to);
                }
                foreach (string cc in cclist.Split(';'))
                {
                    if (cc.Equals(string.Empty)) continue;
                    mail.CC.Add(cc);
                }
                //mail.To.Add("leyou88@naver.com");

                mail.Body = body;
                mail.IsBodyHtml = true;

                Attachment attach = new Attachment(filePath);
                mail.Attachments.Add(attach);

                SmtpServer.Send(mail);
                retvalue = true;
            }
            catch (Exception ex)
            {
                retvalue = false;
                throw ex;
            }

            return retvalue;
        }


        public MailFormat GetMailFormat(string sendMailType)
        {
            System.Xml.XmlDocument doc;
            System.Xml.XmlElement root;
            System.Xml.XmlNode n;

            string mailFormatPath = ConfigurationManager.AppSettings["mailFormat"];
            MailFormat format = new MailFormat();
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
    }
}
