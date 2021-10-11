<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Admin.master" AutoEventWireup="true" CodeFile="MedicalStudy.aspx.cs" Inherits="Pages_Report_MOHW_MedicalStudy" %>

<%@ Register Src="~/Pages/Report/MOHW/Conditions.ascx" TagPrefix="uc1" TagName="Conditions" %>
<%@ Register Src="~/Pages/Report/MOHW/CreateReport.ascx" TagPrefix="uc1" TagName="CreateReport" %>
 
<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>  
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderContent" Runat="Server">

    <div class="panel panel-topinfo">
        <div class="panel-heading">
            <h1 class="panel-title fl" style="display: inline">임상시험지원</h1>
        </div>
        <div class="panel-body pd" >
            <uc1:Conditions runat="server" ID="Conditions" />
            <div class="btnset">
                <button type="button" class="btn btn-sm btn-navy fl" id="btnSearch">Search</button>
                <button type="button" class="btn btn-sm btn-danger fr" style="margin-left: 10px;" id="btnCreate"><i class="fa fa-floppy-o"></i>Create Report</button>
                <button type="button" id="btnExport" style="margin-left: 5px; display:none;" class="btn btn-sm btn-gray fr"><i class="fa fa-floppy-o"></i>Excel Export</button>
            </div>
            <div style="margin-top: 10px;">
                <div id="jsGridSourceList"></div>
            </div>
        </div>
    </div> 
    <uc1:CreateReport runat="server" ID="CreateReport" />
    <input type="hidden" id="hddUserID" runat="server" />
    <input type="hidden" id="hddUserName" runat="server" />
    <input type="hidden" id="hddGetDate" runat="server" />
    <input type="hidden" id="hddMohwType" runat="server" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderBottom" Runat="Server">
    <script src="../../../Scripts/Pages/Report/MedicalStudy.js"></script>
</asp:Content>

