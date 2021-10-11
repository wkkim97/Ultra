<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Event.master" AutoEventWireup="true" CodeFile="ScientificExchangedMeeting.aspx.cs" Inherits="Pages_Event_ScientificExchangedMeeting" EnableEventValidation="false" %>

<%@ Register Src="~/Common/Controls/CRMProductSearcher.ascx" TagPrefix="uc1" TagName="CRMProductSearcher" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cpHolderHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderForm" Runat="Server">
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
                        <input id="datStartTime" class="form-control" size="16" type="text" value="" readonly="readonly" />
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
                    <%--<textarea id="taProduct" class="example" rows="1" style="width: 100%"></textarea>--%>
                    <uc1:CRMProductSearcher runat="server" ID="CRMProductSearcher" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderEtc" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cpHolderBottom" Runat="Server">
    <script type="text/javascript" src="/ultra/Scripts/Pages/Event/ScientificExchangedMeeting.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/Common/CRMProductSearcher.js"></script>
</asp:Content>

