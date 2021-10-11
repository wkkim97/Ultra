<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Event.master" AutoEventWireup="true" CodeFile="CongressEvents.aspx.cs" Inherits="Pages_Event_CongressEvents" EnableEventValidation="false" %>

<%@ Register Src="~/Common/Controls/HostSearcher.ascx" TagPrefix="uc1" TagName="HostSearcher" %>
<%@ Register Src="~/Common/Controls/HostSearcherModal.ascx" TagPrefix="uc1" TagName="HostSearcherModal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHolderHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderForm" Runat="Server">
    <table class="table form-group-sm">
        <colgroup>
            <col width="105px" />
            <col width="*" />
        </colgroup>
        <thead>
            <tr>
                <th colspan="2">Event Information</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <th>Congress<br />Type</th>
                <td>
                    <select id="hddlCongressType" class="form-control">
                        <option value="LOS">Local Operational Sponsorship (국내개최/국내학술대회 운영지원)</option>
                        <option value="IOS">International Operational Sponsorship (국내개최/국제학술대회 운영지원)</option>
                        <option value="LPS">Local Participants Sponsorship (국내학술대회 참가자지원)</option>
                        <option value="IPS">International Participants Sponsorship (국제학술대회 참가자지원)</option>
                    </select>
                </td>
            </tr>
            <tr>
                <th>학술대회명</th>
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
                <th>Venue</th>
                <td>
                    <input type="text" id="txtVenue" class="form-control" />
					<label id="spanVenue" style="display:none;">* 국가와 도시를 반드시 입력해 주세요.</label>
                </td>
            </tr>
            <tr>
                <th>주최</th>
                <td>
                    <div id="hdivHostSearch"><uc1:HostSearcher runat="server" ID="empSearcherDataOwner" /></div>
                    <div id="hdivHostText" style="display:none;"><input type="text" id="htxtHost" class="form-control" /></div>
                </td>
            </tr>
            <tr>
                <th>Payment to</th>
                <td>
                    <div class="radio" style="padding:0px">
					    <label><input type="radio" id="radoKRPIA" name="radoPaymentTo" value="0" checked="checked" /> KRPIA</label>
					    <label><input type="radio" id="radoAcademy" name="radoPaymentTo" value="1" /> 해당학회</label>
                    </div>
                </td>
            </tr>
            <tr id="htrParticipantCount" style="display:none">
                <th>참가자지원수</th>
                <td><input id="txtParticipantCount" type="text" class="form-control number" /></td>
            </tr>
            <tr>
                <th>KRPIA<br />사전 신고 번호</th>
                <td>
					<input id="txtKRPIANum" type="text" class="form-control krpia_number" />
					<label>* 행사일기준, 사후 30일 이내 지원일자 / 실제지원금액 보고 요망</label>
                </td>
            </tr>
        </tbody>
    </table>
    <uc1:HostSearcherModal runat="server" ID="HostSearcherModal" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderEtc" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cpHolderBottom" Runat="Server">
    <script type="text/javascript" src="/ultra/Scripts/Pages/Event/CongressEvents.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/Common/HostSearcher.js"></script>
</asp:Content>

