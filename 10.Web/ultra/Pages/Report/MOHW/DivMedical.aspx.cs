using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Report_MOHW_DivMedical : Bayer.Ultra.WebBase.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
        hddMohwType.Value = Bayer.Ultra.Framework.Common.ApprovalUtil.MOHW_TYPE.DIV_MEDICAL;
        hddUserName.Value = Sessions.UserName;
        hddGetDate.Value = DateTime.Now.ToString("yyyy-MM-dd");

    }
}