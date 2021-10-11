<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintReceiptFreeGood.aspx.cs" Inherits="Pages_Report_PrintReceiptFreeGood" %>


<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <link rel="shortcut icon" href="/ultra/Styles/images/favicon.ico" media="all" />
    <link rel="stylesheet" href="/ultra/Styles/Css/font-awesome.min.css" media="all" />
    <link rel="stylesheet" href="/ultra/Styles/Bootstrap/css/bootstrap.css" />
    <link rel="stylesheet" href="/ultra/Styles/JSGrid/jsgrid.css" />
    <link rel="stylesheet" href="/ultra/Styles/JSGrid/jsgrid-theme-dn.css" />
    <link rel="stylesheet" href="/ultra/Styles/Bootstrap/css/bootstrap-datetimepicker.css" />
    <link rel="stylesheet" href="/ultra/Styles/Css/Style.css" /> 
    <link rel="stylesheet" href="/ultra/Styles/Css/jquery.fileupload.css" />

    <script type="text/javascript" src="/ultra/Scripts/JQuery/jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="/ultra/Styles/Bootstrap/js/bootstrap.js"></script>
    <script type="text/javascript" src="/ultra/Styles/JSGrid/jsgrid.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/Pages/FormEvent.js"></script>
    <script src="/ultra/Scripts/Common/Common.js"></script>
    <script type="text/javascript"> 
        $(function () {
            try {
                var d = $.Deferred();
                var processid = $.urlParam("processid");
                var idx = $.urlParam("idx");


                var selectUrl = REPORT_SERVICE_URL + "/SelectReceiptForFreeGoodItem/" + processid + "/" + idx;
                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (item) {
                    $("#txtProduct").html(item.PRODUCT_NAME);
                    $("#txtPurpose").html(item.PURPOSE);
                    $("#txtDivision").html(item.BU);
                    $("#txtEventKey").html(item.EVENT_KEY);
                    $("#txtProductCode").html(item.PRODUCT_CODE);
                    $("#txtPackage").html(item.STD_CODE);
                    $("#txtQty").html(item.QTY);
                    $("#txtSignDate").html($("input[id$=hhdDate]").val());
                    $("#txtHcoName").html(item.HCO_NAME);
                    $("#hhdHcoCode").html(item.HCO_CODE);
                    $("#txtHcpName").html(item.HCP_NAME);
                    $("#hhdHcpCode").html(item.HCP_CODE);
                    $("#txtRequestName").html(item.REQUESTER_NAME);

                    window.print();
                });

            }
            catch (ex) {
                fn_showError({ message: ex.message });
            }
        });

        $.urlParam = function (name) {
            var results = new RegExp('[\?&amp;]' + name + '=([^&amp;#]*)').exec(window.location.href);
            return results[1] || 0;
        }
    </script>
    <title></title>
</head>
<body style="background-color:#fff">
    <form id="form1" runat="server">
        <div id="wrap" > 
            <div class="panel-topinfo">
                <div class="panel-heading">
                    <h1 class="panel-title">Sample Acknowledbement Form</h1> 
                </div>
                <div class="panel-body pd">
	                <div class="panel">
                        <div id="divInputEvent" class="panel-body">
                            <table class="table">
                                <colgroup>
                                    <col style="width: 16%" />
                                    <col />
                                </colgroup>
                                <tbody> 
                                    <tr>
                                        <th scope="row">Division</th>
                                        <td>
                                            <span id="txtDivision"></span> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">No</th>
                                        <td>
                                            <span id="txtEventKey"></span> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">Product(품명)</th>
                                        <td>
                                            <span id="txtProduct"></span> 
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">Product Code(제품코드)</th>
                                        <td>
                                            <span id="txtProductCode"></span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">Package(포장내총수량-규격)</th>
                                        <td>
                                            <span id="txtPackage"></span>
                                        </td>
                                    </tr>
                                    <tr >
                                        <th scope="row">Purpose(목적)</th>
                                        <td>
                                            <span id="txtPurpose"></span>
                                        </td>
                                    </tr>
                                    <tr >
                                        <th scope="row">Quantity(수량)</th>
                                        <td>
                                            <span id="txtQty"></span>
                                        </td>
                                    </tr> 
                                    <tr > 
                                        <td colspan="2"> 
                                            ※ 위와 같이 인수 하였으며, 제공 받은 견본품을 환자에게 재판매 또는 처방의 용도로 사용하지 않을 것임을 확인합니다.					
                                            <br />※ 본 견본품은 관계법령(약사법 제47조의2, 약사법 시행규칙 제44조의2, 의료기기법 제13조의2, 의료기기 유통 및 판매질서 유지에 관한 규칙 제3조 등)에 따라 지출보고서로 제출될 수 있으며, 상기 견본품은 약사법, 의료기기법 및 공정경쟁규약에 따라 제공됩니다. 					
                                            <br />※ 약품목록등록을 위하여 제공되는 견본품은 공정경쟁규약에서 규정한 바에 따라 등록절차완료 시 당사로 반환하여 주실 것을 부탁드립니다.					
					                        <br /> 
                                            <br />※ I acknowledge and agree that I received the samples as listed above, and that I shall not use the received samples for resale or prescription to patients.  					
                                            <br />※ These samples may be submitted in expense reports pursuant to the relevant laws (Article 47-2 of the Pharmaceutical Affairs Act, Article 44-2 of the Enforcement Rules of the Pharmaceutical Affairs Act, Article 13-2 of the Medical Devices Act and Article 3 of the Rules on Distribution of Medical Devices and Maintenance of Order in Sales).  The above samples are provided in compliance with the Pharmaceutical Affairs Act, the Medical Devices Act and the Fair Competition Code. 					
                                            <br />※ As for the samples provided for drug listing registration, please return them to us upon completion of the registration process pursuant to the Fair Competition Code. 					
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">일자</th>
                                        <td>
                                                <span id="txtSignDate"></span>
                                        </td>
                                    </tr> 
                                    <tr>
                                        <th scope="row">인수기관</th>
                                        <td>
                                            <span id="txtHcoName"></span>
                                            <input type="hidden" id="hhdHcoCode"/> 
                                        </td>
                                    </tr> 
                                    <tr>
                                        <th scope="row">인수자</th>
                                        <td>
                                            <span id="txtHcpName"></span>
                                            <input type="hidden" id="hhdHcpCode"/> 
                                        </td>
                                    </tr>  
                                    <tr>
                                        <th scope="row">Signature</th>
                                        <td> 
                                            <div class="signature-pad" style="height:150px;">
                                            </div>  
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">바이엘 담당자</th>
                                        <td>
                                            <span id="txtRequestName"></span> 
                                        </td>
                                    </tr> 
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div style="display:none;">
            <input type="hidden" id="hhdDate" runat="server" />
        </div>
    </form>
</body>
</html>
