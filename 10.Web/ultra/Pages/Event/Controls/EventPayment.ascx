<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventPayment.ascx.cs" Inherits="Pages_Event_Controls_EventPayment" %>
<style type="text/css">
    .agenda-title h2:hover {
        cursor: pointer;
        text-decoration: underline;
    }

    .report-card a:hover {
        cursor: pointer;
        text-decoration: underline !important;
    }

    .payment .jsgrid-align-right {
        font-size: 12px;
    }

    .header-fixed {
        width: 100%;
    }

        .header-fixed th:nth-child(1) {
            width: 8%;
        }

        .header-fixed th:nth-child(2) {
            width: 8%;
        }

        .header-fixed th:nth-child(3) {
            width: 13%;
        }

        .header-fixed th:nth-child(4) {
            width: 7%;
        }

        .header-fixed th:nth-child(5) {
            width: 7%;
        }

        .header-fixed th:nth-child(6) {
            width: 9%;
        }

        .header-fixed th:nth-child(7) {
            width: 9%;
        }

        .header-fixed th:nth-child(8) {
            width: 9%;
        }

        .header-fixed th:nth-child(9) {
            width: 9%;
        }

        .header-fixed th:nth-child(10) {
            width: 9%;
        }

        .header-fixed th:nth-child(11) {
            width: 9%;
        }

        .header-fixed td:nth-child(1) {
            width: 8%;
        }

        .header-fixed td:nth-child(2) {
            width: 8%;
        }

        .header-fixed td:nth-child(3) {
            width: 13%;
        }

        .header-fixed td:nth-child(4) {
            width: 7%;
        }

        .header-fixed td:nth-child(5) {
            width: 7%;
        }

        .header-fixed td:nth-child(6) {
            width: 9%;
            text-align: right;
        }

        .header-fixed td:nth-child(7) {
            width: 9%;
            text-align: right;
        }

        .header-fixed td:nth-child(8) {
            width: 9%;
            text-align: right;
        }

        .header-fixed td:nth-child(9) {
            width: 9%;
            text-align: right;
        }

        .header-fixed td:nth-child(10) {
            width: 9%;
            text-align: right;
        }

        .header-fixed td:nth-child(11) {
            width: 9%;
            text-align: right;
        }

        .header-fixed > thead,
        .header-fixed > tbody,
        .header-fixed > thead > tr,
        .header-fixed > tbody > tr,
        .header-fixed > thead > tr > th,
        .header-fixed > tbody > tr > td {
            display: block;
            text-overflow: ellipsis;
            white-space: nowrap;
            overflow: hidden !important;
        }

            .header-fixed > tbody > tr:after,
            .header-fixed > thead > tr:after {
                content: ' ';
                display: block;
                visibility: hidden;
                clear: both;
            }

        .header-fixed > tbody {
            overflow-y: auto !important;
            height: 250px;
        }

            .header-fixed > tbody > tr > td,
            .header-fixed > thead > tr > th {
                float: left;
            }

    .expense-report dt {
        padding: 3px 0;
        border-bottom: 1px solid #e6e6e6;
        width: 300px;
        text-align: center;
    }

    .expense-report dd {
        padding: 3px 15px;
        border-bottom: 1px solid #e6e6e6;
        overflow: hidden;
    }

    .expense-report .srm-total {
        font-weight: bold;
        background-color: #f9f9f9;
    }

    .expense-report dd:nth-child(odd) {
        background-color: #f9f9f9;
    }

    
    .jsgrid-header-row > .jsgrid-header-cell { text-overflow: ellipsis; -o-text-overflow: ellipsis; white-space: nowrap; overflow: hidden; }
    /*.jsgrid-cell { text-overflow: ellipsis; -o-text-overflow: ellipsis; white-space: nowrap; overflow: hidden; }*/
