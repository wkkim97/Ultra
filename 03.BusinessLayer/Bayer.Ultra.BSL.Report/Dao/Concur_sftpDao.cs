using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.BSL.Report.Dao
{
    class Concur_sftpDao : Framework.Database.DaoBase
    {
        public void trasfersFTP_Concur(string yyyymm)
        {
            try
            {
                if (yyyymm == "1")
                {

                }
                string source = @"C:\temp\VATBKL4.xlsx";
                string destination = @"users";
                string host = "10.190.195.68";
                string username = "concur@3FTP68";
                string password = "Newpass33##";
                int port = 2222;  //Port 22 is defaulted for SFTP upload

                sftp_transfer.UploadSFTPFile(host, username, password, source, destination, port);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
