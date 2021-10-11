<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Medical.master" AutoEventWireup="true" CodeFile="MicroMarketing.aspx.cs" Inherits="Pages_Medical_Study_StudyList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHolderHead" Runat="Server">
    <link href="/ultra/Styles/waitMe/waitMe.css" rel="stylesheet" />
    <script src="../../Scripts/Pages/FormEvent.js"></script>
    <script src="/ultra/Styles/waitMe/waitMe.js"></script>
    <style>
        body#l_frame {
            overflow-y : auto;
        }
        .sel-wrapper {
            position : absolute;
            padding-right : 19px;
            top: 20px;
            right: 15px;
            width: 40%;
            font-size: 0;
        }
        .sel-wrapper select{
            width: 49%;
            display : inline-block;
        }
        .sel-wrapper.equipment #selHospital{
            width: 98%;
        }
        .sel-wrapper.equipment #selQuarter{
            display: none;
        }
        .sel-wrapper select:first-child{
            margin-right: 2%;
        }
        #divHospitalList, #rightJsGridList {
            width: 100%;
        }
        #divHospitalList .jsgrid-header-row > .jsgrid-header-cell {
            border-top: none;
        }
        .jsgrid-grid-header {
            overflow-y: hidden;
        }
        .no-data {
            padding-top: 36px;
        }
        #divHospitalList .highlight > .jsgrid-cell {
            background: #c4e2ff;
            border-color: #c4e2ff;
        }
        .panel-heading{
            margin-bottom : 20px;
        }
        .tab-pane .panel-body {
            margin-bottom: 15px;
        }
        .tab-content .panel-heading .panel-title{
            font-size: 14px;
            font-weight: 700;
            padding : 0;
        }
        .btn-wrapper {
            padding : 15px 19px;
        }
        input[type="number"]::-webkit-outer-spin-button,
        input[type="number"]::-webkit-inner-spin-button {
            -webkit-appearance: none;
            margin: 0;
        }
        input[type='number'] {
            -moz-appearance:textfield;
        }
        input[disabled].form-control { 
            background-color : #efefef;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderForm" Runat="Server">
    <div class="row">
        <%if (!auth) { %>
        <div class="col-sm-12">
            <div class="box-panel">
                <div class="panel-heading">
                    <div class="text-center"><%=errorMessage%></div>
                </div>
            </div>
        </div>
        <%} %>
        <%else { %>
        <div class="col-sm-12">
            <ul id="tabEventMaster" class="nav nav-tabs nav-tabs-fixcenter" role="tablist">
                <li id="equipment" runat="server" role="presentation" class="active"><a href="#equipment" aria-controls="tpForm" role="tab" data-toggle="tab" onclick="handleTabSelected('equipment', this)">Equipment</a></li>
                <li id="examination" runat="server" role="presentation"><a href="#examination" aria-controls="tpCostPlan" role="tab" data-toggle="tab" onclick="handleTabSelected('examination', this)">Examination</i></a></li>
                <li id="market_share" runat="server" role="presentation"><a href="#market_share" aria-controls="tpCostPlan" role="tab" data-toggle="tab" onclick="handleTabSelected('market_share', this)">Market Share</i></a></li>
            </ul>
            <div class="box-panel">
                <div class="panel-heading">
			        <h1 class="panel-title">Micro Marketing : <span id="subtitle">Equipment</span></h1>
                    <div class="sel-wrapper equipment">
                        <select id="selHospital" class="form-control inblock chosen-select">
                        </select>
                        <select id="selQuarter" class="form-control inblock" onchange="changeQuarter(this.value)">
                            <option>2018-1Q</option>
                        </select>
                    </div>
                </div>
                <div class="tab-content">
                    <div role="tabpanel" class="tab-pane active panel-default" id="equipment">
                        <div class="row row-md">
                            <div class="col-sm-12 text-center no-data">No data selected</div>
                            <div class="col-sm-12 has-data hidden">
                                <div class="panel-body">
                                    <table id="equipment-table" class="table form-group-sm" style="margin-bottom: 0px;">
                                        <colgroup>
                                            <col style="width: 25%;">
                                            <col style="width: 25%;">
                                            <col style="width: 25%;">
                                            <col>
                                        </colgroup>
                                        <tbody>
                                            <tr>
                                                <th>Category</th>
                                                <td>
                                                    <select id="eq_category" class="form-control" name="CATEGORY">
                                                        <option selected disabled>선택</option>
                                                        <option value="CT">CT</option>
                                                        <option value="MRI">MRI</option>
                                                        <option value="General Angio">General Angio</option>
                                                        <option value="Cadiac Angio">Cadiac Angio</option>
                                                    </select>
                                                </td>
                                                <th>Status</th>
                                                <td>
                                                    <select id="eq_status" class="form-control" name="STATUS">
                                                        <option selected disabled>선택</option>
                                                        <option value="Installed">Installed</option>
                                                    </select>
                                                </td>
                                                
                                            </tr>
                                            <tr>
                                                <th>Manufacture</th>
                                                <td>
                                                    <select id="eq_manufacturer" class="form-control" name="MANUFACTURER">
                                                        <option selected disabled>선택</option>
                                                    </select>
                                                </td>
                                                <th>Equipment type</th>
                                                <td>
                                                    <select id="eq_equipment_type" class="form-control" name="EQUIPMENT_TYPE">
                                                        <option selected disabled>선택</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Scanner model type</th>
                                                <td>
                                                    <select id="eq_scanner_model_type" class="form-control" name="SCANNER_MODEL_TYPE">
                                                        <option selected disabled>선택</option>
                                                        <option value="4">4</option>
                                                        <option value="16">16</option>
                                                        <option value="32">32</option>
                                                        <option value="64">64</option>
                                                        <option value="128">128</option>
                                                        <option value="256">256</option>
                                                        <option value="320">320</option>
                                                        <option value="640">640</option>
                                                        <option value="1T">1T</option>
                                                        <option value="1.5T">1.5T</option>
                                                        <option value="3T">3T</option>
                                                    </select>
                                                </td>
                                                <th>Injector manufacturer</th>
                                                <td>
                                                    <select id="eq_injector_manufacturer" class="form-control" name="INJECTOR_MANUFACTURER">
                                                        <option selected disabled>선택</option>
                                                        <option value="LF">LF</option>
                                                        <option value="EZ-EM">EZ-EM</option>
                                                        <option value="CMS (Ulrich)">CMS (Ulrich)</option>
                                                        <option value="Guerbet">Guerbet</option>
                                                        <option value="Bracco">Bracco</option>
                                                        <option value="Nemoto">Nemoto</option>
                                                        <option value="Medrad">Medrad</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Date of installation</th>
                                                <td>
                                                    <div class="input-group date form-date-hour-min form_dateTime" data-date="" data-date-format="yyyy-mm-dd hh:ii:ss" data-link-field="dtStartTime">
                                                        <input id="eq_date_of_installation" class="form-control" size="16" type="text" value="" readonly name="DATE_OF_INSTALLATION"/>
                                                        <span class="input-group-addon"><span class="fa fa-times"></span></span>
                                                        <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                                    </div>
                                                </td>
                                                <th></th>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Memo</th>
                                                <td colspan="3">
                                                    <textarea id="eq_memo" class="form-control" rows="2" name="MEMO"></textarea>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div class="btnset">
                                        <button class="btn btn-sm btn-gray fl btnDelete" id="" type="button"><i class="fa fa-trash-o"></i>Delete</button>
                                        <button class="btn btn-sm btn-navy fl btnNew" id="" style="margin-left: 3px;" type="button"><i class="fa fa-close"></i>New</button>
                                        <button class="btn btn-sm btn-red fr btnSave" id="" type="button"><i class="fa fa-floppy-o"></i>Save</button>
                                    </div>
                                </div>
		                        <div class="panel-body">

		                            <div id="JsGrid_equipment"></div>
			
			                        <!-- jsgrid pager -->
			                        <div class="jsgrid-pager-container" style="display: block;">
			                        </div><!--// jsgrid pager -->
		                        </div><!-- // .panel-body -->
		                    </div>
                        </div>
                    </div>
                    <div role="tabpanel" class="tab-pane" id="examination">
                        <div class="row row-md">
                            <div class="col-sm-12 text-center no-data">No data selected</div>
                            <div class="col-sm-12 has-data hidden">
                                <div class="panel-body">
                                    <table id="examination-table"class="table form-group-sm" style="margin-bottom: 0px;">
                                        <colgroup>
                                            <col style="width: 25%;">
                                            <col style="width: 25%;">
                                            <col style="width: 25%;">
                                            <col>
                                        </colgroup>
                                        <tbody>
                                            <tr>
                                                <th>Examination type</th>
                                                <td>
                                                    <select id="ex_examination_type" class="form-control" name="EXAMINATION_TYPE">
                                                        <option selected disabled>선택</option>
                                                    </select>
                                                </td>
                                                <th>Scan type</th>
                                                <td>
                                                    <select id="ex_scan_type" class="form-control" name="SCAN_TYPE">
                                                        <option selected disabled>선택</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Number of cases</th>
                                                <td>
                                                    <input type="number" id="ex_number_of_cases" class="form-control" name="NUMBER_OF_CASES"/>
                                                </td>
                                                <th>Number of enhanced cases</th>
                                                <td>
                                                    <input type="number" id="ex_number_of_enhanced_cases" class="form-control" name="NUMBER_OF_ENHANCED_CASES"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Case relation(%)</th>
                                                <td>
                                                    <input type="text" id="ex_case_relation" class="form-control" name="CASE_RELATION" disabled readonly/>
                                                </td>
                                                <th></th>
                                                <td>
                                                    
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Comment</th>
                                                <td colspan="3">
                                                    <textarea id="ex_comment" class="form-control" rows="2" name="COMMENT"></textarea>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div class="btnset">
                                        <button class="btn btn-sm btn-gray fl btnDelete" id="" type="button"><i class="fa fa-trash-o"></i>Delete</button>
                                        <button class="btn btn-sm btn-navy fl btnNew" id="" style="margin-left: 3px;" type="button"><i class="fa fa-close"></i>New</button>
                                        <button class="btn btn-sm btn-red fr btnSave" id="" type="button"><i class="fa fa-floppy-o"></i>Save</button>
                                    </div>
                                </div>
		                        <div class="panel-body">

		                            <div id="JsGrid_examination"></div>
			
			                        <!-- jsgrid pager -->
			                        <div class="jsgrid-pager-container" style="display: block;">
			                        </div><!--// jsgrid pager -->
		                        </div><!-- // .panel-body -->
		                    </div>
                        </div>
                    </div>
                
                    <div role="tabpanel" class="tab-pane" id="market_share">
                        <div class="row row-md">
                            <div class="col-sm-12 text-center no-data">No data selected</div>
                            <div class="col-sm-12 has-data hidden">
                                <div class="panel-body">
                                    <table id="market_share-table" class="table form-group-sm" style="margin-bottom: 0px;">
                                        <colgroup>
                                            <col style="width: 25%;">
                                            <col style="width: 25%;">
                                            <col style="width: 25%;">
                                            <col>
                                        </colgroup>
                                        <tbody>
                                            <tr>
                                                <th>Product family</th>
                                                <td>
                                                    <select id="ms_famaily" class="form-control" name="PRODUCT_FAMAILY">
                                                        <option selected disabled>선택</option>
                                                        <option value="XCM">XCM</option>
                                                        <option value="MRCM">MRCM</option>
                                                    </select>
                                                </td>
                                                <th>Product</th>
                                                <td>
                                                    <select id="ms_product" class="form-control" name="PRODUCT">
                                                        <option selected disabled>선택</option>
                                                        <option value="XCM">XCM</option>
                                                        <option value="MRCM">MRCM</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Segment</th>
                                                <td>
                                                    <input type="text" readonly disabled id="ms_segment" class="form-control" name="SEGMENT">
                                                </td>
                                                <th>Sub category</th>
                                                <td>
                                                    <select id="ms_subsegment" class="form-control" name="SUB_CATEGORY">
                                                        <option selected disabled>선택</option>
                                                    </select>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Manufacturer</th>
                                                <td>
                                                    <input type="text" readonly disabled id="ms_manufacturer" class="form-control" name="MANUFACTURER">
                                                </td>
                                                <th>Price</th>
                                                <td>
                                                    <input type="text" readonly disabled id="ms_price" class="form-control" oninput="this.value=numberWithCommas(this.value)" name="PRICE"/>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Quantity</th>
                                                <td>
                                                    <input type="text" id="ms_quantity" class="form-control" oninput="this.value=numberWithCommas(this.value)" name="QUANTITY"/>
                                                </td>
                                                <th>Total</th>
                                                <td>
                                                    <input type="text" id="ms_total" class="form-control" readonly disabled name="SALES"/>
                                                </td>
                                            </tr>
                                            <tr>
                        
                                            </tr>
                                            <tr>
                                                <th>Comment</th>
                                                <td colspan="3">
                                                    <textarea id="ms_comment" name="COMMENT" class="form-control" rows="2"></textarea>
                                                </td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <div class="btnset">
                                        <button class="btn btn-sm btn-gray fl btnDelete" type="button"><i class="fa fa-trash-o"></i>Delete</button>
                                        <button class="btn btn-sm btn-navy fl btnNew" style="margin-left: 3px;" type="button"><i class="fa fa-close"></i>New</button>
                                        <button class="btn btn-sm btn-red fr btnSave" type="button"><i class="fa fa-floppy-o"></i>Save</button>
                                    </div>
                                </div>
		                        <div class="panel-body">

		                            <div id="JsGrid_market_share"></div>
			
			                        <!-- jsgrid pager -->
			                        <div class="jsgrid-pager-container" style="display: block;">
			                        </div><!--// jsgrid pager -->
		                        </div><!-- // .panel-body -->
		                    </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <%} %>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="contentModalBody" Runat="Server">
    <!-- Modal ms-->
    <div id="exModal" class="hidden modalTab">
        <div class="modal-header">
            <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
            <h4 class="modal-title" id="myModalLabel">Modify and copy to the next Quarter</h4>
        </div>
        <div class="modal-body">
                <table class="table form-group-sm" style="margin-bottom: 0px;">
                    <colgroup>
                        <col style="width: 25%;">
                        <col style="width: 25%;">
                        <col style="width: 25%;">
                        <col>
                    </colgroup>
                    <tbody>
                        <tr>
                            <th>Examination type</th>
                            <td>
                                <select id="ex_modal_examination_type" class="form-control" name="EXAMINATION_TYPE">
                                    <option selected disabled>선택</option>
                                </select>
                            </td>
                            <th>Scan type</th>
                            <td>
                                <select id="ex_modal_scan_type" class="form-control" name="SCAN_TYPE">
                                    <option selected disabled>선택</option>
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <th>Number of cases</th>
                            <td>
                                <input type="number" id="ex_modal_number_of_cases" class="form-control" name="NUMBER_OF_CASES"/>
                            </td>
                            <th>Number of enhanced cases</th>
                            <td>
                                <input type="number" id="ex_modal_number_of_enhanced_cases" class="form-control" name="NUMBER_OF_ENHANCED_CASES"/>
                            </td>
                        </tr>
                        <tr>
                            <th>Case relation(%)</th>
                            <td>
                                <input type="text" id="ex_modal_case_relation" class="form-control" name="CASE_RELATION" disabled readonly/>
                            </td>
                            <th></th>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <th>Comment</th>
                            <td colspan="3">
                                <textarea id="ex_modal_comment" class="form-control" rows="2" name="COMMENT"></textarea>
                            </td>
                        </tr>

                    </tbody>
                </table>
        </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary" onclick="copyExamination()">Copy</button>
        </div>
    </div>
    <div id="msModal" class="hidden modalTab">
        <div class="modal-header">
    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><span aria-hidden="true">&times;</span></button>
    <h4 class="modal-title" id="myModalLabel">Modify and copy to the next Quarter</h4>
        </div>
        <div class="modal-body">
            <table class="table form-group-sm" style="margin-bottom: 0px;">
                <colgroup>
                    <col style="width: 25%;">
                    <col style="width: 25%;">
                    <col style="width: 25%;">
                    <col>
                </colgroup>
                <tbody>
                    <tr>
                        <th>Product family</th>
                        <td>
                            <select id="ms_modal_famaily" class="form-control" name="PRODUCT_FAMAILY">
                                <option selected disabled>선택</option>
                                <option value="XCM">XCM</option>
                                <option value="MRCM">MRCM</option>
                            </select>
                        </td>
                        <th>Product</th>
                        <td>
                            <select id="ms_modal_product" class="form-control" name="PRODUCT">
                                <option selected disabled>선택</option>
                                <option value="XCM">XCM</option>
                                <option value="MRCM">MRCM</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <th>Segment</th>
                        <td>
                            <input type="text" readonly disabled id="ms_modal_segment" class="form-control" name="SEGMENT">
                        </td>
                        <th>Sub category</th>
                        <td>
                            <select id="ms_modal_subsegment" class="form-control" name="SUB_CATEGORY">
                                <option selected disabled>선택</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <th>Manufacturer</th>
                        <td>
                            <input type="text" readonly disabled id="ms_modal_manufacturer" class="form-control" name="MANUFACTURER">
                        </td>
                        <th>Price</th>
                        <td>
                            <input type="text" readonly disabled id="ms_modal_price" class="form-control" oninput="this.value=numberWithCommas(this.value)" name="PRICE"/>
                        </td>
                    </tr>
                    <tr>
                        <th>Quantity</th>
                        <td>
                            <input type="text" id="ms_modal_quantity" class="form-control" oninput="this.value=numberWithCommas(this.value)" name="QUANTITY"/>
                        </td>
                        <th>Total</th>
                        <td>
                            <input type="text" id="ms_modal_total" class="form-control" readonly disabled name="SALES"/>
                        </td>
                    </tr>
                    <tr>
                        
                    </tr>
                    <tr>
                        <th>Comment</th>
                        <td colspan="3">
                            <textarea id="ms_modal_comment" name="COMMENT" class="form-control" rows="2"></textarea>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div class="modal-footer">
            <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
            <button type="button" class="btn btn-primary" onclick="copyMarketShare()">Copy</button>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cpHolderBottom" Runat="Server">
    <input type="hidden" id="hhdUserID" runat="server" />
    <input type="hidden" id="hhdUserName" runat="server" />
    <input type="hidden" id="userType" runat="server" />
    <script src="../../Scripts/Pages/Report/MicroMarketing.js"></script>
    <script src="../../Scripts/JQuery/cookie.js"></script>
    <script src="../../Scripts/JQuery/jquery-ui-1.12.1.custom/jquery-ui.js"></script>
</asp:Content>
