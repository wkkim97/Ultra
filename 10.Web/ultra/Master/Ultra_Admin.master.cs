using Bayer.Ultra.WebBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Master_Ultra_Admin : System.Web.UI.MasterPage
{
    PageBase _pageBase;

    protected override void OnInit(EventArgs e)
    {
        this._pageBase = this.Page as PageBase;
        base.OnInit(e);
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!this.IsPostBack)
        {
            this.spanUserName.InnerText = _pageBase.Sessions.UserName;
            this.spanOrgName.InnerText = string.Format(" | {0}", _pageBase.Sessions.OrgName);
            this.hddUserID.Value = _pageBase.Sessions.UserID;
            CreateMenu();
        }
    }

    private void CreateMenu()
    {
        //UlTra User
        HtmlGenericControl liExcelUpload = new HtmlGenericControl("li");
        HtmlGenericControl aExcelUpload = new HtmlGenericControl("a");
        aExcelUpload.Attributes.Add("href", "/ultra/Pages/Admin/ExcelUpload.aspx");
        aExcelUpload.InnerText = "Excel Upload";
        liExcelUpload.Controls.Add(aExcelUpload);
        ulUlTraUser.Controls.Add(liExcelUpload);

        HtmlGenericControl liMedicalSociety = new HtmlGenericControl("li");
        HtmlGenericControl aMedicalSociety = new HtmlGenericControl("a");
        aMedicalSociety.Attributes.Add("href", "/ultra/Pages/Admin/MedicalSociety.aspx");
        aMedicalSociety.InnerText = "Medical Society";
        liMedicalSociety.Controls.Add(aMedicalSociety);
        ulUlTraUser.Controls.Add(liMedicalSociety);

        HtmlGenericControl liConcurHistory = new HtmlGenericControl("li");
        HtmlGenericControl aConcurHistory = new HtmlGenericControl("a");
        aConcurHistory.Attributes.Add("href", "/ultra/Pages/Admin/ConcurHistory.aspx");
        aConcurHistory.InnerText = "Concur History";
        liConcurHistory.Controls.Add(aConcurHistory);
        ulUlTraUser.Controls.Add(liConcurHistory);

        HtmlGenericControl liICCMaster = new HtmlGenericControl("li");
        HtmlGenericControl aICCMaster = new HtmlGenericControl("a");
        aICCMaster.Attributes.Add("href", "/ultra/Pages/Admin/ICCMaster.aspx");
        aICCMaster.InnerText = "ICC Master";
        liICCMaster.Controls.Add(aICCMaster);
        ulUlTraUser.Controls.Add(liICCMaster);


        HtmlGenericControl liParticipants = new HtmlGenericControl("li");
        HtmlGenericControl aParticipants = new HtmlGenericControl("a");
        aParticipants.Attributes.Add("href", "/ultra/Pages/Admin/AddParticipants.aspx");
        aParticipants.InnerText = "Add Participants";
        liParticipants.Controls.Add(aParticipants);
        ulUlTraUser.Controls.Add(liParticipants);

        HtmlGenericControl liHcpSearch = new HtmlGenericControl("li");
        HtmlGenericControl aHcpSearch = new HtmlGenericControl("a");
        aHcpSearch.Attributes.Add("href", "/ultra/Pages/Admin/HcpSearch.aspx");
        aHcpSearch.InnerText = "HCP Search";
        liParticipants.Controls.Add(aHcpSearch);
        ulUlTraUser.Controls.Add(liHcpSearch);

        //Micromarketing function
        HtmlGenericControl radMicro = new HtmlGenericControl("li");
        HtmlGenericControl aradMicro = new HtmlGenericControl("a");
        aHcpSearch.Attributes.Add("href", "/ultra/Pages/Admin/RAD_Micro_Marketing.aspx");
        aHcpSearch.InnerText = "RAD Micro Master";
        liParticipants.Controls.Add(radMicro);
        ulUlTraUser.Controls.Add(aradMicro);


        //System Admin
        HtmlGenericControl liEventConfiguration = new HtmlGenericControl("li");
        HtmlGenericControl aEventConfiguration = new HtmlGenericControl("a");
        aEventConfiguration.Attributes.Add("href", "/ultra/Pages/Admin/EventConfiguration.aspx");
        aEventConfiguration.InnerText = "Event Configuration";
        liEventConfiguration.Controls.Add(aEventConfiguration);
        ulSystemAdmin.Controls.Add(liEventConfiguration);


        //Report  
        HtmlGenericControl lireport0 = new HtmlGenericControl("li");
        HtmlGenericControl areport0 = new HtmlGenericControl("a");
        areport0.Attributes.Add("href", "/ultra/Pages/Report/MOHW/MohwReportList.aspx");
        areport0.InnerText = "MOHW Report List";
        lireport0.Controls.Add(areport0);
        ulReport.Controls.Add(lireport0);

        //Report  
        HtmlGenericControl lireport01 = new HtmlGenericControl("li");
        HtmlGenericControl areport01 = new HtmlGenericControl("a");
        areport01.Attributes.Add("href", "/ultra/Pages/Report/MOHW/DivMedical.aspx");
        areport01.InnerText = "개별요양기관 제품 설명회";
        lireport01.Controls.Add(areport01);
        ulReport.Controls.Add(lireport01);

        HtmlGenericControl lireport02 = new HtmlGenericControl("li");
        HtmlGenericControl areport02 = new HtmlGenericControl("a");
        areport02.Attributes.Add("href", "/ultra/Pages/Report/MOHW/PluralityMedical.aspx");
        areport02.InnerText = "복수요양기관 제품 설명회";
        lireport02.Controls.Add(areport02);
        ulReport.Controls.Add(lireport02);

        HtmlGenericControl lireport03 = new HtmlGenericControl("li");
        HtmlGenericControl areport03 = new HtmlGenericControl("a");
        areport03.Attributes.Add("href", "/ultra/Pages/Report/MOHW/Sample.aspx");
        areport03.InnerText = "견본품 제공";
        lireport03.Controls.Add(areport03);
        ulReport.Controls.Add(lireport03);

        HtmlGenericControl lireport05 = new HtmlGenericControl("li");
        HtmlGenericControl areport05 = new HtmlGenericControl("a");
        areport05.Attributes.Add("href", "/ultra/Pages/Report/MOHW/Participants.aspx");
        areport05.InnerText = "학술대회 지원";
        lireport05.Controls.Add(areport05);
        ulReport.Controls.Add(lireport05);

        HtmlGenericControl lireport06 = new HtmlGenericControl("li");
        HtmlGenericControl areport06 = new HtmlGenericControl("a");
        areport06.Attributes.Add("href", "/ultra/Pages/Report/MOHW/MedicalStudy.aspx");
        areport06.InnerText = "임상시험 지원";
        lireport06.Controls.Add(areport06);
        ulReport.Controls.Add(lireport06);

        HtmlGenericControl lireport07 = new HtmlGenericControl("li");
        HtmlGenericControl areport07 = new HtmlGenericControl("a");
        areport07.Attributes.Add("href", "/ultra/Pages/Report/MOHW/MarketResearch.aspx"); 
        areport07.InnerText = "시판 후 조사";
        lireport07.Controls.Add(areport07);
        ulReport.Controls.Add(lireport07);

        //version 1.0.6 KRPIA Report
        HtmlGenericControl lireport08 = new HtmlGenericControl("li");
        HtmlGenericControl areport08 = new HtmlGenericControl("a");
        areport08.Attributes.Add("href", "/ultra/Pages/Report/MOHW/krpia.aspx");
        areport08.InnerText = "KRPIA";
        lireport08.Controls.Add(areport08);
        ulReport.Controls.Add(lireport08);

    }
}
