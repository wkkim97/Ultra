<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Medical.master" AutoEventWireup="true" CodeFile="HCPInquiry.aspx.cs" Inherits="Pages_Medical_Study_StudyList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHolderHead" Runat="Server">
    <link href="/ultra/Styles/waitMe/waitMe.css" rel="stylesheet" />
    <link rel="stylesheet" href="/ultra/Styles/Css/jquery.fileupload.css" />
    <script src="../../Scripts/Pages/FormEvent.js"></script>
    <script src="/ultra/Styles/waitMe/waitMe.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/JQuery/uploader/vendor/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/JQuery/uploader/jquery.fileupload.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/Common/FileUploader.js"></script>
    <style>
        body#l_frame {
            overflow-y : auto;
        }
        input[type="number"]::-webkit-outer-spin-button,
        input[type="number"]::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }
        input[type='number'] {
            -moz-appearance:textfield;
        }
        input[disabled].form-control { 
            background-color : #efefef;
        }
        .badge{
            background-color : #FFF;
            color : #325e79;
        }
        .badge.status{
            color : #FFF;
            padding : 6px;
            font-weight : 600;
        }
        .badge.{
            background-color : #dd5468;
        }
        .badge.requesting{
            background-color : #67b8f3;
        }
        .badge.processing{
            background-color : #8bb544;
        }
        .badge.complete{
            background-color : #3581bf;
        }
        .badge.delivered{
            background-color : #8a6d3b;
        }
        .statusBtns .btn{
            color : #fff;
            background-color : #b7bcce;
            margin-right : 10px;
            margin-bottom : 10px;
        }
        .statusBtns .btn.active{
            background : #325e79;
        }
        #newBtn{
            margin-bottom : 10px;
        }
        .section{
            padding-top : 20px;
        }
        .w50{
            width : 50%;
        }
        .w70{
            width : 70%;
        }
        .inblock{
            display : inline-block;
        }
        input[disabled]{
            cursor : not-allowed;
        }
        .modal-table tbody td {
            color: #333;
        }
        .modal-table {
            margin-top : 10px;
            margin-bottom : 10px;
        }
        .comment-wrapper{
            padding : 10px 30px;
        }
        .comment-wrapper>div:first-child{
            margin-bottom : 10px;
        }
        .jsgrid-grid-header{
            overflow-y : auto;
        }
        #log{
            width : 96%;
            display : inline-block;
            background : #efefef;
            color : #333;
            padding : 10px 12px;
            box-shadow : none;
            border-color : #e0e0e0;
            font-size : 13px;
            border-radius : 4px;
            overflow-y : auto;
            height : 80px;
            font-weight : 500px;
        }
        #new-organization{
            display : inline-block;
            margin-right : 10px;
        }
        [disabled].form-control{
            color : #8d91a0;
            background-color: #efefef;
        }
        #log p{
            margin-bottom : 2px;
            font-weight : 500;
            color : #888;
        }
        .modal-header{
            padding-bottom : 0;
        }
        .attach-text{
            margin-right: 5px;
            margin-bottom: 5px;
        }
        .form-control[type=checkbox]{
            background-color: transparent;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderForm" Runat="Server">
    <div class="row">
        <%if (!auth) { %>
        <div class="col-sm-12">
            <div class="box-panel">
                <div class="panel-heading">
                    <div class="text-center"><%=errorMessage%></div>
                </div>
            </div>
        </div>
        <%} %>
        <%else { %>
        <div class="col-sm-12">
            
            <div class="box-panel">
                <div class="panel-heading">
			        <h1 class="panel-title">
                        HCP Inquiry
                    </h1>
                    <div class="pull-right">
                        <span>Rows : </span>
                        <select id="pagesize-list" class="form-control" style="width:100px; display:inline;" onchange="changeGridRows(this)">
                            <option selected="selected" value="15">15</option>
                            <option value="50">50</option>
                            <option value="100">100</option>
                        </select>
                    </div>
                </div>
                <div class="panel-body pd">
                    <div class="row">
                        <div class="section statusBtns col-md-10">
                            <button class="btn btn-sm active" onclick="filterBtnFn(this)" data-toggle="modal" data-target="#divCRMProductSearcherModal" type="button" data-value="ALL">
		                        All <span class="badge" id="count-all">0</span>
	                        </button>
                            <button class="btn btn-sm" onclick="filterBtnFn(this)" data-toggle="modal" data-target="#divCRMProductSearcherModal" type="button" data-value="REQUESTING">
		                        Requesting <span class="badge" id="count-requesting">0</span>
	                        </button>
                            <button class="btn btn-sm" onclick="filterBtnFn(this)" data-toggle="modal" data-target="#divCRMProductSearcherModal" type="button" data-value="PROCESSING">
		                        Processing <span class="badge" id="count-processing">0</span>
	                        </button>
                            <button class="btn btn-sm" onclick="filterBtnFn(this)" data-toggle="modal" data-target="#divCRMProductSearcherModal" type="button" data-value="COMPLETE">
		                        Complete <span class="badge" id="count-complete">0</span>
	                        </button>
                            <button class="btn btn-sm" onclick="filterBtnFn(this)" data-toggle="modal" data-target="#divCRMProductSearcherModal" type="button" data-value="DELIVERED">
		                        Delivered <span class="badge" id="count-delivered">0</span>
	                        </button>
                        </div>
                        <div class="section col-md-2 text-right">
                            <button id="newBtn" class="btn btn-sm btn-navy" onclick="newBtnFn()" data-toggle="modal" data-target="#divCRMProductSearcherModal" type="button">
		                        <span class="fa fa-plus"></span> New
	                        </button>
                        </div>
                        <div class="section col-md-12">
                            <div id="HCPInquiryGrid">
                            </div>
                        </div>
                        
                        <div class="section col-md-12">
                            <button id="sendConfirmBtn" onclick="sendConfirmFn()" type="button" class="btn btn-sm btn-navy hidden"><span class="glyphicon glyphicon-ok" aria-hidden="true"></span> Confirm</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%} %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentModalBody" Runat="Server">
    <!-- Modal ms-->
    <div class="modalTab">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <div data-user-hidden="new" data-admin-hidden="new">
                <div class="form-control" id="log" rows="3" readonly disabled>
                </div>
            </div>
        </div>
        <div class="modal-body">
            <div class="text-right"><small>Administrator : Sumin Jo, DaeRo Lee</small></div>
            <div class="modal-table">
                <table class="write">
                    <colgroup>
                        <col style="width: 22%">
                        <col>
                    </colgroup>
                    <tbody>
                        <tr>
                            <th scope="row">*Requester</th>
                            <td><span id="requesterText"></span></td>
                        </tr>
                        <tr data-user-hidden="requesting processing complete delivered" data-admin-hidden=" requesting processing complete delivered">
                            <th scope="row">*Customer Name</th>
                            <td>
                                <input id="name" type="text" name="NAME" class="form-control inblock w50" data-user-disabled="" onkeypress="inputEnter('nameSearchBtn', event)"/>
                            </td>
                        </tr>
                        <tr data-user-hidden="new " data-admin-hidden="new">
                            <th scope="row">*Customer Name</th>
                            <td>
                                <span id="name_text"></span>
                            </td>
                        </tr>
                        <tr data-user-hidden="requesting processing complete delivered" data-admin-hidden=" requesting processing complete delivered">
                            <th scope="row">Customer ORG</th>
                            <td>
                                <input id="org" type="text" name="CUSPOMER_ORG" class="form-control inblock w50" data-user-disabled="" onkeypress="inputEnter('nameSearchBtn', event)"/>
                            </td>
                        </tr>
                        <tr data-user-hidden="new " data-admin-hidden="new">
                            <th scope="row">Customer ORG</th>
                            <td>
                                <span id="org_text"></span>
                            </td>
                        </tr>
                        <tr data-user-hidden="requesting processing complete delivered" data-admin-hidden=" requesting processing complete delivered">
                            <th scope="row">Customer Specialty</th>
                            <td>
                                <input id="specialty" type="text" name="SPECIALTY" class="form-control inblock w50" data-user-disabled="" onkeypress="inputEnter('nameSearchBtn', event)"/>
                                <input id="request-type" type="hidden" name="REQUEST_TYPE"/>
                                <input id="doc-onekey-code" type="hidden" name="DOC_ONEKEY_CODE"/>
                                <button type="button" id="nameSearchBtn" onclick="searchBtnFn()" class="btn btn-navy btn-md" style="margin-bottom:3px;" data-user-hidden="">Search</button>
                                <button type="button" id="resethBtn" onclick="resetBtnFn()" class="btn btn-red btn-md hidden" style="margin-bottom:3px;" data-all-hidden="new">Reset</button>
                            </td>
                        </tr>
                        <tr data-user-hidden="new " data-admin-hidden="new">
                            <th scope="row">Customer Specialty</th>
                            <td>
                                <span id="spe_text"></span>
                            </td>
                        </tr>

                        <tr id="result-wrapper" class="hidden">
                            <th scope="row">*Result</th>
                            <td>
                                <select id="result" class="form-control w70" onchange="selectResultFn(this)">
                                </select>
                            </td>
                        </tr>

                        <tr class="new-item update-item" data-user-hidden="new requesting processing complete delivered" data-admin-hidden="new requesting processing complete delivered">
                            <th scope="row">*Period (Year)</th>
                            <td class="row period-wrapper">
                                <div class="col-sm-6">
                                    <label for="male">
                                        From 
                                        <select id="select-from" class="form-control inblock" style="width:100px" onchange="selectYear('from',this)">

                                        </select>
                                    </label>
                                </div>
                                <div class="col-sm-6">
                                    <label for="female">
                                        To 
                                        <select id="select-to" class="form-control inblock" style="width:100px" onchange="selectYear('to',this)">

                                        </select>
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr data-user-hidden="new " data-admin-hidden="new">
                            <th scope="row">*Period (Year)</th>
                            <td class="row">
                                <span id="period_text"></span>
                            </td>
                        </tr>
                        <tr class="new-item update-item" data-user-hidden="new requesting processing complete delivered" data-admin-hidden="new requesting processing complete delivered">
                            <th scope="row"></th>
                            <td class="row agree-check">
                                <div class="col-sm-12" >
                                    <label>
                                        <input type="checkbox" id="agreeCheckbox" value="Y"/> 조회 청구자가 위 HCP 본인임을 확인합니다(필수).
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr data-user-hidden="new requesting processing complete delivered " data-admin-hidden="new requesting complete delivered">
                            <th scope="row">*Delivery Date(to MR)</th>
                            <td>
                                <div class="input-group date form_datetime" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtContractDate" style="max-width:200px">
                                    <input type="text" id="delivery-date" class="form-control" />
                                    <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                </div>
                            </td>
                        </tr>
                        <tr data-user-hidden="new requesting processing " data-admin-hidden="new requesting processing ">
                            <th scope="row">*Delivery Date(to MR)</th>
                            <td>
                                <span id="delivery_text"></span>
                            </td>
                        </tr>
                        <tr data-user-hidden="new requesting processing delivered " data-admin-hidden="new requesting processing complete delivered">
                            <th scope="row">*Delivery Date(toHCP)</th>
                            <td>
                                <div class="input-group date form_datetime" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtContractDate" style="max-width:200px">
                                    <input type="text" id="delivery-date-toHCP" class="form-control" />
                                    <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                </div>
                                <div class="col-sm-12" >
                                    <label>
                                        <input type="checkbox" id="agreeCheckbox_1" value="Y"/> HCP 본인에게 상기 일자에 자료를 전달 하였습니다.(필수)
                                    </label>
                                </div>

                            </td>
                        </tr>
                        <tr data-user-hidden="new requesting processing complete" data-admin-hidden="new requesting processing complete">
                            <th scope="row">*Delivery Date(toHCP)</th>
                            <td>
                                <span id="delivery_text_toHCP"></span>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div data-user-hidden="requesting processing complete" data-admin-hidden=" requesting processing complete">
                <small>
                    * The fields with “*” need to be mandatorily input. In case of no data/attachment, error message has to be shown.<br />
                    * This request goes to DM Specialist(Sumin Jo) via email when the requester clicks “Request” button
                </small>
            </div>
        </div>
        <div class="modal-footer">
            <button type="button" id="closeBtn" class="btn btn-lg btn-navy" data-dismiss="modal">Close</button>
            <button type="button" onclick="requestBtnFn()" class="btn btn-lg btn-red" data-all-hidden="requesting processing complete delivered" data-all-disabled="new processing complete delivered">Request</button>
            <button type="button" onclick="confirmBtnFn()" class="btn btn-lg btn-red" data-admin-hidden="new processing complete delivered" data-user-hidden="new requesting processing complete delivered">Confirm</button>
            <button type="button" onclick="completeBtnFn()" class="btn btn-lg btn-red" data-admin-hidden="new requesting complete delivered" data-user-hidden="new requesting processing complete delivered">Complete</button>
            <button type="button" onclick="deliveredBtnFn()" class="btn btn-lg btn-red" data-admin-hidden="new requesting processing complete delivered" data-user-hidden="new requesting processing delivered">Delivered</button>
        </div>
    </div>
    <iframe id="iframeFileDown" width="0" height="0"></iframe>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cpHolderBottom" Runat="Server">
    <input type="hidden" id="hhdUserID" runat="server" />
    <input type="hidden" id="hhdUserName" runat="server" />
    <input type="hidden" id="hhdUserOrgName" runat="server" />
    <input type="hidden" id="hhdUserEmail" runat="server" />
    <input type="hidden" id="hddProcessStatus" runat="server" />
    <input type="hidden" id="hddProcessID" runat="server" />
    <input type="text" id="userType" value="END USER" runat="server" />
    <input type="hidden" id="hddUserID" runat="server" />
    <input type="hidden" id="hhdStatus" runat="server" />
    
    <script src="../../Scripts/Pages/Report/HCPInquiry.js"></script>
    <script src="../../Scripts/JQuery/cookie.js"></script>
    <script src="../../Scripts/JQuery/jquery-ui-1.12.1.custom/jquery-ui.js"></script>
</asp:Content>
