<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Event.master" AutoEventWireup="true" CodeFile="ProductBriefing.aspx.cs" Inherits="Pages_Event_ProductBriefing" EnableEventValidation="false" %>

<%@ MasterType VirtualPath="~/Master/Ultra_Event.master" %>
<%@ Register Src="~/Common/Controls/CRMProductSearcher.ascx" TagPrefix="uc1" TagName="CRMProductSearcher" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHolderHead" runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderForm" runat="Server">
    <table class="table form-group-sm">
        <thead>
            <tr>
                <th colspan="2">Event Information</th>
            </tr>
        </thead>

        <tbody>
            <tr>
                <th>Subject</th>
                <td>
                    <input id="txtSubject" type="text" class="form-control" />
                </td>
            </tr>
            <tr>
                <th>Start Time</th>
                <td>
                    <div class="input-group date form-date-hour-min form_StartTime" data-date="" data-date-format="yyyy-mm-dd hh:ii:ss" data-link-field="dtStartTime">
                        <input id="datStartTime" class="form-control" size="16" type="text" value="" readonly />
                        <span class="input-group-addon"><span class="fa fa-times"></span></span>
                        <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                    </div>
                    <input type="hidden" id="dtStartTime" value="" />
                </td>
            </tr>
            <tr>
                <th>ProductName<br />
                    표준코드명칭</th>
                <td>
                    <uc1:CRMProductSearcher runat="server" ID="CRMProductSearcher" />
                </td>
            </tr>
        </tbody>
    </table>
    
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderBottom" runat="Server">
    <script type="text/javascript" src="/ultra/Scripts/Pages/Event/ProductBriefing.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/Common/CRMProductSearcher.js"></script>
</asp:Content>

