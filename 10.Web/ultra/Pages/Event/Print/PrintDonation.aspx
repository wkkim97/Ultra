<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintDonation.aspx.cs" Inherits="Pages_Event_Print_PrintYourDocesCover" %>

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
                var processId = $.urlParam("processid");
                var selectUrl = EVENT_SERVICE_URL + "/SelectDonation/" + processId;
                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (data) {
                    d.resolve(data);
                    if (data != null) {

                        //Recipient Information
                        $("#spanRecipient").text(data.RECIPIENT + " (sign)");
                        $("#spanAddress").text(data.ADDRESS);
                        $("#spanTel").text(data.TEL);
                        $("#spanEMail").text(data.EMAIL);
                        $("input[name=rdoEligibility]").each(function () {
                            if ($(this).val() == data.IS_ELIGIBILITY)
                                $(this).prop('checked', true);
                            else
                                $(this).prop('checked', false);
                        });
                        $("input[name=rdoReceipt]").each(function () {
                            if ($(this).val() == data.AFTER_RECEIPT)
                                $(this).prop('checked', true);
                            else
                                $(this).prop('checked', false);
                        });
                        $("#spanComment").html(data.COMMENT.replace(/(?:\r\n|\r|\n)/g, '<br />'));
                        var categorys = data.CATEGORY.split(";");
                        $("input[name=hchkCategory]").each(function () {
                            $(this).prop('checked', false);
                            for (var i = 0; i < categorys.length; i++) {
                                if ($(this).val() == categorys[i].trim())
                                    $(this).prop('checked', true);
                            }
                        });

                        var fields = [
                                { name: "BU", title: "BU", type: "text", width: 20 },
                                { name: "PRODUCT_CODE", title: "Product", type: "text", css: "hide", width: 0 },
                                { name: "PRODUCT_NAME", title: "Product", type: "text" },
                                {
                                    name: "QTY", title: "Quantitly", type: "number", width: 50, align: "right",
                                    itemTemplate: function (value, item) {
                                        return fn_AddComma(value);
                                    }
                                },
                                {
                                    name: "BASE_PRICE", title: "Price", type: "number", width: 50, align: "right",
                                    itemTemplate: function (value, item) {
                                        return fn_AddComma(value);
                                    }
                                },
                                {
                                    name: "AMOUNT", title: "Amount", type: "number", width: 50, align: "right",
                                    itemTemplate: function (value, item) {
                                        return fn_AddComma(value);
                                    }
                                }
                        ];

                        // Product Grid 선언부
                        $("#jsGridProduct").jsGrid({
                            width: "100%",
                            sorting: false,
                            paging: false,
                            autoload: false,
                            editing: false,
                            
                            controller: {
                                loadData: function () {
                                    var selectUrl = EVENT_SERVICE_URL + "/SelectDonationProducts/" + data.PROCESS_ID;
                                    var d = $.Deferred();

                                    $.ajax({
                                        url: selectUrl,
                                        type: "GET",
                                        dataType: "json",
                                        cache: false,
                                        contentType: "application/json; charset=utf-8",
                                    }).done(function (response) {
                                        d.resolve(response);
                                    }).fail(function (jqXHR, textStatus, errorThrown) {
                                        d.reject(jqXHR);
                                    });
                                    return d.promise();
                                }
                            },
                            fields: fields

                        }).jsGrid("loadData");

                        //window.print();
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

        $.urlParam = function (name) {
            var results = new RegExp('[\?&amp;]' + name + '=([^&amp;#]*)').exec(window.location.href);
            return results[1] || 0;
        }
    </script>
    <style type="text/css">
        .write tbody tr {
            height:30px;
        }
        .jsgrid-grid-header,
        .jsgrid-grid-body{
          overflow: auto;
        }
    </style>
    <title></title>
</head>
<body style="background-color:#fff">
    <form id="form1" runat="server">
        <div id="wrap" > 
            <div class="panel-topinfo">
                <div class="panel-heading">
                    <h1 class="panel-title">거래명세서</h1> 
                    <button class="btn" onclick="widnow.print()">print</button>
                </div>
                <div class="panel-body pd">
	                <div class="panel panel-dashboard">
                        <div class="panel-heading">
                            <h3 class="panel-title">Recipient Information</h3>
                        </div>
                        <div id="divInputEvent" class="panel-body">
			                <table class="table">
				                <tbody>
					                <tr>
						                <th>Recipient</th>
						                <td colspan="3">
                                            <span id="spanRecipient"></span>
						                </td>
					                </tr>
					                <tr>
						                <th>Address</th>
						                <td colspan="3">
                                            <span id="spanAddress"></span>
						                </td>
					                </tr>
					                <tr>
						                <th>Tel.</th>
						                <td>
                                            <span id="spanTel"></span>
						                </td>
						                <th>eMail</th>
						                <td>
                                            <span id="spanEMail"></span>
						                </td>
					                </tr>
					                <tr>
						                <th>기부단체의 적격성 여부</th>
						                <td colspan="3"> 
							                <div class="radio" style="padding:0px">
								                <label><input name="rdoEligibility" type="radio" value="Y" disabled="disabled" />YES</label>
								                <label><input name="rdoEligibility" type="radio" value="N" disabled="disabled" />NO</label>
							                </div>  
						                </td>
					                </tr>
					                <tr>
						                <th>기부 이후 영수증 타입</th>
						                <td colspan="3"> 
							                <div class="radio" style="padding:0px">
								                <label><input name="rdoReceipt" type="radio" value="EDR" disabled="disabled" />적격기부영수증</label>
								                <label><input name="rdoReceipt" type="radio" value="TB" disabled="disabled" />세금계산서</label>
								                <label><input name="rdoReceipt" type="radio" value="ETC" disabled="disabled" />기타</label>
							                </div>  
						                </td>
					                </tr>
					                <tr>
						                <th>Comment</th>
						                <td colspan="3">
                                            <span id="spanComment"></span>
						                </td>
					                </tr>
					                <tr>
						                <th>Category</th>
						                <td colspan="3">
							                <div class="checkbox" style="padding:0px">
								                <label><input name="hchkCategory" type="checkbox" value="HPO" disabled="disabled" />Healthcare Professinal Organization</label>
								                <label><input name="hchkCategory" type="checkbox" value="EO" disabled="disabled" />Educational Organization</label>
								                <label><input name="hchkCategory" type="checkbox" value="CO" disabled="disabled" />Charity Organization</label>
								                <label><input name="hchkCategory" type="checkbox" value="ETC" disabled="disabled" />Others</label>
							                </div>  
						                </td>
					                </tr>
				                </tbody>
			                </table>
                        </div>
                    </div>
                    <div class="panel panel-dashboard">
                        <div class="panel-heading">
                            <h3 class="panel-title">Product Information(Donation)</h3>
                        </div>
                        <div class="panel-body" style="padding-bottom:20px">
                            <div id="jsGridProduct"></div>
                        </div>
                    </div>
                    <label>※ 위와 같이 인수 하였으며, 제공 받은 기부물품을 환자에게 재판매 또는 처방의 용도로 사용하지 않을 것임을 확인합니다.</label>
                    <label>※ 본 인수증에 서명하는 것은 제공된 기부물품의 수령확인 목적으로 바이엘코리아에서 하기 개인정보(소속기관, 성명)를 수집∙이용하는 것에 동의하는 것으로 간주됩니다.</label>

                    <label>※ I acknowledge and agree that I received the samples as listed above, and that I shall not use the received donation product for resale or prescription to patients.</label>
                    <label>※ By signing this receipt, it will be considered to give consent to collect and use the following personal information(affiliation, name) from Bayer Korea Ltd. for the purpose of confirming receipt of the donation product provided.</label>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
