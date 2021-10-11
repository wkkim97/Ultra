<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Medical.master" AutoEventWireup="true" CodeFile="StudyList.aspx.cs" Inherits="Pages_Medical_Study_StudyList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHolderHead" Runat="Server">
    <script src="../../../Scripts/Pages/FormEvent.js"></script>
    <style>
        .jsgrid-cell {
            text-overflow: ellipsis;
            white-space: nowrap;
            overflow: hidden !important;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderForm" Runat="Server">
    	<div class="panel panel-topinfo">
		<div class="panel-heading">
			<h1 class="panel-title">Study List</h1>
			<div class="btn-group btn-group-sm" role="group">
				<a href="#" onclick="fn_NewStudyPage()" class="btn btn-default" >New</a>
			</div>
		</div>
		<div class="panel-body pd">
		 
		    <div id="jsGridList"></div>
		 
			
			<!-- jsgrid pager -->
			<div class="jsgrid-pager-container" style="display: block;">
	 
			</div><!--// jsgrid pager -->

		</div><!-- // .panel-body -->
	</div><!-- // .panel-topinfo -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderBottom" Runat="Server">
    <input type="hidden" id="hhdUserID" runat="server" /> <script src="../../../Scripts/Pages/Medical/StudyList.js"></script>
    <script src="../../../Scripts/JQuery/cookie.js"></script>
</asp:Content>