</style>
<div class="box-panel">
    <ul class="nav nav-tabs nav-tabs-md" role="tablist">
        <li id="tabByHCP" runat="server" role="presentation" class="active"><a href="#tpByHCP" aria-controls="tpByHCP" role="tab" data-toggle="tab">By HCP</a></li>
        <!-- version 1.0.5 Payment Complete 시, Plan vs Actual 30% Gap 발생시 comment 입력 : <i> tag 추가 -->
        <li id="tabPlanActual" runat="server" role="presentation"><a href="#tpPlanActual" aria-controls="tpPlanActual" role="tab" data-toggle="tab">Plan vs Actual<i class="fa" style="position:absolute;top:5px;right:5px;color:#dd5468"></i></a></li>
        <li id="tabExpenseUpload" runat="server" role="presentation"><a href="#tpExpenseUpload" aria-controls="tpExpenseUpload" role="tab" data-toggle="tab">Expense Upload</a></li>
    </ul>
    <div class="tab-content payment">
        <div role="tabpanel" class="tab-pane active" id="tpByHCP">
            <div class="tab-cont-area row row-md closed">
                <div class="list-area">
                    <div class="list-heading">
                        <h3 class="list-tit fl">By HCP</h3>
                        <div class="label-area fr">
                            <span class="label bg-skyblue">Concur</span>
                            <span class="label bg-green">SmartBuy</span>
                            <span class="label bg-pink">Your-Doces</span>
                        </div>
                    </div>
                    <div class="table-responsive">
                        <div id="jsGridPaymentByHCP"></div>
                    </div>
                    <h3 class="list-tit">Expense Report</h3>
                    <div class="row row-md">
                        <div class="col-xs-6 col-md-4">
                            <dl id="dlPaymentConcur" class="report-card">
                                <dt><span class="colr-skyblue">Concur</span> <span class="label bg-skyblue">Concur</span></dt>
                                <%--                                <dd>
                                    <div class="info">
                                        <strong class="date">17.10.18</strong>
                                        <p>1C25DF79B2314722B3A3</p>
                                        <p>1C25DF79B2314722B3A3 - 1</p>
                                    </div>
                                    <strong class="sum">80,000</strong>
                                </dd>--%>
                            </dl>
                        </div>
                        <div class="col-xs-6 col-md-4">
                            <dl id="dlPaymentSRM" class="report-card">
                                <dt><span class="colr-green">SmartBuy</span> <span class="label bg-green">SmartBuy</span></dt>
                            </dl>
                        </div>
                        <div class="col-xs-6 col-md-4">
                            <dl id="dlPaymentYourDoces" class="report-card">
                                <dt><span class="colr-pink">Your-Doces</span> <span class="label bg-pink">Your-Doces</span></dt>
                                <%--                                <dd>
                                    <strong class="date">17.10.18</strong>
                                    <p>240370205</p>
                                    <p>JiIn Moon-7408261</p>
                                    <strong class="sum">1,080,000</strong>
                                </dd>--%>
                                <%--                                <dd>
                                    <strong class="date">17.10.18</strong>
                                    <p>240370205</p>
                                    <p>ChulSoo Ahn - 7208061</p>
                                    <strong class="sum">400,000</strong>
                                </dd>--%>
                            </dl>
                        </div>
                    </div>
                </div>
                <!-- // .list-area  -->
            </div>
            <!-- // .row-md -->
        </div>
        <div role="tabpanel" class="tab-pane" id="tpPlanActual">
            <div class="tab-cont-area row row-md closed">
                <!-- // 닫으려면 class closed 추가 -->
                <div class="list-area">
                    <h3 class="list-tit">Plan vs Actual</h3>
                    <div id="jsGridPaymentPlanActual"></div>
                </div>
                <!-- // .list-area  -->

                <!-- version 1.0.5 Payment Complete 시, Plan vs Actual 30% Gap 발생시 comment 입력 -->
                <div class="list-area" id="GapReason">

                    <h3 class="list-tit">Reason of Gap(30%) 
                         <button id="btnSaveGap" type="button" class="btn btn-sm btn-red" style="float:right"><i class="fa fa-floppy-o"></i>Save</button>

                    </h3>
                    <textarea id="txtGapReason" class="form-control" rows="3" style="margin-top:5px;width: 100%;"></textarea>
                    
                </div>


            </div>
            <!-- // .row-md -->
        </div>
        <div role="tabpanel" class="tab-pane" id="tpExpenseUpload">
            <div class="tab-cont-area row row-md">
                <!-- // 닫으려면 class closed 추가 -->
                <div class="write-area" style="margin-top: 20px;">
                    <h3 class="write-tit">Expense Report for SmartBuy</h3>
                    <div class="box-panel">
                        <form action="#">
                            <div class="form-group">
                                <label for="txtUploadSRM_PoNumber">PO Number(10자리 입력)</label>
                                <input type="text" id="txtUploadSRM_PoNumber" class="form-control" style="text-transform: uppercase;" maxlength="10">
                            </div>
                            <div id="divUploadFiles_SRM" class="attach attach-area btnset upload" data-attachment-type="PaymentSRM" style="padding-bottom: 10px; padding-top: 3px;">
                                <div id="progress" class="progress" style="height: 7px !important; visibility: hidden; margin-bottom: 0px;">
                                    <div class="progress-bar progress-bar-success">
                                    </div>
                                </div>
                                <span class="btn btn-sm btn-navy fileinput-button">
                                    <i class="fa fa-paperclip"></i>
                                    <span>Upload</span>
                                    <!-- The file input field used as target for the file upload widget -->
                                    <input class="fileupload" id="fileupload" type="file" name="files" accept=".xlsx" />
                                </span>
                                <!-- The global progress bar -->
                                <!-- The container for the uploaded files -->
                                <div id="files" class="files">
                                <ul class="attach-list files"></ul>
                                </div>
                            </div>
                            <dl id="dlUploadSRMResult" class="upload_result">
                                <dt>Upload Result <a href="#" onclick="showUploadSRMDetail();"><i class="fa fa-asterisk"></i>Detail</a></dt>
                            </dl>
                            <div class="form-group">
                                <label for="lb_SRMComment">Comment</label>
                                <textarea id="lb_SRMComment" class="form-control"></textarea>
                            </div>
                            <div class="btnset">
                                <button id="btnDeleteUploadSRM" type="button" class="btn btn-sm btn-navy fl" disabled><i class="fa fa-trash-o"></i>Delete</button>
                                <button id="btnSaveUploadSRM" type="button" class="btn btn-sm btn-red fr" disabled><i class="fa fa-floppy-o"></i>Save</button>
                            </div>
                        </form>
                        <button type="button" class="btn-panel-close"><i class="fa fa-close"><span class="tts">Close</span></i></button>
                    </div>
                    <!-- // .box-panel -->
                </div>
                <!-- // .write-area -->
                <div class="list-area" style="margin-top: 20px;">
                    <button type="button" class="btn-panel-open"><i class="fa fa-plus-square"><span class="tts">Open</span></i></button>
                    <h3 class="list-tit">Upload History</h3>
                    <div id="jsGridPaymentSRMHistory"></div>
                    <br />
                    <div class="list-ctrl row">
                        <div class="col-xs-8">
                            <h3 class="list-tit" style="margin-top: 10px; margin-bottom: 5px;">PO Amount</h3>
                        </div>
                        <div class="col-xs-4 text-right">
                            <button id="btnInputPoNumber" type="button" class="btn btn-sm btn-darkgray"><i class="fa fa-floppy-o"></i>Add</button>
                        </div>
                    </div>
                    <div id="jsGridPaymentInputSRM"></div>



                </div>
                <!-- // .list-area  -->
            </div>
            <!-- // .row-md -->
        </div>
    </div>
    <input type="hidden" id="hddUploadSRMList" value="" />
    <!-- // .row-md -->
    <script type="text/javascript" src="/ultra/Scripts/Master/Payment.js"></script>
