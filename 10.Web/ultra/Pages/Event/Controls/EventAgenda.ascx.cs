using Bayer.Ultra.Framework.Common.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Event_Controls_EventAgenda : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void SetRoleType(string roleType)
    {
        rdoRoleTypeLecture.Checked = false;
        rdoRoleTypeConsulting.Checked = false;
        rdoRoleTypeLecture.Disabled = true;
        rdoRoleTypeConsulting.Disabled = true;

        if (roleType.ToLower().Equals("lecture"))
        {
            rdoRoleTypeLecture.Checked = true;
        }
        else if (roleType.ToLower().Equals("consulting"))
        {
            rdoRoleTypeConsulting.Checked = true;
        }
        else
        {
            rdoRoleTypeLecture.Disabled = false;
            rdoRoleTypeConsulting.Disabled = false;
        }

    }
}