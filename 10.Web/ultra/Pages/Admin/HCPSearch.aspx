<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Admin.master" AutoEventWireup="true" CodeFile="HCPSearch.aspx.cs" Inherits="Pages_Admin_HCPSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderContent" Runat="Server">
    
    <div class="panel panel-topinfo">
        <div class="panel-body">
            <div class="panel-heading">
                <h3 class="panel-title">Event List</h3>
                <div class="search-area col-lg-4" style="top:10px;">
                    <input type="search" class="form-control" id="searchText" placeholder="Search Event Key" />
                </div>
            </div>
            <div class="panel-body">
                <div class="box-panel">
                    <div id="jsGridHCPList"></div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderBottom" Runat="Server">
    <script type="text/javascript" src="/ultra/Scripts/Pages/Admin/HcpSearch.js"></script>
</asp:Content>

