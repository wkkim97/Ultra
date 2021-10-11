<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Event.master" AutoEventWireup="true" CodeFile="AdventisementBooth.aspx.cs" Inherits="Pages_Event_AdventisementBooth" EnableEventValidation="false" %>

<%@ Register Src="~/Common/Controls/HostSearcher.ascx" TagPrefix="uc1" TagName="HostSearcher" %>
<%@ Register Src="~/Common/Controls/HostSearcherModal.ascx" TagPrefix="uc1" TagName="HostSearcherModal" %>

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
                <th>Start Date</th>
                <td>
                    <div class="input-group date form_date form_Startdate" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtStartDate">
                        <input id="datStartDate" class="form-control" size="10" type="text" value="" readonly="readonly" />
                        <span class="input-group-addon"><span class="fa fa-times"></span></span>
                        <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                    </div>
                    <input type="hidden" id="dtStartDate" value="" />
                </td>
            </tr>
            <tr>
                <th>End Date</th>
                <td>
                    <div class="input-group date form_date form_Enddate" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtEndDate">
                        <input id="datEndDate" class="form-control" size="10" type="text" value="" readonly="readonly" />
                        <span class="input-group-addon"><span class="fa fa-times"></span></span>
                        <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                    </div>
                    <input type="hidden" id="dtEndDate" value="" />
                </td>
            </tr>
            <tr>
                <th>주최</th>
                <td>
                    <uc1:HostSearcher runat="server" ID="empSearcherDataOwner" />
                </td>
            </tr>
        </tbody>
    </table>
    <uc1:HostSearcherModal runat="server" ID="HostSearcherModal" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderEtc" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cpHolderBottom" Runat="Server">
    <script type="text/javascript" src="/ultra/Scripts/Pages/Event/AdventisementBooth.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/Common/HostSearcher.js"></script>
</asp:Content>

