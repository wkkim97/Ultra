<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Event.master" AutoEventWireup="true" CodeFile="FreeGood.aspx.cs" Inherits="Pages_Event_FreeGood" %>

<%@ Register Src="~/Common/Controls/ProductSearchModal.ascx" TagPrefix="uc1" TagName="ProductSearchModal" %>
<%@ Register Src="~/Common/Controls/MedicalSearcher.ascx" TagPrefix="uc1" TagName="MedicalSearcher" %>
<%@ Register Src="~/Common/Controls/MedicalSearcherModal.ascx" TagPrefix="uc1" TagName="MedicalSearcherModal" %>

<%@ Register Src="~/Common/Controls/HealthCarePharmacistSeacherModal.ascx" TagPrefix="uc1" TagName="HealthCarePharmacistSeacherModal" %> 

 


<asp:Content ID="Content1" ContentPlaceHolderID="cpHolderHead" runat="Server">

    <style type="text/css">
        /*.text-core {
        height: 26px !important;
    }
    .inputFreeGood {
        top: 7px;
        height: 28px; 
        padding-top: 0px;
        padding-bottom: 0px;
        margin-top: 2px;
        border-bottom-width: 1px;
    }
    .txtRight{
        text-align: right;
    }*/
        .jsgrid-cell {
            text-overflow: ellipsis;
            -o-text-overflow: ellipsis;
            white-space: normal !important;
            overflow: hidden;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderForm" runat="Server">
    <uc1:ProductSearchModal runat="server" ID="ProductSearchModal" />
    <table class="table form-group-sm">
        <tbody>
            <tr>
                <th>Subject</th>
                <td>
                    <input class="form-control" id="txtSubject" type="text" /></td>
            </tr>
            <tr>
                <th>Purpose</th>
                <td>
                    <select id="selPurpose" class="form-control">
                        <option title="성상확인용 (Identification of form. dosage)"  value="1" sampletype="HCP">성상확인용 (Identification of form. dosage)</option>
                        <option title="약제과 제출용 (DC Use)" value="2" sampletype="HCP|NON-HCP">약제과 제출용 (DC Use)</option>
                        <option title="SPU(응급임상) / MAP(치료목적)" value="13" sampletype="HCP|NON-HCP|INVEST">SPU(응급임상) / MAP(치료목적)</option> 
                        <option title="임상시험용 (Investigational Medical Product)" value="3" sampletype="INVEST">임상시험용 (Investigational Medical Product)</option>
                        <!--<option title="Promotion (Only for CH)" value="4" sampletype="NON-HCP">Promotion (Only for CH)</option>-->
                        <option title="Medical Appliance for Injection" value="5" sampletype="NON-HCP">Medical Appliance for Injection</option>
                        <!--<option title="Product Testing(Only AH)" value="6" sampletype="NON-HCP">Product Testing(Only AH)</option>--><!--Tickets No:INC15365079-->
                        <!--<option title="Clinical trial and Inspection" value="7" sampletype="NON-HCP">Clinical trial and Inspection</option>--><!--Tickets No:INC15365079-->
                        <option title="Promotion(non-pharmaceutical products only)" value="8" sampletype="NON-HCP">Promotion(non-pharmaceutical products only)</option>
                        <option title="Product photograph" value="9" sampletype="HCP|NON-HCP">Product photograph</option>
                        <!--<option title="PPL-product placement for drama of other TVC" value="10" sampletype="HCP|NON-HCP">PPL-product placement for drama of other TVC</option>--><!--Tickets No:INC15365079-->
                        <!--<option title="Programs" value="11" sampletype="HCP|NON-HCP">Programs</option>--><!--Tickets No:INC15365079-->
                        <!--<option title="Phantom Study" value="12" sampletype="HCP|NON-HCP">Phantom Study</option>--><!--Tickets No:INC15365079-->
                        <option title="Demo-Gimmick(WH only)" value="14" sampletype="HCP|NON-HCP">Demo-Gimmick(WH only)</option>
                        <option title="구매전 의료기기대여(RAD only)" value="16" sampletype="RMD|HCP">구매전 의료기기대여(RAD only)</option><!--Tickets No:INC15387473-->
                        <!--<option title="Experience marketing (AH Only)" value="15" sampletype="NON-HCP">Experience marketing (AH Only)</option>--><!--Tickets No:INC15365079-->
                    </select>
                </td>
            </tr>
            <tr>
                <th>Business Unit(BU)</th>
                <td>
                    <div class="radio" style="padding: 0px">
                        <label>
                            <input name="rdoBU" type="radio" value="HH" checked="" />HH</label>
                        <label>
                            <input name="rdoBU" type="radio" value="WH" />WH</label>
                        <label>
                            <input name="rdoBU" type="radio" value="SM" />SM</label>
                        <label>
                            <input name="rdoBU" type="radio" value="RAD" />RAD</label>
                        <label>
                            <input name="rdoBU" type="radio" value="CH" />CH</label>
                        <label>
                            <input name="rdoBU" type="radio" value="AH" />AH</label>
                    </div>
                </td>
            </tr>
            <tr>
                <th>Location</th>
                <td>
                    <select id="selLocation" class="form-control">
                        <option value="seoul">서울</option>
                        <option value="yongma">용마</option>
                        <option value="daejeon">대전</option>
                        <option value="daegu">대구</option>
                        <option value="busan">부산</option>
                        <option value="kwangju">광주</option>
                        <option value="ansan">안산공장</option>
                        <option value="anseong">안성공장</option>
                        <%-- 2022.06.20 add by qiang INC15741711--%>
                        <option value="sunyudo">선유도사무실</option>
                    </select>

                </td>
            </tr>
            <tr id="trImpactNo">
                <th>Impact No</th>
                <td>
                    <uc1:MedicalSearcher runat="server" ID="MedicalSearcher" />
                    <uc1:MedicalSearcherModal runat="server" ID="MedicalSearcherModal" />
                </td>
            </tr>
            <tr id="trPoNo">
                <th>PO No</th>
                <td>
                    <input class="form-control" id="txtPoNo" type="text" />
                </td>
            </tr>

            <!-- Ver 1.0.7 : Go-Direct -->            
            <tr id="trDeliveryDate">
                <th>배송일자</th>
                <td>
                    <div class="input-group date form-date form_date form_date_deliverydate" data-date="" data-date-format="yyyy-mm-dd" data-link-field="txtDeliveryDate">
                        <input id="dateDeliveryDate" class="form-control" size="16" type="text" value="" readonly />
                        <span class="input-group-addon"><span class="fa fa-times"></span></span>
                        <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                    </div>
                    <input type="hidden" id="txtDeliveryDate" value="" />


                    
                </td>
            </tr>
            <tr id="trReturnDate">
                <th>반납일자</th>
                <td>
                    <div class="input-group date form-date form_date form_date_returndate" data-date="" data-date-format="yyyy-mm-dd" data-link-field="txtReturnDate">
                        <input id="dateReturnDate" class="form-control" size="16" type="text" value="" readonly />
                        <span class="input-group-addon"><span class="fa fa-times"></span></span>
                        <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                    </div>
                    <input type="hidden" id="txtReturnDate" value="" />


                    
                </td>
            </tr>
           <!-- Ver 1.0.7 : Go-Direct -->

            <tr>
                <th>Comment</th>
                <td>
                    <textarea id="txtComment" class="form-control" rows="3" style="width: 100%;"></textarea>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="cpHolderEtc" runat="Server">
    <div id="hdivHCPArea" style="display: none">
        <div class="box-panel" style="margin-bottom: 20px; display: none;" id="hdivAddHCP">
            <button type="button" class="btn-panel-close"><i class="fa fa-close"><span class="tts">Close</span></i></button>
            <div class="row row-md">
                <div class="col-md-12">
                    <h3 class="panel-title">Add Data(HCP)</h3>
                    <div class="radio">
                        <label>
                            <input type="radio" name="optHCPType" id="optDoctor" value="D" checked="checked" />
                            의사
                        </label>
                        <label>
                            <input type="radio" name="optHCPType" id="optPharmacist" value="P" />
                            약사
                        </label>
                        <!--
                        <label>
                            <input type="radio" name="optHCPType" id="optHCO" value="H" />
                            병원
                        </label>
                        -->
                    </div>
                    <table class="table-default form-group-sm">
                        <colgroup>
                            <col />
                            <col />
                            <col />
                        </colgroup>
                        <tbody>
                            <tr>
                                <th scope="row" style="text-align: left">HCP</th>
                                <th scope="row" style="text-align: left">Sample</th>
                                <th scope="row" style="text-align: left">Qty<span id="notice" style="font-size:10px;">(Notice: 평가기간 동안 평가하기 위하여 필요한 최소수량)</span></th><!--Tickets No:INC15364999-->
                            </tr>
                            <tr>
                                <td>
                                    <%--  <textarea id="taHCP" class="example" rows="1" style="width: 100%;"></textarea> --%>
                                    <%-- <textarea id="taMedicalHCP" class="example" rows="1" style="width: 100%;display:none;"></textarea> --%>
                                    <div class="input-group">
                                        <input class="form-control" id="taHCP" type="text" readonly="" value="" disabled />
                                        <span class="input-group-btn">
                                            <button class="btn btn-sm btn-info" id="btnHCP" data-toggle="modal" data-target="#layer_searchHcp" type="button"><span class="glyphicon glyphicon-search"></span></button>
                                        </span>
                                    </div>
                                </td>
                                <td>
                                    <div class="input-group">
                                        <input class="form-control" id="taProd" type="text" readonly="" value="" disabled />
                                        <span class="input-group-btn">
                                            <button class="btn btn-sm btn-info " id="btnProd" data-toggle="modal" type="button"><span class="glyphicon glyphicon-search"></span></button>
                                        </span>
                                    </div>
                                </td>
                                <td>
                                    <input type="text" class="form-control inputFreeGood txtRight" id="txtQty" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="btnset">
                        <button type="button" class="btn-sm btn-navy fl" style="display: none;" id="btnHCPDelete"><i class="fa fa-trash-o"></i>Delete</button>
                        <button type="button" class="btn-sm btn-red fr" id="btnHCPSave"><i class="fa fa-floppy-o"></i>SAVE</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="box-panel" style="margin: 0px 0px 20px 0px;">
            <div class="row row-md">
                <div class="col-md-12">
                    <div class="panel-heading">
                        <button type="button" class="btn btn-sm btn-wyellow fr" id="btnHCPAdd"><i class="fa"></i>Add</button>
                        <h3 class="panel-title">Sample Information(HCP)</h3>
                    </div>
                    <div class="panel-body">
                        <div id="jsGridHcp"></div>
                    </div>
                </div>

            </div>
        </div>
    </div>
    <div id="hdivOtherArea" style="display: none">
        <div class="box-panel" style="margin-bottom: 20px; display: none;" id="hdivAddOther">
            <button type="button" class="btn-panel-close"><i class="fa fa-close"><span class="tts">Close</span></i></button>
            <div class="row row-md">
                <div class="col-md-12">
                    <h3 class="panel-title">Add Data(OTHER)</h3>
                    <table class="table-default form-group-sm">
                        <colgroup>
                            <col />
                            <col />
                            <col />
                            <col />
                        </colgroup>
                        <tbody>
                            <tr>
                                <th scope="row" style="text-align: left">Organization</th>
                                <th scope="row" style="text-align: left">Customer</th>
                                <th scope="row" style="text-align: left">Sample</th>
                                <th scope="row" style="text-align: left">Qty</th>
                            </tr>
                            <tr>
                                <td>
                                    <input type="text" class="form-control inputFreeGood" id="txtOtOrg" />
                                </td>
                                <td>
                                    <input type="text" class="form-control inputFreeGood" id="txtOtCum" />
                                </td>
                                <td>

                                    <div class="input-group">
                                        <input class="form-control" id="taSample" type="text" readonly="" value="" disabled />
                                        <span class="input-group-btn">
                                            <button class="btn btn-sm btn-info " id="btnSample" data-toggle="modal" type="button"><span class="glyphicon glyphicon-search"></span></button>
                                        </span>
                                    </div>
                                </td>
                                <td>
                                    <input type="text" class="form-control inputFreeGood txtRight" id="txtOtQty" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="btnset">
                        <button type="button" class="btn-sm btn-navy fl" id="btnOtherDelete" style="display: none;"><i class="fa fa-trash-o"></i>Delete</button>
                        <button type="button" class="btn-sm btn-red fr" id="btnOtherSave"><i class="fa fa-floppy-o"></i>SAVE</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="box-panel" style="margin: 0px 0px 20px 0px;">
            <div class="row row-md">
                <div class=" col-md-12">
                    <div class="panel-heading">
                        <button type="button" class="btn btn-sm btn-wyellow fr" id="btnOtAdd"><i class="fa"></i>Add</button>
                        <h3 class="panel-title">Sample Information(OTHER)</h3>
                    </div>
                    <div class="panel-body">
                        <div id="jsGridOther"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div id="layer_searchHcp" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-alert" role="document">
            <div class="modal-content">
                <div class="box-panel" style="width: 100%">
                    <div id="tabContractHCP" class="tab-pane-korea">
                        <div id="divContractHCP" class="row row-sm">
                            <div class="col-xs-7 col-md-12">
                                <div class="row row-sm">
                                    <div class="form-group col-xs-4">
                                        <label for="lb_name">Name(HCP)</label>
                                        <input type="text" id="txtHcpName" class="form-control" />
                                    </div>
                                    <div class="form-group col-xs-8">
                                        <label for="lb_hp">Hospital/Phamacy(HCO)</label>
                                        <input type="text" id="txtHcoName" class="form-control" />
                                    </div>
                                </div>
                            </div>
                            <div class="form-group col-xs-5 col-md-12">
                                <div class="row row-sm">
                                    <label for="lb_row" class="lb_special col-md-4">Specialty</label>
                                    <div class="col-xs-8 col-md-4 col-lg-5">
                                        <input type="text" id="txtSpecialty" class="form-control" />
                                    </div>
                                    <div class="col-xs-4 col-md-4 col-lg-3">
                                        <button id="htnSearchContractHcp" type="button" class="btn btn-sm btn-block btn-darkgray">Search</button>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="tb-scroll">
                            <div class="tb-head">
                                <table>
                                    <colgroup>
                                        <col style="width: 100%">
                                    </colgroup>
                                </table>
                            </div>
                            <!-- // .tb-head -->
                            <div class="tb-body">
                                <table id="tblHcp">
                                    <colgroup>
                                        <col style="width: 100%" />
                                    </colgroup>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                            <!-- // .tb-body -->
                        </div>
                        <div class="btnset">
                            <button type="button" class="btn btn-sm btn-block btn-red" id="btnAttendContractHCP">Add Attend</button>
                        </div>
                    </div>
                    <button type="button" class="btn-panel-close"><i class="fa fa-close"><span class="tts">Close</span></i></button>
                </div> 
            </div>
        </div>
    </div>
    <uc1:HealthCarePharmacistSeacherModal runat="server" ID="HealthCarePharmacistSeacherModal" />
     <!-- Ver 1.0.7 : Go-Direct -->
    
     <!-- Ver 1.0.7 : Go-Direct -->
    <div class="modal fade" id="sample_hcp_info" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-lg" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
                </div>
                <div class="modal-body">
                    <div id="divSampleHcp" class="name-heading panel-fill">
                        <h1 class="name-title"></h1>
                    </div>
                    <table id="tblExistSample" class="table table-striped">
                        <thead>
                            <tr>
                                <th>Date</th>
                                <th>Subject</th>
                                <th>EventKey</th>
                                <th>Requester</th>
                                <th>Sample</th>
                                <th>HCP</th>
                            </tr>
                        </thead>
                        <tbody>
                        </tbody>
                    </table>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-lg btn-navy" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderBottom" runat="Server">
    <script src="../../Scripts/Pages/Event/FreeGood.js"></script>
    <script src="../../Scripts/Common/ProductSearch.js"></script>
    <script src="../../Scripts/Common/MedicalSearcher.js"></script>

    <script src="../../Scripts/Common/HealthCarePharmacistSeacher.js"></script>
    <script src="../../Scripts/Common/HealthCareOfficeSearcher.js"></script>
    <input type="hidden" id="datStartTime" runat="server" />
</asp:Content>

