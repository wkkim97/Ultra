using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bayer.Ultra.Framework;
using Newtonsoft.Json;
using Bayer.Ultra.Framework.Common.Dto.Common;
using System.Web.UI;
using Bayer.Ultra.Framework.Config;
using System.Reflection;
using System.Web;

namespace Bayer.Ultra.WebBase
{
    public class PageBase : System.Web.UI.Page
    {


        #region 멤버변수

        /// <summary>
        /// 사용자 세션클래스 
        /// </summary>
        public AccountInfo Sessions = null;

        /// <summary>
        /// 에러메시지
        /// </summary>
        protected string errorMessage = string.Empty;

        protected string visitLimitCount = "4";
        protected string hcpLimitAmount = "0";

        #endregion

        #region 생성자
        public PageBase()
        {
            InitConfiguration();
        }
        #endregion

        #region 설정파일 초기화

        private void InitConfiguration()
        {
            Framework.Config.Element.LogInElement login = Framework.Config.WebSiteConfigHandler.Login;

        }

        #endregion

        #region 공통이벤트 - 실행순서
        /// <summary>
        /// 페이지 PreInit
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreInit(EventArgs e)
        {
            String username_id = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
            string mailLink = this.Request["maillink"].NullObjectToEmptyEx();
            bool fromMail = mailLink.ToUpper().Equals("Y") ? true : false;
            UserInfoDto userInfo;
            string stUserID = string.Empty;
            // 최초 페이지 호출시
            if (!IsPostBack && Request["UserInfo"] != null)
            { 
                string jsonUserInfo = Request["UserInfo"].NullObjectToEmptyEx();
                SetCookie("ultrakey", jsonUserInfo, "");
                if (Framework.Config.WebSiteConfigHandler.Login.UseEncryption)
                    jsonUserInfo = EncryptionUtils.UserInfoDecrypt(jsonUserInfo);

                userInfo = JsonConvert.DeserializeObject<UserInfoDto>(jsonUserInfo);

                SetSession(userInfo); //세션클래스생성
                stUserID = userInfo.USER_ID;
            }
            else
            { 
                if (System.Web.HttpContext.Current.Request.Cookies["ultrakey"].IsNotNullOrEmptyEx() && Request.Cookies["ultrakey"].Value.IsNotNullOrEmptyEx())    // 사용자 정보 Cookie 정보가 존재할경우
                {
                    string des = EncryptionUtils.UserInfoDecrypt(System.Web.HttpContext.Current.Request.Cookies["ultrakey"].Value);
                    userInfo = JsonConvert.DeserializeObject<UserInfoDto>(des);
                    SetSession(userInfo);
                    stUserID = userInfo.USER_ID;  
                }
                else if ( fromMail || stUserID.Length > 0)     // 메일 링크로 접속할 경우 Window Logon Identity로 인증처리함
                {  
                    string url = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.WcfServices.CommonService.Url;
                    stUserID = GetWindowsUserID();
                    string strUserInfo = GetUserInfo(url, "ko-kr", stUserID);

                    // Window Logon Identity 가 정상적인 사용자가 아닐 경우 로그인 페이지로 Redirect
                    if (strUserInfo.Equals(string.Empty))
                    {
                        Response.Redirect(string.Format("{0}?ReturnURL={1}", WebSiteConfigHandler.Login.URL, Server.UrlEncode(Request.RawUrl)));
                        Response.End();
                    }  
                    userInfo = JsonConvert.DeserializeObject<UserInfoDto>(strUserInfo);
                    string encyptedUserInfo = EncryptionUtils.UserInfoEncrypt(strUserInfo);
                    SetCookie("ultrakey", encyptedUserInfo, "");
                    SetSession(userInfo);
                }
                else if (this.Session[Core.Consts.SessionClassName] == null) //세션 timeout
                {
                    if (Request.Url.AbsolutePath.EndsWith("Main.aspx"))
                    {
                        Response.Redirect(WebSiteConfigHandler.Login.URL);
                        Response.End();
                    }
                    else
                        RedirectLogin(WebSiteConfigHandler.Login.URL, Server.UrlEncode(Request.RawUrl));
                }
                //else //iframe 내부호출용 페이지 에서
                //{
                //    this.Sessions = (AccountInfo)this.Session[Core.Consts.SessionClassName];
                //}
  

            }

            // Ad Security 그룹 쿠키 생성
            if(stUserID.Length > 0 && HttpContext.Current.Request.Cookies[""] != null)
            {
                List<string> groups = null;
                using (Bayer.Ultra.Framework.Common.Activedirectory.LdapQuery ad = new Framework.Common.Activedirectory.LdapQuery())
                {
                    ad.GetMemberOf(stUserID);
                    SetCookie("ultraUserGroups", string.Join("|", groups), "");
                }
            }

            base.OnPreInit(e);
        }

