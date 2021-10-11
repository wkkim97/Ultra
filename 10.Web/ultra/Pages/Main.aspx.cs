using Bayer.Ultra.Framework;
using Bayer.Ultra.Framework.Common.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_main : Bayer.Ultra.WebBase.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            GetEventMenuList();
            InitControls();
        }
    }

    private void InitControls()
    {
        if (Sessions == null) return;

        this.hhdUserID.Value = Sessions.UserID;
        this.spanUserName.InnerText = Sessions.UserName;
        this.spanOrgName.InnerText = string.Format(" | {0}", Sessions.OrgName);
        



        // tabpage 파라메터가 존재할경우 hidden 필드값에 추가함.
        if (Request["tabpage"] != null)
        {
            string tabpage = Request["tabpage"];
            System.Collections.Hashtable ht = new System.Collections.Hashtable();

            try
            {

                string url = System.Web.HttpUtility.UrlDecode(tabpage);
                string page = url.Substring(0, url.IndexOf('?'));
                List<string> list = url.Remove(0, url.IndexOf('?') + 1).Split('&').ToList();

                foreach (string item in list)
                {
                    string[] value = item.Split('=');
                    ht.Add(value[0], value[1]);
                }
                hhdAddTabPage.Value = page;
                hhdAddTabProcessId.Value = ht.ContainsKey("processid") ? ht["processid"].ToString() : "";
                hhdAddTabEventId.Value = ht.ContainsKey("eventid") ? ht["eventid"].ToString() : "";
                hhdAddTabTitle.Value = ht.ContainsKey("title") ? ht["title"].ToString() : "";
            }
            catch { }
            finally
            {
                if (ht != null)
                {
                    ht.Clear();
                    ht = null;
                }
            }
        }
    }

    private void GetEventMenuList()
    {
        try
        {
            string securityGroups = string.Empty;
            if (System.Web.HttpContext.Current.Request.Cookies["ultraUserGroups"].IsNotNullOrEmptyEx() && Request.Cookies["ultraUserGroups"].Value.IsNotNullOrEmptyEx())    // 사용자 정보 Cookie 정보가 존재할경우
            {
                securityGroups = HttpContext.Current.Request.Cookies["ultraUserGroups"].Value;
            }

            List<DTO_USER_CONFIG_MENU_SORT> menuList;
            StringBuilder DocOrderList = new StringBuilder();
            using (Bayer.Ultra.BSL.Common.Mgr.CommonMgr_Nx mgr = new Bayer.Ultra.BSL.Common.Mgr.CommonMgr_Nx())
            {
                menuList = mgr.SelectUserConfigMenuSort(this.Sessions.UserID);

                if (menuList == null) return;

                foreach (DTO_USER_CONFIG_MENU_SORT menu in menuList)
                {
                    //< li >< a href = "#" class="openTab" data-iframe-src="ProductBriefing.aspx">Product Briefing</a></li>
                    if (this.Sessions.IsCrmUser)
                    {
                        //ver1.0.3 Consulting/abm CRM 유저는 보이지 않게 설정
                        if (menu.EVENT_ID.Equals("E0001") || menu.EVENT_ID.Equals("E0002") || menu.EVENT_ID.Equals("E0003") || menu.EVENT_ID.Equals("E0008")) continue;

                        //< VeeVa Roll -out : Create event by CRM user>
                        //if (menu.EVENT_ID.Equals("E0008")) continue;


                    }

                    HtmlGenericControl li = new HtmlGenericControl("li");
                    HtmlGenericControl anchor = new HtmlGenericControl("a");

                    anchor.Attributes.Add("href", "#");
                    anchor.Attributes.Add("class", "openTab");
                    anchor.Attributes.Add("data-iframe-src", string.Format("{0}/{1}", "/ultra/Pages/Event", menu.WEB_PAGE_NAME));
                    anchor.Attributes.Add("data-event-id", menu.EVENT_ID);
                    anchor.InnerText = menu.EVENT_NAME;

                    li.Controls.Add(anchor);
                    ulEventList.Controls.Add(li);

                    DocOrderList.AppendFormat("<tr id='htrDocID_{0}' event_id='{0}'><td class='doc_name'>{1}</td><td class='doc_sort'>{2}</td></tr>", menu.EVENT_ID, menu.EVENT_NAME, menu.SORT);
                }

                htblDocOrderList.InnerHtml = DocOrderList.ToString();

            }

            //library
            HtmlGenericControl liCompletedApproval = new HtmlGenericControl("li");
            HtmlGenericControl aCompletedApproval = new HtmlGenericControl("a");
            aCompletedApproval.Attributes.Add("href", "#");
            aCompletedApproval.Attributes.Add("class", "openTab");
            aCompletedApproval.Attributes.Add("data-event-id", "library-complete");
            aCompletedApproval.Attributes.Add("data-iframe-src", "/ultra/Pages/Library/CompletedList.aspx");
            aCompletedApproval.InnerText = "Complete";
            liCompletedApproval.Controls.Add(aCompletedApproval);
            ulLibrary.Controls.Add(liCompletedApproval);

            HtmlGenericControl liRejectApproval = new HtmlGenericControl("li");
            HtmlGenericControl aRejectApproval = new HtmlGenericControl("a");
            aRejectApproval.Attributes.Add("href", "#");
            aRejectApproval.Attributes.Add("class", "openTab");
            aRejectApproval.Attributes.Add("data-event-id", "library-reject");
            aRejectApproval.Attributes.Add("data-iframe-src", "/ultra/Pages/Library/RejectList.aspx");
            aRejectApproval.InnerText = "Reject";
            liRejectApproval.Controls.Add(aRejectApproval);
            ulLibrary.Controls.Add(liRejectApproval);

            HtmlGenericControl liWithdrawApproval = new HtmlGenericControl("li");
            HtmlGenericControl aWithdrawApproval = new HtmlGenericControl("a");
            aWithdrawApproval.Attributes.Add("href", "#");
            aWithdrawApproval.Attributes.Add("class", "openTab");
            aWithdrawApproval.Attributes.Add("data-event-id", "library-withdraw");
            aWithdrawApproval.Attributes.Add("data-iframe-src", "/ultra/Pages/Library/WithdrawList.aspx");
            aWithdrawApproval.InnerText = "Withdraw";
            liWithdrawApproval.Controls.Add(aWithdrawApproval);
            ulLibrary.Controls.Add(liWithdrawApproval);


            HtmlGenericControl liCancelApproval = new HtmlGenericControl("li");
            HtmlGenericControl aCancelApproval = new HtmlGenericControl("a");
            aCancelApproval.Attributes.Add("href", "#");
            aCancelApproval.Attributes.Add("class", "openTab");
            aCancelApproval.Attributes.Add("data-event-id", "library-cancel");
            aCancelApproval.Attributes.Add("data-iframe-src", "/ultra/Pages/Library/CancelList.aspx");
            aCancelApproval.InnerText = "Cancel";
            liCancelApproval.Controls.Add(aCancelApproval);
            ulLibrary.Controls.Add(liCancelApproval);

            //Medical
            HtmlGenericControl liMedicalStudy = new HtmlGenericControl("li");
            HtmlGenericControl aMedicalStudy = new HtmlGenericControl("a");
            aMedicalStudy.Attributes.Add("href", "#");
            aMedicalStudy.Attributes.Add("class", "openTab");
            aMedicalStudy.Attributes.Add("data-iframe-src", "/ultra/Pages/Medical/Study/StudyList.aspx");
            aMedicalStudy.InnerText = "Study List";
            liMedicalStudy.Controls.Add(aMedicalStudy);
            ulmedical.Controls.Add(liMedicalStudy);

            //Medical
            HtmlGenericControl liMedicalPms = new HtmlGenericControl("li");
            HtmlGenericControl aMedicalPms = new HtmlGenericControl("a");
            aMedicalPms.Attributes.Add("href", "#");
            aMedicalPms.Attributes.Add("class", "openTab");
            aMedicalPms.Attributes.Add("data-iframe-src", "/ultra/Pages/Medical/Pms/PmsMain.aspx");
            aMedicalPms.InnerText = "PMS";
            liMedicalPms.Controls.Add(aMedicalPms);
            ulmedical.Controls.Add(liMedicalPms);


            //Medical
            HtmlGenericControl liReceipt = new HtmlGenericControl("li");
            HtmlGenericControl aReceipt = new HtmlGenericControl("a");
            aReceipt.Attributes.Add("href", "#");
            aReceipt.Attributes.Add("class", "openTab");
            aReceipt.Attributes.Add("data-iframe-src", "/ultra/Pages/Report/ReceiptForFreeGood.aspx");
            aReceipt.InnerText = "Receipt For Free Good";
            liReceipt.Controls.Add(aReceipt);
            ulReport.Controls.Add(liReceipt);

            //Admin
            HtmlGenericControl liAdminApproval = new HtmlGenericControl("li");
            HtmlGenericControl aAdminApproval = new HtmlGenericControl("a");
            aAdminApproval.Attributes.Add("href", "#");
            aAdminApproval.Attributes.Add("class", "openTab");
            aAdminApproval.Attributes.Add("data-event-id", "library-admin");
            aAdminApproval.Attributes.Add("data-iframe-src", "/ultra/Pages/Library/AdminList.aspx");
            aAdminApproval.InnerText = "Admin";
            liAdminApproval.Controls.Add(aAdminApproval);
            ulReport.Controls.Add(liAdminApproval);

            //Micro Marketing
            //securityGroups에 포함되지 않으면 보이지 않는다
            if (securityGroups.IndexOf(Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.RAD_USER) != -1 || securityGroups.IndexOf(Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.RAD_KEY_USER) != -1 || securityGroups.IndexOf(Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.RAD_KEY_USER2) != -1)
            {
                HtmlGenericControl liMicroMarketing = new HtmlGenericControl("li");
                HtmlGenericControl aMicroMarketing = new HtmlGenericControl("a");
                aMicroMarketing.Attributes.Add("href", "#");
                aMicroMarketing.Attributes.Add("class", "openTab");
                aMicroMarketing.Attributes.Add("data-iframe-src", "/ultra/Pages/Report/MicroMarketing.aspx");
                aMicroMarketing.InnerText = "Micro Marketing";
                liMicroMarketing.Controls.Add(aMicroMarketing);
                ulReport.Controls.Add(liMicroMarketing);
            }


            //Non One Key
            HtmlGenericControl liNonOneKey = new HtmlGenericControl("li");
            HtmlGenericControl aNonOneKey = new HtmlGenericControl("a");
            aNonOneKey.Attributes.Add("href", "#");
            aNonOneKey.Attributes.Add("class", "openTab");
            aNonOneKey.Attributes.Add("data-iframe-src", "/ultra/Pages/Report/NonOneKey.aspx");
            aNonOneKey.InnerText = "Non Onekey";
            liNonOneKey.Controls.Add(aNonOneKey);
            ulReport.Controls.Add(liNonOneKey);

            //HCP Inquiry
            HtmlGenericControl liHCPInquiry = new HtmlGenericControl("li");
            HtmlGenericControl aHCPInquiry = new HtmlGenericControl("a");
            aHCPInquiry.Attributes.Add("href", "#");
            aHCPInquiry.Attributes.Add("class", "openTab");
            aHCPInquiry.Attributes.Add("data-iframe-src", "/ultra/Pages/Report/HCPInquiry.aspx");
            aHCPInquiry.InnerText = "HCP Inquiry";
            liHCPInquiry.Controls.Add(aHCPInquiry);
            ulReport.Controls.Add(liHCPInquiry);

            // version 1.0.7 HCP validation function for Easy On
            //HCP Search
            //SYSTEM_DESINGER, HCP_SEARCH_KEY_USER,SYSTEM_ADMIN
            //securityGroups에 포함되지 않으면 보이지 않는다
            if (securityGroups.IndexOf(Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.SYSTEM_DESINGER) != -1 || securityGroups.IndexOf(Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.HCP_SEARCH_KEY_USER) != -1 || securityGroups.IndexOf(Bayer.Ultra.Framework.Common.ApprovalUtil.SECURITY_GROUP.SUPPORT_USER) != -1)
            {
                HtmlGenericControl liMicroMarketing = new HtmlGenericControl("li");
                HtmlGenericControl aMicroMarketing = new HtmlGenericControl("a");
                aMicroMarketing.Attributes.Add("href", "#");
                aMicroMarketing.Attributes.Add("class", "openTab");
                aMicroMarketing.Attributes.Add("data-iframe-src", "/ultra/Pages/Report/HCPSearch.aspx");
                aMicroMarketing.InnerText = "HCP Search";
                liMicroMarketing.Controls.Add(aMicroMarketing);
                ulReport.Controls.Add(liMicroMarketing);
            }

        }
        catch (Exception ex)
        {

        }
    }
}