</div>
<!-- // #tab_expense -->
<div id="layer-concur" class="layer-card">
    <dl class="report-card">
        <dt><span class="colr-skyblue">Concur</span> <span class="label bg-skyblue">Concur</span></dt>
        <dd>
            <div class="info">
                <strong class="date">17.10.18</strong>
                <p>1C25DF79B2314722B3A3</p>
                <p>1C25DF79B2314722B3A3 - 1</p>
            </div>
            <strong class="sum">80,000</strong>
        </dd>
    </dl>
    <button type="button" class="close" aria-label="Close"><i class="fa fa-times" aria-hidden="true"></i></button>
</div>
<div id="layer-srm" class="layer-card">
    <dl class="report-card">
        <dt><span class="colr-green">SmartBuy</span> <span class="label bg-green">SmartBuy</span></dt>
        <dd>
            <strong class="date">17.10.18</strong>
            <p>1C25DF79B2314722B3A3</p>
            <div class="remove-item">
                <a href="#" class="btn btn-xs btn-gray">Meale stimate.PDF</a>
                <button type="button" class="fa fa-times"><span class="tts">Close</span></button>
            </div>
            <strong class="sum">80,000</strong>
        </dd>
    </dl>
    <button type="button" class="close" aria-label="Close"><i class="fa fa-times" aria-hidden="true"></i></button>
