<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PrintYourDocesCover.aspx.cs" Inherits="Pages_Event_Print_PrintYourDocesCover" %>

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
        var processId = "";
        $(function () {
            
            try {
             
                var d = $.Deferred();
                processId = $.urlParam("processid");
                var idx = $.urlParam("idx"), ruleidx = $.urlParam("ruleidx");
                var selectUrl = EVENT_SERVICE_URL + "/SelectAgendaRoleInfoPrint/" + processId + "/" + idx + "/" + ruleidx;
                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (item) {
                    d.resolve(item);
                    if (item != null) {
                        $("#spanEventkey").text(item.EVENT_KEY.substring(2,item.EVENT_KEY.length));
                        //$("#spanEventname").text(item.EVENT_NAME);
                        $("#spanEventname").text("Ultra_Your-Doces : " + item.PREFIX_EVENT_KEY);
                        $("#spanScientific").val(item.MATERIAL_CODE);
                        $("#costcenter").val(item.MATERIAL_CODE);
                        $("#vendorno").val(item.SAP_NO);
                        $("#costcenter").val(item.COST_CENTER);
                       
                        //$("#radioKRPIA:radio[value='" + item.KRPIA + "']").attr("checked", true);
                        $("input:radio[name='rdoAGREE']:radio[value='" + item.KRPIA + "']").prop("checked", true);

                        
                        $("#spanAmount").text(fn_AddComma(item.AMOUNT));
                        $("#spanHcpname").text(item.HCP_NAME);
                        $("#spanHcpcode").text(item.HCP_CODE);
                        $("#spanName").text(item.REQUESTER_NAME);
                        $("#spanCwid").text(item.REQUESTER_ID);
                        //window.print();
                    }
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    d.reject(jqXHR);
                });
                d.promise();
            }
            catch (ex) {
                fn_showError({ message: ex.message });
                alert("error\n"+ex.message);
            }
			//$("#save_print").click(function () {
			//	alert("저장 기능은 준비중입니다.");
			//	window.print();
				
			//});
			
        });
		function fn_saveprint(){
		
		    //alert($("input[name=rdoAGREE]:checked").length);
		    var agenda_role_idx = $.urlParam("ruleidx");
		    var userId = $("input[id$=hddUserID]").val()
            , mcode = $("input[id$=spanScientific]").val()
            , costcenter = $("input[id$=costcenter]").val()
            , sap_no = $("input[id$=vendorno]").val()
            , krpia = $("input[name=rdoAGREE]:checked").val();
		    var Yourdoc_data = {		        
		        processID: processId,
		        agenda_role_idx: agenda_role_idx,
		        mcode: mcode,
		        userId: userId,
		        costcenter: costcenter,
		        sap_no:sap_no,
		        krpia:krpia
		    }



			if($("input[name=rdoAGREE]:checked").length>=1  ){
			    
			    //costcenter = costcenter.replace("/", "_");
			    
			    //var url = EVENT_SERVICE_URL + "/UpdateMaterialcode/" + processId + "/" + agenda_role_idx + "/" + mcode + "/" + userId + "/" + costcenter + "/" + sap_no + "/" + krpia ;
			    var url = EVENT_SERVICE_URL + "/UpdateMaterialcode"
			    //alert(url);
			    $.ajax({
			        url: url,
			        type: "POST",
			        data: JSON.stringify(Yourdoc_data),
			        //dataType: "json",
			        async: false,
			        contentType: "application/json; charset=utf-8",
			        success: function () {
			            //alert("ok");
			            window.print();
			            //if (result) {
			            //    alertMessage = "HCP, Sample는 결재된 문서가 있습니다.";
			            //    retvalue = -2;
			            //}
			        },
			        error: function (error) {
			            //fn_showError({
			            //    message: error.responseText
			            // });
			            alert("Error,Please Contact helpdesk team!!\n" + error.responseText);
			        },
			    });
				
			}else{
				alert("개인정보활용동의서 Check 해주세요");
				return;
			}
			
		
		}
        $.urlParam = function (name) {
            var results = new RegExp('[\?&amp;]' + name + '=([^&amp;#]*)').exec(window.location.href);
            return results[1] || 0;
        }
    </script>
    <style type="text/css">
        .write tbody tr {
            height:30px;
        }
        .heightlightforprint{
            font-size:1.5em;
            color:black;
            font-weight:900;
            letter-spacing:3px;
            font-family:"Consolas";
            text-transform:uppercase;

        }
    </style>
    <title></title>
