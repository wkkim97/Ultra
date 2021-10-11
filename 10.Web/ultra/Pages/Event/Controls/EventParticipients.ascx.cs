using Bayer.Ultra.Framework.Common.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Event_Controls_EventParticipients : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void SetCountry(List<DTO_MASTER_COUNTRY> countries)
    {
        lb_country.Items.Clear();
        foreach (DTO_MASTER_COUNTRY country in countries)
        {
            this.lb_country.Items.Add(new ListItem(country.COUNTRY_NAME, country.COUNTRY_CODE));
        }
    }

    public void SetOtherHCP(string eventID, string org)
    {
        if (eventID.Equals("E0007"))
        {
            this.divHCPType.Visible = true;
            this.divHCOSearcher.Visible = true;
            //2021.06.10 HCO 입력 할 수 있게 변경
            this.divInputHCOName.Visible = true;
            this.selHCPType.Items.Add(new ListItem("약사", "Pharmacist"));
            this.selHCPType.Items.Add(new ListItem("간호사", "Nurse"));
            this.selHCPType.Items.Add(new ListItem("인턴(레지던트)", "Intern"));
            this.selHCPType.Items.Add(new ListItem("방사선사", "Radiologist"));
            this.selHCPType.Items.Add(new ListItem("Foreigner", "Foreigner"));
            this.selHCPType.Items.Add(new ListItem("의사(PMS only)", "Doctor_PMS"));
            this.selHCPType.Items.Add(new ListItem("Business Guest", "Guest"));
            this.liContract.Visible = true;
            this.divHCPCountry.Attributes.Add("style", "display:none");
        }
        else
        {
            if (org.StartsWith("BKL-CH"))
            {
                this.divHCPType.Visible = true;
                this.divHCOSearcher.Visible = true;
                this.divInputHCOName.Visible = true;
                this.selHCPType.Items.Clear();
                this.selHCPType.Items.Add(new ListItem("약사", "Pharmacist"));
                this.selHCPType.Items.Add(new ListItem("간호사", "Nurse"));
                this.selHCPType.Items.Add(new ListItem("Foreigner", "Foreigner"));
                this.selHCPType.Items.Add(new ListItem("Business Guest", "Guest"));
                this.divHCPCountry.Attributes.Add("style", "display:none");
                this.divInputHCOName.Attributes.Add("style", "display:none");
            }
            else
            {
                this.selHCPType.Items.Clear();
                this.selHCPType.Items.Add(new ListItem("Foreigner", "Foreigner"));
                this.selHCPType.Items.Add(new ListItem("Business Guest", "Guest"));
                //this.selHCPType.Disabled = true;
                this.divHCPType.Visible = true;                
                this.divHCOSearcher.Visible = false;
                this.divInputHCOName.Visible = true;
            }
            this.liContract.Visible = false;
        }

    }
}