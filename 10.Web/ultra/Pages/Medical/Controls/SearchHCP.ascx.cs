using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Medical_Controls_SearchHCP : System.Web.UI.UserControl
{
    public string JSOBJECTNAME { get; set; }

    /// <summary>
    /// 팝업인지 임베디드인지 구분('M', 'I')
    /// </summary>
    public string TYPE { get; set; }
    protected void Page_Load(object sender, EventArgs e)
    {
        try
        {
            if (!this.IsPostBack)
            {
                PageInit();
            }
        }
        catch (Exception ex)
        {
        }

    }

    private void PageInit()
    {
        StringBuilder sb = new StringBuilder(128);
        try
        {
            //Page.ClientScript.RegisterClientScriptInclude("Registration", ResolveUrl("../../../Scripts/Pages/Medical/SearchHCP.js"));

            sb.AppendFormat(" var {0} = null; ", JSOBJECTNAME);
            sb.Append(" $(document).ready(function() {");
            sb.Append(" if ( " + JSOBJECTNAME + " == null) {");
            sb.AppendFormat(" {0} = new SearchHCP();", JSOBJECTNAME);
            sb.AppendFormat(" {0}.Init();", JSOBJECTNAME);
            sb.Append(" }");
            sb.Append(" });");
            ClientScriptManager cs = Page.ClientScript;
            cs.RegisterClientScriptBlock(this.GetType(), "AddSearchHCP", sb.ToString(), true);
        }
        catch (Exception ex)
        {
            throw ex;
        }
        finally
        {
            if(sb != null)
            {
                sb.Clear();
                sb = null;
            }
        }
    }
}