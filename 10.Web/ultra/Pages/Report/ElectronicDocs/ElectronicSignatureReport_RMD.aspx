<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ElectronicSignatureReport_RMD.aspx.cs" Inherits="Pages_Report_ElectronicSignatureReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="shortcut icon" href="/ultra/Styles/images/favicon.ico" media="all" />
    <link rel="stylesheet" href="/ultra/Styles/Css/font-awesome.min.css" media="all" />
    <link rel="stylesheet" href="/ultra/Styles/Bootstrap/css/bootstrap.css" />
    <link rel="stylesheet" href="/ultra/Styles/JSGrid/jsgrid.css" />
    <link rel="stylesheet" href="/ultra/Styles/JSGrid/jsgrid-theme-dn.css" />
    <link rel="stylesheet" href="/ultra/Styles/Bootstrap/css/bootstrap-datetimepicker.css" />
    <link rel="stylesheet" href="/ultra/Styles/Css/Style.css" />
    <link href="../../Styles/Css/font-awesome.min.css" rel="stylesheet" />
    <script type="text/javascript" src="/ultra/Scripts/JQuery/jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="/ultra/Styles/Bootstrap/js/bootstrap.js"></script>
    <script type="text/javascript" src="/ultra/Styles/JSGrid/jsgrid.js"></script>
    <script type="text/javascript" src="/ultra/Styles/Bootstrap/js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/Pages/FormEvent.js"></script>
    <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.core.js"></script>
    <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.plugin.ajax.js"></script>
    <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.plugin.arrow.js"></script>
    <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.plugin.autocomplete.js"></script>
    <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.plugin.clear.js"></script>
    <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.plugin.filter.js"></script>
    <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.plugin.focus.js"></script>
    <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.plugin.prompt.js"></script>
    <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.plugin.suggestions.js"></script>
    <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.plugin.tags.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/Common/UserAutocompleteBox.js"></script>
    <script src="/ultra/Scripts/Common/Common.js"></script>
    <script src="/ultra/Scripts/Common/Command.js"></script>
    <script type="text/javascript">
        /*
        $(function () {
            try {
             
                var d = $.Deferred();
                var processId = $.urlParam("processid"), idx = $.urlParam("idx");
                var selectUrl = REPORT_SERVICE_URL + "/SelectReceiptForFreeGoodItem/" + processId + "/" + idx;
                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (item) {
                    d.resolve(item);
                    if (item != null) {
                        $("#txtProduct").html(item.PRODUCT_NAME);
                        $("#txtPurpose").html(item.PURPOSE);
                        $("#txtQty").html(item.QTY);
                        $("#txtSignDate").html(item.RECEIPT_DATE);
                        $("#txtHcoName").html(item.HCO_NAME);
                        $("#hhdHcoCode").html(item.HCO_CODE);
                        $("#txtHcpName").html(item.HCP_NAME);
                        $("#hhdHcpCode").html(item.HCP_CODE);
                        $("#txtRequestName").html(item.REQUESTER_NAME);
                        $("#hhdProcessId").val(item.PROCESS_ID);
                        $("#hhdIdx").val(item.IDX);
                        $("#hhdEventKey").val(item.EVENT_KEY);
                        $("imgSign").attr("src", item.SIGN_IMG_URL);
                    }
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    d.reject(jqXHR);
                });
                d.promise();
            }
            catch (ex) {
                fn_showError({ message: ex.message });
            }
        });
        */
         
        $.urlParam = function(name){

            var results = new RegExp('[\?&amp;]' + name + '=([^&amp;#]*)').exec(window.location.href);

            return results[1] || 0;

        }
         
    </script>
    <title></title>
