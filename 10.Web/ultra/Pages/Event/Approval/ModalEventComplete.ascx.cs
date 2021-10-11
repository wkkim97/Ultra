using Bayer.Ultra.Framework.Common.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Event_Approval_ModalCompleteEvent : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void SetCommentCategory(List<DTO_COMMON_CODE_SUB> categories, string defaultCode)
    {
        selCommentType.Items.Clear();
        int i = 0;
        foreach (DTO_COMMON_CODE_SUB category in categories)
        {
            this.selCommentType.Items.Add(new ListItem(category.CODE_NAME, category.SUB_CODE));
            if (category.SUB_CODE.Equals(defaultCode))
                this.selCommentType.Items[i].Selected = true;
            i++;
        }
    }
    public void SetMedical()
    {
        medical_area.Visible = true;
    }
}