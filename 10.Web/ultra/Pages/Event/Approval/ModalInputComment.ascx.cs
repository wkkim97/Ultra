using Bayer.Ultra.Framework.Common.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Event_Approval_ModalInputComment : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    public void SetInputCommentCategory(List<DTO_COMMON_CODE_SUB> categories)
    {
        selInputCommentType.Items.Clear();
        this.selInputCommentType.Items.Add(new ListItem("-- 선택바랍니다. --", "0000"));
        foreach (DTO_COMMON_CODE_SUB category in categories)
        {
            this.selInputCommentType.Items.Add(new ListItem(category.CODE_NAME, category.SUB_CODE));
        }
        this.selInputCommentType.Items[0].Selected = true;
    }
}