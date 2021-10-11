using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Common_Controls_FileUploader : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //[System.ComponentModel.Bindable(true)]
    public string FileAttachType
    {
        get
        {
            return this.hddFU_AttachType.Value;
        }
        set
        {
            this.hddFU_AttachType.Value = value;
        }
    }

    //[System.ComponentModel.Bindable(true)]
    public string IsMultiple
    {
        get
        {
            return this.hddFU_IsMultiple.Value;
        }
        set
        {
            this.hddFU_IsMultiple.Value = value;
        }
    }
}

public enum AttachType
{
    EventCommon,
    InputComment,
    CostPlan,
}