</div>
<div id="layer-your-doces" class="layer-card">
    <dl class="report-card">
        <dt><span class="colr-pink">Your-Doces</span> <span class="label bg-pink">Your-Doces</span></dt>
        <dd>
            <strong class="date">17.10.18</strong>
            <p>240370205</p>
            <p>JiIn Moon-7408261</p>
            <strong class="sum">1,080,000</strong>
        </dd>
        <dd>
            <strong class="date">17.10.18</strong>
            <p>240370205</p>
            <p>ChulSoo Ahn - 7208061</p>
            <strong class="sum">400,000</strong>
        </dd>
    </dl>
    <button type="button" class="close" aria-label="Close"><i class="fa fa-times" aria-hidden="true"></i></button>
</div>
<!-- 엑셀 업로드 세부 -->
<div id="layer_uploadSRMDetail" class="modal fade" tabindex="-1" role="dialog">
    <div class="modal-dialog modal-lg" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
                <h4 class="modal-title">Expense Report</h4>
            </div>
            <div class="modal-body">
                <dl id="dlUploadSRMDetailHeader" class="dl-horizontal expense-report" style="width: 100%;">
                </dl>
                <table class="table table-striped header-fixed">
                    <thead>
                        <tr style="font-size: 11px;">
                            <th scope="col">Type</th>
                            <th scope="col">Name</th>
                            <th scope="col">Organization</th>
                            <th scope="col">Role</th>
                            <th scope="col">참석여부</th>
                            <th scope="col">Total Cost</th>
                            <th scope="col">Accomodation</th>
                            <th scope="col">Meal & Beverage</th>
                            <th scope="col">Transportation</th>
                            <th scope="col">Gimmick/Sourvenir</th>
                            <th scope="col">Comment</th>
                        </tr>
                    </thead>
                    <tbody style="font-size: 11px;">
                    </tbody>
                </table>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-lg btn-navy" data-dismiss="modal">Ok</button>
            </div>
        </div>
    </div>
</div>
<!-- 엑셀 업로드 세부 -->

<!-- PO Amount 입력 -->
<div id="layer_InputSRM" class="modal fade" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
                <h4 class="modal-title">PO Amount</h4>
            </div>
            <div class="modal-body">
                <div class="modal-table">
                    <table class="write">
                        <colgroup>
                            <col style="width: 25%" />
                            <col />
                        </colgroup>
                        <tbody>
                            <tr>
                                <th scope="row">PO Number</th>
                                <td>
                                    <input type="text" id="txtInputSRM_PoNumber" class="form-control" style="text-transform: uppercase;" />
                                </td>
                            </tr>
                            <tr>
                                <th scope="row">Category</th>
                                <td>
                                    <select id="selInputSRMCategory" class="form-control" runat="server"></select>
                                </td>
                            </tr>
                            <tr>
                                <th scope="row">Post Date</th>
                                <td>
                                    <div class="input-group date form_date" data-date="" data-date-format="yyyy-mm-dd hh:ii:ss" data-link-field="dtStartTime">
                                        <input id="datInputSRM_PostDate" class="form-control" size="16" type="text" value="" readonly />
                                        <span class="input-group-addon"><span class="fa fa-times"></span></span>
                                        <span class="input-group-addon"><span class="fa fa-calendar"></span></span>
                                    </div>
                                    <input type="hidden" id="dtStartTime" value="" />
                                </td>
                            </tr>
                            <tr>
                                <th scope="row">Amount</th>
                                <td>
                                    <input type="text" id="numInputSRM_Amount" class="form-control number" value="0" />
                                </td>
                            </tr>
                            <tr>
                                <th scope="row">Comment</th>
                                <td>
                                    <textarea id="txtInputSRM_Comment" class="form-control"></textarea></td>
                            </tr>

                        </tbody>
                    </table>
                </div>
            </div>
            <div class="modal-footer">
                <button id="btnDeleteInputSRM" type="button" class="btn btn-sm btn-navy fl"><i class="fa fa-trash-o"></i>Delete</button>
                <button id="btnSaveInputSRM" type="button" class="btn btn-sm btn-red fr"><i class="fa fa-floppy-o"></i>Save</button>
            </div>
        </div>
    </div>
    <input type="hidden" id="hddPayment_InputSRM" value="" />
</div>
<!-- PO Amount 입력 -->

