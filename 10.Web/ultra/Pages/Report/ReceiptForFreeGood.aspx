<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReceiptForFreeGood.aspx.cs" Inherits="Pages_Report_ReceiptForFreeGood" %>

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
    <link rel="stylesheet" href="/ultra/Styles/Css/jquery.fileupload.css" />

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
    <script type="text/javascript" src="/ultra/Scripts/JQuery/uploader/vendor/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/Common/UserAutocompleteBox.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/JQuery/uploader/jquery.fileupload.js"></script>

    <script src="/ultra/Scripts/Common/Common.js"></script>
    <script src="/ultra/Scripts/Common/Command.js"></script>
    <script src="/ultra/Scripts/Pages/Report/ReceiptForFreeGood.js"></script>
    <script src="/ultra/Styles/signaturepad/js/signature_pad.js"></script>
    <script src="/ultra/Scripts/Common/FileUploader.js"></script>
    <link href="/ultra/Styles/waitMe/waitMe.css" rel="stylesheet" />
    <script src="/ultra/Styles/waitMe/waitMe.js"></script>
    <style type="text/css">
        /*.modal-content.modal-Report {
          height: auto;
          min-height: 100%;
          width:900px;
          border-radius: 0; 
        }*/
       
    </style>
    
    <script type="text/javascript">
        var _gaq = _gaq || [];
        _gaq.push(['_setAccount', 'UA-39365077-1']);
        _gaq.push(['_trackPageview']);

        (function() {
            var ga = document.createElement('script'); ga.type = 'text/javascript'; ga.async = true;
            ga.src = '/ultra/Styles/signaturepad/js/ga.js'; 
            var s = document.getElementsByTagName('script')[0]; s.parentNode.insertBefore(ga, s);
        })();
    </script>
    <title></title>
