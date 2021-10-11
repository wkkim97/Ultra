<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Medical.master" AutoEventWireup="true" CodeFile="StudyDetail.aspx.cs" Inherits="Pages_Medical_Study_StudyDetail" %>

<%@ Register Src="~/Pages/Medical/Controls/Study.ascx" TagPrefix="uc1" TagName="Study" %>
<%@ Register Src="~/Pages/Medical/Controls/ContractList.ascx" TagPrefix="uc1" TagName="ContractList" %>
<%@ Register Src="~/Pages/Medical/Controls/StudyLog.ascx" TagPrefix="uc1" TagName="StudyLog" %>
<%@ Register Src="~/Pages/Medical/Controls/ContractDetail.ascx" TagPrefix="uc1" TagName="ContractDetail" %>
 


<asp:Content ID="Content1" ContentPlaceHolderID="cpHolderHead" Runat="Server">

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderForm" Runat="Server">
    <ul class="nav nav-tabs nav-tabs-fixcenter" id="ulStudy">
	    <li role="presentation" class="active"><a id="haStudy" href="#hdivDetailArea" >Study</a></li>
	    <li role="presentation"><a id="haContract" href="#hdivContractArea" >Contract<br/>Information</a></li>  
	    <li role="presentation" style="display:none;"><a id="haLog" href="#hdivStudyLogArea" >Log</a></li>
    </ul><!-- // .nav-tabs-fixcenter -->
  
	<div class="box-panel" id="tabContantArea">
		<div class="tab-cont-area row row-md" id="hdivDetailArea">
            <uc1:Study runat="server" ID="Study" />
		</div> 
        <div class="tab-cont-area"  id="hdivContractArea"  style="display:none;">
            <uc1:ContractList runat="server" ID="ContractList" />
		</div>  
        <div class="tab-cont-area"  id="hdivStudyLogArea"  style="display:none;">
            <uc1:StudyLog runat="server" ID="StudyLog" />
		</div> 
        <div class="tab-cont-area"  id="hdivContractDetailArea"  style="display:none;">
            <uc1:ContractDetail runat="server" ID="ContractDetail" />
		</div>  
	</div><!-- // #tab_expense -->
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderBottom" Runat="Server">
    <input type="hidden" id="hhdMedicalIdx" runat="server" />
    <input type="hidden" id="hhdUserName" runat="server" />
    <input type="hidden" id="hhdUserID" runat="server" />
    <input type="hidden" id="hhdAuthorID" runat="server" />
    <script src="../../../Scripts/Pages/Medical/StudyDetail.js"></script> 

</asp:Content>

