using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using Bayer.Ultra.Framework;
using System.IO;
using Bayer.Ultra.Framework.Common.Dto.Common;
using Newtonsoft.Json;
using System.Text;
using System.Reflection;

public partial class Pages_Authentication_Logon : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            if (Request["ReturnURL"] != null)
            {
                this.hddReturnURL.Value = Request["ReturnURL"];
            }
            if (Request.Cookies["ultraLogin"] != null)
            {
                string login = EncryptionUtils.eWDecrypt(Request.Cookies["ultraLogin"].Value);
                this.txtUserID.Value = login.Split('|')[0];
                //this.txtPassword.Value = login.Split('|')[1];
                hddExistsSaveID.Value = "true";
            }
            else
            {
                hddExistsSaveID.Value = "false";
            }

        }
        Session.Abandon();
    }

    protected void BtnLogin_ServerClick(object sender, EventArgs e)
    {
        bool localDevelop = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.LocalDevelop.Used;
        string userID = this.txtUserID.Value.Trim();
        string password = this.txtPassword.Value.Trim();
        
       
        string domainName = HttpContext.Current.Request.Url.GetLeftPart(UriPartial.Authority);
        try
        {
            string url = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.WcfServices.CommonService.Url;

            bool isCertified = false;
            localDevelop = true;
            if (localDevelop)
            {
                isCertified = true;
                List<string> adminRoles = new List<string>();
                adminRoles.AddRange(
                    new string[] {
                        Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.LPC_USER,
                        Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.MEDICAL_ADMIN,
                        Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.SUPPORT_USER,
                        Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.SYSTEM_ADMIN,
                        Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.SYSTEM_DESINGER,
                        Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.RAD_USER,
                        Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.RAD_KEY_USER,
                        Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.NON_ONEKEY_KEY_USER,
                        // version 1.0.7 HCP validation function for Easy On
                        Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.HCP_SEARCH_KEY_USER
                    });
                SetCookie("ultraUserGroups", string.Join("|", adminRoles), "");
            }
            else  //개발관련 작업이 아니면 AD에 로그인 체크
            {
                isCertified = CheckLogin(url, userID, password);
            }

            if (isCertified)
            {
                string elogin = EncryptionUtils.eWEncrypt(userID + "|" + password);
                SetCookie("ultraLogin", elogin, "");
                //사용자
                string strUserInfo = GetUserInfo(url, "ko-kr", userID);
                UserInfoDto userInfo = JsonConvert.DeserializeObject<UserInfoDto>(strUserInfo);
                if (userInfo.TRAINING_COMPLETED.ToLower().Equals("yes"))
                {
                    string encyptedUserInfo = EncryptionUtils.UserInfoEncrypt(strUserInfo);
                    // Login History 주석처리함.
                    //InsertLoginHistory(url, userID, userInfo.FULL_NAME, userInfo.MAIL_ADDRESS, userInfo.ORG_ACRONYM);

                    this.hddUserInfo.Value = encyptedUserInfo;


                    RedirectWithPost(encyptedUserInfo);
                }
                else
                {
                    this.hddCheckLoginMessage.Value = "트레이닝을 완료하셔야 합니다..";
                }
            }
            else
            {
                this.hddMessage.Value = "아이디 또는 비밀번호를 다시 확인하세요.";
            }
        }
        catch (Exception ex)
        {
            this.hddMessage.Value = ex.Message;
        }
    }

    #region SetCookie 지정한 쿠키를 설정
    /// <summary>
    /// 지정한 쿠키를 설정
    /// </summary>
    /// <param name="cookieName">쿠키명</param>
    /// <param name="cookieValue">쿠키값</param>
    /// <param name="domainName">도메인명</param>
    private void SetCookie(string cookieName, string cookieValue, string domainName)
    {
        HttpCookie oMyCookie = null;

        try
        {
            oMyCookie = new HttpCookie(cookieName);
            oMyCookie.Value = cookieValue;
            oMyCookie.Domain = domainName;
            oMyCookie.Path = "/";
            //oMyCookie.Expires = DateTime.Now.AddDays(1d);
            Response.Cookies.Add(oMyCookie);

        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    #endregion

    /// <summary>
    /// 로그인 체크
    /// </summary>
    /// <param name="url"></param>
    /// <param name="account"></param>
    /// <param name="password"></param>
    /// <returns></returns>
    private bool CheckLogin(string url, string account, string password)
    {
        string requestData = string.Format("\"{0}\":\"{1}\",\"{2}\":\"{3}\"", "account", account, "password", password);
        requestData = "{" + requestData + "}";
        string endPoint = string.Format("{0}/checkad", url);
        bool bAuth = false;
        List<string> groups = null;
        try
        {
            using (Bayer.Ultra.Framework.Common.Activedirectory.LdapQuery ldap = new Bayer.Ultra.Framework.Common.Activedirectory.LdapQuery())
            using (Bayer.Ultra.WebBase.RestClient rest = new Bayer.Ultra.WebBase.RestClient(endPoint, Bayer.Ultra.WebBase.HttpVerb.POST))
            {
                //var responseData = rest.MakeRequest(requestData); 
                bAuth = ldap.IsAuthentication(account, password);     // AD 인증 
                groups = ldap.GetMemberOf(account);

                
                if (bAuth)
                {
                    if (bAuth.ToString().ToLower().Equals("true"))
                    {
                        if (groups.Count > 0)
                        {
                            SetCookie("ultraUserGroups", string.Join("|", groups), "");
                        }
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
                else
                {
                    return false;
                }
            }

        }
        catch
        {
            throw;
        }
    }

    /// <summary>getuserinfo
    /// 사용자 정보 조회
    /// </summary>
    /// <param name="url"></param>
    /// <param name="language"></param>
    /// <param name="user"></param>
    private string GetUserInfo(string url, string language, string user)
    {
        string requestData = string.Format("\"{0}\":\"{1}\",\"{2}\":\"{3}\"", "language", language, "user", user);
        requestData = "{" + requestData + "}";
        string endPoint = string.Format("{0}/getuserinfo", url);
        try
        {
            using (Bayer.Ultra.WebBase.RestClient rest = new Bayer.Ultra.WebBase.RestClient(endPoint, Bayer.Ultra.WebBase.HttpVerb.POST))
            {
                return rest.MakeRequest(requestData);
            }
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// 로그인 History기록
    /// </summary>
    /// <param name="url"></param>
    /// <param name="userID"></param>
    /// <param name="fullName"></param>
    /// <param name="mailAddress"></param>
    /// <param name="orgAcronym"></param>
    private DTO_LOGIN_HISTORY InsertLoginHistory(string url, string userID, string fullName, string mailAddress, string orgAcronym)
    {
        string[] computer_name = null;
        string machineName = string.Empty, clientIP = string.Empty, loginUserName = string.Empty; // 현재 시스템에 로그인된 사용자의 이름
        DTO_LOGIN_HISTORY dtoLoginHistory = null;
        string endPoint = string.Format("{0}/insertloginhistory", url);
        try
        {
            computer_name = System.Net.Dns.GetHostEntry(HttpContext.Current.Request.ServerVariables["remote_addr"]).HostName.Split(new char[] { '.' });
            machineName = computer_name[0].ToString();
            clientIP = HttpContext.Current.Request.UserHostAddress.ToString();
            loginUserName = HttpContext.Current.Request.LogonUserIdentity.Name.ToString(); // 현재 시스템에 로그인된 사용자의 이름

            dtoLoginHistory = new DTO_LOGIN_HISTORY()
            {
                USER_ID = userID,
                CLIENTIP = clientIP,
                FULL_NAME = fullName,
                MAILADDRESS = mailAddress,
                ORG_ACRONYM = orgAcronym,
                CREATE_DATE = DateTime.Now,
                WINDOWUSERNAME = loginUserName,
                WINDOWDOMAINNAME = machineName
            };

            using (Bayer.Ultra.WebBase.RestClient rest = new Bayer.Ultra.WebBase.RestClient(endPoint, Bayer.Ultra.WebBase.HttpVerb.POST))
            {
                rest.MakeRequest(JsonConvert.SerializeObject(dtoLoginHistory));
            }
        }
        catch (Exception ex)
        {

        }

        return dtoLoginHistory;

    }

    private void RedirectWithPost(string postData)
    {
        try
        {
            Response.Clear();

            string returnUrl = this.hddReturnURL.Value;

            if (returnUrl.Equals(string.Empty))
                returnUrl = "/ultra/Pages/Main.aspx";

            StringBuilder sb = new StringBuilder();
            sb.Append("<html>");
            sb.AppendFormat(@"<body onload='document.forms[""form""].submit()'>");
            sb.AppendFormat("<form name='form' action='{0}' method='post'>", returnUrl);
            sb.AppendFormat("<input type='hidden' name='UserInfo' value='{0}'>", postData);
            sb.Append("</form>");
            sb.Append("</body>");
            sb.Append("</html>");

            Response.Write(sb.ToString());
        }
        catch
        {
        }
        finally
        {
            Response.End();
        }
    }
}