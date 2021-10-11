using Bayer.Ultra.Framework.Common.Dto.Approval;
using Bayer.Ultra.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using Bayer.Ultra.Framework;
using Bayer.Ultra.BSL.Configuration.Mgr;
using Bayer.Ultra.Framework.Common.Dto.Configuration;
using System.ComponentModel;
using Bayer.Ultra.BSL.Approval.Mgr;
using Bayer.Ultra.Framework.Config;
using Bayer.Ultra.Framework.Common;
using Bayer.Ultra.Framework.Common.Dto.Common;
using Bayer.Ultra.BSL.Common.Mgr;

public partial class Master_Ultra_Event_change : System.Web.UI.MasterPage
{
    #region 멤버변수
    EventPageBase _eventPage;
    public event EventHandler SaveClicked;
    public event EventHandler RequestClicked;
    #endregion

    #region OnInit

    protected override void OnInit(EventArgs e)
    {
        String username_id = System.Security.Principal.WindowsIdentity.GetCurrent().Name;
        _eventPage = this.Page as EventPageBase;

        this.EventID = this.Page.Request["eventid"].NullObjectToEmptyEx();
        this.ProcessID = this.Page.Request["processid"].NullObjectToEmptyEx();
        this.Delegation = this.Page.Request["delegation"].IsNullOrEmptyEx() ? "none" : this.Page.Request["delegation"].NullObjectToEmptyEx(); //Delegation이 아니면 none 그렇지 않으면 all 또는 only
        InitApproveMenuBarEvent();
        base.OnInit(e);
        
    }
    #endregion

