<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Library_List.master" AutoEventWireup="true" CodeFile="CompletedList.aspx.cs" Inherits="Pages_Library_CompletedList" %>

<%@ MasterType VirtualPath="~/Master/Ultra_Library_List.master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="cpHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cpHolderTitle" Runat="Server">
   Complete
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderForm" runat="Server">
    <div id="divCompletedApproval"></div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderBottom" runat="Server">
    <script type="text/javascript" src="/ultra/Scripts/Pages/Library/CompletedList.js"></script>
</asp:Content>

