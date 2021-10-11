<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Admin.master" AutoEventWireup="true" CodeFile="MohwReportList.aspx.cs" Inherits="Pages_Report_MOHW_MohwReportList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderContent" Runat="Server">
    <div class="panel panel-topinfo">
        <div class="panel-heading">
            <h1 class="panel-title">MOHW Report List</h1>
            <div class="btn-group btn-group-sm" role="group">
                <a href="#" class="btn btn-default" data-toggle="modal" data-target="#Detail_Search">Detail Search</a>
            </div>
        </div>
        <div class="panel-body pd">
            <div id="jsGridList"></div>
        </div>
    </div>

            <div class="modal fade" id="Detail_Search" tabindex="-1" role="dialog">
                <div class="modal-dialog" role="document">
                    <div class="modal-content">
                        <div class="modal-header">
                            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
                            <h4 class="modal-title">Detail Search</h4>
                        </div>
                        <div class="modal-body">
                            <div class="modal-table">
                                <table class="write">
                                    <colgroup>
                                        <col style="width: 22%" />
                                        <col />
                                    </colgroup>
                                    <tbody> 
                                        <tr>
                                            <th scope="row">Subject</th>
                                            <td>
                                                <input type="text" id="lb_Subject" class="form-control" />
                                            </td>
                                        </tr> 
                                        <tr>
                                            <th scope="row">Start Date</th>
                                            <td>
                                                <div class="row">
                                                    <div class="col-lg-6">
                                                        <div class="input-group date" data-date="">
                                                            <input id="lb_EvtStart" class="form-control" size="10" type="text" placeholder="Start Date" readonly="readonly" />
                                                            <span class="input-group-addon"><span onclick="fn_DelInput('lb_EvtStart')" class="fa fa-times"></span></span>
                                                            <span class="input-group-addon form_datehour"><span class="fa fa-calendar-o" id="hbtnEvtStart"></span></span>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="input-group date" data-date="">
                                                            <input id="lb_EvtEnd" class="form-control" size="10" type="text" placeholder="End Date" readonly="readonly" />
                                                            <span class="input-group-addon"><span onclick="fn_DelInput('lb_EvtEnd')" class="fa fa-times"></span></span>
                                                            <span class="input-group-addon form_datehour"><span class="fa fa-calendar-o" id="hbtnEvtEnd"></span></span>
                                                        </div>
                                                    </div>
                                                </div>
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                        <div class="modal-footer">
                            <button type="button" class="btn btn-lg btn-navy" data-dismiss="modal">Cancel</button>
                            <button type="button" onclick="detailSearch()" class="btn btn-lg btn-red">Search</button>
                        </div>
                    </div>
                </div>
            </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderBottom" Runat="Server">
        <input type="hidden" id="hddUserID" runat="server" />
    <script src="../../../Scripts/Pages/Report/MohwReportList.js"></script>
         <iframe id="iframeFileDown" width="0" height="0"></iframe>
</asp:Content>

