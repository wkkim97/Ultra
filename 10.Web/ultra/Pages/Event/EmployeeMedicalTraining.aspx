<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Event.master" AutoEventWireup="true" CodeFile="EmployeeMedicalTraining.aspx.cs" Inherits="Pages_Event_EmployeeMedicalTraining" EnableEventValidation="false" %>

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
                    <div class="input-group date form-date-hour-min form_StartTime" data-date="" data-date-format="yyyy-mm-dd hh:ii:ss" data-link-field="dtStartTime">
                        <input id="datStartTime" class="form-control" size="10" type="text" value="" readonly="readonly" />
                        <span class="input-group-addon"><span class="fa fa-times"></span></span>
                        <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                    </div>
                    <input type="hidden" id="dtStartTime" value="" />
                </td>
            </tr>
            <tr>
                <th>End Date</th>
                <td>
                    <div class="input-group date form-date-hour-min form_EndTime" data-date="" data-date-format="yyyy-mm-dd hh:ii:ss" data-link-field="dtEndTime">
                        <input id="datEndTime" class="form-control" size="10" type="text" value="" readonly="readonly" />
                        <span class="input-group-addon"><span class="fa fa-times"></span></span>
                        <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                    </div>
                    <input type="hidden" id="dtEndTime" value="" />
                </td>
            </tr>
            <tr>
                <th>Venue & Address</th>
                <td>
                    <textarea  rows="3" id="txtAddress" class="form-control"  ></textarea>
                    <p style="padding-top:10px;font-size:0.9em">
                        행사장소와 식사장소가 상이한 경우, 두 장소 모두 기입요망<br />
(예시: 행사장소 : BKL 서울시 동작구 보라매로5길 23 / 식사장소 : ABC 서울시 동작구 보라매로100길 1000)
                    </p>
                </td>
            </tr>
            <tr>
                <th colspan="2" style="height: 36px;">Venue selection reason</th>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="checkbox">
                        <label>
                            <input class="chk-venue-selection-reason" type="checkbox" checked="" value="1" />소위 6성급 이상 호텔이 아닙니다.</label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input class="chk-venue-selection-reason" type="checkbox" checked="" value="2" />
                            대다수의 참석자들이 근무하는 지역입니다.</label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input class="chk-venue-selection-reason" type="checkbox" checked="" value="3" />
                            관광/오락/유흥등의 주된 목적으로 하는 장소가 아닙니다.</label>
                    </div>
                    <div>
                        <strong>Reason</strong><br />
                        <input id="txtReason" type="text" class="form-control" />
                    </div>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderEtc" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cpHolderBottom" Runat="Server">
    <script type="text/javascript" src="/ultra/Scripts/Pages/Event/EmployeeMedicalTraining.js"></script>
</asp:Content>

