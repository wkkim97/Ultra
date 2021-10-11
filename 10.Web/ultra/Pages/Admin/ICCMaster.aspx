<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Admin.master" AutoEventWireup="true" CodeFile="ICCMaster.aspx.cs" Inherits="Pages_Admin_ICCMaster" %>

<%@ Register Src="~/Common/Controls/HCPSeacherModal.ascx" TagPrefix="uc1" TagName="HCPSeacherModal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <link rel="stylesheet" href="/ultra/Styles/Bootstrap/css/bootstrap-datetimepicker.css" />
    <link rel="stylesheet" href="/ultra/Styles/Css/jquery.fileupload.css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderContent" Runat="Server">
    
    <div class="panel panel-topinfo">
        <div class="panel-body">
            <div id="hdivlist">
                <div class="panel-heading">
                    <h3 class="panel-title">ICC Master List</h3>
                
                    <div class="btnset">
                        <button type="button" class="btn btn-sm btn-red fr" id="hbtnAdd"><i class="fa fa-floppy-o"></i>Add</button>
                    </div>
                </div>
                <div class="panel-body">
                    <div class="box-panel">
                        <div id="jsGridICCList"></div>
                    </div>
                </div>
            </div>
            <div id="hdivadd" style="display:none;">
                <div class="panel-heading">
                    <h3 class="panel-title">Add ICC Master</h3>
                </div>
                <div class="panel-body">
                    <div class="box-panel">
                        <table class="table">
                            <colgroup>
                                <col style="width: 180px" />
                                <col />
                                <col style="width: 180px" />
                                <col />
                            </colgroup>
                            <tr>
                                <th class="text-left">Type</th>
                                <td colspan="3">
                                    <select id="optType" class="form-control">
                                        <option value="Lecture">강연</option>
                                        <option value="Consulting">자문</option>
                                    </select>
                                </td>
                            </tr>
                            <tr>
                                <th class="text-left">해당연도</th>
                                <td>
                                    <input type="text" class="form-control numberOnly" id="htxtYear" maxlength="4" />
                                </td>
                                <th class="text-left">일자</th>
                                <td>
                                    <div id="hdivLectureDate">
                                        <div class="input-group date form_datetime" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtDateTime">
                                            <input id="datDateTime" class="form-control" size="10" type="text" value="" readonly="readonly" />
                                            <span class="input-group-addon"><span class="fa fa-times"></span></span>
                                            <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                        </div>
                                        <input type="hidden" id="dtDateTime" value="" />
                                    </div>
                                    <div id="hdivConsultingDate" style="display:none;">
                                        <div class="row row-md">
                                            <div class="col-md-6">
                                                <div class="input-group date form_date form_Startdate" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtStartDate">
                                                    <input id="datStartDate" class="form-control" size="10" type="text" value="" readonly="readonly" placeholder="Start Date" />
                                                    <span class="input-group-addon"><span class="fa fa-times"></span></span>
                                                    <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                                </div>
                                                <input type="hidden" id="dtStartDate" value="" />
                                            </div>
                                            <div class="col-md-6">
                                                <div class="input-group date form_date form_Enddate" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtEndDate">
                                                    <input id="datEndDate" class="form-control" size="10" type="text" value="" readonly="readonly" placeholder="End Date" />
                                                    <span class="input-group-addon"><span class="fa fa-times"></span></span>
                                                    <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                                </div>
                                                <input type="hidden" id="dtEndDate" value="" />
                                            </div>
                                        </div>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th class="text-left">장소</th>
                                <td colspan="3">
                                    <input type="text" class="form-control" id="htxtAddress" />
                                </td>
                            </tr>
                            <tr>
                                <th class="text-left">목적</th>
                                <td colspan="3">
                                    <input type="text" class="form-control" id="htxtPurpose" />
                                </td>
                            </tr>
                            <tr>
                                <th class="text-left">연자명 (소속)</th>
                                <td colspan="3">
                                    <div class="input-group hco-searcher">
                                        <input class="form-control hcp-code" id="htxtHCPName" type="text" readonly="" value="" disabled />
                                        <span class="input-group-btn">
                                            <button class="btn btn-info " data-toggle="modal" data-target="#divHCPSeacherModal" type="button"><span class="glyphicon glyphicon-search"></span></button>
                                        </span>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th class="text-left">강연/자문 주제</th>
                                <td colspan="3">
                                    <input type="text" class="form-control" id="htxtSubject" />
                                </td>
                            </tr>
                            <tr>
                                <th class="text-left">행사 개최국</th>
                                <td>
                                    <input type="text" class="form-control" id="htxtHostCountry" />
                                </td>
                                <th class="text-left">연자 초청국</th>
                                <td>
                                    <input type="text" class="form-control" id="htxtInvitingCountry" />
                                </td>
                            </tr>
                            <tr>
                                <th class="text-left">지급 국가</th>
                                <td>
                                    <input type="text" class="form-control" id="htxtPaymentCountry" />
                                </td>
                                <th class="text-left">지급 일자</th>
                                <td>
                                    <div class="input-group date form_date form_PaymentDate" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtPaymentDate">
                                        <input id="datPaymentDate" class="form-control" size="10" type="text" value="" readonly="readonly" />
                                        <span class="input-group-addon"><span class="fa fa-times"></span></span>
                                        <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                    </div>
                                    <input type="hidden" id="dtPaymentDate" value="" />
                                </td>
                            </tr>
                            <tr>
                                <th class="text-left">지급액 <br />(계약서 통화 단위 기준)</th>
                                <td>
                                    <input type="text" class="form-control number" id="htxtCurrencyAmount" />
                                </td>
                                <th class="text-left">계약서 통화 단위 <br />(e.g. USD, EUR, JPY, etc)</th>
                                <td>
                                    <input type="text" class="form-control" id="htxtCurrency" />
                                </td>
                            </tr>
                            <tr>
                                <th class="text-left">지급액 (한화)</th>
                                <td colspan="3">
                                    <input type="text" class="form-control number" id="htxtKRWAmount" />
                                </td>
                            </tr>
                            <tr>
                                <th class="text-left">항공 (Flight)</th>
                                <td colspan="3">
                                    <div class="radio">
                                        <label><input type="radio" name="optFlightClass" value="Business" checked="checked" /> Business</label>
                                        <label><input type="radio" name="optFlightClass" value="Economy" /> Economy</label>
                                    </div>
                                
                                    <div class="row row-md" style="padding-bottom:10px;">
                                        <div class="col-md-6">
                                            <div class="input-group date form_date form_FlightCheckin" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtFlightCheckin">
                                                <input id="datFlightCheckin" class="form-control" size="10" type="text" value="" readonly="readonly" placeholder="CheckIn" />
                                                <span class="input-group-addon"><span class="fa fa-times"></span></span>
                                                <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                            </div>
                                            <input type="hidden" id="dtFlightCheckin" value="" />
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-group date form_date form_FlightCheckout" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtFlightCheckout">
                                                <input id="datFlightCheckout" class="form-control" size="10" type="text" value="" readonly="readonly" placeholder="CheckOut" />
                                                <span class="input-group-addon"><span class="fa fa-times"></span></span>
                                                <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                            </div>
                                            <input type="hidden" id="dtFlightCheckout" value="" />
                                        </div>
                                    </div>
                                    <input type="text" class="form-control" id="htxtFlightComment" />
                                </td>
                            </tr>
                            <tr>
                                <th class="text-left">숙박 (Accommodation)</th>
                                <td colspan="3">
                                    <div class="row row-md" style="padding-bottom:10px;">
                                        <div class="col-md-6">
                                            <div class="input-group date form_date form_AccommodationCheckin" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtAccommodationCheckin">
                                                <input id="datAccommodationCheckin" class="form-control" size="10" type="text" value="" readonly="readonly" placeholder="CheckIn" />
                                                <span class="input-group-addon"><span class="fa fa-times"></span></span>
                                                <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                            </div>
                                            <input type="hidden" id="dtAccommodationCheckin" value="" />
                                        </div>
                                        <div class="col-md-6">
                                            <div class="input-group date form_date form_AccommodationCheckout" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtAccommodationCheckout">
                                                <input id="datAccommodationCheckout" class="form-control" size="10" type="text" value="" readonly="readonly" placeholder="CheckOut" />
                                                <span class="input-group-addon"><span class="fa fa-times"></span></span>
                                                <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                            </div>
                                            <input type="hidden" id="dtAccommodationCheckout" value="" />
                                        </div>
                                    </div>
                                    <input type="text" class="form-control" id="htxtAccommodationComment" />
                                </td>
                            </tr>
                            <tr>
                                <th class="text-left">식음료 (Meal&Beverage)</th>
                                <td colspan="3">
                                    <input type="text" class="form-control number" id="htxtMealBeverage" />
                                </td>
                            </tr>
                            <tr>
                                <th class="text-left">교통비 (Transportation)</th>
                                <td colspan="3">
                                    <input type="text" class="form-control number" id="htxtTransportation" />
                                </td>
                            </tr>
                            <tr>
                                <th class="text-left">Comment</th>
                                <td colspan="3">
                                    <input type="text" class="form-control" id="htxtComment" />
                                </td>
                            </tr>
                            <tr>
                                <th class="text-left">KRPIA 개인정보 제공 동의 여부</th>
                                <td colspan="3">
                                    <div class="radio">
                                        <label><input type="radio" name="optKRPIA" value="Y" checked="checked" /> Y</label>
                                        <label><input type="radio" name="optKRPIA" value="N" /> N</label>
                                        <label><input type="radio" name="optKRPIA" value="" /> NA</label>
                                    </div>
                                </td>
                            </tr>
                            <tr>
                                <th class="text-left">Attachment</th>
                                <td colspan="3">
                                    <!-- .attach -->
                                    <div id="divAttachFiles" class="attach attach-area" data-attachment-type="ICCMaster" style="padding-bottom: 10px; padding-top: 3px; padding-left: 0px; border-bottom-width: 0px;">
                                        <div id="progress" class="progress" style="height: 7px !important; visibility: hidden; margin-bottom: 0px;">
                                            <div class="progress-bar progress-bar-success">
                                            </div>
                                        </div>
                                        <span class="btn btn-sm btn-navy fileinput-button">
                                            <i class="fa fa-paperclip"></i>
                                            <span>Attachment</span>
                                            <!-- The file input field used as target for the file upload widget -->
                                            <input class="fileupload" id="fileupload" type="file" name="files" multiple />
                                        </span>
                                        <!-- The global progress bar -->
                                        <!-- The container for the uploaded files -->
                                        <ul class="attach-list files"></ul>
                                    </div>
                                    <!-- // .attach -->
                                </td>
                            </tr>
                        </table>
                        <input type="hidden" id="hhdIccId" />
                        
                        <div class="btnset">
                            <button type="button" class="btn btn-sm btn-navy fl" id="btnCancel"><i class="fa fa-trash-o"></i>Cancel</button>
                            <button type="button" class="btn btn-sm btn-red fr" id="btnSave"><i class="fa fa-floppy-o"></i>Save</button>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    
    <uc1:HCPSeacherModal runat="server" ID="HCPSeacherModal" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderBottom" Runat="Server">
    <script type="text/javascript" src="/ultra/Styles/Bootstrap/js/bootstrap-datetimepicker.min.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/Pages/Admin/ICCMaster.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/Common/HCPSeacher.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/JQuery/uploader/vendor/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/JQuery/uploader/jquery.iframe-transport.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/JQuery/uploader/jquery.fileupload.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/Common/ICCFileUploader.js"></script>
</asp:Content>

