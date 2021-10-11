using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class Pages_Authentication_Logout : System.Web.UI.Page
{
    protected void Page_Load(object sender, System.EventArgs e)
    {
        try
        {
            Response.Cookies.Clear();
            
            this.Session.Clear();
            this.Session.Abandon();

            System.Web.Security.FormsAuthentication.SignOut();

            HttpContext.Current.Response.Cookies.Set(new HttpCookie("ultrakey", ""));
        }
        catch (Exception ex)
        {
            throw ex;
        }

        finally
        {
            try
            {
                this.Response.Clear();
                this.Response.Redirect(Bayer.Ultra.Framework.Config.WebSiteConfigHandler.Login.URL);
                this.Response.End();
            }

            catch (Exception)
            {
            }
        }
    }

    #region Web Form 디자이너에서 생성한 코드
    override protected void OnInit(EventArgs e)
    {
        //
        // CODEGEN: 이 호출은 ASP.NET Web Form 디자이너에 필요합니다.
        //
        InitializeComponent();
        base.OnInit(e);
    }

    /// <summary>
    /// 디자이너 지원에 필요한 메서드입니다.
    /// 이 메서드의 내용을 코드 편집기로 수정하지 마십시오.
    /// </summary>
    private void InitializeComponent()
    {
    }
    #endregion
}