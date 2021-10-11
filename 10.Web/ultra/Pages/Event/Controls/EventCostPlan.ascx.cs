using Bayer.Ultra.Framework.Common.Dto.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Event_Controls_EventCostPlan : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public List<DTO_WF_CONFIG_COST_CATEGORY> Category
    {
        get; set;
    }

    public void SetCategory(List<DTO_WF_CONFIG_COST_CATEGORY> categories)
    {
        lb_category.Items.Clear();
        foreach (DTO_WF_CONFIG_COST_CATEGORY category in categories)
        {
            this.lb_category.Items.Add(new ListItem(category.CATEGORY_NAME, category.CATEGORY_CODE));
        }
    }
}