<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Admin.master" AutoEventWireup="true" CodeFile="EventConfiguration.aspx.cs" Inherits="Pages_Admin_EventConfiguration" %>

<%@ Register Src="~/Common/Controls/EmployeeSearcher.ascx" TagPrefix="uc1" TagName="EmployeeSearcher" %>
<%@ Register Src="~/Common/Controls/EmployeeSearcherModal.ascx" TagPrefix="uc1" TagName="EmployeeSearcherModal" %>



<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style>
        #tpGenernalInfo input[type=checkbox], input[type=radio] {
            margin-top: 0px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderContent" runat="Server">
    <div class="panel panel-topinfo">
        <div class="panel-body">
            <div class="panel-heading">
                <div class="row">
                    <table class="table-default">
                        <colgroup>
                            <col style="width: 100px" />
                            <col style="width: 200px" />
                            <col />
                        </colgroup>
                        <tr>
                            <th scope="row">UlTra Event </th>
                            <td>
                                <select id="selEventList" class="form-control"></select></td>
                            <td>
                                <div class="btn-group btn-group-sm" role="group">
                                    <button id="btnNew" type="button" class="btn btn-default">New</button>
                                    <button id="btnSaveEventConfiguration" type="button" class="btn btn-default">Save</button>
                                </div>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
    </div>
    <ul id="tabEventConfiguration" class="nav nav-tabs nav-tabs-fixcenter" role="tablist">
        <li id="tabGenernalInfo" runat="server" role="presentation" class="active"><a href="#tpGenernalInfo" aria-controls="tpGenernalInfo" role="tab" data-toggle="tab">General Information</a></li>
        <li id="tabApproval" runat="server" role="presentation"><a href="#tpApproval" aria-controls="tpApproval" role="tab" data-toggle="tab">Additional Apprver/Recipient/Reviewer</a></li>
    </ul>
    <div class="tab-content">
        <div role="tabpanel" class="tab-pane active" id="tpGenernalInfo">
            <div class="box-panel">
                <div class="panel panel-dashboard">
                    <div class="panel-heading">
                        <h3 class="panel-title">Information</h3>
                    </div>
                    <div class="panel-body">
                        <table class="table form-group-sm" style="margin-bottom: 0px;">
                            <colgroup>
                                <col style="width: 25%" />
                                <col style="width: 25%;" />
                                <col style="width: 25%;" />
                                <col />
                            </colgroup>
                            <tbody>
                                <tr>
                                    <th>Event Name</th>
                                    <td>
                                        <input type="text" id="txtEventName" class="form-control" />
                                    </td>
                                    <th>Data Owner</th>
                                    <td>
                                        <div id="divDataOwner">
                                            <uc1:EmployeeSearcher runat="server" ID="empSearcherDataOwner" />
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <th>TableName</th>
                                    <td>
                                        <input type="text" id="txtTableName" class="form-control" /></td>
                                    <th>Page Name</th>
                                    <td>
                                        <input type="text" id="txtPageName" class="form-control" /></td>
                                </tr>
                                <tr>
                                    <th>Company</th>
                                    <td>
                                        <div id="divCompany">
                                        </div>
                                    </td>
                                    <th>Prefix of Event</th>
                                    <td>
                                        <input type="text" id="txtEventPrefix" class="form-control" /></td>
                                </tr>
                                <tr>
                                    <th>Skip Approval</th>
                                    <td>
                                        <div class="radio">
                                            <label>
                                                <input type="radio" name="optSkipApproval" id="optUsingApprovalYes" value="Y" checked />
                                                Yes
                                            </label>
                                            <label>
                                                <input type="radio" name="optSkipApproval" id="optUsingApprovalNo" value="N" />No
                                            </label>
                                        </div>
                                    </td>
                                    <th>Only Approval</th>
                                    <td>
                                        <div class="radio">
                                            <label>
                                                <input type="radio" name="optOnlyApproval" id="optOnlyApprovalYes" value="Y" />
                                                Yes
                                            </label>
                                            <label>
                                                <input type="radio" name="optOnlyApproval" id="optOnlyApprovalNo" value="N" checked />No
                                            </label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <th>Information Classification</th>
                                    <td>
                                        <select id="optInfoClassification" class="form-control"></select>
                                    </td>
                                    <th>Retention Period</th>
                                    <td>
                                        <select id="optRetentionPeriod" class="form-control"></select>
                                    </td>
                                </tr>
                                <tr>
                                    <th>Show Event List</th>
                                    <td>
                                        <div class="radio">
                                            <label>
                                                <input type="radio" name="optShowEventList" id="optShowEventListYes" value="Y" checked />
                                                Yes
                                            </label>
                                            <label>
                                                <input type="radio" name="optShowEventList" id="optShowEventListNo" value="N" />No
                                            </label>
                                        </div>
                                    </td>
                                    <th></th>
                                    <td></td>

                                </tr>
                                <tr>
                                    <th>Service Name</th>
                                    <td colspan="3">
                                        <input type="text" id="txtServiceName" class="form-control" /></td>
                                </tr>
                                <tr>
                                    <th>Description</th>
                                    <td colspan="3">
                                        <textarea id="txtEventDesc" class="form-control" rows="2"></textarea>
                                    </td>
                                </tr>

                            </tbody>
                        </table>
                    </div>

                    <div class="panel-heading">
                        <h3 class="panel-title">Approval</h3>
                    </div>
                    <div class="panel-body">
                        <table class="table form-group-sm">
                            <tbody>
                                <tr>
                                    <th class="col-md-3">Forward</th>
                                    <td class="col-md-9">
                                        <div class="radio">
                                            <label>
                                                <input type="radio" name="optForward" id="optForwardYes" value="Y" checked />
                                                Yes
                                            </label>
                                            <label>
                                                <input type="radio" name="optForward" id="optForwardNo" value="N" />No
                                            </label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <th class="col-md-3">Add Reviewer</th>
                                    <td class="col-md-12">
                                        <div class="radio">
                                            <label>
                                                <input type="radio" name="optAddReviewer" id="optAddReviewerYes" value="Y" checked />
                                                Yes
                                            </label>
                                            <label>
                                                <input type="radio" name="optAddReviewer" id="optAddReviewerNo" value="N" />No
                                            </label>
                                            <input type="text" id="txtReviewerDesc" class="form-control" />
                                        </div>

                                    </td>
                                </tr>
                                <tr>
                                    <th class="col-md-3">Default Line</th>
                                    <td class="col-md-12">
                                        <table class="table form-group-sm" style="margin-bottom: 0px;">
                                            <colgroup>
                                                <col style="width: 25%" />
                                                <col />
                                            </colgroup>
                                            <tbody>
                                                <tr>
                                                    <th>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optDefaultApproval" id="optApprovalLevel" value="0001" checked />
                                                                Approval Level
                                                            </label>
                                                        </div>
                                                    </th>
                                                    <td>
                                                        <select id="selApprovalLevel" class="form-control">
                                                            <option value="1">1</option>
                                                            <option value="2">2</option>
                                                            <option value="3">3</option>
                                                            <option value="4">4</option>
                                                            <option value="5">5</option>
                                                        </select></td>
                                                    <th>
                                                        <div class="radio">
                                                            <label>
                                                                <input type="radio" name="optDefaultApproval" id="optJobTitle" value="0002" />
                                                                Job Title
                                                            </label>
                                                        </div>
                                                    </th>
                                                    <td>
                                                        <select id="selJobTitle" class="form-control"></select></td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>

                            </tbody>
                        </table>
                    </div>
                </div>

                <div class="panel panel-dashboard">

                    <div class="panel-heading">
                        <h3 class="panel-title">Module</h3>
                    </div>

                    <div class="panel-body">

                        <table class="table form-group-sm table-bordered">
                            <colgroup>
                                <col style="width: 25%;" />
                                <col style="width: 25%;" />
                                <col />
                                <col style="width: 25%;" />
                            </colgroup>
                            <tr style="height: 35px;">
                                <th>
                                    <div class="form-inline">
                                        <label>
                                            <input type="checkbox" value="" id="chkAddCostPlan" />
                                            Cost Plan</label>
                                    </div>
                                </th>
                                <th>
                                    <div class="form-inline">
                                        <label>
                                            <input type="checkbox" value="" id="chkAddParticipants" />
                                            Participants</label>
                                    </div>
                                </th>
                                <th>
                                    <div class="form-inline">
                                        <label class="">
                                            <input type="checkbox" value="" id="chkAddAgenda" />
                                            Agenda</label>
                                    </div>
                                </th>
                                <th>
                                    <div class="form-inline">
                                        <label class="">
                                            <input type="checkbox" value="" id="chkAddPayment" />
                                            Payment</label>
                                    </div>
                                </th>

                            </tr>
                            <tr>
                                <td>
                                    <div class="dropup">
                                        <button type="button" class="btn btn-sm dropdown-toggle form-control" data-toggle="dropdown" aria-haspopup="true" aria-expanded="false">
                                            Cost Category<span class="caret"></span>
                                        </button>
                                        <ul class="dropdown-menu" id="selCostCategory">
                                        </ul>
                                    </div>
                                </td>
                                <td>
                                    <label class="checkbox-inline">
                                        <input type="checkbox" value="" id="chkCheckCountRule" />
                                        Check Count Rule</label>
                                </td>
                                <td>
                                    <select id="optAgendaRoleType" class="form-control">
                                        <option value="Select">선택</option>
                                        <option value="Lecture">강연</option>
                                        <option value="Consulting">자문</option>
                                    </select></td>
                                <td></td>
                            </tr>
                        </table>
                    </div>
                </div>

                <div class="panel panel-dashboard">

                    <div class="panel-heading">
                        <h3 class="panel-title">Event Complete</h3>
                    </div>

                    <div class="panel-body">

                        <table class="table form-group-sm table-bordered">
                            <colgroup>
                                <col style="width: 25%;" />
                                <col style="width: 25%;" />
                                <col />
                                <col style="width: 25%;" />
                            </colgroup>
                            <tr>
                                <th>Comment</th>
                                <td>
                                    <div class="radio">
                                        <label>
                                            <input type="radio" name="optEventCompleteComment" id="optEventCompleteCommentYes" value="Y" checked />
                                            Yes
                                        </label>
                                        <label>
                                            <input type="radio" name="optEventCompleteComment" id="optEventCompleteCommentNo" value="N" />No
                                        </label>
                                    </div>
                                    <select id="selCommentCategory" class="form-control"></select>
                                </td>
                                <th>Attach</th>
                                <td>
                                    <div class="radio">
                                        <label>
                                            <input type="radio" name="optEventCompletAttach" id="optEventCompletAttachYes" value="Y" checked />
                                            Yes
                                        </label>
                                        <label>
                                            <input type="radio" name="optEventCompletAttach" id="optEventCompletAttachNo" value="N" />No
                                        </label>
                                    </div>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>

            </div>
            <!-- box-panle -->
        </div>
        <div role="tabpanel" class="tab-pane" id="tpApproval">
            <div class="box-panel">
                <div class="tab-cont-area row row-sm">
                    <div class="">
                        <div class="box-panel">
                            <div class="form-group-sm">
                                <label for="selAppreoverType">Approver Type</label>
                                <select id="selAppreoverType" class="form-control">
                                    <option value="B">Before Approver</option>
                                    <option value="A">After Approver</option>
                                    <option value="R">Recipient</option>
                                    <option value="V">Reviewer</option>
                                </select>
                            </div>
                            <div class="form-group-sm">
                                <label for="lb_approver">Approver</label>
                                <table class="table form-group-sm" style="border: none !important; border-width: 0px !important; margin-bottom: 0px;">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <div id="divApprover">
                                                    <uc1:EmployeeSearcher runat="server" ID="EmployeeSearcher" />
                                                </div>
                                            </td>
                                            <td>
                                                <div class="checkbok">
                                                    <label>
                                                        <input type="checkbox" value="" id="chkIsMandatory" />
                                                        <span>Mandatory</span></label>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>

                            </div>
                            <div class="form-group-sm">
                                <label for="lb_approver">Condition</label>
                                <div class="input-group">
                                    <span class="input-group-btn" style="width: 240px;">
                                        <select id="selTableColumn" class="form-control"></select>
                                    </span>
                                    <span class="input-group-btn" style="width: 120px;">
                                        <select id="selCondition" class="form-control">
                                            <option value="Equals">Equals</option>
                                            <option value="GreaterThan">GreaterThan</option>
                                            <option value="LessThan">LessThan</option>
                                            <option value="StartWith">StartWith</option>
                                            <option value="NotStartWith">NotStartWith</option>
                                            <option value="Include">Include</option>
                                            <option value="NotInclude">NotInclude</option>
                                            <option value="IsNull">IsNull</option>
                                            <option value="IsNotNull">IsNotNull</option>

                                        </select>
                                    </span>
                                    <input type="text" class="form-control" id="txtConditionValue" />
                                    <span class="input-group-btn" style="width: 80px;">
                                        <select id="selAndOr" class="form-control">
                                            <option value="And">And</option>
                                            <option value="Or">Or</option>
                                        </select>
                                    </span>
                                    <span class="input-group-btn">
                                        <button class="btn btn-sm btn-red" type="button" id="btnAddCondition"><i class="fa fa-plus"></i></button>
                                    </span>
                                </div>
                            </div>
                            <div class="tb-scroll">
                                <div id="divCondition"></div>
                            </div>
                            <div class="btnset">
                                <button type="button" class="btn btn-sm btn-navy fl" id="btnNewCondition"><i class="fa fa-trash-o"></i>New</button>
                                <button type="button" class="btn btn-sm btn-red fr" id="btnSaveCondition"><i class="fa fa-floppy-o"></i>Save</button>
                            </div>
                            <button type="button" class="btn-panel-close"><i class="fa fa-close"><span class="tts">Close</span></i></button>
                        </div>
                        <!-- // .box-panel -->
                    </div>
                    <!-- // .write-area -->
                    <div class="">
                        <button type="button" class="btn-panel-open"><i class="fa fa-plus-square"><span class="tts">Open</span></i></button>
                        <div class="box-panel">
                            <div class="form-group-sm">
                                <label for="divBeforeApprover">Before Approver</label>
                                <div id="divBeforeApprover"></div>
                                <label for="divAfterApprover">After Approver</label>
                                <div id="divAfterApprover"></div>
                                <label for="divRecipient">Recipient</label>
                                <div id="divRecipient"></div>
                                <label for="divReviewer">Reviewer</label>
                                <div id="divReviewer"></div>
                            </div>
                        </div>
                    </div>
                    <!-- // .list-area  -->
                </div>
            </div>
            <!-- // .row-md -->
        </div>

        <uc1:EmployeeSearcherModal runat="server" ID="EmployeeSearcherModal" />
        <input type="hidden" id="hddEventID" runat="server" value="" />
        <input type="hidden" id="hddApprovalType" value="" />
        <input type="hidden" id="hddConditionIDX" value="0" />
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderBottom" runat="Server">
    <script type="text/javascript" src="/ultra/Scripts/Pages/Admin/EventConfiguration.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/Common/EmployeeSearcher.js"></script>
</asp:Content>
