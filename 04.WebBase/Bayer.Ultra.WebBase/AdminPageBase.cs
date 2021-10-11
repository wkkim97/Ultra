using Bayer.Ultra.Framework;
using Bayer.Ultra.Framework.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bayer.Ultra.WebBase
{
    public class AdminPageBase : PageBase
    {
        Dictionary<string, List<string>> _AdminAuth = new Dictionary<string, List<string>>();

        public AdminPageBase()
        {
            InitAdminAuth();
        }

        private void InitAdminAuth()
        {
            _AdminAuth.Clear();
            List<string> ultraUser = new List<string>()
            {
                
                "ConcurHistory.aspx",
                "AddParticipants.aspx",
                "ExcelUpload.aspx",
                "MedicalSociety.aspx",
                "HcpSearch.aspx"

            };
            List<string> systemAdmin = new List<string>()
            {
                //Ultra Team
                "ExcelUpload.aspx",
                "MedicalSociety.aspx",
                "ConcurHistory.aspx",
                "AddParticipants.aspx",
                "HcpSearch.aspx"
            };
            List<string> systemDesinger = new List<string>()
            {
                "ConcurHistory.aspx",
                "ExcelUpload.aspx",
                "MedicalSociety.aspx",
                "EventConfiguration.aspx",
                "AddParticipants.aspx",
                "ICCMaster.aspx",
                "HcpSearch.aspx"
            };

            List<string> lpcUser = new List<string>()
            {
                "MedicalSociety.aspx",
                "ICCMaster.aspx",
                "HcpSearch.aspx"

            };

            //Micromarketing function
            List<string> radkeyUser = new List<string>()
            {
                "RAD_Micro_Marketing.aspx",

            };
            _AdminAuth.Add(Framework.Common.ApprovalUtil.SECURITY_GROUP.LPC_USER, lpcUser);
            _AdminAuth.Add(Framework.Common.ApprovalUtil.SECURITY_GROUP.SUPPORT_USER, ultraUser);
            _AdminAuth.Add(Framework.Common.ApprovalUtil.SECURITY_GROUP.SYSTEM_ADMIN, systemAdmin);
            _AdminAuth.Add(Framework.Common.ApprovalUtil.SECURITY_GROUP.SYSTEM_DESINGER, systemDesinger);
            //Micromarketing function
            _AdminAuth.Add(Framework.Common.ApprovalUtil.SECURITY_GROUP.RAD_KEY_USER, radkeyUser);
        }

        protected override void OnPreInit(EventArgs e)
        {
            if (System.Web.HttpContext.Current.Request.Cookies["ultraUserGroups"].IsNotNullOrEmptyEx() && Request.Cookies["ultraUserGroups"].Value.IsNotNullOrEmptyEx())    // 사용자 정보 Cookie 정보가 존재할경우
            {
                string securityGroups = System.Web.HttpContext.Current.Request.Cookies["ultraUserGroups"].Value;

                if (string.IsNullOrEmpty(securityGroups))
                {
                    string redirectUrl = WebSiteConfigHandler.UltraInfo.Pages.Error;
                    Response.Redirect(redirectUrl);
                }
                else
                {
                    string currentUrl = Request.Url.AbsolutePath;

                    bool certification = false;
                    foreach (string group in securityGroups.Split(new char[] { '|' }))
                    {   
                        if (_AdminAuth.ContainsKey(group))
                        {
                            var exist = _AdminAuth[group].Where(url => currentUrl.EndsWith(url)).FirstOrDefault();
                            if (exist != null)
                            {
                                certification = true;
                                break;
                            }
                        }
                    }
                    
                    if (!certification)
                    {
                        string redirectUrl = WebSiteConfigHandler.UltraInfo.Pages.Error;
                        Response.Redirect(redirectUrl);
                    }
                }

            }
            base.OnPreInit(e);
        }
    }
}
