<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Event.master" AutoEventWireup="true" CodeFile="ConsultingMeeting.aspx.cs" Inherits="Pages_Event_ConsultingMeeting" %>

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
            <!-- version 1.0.6 ABM 문서에 Category  추가 start -->
             <tr>
                <th>Event Type</th>
                <td>
                    <div class="radio" style="padding: 0px">                        
                        <label>
                            <input name="rdoType" type="radio" value="ABM"  />ABM(그룹 자문)</label>
                        <label>
                            <input name="rdoType" type="radio" value="Consulting" />Consulting(개별 자문, 1:1 Consulting Meeting)</label>                        
                    </div>
                </td>
            </tr>
            <!-- version 1.0.6 ABM 문서에 Category  추가 end-->
            <tr>
                <th>Start Time</th>
                <td>
                    <div class="input-group date form-date-hour-min form_StartTime" data-date="" data-date-format="yyyy-mm-dd hh:ii:ss" data-link-field="dtStartTime">
                        <input id="datStartTime" class="form-control" size="16" type="text" value="" readonly="true" />
                        <span class="input-group-addon"><span class="fa fa-times"></span></span>
                        <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                    </div>
                    <input type="hidden" id="dtStartTime" value="" />
                </td>
            </tr>
            <tr>
                <th>End Time</th>
                <td>
                    <div class="input-group date form-date-hour-min form_EndTime" data-date="" data-date-format="yyyy-mm-dd hh:ii:ss" data-link-field="dtEndTime">
                        <input id="datEndTime" class="form-control" size="16" type="text" value="" readonly="true" />
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
                            <input class="chk-venue-selection-reason" type="checkbox" checked="" value="2" />대다수의 참석자들이 근무하는 지역입니다.</label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input class="chk-venue-selection-reason" type="checkbox" checked="" value="3" />관광/오락/유흥등의 주된 목적으로 하는 장소가 아닙니다.</label>
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
                            <input class="chk-purpose-objective" type="checkbox" value="1" />바이엘 코리아 의약품 또는 의료기기의 치료분야와 관련된 전임상 및 임상 연구</label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input class="chk-purpose-objective" type="checkbox" value="2" />신제품(출시 2년 이내)에 대한 소개 프로그램 및 프로젝트 개발</label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input class="chk-purpose-objective" type="checkbox" value="3" />바이엘 코리아 직원을 위한 의약학/과학적 교육 프로그램/자료 개발</label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input class="chk-purpose-objective" type="checkbox" value="4" />바이엘 코리아 의약품 또는 의료기기와 관련된 의약학/과학적/교육적 출판물의 개발</label>
                    </div>
                    <div class="checkbox">
                        <label>
                            <input class="chk-purpose-objective" type="checkbox" value="5" />출시 전 제품 또는 판매중인 의약품 또는 의료기기의 새로운 적응 증 및 이와 관련된 경제성 평가 연구</label>
                    </div>

                </td>
            </tr>
            <tr>
                <th colspan="2" style="height: 36px;">Compliance Check</th>
            </tr>
            <tr>
                <td colspan="2">

                    <div class="row" style="padding:10px 10px 0px 10px"  >
                        <label style="font-weight:100">                            
                            최근 6개월 이내에 <strong>동일한 주제</strong>로 자문이 진행된 바 있습니까?
                        </label>
                    </div>
                    <div class="radio" style="padding: 0px">                        
                        <label>
                            <input name="rdocompliance1" type="radio" value="Yes"  />Yes</label>
                        <label>
                            <input name="rdocompliance1" type="radio" value="No" />No</label>                        
                    </div>
                    <div id="compliance_1_reason" style="display:none">
                        <strong>Reason(Yes 인 경우 사유 작성)</strong><br />
                        <input id="txtReasoncompliance1" type="text" class="form-control" />
                    </div>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <div class="row" style="padding:10px 10px 0px 10px"  >
                        <label style="font-weight:100">                                 
                            최근 6개월 이내에 <strong>동일한 자문자</strong>에게 자문 미팅을 진행한 적이 있습니까?
                        </label>
                    </div>
                    <div class="radio" style="padding: 0px">                        
                        <label>
                            <input name="rdocompliance2" type="radio" value="Yes"  />Yes</label>
                        <label>
                            <input name="rdocompliance2" type="radio" value="No" />No</label>                        
                    </div>
                    <div id="compliance_2_reason" style="display:none">
                        <strong>Reason(Yes 인 경우 사유 작성)<br />
                        <input id="txtReasoncompliance2" type="text" class="form-control" />
                    </div>

                </td>
            </tr>
            
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderEtc" Runat="Server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cpHolderBottom" Runat="Server">
    <script type="text/javascript" src="/ultra/Scripts/Pages/Event/ConsultingMeeting.js"></script>
</asp:Content>

