using Bayer.Ultra.Framework.Common.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Medical_Study : System.Web.UI.UserControl
{
    private const string categoryCode = "ME10";  // Category Code
    private const string statusCode = "ME01"; // Status Code
    private const string teamCode = "ME02";  // Team Code
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!this.IsPostBack)
            {
                PageInit();
            }

        }
        catch (Exception ex)
        { 
        }
    }


    private void PageInit()
    { 
        using (Bayer.Ultra.BSL.Common.Mgr.CommonMgr_Nx oMgr = new Bayer.Ultra.BSL.Common.Mgr.CommonMgr_Nx())
        {
            List<DTO_COMMON_CODE_SUB> category = oMgr.SelectCommonCode(categoryCode);
            List<DTO_COMMON_CODE_SUB> sataus = oMgr.SelectCommonCode(statusCode);
            List<DTO_COMMON_CODE_SUB> team = oMgr.SelectCommonCode(teamCode);
            SetControlBind(category, categoryCode);
            SetControlBind(sataus, statusCode);
            SetControlBind(team, teamCode);
        } 
    }

    private void SetControlBind(List<DTO_COMMON_CODE_SUB> items, string classCode)
    {
        foreach (DTO_COMMON_CODE_SUB item in items)
        {
            if (classCode == categoryCode)
            {
                selCategory.Items.Add(new ListItem(item.CODE_NAME, item.SUB_CODE));
            }
            else if (classCode == statusCode)
            {
                selStatus.Items.Add(new ListItem(item.CODE_NAME, item.SUB_CODE));
            }
            else if (classCode == teamCode)
            {
                selTeam.Items.Add(new ListItem(item.CODE_NAME, item.SUB_CODE));
            }
        }

    }
}