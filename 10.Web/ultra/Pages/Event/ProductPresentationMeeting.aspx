<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Event.master" AutoEventWireup="true" CodeFile="ProductPresentationMeeting.aspx.cs" Inherits="Pages_Event_ProductPresentationMeeting" %>

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
                <th>Start Time<i class="fa fa-adjust changeButton" id="old_form_StartTime"   ></i></th>
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
                <th>End Time</th>
                <td>
                    <div class="input-group date form-date-hour-min form_EndTime" data-date="" data-date-format="yyyy-mm-dd hh:ii:ss" data-link-field="dtStartTime">
                        <input id="datEndTime" class="form-control" size="16" type="text" value="" readonly />
                        <span class="input-group-addon"><span class="fa fa-times"></span></span>
                        <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                    </div>
                    <input type="hidden" id="dtEndTime" value="" />
                </td>
            </tr>
            <tr>
                <th>ProductName<br />
                    표준코드명칭</th>
                <td>
                    <%--<textarea id="taCRMProduct" class="example" rows="1" style="width: 100%"></textarea></td>--%>
                    <uc1:CRMProductSearcher runat="server" ID="CRMProductSearcher" />
            </tr>
            <tr>
                <th >Venue & Address<i class="fa fa-adjust changeButton" id="old_txtAddress"   ></i></th>
                <td>
                    <textarea  rows="3" id="txtAddress" class="form-control" ></textarea>
                    <p style="padding-top:10px;font-size:0.9em">
                        행사장소와 식사장소가 상이한 경우, 두 장소 모두 기입요망<br />
(예시: 행사장소 : BKL 서울시 동작구 보라매로5길 23 / 식사장소 : ABC 서울시 동작구 보라매로100길 1000)
                    </p>
                </td>
            </tr>
            <tr>
                <th>KRPIA 신고번호<i class="fa fa-adjust changeButton" id="old_txtKRPIANumber"   ></th>
                <td>
                    <input id="txtKRPIANumber" type="text" class="form-control krpia_number" />
                    <br />
                    숙박이 필요한 경우 60일 이전, 없는 경우 7일 이전
                </td>
            </tr>
            <tr>
                <th colspan="2" style="height: 36px;">Venue selection reason</th>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="checkbox">
                        <label>
                            <input class="chk-venue-selection-reason" type="checkbox" checked value="1" />소위 6성급 이상 호텔이 아닙니다.</label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input class="chk-venue-selection-reason" type="checkbox" checked value="2" />
                            대다수의 참석자들이 근무하는 지역입니다.</label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input class="chk-venue-selection-reason" type="checkbox" checked value="3" />
                            관광/오락/유흥등의 주된 목적으로 하는 장소가 아닙니다.</label>
                    </div>
                    <div>
                        <strong>Reason</strong><br />
                        <input id="txtReason" type="text" class="form-control" />
                    </div>
                </td>
            </tr>
            <tr>
                <th colspan="2" style="height: 36px;">Purpose/Objective</th>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="checkbox">
                        <label>
                            <input class="chk-purpose-objective" type="checkbox" value="1" />제품과 관련된 과학 및 의학적 정보 전달</label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input class="chk-purpose-objective" type="checkbox" value="2" />
                            제품의 부작용 및 안전성 관련된 정보 전달</label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input class="chk-purpose-objective" type="checkbox" value="3" />
                            제품의 보험심사기준 제공</label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input class="chk-purpose-objective" type="checkbox" value="4" />
                            제품과 관련된 최신의 임상 정보 제공</label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input class="chk-purpose-objective" type="checkbox" value="5" />
                            제품 관련 질병 치료제의 최신 정보 업데이트 및 관련 임상 저널 소개</label>
                    </div>
                    <!-- version 1.0.7 : Purpose 추가 (Web/Online/Virtual event)-INC0002492193 -->
					<div class="checkbox">
                        <label>
                            <input class="chk-purpose-objective" type="checkbox" value="6" />
                            Web/Online/Virtual event</label>
                    </div>

                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderEtc" runat="Server">
    <div id="divEstimated" class="panel panel-dashboard">
        <div class="panel-heading">
            <h3 class="panel-title">Participants(Estimated)</h3>
        </div>
        <div class="panel-body">
            <table class="table table-striped">
                <colgroup>
                    <col style="width: 16.66%;" />
                    <col style="width: 16.66%;" />
                    <col style="width: 16.66%;" />
                    <col />
                    <col style="width: 16.66%;" />
                    <col style="width: 16.66%;" />
                </colgroup>
                <thead>
                    <tr>
                        <th scope="col" style="text-align: center;">Go</th>
                        <th scope="col" style="text-align: center;">Public</th>
                        <th scope="col" style="text-align: center;">Private</th>
                        <th scope="col" style="text-align: center;">Other</th>
                        <th scope="col" style="text-align: center;">Foreigner</th>
                        <th scope="col" style="text-align: center;">Bayer</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>
                            <input type="number" id="numEstimatedGo" class="form-control number" /></td>
                        <td>
                            <input type="number" id="numEstimatedPublic" class="form-control number" /></td>
                        <td>
                            <input type="number" id="numEstimatedPrivate" class="form-control number" /></td>
                        <td>
                            <input type="number" id="numEstimatedOther" class="form-control number" /></td>
                        <td>
                            <input type="number" id="numEstimatedForeigner" class="form-control number" /></td>
                        <td>
                            <input type="number" id="numEstimatedBayer" class="form-control number" /></td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cpHolderBottom" runat="Server">
    <script type="text/javascript" src="/ultra/Scripts/Pages/Event/ProductPresentationMeeting.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/Common/CRMProductSearcher.js"></script>
</asp:Content>

