using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Common_Controls_ExcelFileUploader : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    [System.ComponentModel.BindableAttribute(true)]
    public ExcelUploadType UploadType { get; set; }
}

public enum ExcelUploadType
{
    PaymentConcur,
    PaymentYourDoces
}
