<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Medical.master" AutoEventWireup="true" CodeFile="PmsMain.aspx.cs" Inherits="Pages_Medical_Pms_PmsMain" %>

<%@ Register Src="~/Common/Controls/ProductSearchModal.ascx" TagPrefix="uc1" TagName="ProductSearchModal" %>


<asp:Content ID="Content1" ContentPlaceHolderID="cpHolderHead" Runat="Server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderForm" Runat="Server">
    <div class="panel panel-topinfo">
		<div class="panel-heading">
            <h1 class="panel-title" style="display: inline">PMS List  
            <a class="btn-tooltip" style="display: inline; margin-left: 5px;" href="#" id="hrefHelp"><i class="fa fa-question-circle"></i></a>
            </h1>
            <button type="button" class="btn btn-sm btn-navy fr" id="btnExcel"><i class="fa fa-floppy-o"></i>Excel</button>
		</div>
        <div class="panel-body pd"> 
            <div class="tab-cont-area row row-md closed">
                    <div class="write-area" style="width: 100%;margin-bottom:10px;">
                        <div class="box-panel">
                            <table class="table-default">
                                <colgroup>
                                    <col style="width: 90px;">
                                    <col >
                                    <col style="width: 110px;">
                                    <col >
                                    <col style="width: 90px;">
                                    <col  >
                                </colgroup>
                                <tbody>
                                    <tr>
                                        <th scope="row">Contract No.</th>
                                        <td>
                                            <input type="text" id="txtContractNo" class="form-control" placeholder="{99999-999-99}" maxlength="12">
                                        </td> 
                                        <th scope="row">Product</th>
                                        <td>
                                            <textarea id="taProd" class="example" rows="1" style="width: 100%;"></textarea>
                                        </td>
                                        <th scope="row">재심사</th>
                                        <td>
                                            <div class="radio">
                                                <label>
                                                    <input name="lb_role" type="radio" value="Y" >
                                                    Y</label>
                                                <label>
                                                    <input name="lb_role" type="radio" value="N" checked=""/>
                                                    N</label>
                                            </div>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">HCP</th>
                                        <td colspan="5">
                                            <textarea id="taDoc" class="example" rows="1" style="width: 100%;"></textarea>
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">단가/건</th>
                                        <td>
                                            <input type="text" class="form-control" id="txtConst" value-field="#hhdConst" style="text-align: right;"/>
                                            <input type="text" id="hhdConst" />

                                        </td>
                                        <th scope="row">건수</th>
                                        <td>
                                            <input type="text" class="form-control" id="txtNumber"  value-field="#hhdNumber" style="text-align: right;"/>
                                            <input type="hidden" id="hhdNumber" />
                                        </td>
                                        <th scope="row">지급비용</th>
                                        <td>
                                            <input type="text" class="form-control" id="txtAmount" style="text-align: right;"/>
                                            <input type="text" id="hhdAmount" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">비용지급일자</th>
                                        <td>
                                            <div class="input-group date form_datetime" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtDate">
                                                <input type="text" id="dtDate" class="form-control" />
                                                <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                                            </div>
                                        </td>
                                        <th scope="row">비용지급 방법</th>
                                        <td>
                                            <div class="radio">
                                                <label>
                                                    <input name="lb_role1" type="radio" value="PO">PO(SRM)</label>
                                                <label>
                                                    <input name="lb_role1" type="radio" value="NPO">Non-PO(Your-Doces)</label>
                                            </div>
                                        </td>
                                        <th scope="row">Evidence No.</th>
                                        <td>
                                            <input type="text" class="form-control" id="txtEvidenceId">
                                        </td>
                                    </tr>
                                    <tr>
                                        <th scope="row">Comment</th>
                                        <td colspan="5">
                                            <textarea class="form-control" id="taComment"></textarea></td>
                                    </tr>
                                </tbody>
                            </table>
                            <div class="btnset">
                                <button type="button" class="btn btn-sm btn-navy fl" id="btnDelete"><i class="fa fa-trash-o"></i>Delete</button>
                                <button type="button" class="btn btn-sm btn-red fr" id="btnSave"><i class="fa fa-floppy-o"></i>SAVE</button>
                                <button type="button" class="btn btn-sm btn-wyellow fr" id="btnNew"><i class="fa"></i>New</button>
                            </div>
                 
                            <button type="button" class="btn-panel-close"><i class="fa fa-close"><span class="tts">Close</span></i></button>
                        </div>
                        <!-- // .box-panel -->
                    </div>
                    <div class="list-area" style="width: 100%">
                        <button type="button" class="btn-panel-open"><i class="fa fa-plus-square"><span class="tts">Open</span></i></button>
                        <div id="jsGridList"></div>
                    </div>
                   
                </div>
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
     <iframe id="iframeFileDown" width="0" height="0"></iframe>
    <uc1:ProductSearchModal runat="server" ID="ProductSearchModal" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderBottom" Runat="Server">
    <script src="/Ultra/Scripts/Pages/Medical/Pms.js"></script>
    <input type="hidden" id="hhdUserID" runat="server" /> 
    <input type="hidden" id="hhdIdx" /> 
    <script src="../../../Scripts/Common/ProductSearch.js"></script>
    <script src="../../../Scripts/JQuery/cookie.js"></script>
</asp:Content>

