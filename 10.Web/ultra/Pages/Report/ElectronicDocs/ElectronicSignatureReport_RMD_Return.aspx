<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ElectronicSignatureReport_RMD_Return.aspx.cs" Inherits="Pages_Report_ElectronicSignatureReport" %>

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
                                        <col style="width: 16%" />
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
                                                <b>반납 여부    <span id="txtReturnType" style="color:red;font-size:17px" runat="server"></span></b>
                                                <br />
                                                 <br/>※ 위와 같이 평가기간이 종료된 성능확인용 의료기기의 반환을 확인 합니다.					
                                                <br/>※ 본 확인증에 서명하는 것은 제공된 물품의 반환확인 목적으로 바이엘코리아에서 하기 개인정보(소속기관, 성명)를 수집·이용하는것에 동의하는 것으로 간주됩니다.
                                                <br/>※ I confirmed that the return of the medical device for performance evaluation after the evaluation period as described above.					
                                                <br/>※ By signing this confirmation, it will be considered to give consent to collect and use the following personal information(affiliation, name) from Bayer Korea Ltd. For the purpose of confirming return of the product provided.					
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
