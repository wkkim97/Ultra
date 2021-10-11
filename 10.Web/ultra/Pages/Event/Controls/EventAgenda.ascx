<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventAgenda.ascx.cs" Inherits="Pages_Event_Controls_EventAgenda" %>
<style type="text/css">
    .agenda-title h2:hover {
        cursor: pointer;
        text-decoration: underline;
    }
</style>

<div class="box-panel" id="divInputAgenda">
    <div class="tab-cont-area row row-md">
        <div class="write-area">
            <div class="box-panel">
                <form action="#">
                    <table class="table-default">
                        <colgroup>
                            <col style="width: 65px" />
                            <col />
                            <col style="width: 80px" />
                            <col style="width: 60px" />
                        </colgroup>
                        <tr>
                            <th scope="row">Day</th>
                            <td colspan="3">
                                <select id="optAgendaDay" class="form-control">
                                    <option value="1">Day1</option>
                                    <option value="2">Day2</option>
                                    <option value="3">Day3</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <th scope="row">Time</th>
                            <td colspan="3">
                                <div class="form-inline">
                                    <div class="form-group col-lg-12" style="padding-left: 0px; padding-right: 0px;">
                                        <select id="optAgendaStartHour" class="form-control" style="width: 49%">
                                            <option value="00">00</option>
                                            <option value="01">01</option>
                                            <option value="02">02</option>
                                            <option value="03">03</option>
                                            <option value="04">04</option>
                                            <option value="05">05</option>
                                            <option value="06">06</option>
                                            <option value="07">07</option>
                                            <option value="08">08</option>
                                            <option value="09" selected>09</option>
                                            <option value="10">10</option>
                                            <option value="11">11</option>
                                            <option value="12">12</option>
                                            <option value="13">13</option>
                                            <option value="14">14</option>
                                            <option value="15">15</option>
                                            <option value="16">16</option>
                                            <option value="17">17</option>
                                            <option value="18">18</option>
                                            <option value="19">19</option>
                                            <option value="20">20</option>
                                            <option value="21">21</option>
                                            <option value="22">22</option>
                                            <option value="23">23</option>
                                        </select>
                                        <select id="optAgendaStartMinute" class="form-control" style="width: 49%">
                                            <option value="00" selected>00</option>
                                            <option value="10">10</option>
                                            <option value="20">20</option>
                                            <option value="30">30</option>
                                            <option value="40">40</option>
                                            <option value="50">50</option>
                                        </select>
                                    </div>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th scope="row">Duration</th>
                            <td colspan="3">
                                <div class="form-inline">
                                    <input id="numAgendaDuration" type="text" class="form-control number" style="width: 60%"><span style="padding-left: 10px">Minute</span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th scope="row">Subject</th>
                            <td colspan="3">
                                <input id="txtAgendaSubject" type="text" class="form-control"></td>
                        </tr>
                    </table>
                    <div class="btnset">
                        <button id="btnDeleteAgenda" type="button" class="btn btn-sm btn-navy fl"><i class="fa fa-trash-o"></i>Delete</button>
                        <button id="btnSaveReset" type="button" class="btn btn-sm btn-gray fl" style="margin-left: 3px;"><i class="fa fa-close"></i>Reset</button>
                        <button id="btnSaveAgenda" type="button" class="btn btn-sm btn-red fr"><i class="fa fa-floppy-o"></i>Save</button>
                    </div>
                </form>
                <button type="button" class="btn-panel-close"><i class="fa fa-close"><span class="tts">Close</span></i></button>
            </div>
            <!-- // .box-panel -->
        </div>
        <!-- // .write-area -->
        <div class="list-area">
            <button type="button" class="btn-panel-open"><i class="fa fa-plus-square"><span class="tts">Open</span></i></button>
            <div class="row">
                <div class="label-area col-xs-6">
                    <span class="label bg-green"><i class="fa fa-user-circle"></i>패널</span>
                    <span class="label bg-blue"><i class="fa fa-user-circle"></i>좌장</span>
                    <span class="label bg-yellow"><i class="fa fa-user-circle"></i>강연</span>
                    <span class="label bg-black"><i class="fa fa-user-circle"></i>자문</span>
                </div>
                <p class="col-xs-6 total">Total <strong id="totalAgendaRole">0</strong></p>
            </div>
            <div id="divAgendaList">
            </div>
        </div>
        <!-- // .list-area  -->
    </div>
    <!-- // .row-md -->
</div>
<!-- // .box-panel -->


