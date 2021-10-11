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
        this.hhdUserName.Value = Sessions.UserName;

        string securityGroups = HttpContext.Current.Request.Cookies["ultraUserGroups"].Value;
        if (securityGroups.IndexOf(Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.RAD_USER) != -1) this.userType.Value = "END USER";
        else if (securityGroups.IndexOf(Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.RAD_KEY_USER) != -1) this.userType.Value = "KEY USER";
        else if (securityGroups.IndexOf(Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.RAD_KEY_USER2) != -1) this.userType.Value = "KEY USER";
    }
    //유저가 Radiology security group인지 여부를 조회한다.
    private bool isRad()
    {
        string securityGroups = string.Empty;
        if (System.Web.HttpContext.Current.Request.Cookies["ultraUserGroups"] != null && Request.Cookies["ultraUserGroups"].Value != null)    // 사용자 정보 Cookie 정보가 존재할경우
        {
            securityGroups = HttpContext.Current.Request.Cookies["ultraUserGroups"].Value;

           if (securityGroups.IndexOf(Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.RAD_USER) != -1 || securityGroups.IndexOf(Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.RAD_KEY_USER) != -1 || securityGroups.IndexOf(Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.RAD_KEY_USER2) != -1)
           //     if (securityGroups.IndexOf(Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.RAD_USER) != -1 )
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
        }
    }
}