</head>
<body style="background-color:#fff">
    <form id="form1" runat="server">
        <div id="wrap" > 
            <div class="panel-topinfo">
                <div class="panel-heading">
                    <h1 class="panel-title">Medical Event Cover-Letter</h1>
					<div style="margin:0px 0px 10px 0px">
						<a href="javascript:fn_saveprint()" class="btn btn-xs btn-danger" style="float:right" id="save_print" >Save&Print</a>
					</div>
                </div>
                <div class="panel-body pd">
                    <table class="table">
                        <colgroup>
                            <col style="width: 36%" />
                            <col style="margin-left:20px;" />
                        </colgroup>
                        <tbody>
                            <tr>
                                <th>Medical Event Doc. Nr.</th>
                                <td><span class="heightlightforprint" id="spanEventkey"></span></td>
                            </tr>
                            <tr>
                                <th>Event Type</th>
                                <td><span class="heightlightforprint" id="spanEventname"></span></td>
                            </tr>
                            <tr>
                                <th>Name</th>
                                <td><span id="spanName" ></span></td>
                            </tr>
                            <tr>
                                <th>CWID</th>
                                <td><span id="spanCwid"></span></td>
                            </tr>
                            <tr>
                                <th>Cost Center/Internal Order</th>
                                <td><input class='form-control' id='costcenter' /></td>
                            </tr>
                            <tr>
                                <th>Scientific Material</th>
                                <td><input class='form-control' id='spanScientific' /></span></td>
                            </tr>
                            <tr>
                                <th>SAP Vendor No.</th>
                                <td><input class='form-control' id='vendorno' /></td>
                            </tr>
                            
                            <tr>
                                <th>Actual Cost</th>
                                <td><span class="heightlightforprint" id="spanAmount"></span></td>
                            </tr>
                            <tr>
                                <th>HCP Name</th>
                                <td><span id="spanHcpname"></span></td>
                            </tr>
                            <tr>
                                <th>HCP Code</th>
                                <td><span class="heightlightforprint" id="spanHcpcode"></span></td>
                            </tr>
							<tr>
                                <th>개인정보 :<br> KRPIA 강연/자문 보고 동의</th>
                                <td>
									<div class="radio" style="padding: 0px">
										<label>
											<input name="rdoAGREE" type="radio" value="Y" id="radioKRPIA"  />동의(Agreed)</label>
										<label>
											<input name="rdoAGREE" type="radio" value="N" id="radioKRPIA" />미동의(Refused)</label>
									</div>
								
								
								
								</td>
                            </tr>
							
                            <!--Cover Letter 수정 -->
							<tr>
                                <th>(Foreign HCP/HCO only) Actual Payment in foreign currency<br />외환으로 비용지급을 해야 하는 경우, 지불해야 하는 화폐명과 지불액 입력
                                    <br />*** Only for foreign HCP/HCO, allow to adjust the actual payment without further action.
                                </th>
                                <td><input class='form-control' id='foreign_HCP' /><br />(예시:USD 100)</td>
                            </tr>
							
							<tr>
                                <th>(Exceptional Withholding Tax only)<br />원천세율 8.8%(거주자의 기타소득)외 세율이나 원천세대상이 아닌 경우 기재
                                </th>
                                <td><input class='form-control' id='tax_exception' /><br />(예시 : 사업소득자, 해외 HCP/HCO</td>
                            </tr>
                            <!--Cover Letter 수정 -->
							
							
							
							
							
							
							
                            <tr>
                                <th>Remarks</th>
                                <td><textarea class='form-control' rows=3></textarea></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <input type="hidden" id="hddUserID" runat="server" />
    </form>

    <div id="layer_error" class="modal fade" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-alert" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i aria-hidden="true" class="fa fa-times"></i></button>
                        </div>
                        <div class="modal-body">
                            <div class="alert-message">
                                <i class="fa fa-exclamation-triangle text-warning"></i>
                                <p>
                                    <strong class="colr-point">error</strong><br>
                                    <span class="span-message">실패했습니다. 다시 시도해주세요.</span>
                                </p>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <%--<button type="button" class="btn btn-sm btn-darkgray" data-dismiss="modal">Cancel</button>--%>
                            <button type="button" class="btn btn-sm btn-warning" data-dismiss="modal">Confirm</button>
                        </div>
                    </div>
                </div>
            </div>
</body>
</html>
