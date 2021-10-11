using Bayer.Ultra.Framework.Common.Dto.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Event_Controls_EventPayment : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void SetCategory(List<DTO_WF_CONFIG_COST_CATEGORY> categories)
    {
        selInputSRMCategory.Items.Clear();
        foreach (DTO_WF_CONFIG_COST_CATEGORY category in categories)
        {
            this.selInputSRMCategory.Items.Add(new ListItem(category.CATEGORY_NAME, category.CATEGORY_CODE));
        }
    }
}