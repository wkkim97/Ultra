using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Bayer.Ultra.Framework;
using Bayer.Ultra.WebBase;
using Bayer.Ultra.BSL.Configuration.Mgr;
using Bayer.Ultra.Framework.Common.Dto.Configuration;
using System.Web.UI.WebControls;

/// <summary>
/// EventPageBase의 요약 설명입니다.
/// </summary>
public abstract class EventPageBase : Bayer.Ultra.WebBase.PageBase
{

    public EventPageBase()
    {
        //
        // TODO: 여기에 생성자 논리를 추가합니다.
        //        
    }

    protected override void OnPreInit(EventArgs e)
    {
        base.OnPreInit(e);
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected override void OnLoad(EventArgs e)
    {
        if (!IsPostBack)
        {
            InitPageDefaultInfo();

        }
        InitPageInfo();
        base.OnLoad(e);
    }

    /// <summary>
    /// EventID와 ProcessID를 기본적으로 설정한다.
    /// </summary>
    private void InitPageDefaultInfo()
    {
        if (Sessions != null)
        {
            this.UserID = Sessions.UserID;
            this.UserName = Sessions.UserName;
            this.CompanyCode = Sessions.CompanyCode;
            this.CompanyName = Sessions.CompanyName;
            this.Organization = Sessions.OrgName;

            //<VeeVa Roll-out : Create event by CRM user>
            this.isCRMUser = Sessions.IsCrmUser.ToString();
        }
    }

    protected abstract void InitPageInfo();

    #region Property

    public string UserID
    {
        get { return ViewState["UserID"].ToString(); }
        set { ViewState["UserID"] = value; }
    }

    public string UserName
    {
        get { return ViewState["UserName"].ToString(); }
        set { ViewState["UserName"] = value; }
    }

    public string CompanyCode
    {
        get { return ViewState["CompanyCode"].ToString(); }
        set { ViewState["CompanyCode"] = value; }
    }

    public string CompanyName
    {
        get { return ViewState["CompanyName"].ToString(); }
        set { ViewState["CompanyName"] = value; }
    }

    public string Organization
    {
        get { return ViewState["Organization"].ToString(); }
        set { ViewState["Organization"] = value; }
    }
    //<VeeVa Roll-out : Create event by CRM user>
    public string isCRMUser
    {
        get { return ViewState["isCRMUser"].ToString(); }
        set { ViewState["isCRMUser"] = value; }
    }

    public string VisitLimitCount
    {
        get { return ViewState["VisitLimitCount"].ToString(); }
        set
        {
            ViewState["VisitLimitCount"] = value;
            visitLimitCount = value;
        }
    }

    public string HCPLimitAmount
    {
        get { return ViewState["HCPLimitAmount"].ToString(); }
        set
        {
            ViewState["HCPLimitAmount"] = value;
            hcpLimitAmount = value;
        }
    }

    #endregion

}