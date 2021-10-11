using Bayer.Ultra.BSL.Approval.Mgr;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Medical_Study_StudyList : Bayer.Ultra.WebBase.PageBase
{
    public bool auth = false;
    public string errorMessage = null;
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            auth = isRad();
            if (auth)
            {
                PageInit();
            }
            else
            {
                errorMessage = "사용 권한이 없습니다.";
            }
        }
        catch(Exception ex)
        {
            errorMessage = ex.ToString();
        }
    }

    private void PageInit()
    {
        this.hhdUserID.Value = Sessions.UserID;
        this.hddUserID.Value = Sessions.UserID;
        this.hhdUserName.Value = Sessions.UserName;
        this.hhdUserOrgName.Value = Sessions.OrgName;
        this.hhdUserEmail.Value = Sessions.MailAddress;
        string userType = "END USER";

        string securityGroups = HttpContext.Current.Request.Cookies["ultraUserGroups"].Value;
        if (securityGroups != "")
        {
            if (securityGroups.IndexOf(Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.RAD_USER) != -1) userType = "END USER";
            else if (securityGroups.IndexOf(Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.RAD_KEY_USER) != -1) userType = "KEY USER";
            else if (securityGroups.IndexOf(Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.NON_ONEKEY_KEY_USER) != -1) userType = "KEY USER";
        }

        if (Sessions.UserID == "GITDC" || Sessions.UserID == "GIGOK") userType = "KEY USER";

        this.userType.Value = userType;

        if (Request["processid"] != null)
        {
            string status = Request["processid"];
            this.hhdStatus.Value = status;
        }
    }
    //유저가 Radiology security group인지 여부를 조회한다.
    private bool isRad()
    {
        
        return true;
        /*string securityGroups = string.Empty;

        if (System.Web.HttpContext.Current.Request.Cookies["ultraUserGroups"] != null)    // 사용자 정보 Cookie 정보가 존재할경우
        {   
            securityGroups = HttpContext.Current.Request.Cookies["ultraUserGroups"].Value;

            if (securityGroups.IndexOf(Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.RAD_USER) != -1 || securityGroups.IndexOf(Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.RAD_KEY_USER) != -1 || securityGroups.IndexOf(Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.NON_ONEKEY_KEY_USER) != -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }*/
    }
}