</head>
<body id="l_frame">
    <form id="form1" runat="server">
        <div id="wrap" class="container">
            <div class="panel panel-topinfo">
                <div class="panel-heading">
                    <h1 class="panel-title">Receipt For Free Good</h1> 
                    <div class="btnset">
                        <button type="button" class="btn btn-sm btn-navy fr" id="btnExcel"><i class="fa fa-floppy-o"></i>Excel</button>
                        <button type="button" class="btn btn-sm btn-navy fr" style="display:none" id="btnExcel_report"><i class="fa fa-floppy-o"></i>Excel_report</button>
                    </div>
                </div>
                <div class="panel-body pd"> 
                    <div id="jsGridList"></div>
                </div>
            </div>
            <div class="modal fade" id="divModalReceipt" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
                            <h4 class="modal-title" id="modalTitle">Receipt</h4>
                            <a id="btnCancelForm" class="fr"  data-dismiss="modal">Cancel form</a>
                        </div>
                        <div class="modal-body">
                            <div class="modal-table">
                                <table class="write">
                                    <colgroup>
                                        <col style="width: 21%" />
                                        <col />
                                    </colgroup>
                                    <tbody>
                                        <tr>
                                            <th scope="row">ReceiptType</th>
                                            <td>
                                                <div class="radio" style="padding: 0px">
                                                    <div id="receiptType_normal" style="">
                                                    <label>
                                                        <input name="rdoType" type="radio" value="paper" checked="" />Paper</label>
                                                    <label>
                                                        <input name="rdoType" type="radio" value="electronic" />Electronic</label>
                                                    <label>
                                                        <input name="rdoType" type="radio" value="return" />반품</label>
                                                    </div>
                                                    <div id="receiptType_rmd" style="display:none">
                                                    <label >
                                                        <input name="rdoType" type="radio" value="electronicRMD" />의료기기Electronic</label>
                                                    <label>
                                                        <input name="rdoType" type="radio" value="electronicRMD_Return" />의료기기Electronic(반납)</label>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                        <tr id="trReceiptDate">
                                            <th scope="row">Receipt Date</th>
                                            <td>
                                                <span id="spanReceiptDate" style="display:none;"></span>
                                                <div id="divReceiptDataArea">
                                                    <div class="input-group date form_datetime" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtReceiptDate">
                                                        <input id="dtReceiptDate" class="form-control" size="10" readonly type="text" />
                                                        <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>

                                                    </div>
                                                </div>
                                                <div style="color:red">
                                                    Receipt Date 설정 시 인수증 기재된 날짜와 동일하게 날짜 설정
                                                </div>
                                            </td>
                                        </tr>
                                        <tr id="trReturn">
                                            <th scope="row">반품 요청일</th>
                                            <td>
                                                <span id="hspanreturnDate"></span>
                                            </td>
                                        </tr>
                                        <tr id="trReturnComplete">
                                            <th scope="row">반품 완료일</th>
                                            <td>
                                                <span id="hspanreturncompleteDate"></span>
                                            </td>
                                        </tr>
                                        <tr id="trReturnType">
                                            <th scope="row">반품 Comment</th>
                                            <td>
                                                 <select id="ReturnComment" class="form-control">
                                                    <option title="미반환 : 소모품"  value="1" >미반환 : 소모품</option>
                                                    <option title="미반환 : 제품 구매" value="2" >미반환 : 제품 구매</option>
                                                    <option title="반환 : 제품 구매 안함" value="3" >반환 : 제품 구매 안함</option> 
                                                    <option title="반환 : 신규제품 구매" value="4" >반환 : 신규제품 구매</option>
                                                    
                                                </select>
                                            </td>
                                        </tr>
                                        
                                        <tr id="trSapOrder">
                                            <th scope="row">SAP Order</th>
                                            <td>
                                                <input id="txtSapOrder" type="text"   class="form-control" />
                                            </td>
                                        </tr>  
                                    </tbody>
                                </table>
                            </div>
                            <div id="divAttachArea">
                                <div class="panel panel-dashboard">
                                    <div id="divAttachFiles_Receipt" class="attach" data-attachment-type="ReceiptFreeGood">
                                        <div class="attach-heading" style="border-bottom: 1px solid #e0e0e0">
                                            <h3 class="attach-title">Attachment(<a onclick="fn_PrintReceiptFreeGood()">Download Form</a>)</h3>
                                            <span class="btn btn-sm btn-navy fileinput-button" id="hspanAddAttach">
                                                <i class="fa fa-paperclip"></i>
                                                <span>Attachment</span>
                                                <input class="fileupload" id="fileupload" type="file" name="files[]" accept=".pdf,.jpg,.png,.gif,.bmp" />
                                            </span>
                                            <!-- The global progress bar -->
                                            <!-- The container for the uploaded files -->
                                        </div>
                                        <div id="progress" class="progress" style="height: 7px !important;">
                                            <div class="progress-bar progress-bar-success">
                                            </div>
                                        </div>
                                        <div id="files" class="files">
                                            <ul class="attach-list"></ul>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" id="btnReturnCancel" class="btn btn-navy fl" data-dismiss="modal">반품취소</button>
                            <button type="button" id="btnNext" class="btn btn-navy fr" data-dismiss="modal">NEXT</button>
                            <button type="button" id="btnReceipt"  class="btn btn-red fr">Receipt</button>
                            <button type="button" id="btnPaperSave"  data-loading-text="<i class='fa fa-circle-o-notch fa-spin'></i> Processing" class="btn btn-red fr">Save</button>
                            <button type="button" id="btnClose" class="btn btn-navy fr" data-dismiss="modal">close</button>
                        </div>
                    </div>
                </div>
            </div>

            <div class="modal fade" id="divElectronicSign"  tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document" id="modal-ElectronicSign">
                    <div class="modal-content modal-Report">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
                            <h4 class="modal-title"  >Sample Acknowledbement Form</h4>
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
                                        <tr id="electronic_comment_normal"> 
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
                                        <tr id="electronic_comment_rmd" style="display:none"> 
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
                                        <tr id="electronic_comment_rmd_return" style="display:none"> 
                                            <td colspan="2"> 
                                                <b>반납 여부    <span id="txtReturnType" style="color:red;font-size:17px"></span></b>
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
                                               <div class="signature-pad" >
                                                <div class="signature-pad--body" style="border: 1px solid #d5d5d5;">
                                                    <canvas id="signature-pad" style="-ms-touch-action: none; touch-action: none;"></canvas> <%-- width="630" height="140"--%>
                                                </div>
                                                <div class="signature-pad--footer">  
                                                    <button class="button clear" type="button" id="btnPadClear" >Clear</button> 
                                                </div>
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
                        <div class="modal-footer"> 
                            <button type="button" id="btnSignSave"  class="btn btn-red fr">Save</button>
                            <button type="button" id="btnSignSave_RMDReturn" class="btn btn-red fr">Save(반납)</button>
                            <input type="hidden" id="hhdProcessId" />
                            <input type="hidden" id="hhdIdx" />
                            <input type="hidden" id="hhdEventKey" />
                        </div>
                    </div>
                </div>
            </div>


            <!-- Ver 1.0.7 : Go-Direct -->
             <div class="modal fade" id="divElectronicSign_RMD"  tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document" id="modal-ElectronicSign_RMD">
                    <div class="modal-content modal-Report">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
                            <h4 class="modal-title"  >Sample Acknowledbement Form</h4>
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
                                                <span id="txtDivision_RMD"></span> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <th scope="row">No</th>
                                            <td>
                                                <span id="txtEventKey_RMD"></span> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <th scope="row">Product(품명)</th>
                                            <td>
                                                <span id="txtProduct_RMD"></span> 
                                            </td>
                                        </tr>
                                        <tr>
                                            <th scope="row">Product Code(제품코드)</th>
                                            <td>
                                                <span id="txtProductCode_RMD"></span>
                                            </td>
                                        </tr>
                                        <tr>
                                            <th scope="row">Package(포장내총수량-규격)</th>
                                            <td>
                                                <span id="txtPackage_RMD"></span>
                                            </td>
                                        </tr>
                                        <tr >
                                            <th scope="row">Purpose(목적)</th>
                                            <td>
                                               <span id="txtPurpose_RMD"></span>
                                            </td>
                                        </tr>
                                        <tr >
                                            <th scope="row">Quantity(수량)</th>
                                            <td>
                                                <span id="txtQty_RMD"></span>
                                            </td>
                                        </tr> 
                                        <tr > 
                                            <td colspan="2"> 
                                                <br/>※ 위와 같이 인수 하였으며, 제공 받은 견본품을 환자에게 사용할 경우 그 비용을 환자에게 별도로 청구할 수 없습니다.					
                                                <br/>※ 본 견본품은 관계법령(의료기기법 제13조의2, 의료기기 유통 및 판매질서 유지에 관한 규칙 제3조 등)에 따라 지출보고서로 제출될 수 있으며, 상기 견본품은 의료기기법 및 공정경쟁규약에 따라 제공됩니다. 					
                                                <br/>※ 평가용으로 제공되는 의료기기는 공정경쟁규약에서 규정한 바에 따라 필요한 최소한의 기간(1개월 이내) 동안만 제공할 수 있습니다.
                                                <br/>					
                                                <br/>※ 평가기간 동안 평가용 제품의 소유권은 바이엘코리아㈜에 있으며, 이를 이전하여서는 안됩니다. 					
                                                <br/>※ 평가기간이 종료하면 보건의료인 또는 의료기관이 해당 제품을 구매하지 않는 한, 해당 제품은 바이엘코리아로 반환됩니다.					
                                                <br/>※ 본 인수증에 서명하는 것은 제공된 물품의 수령확인 목적으로 바이엘코리아에서 하기 개인정보(소속기관, 성명)를 수집·이용하는
                                                <br/>것에 동의하는 것으로 간주됩니다."					
                                                <br/>					
                                                <br/>※ I acknowledge and agree that I received the samples as listed above, and if the samples are used on the patient it can not be charged to the patient separately.					
                                                <br/>※ These samples may be submitted in expense reports pursuant to the relevant laws (Article 13-2 of the Medical Devices Act and Article 3 of the Rules on Distribution of Medical Devices and Maintenance of Order in Sales).  The above samples are provided in compliance with the Medical Devices Act and the Fair Competition Code. 					
                                                <br/>※ Medical devices provided for evaluation may only be provided for the minimum required period (within one month) as indicated by the Fair Competition Code. 					
                                                <br/>※ During the evaluation period, the above medical device for performance evaluation are owned by Bayer Korea and it can't be transferred.	"					
                                                <br/>※ After the end of the evaluation period, the product will be returned to Bayer Korea unless the HCO or HCP purchase the product.					
                                                <br/>※ By signing this receipt, it will be considered to give consent to collect and use the following personal information(affiliation, name) from Bayer Korea Ltd. For the purpose of confirming receipt of the product provided.					

                                            </td>
                                        </tr>
                                        <tr>
                                            <th scope="row">일자</th>
                                            <td>
                                                 <span id="txtSignDate_RMD"></span>
                                            </td>
                                        </tr> 
                                        <tr>
                                            <th scope="row">인수기관</th>
                                            <td>
                                                <span id="txtHcoName_RMD"></span>
                                                <input type="hidden" id="hhdHcoCode_RMD"/> 
                                            </td>
                                        </tr> 
                                        <tr>
                                            <th scope="row">인수자</th>
                                            <td>
                                                <span id="txtHcpName_RMD"></span>
                                                <input type="hidden" id="hhdHcpCode_RMD"/> 
                                            </td>
                                        </tr>  
                                        <tr>
                                            <th scope="row">Signature</th>
                                            <td> 
                                               <div class="signature-pad" >
                                                <div class="signature-pad--body" style="border: 1px solid #d5d5d5;">
                                                    <canvas id="signature-pad_RMD" style="-ms-touch-action: none; touch-action: none;"></canvas> <%-- width="630" height="140"--%>
                                                </div>
                                                <div class="signature-pad--footer">  
                                                    <button class="button clear" type="button" id="btnPadClear_RMD" >Clear</button> 
                                                </div>
                                                </div>  
                                            </td>
                                        </tr>
                                        <tr>
                                            <th scope="row">바이엘 담당자</th>
                                            <td>
                                                <span id="txtRequestName_RMD"></span> 
                                            </td>
                                        </tr> 
                                    </tbody>
                                </table>
                            </div>
                            
                        </div>
                        <div class="modal-footer"> 
                            <button type="button" id="btnSignSave_RMD"  class="btn btn-red fr">Save</button>  
                            
                        </div>
                    </div>
                </div>
            </div>
      
            <input type="hidden" id="hddUserID" runat="server" />
            <input type="hidden" id="hddProcessID" runat="server" />
            <input type="hidden" id="hhdUserName" runat="server" />
            <input type="hidden" id="hhdDate" runat="server" />
            <input type="hidden" id="hddProcessStatus" runat="server" value="" />
        </div>
            <!-- 이벤트 완료 -->
            <div id="layer_success" class="modal fade" tabindex="-1" role="dialog">
                <div class="modal-dialog modal-alert" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i aria-hidden="true" class="fa fa-times"></i></button>
                        </div>
                        <div class="modal-body">
                            <div class="alert-message">
                                <i class="fa fa-check-circle text-success"></i>
                                <p>
                                    <strong class="colr-point">success</strong><br>
                                    <span class="span-message">성공적으로 완료되었습니다</span>
                                </p>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <%--<button type="button" class="btn btn-sm btn-darkgray" data-dismiss="modal">Cancel</button>--%>
                            <button type="button" class="btn btn-sm btn-success" data-dismiss="modal">Ok</button>
                        </div>
                    </div>
                </div>
            </div>
            <!-- // #layer_success -->

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
        <div id="layer_alert" class="modal fade" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-alert" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i aria-hidden="true" class="fa fa-times"></i></button>
                    </div>
                    <div class="modal-body">
                        <div class="alert-message">
                            <i class="fa fa-minus-circle text-danger"></i>
                            <p>
                                <strong class="colr-point">alert</strong><br>
                                <span class="span-message">취소하시겠습니까?</span>
                            </p>
                        </div>
                    </div>
                    <div class="modal-footer">
                        <button id="btnConfirm" type="button" class="btn btn-sm btn-danger">Yes</button>
                        <button id="btnCancel" type="button" class="btn btn-sm btn-darkgray">No</button>
                    </div>
                </div>
            </div>
        </div>
           <iframe id="iframeFileDown" width="0" height="0"></iframe>
    </form>
</body>
</html>
