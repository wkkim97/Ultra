<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventDelegation.ascx.cs" Inherits="Pages_Event_Controls_EventDelegation" %>

<div class="box-panel">
    <ul class="nav nav-tabs nav-tabs-md" role="tablist">
        <li role="presentation" class="active"><a href="#tab_delegation" data-toggle="tab" role="tab" aria-controls="tab_delegation">Delegation</a></li>
    </ul>
    <div id="tab_delegation" class="tab-cont-area row row-md">
        <!-- // 닫으려면 class closed 추가 -->
        <div class="write-area">
            <div class="box-panel">
                <div id="divEtcDelegation" class="etc-delegation">
                    <div class="form-group">
                        <label for="lb_keyword">Search User</label>
                        <div class="row row-sm">
                            <div class="col-xs-9">
                                <input type="text" id="lb_keyword" class="form-control">
                            </div>
                            <div class="col-xs-3">
                                <button id="btnSearchDelegationEmployee" type="button" class="btn btn-sm btn-block btn-darkgray">Search</button>
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
                            <table id="tblEE_Employee">
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
                    <div class="form-group row row-sm">
                        <%----2022.04.18 comment out below line, INC15160130--%>
<%--                        <label>Role for participant input</label>--%>
                        <%----2022.04.18 add below line, INC15160130--%>
                        <label>Role for participant status change</label>
                        <div id="divRdoDelegationRole" class="radio">
                            <label>
                                <input type="radio" name="lb_role" checked value="Only Participants">
                                Assigned participants</label>
                            <label>
                                <input type="radio" name="lb_role" value="All">
                                All participants</label>
                        </div>
                    </div>
                </div>
                <div class="btnset">
                    <%--<button id="btnAddParticipant" type="button" class="btn btn-sm btn-block btn-red">Add Attend</button>--%>
                    <%--<button type="submit" class="btn btn-sm btn-navy fl"><i class="fa fa-trash-o"></i>Delete</button>--%>
                    <button id="btnAddDelegation" type="button" class="btn btn-sm btn-block btn-red"><i class="fa fa-floppy-o"></i>Add</button>
                </div>
                <button type="button" class="btn-panel-close"><i class="fa fa-close"><span class="tts">Close</span></i></button>
            </div>
            <!-- // .box-panel -->
        </div>
        <!-- // .write-area -->
        <div class="list-area">
            <button type="button" class="btn-panel-open"><i class="fa fa-plus-square"><span class="tts">Open</span></i></button>
            <div class="list-ctrl row">
                <div class="col-xs-12 text-right">
                    <button id="btnDeleteDelegation" type="button" class="btn btn-sm bg-gray"><i class="fa fa-remove"></i>Delete</button>
                </div>
            </div>
            <div id="jsGridDelegation"></div>
        </div>
        <!-- // .list-area  -->
    </div>
    <!-- // .row-md -->
</div>
<!-- // .box-panel -->
