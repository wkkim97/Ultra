<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Medical.master" AutoEventWireup="true" CodeFile="NonOneKey.aspx.cs" Inherits="Pages_Medical_Study_StudyList" %>

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
        .badge.rejected{
            background-color : #dd5468;
        }
        .badge.requesting{
            background-color : #67b8f3;
        }
        .badge.registering{
            background-color : #8bb544;
        }
        .badge.complete{
            background-color : #3581bf;
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
                        Non One-key
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
                            <button class="btn btn-sm" onclick="filterBtnFn(this)" data-toggle="modal" data-target="#divCRMProductSearcherModal" type="button" data-value="REJECTED">
		                        Rejected <span class="badge" id="count-rejected">0</span>
	                        </button>
                            <button class="btn btn-sm" onclick="filterBtnFn(this)" data-toggle="modal" data-target="#divCRMProductSearcherModal" type="button" data-value="REQUESTING">
		                        Requesting <span class="badge" id="count-requesting">0</span>
	                        </button>
                            <button class="btn btn-sm" onclick="filterBtnFn(this)" data-toggle="modal" data-target="#divCRMProductSearcherModal" type="button" data-value="REGISTERING">
		                        Registering <span class="badge" id="count-registering">0</span>
	                        </button>
                            <button class="btn btn-sm" onclick="filterBtnFn(this)" data-toggle="modal" data-target="#divCRMProductSearcherModal" type="button" data-value="COMPLETE">
		                        Complete <span class="badge" id="count-complete">0</span>
	                        </button>
                        </div>
                        <div class="section col-md-2 text-right">
                            <button id="newBtn" class="btn btn-sm btn-navy" onclick="newBtnFn()" data-toggle="modal" data-target="#divCRMProductSearcherModal" type="button">
		                        <span class="fa fa-plus"></span> New
	                        </button>
                        </div>
                        <div class="section col-md-12">
                            <div id="nonOneKeyGrid">
                            </div>
                        </div>
                        <div class="section col-md-12">
                            <button id="sendRequestBtn" onclick="sendRequestFn()" type="button" class="btn btn-sm btn-red hidden" style="margin-right:10px;"><span class="glyphicon glyphicon-share-alt" aria-hidden="true"></span> Send request</button>
                            <button id="sendCompleteBtn" onclick="sendCompleteFn()" type="button" class="btn btn-sm btn-navy hidden"><span class="glyphicon glyphicon-ok" aria-hidden="true"></span> Complete</button>
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
                        <tr data-user-hidden="requesting registering complete" data-admin-hidden="rejected requesting registering complete">
                            <th scope="row">*Customer Type</th>
                            <td>
                                <select id="customer-type" class="form-control w70" name="CUSTOMER_TYPE" data-user-disabled="rejected">
                                    <option disabled="disabled" selected="selected" value="">선택</option>
                                    <option value="방사선사">방사선사</option>
                                    <option value="간호사">간호사</option>
                                    <option value="약사">약사</option>
                                    <option value="인턴">인턴</option>
                                </select>
                            </td>
                        </tr>
                        <tr data-user-hidden="new rejected" data-admin-hidden="new">
                            <th scope="row">*Customer Type</th>
                            <td>
                                <span id="customer_type_text"></span>
                            </td>
                        </tr>
                        <tr data-user-hidden="requesting registering complete" data-admin-hidden="rejected requesting registering complete">
                            <th scope="row">*Name</th>
                            <td>
                                <input id="name" type="text" name="NAME" class="form-control inblock w50" data-user-disabled="rejected" onkeypress="inputEnter('nameSearchBtn', event)"/>
                                <input id="request-type" type="hidden" name="REQUEST_TYPE"/>
                                <input id="doc-onekey-code" type="hidden" name="DOC_ONEKEY_CODE"/>
                                <button type="button" id="nameSearchBtn" onclick="searchBtnFn()" class="btn btn-navy btn-md" style="margin-bottom:3px;" data-user-hidden="rejected">Search</button>
                                <button type="button" id="resethBtn" onclick="resetBtnFn()" class="btn btn-red btn-md hidden" style="margin-bottom:3px;" data-all-hidden="new">Reset</button>
                            </td>
                        </tr>
                        <tr data-user-hidden="new rejected" data-admin-hidden="new">
                            <th scope="row">*Name</th>
                            <td>
                                <span id="name_text"></span>
                            </td>
                        </tr>
                        <tr id="result-wrapper" class="hidden">
                            <th scope="row">*Result</th>
                            <td>
                                <select id="result" class="form-control w70" onchange="selectResultFn(this)">
                                </select>
                            </td>
                        </tr>
                        <tr class="new-item update-item" data-user-hidden="new requesting registering complete" data-admin-hidden="new rejected requesting registering complete">
                            <th scope="row">*Gender</th>
                            <td class="row gender-wrapper">
                                <div class="col-sm-6">
                                    <label for="male">
                                        <input id="male" name="GENDER" value="남성" type="radio">
                                        남성
                                    </label>
                                </div>
                                <div class="col-sm-6">
                                    <label for="female">
                                        <input id="female" name="GENDER" value="여성" type="radio"> 
                                        여성
                                    </label>
                                </div>
                            </td>
                        </tr>
                        <tr data-user-hidden="new rejected" data-admin-hidden="new">
                            <th scope="row">*Gender</th>
                            <td class="row">
                                <span id="gender_text"></span>
                            </td>
                        </tr>
                        <tr class="new-item" data-user-hidden="new requesting registering complete" data-admin-hidden="new rejected requesting registering complete">
                            <th scope="row">*Organization</th>
                            <td>
                                <div>
                                    <input id="search-hospital-text1" type="text" class="form-control inblock w50" onkeypress="inputEnter('organizationBtn', event)"/>
                                    <button type="button" onclick="searchHospitalBtnFn(1)" class="btn btn-navy btn-md" style="margin-bottom:3px;" id="organizationBtn">Search</button>
                                </div>
                                <div>
                                    <select id="organization" name="ORGANIZATION" class="form-control w70 hidden">
                                    </select>
                                </div>
                            </td>
                        </tr>
                        <tr class="new-item text-item" data-user-hidden="new rejected" data-admin-hidden="new">
                            <th scope="row">*Organization</th>
                            <td class="row">
                                <span id="organization_text"></span>
                            </td>
                        </tr>
                        <tr class="update-item" data-user-hidden="requesting registering complete" data-admin-hidden="rejected requesting registering complete">
                            <th scope="row">*New organization</th>
                            <td>
                                <div>
                                    <input id="search-hospital-text2" type="text" class="form-control inblock w50" onkeypress="inputEnter('newOrgBtn', event)"/>
                                    <button id="newOrgBtn" type="button" onclick="searchHospitalBtnFn(2)" class="btn btn-navy btn-md" style="margin-bottom:3px;">Search</button>
                                </div>
                                <select id="new-organization" name="ORGANIZATION" class="form-control w70 hidden">
                                </select>
                                <label>
                                    <input type="checkbox" id="deleteCheckbox" onclick="deleteRequest(this)"/> 휴직/퇴사
                                </label>
                            </td>
                        </tr>
                        <tr class="update-item text-item" data-user-hidden="new rejected" data-admin-hidden="new">
                            <th scope="row">*New Organization</th>
                            <td class="row">
                                <span id="new_organization_text"></span>
                            </td>
                        </tr>
                        <tr class="new-item update-item" data-user-hidden="requesting registering complete" data-admin-hidden="rejected requesting registering complete">
                            <th scope="row">*Remark</th>
                            <td>
                                <textarea class="form-control" name="REMARK" id="remark"></textarea>
                            </td>
                        </tr>
                        <tr data-user-hidden="new rejected" data-admin-hidden="new">
                            <th scope="row">*Remark</th>
                            <td class="row">
                                <span id="remark_text"></span>
                            </td>
                        </tr>
                        <tr id="attachment-wrapper" class="new-item update-item">
                            <th scope="row">*Attachment</th>
                            <td>
                                <div id="divAttachFiles_ReportNonOnekey" class="attach" data-attachment-type="ReportNonOnekey">
                                    <div class="attach-heading" style="border-bottom: 1px solid #e0e0e0">
                                        <span class="btn btn-sm btn-navy fileinput-button" style="margin-top:10px;" data-user-hidden="requesting registering complete" data-admin-hidden="rejected requesting registering complete">
                                            <i class="fa fa-paperclip"></i>
                                            <span>Attachment</span>
                                            <!-- The file input field used as target for the file upload widget -->
                                            <input class="fileupload" id="fileupload" type="file" name="files[]" multiple />
                                        </span>
                                        <!-- The global progress bar -->
                                        <!-- The container for the uploaded files -->
                                    </div>
                                    <div id="progress" class="progress" style="height: 7px !important; display: none">
                                        <div class="progress-bar progress-bar-success">
                                        </div>
                                    </div>
                                    <div id="files" class="files">
                                        <ul class="attach-list"></ul>
                                    </div>
                                </div>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div data-user-hidden="requesting registering complete" data-admin-hidden="rejected requesting registering complete">
                <small>
                    * The fields with “*” need to be mandatorily input. In case of no data/attachment, error message has to be shown.<br />
                    * This request goes to DM Specialist(Sumin Jo) via email when the requester clicks “Request” button
                </small>
            </div>
            <div class="comment-wrapper" data-user-hidden="new rejected requesting registering complete" data-admin-hidden="new rejected complete">
                <div>Comment</div>
                <div>
                    <input id="comment" type="text" name="COMMENT" class="form-control" placeholder="Please insert reject comment..."/>
                </div>
            </div>
        </div>
        <div class="modal-footer">
            <button type="button" id="closeBtn" class="btn btn-lg btn-navy" data-dismiss="modal">Close</button>
            <button type="button" onclick="requestBtnFn()" class="btn btn-lg btn-red" data-all-hidden="rejected requesting registering complete" data-all-disabled="new rejected registering complete">Request</button>
            <button type="button" onclick="rejectBtnFn()" class="btn btn-lg btn-red" data-admin-hidden="new rejected complete" data-user-hidden="new rejected requesting registering complete">Reject</button>
            <button type="button" onclick="reRequestBtnFn()" class="btn btn-lg btn-red" data-admin-hidden="new rejected requesting registering complete" data-user-hidden="new requesting registering complete">Re-request</button>
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
    <input type="hidden" id="userType" value="END USER" runat="server" />
    <input type="hidden" id="hddUserID" runat="server" />
    <input type="hidden" id="hhdStatus" runat="server" />
    
    <script src="../../Scripts/Pages/Report/NonOneKey.js"></script>
    <script src="../../Scripts/JQuery/cookie.js"></script>
    <script src="../../Scripts/JQuery/jquery-ui-1.12.1.custom/jquery-ui.js"></script>
</asp:Content>
