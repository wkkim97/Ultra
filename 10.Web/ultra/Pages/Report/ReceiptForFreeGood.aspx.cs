using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Report_ReceiptForFreeGood : Bayer.Ultra.WebBase.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hhdDate.Value = DateTime.Now.ToString("yyyy-MM-dd");
        hddUserID.Value = Sessions.UserID;
        hhdUserName.Value = Sessions.UserName; 
    }
}