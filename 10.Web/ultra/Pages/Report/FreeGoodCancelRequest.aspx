<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FreeGoodCancelRequest.aspx.cs" Inherits="Pages_Report_FreeGoodCancelRequest" %>

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
 
    <script type="text/javascript" src="/ultra/Scripts/JQuery/jquery-3.2.1.min.js"></script>
 
    <script type="text/javascript">
      
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
                        $("#txtDiv").html(item.BU);
                        $("#txtQty").html(item.QTY); 
                        $("#hhdHcoCode").html(item.HCO_CODE); 
                        $("#hhdProcessId").val(item.PROCESS_ID);
                        $("#hhdIdx").val(item.IDX);
                        $("#hhdEventKey").val(item.EVENT_KEY);
                        $("#spanManager").html(item.LAST_APPROVER);
                        window.print();
                    }
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    d.reject(jqXHR);
                });
                d.promise();
            }
            catch (ex) {
                alert( ex.message );
            }
        });
      
         
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
                            <h4 class="modal-title"  >Free Goods Cancel Request</h4>
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
                                            <th scope="row">Div</th>
                                            <td>
                                                <span id="txtDiv" runat="server"></span>
                                            </td>
                                        </tr>
                                        <tr style="display:none;">
                                            <th scope="row">No</th>
                                            <td>
                                                <span id="txtEventKey" runat="server"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th scope="row">Product Name(품명)</th>
                                            <td>
                                                <span id="txtProduct" runat="server"></span> 
                                            </td>
                                        </tr>
                                        <tr >
                                            <th scope="row">Quantity(수량)</th>
                                            <td>
                                                <span id="txtQty" runat="server"></span>
                                            </td>
                                        </tr> 
                                        <tr>
                                            <th scope="row">Bathc No</th>
                                            <td> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <th scope="row">Expire Date(유효기간만료일)</th>
                                            <td>
                                                
                                            </td>
                                        </tr>
                                        <tr >
                                            <th scope="row">Reason(사유)</th>
                                            <td> 
                                            </td>
                                        </tr> 
                                        <tr >
                                            <th scope="row">담당자</th>
                                            <td> 
                                                <span style="text-align:right"><span id="spanOnwer" runat="server" ></span>&nbsp;&nbsp;(sign)</span>
                                            </td> 
                                        </tr> 
                                        <tr >
                                            <th scope="row">Manager</th>
                                            <td> 
                                                <span style="text-align:right"><span id="spanManager" ></span>&nbsp;&nbsp;(sign)</span>
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
