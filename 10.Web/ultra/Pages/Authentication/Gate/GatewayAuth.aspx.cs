using Bayer.Ultra.Framework;
using Bayer.Ultra.Framework.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Authentication_Gate_GatewayAuth : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            string returnUrl = Request["ReturnURL"];

            if (returnUrl.Equals(string.Empty))
                returnUrl = "/ultra/Pages/Main.aspx";

            var userAgent = HttpContext.Current.Request.UserAgent.ToLower();
            if (userAgent.Contains("iphone;") || userAgent.Contains("ipad;"))
            {
                // iPhone  or  iPad 
                Response.Redirect(string.Format("{0}?ReturnURL={1}", WebSiteConfigHandler.Login.URL, HttpUtility.UrlEncode(returnUrl)));
            }
            else
            {
                Response.Redirect(string.Format("/Ultra/Pages/Authentication/WindowsAuth/Auth.aspx?ReturnURL={0}", HttpUtility.UrlEncode( returnUrl)));
            }
        } 
        catch(Exception ex)
        {
            LogManager.WriteLog(GetType(), MethodInfo.GetCurrentMethod().Name, null, ex.ToString(), "GateWay Auth Error");
        }
        finally
        {
            Response.End();
        }
    }
     
}