</head>
<body style="background-color:#fff">
    <form id="form1" runat="server">
        <div id="wrap" >
     <div class="modal-Report">
                        <div class="modal-header"> 
                            <h4 class="modal-title"  >Sample Acknowledgment Form</h4>
                        </div>
                        <div class="modal-body">
                            <div class="modal-table">
                                <table class="write">
                                    <colgroup>
                                        <col style="width: 30%" />
                                        <col />
                                    </colgroup>
                                    <tbody>
                                        <tr>
                                            <th scope="row">Division</th>
                                            <td>
                                                <span id="txtDivision" runat="server"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th scope="row">No</th>
                                            <td>
                                                <span id="txtEventKey" runat="server"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th scope="row">Product(품명)</th>
                                            <td>
                                                <span id="txtProduct" runat="server"></span> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <th scope="row">Product Code(제품코드)</th>
                                            <td>
                                                <span id="txtProductCode" runat="server"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th scope="row">Package(포장내총수량-규격)</th>
                                            <td>
                                                <span id="txtPackage" runat="server"></span>
                                            </td>
                                        </tr>
                                        <tr >
                                            <th scope="row">Purpose(목적)</th>
                                            <td>
                                               <span id="txtPurpose" runat="server"></span>
                                            </td>
                                        </tr>
                                        <tr >
                                            <th scope="row">Quantity(수량)</th>
                                            <td>
                                                <span id="txtQty" runat="server"></span>
                                            </td>
                                        </tr> 
                                        <tr > 
                                            <td colspan="2"> 
                                                <br/>※ 위와 같이 인수 하였으며, 제공 받은 견본품을 환자에게 사용할 경우 그 비용을 환자에게 별도로 청구할 수 없습니다.					
                                                <br/>※ 본 견본품은 관계법령(의료기기법 제13조의2, 의료기기 유통 및 판매질서 유지에 관한 규칙 제3조 등)에 따라 지출보고서로 제출될 수 있으며, 상기 견본품은 의료기기법 및 공정경쟁규약에 따라 제공됩니다. 					
                                                <br/>※ 평가용으로 제공되는 의료기기는 공정경쟁규약에서 규정한 바에 따라 필요한 최소한의 기간(1개월 이내) 동안만 제공할 수 있습니다.
                                                <br/>※ 평가기간 동안 평가용 제품의 소유권은 바이엘코리아㈜에 있으며, 이를 이전하여서는 안됩니다. 					
                                                <br/>※ 평가기간이 종료하면 보건의료인 또는 의료기관이 해당 제품을 구매하지 않는 한, 해당 제품은 바이엘코리아로 반환됩니다.					
                                                <br/>※ 본 인수증에 서명하는 것은 제공된 물품의 수령확인 목적으로 바이엘코리아에서 하기 개인정보(소속기관, 성명)를 수집·이용하는것에 동의하는 것으로 간주됩니다.
                                                <br/>
                                                <br/>※ I acknowledge and agree that I received the samples as listed above, and if the samples are used on the patient it can not be charged to the patient separately.					
                                                <br/>※ These samples may be submitted in expense reports pursuant to the relevant laws (Article 13-2 of the Medical Devices Act and Article 3 of the Rules on Distribution of Medical Devices and Maintenance of Order in Sales).  The above samples are provided in compliance with the Medical Devices Act and the Fair Competition Code. 					
                                                <br/>※ Medical devices provided for evaluation may only be provided for the minimum required period (within one month) as indicated by the Fair Competition Code. 					
                                                <br/>※ During the evaluation period, the above medical device for performance evaluation are owned by Bayer Korea and it can't be transferred.	
                                                <br/>※ After the end of the evaluation period, the product will be returned to Bayer Korea unless the HCO or HCP purchase the product.					
                                                <br/>※ By signing this receipt, it will be considered to give consent to collect and use the following personal information(affiliation, name) from Bayer Korea Ltd. For the purpose of confirming receipt of the product provided.					

                                            </td>
                                        </tr>
                                        <tr>
                                            <th scope="row">일자</th>
                                            <td>
                                                 <span id="txtSignDate" runat="server"></span>
                                            </td>
                                        </tr> 
                                        <tr>
                                            <th scope="row">인수기관</th>
                                            <td>
                                                <span id="txtHcoName" runat="server"></span>
                                                <input type="hidden" id="hhdHcoCode"/> 
                                            </td>
                                        </tr> 
                                        <tr>
                                            <th scope="row">인수자</th>
                                            <td>
                                                <span id="txtHcpName" runat="server"></span>
                                                <input type="hidden" id="hhdHcpCode"/> 
                                            </td>
                                        </tr>  
                                        <tr>
                                            <th scope="row">Signature</th>
                                            <td> 
                                               <div class="signature-pad" > 
                                                    <img id="imgSign" runat="server" /> 
                                                </div>  
                                            </td>
                                        </tr>
                                        <tr>
                                            <th scope="row">바이엘 담당자</th>
                                            <td>
                                                <span id="txtRequestName" runat="server"></span> 
                                            </td>
                                        </tr> 
                                    </tbody>
                                </table>
                            </div>
                            
                        </div>
                        <div class="modal-footer">  
                            <input type="hidden" id="hhdProcessId"  runat="server"/>
                            <input type="hidden" id="hhdIdx" runat="server" />
                            <input type="hidden" id="hhdEventKey" runat="server" />
                        </div>
                    </div>

            <input type="hidden" id="hddUserID" runat="server" />
        </div>
    </form>
</body>
</html>
