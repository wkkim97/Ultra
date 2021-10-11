using Bayer.Ultra.Framework.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Common_Controls_ApproveMenuBar : System.Web.UI.UserControl
{
    public event EventHandler btnFowardApprovalClick;
    public event EventHandler btnRejectClick;
    public event EventHandler btnForwardClick;
    public event EventHandler btnRecallClick;
    public event EventHandler btnWithdrawClick;
    public event EventHandler btnRemindClick;
    public event EventHandler btnExitClick;
    public event EventHandler btnSaveClick;
    public event EventHandler btnInputCommandClick;
    public event EventHandler btnReUseClick;

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    #region Property

    public string Title
    {
        get { return this.hspanEventTitle.InnerText; }
        set { this.hspanEventTitle.InnerText = value; }
    }

    #endregion  

    protected void btnForwardApproval_ServerClick(object sender, EventArgs e)
    {
        if (btnFowardApprovalClick != null) btnFowardApprovalClick(sender, e);
    }

    protected void btnReject_ServerClick(object sender, EventArgs e)
    {
        if (btnRejectClick != null) btnRejectClick(sender, e);
    }

    protected void btnForward_ServerClick(object sender, EventArgs e)
    {
        if (btnForwardClick != null) btnForwardClick(sender, e);
    }

    protected void btnRecall_ServerClick(object sender, EventArgs e)
    {
        if (btnRecallClick != null) btnRecallClick(sender, e);
    }

    protected void btnWithdraw_ServerClick(object sender, EventArgs e)
    {
        if (btnWithdrawClick != null) btnWithdrawClick(sender, e);
    }

    protected void btnRemind_ServerClick(object sender, EventArgs e)
    {
        if (btnRemindClick != null) btnRemindClick(sender, e);
    }

    protected void btnExit_ServerClick(object sender, EventArgs e)
    {
        if (btnExitClick != null) btnExitClick(sender, e);
    }

    protected void btnSave_ServerClick(object sender, EventArgs e)
    {
        if (btnSaveClick != null) btnSaveClick(sender, e);
    }

    protected void btnInputComment_ServerClick(object sender, EventArgs e)
    {
        if (btnInputCommandClick != null) btnInputCommandClick(sender, e);
    }

    protected void btnReUse_ServerClick(object sender, EventArgs e)
    {
        if (btnReUseClick != null) btnReUseClick(sender, e);
    }

    int[] _buttonStatus;
    public int[] ButtonStatus
    {
        set
        {
             _buttonStatus = value;
            this.btnRequest.Visible = Convert.ToBoolean(_buttonStatus[(int)ApprovalUtil.ApprovalButtons.Requet]);
        }        
    }

    public string Test
    {
        get;set;
    }

}