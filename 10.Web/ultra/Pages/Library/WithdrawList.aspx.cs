﻿using Bayer.Ultra.Framework.Common.Dto.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class Pages_Library_WithdrawList : Bayer.Ultra.WebBase.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            InitControls();
    }

    private void InitControls()
    {
        Master.UserID = Sessions.UserID;
    }
}