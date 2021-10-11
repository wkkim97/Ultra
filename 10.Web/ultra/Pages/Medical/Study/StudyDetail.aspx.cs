using Bayer.Ultra.Framework.Common.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Medical_Study_StudyDetail : Bayer.Ultra.WebBase.PageBase
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
            this.errorMessage = ex.ToString();
        }

    }
    private void PageInit()
    {

        // medicalidx를 processid 로 받음.
        if (Request["processid"] != null)
        {
            hhdMedicalIdx.Value = Request["processid"].ToString();
        }
  
        hhdUserID.Value = Sessions.UserID;
        hhdUserName.Value = Sessions.UserName;
    }
}