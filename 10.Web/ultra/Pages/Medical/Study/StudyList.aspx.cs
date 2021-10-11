using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Medical_Study_StudyList : Bayer.Ultra.WebBase.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            PageInit();
        }
        catch(Exception ex)
        {
            this.errorMessage = ex.ToString();
        }
    }

    private void PageInit()
    {
        this.hhdUserID.Value = Sessions.UserID;
    }
}