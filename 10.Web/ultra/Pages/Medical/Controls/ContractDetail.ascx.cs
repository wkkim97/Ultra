using Bayer.Ultra.Framework.Common.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Medical_Controls_ContractDetail : System.Web.UI.UserControl
{
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
            List<DTO_COMMON_CODE_SUB> sataus = oMgr.SelectCommonCode("ME03");
            SetControlBind(sataus, "ME03");


        }
    }

    private void SetControlBind(List<DTO_COMMON_CODE_SUB> items, string classCode)
    {
        foreach (DTO_COMMON_CODE_SUB item in items)
        {
            selContractStatus.Items.Add(new ListItem(item.CODE_NAME, item.SUB_CODE));
        }

    }
}