    #region PageLoad
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            try
            {

            
                this.hddUserID.Value = _eventPage.UserID;
                this.hddCompanyCode.Value = _eventPage.CompanyCode;

                //<VeeVa Roll-out : Create event by CRM user>
                this.hddisCRMUser.Value = _eventPage.isCRMUser.ToString();
                //this.hddisCRMUser.Value = "True";


                GetEventConfiguration(this.EventID);
                if (string.IsNullOrEmpty(this.ProcessID)) InitEventConfig();
                else SelectEventCommonInfo();

                SetVisableTab();
                SetCommandButtonStatus();
                //메일에서 Link로 넘어온경우 Readers Group에 없는경우를 체크하기 위해
                if (ProcessID.Length > 0 && !(ProcessStatus.Equals("Temp") || ProcessStatus.Equals("Saved")))
                    GetCheckApprovalProcessUser(ProcessID);

                SetGeneralInformation();

                RejectedProcessID = string.Empty; //TO-DO:
                
            }
            catch(Exception ex) {
                string ErrorMessage = string.Empty;
                ErrorMessage = ex.Message;
                throw;
            }

        }

    }


    #endregion

    #region GetCheckApprovalProcessUser
    private void GetCheckApprovalProcessUser(string processID)
    {
        bool retValue = false;
        using (_ApprovalMgr_Nx mgr = new _ApprovalMgr_Nx())
        {
            retValue = mgr.IsApprovalProcessUser(processID, _eventPage.UserID);
        }
        if (!retValue)
        {
            string redirectUrl = WebSiteConfigHandler.UltraInfo.Pages.Error;
            Response.Redirect(redirectUrl);
        }
    }
    #endregion

    #region 버튼 이벤트
    private void InitApproveMenuBarEvent()
    {
        this.ApproveMenuBar.btnRecallClick += ApproveMenuBar_btnRecallClick;
        this.ApproveMenuBar.btnWithdrawClick += ApproveMenuBar_btnWithdrawClick;
        this.ApproveMenuBar.btnRemindClick += ApproveMenuBar_btnRemindClick;
        this.ApproveMenuBar.btnReUseClick += ApproveMenuBar_btnReUseClick;
        this.ApproveMenuBar.btnExitClick += ApproveMenuBar_btnExitClick;
    }

    private void ApproveMenuBar_btnRecallClick(object sender, EventArgs e)
    {

    }

    private void ApproveMenuBar_btnWithdrawClick(object sender, EventArgs e)
    {

    }

    private void ApproveMenuBar_btnRemindClick(object sender, EventArgs e)
    {

    }

    private void ApproveMenuBar_btnReUseClick(object sender, EventArgs e)
    {

    }


    private void ApproveMenuBar_btnExitClick(object sender, EventArgs e)
    {

    }

    #endregion

    protected void CallClientScriptByCode(string funcCode)
    {
        string radconfirmscript = "<script language='javascript'>function f(){ " + funcCode + "; Sys.Application.remove_load(f);}; Sys.Application.add_load(f);</script>";
        Page.ClientScript.RegisterStartupScript(this.GetType(), "CallClientScript", radconfirmscript);
    }

    #region Event설정 

    /// <summary>
    /// 화면 기본 설정
    /// </summary>
    private void InitEventConfig()
    {
        using (_ApprovalMgr_Nx mgr = new _ApprovalMgr_Nx())
        {
            this.hddProcessID.Value = mgr.GetNewProcessID();
        }

        this.hspanRequester.InnerText = _eventPage.UserName;
        this.hspanOrganization.InnerText = _eventPage.Organization;
        this.hspanRequestedDate.InnerText = DateTime.Now.ToString("yyyy-MM-dd");
        this.hspanRetaentionPeriod.InnerText = LifeCycleText;
    }

    /// <summary>
    /// 이벤트 기본정보를 불러온다.
    /// </summary>
    /// <param name="eventID"></param>
    private void GetEventConfiguration(string eventID)
    {
        bool setEventCompleteCommentType = false;
        string eventCompleteCommentType = string.Empty;
        using (ConfigurationMgr mgr = new ConfigurationMgr())
        {
            DTO_WF_CONFIG config = mgr.GetConfiguration(eventID);
            if (config != null)
            {
                ApproveMenuBar.Title = config.EVENT_NAME;
                EventName = config.EVENT_NAME;
                LifeCycle = config.RETENTION_PERIOD_CODE;
                LifeCycleText = config.RETENTION_PERIOD_TEXT;
                ReviewerDesc = config.OPT_ADD_REVIEWER_DESC;
                AllowForward = config.OPT_FORWARD.ToLower().Equals("y") ? true : false;
                EnableCostPlan = config.OPT_ADD_COST_PLAN.ToLower().Equals("y") ? true : false;
                EnableParticipients = config.OPT_ADD_PARTICIPANTS.ToLower().Equals("y") ? true : false;
                EnableAgenda = config.OPT_ADD_AGENDA.ToLower().Equals("y") ? true : false;
                EnablePayment = config.OPT_ADD_PAYMENT.ToLower().Equals("y") ? true : false;
                SkipApproval = config.OPT_SKIP_APPROVAL.ToLower().Equals("y") ? true : false;
                OnlyApproval = config.OPT_ONLY_APPROVAL.ToLower().Equals("y") ? true : false;
                CheckCountRule = config.OPT_COUNT_RULE.ToLower().Equals("y") ? true : false;
                RoleType = config.AGENDA_ROLE_TYPE.ToString();
                if (config.OPT_EVENT_COMPLETE_COMMENT.ToLower().Equals("y")) setEventCompleteCommentType = true;
                EventCompleteOption = string.Format("{0}:{1}", config.OPT_EVENT_COMPLETE_COMMENT, config.OPT_EVENT_COMPLETE_ATTACH);
                eventCompleteCommentType = config.OPT_EVENT_COMPLETE_COMMENT_CATEGORY ?? string.Empty;

                //version 1.0.5 Event complete 시 briefing material(LMR) number check / Consulting Event complete 시 medical compliance 문구 추가
                EventCompleteOption_LMR = config.OPT_EVENT_CHECK_DETAIL_MATERIAL.ToLower().Equals("y") ? true : false;
                EventCompleteOption_MEDICAL = config.OPT_EVENT_CHECK_MEDICAL.ToLower().Equals("y") ? true : false;


                _eventPage.VisitLimitCount = config.VISIT_LIMIT_COUNT.ToString();
                _eventPage.HCPLimitAmount = config.HCP_LIMIT_AMOUNT.ToString();
            }
            List<DTO_WF_CONFIG_COST_CATEGORY> categories = mgr.SelectConfigCostCategory(eventID);
            EventCostPlan.SetCategory(categories);
            EventPayment.SetCategory(categories);
            EventParticipients.SetOtherHCP(eventID, _eventPage.Organization);
        }

        //국가 설정
        using (CommonMgr_Nx mgr = new CommonMgr_Nx())
        {
            EventParticipients.SetCountry(mgr.SelectCountry());
            EventAgenda.SetRoleType(RoleType);
            List<DTO_COMMON_CODE_SUB> commentCategories = mgr.SelectCommonCode("S013");
            ModalInputComment.SetInputCommentCategory(commentCategories);
            if (setEventCompleteCommentType)
                ModalEventComplete.SetCommentCategory(commentCategories, eventCompleteCommentType);

            //version 1.0.5 Event complete 시 Consulting Event complete 시 medical compliance 문구
            if (EventCompleteOption_MEDICAL)
                ModalEventComplete.SetMedical();
        }
    }
    /// <summary>
    /// Tab 관련
    /// </summary>
    /// <param name="eventPage"></param>
    private void SetVisableTab()
    {
        this.tabCostPlan.Visible = divGI_CostPlan.Visible = EnableCostPlan;
        this.tabParticipants.Visible = tabEtc.Visible = divGI_Participants.Visible = EnableParticipients;
        this.tabAgenda.Visible = divGI_Agenda.Visible = EnableAgenda;
        this.tabPayment.Visible = EnablePayment;
        //this.divGI_ApprovalLine.Visible = !SkipApproval;
        this.tblGI_ApprovalLine.Visible = !SkipApproval;
        this.tblGI_ApprovalLine_R.Visible = !SkipApproval;
    }

    private int[] GetEventButtonStatus()
    {
        if (string.IsNullOrEmpty(this.ProcessID))
        {
            return ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.NEW_REQUESTER);
        }
        else
        {
            return GetDocbtnStatus(this.ProcessID);
        }
    }

    #region GetProcessApproveByUser
    public List<DTO_PROCESS_APPROVAL_USER_STATUS> GetProcessApproveByUser(string processID)
    {
        using (_ApprovalMgr_Nx mgr = new _ApprovalMgr_Nx())
        {
            return mgr.SelectProcessApprovalStatusByUser(processID, _eventPage.UserID);
        }
    }
    #endregion

    #region GetDocbtnStatus
    public int[] GetDocbtnStatus(string processID)
    {
        int[] buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.DEFAULT);
        List<DTO_PROCESS_APPROVAL_USER_STATUS> retValue = GetProcessApproveByUser(processID);
        if (retValue.Count > 0)
        {
            foreach (DTO_PROCESS_APPROVAL_USER_STATUS item in retValue)
            {
                if ((item.APPROVAL_TYPE.NullObjectToEmptyEx().Equals(ApprovalUtil.ProcessStatus.REJECTER) || item.APPROVAL_TYPE.NullObjectToEmptyEx().Equals(ApprovalUtil.ProcessStatus.ACEPTER))
                    && item.PROCESS_STATUS.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalStatus.Reject.ToString()))
                {
                    buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.DEFAULT);
                }
                else
                {
                    // 문서가 완료되었을 경우
                    if (item.PROCESS_STATUS.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalStatus.Completed.ToString())
                        || item.PROCESS_STATUS.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalStatus.Withdraw.ToString())
                        || item.PROCESS_STATUS.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalStatus.EventCompleted.ToString())
                        || item.PROCESS_STATUS.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalStatus.PaymentCompleted.ToString()))
                    {
                        if (item.APPROVAL_TYPE.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalType.RECIPIENT))
                        {
                            if (!hddRequesterID.Value.Equals(_eventPage.UserID)) //로그인 사용자가 Requester인 경우는 제외
                            {
                                buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.COMPLETED_RECIPIENT);
                                // Recipient가 Withdraw를 수행하였을 경우 버튼을 비활성화 하도록 처리
                                if (item.APPROVER_STATUS.NullObjectToEmptyEx().Equals(ApprovalUtil.ProcessStatus.ACEPTER))
                                {
                                    buttonStatus[(int)ApprovalUtil.ApprovalButtons.Withdraw] = 0;
                                }
                            }
                        }
                        if (item.APPROVAL_TYPE.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalType.REVIEWER))
                        {
                            buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.COMPLETED_REVIEWER);
                        }
                        if (item.APPROVAL_TYPE.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalType.DRAFTER))
                        {
                            if (item.PROCESS_STATUS.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalStatus.EventCompleted.ToString()))
                            {
                                buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.EVENT_COMPLETE);
                            }
                            else if (item.PROCESS_STATUS.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalStatus.PaymentCompleted.ToString()))
                            {
                                buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.PAYMENT_COMPLETE);
                            }
                            else
                            {
                                //결재만 이면 - FreeGoods, Donation
                                if (this.OnlyApproval)
                                    buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.COMPLETED_APPROVER);
                                else
                                    buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.COMPLETED_REQUESTER);
                            }
                        }
                        if (item.APPROVAL_TYPE.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalType.APPROVER))
                        {
                            buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.COMPLETED_APPROVER);
                        }
                    }
                    // 문서가 거절되었을 경우
                    else if (item.PROCESS_STATUS.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalStatus.Reject.ToString()))
                    {
                        if (item.APPROVAL_TYPE.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalType.DRAFTER))
                        {
                            buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.REJECTED_REQUESTER);
                        }
                        else
                        {
                            buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.DEFAULT);
                            this.informationMessage.Value = "This document has been rejected.";
                        }
                    }
                    else if (item.PROCESS_STATUS.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalStatus.Canceled.ToString()))
                    {
                        if (item.APPROVAL_TYPE.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalType.DRAFTER))
                        {
                            buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.EXIT);
                        }
                        else
                        {
                            buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.DEFAULT);
                            this.informationMessage.Value = "This document has been rejected.";
                        }
                    }
                    else
                    {
                        // 결재 요청 및 결재승인을 수행하였을 경우는 이후 조회시 Exit 버튼만 표시하도록 처리
                        //2019.12.3 추가 by wookyung kim
                        //|| item.APPROVER_STATUS.NullObjectToEmptyEx().Equals(ApprovalUtil.ProcessStatus.AWAITER 추가 결제자가 아니고, 추후 결제 자이면 exit 만
                        if (!item.APPROVAL_TYPE.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalType.DRAFTER)
                            && (item.APPROVER_STATUS.NullObjectToEmptyEx().Equals(ApprovalUtil.ProcessStatus.ACEPTER)
                            || item.APPROVER_STATUS.NullObjectToEmptyEx().Equals(ApprovalUtil.ProcessStatus.AWAITER))
                            )
                        {
                            buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.EXIT);
                        }
                        else
                        {
                            if (!item.APPROVAL_TYPE.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalType.REVIEWER))
                            {
                                if (item.APPROVAL_TYPE.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalType.APPROVER))
                                {
                                    buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.ON_GOING_APPROVER);
                                }
                                else if (item.APPROVAL_TYPE.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalType.DRAFTER) || item.PROCESS_STATUS.NullObjectToEmptyEx().Equals(ApprovalUtil.ApprovalStatus.Processing.ToString()))
                                {
                                    buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.ON_GOING_REQUESTER);
                                }
                            }
                        }

                    }
                }
                ProcessStatus = item.PROCESS_STATUS;
            }

        }
        else
        {
            //Approver에 없는경우 저장상태인지 아니면 ReadersGroup에 포함되어 있는지를 구분하기 위해
            //문서 Status를 한번 가져와 비교한다.
            string processStatus = ApprovalUtil.ApprovalStatus.Temp.ToString();
            string requestid = null;

            using (_ApprovalMgr_Nx mgr = new _ApprovalMgr_Nx())
            {
                // 2015. 02. 12 Recall 시 userid를 비교하기 위해 requestID를 추가적으로 가져온다.
                string documentprocess = mgr.GetProcessEventStatus(processID);

                string[] status = documentprocess.Split(new char[] { '/' });
                processStatus = status[0];

                if (processStatus == "Temp")
                {
                    requestid = _eventPage.UserID;
                }
                else
                {
                    requestid = status[1];
                }
                hddRequesterID.Value = requestid;
            }

            if (processStatus == string.Empty) processStatus = ApprovalUtil.ApprovalStatus.Temp.ToString();
            if (processStatus.Equals(ApprovalUtil.ApprovalStatus.Temp.ToString()) || processStatus.Equals(ApprovalUtil.ApprovalStatus.Saved.ToString()))
            {
                // 2015. 02.12  
                // 수정내용 : Recall 된 문서를 approver 가 열수 없도록 하기 위해 requestID 를 받아와 Session userID 와 비교한다.
                if (requestid == _eventPage.UserID)
                {
                    buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.NEW_REQUESTER);
                    ((HtmlButton)ApproveMenuBar.FindControl("btnCancel")).InnerText = "Delete";
                }
                else
                {
                    if (this.Delegation.Equals("none"))
                    {
                        string redirectUrl = WebSiteConfigHandler.UltraInfo.Pages.Error;
                        Response.Redirect(redirectUrl);
                    }
                }
            }
            else if (processStatus.Equals(ApprovalUtil.ApprovalStatus.Request.ToString()) || processStatus.Equals(ApprovalUtil.ApprovalStatus.Processing.ToString()))
            {
                buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.EXIT);
            }
            else if (processStatus.Equals(ApprovalUtil.ApprovalStatus.Canceled.ToString()))
            {
                if (requestid == _eventPage.UserID)
                    buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.REJECTED_REQUESTER);
                else
                    buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.EXIT);
            }
            else
            {
                buttonStatus = ApprovalUtil.GetApprovalButtonAuthList(ApprovalUtil.ApprovalViewStatus.COMPLETED_REVIEWER);
            }
            ProcessStatus = processStatus;
        }
        return buttonStatus;
    }
    #endregion

    /// <summary>
    /// Button 관련
    /// </summary>
    /// <param name="eventPage"></param>
    private void SetCommandButtonStatus()
    {
        this.CommandAuthList = GetEventButtonStatus();

        int[] buttonStatus = CommandAuthList;
        HtmlButton btnRequest = (HtmlButton)ApproveMenuBar.FindControl("btnRequest");
        HtmlButton btnApproval = (HtmlButton)ApproveMenuBar.FindControl("btnApproval");
        HtmlButton btnForwardApproval = (HtmlButton)ApproveMenuBar.FindControl("btnForwardApproval");
        HtmlButton btnReject = (HtmlButton)ApproveMenuBar.FindControl("btnReject");
        HtmlButton btnForward = (HtmlButton)ApproveMenuBar.FindControl("btnForward");
        HtmlButton btnRecall = (HtmlButton)ApproveMenuBar.FindControl("btnRecall");
        HtmlButton btnWithdraw = (HtmlButton)ApproveMenuBar.FindControl("btnWithdraw");
        HtmlButton btnRemind = (HtmlButton)ApproveMenuBar.FindControl("btnRemind");
        HtmlButton btnExit = (HtmlButton)ApproveMenuBar.FindControl("btnExit");
        HtmlButton btnSave = (HtmlButton)ApproveMenuBar.FindControl("btnSave");
        HtmlButton btnInputComment = (HtmlButton)ApproveMenuBar.FindControl("btnInputComment");
        HtmlButton btnReUse = (HtmlButton)ApproveMenuBar.FindControl("btnReUse");
        HtmlButton btnEventComplete = (HtmlButton)ApproveMenuBar.FindControl("btnEventComplete");
        HtmlButton btnPaymentComplete = (HtmlButton)ApproveMenuBar.FindControl("btnPaymentComplete");
        HtmlButton btnCancel = (HtmlButton)ApproveMenuBar.FindControl("btnCancel");
        HtmlButton btnEdit = (HtmlButton)ApproveMenuBar.FindControl("btnEdit");

        btnRequest.Visible = buttonStatus[(int)ApprovalUtil.ApprovalButtons.Requet] == 1 ? true : false;
        btnApproval.Visible = buttonStatus[(int)ApprovalUtil.ApprovalButtons.Approval] == 1 ? true : false;
        btnForwardApproval.Visible = buttonStatus[(int)ApprovalUtil.ApprovalButtons.ForwardApproval] == 1 ? true : false;
        btnReject.Visible = buttonStatus[(int)ApprovalUtil.ApprovalButtons.Reject] == 1 ? true : false;
        if (AllowForward)
            btnForward.Visible = buttonStatus[(int)ApprovalUtil.ApprovalButtons.Forward] == 1 ? true : false;
        else
            btnForward.Visible = false;
        btnRecall.Visible = buttonStatus[(int)ApprovalUtil.ApprovalButtons.Recall] == 1 ? true : false;
        btnWithdraw.Visible = buttonStatus[(int)ApprovalUtil.ApprovalButtons.Withdraw] == 1 ? true : false;
        btnRemind.Visible = buttonStatus[(int)ApprovalUtil.ApprovalButtons.Remind] == 1 ? true : false;
        btnExit.Visible = buttonStatus[(int)ApprovalUtil.ApprovalButtons.Exit] == 1 ? true : false;
        btnSave.Visible = buttonStatus[(int)ApprovalUtil.ApprovalButtons.Save] == 1 ? true : false;
        btnInputComment.Visible = buttonStatus[(int)ApprovalUtil.ApprovalButtons.InputComment] == 1 ? true : false;
        btnReUse.Visible = buttonStatus[(int)ApprovalUtil.ApprovalButtons.ReUse] == 1 ? true : false;
        btnEventComplete.Visible = buttonStatus[(int)ApprovalUtil.ApprovalButtons.CompleteEvent] == 1 ? true : false;
        btnPaymentComplete.Visible = buttonStatus[(int)ApprovalUtil.ApprovalButtons.CompletePayment] == 1 ? true : false;
        btnCancel.Visible = buttonStatus[(int)ApprovalUtil.ApprovalButtons.Cancel] == 1 ? true : false;
        btnEdit.Visible = buttonStatus[(int)ApprovalUtil.ApprovalButtons.Edit] == 1 ? true : false;
    }

    private void SetGeneralInformation()
    {
        if (ProcessStatus.Equals(ApprovalUtil.ApprovalStatus.Temp.ToString())) //          || ProcessStatus.Equals(ApprovalUtil.ApprovalStatus.Saved.ToString())
        {
            divGI_CostPlan.Visible = false;
            divGI_Participants.Visible = false;
            divGI_Agenda.Visible = false;
            divGI_ApprovalLine.Visible = false;
            divGI_InputComment.Visible = false;

        }
        else
        {
            if (divGI_CostPlan.Visible) divGI_CostPlan.Visible = true;
            if (divGI_Participants.Visible)
            {
                divGI_Participants.Visible = true;
            }
            if (divGI_Agenda.Visible) divGI_Agenda.Visible = true;
            if (divGI_ApprovalLine.Visible) divGI_ApprovalLine.Visible = true; //결재가 없는 경우체크
            divGI_InputComment.Visible = true;
        }
    }

    /// <summary>
    /// 결재가 없는 문서인 경우가 Ultra에서 추가되어 ...
    /// </summary>
    private void DisplayEventCommonInfo()
    {
        using (_ApprovalMgr_Nx mgr = new _ApprovalMgr_Nx())
        {
            DTO_PROCESS_EVENT evt = mgr.SelectProcessEvent(ProcessID);
            if (evt != null)
            {
                hddRequesterID.Value = evt.REQUESTER_ID;
                hspanRequester.InnerText = evt.REQUESTER_NAME;
                ProcessStatus = evt.PROCESS_STATUS;
                hspanRequestedDate.InnerText = evt.REQUEST_DATE.HasValue ? evt.REQUEST_DATE.Value.ToString("yyyy-MM-dd") : string.Empty;
                hddCRMStatus.Value = string.Format("{0}:{1}", evt.BY_CRM, evt.CRM_STATUS);
                this.hspanEventKey_1.InnerHtml = evt.EVENT_KEY;
            }
            else
            {
                ProcessStatus = "Temp";
            }
        }
    }


    private void DisplayApprovalLine()
    {
        try
        {
            List<UserAutocompleteDto> reviewers = new List<UserAutocompleteDto>(); // 화면 Autocomplete에 Reviewer를 뿌리기 위해
            using (_ApprovalMgr_Nx mgr = new _ApprovalMgr_Nx())
            {
                foreach (DTO_PROCESS_APPROVAL_LIST item in mgr.SelectProcessApproverList(ProcessID))
                {

                    if (item.APPROVAL_TYPE.Equals(ApprovalUtil.ApprovalType.DRAFTER) || item.APPROVAL_TYPE.Equals(ApprovalUtil.ApprovalType.APPROVER))
                    {
                        if (item.IDX == 1)
                        {
                            hspanRequester.InnerText = item.APPROVER;
                            hspanOrganization.InnerText = item.APPROVER_ORG_NAME;
                        }
                        TableRow row = new TableRow();
                        row.ID = string.Format("rowID_{0}", item.IDX.ToString());
                        TableCell cellNo = new TableCell();
                        cellNo.Text = item.IDX.ToString();
                        TableCell cellName = new TableCell();

                        // Lync Name Card 추가
                        HtmlGenericControl span = new HtmlGenericControl("span");
                        span.ID = string.Format("pre_user_{0}", item.APPROVER_ID);
                        span.Attributes.Add("value", item.MAIL_ADDRESS);
                        span.Attributes.Add("userid", item.APPROVER_ID);
                        //span.Attributes.Add("onmouseover", "ShowOOUI('" + item.MAIL_ADDRESS + "',this);");
                        //span.Attributes.Add("onmouseout", "HideOOUI('" + item.MAIL_ADDRESS + "');");
                        span.InnerText = item.APPROVER;

                        // Lync Status 
                        HtmlGenericControl spanStatus = new HtmlGenericControl("span");
                        spanStatus.ID = string.Format("pre_{0}", item.APPROVER_ID);
                        spanStatus.Attributes.Add("userid", item.APPROVER_ID);
                        spanStatus.Attributes.Add("value", item.MAIL_ADDRESS);
                        spanStatus.Attributes.Add("userid", item.APPROVER_ID);
                        spanStatus.Attributes.Add("class", "lync_status");
                        spanStatus.Attributes.Add("onmouseover", "ShowOOUI('" + item.MAIL_ADDRESS + "',this);");
                        spanStatus.Attributes.Add("onmouseout", "HideOOUI('" + item.MAIL_ADDRESS + "');");

                        span.Controls.AddAt(0, spanStatus);
                        cellName.Controls.Add(span);
                        //cellName.Text = item.APPROVER;
                        TableCell cellSing = new TableCell();
                        string approvalDate = item.APPROVAL_DATE.HasValue ? item.APPROVAL_DATE.Value.ToString("yyyy-MM-dd HH:mm:ss") : string.Empty;
                        cellSing.Text = string.Format("{0}<br/>{1}", approvalDate, item.COMMENT);
                        row.Cells.Add(cellNo);
                        row.Cells.Add(cellName);
                        row.Cells.Add(cellSing);
                        tblGI_ApprovalLine.Rows.Add(row);
                    }
                    else if (item.APPROVAL_TYPE.Equals(ApprovalUtil.ApprovalType.RECIPIENT))
                    {
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        li.InnerText = item.APPROVER;
                        ulRecipient.Controls.Add(li);

                    }
                    else if (item.APPROVAL_TYPE.Equals(ApprovalUtil.ApprovalType.REVIEWER))
                    {
                        HtmlGenericControl li = new HtmlGenericControl("li");
                        li.InnerText = item.APPROVER;
                        ulReviewer.Controls.Add(li);

                        reviewers.Add(new UserAutocompleteDto()
                        {
                            USER_ID = item.APPROVER_ID,
                            FULL_NAME = item.APPROVER,
                            ORG_ACRONYM = item.APPROVER_ORG_NAME
                        });
                    }
                }

                if (ProcessStatus.Equals("Temp") || ProcessStatus.Equals("Saved"))
                {
                    if (reviewers.Count > 0)
                        this.hddAutocompleteBoxEntries.Value = Newtonsoft.Json.JsonConvert.SerializeObject(reviewers);
                    else
                        this.hddAutocompleteBoxEntries.Value = string.Empty;
                }
                else
                {
                    this.rowReviewer.Visible = false;
                }
            }
        }
        catch
        {
            throw;
        }
    }

    private void DisplayInputComment()
    {
        try
        {
            using (_ApprovalMgr_Nx mgr = new _ApprovalMgr_Nx())
            {
                foreach (DTO_LOG_EVENT comment in mgr.SelectLogEventList(ProcessID))
                {
                    TableRow row = new TableRow();
                    TableCell cellName = new TableCell();
                    string name = string.Empty;
                    if (comment.CREATE_DATE != null && !string.IsNullOrEmpty(comment.CREATE_DATE.ToString()))
                        name = "<br/><strong>(" + Convert.ToDateTime(comment.CREATE_DATE).ToString("yyyy-MM-dd HH:mm") + ")</strong>";
                    cellName.Text = comment.REGISTER + name;
                    TableCell cellType = new TableCell();
                    cellType.Text = comment.LOG_TYPE;
                    TableCell cellCategory = new TableCell();
                    cellCategory.Text = comment.LOG_CATEGORY_NAME;
                    TableCell cellComment = new TableCell();
                    string content = string.Empty;
                    if (!string.IsNullOrEmpty(comment.LOG_CATEGORY_NAME))
                        content = "<strong>[" + comment.LOG_CATEGORY_NAME + "]</strong><br/>";
                    content += comment.COMMENT;
                    if (!string.IsNullOrEmpty(comment.FILE_PATH))
                    {
                        content += string.Format(" <a href='#' class='fa fa-paperclip colr-darkgray' data-attach-file='{0}') onclick='fn_DownloadCommentAttachFile(this);'></a>", HttpUtility.UrlEncode(comment.FILE_PATH));
                    }
                    cellComment.Text = content;

                    row.Cells.Add(cellName);
                    row.Cells.Add(cellType);
                    row.Cells.Add(cellComment);

                    tblInputComment.Rows.Add(row);
                }
            }
        }
        catch
        {
            throw;
        }
    }

    /// <summary>
    /// 각 이벤트 문서의 공통정보를 조회
    ///  -결재 라인
    ///  -코멘트
    /// </summary>
    public void SelectEventCommonInfo()
    {
        DisplayEventCommonInfo();
        DisplayApprovalLine();
        DisplayInputComment();
    }


    #endregion

    #region Property

    public string InformationMessage
    {
        get { return this.hddInformation.Value; }
        set { this.hddInformation.Value = value; }
    }

    public string EventID
    {
        get { return this.hddEventID.Value; }
        set { this.hddEventID.Value = value; }
    }

    public string EventKey
    {
        get { return hspanEventKey.InnerText; }
        set { hspanEventKey.InnerText = value; }
    }

    public string ProcessID
    {
        get { return this.hddProcessID.Value; }
        set { this.hddProcessID.Value = value; }
    }

    public string ProcessStatus
    {
        get { return this.hddProcessStatus.Value; }
        set
        {
            this.hddProcessStatus.Value = value;
            if (!value.Equals(ApprovalUtil.ApprovalStatus.Temp.ToString())) //Temp인 경우 미표시
                hspanStatus.InnerText = value;
        }
    }

    public string Delegation
    {
        get { return this.hddDelegation.Value; }
        set { this.hddDelegation.Value = value; }
    }

    public string Subject
    {
        get { return ViewState["Subject"].ToString(); }
        set { ViewState["Subject"] = value; }
    }

    public int[] CommandAuthList { get; set; }

    public List<DTO_APPROVAL_LINE> ApprovalLine
    {
        get { return (List<DTO_APPROVAL_LINE>)ViewState["ApprovalLine"]; }
        set { ViewState["ApprovalLine"] = value; }
    }

    public string EventName
    {
        get { return ViewState["EventName"].ToString(); }
        set { ViewState["EventName"] = value; }
    }

    public string LifeCycle
    {
        get { return ViewState["LifeCycle"].ToString(); }
        set
        {
            ViewState["LifeCycle"] = value;
            this.hddLifeCycle.Value = value;
        }
    }

    public string LifeCycleText
    {
        get { return ViewState["LifeCycleText"].ToString(); }
        set
        {
            hspanRetaentionPeriod.InnerText = value;
            ViewState["LifeCycleText"] = value;
        }
    }

    public string RequestDate
    {
        get { return ViewState["RequestDate"].ToString(); }
        set
        {
            hspanRequestedDate.InnerText = value;
            ViewState["RequestDate"] = value;
        }
    }

    public string ReviewerDesc
    {
        get { return ViewState["ReviewerDesc"].ToString(); }
        set { ViewState["ReviewerDesc"] = value; }
    }

    public bool AllowForward
    {
        get { return Convert.ToBoolean(ViewState["AllowForward"]); }
        set { ViewState["AllowForward"] = value; }
    }

    public bool EnableCostPlan
    {
        get { return Convert.ToBoolean(ViewState["EnableCostPlan"]); }
        set { ViewState["EnableCostPlan"] = value; }
    }

    public bool EnableParticipients
    {
        get { return Convert.ToBoolean(ViewState["EnableParticipients"]); }
        set { ViewState["EnableParticipients"] = value; }
    }

    public bool EnableAgenda
    {
        get { return Convert.ToBoolean(ViewState["EnableAgenda"]); }
        set { ViewState["EnableAgenda"] = value; }
    }

    public bool EnablePayment
    {
        get { return Convert.ToBoolean(ViewState["EnablePayment"]); }
        set { ViewState["EnablePayment"] = value; }
    }


    public string RejectedProcessID
    {
        get { return ViewState["RejectedProcessID"].ToString(); }
        set { ViewState["RejectedProcessID"] = value; }
    }

    /// <summary>
    /// 결재 Skip여부
    /// </summary>
    public bool SkipApproval
    {
        get { return Convert.ToBoolean(ViewState["SkipApproval"]); }
        set
        {
            ViewState["SkipApproval"] = value;
            this.hddSkipApproval.Value = value.ToString();
        }
    }

    public bool OnlyApproval
    {
        get { return Convert.ToBoolean(ViewState["OnlyApproval"]); }
        set
        {
            ViewState["OnlyApproval"] = value;
            this.hddOnlyApproval.Value = value.ToString();
        }
    }

    /// <summary>
    /// Agenda의 비용관련 횟수 체크여부
    /// </summary>
    public bool CheckCountRule
    {
        get { return Convert.ToBoolean(ViewState["CheckCountRule"]); }
        set { ViewState["CheckCountRule"] = value; }
    }

    public string RoleType
    {
        get { return ViewState["RoleType"].ToString(); }
        set { ViewState["RoleType"] = value; }
    }

    public string EventCompleteOption
    {
        set { this.hddEventCompleteOption.Value = value; }
    }
    
    /// version 1.0.5 Event complete 시 briefing material(LMR) number check / Consulting Event complete 시 medical compliance 문구 추가
    public bool EventCompleteOption_LMR
    {
        get { return Convert.ToBoolean(ViewState["EventCompleteOption_LMR"]); }
        set { ViewState["EventCompleteOption_LMR"] = value;
            
        }
    }

    public bool EventCompleteOption_MEDICAL
    {
        get { return Convert.ToBoolean(ViewState["EventCompleteOption_MEDICAL"]); }
        set { ViewState["EventCompleteOption_MEDICAL"] = value;
            this.hddEventCompleteOption_medical.Value = value.ToString();
        }
    }

    #endregion

}