<div class="modal fade" id="role_information" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
                <h4 class="modal-title">Role Information</h4>
            </div>
            <div style="text-align: right; padding-right:30px;">
                <a href="https://bayergroup.sharepoint.com/sites/021843/ultra/ulink/UlTra_FMV_Appendix%203%20%EC%86%8C%EC%86%8D%EA%B8%B0%EA%B4%80%EC%97%90%20%EB%94%B0%EB%A5%B8%20%EA%B0%95%EC%97%B0%EC%9E%90%EB%AC%B8%20%EB%B9%84%EC%9A%A9.2018.01.02.xlsx?Web=1" target="_blank">FMV Guide</a> / 
                <a href="https://bayergroup.sharepoint.com/sites/021245/ST/Shared%20Documents/Forms/Standard%20Template/docsethomepage.aspx?ID=154&FolderCTID=0x0120D5200051AA9421C5E412458FFE902E3DF38CCF00D427CF1D95453C448779192BFD2339B5&List=e28acb58-e938-4f69-a1ba-6bdebc538cd9&RootFolder=%2Fsites%2F021245%2FST%2FShared%20Documents%2FHCP%20002&RecSrc=%2Fsites%2F021245%2FST%2FShared%20Documents%2FHCP%20002" target="_blank">Service Agreement </a>
            </div>
            <div class="modal-body" style="padding-top: 1px;">
                <div class="modal-table">
                    <table class="write">
                        <colgroup>
                            <col style="width: 25%" />
                            <col />
                        </colgroup>
                        <tbody>
                            <tr>
                                <th scope="row">Role Type</th>
                                <td>
                                    <div class="radio">
                                        <label>
                                            <input id="rdoRoleTypeLecture" type="radio" name="lb_role_type" runat="server" value="Lecture" />
                                            강연</label>
                                        <label>
                                            <input id="rdoRoleTypeConsulting" type="radio" name="lb_role_type" runat="server" value="Consulting" />
                                            자문</label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th scope="row">Role</th>
                                <td>
                                    <select id="optAgendaRole" class="form-control" style="width: 100%;">
                                        <option>선택해주세요</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <th scope="row">Name</th>
                                <td>
                                    <select id="optAgendaRoleParticipant" class="form-control" style="width: 100%;">
                                        <option>선택해주세요</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <th scope="row">Criteria <a href="#tooltip_criteria" class="btn-tooltip"><i class="fa fa-question-circle"></i></a>
                                    <div id="tooltip_criteria" class="layer-tooltip">
                                        <div>
                                            <button type="button" id="new_criteria_btn" class="btn btn-default"  >2018-08-19 이후</button>
                                            <button type="button" id="old_criteria_btn" class="btn btn-default"  >2018-08-19 이전</button>
                                        </div>
                                        
                                        <div id="old_criteria" style="display:none">
                                            <dl>
                                                <dt>Criteria(2018-08-19이전)</dt>
                                                <dd>
                                                    <ul>
                                                        <li><span class="num">1</span> Teaching Hospital의 교육 의사</li>
                                                        <li><span class="num">2</span> 학회의 학술위원</li>
                                                        <li><span class="num">3</span> 다수의 임상경험자</li>
                                                        <li><span class="num">4</span> 주제와 관련된 TA에서 8년 이상 경험을 가진 전문의</li>
                                                        <li><font color="#325e79">* 상기의 Criteria에 최소 2가지 이상 적용되어야 하며, 동시에 각 강연/자문 주제와 연관된 의과학적 경험과 지식을 가진 보건의료전문가여야 함.</font></li>    
                                                    </ul>
                                                </dd>
                                            </dl>                                        
                                        </div>
                                        <div id="new_criteria">
                                            <dl>
                                                <dt>Criteria(2018-08-19이후)</dt>
                                                <dd>
                                                    <ul>
                                                        <li><span class="num">1</span>대학병원의 조교수 또는 수련병원의 과장 이상 </li>
                                                        <li><span class="num">2</span>학회의 임원/위원 경험자이거나 연구회 임원 경험자 </li>
                                                        <li><span class="num">3</span>제품과 관련된 충분한 임상 진료 경험 </li>
                                                        <li><span class="num">4</span>주제와 관련된 진료분야에서 전문의 취득 후 5년 이상 </li>
                                                        <li><font color="#325e79">* 상기의 Criteria에 최소 2가지 이상 적용되어야 하며, 동시에 각 강연/자문 주제와 연관된 의과학적 경험과 지식을 가진 보건의료전문가여야 함.</font></li>    
                                                    </ul>
                                                </dd>
                                            </dl>                                        
                                        </div>
                                        <button type="button" class="close"><i class="fa fa-times" aria-hidden="true"></i></button>
                                    </div>
                                </th>
                                <td>
                                    <div id="divCriteria" class="checkbox">
                                        <label>
                                            <input id="chkCriteria1" type="checkbox" />
                                            1</label>
                                        <label>
                                            <input id="chkCriteria2" type="checkbox" />
                                            2</label>
                                        <label>
                                            <input id="chkCriteria3" type="checkbox" />
                                            3</label>
                                        <label>
                                            <input id="chkCriteria4" type="checkbox" />
                                            4</label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th scope="row">Reason</th>
                                <td>
                                    <textarea id="txtAgendaRoleReason" rows="3" class="form-control"></textarea></td>
                            </tr>
                            <tr>
                                <th scope="row">Accumulate <a href="#tooltip_accumulate" class="btn-tooltip"><i class="fa fa-plus-circle"></i></a>
                                    <div id="tooltip_accumulate" class="layer-tooltip">
                                        <dl style="width:450px;">
                                            <dt>Accumulate List</dt>
                                            <dd>
                                                <table class="table">
                                                    <colgroup>
                                                        <col />
                                                        <col width="100px" />
                                                        <col width="80px" />
                                                    </colgroup>
                                                    <thead>
                                                        <tr>
                                                            <th>Subject</th>
                                                            <th>StartDate</th>
                                                            <th>Amount</th>
                                                        </tr>
                                                    </thead>
                                                    <tbody id="tbdAccumulateList"></tbody>
                                                </table>
                                            </dd>
                                        </dl>
                                        <button type="button" class="close"><i class="fa fa-times" aria-hidden="true"></i></button>
                                    </div>
                                </th>
                                <td id="tdAgendaRoleAccumulate" class="text-right" data-total-amount="0"><strong class="colr-point">0</strong></td>
                            </tr>
                            <tr>
                                <th scope="row">Amount<br />(세전금액)</th>
                                <td>
                                    <input id="numAgendaRoleAmount" type="text" class="form-control number" style="width: 100%;" /></td>
                            </tr>
                            <tr>
                                <th scope="row">Material Code</th>
                                <td>
                                    <!--//version 1.0.5 Material Code 입력 방법 변경-->
                                    <div class="radio" style="margin:0px;padding: 0px" id="material_flag">                        
                                        <label>
                                            <input name="radmaterialcode" id="radmaterialcodeY" type="radio" value="Yes"  />지금입력</label>
                                        <label>
                                            <input name="radmaterialcode" id="radmaterialcodeN" type="radio" value="No" checked />추후입력</label>                        
                                    </div>

                                    <input id="txtAgendaRoleMaterialCode" type="text" class="form-control" style="display:none;width: 100%;" />
                                    <p><b>자문의 경우</b> "자문 시 결과물 첨부" 라고 기재</p>
                                </td>
                            </tr>
                        </tbody>
                    </table>

                    <div id="divAttachFiles_AgendaRole" class="attach attach-area" data-attachment-type="AgendaRole" style="padding-bottom: 10px; padding-top: 3px;">
                        <div id="progress" class="progress" style="height: 7px !important; visibility: hidden; margin-bottom: 0px;">
                            <div class="progress-bar progress-bar-success">
                            </div>
                        </div>
                        <span class="btn btn-sm btn-navy fileinput-button">
                            <i class="fa fa-paperclip"></i>
                            <span>Attachment</span>
                            <!-- The file input field used as target for the file upload widget -->
                            <input class="fileupload" id="fileupload" type="file" name="files" />
                        </span>
                        <!-- The global progress bar -->
                        <!-- The container for the uploaded files -->
                        <div id="files" class="files">
                            <ul class="attach-list files"></ul>
                        </div>
                    </div>
                </div>
            </div>
            <div class="modal-footer">
                <button id="btnDeleteAgendaRole" type="button" class="btn btn-lg btn-navy fl" data-dismiss="modal">Delete</button>
                <button id="btnSaveAgendaRole" type="button" class="btn btn-lg btn-red fr">Save</button>
                <button id="btnPrintYourDoces" type="button" class="btn btn-lg btn-navy fr">Your-Doces Cover</button>
            </div>
        </div>
    </div>
</div>
<!-- // #role_information -->

<input type="hidden" id="hddAgendaIDX" value="0" />
<!-- Agenda 수정/삭제에 사용할 Agenda IDX -->
<input type="hidden" id="hddModalAgendaIDX" value="0" />
<!-- Role Information에서 사용할 Agenda IDX, 수정/삭제시의 Agenda IDX와 Role관리시의 Agenda IDX를 구분 -->
<input type="hidden" id="hddAgendaRoleIDX" value="0" />