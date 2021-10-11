using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Event_Print_PrintYourDocesCover : Bayer.Ultra.WebBase.PageBase
{
   
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            
            this.hddUserID.Value = Sessions.UserID;
           
        }

    }
}