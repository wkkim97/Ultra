using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Report_MOHW_Conditions : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        hhdStatDate.Value = DateTime.Now.AddDays(-10).ToString("yyyy-MM-dd");
        hhdEndDate.Value = DateTime.Now.AddDays(10).ToString("yyyy-MM-dd");
    }
}