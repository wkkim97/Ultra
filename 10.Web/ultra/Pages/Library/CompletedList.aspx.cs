using Bayer.Ultra.Framework.Common.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_Library_CompletedList : Bayer.Ultra.WebBase.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            InitControls();
    }

    private void InitControls()
    {
        Master.UserID = Sessions.UserID;
    }

    //private void GetDocList()
    //{
    //    try
    //    {
    //        List<DTO_USER_CONFIG_MENU_SORT> menuList;
    //        using (Bayer.Ultra.BSL.Common.Mgr.CommonMgr_Nx mgr = new Bayer.Ultra.BSL.Common.Mgr.CommonMgr_Nx())
    //        {
    //            menuList = mgr.SelectUserConfigMenuSort(Master.UserID);

    //            if (menuList == null) return;

    //            StringBuilder sb = new StringBuilder();

    //            sb.AppendLine("<label style=\"display:block; font-weight:normal\">");
    //            sb.AppendFormat("<input type=\"checkbox\" onclick=\"CheckAll()\" id=\"checkall\" />");
    //            sb.AppendFormat(" Check All");
    //            sb.AppendLine("</label>");

    //            foreach (DTO_USER_CONFIG_MENU_SORT menu in menuList)
    //            {
    //                sb.AppendLine("<label style=\"display:block; font-weight:normal\">");
    //                sb.AppendFormat("<input type=\"checkbox\" data-event-id=\"{0}\" name=\"chk\" />", menu.EVENT_ID);
    //                sb.AppendFormat(" " + menu.EVENT_NAME);
    //                sb.AppendLine("</label>");
    //            }
    //            hdivDocList.InnerHtml = sb.ToString();
    //        }
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
}