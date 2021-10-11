<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ContractDetail.ascx.cs" Inherits="Pages_Medical_Controls_ContractDetail" %>
<%@ Register Src="~/Pages/Medical/Controls/SearchHCP.ascx" TagPrefix="uc1" TagName="SearchHCP" %>
<%@ Register Src="~/Pages/Medical/Controls/Payment.ascx" TagPrefix="uc1" TagName="Payment" %>
<%@ Register Src="~/Pages/Medical/Controls/IMP.ascx" TagPrefix="uc1" TagName="IMP" %>


<style type="text/css">
    .text-core {
        height: 26px !important;
    }

    .text-wrap {
        height: 34px !important;
    }

    .text-core .text-wrap .text-tags .text-tag .text-button {
        font-size: 12px !important;
    }

    .text-tags text-tags-on-top {
        padding-top: 7px;
    }
</style>

<div class="tab-cont-area row row-md">
    <div class="contract_information">
        <div class="table-heading">
            <h2 class="table-title">Contract Information</h2>
            <a href="#" id="btnBack" class="btn btn-sm btn-default">Back</a>
        </div>
        <div class="box-panel" style="overflow: visible;">
            <div class="table-responsive" style="overflow: visible;">
                <table class="table-default">
                    <colgroup>
                        <col style="width: 80px;">
                        <col style="width: 430px;">
                        <col style="width: 80px;">
                        <col style="width: 140px;">
                        <col style="width: 80px;">
                        <col style="width: 140px;">
                    </colgroup>
                    <tbody>
                        <tr>
                            <th scope="row">시험책임자</th>
                            <td style="overflow: visible; padding-top: 0px; padding-bottom: 8px;" id="tdDoctorArea">
                                <textarea id="taDoctor" class="example" rows="1" style="width: 430px; height: 34px; line-height: 22px;"></textarea>
                                <span id="spanDoctorInfo" ></span>
                                <input type="hidden" id="hhdHcpcode" />
                                <input type="hidden" id="hhdHcocode" />
                                <input type="hidden" id="hhdHcpname" />
                                <input type="hidden" id="hhdHconame" />
                                <input type="hidden" id="hhdSpecialtyCode" />
                                <input type="hidden" id="hhdSpecialtyName" />
                            </td>
                            <th scope="row">지급된 연구비</th>
                            <td>
                                <input type="text" class="form-control" id="txtResearchFund" style="text-align: right;" readonly /></td>
                            <th scope="row">계약상태</th>
                            <td>
                                <select class="form-control" id="selContractStatus" runat="server">
                                </select>
                            </td>
                        </tr>
                        <tr>
                            <th scope="row">Contract<br />
                                No</th>
                            <td><span id="spanPreFixContractNo" style="display: inline;"></span>-
                                <input type="text" id="txtLastContractNo" class="form-control" style="width: 385px; display: inline;" /></td>
                            <th scope="row">최초<br>
                                계약체결일</th>
                            <td>
                                <div class="input-group date form_datetime" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtContractDate">
                                    <input type="text" id="dtContractDate" class="form-control" />
                                    <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                </div>
                            </td>
                            <th scope="row" class="DueDate" style="display: none;">계약<br />
                                만료일</th>
                            <td class="DueDate" style="display: none;">
                                <div class="input-group date form_datetime" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtContractLastDate">
                                    <input type="text" id="dtContractLastDate" class="form-control" />
                                    <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <th scope="row">Remark</th>
                            <td colspan="5">
                                <input type="text" id="txtRemark" class="form-control" /></td>
                        </tr>
                    </tbody>
                </table>
            </div>
            <div class="btnset">
                <button type="button" class="btn btn-sm btn-navy fl" id="btnContractDelete"><i class="fa fa-trash-o"></i>Delete</button>
                <button type="button" class="btn btn-sm btn-red fr" id="btnContractSave"><i class="fa fa-floppy-o"></i>Save</button>
            </div>
        </div>
    </div>
    <div class="contract_information" id="divPaymentArea"> 
        <div class="box-panel" style="overflow: visible;">
            <div class="table-heading">
                <h2 class="table-title">Payment</h2>
                <button type="button" class="btn btn-sm btn-wyellow fr" id="btnAddPayment"><i class="fa"></i>Add</button>
            </div> 
            <div id="jsGridPaymentList"></div>

        </div>
    </div>
    <div class="contract_information" id="divIMPArea"> 
        <div class="box-panel" style="overflow: visible;">
            <div class="table-heading">
                <h2 class="table-title">IMP</h2>
                <button type="button" class="btn btn-sm btn-wyellow fr" id="btnAddIMP"><i class="fa"></i>Add</button>
            </div> 
            <div id="jsGridIMPList"></div>

        </div>
    </div>
    <uc1:Payment runat="server" ID="Payment" />
    <uc1:IMP runat="server" ID="IMP" />
    <uc1:SearchHCP runat="server" JSOBJECTNAME="jsSearchHcp1" TYPE="I" ID="SearchHCP" />
    <div class="list-area" id="divHCRListArea">
        <button type="button" class="btn-panel-open"><i class="fa fa-plus-square"><span class="tts">Open</span></i></button>
        <div class="table-heading">
            <h2 class="table-title">공동연구자</h2>
            <button type="button" class="btn btn-sm btn-navy" id="btnContractHCRDelete"><i class="fa fa-trash-o"></i>Delete</button>
        </div>
        <div id="jsgHcr"></div>
    </div>

    <div id="layer_searchHcp" class="modal fade" tabindex="-1" role="dialog">
        <div class="modal-dialog modal-alert" role="document">
            <div class="modal-content"> 
			    <div class="box-panel" style="width:100%"> 
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
                                        <input type="text" id="txtSpecialty" class="form-control">
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
                                        <col style="width: 100%"/>
                                    </colgroup>
                                    <tbody>
                                    </tbody>
                                </table>
                            </div>
                            <!-- // .tb-body -->
                        </div>
                        <div class="btnset">
                            <button type="button" class="btn btn-sm btn-block btn-red" id="btnAttendContractHCP" >Add Attend</button>
                        </div> 
				    </div> 
				    <button type="button" class="btn-panel-close"><i class="fa fa-close"><span class="tts">Close</span></i></button>
			    </div><!-- // .box-panel -->
	 
            </div>
        </div>
    </div> 
</div> 
<script src="../../../Scripts/Pages/Medical/ContractDetail.js"></script>
