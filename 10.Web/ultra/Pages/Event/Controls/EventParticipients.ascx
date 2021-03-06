<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventParticipients.ascx.cs" Inherits="Pages_Event_Controls_EventParticipients" %>
<%@ Register Src="~/Common/Controls/HealthCareOfficeSeacherModal.ascx" TagPrefix="uc1" TagName="HealthCareOfficeSeacherModal" %>
<%@ Register Src="~/Common/Controls/HealthCareOfficeSeacher.ascx" TagPrefix="uc1" TagName="HealthCareOfficeSeacher" %>




<div id="divInputParticipants" class="box-panel">
    <div class="tab-cont-area row row-md">
        <!-- // 닫으려면 class closed 추가 -->
        <div class="write-area">
            <div class="box-panel">
                <div class="tabpanel-sm" role="tabpanel">
                    <ul id="tabAddParticipant" class="nav nav-tabs nav-tabs-justified" role="tablist">
                        <li role="presentation" class="active" id="liKoreaHCP"><a href="#tabKoreaHCP" data-toggle="tab" role="tab" aria-controls="tabKoreaHCP">HCP<br>
                            Korea</a></li>
                        <li role="presentation"><a href="#tabOtherHCP" data-toggle="tab" role="tab" aria-controls="tabOtherHCP">HCP<br>
                            Other</a></li>
                        <li role="presentation"><a href="#tabEmployee" data-toggle="tab" role="tab" aria-controls="tabEmployee">Bayer</a></li>
                        <li role="presentation" runat="server" id="liContract"><a href="#tabContract" data-toggle="tab" role="tab" aria-controls="tabContract">Contract</a></li>
                    </ul>
                    <div class="tab-content">
                        <div role="tabpanel" class="tab-pane tab-pane-korea active" id="tabKoreaHCP">
                            <div id="divSearchHCP" class="row row-sm">
                                <div class="col-xs-7 col-md-12">
                                    <div class="row row-sm">
                                        <div class="form-group col-xs-4">
                                            <label for="lb_name">Name(HCP)</label>
                                            <input type="text" id="lb_name" class="form-control" />
                                        </div>
                                        <div class="form-group col-xs-8">
                                            <label for="lb_hp">Hospital/Phamacy(HCO)</label>
                                            <input type="text" id="lb_hco" class="form-control" />
                                        </div>
                                    </div>
                                </div>
                                <div class="form-group col-xs-5 col-md-12">
                                    <div class="row row-sm">
                                        <label for="lb_row" class="lb_special col-md-4">Specialty</label>
                                        <div class="col-xs-8 col-md-4 col-lg-5">
                                            <input type="text" id="lb_special" class="form-control">
                                        </div>
                                        <div class="col-xs-4 col-md-4 col-lg-3">
                                            <button id="btnSearchHCP" type="button" class="btn btn-sm btn-block btn-darkgray">Search</button>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div class="tb-scroll">
                                <div class="tb-head">
                                    <table>
                                        <colgroup>
                                            <col style="width: 100%">
                                        </colgroup>
                                        <thead>
                                            <tr>
                                                <th></th>
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                                <!-- // .tb-head -->
                                <div class="tb-body">
                                    <table id="tblEP_HCPKorea">
                                        <colgroup>
                                            <col style="width: 100%" />
                                        </colgroup>
                                        <tbody>
                                        </tbody>
                                    </table>
                                </div>
                                <!-- // .tb-body -->
                            </div>
                            <!-- // .tb-scroll -->
                        </div>
                        <!-- // #tab1 -->
                        <div role="tabpanel" class="tab-pane tab-pane-other" id="tabOtherHCP">
                            <div class="row row-sm">
                                <div class="form-group" id="divHCPType" runat="server">
                                    <label for="selHCPType">HCP Type</label>
                                    <select id="selHCPType" class="form-control" runat="server"></select>
                                </div>
                                <div class="form-group" id="divHCOSearcher" runat="server">
                                    <label for="txtHCOName">Hospital/Phamacy(HCO)</label>
                                    <uc1:HealthCareOfficeSeacher runat="server" ID="HealthCareOfficeSeacher" />
                                </div>
                                <div class="form-group" id="divInputHCOName" runat="server">
                                    <label for="lb_other_hco_name">Hospital/Phamacy(HCO)</label>
                                    <input type="text" id="lb_other_hco_name" class="form-control">
                                </div>
                                <div class="form-group" id="divInputHCPName" runat="server">
                                    <label for="lb_other_hcp_name">Name(HCP)</label>
                                    <input type="text" id="lb_other_hcp_name" class="form-control">
                                </div>
                                <div class="form-group" id="divHCPCountry" runat="server">
                                    <label for="lb_country">Country</label>
                                    <select id="lb_country" class="form-control" runat="server">
                                    </select>
                                </div>
                            </div>
                        </div>
                        <!-- // #tab2 -->
                        <div role="tabpanel" class="tab-pane tab-pane-bayer" id="tabEmployee">
                            <div class="form-group">
                                <label for="lb_keyword">Search User</label>
                                <div class="row row-sm">
                                    <div class="col-xs-9">
                                        <input type="text" id="lb_keyword" class="form-control">
                                    </div>
                                    <div class="col-xs-3">
                                        <button id="btnSearchEmployee" type="button" class="btn btn-sm btn-block btn-darkgray">Search</button>
                                    </div>
                                </div>
                            </div>
                            <div class="tb-scroll">
                                <div class="tb-head">
                                    <table>
                                        <colgroup>
                                            <col style="width: 50%">
                                            <col>
                                        </colgroup>
                                        <thead>
                                            <tr>
                                                <th scope="col" class="text-left" style="padding-left: 23px;">Name</th>
                                                <th scope="col" class="text-left">Org.</th>
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                                <!-- // .tb-head -->
                                <div class="tb-body">
                                    <table id="tblEP_Employee">
                                        <colgroup>
                                            <col style="width: 50%">
                                            <col>
                                        </colgroup>
                                        <tbody>
                                        </tbody>
                                    </table>
                                </div>
                                <!-- // .tb-body -->
                            </div>
                            <!-- // .tb-scroll -->
                        </div>
                        <div role="tabpanel" class="tab-pane tab-pane-bayer" id="tabContract">
                            <div class="tb-scroll">
                                <div class="tb-head">
                                    <table>
                                        <colgroup>
                                            <col style="width: 100%">
                                        </colgroup>
                                        <thead>
                                            <tr>
                                                <th></th>
                                            </tr>
                                        </thead>
                                    </table>
                                </div>
                                <!-- // .tb-head -->
                                <div class="tb-body">
                                    <table id="tblEP_Contract">
                                        <colgroup>
                                            <col style="width: 100%" />
                                        </colgroup>
                                        <tbody>
                                        </tbody>
                                    </table>
                                </div>
                                <!-- // .tb-body -->
                            </div>
                            <!-- // .tb-scroll -->
                        </div>
                        <!-- // #tab3 -->
                        <div class="btnset">
                            <button id="btnAddParticipant" type="button" class="btn btn-sm btn-block btn-red"><i class="fa fa-floppy-o"></i>Add Attend</button>
                        </div>
                    </div>
                </div>
                <button type="button" class="btn-panel-close"><i class="fa fa-close"><span class="tts">Close</span></i></button>
            </div>
            <!-- // .box-panel -->
        </div>
        <!-- // .write-area -->
        <div class="list-area">
            <button type="button" class="btn-panel-open"><i class="fa fa-plus-square"><span class="tts">Open</span></i></button>
            <div class="list-ctrl row">
                <div class="col-xs-6">
                    <button id="btnIsAttended" type="button" class="btn btn-sm bg-green">Attend</button>
                    <button id="btnIsNotAttended" type="button" class="btn btn-sm bg-yellow">Not Attend</button>
                    <button id="btnDeleteParticipant" type="button" class="btn btn-sm bg-gray">Delete</button>
                </div>
                <div class="col-xs-6 text-right" id="tbnDownloadWrapper">
                    <!--1.0.7 ABM 이벤트에서 PV Attestation form 양식 링크 추가-->
                    <a href="https://bayergroup.sharepoint.com/:w:/r/sites/021843/ultra/ulink/RD-SOP-1271%20-%20Post%20advisory%20board%20AE_PTC%20reporting%20attestation%20form.docx?d=w4b65b5ea8bc247fda83813cefa28f758&csf=1&web=1&e=b6dC0I" id="tbnDownloadPV" target="_blank" class="btn btn-sm btn-darkgray" >PV Attestation form</a>

                    <button id="tbnDownloadAttendeeList" type="button" class="btn btn-sm btn-darkgray"><i class="fa fa-download"></i>AttendeeList</button>
                    <button id="tbnDownloadSRM" type="button" class="btn btn-sm btn-darkgray" disabled><i class="fa fa-download"></i>SmartBuy</button>
                </div>
            </div>
            <div id="jsGridPraticipants"></div>
            <!-- 참석자 목록 -->
        </div>
        <!-- // .list-area  -->
    </div>
    <!-- // .row-md -->
</div>
<!-- // .box-panel -->

<div id="layer_visit_participant" class="modal fade" tabindex="-1" role="dialog">
    <div class="modal-dialog modal-alert" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i aria-hidden="true" class="fa fa-times"></i></button>
                <h4 class="modal-title" id="myModalLabel">Visit</h4>
            </div>
            <div class="modal-body">
                <ul class='person-list person-list-crm'></ul>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-sm btn-success" data-dismiss="modal">Ok</button>
            </div>
        </div>
    </div>
</div>

<uc1:HealthCareOfficeSeacherModal runat="server" ID="HealthCareOfficeSeacherModal" />
<script type="text/javascript" src="/ultra/Scripts/Common/HealthCareOfficeSearcher.js"></script>
