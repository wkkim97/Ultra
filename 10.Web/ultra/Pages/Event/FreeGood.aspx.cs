using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Event_FreeGood : EventPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        datStartTime.Value = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
    }

    protected override void InitPageInfo()
    {

    }

}