        private string GetWindowsUserID()
        { 
            string strFullId = Request.LogonUserIdentity.Name;
            strFullId = Environment.UserName;

            LogManager.WriteLog(GetType(), MethodInfo.GetCurrentMethod().Name, null, strFullId, "Request.LogonUserIdentity.Name");

            string stUserID = string.Empty, strNetbiosDomain = string.Empty;
            string url = Bayer.Ultra.Framework.Config.WebSiteConfigHandler.WcfServices.CommonService.Url;

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

        #region SetCookie 지정한 쿠키를 설정
        /// <summary>
        /// 지정한 쿠키를 설정
        /// </summary>
        /// <param name="cookieName">쿠키명</param>
        /// <param name="cookieValue">쿠키값</param>
        /// <param name="domainName">도메인명</param>
        private void SetCookie(string cookieName, string cookieValue, string domainName)
        {
            System.Web.HttpCookie oMyCookie = null;

            try
            {
                oMyCookie = new System.Web.HttpCookie(cookieName);
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
        /// 페이지 초기화
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);
        }

        /// <summary>
        /// 페이지 로드
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
        }

        /// <summary>
        /// 렌더링전에공통함수호출
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPreRender(EventArgs e)
        {
            SetPageHiddenField(); //공통스크립트와 HiddenField설정
            base.OnPreRender(e);
        }

        private void SetSession(UserInfoDto userInfo)
        {
            this.Sessions = new AccountInfo()
            {
                UserID = userInfo.USER_ID,
                UserName = userInfo.FULL_NAME,
                CompanyCode = userInfo.COMPANY_CODE,
                CompanyName = userInfo.COMPANY_NAME,
                OrgName = userInfo.ORG_ACRONYM,
                JobTitle = userInfo.JOB_TITLE,
                MailAddress = userInfo.MAIL_ADDRESS,
                Mobile = userInfo.MOBILE,
                Phone = userInfo.PHONE,
                Title = userInfo.TITLE,
                LeadingSubGroup = userInfo.LEADING_SUBGROUP,
                ListCount = Framework.Config.WebSiteConfigHandler.UserConfiguration.ListCount,
                PageCount = Framework.Config.WebSiteConfigHandler.UserConfiguration.PageCount,
                Language = "ko-KR",
                IsCrmUser = userInfo.CRM_USER_YN.ToLower().Equals("yes") ? true : false
            };

            this.Session[Core.Consts.SessionClassName] = this.Sessions;
        }

        private object GetPropertyOfType(Type t, string property)
        {
            return t.GetType().GetProperty(property).GetValue(t);
        }

        private string GetReferConfigValue(Framework.Config.WebSiteSection section, string path, string attr)
        {
            string[] elements = path.Split(new char[] { '/' });

            object objConfig = section;
            for (int i = 0; i < elements.Length; i++)
            {
                string element = elements[i];
                element = element.First().ToString().ToUpper() + string.Join("", element.Skip(1)); //첫글자 대문자
                objConfig = objConfig.GetType().GetProperty(element).GetValue(objConfig);

            }
            string property = attr.First().ToString().ToUpper() + string.Join("", attr.Skip(1)); //첫글자 대문자
            return objConfig.GetType().GetProperty(property).GetValue(objConfig).ToString();
        }

