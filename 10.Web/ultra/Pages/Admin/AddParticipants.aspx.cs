using Bayer.Ultra.BSL.Common.Mgr;
using Bayer.Ultra.Framework.Common.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Admin_AddParticipants : Bayer.Ultra.WebBase.AdminPageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SetCountry();
        SetOtherHCP();
    }

    public void SetCountry()
    {
        lb_country.Items.Clear();
        //국가 설정
        using (CommonMgr_Nx mgr = new CommonMgr_Nx())
        {
            List<DTO_MASTER_COUNTRY> countries = mgr.SelectCountry();
            foreach (DTO_MASTER_COUNTRY country in countries)
            {
                this.lb_country.Items.Add(new ListItem(country.COUNTRY_NAME, country.COUNTRY_CODE));
            }
        }
    }

    public void SetOtherHCP()
    {
        this.divHCPType.Visible = true;
        this.divHCOSearcher.Attributes.Add("style", "display:inline");
        this.divInputHCOName.Attributes.Add("style", "display:none");
        this.selHCPType.Items.Add(new ListItem("약사", "Pharmacist"));
        this.selHCPType.Items.Add(new ListItem("간호사", "Nurse"));
        this.selHCPType.Items.Add(new ListItem("인턴(레지던트)", "Intern"));
        this.selHCPType.Items.Add(new ListItem("방사선사", "Radiologist"));
        this.selHCPType.Items.Add(new ListItem("Foreigner", "Foreigner"));
        this.selHCPType.Items.Add(new ListItem("의사(PMS only)", "Doctor_PMS"));
        this.selHCPType.Items.Add(new ListItem("Business Guest", "Guest"));
        this.liContract.Visible = false;
        this.divHCPCountry.Attributes.Add("style", "display:none");
    }
}