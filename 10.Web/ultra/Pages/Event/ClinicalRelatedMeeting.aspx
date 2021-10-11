<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Event.master" AutoEventWireup="true" CodeFile="ClinicalRelatedMeeting.aspx.cs" Inherits="Pages_Event_ClinicalRelatedMeeting" EnableEventValidation="false" %>

<%@ Register Src="~/Common/Controls/MedicalSearcher.ascx" TagPrefix="uc1" TagName="MedicalSearcher" %>
<%@ Register Src="~/Common/Controls/MedicalSearcherModal.ascx" TagPrefix="uc1" TagName="MedicalSearchModal" %>

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
                <th>Category</th>
                <td>
                    <select id="hddlCategory" class="form-control">
                        <option value="MV">Monitoring Visit</option>
                        <option value="IM">Investigator Meeting</option>
                        <option value="KM">Initiation Meeting</option>
                    </select>
                </td>
            </tr>
            <tr>
                <th>Impact No.</th>
                <td>
                    <uc1:MedicalSearcher runat="server" ID="MedicalSearcher" />
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
                <th>Venus & Address</th>
                <td>
                    <textarea  rows="3" id="txtAddress" class="form-control" style="width:100%" ></textarea>
                    <p style="padding-top:10px;font-size:0.9em">
                        행사장소와 식사장소가 상이한 경우, 두 장소 모두 기입요망<br />
(예시: 행사장소 : BKL 서울시 동작구 보라매로5길 23 / 식사장소 : ABC 서울시 동작구 보라매로100길 1000)
                    </p>
                    <%--<textarea id="txtAddress" class="example" rows="1" style="width: 100%"></textarea></td>--%>
            </tr>
            
            <tr>
                <th colspan="2" style="height: 36px;">Venue selection reason</th>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="checkbox">
                        <label>
                            <input class="chk-venue-selection-reason" type="checkbox" checked="checked" value="1" />이벤트 적합한 비즈니스 장소임</label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input class="chk-venue-selection-reason" type="checkbox" checked="checked" value="2" />대다수 참석자의 근무지역 (시,군)</label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input class="chk-venue-selection-reason" type="checkbox" checked="checked" value="3" />온천,해수욕장,골프,카지노,스키,워터파크가 없는 곳</label>
                    </div>
                    <div>
                        <strong>Reason</strong><br />
                        <input id="txtReason" type="text" class="form-control" />
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
    <uc1:MedicalSearchModal runat="server" ID="MedicalSearchModal" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderEtc" Runat="Server">
	<input type="hidden" id="hhdUserID" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cpHolderBottom" Runat="Server">
    <script type="text/javascript" src="/ultra/Scripts/Pages/Event/ClinicalRelatedMeeting.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/Common/MedicalSearcher.js"></script>
</asp:Content>