        private void SetPageHiddenField()
        {
            try
            {
                RegisterScriptBlock();
            }
            catch
            {
                throw;
            }
        }
        protected void RegisterScriptBlock()
        {
            int nHeaderCount = 1;
            LiteralControl oLiCtrl = null;

            try
            {
                if (this.Header != null)
                {
                    oLiCtrl = new LiteralControl();

                    if (Header.Controls.Count < 1)
                        nHeaderCount = 0;

                    oLiCtrl.Text = GetRegisterScriptBlockString();

                    // 헤더 밑으로 js 파일을 추가한다.
                    this.Header.Controls.AddAt(nHeaderCount, oLiCtrl);
                }
            }
            catch
            {
                throw;
            }
        }

        protected string GetRegisterScriptBlockString()
        {
            StringBuilder sbConst = new StringBuilder();
            // js에 필요한 Const 를 추가한다.
            sbConst.Append("<meta http-equiv='X-UA-Compatible' content='IE=edge' />");
            sbConst.Append("\r\n<script language='javascript'>\r\n");
            sbConst.Append("   // 전역적으로 사용되는 스크립트 변수\r\n");
            try
            {
                foreach (Framework.Config.Element.ClientScriptVariableElement element in Framework.Config.WebSiteConfigHandler.ClientScriptVariables)
                {
                    string key = element.Key;
                    string value = element.Value;
                    string path = element.Path;
                    string attr = element.Attr;
                    if (element.Path.IsNotNullOrEmptyEx())
                    {
                        Framework.Config.WebSiteSection section = Framework.Config.WebSiteConfigHandler.RootSection;
                        value = GetReferConfigValue(section, path, attr);
                    }

                    value = value.Replace("\\", "\\\\");
                    if (!string.IsNullOrEmpty(key))
                    {
                        sbConst.Append("   var " + key + " = \"" + value + "\";\r\n");
                    }
                }
                //추가할 부분
                sbConst.Append("   var VISIT_LIMIT_COUNT = \"" + visitLimitCount + "\";\r\n");
                sbConst.Append("   var HCP_LIMIT_AMOUNT = \"" + hcpLimitAmount + "\";\r\n");
                sbConst.Append("</script>\r\n");

                return sbConst.ToString();
            }
            catch
            {
                throw;
            }
        }

        #endregion

        #region ClientScript 관련

        protected void RedirectLogin(string loginUrl, string returnUrl)
        {

            //StringBuilder script = new StringBuilder();
            try
            {
                //string url = loginUrl;

                string strCS = string.Format("window.parent.location = '{0}'", loginUrl);

                //if (!string.IsNullOrEmpty(returnUrl))
                //{
                //    url = string.Format("{0}?ReturnURL={1}", url, returnUrl);
                //}

                //script.Append("<script type='text/javascript'>");
                //script.Append("function f() {");
                //script.Append("var parent = window.parent;");
                //script.Append(string.Format("if(parent) { parent.location.href = {0}; }", url));
                //script.Append(string.Format("else { window.location.href = {0}; }", url));
                //script.Append(" return false; ");
                //script.Append("Sys.Application.remove_load(f);");
                //script.Append("}");
                //script.Append("Sys.Application.add_load(f);");
                //script.Append("</script>");
                //Page.ClientScript.RegisterStartupScript(this.GetType(), "RedirectLogin", script.ToString(), true);


                ClientScriptManager cs = Page.ClientScript;

                cs.RegisterClientScriptBlock(this.GetType(), "RedirectScript", strCS, true);

            }
            catch (Exception ex)
            {
                this.errorMessage = ex.ToString();
            }
            finally
            {
                //if (script != null)
                //{
                //    script.Clear();
                //    script = null;
                //}
            }
        }



        #endregion
    }
}
