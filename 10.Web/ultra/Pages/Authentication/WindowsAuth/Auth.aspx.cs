using Bayer.Ultra.Framework;
using Bayer.Ultra.Framework.Common.Dto.Common;
using Bayer.Ultra.Framework.Config;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Authentication_WindowsAuth_Auth : System.Web.UI.Page
{

    //System.Web.UI.Page 에서 Bayer.Ultra.WebBase.PageBase 로변경 session 값을 가지고 와서 이미 있으면 다시 로그인 하지 않는다
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {

            string userID = GetWindowsUserID();
            string url = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.WcfServices.CommonService.Url;
            //userID = "";
            //if (this.Sessions == null) {
            //    userID = GetWindowsUserID();
            //}
            //else
            //{
            //    userID = Sessions.UserID; ;
            //}
            if (userID.Length > 0)
            {
                string strUserInfo = GetUserInfo(url, "ko-kr", userID);
                UserInfoDto userInfo = JsonConvert.DeserializeObject<UserInfoDto>(strUserInfo);
                string encyptedUserInfo = EncryptionUtils.UserInfoEncrypt(strUserInfo);
                SetCookie("ultrakey", encyptedUserInfo, "");
                RedirectWithPost(encyptedUserInfo);
            }
            else
            {
                string returnUrl = Request["ReturnURL"];
                Response.Redirect(string.Format("{0}?ReturnURL={1}", WebSiteConfigHandler.Login.URL, returnUrl));
            }


            //if( userID.Length > 0)
            //{
            //    string strUserInfo = GetUserInfo(url, "ko-kr", userID);
            //    UserInfoDto userInfo = JsonConvert.DeserializeObject<UserInfoDto>(strUserInfo);
            //    string encyptedUserInfo = EncryptionUtils.UserInfoEncrypt(strUserInfo);
            //    SetCookie("ultrakey", encyptedUserInfo, "");
            //    RedirectWithPost(encyptedUserInfo);
            //}
            //else
            //{
            //    string returnUrl = Request["ReturnURL"]; 
            //    Response.Redirect(string.Format("{0}?ReturnURL={1}", WebSiteConfigHandler.Login.URL, returnUrl));
            //}

        }
        catch (Exception ex)
        {
            LogManager.WriteLog(GetType(), MethodInfo.GetCurrentMethod().Name, null, ex.ToString(), "Windows Auth Error");
        }
        finally
        {
            Response.End();
        }
    }

    private string GetWindowsUserID()
    {
        string strFullId = Request.LogonUserIdentity.Name;


        //strFullId += "1" + System.Security.Principal.WindowsIdentity.GetCurrent().Name +"-";
        //strFullId += "2" + HttpContext.Current.User.Identity.Name+"-";
        //strFullId += "3" + Page.User.Identity.Name+"-";
        //strFullId += "4" + System.Security.Principal.WindowsIdentity.GetCurrent().Name+"-";
        //strFullId += "5" + Page.User.Identity.Name+"-";
        //strFullId += "6" + Request.LogonUserIdentity.Name+"-";
        //strFullId += "7" + Environment.UserName+"-";

        LogManager.WriteLog(GetType(), MethodInfo.GetCurrentMethod().Name, null, strFullId, "Windows Auth");
        //strFullId =  
        //strFullId = HttpContext.Current.Request.LogonUserIdentity.Name;
        string stUserID = string.Empty, strNetbiosDomain = string.Empty;
        string url = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.WcfServices.CommonService.Url;


        strFullId = Request.LogonUserIdentity.Name;
        //strFullId = strFullId.Replace("SGWVX", "sgwvx");
        if (strFullId.Contains("\\"))
        {
            strNetbiosDomain = strFullId.Split('\\')[0];
            stUserID = strFullId.Split('\\')[1];
        }
        else
        {
            strNetbiosDomain = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.ActiveDirectory.NetBIOS;
            stUserID = strFullId;
        }

        return stUserID;
    }

    /// <summary>
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

    private void RedirectWithPost(string postData)
    {
        try
        {
            Response.Clear();

            string returnUrl = Request["ReturnURL"];

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