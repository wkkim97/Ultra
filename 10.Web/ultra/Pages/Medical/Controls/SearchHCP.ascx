<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SearchHCP.ascx.cs" Inherits="Pages_Medical_Controls_SearchHCP" %>
<style type="text/css">
    .write-area .tab-pane + .btnset{
        margin-bottom :0px !important;
    }
</style>
		<div class="write-area" id="divKoreaHCP">
			<div class="box-panel" style="width:100%"> 
                <div id="tabKoreaHCP" class="tab-pane-korea">
                    <div id="divSearchHCP" class="row row-sm">
                        <div class="col-xs-7 col-md-12">
                            <div class="row row-sm">
                                <div class="form-group col-xs-4">
                                    <label for="lb_name">Name(HCP)</label>
                                    <input type="text" id="lb_name" class="form-control" />
                                </div>
                                <div class="form-group col-xs-8">
                                    <label for="lb_hp">Hospital/Phamacy(HCO)</label>
                                    <input type="text" id="lb_hco" class="form-control" />
                                </div>
                            </div>
                        </div>
                        <div class="form-group col-xs-5 col-md-12">
                            <div class="row row-sm">
                                <label for="lb_row" class="lb_special col-md-4">Specialty</label>
                                <div class="col-xs-8 col-md-4 col-lg-5">
                                    <input type="text" id="lb_special" class="form-control">
                                </div>
                                <div class="col-xs-4 col-md-4 col-lg-3">
                                    <button id="btnSearchHCP" type="button" class="btn btn-sm btn-block btn-darkgray">Search</button>
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
                            <table id="tblEP_HCPKorea">
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
                        <button type="button" class="btn btn-sm btn-block btn-red" id="btnAttendHCP" >Add Attend</button>
                    </div> 
				</div> 
				<button type="button" class="btn-panel-close"><i class="fa fa-close"><span class="tts">Close</span></i></button>
			</div><!-- // .box-panel -->
		</div><!-- // .write-area -->
<%--<script src="../../../Scripts/Pages/Medical/SearchHCP.js"></script>--%>
