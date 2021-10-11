using Bayer.Ultra.Framework.Common.Dto.Report;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Pages_Report_ElectronicSignatureReport : System.Web.UI.Page // Bayer.Ultra.WebBase.PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            InitPage();
    }

    private void InitPage()
    {
        string processid = string.Empty, idx = string.Empty;

        if(Request["processid"] != null)
        {
            processid = Request["processid"].ToString();
        }
        if (Request["idx"] != null)
        {
            idx = Request["idx"].ToString();
        }

        using (Bayer.Ultra.BSL.Report.Mgr.FreeGoodMgr oMgr = new Bayer.Ultra.BSL.Report.Mgr.FreeGoodMgr())
        {
            ReceiptForFreeGoodDto dto =  oMgr.SelectReceiptItem(processid, idx);

            if(dto != null)
            {
                txtProduct.InnerText = dto.PRODUCT_NAME;
                txtPurpose.InnerText = dto.PURPOSE ;
                txtQty.InnerText = dto.QTY.ToString() ;
                txtSignDate.InnerText = dto.RECEIPT_DATE;
                txtHcoName.InnerText = dto.HCO_NAME;
                txtDivision.InnerText = dto.BU;
                txtProductCode.InnerText = dto.PRODUCT_CODE;
                txtEventKey.InnerText = dto.EVENT_KEY;
                txtPackage.InnerText = dto.STD_CODE;
                txtHcpName.InnerText = dto.HCP_NAME;
              
                txtRequestName.InnerText = dto.REQUESTER_NAME;
                hhdProcessId.Value = dto.PROCESS_ID;
                hhdIdx.Value = dto.IDX.ToString();
                hhdEventKey.Value = dto.EVENT_KEY;
                imgSign.Src = dto.SIGN_IMG_URL ;
                txtReturnType.InnerText = dto.RETURN_COMMENT;
            }
        }
    }



}