using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.Framework.Common
{
    public class SmtpManager
    {
        public static SmtpClient CreateSmtpClientObj()
        {
           string smtpServer = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.SmtpManager.SmtpServer;
            SmtpClient client = new SmtpClient(smtpServer);
            client.UseDefaultCredentials = true;

            return client;

            ///////////////* 기본 인증으로 처리하기위해 주석처리하고 아래 코드로 변경  시작 *//////////////////
            //string sender = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.SmtpManager.Sender;
            //string senderPs = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.SmtpManager.Password;
            //int port = 587;

            //SmtpClient SmtpServer = new SmtpClient(smtpServer);

            //SmtpServer.Port = port;
            //SmtpServer.Credentials = new System.Net.NetworkCredential(sender, senderPs);
            //SmtpServer.EnableSsl = true;

            //return SmtpServer;
            ////////////////////////////// 기본인증 주석 종료부분///////////////////////////////////////


        }
    }
}
