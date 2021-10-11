<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Admin.master" AutoEventWireup="true" CodeFile="ConcurHistory.aspx.cs" Inherits="Pages_Admin_ConcurHistory" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderContent" Runat="Server">
    <div class="panel panel-topinfo" id="panelEdit">
        <div class="panel-heading">
            <div class="row row-sm">
                <table class="table-default">
                    <colgroup>
                        <col style="width: 200px" />
                        <col />
                        <col style="width: 100px" />
                        <col style="width: 200px" />
                        <col />
                    </colgroup>
                    <tr>
                        <th class="text-left">Event Key</th>
                        <th class="text-left">Subject</th>
                        <th class="text-left">Start Date</th>
                        <th class="text-left">Concur ID</th>
                        <th class="text-left">Category</th>
                    </tr>
                    <tr>
                        <td><label id="lblEventKey"></label></td>
                        <td><label id="lblSubject"></label></td>
                        <td><label id="lblStartDate"></label></td>
                        <td><label id="lblConcurID"></label></td>
                        <td>
                            <select id="selCategoryList" class="form-control">
                            </select>
                        </td>
                    </tr>
                </table>
                <input type="hidden" id="hhdProcessID" />
                <div class="btnset">
                    <button type="button" class="btn btn-sm btn-red fr" id="btnSave"><i class="fa fa-floppy-o"></i>Save</button>
                </div>
            </div>
        </div>
    </div>
            
    <div class="panel panel-topinfo">
        <div class="panel-body">
            <div class="panel-heading">
                <h3 class="panel-title">Concur List</h3>
                <div class="search-area col-lg-4" style="top:10px;">
                    <input type="search" class="form-control" id="searchText" placeholder="Search Event Key" value="입력" />
                </div>
            </div>
            <div class="panel-body">
                <div class="box-panel">
                    <div id="jsGridConcurList"></div>
                </div>
            </div>
        </div>
    </div>

    <div class="modal fade concur-history-modal" id="divConcurHistoryModal" tabindex="-1" role="dialog">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
                </div>
                <div class="panel-heading">
                    <h3 class="panel-title">Concur History</h3> - <label id="lblSelConcurId"></label>
                </div>
                <div class="modal-body">
                    <div id="jsGridConcurHistory"></div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-lg btn-navy" data-dismiss="modal">Ok</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderBottom" Runat="Server">
    <script type="text/javascript" src="/ultra/Scripts/Pages/Admin/ConcurHistory.js"></script>
</asp:Content>

