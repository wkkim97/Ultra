<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Medical.master" AutoEventWireup="true" CodeFile="HCPSearch.aspx.cs" Inherits="Pages_HCP_Search" %>


<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderForm" Runat="Server">
     <input type="hidden" id="hhdUserID" runat="server" />
    <input type="hidden" id="hhdUserName" runat="server" />
    <input type="hidden" id="userType" runat="server" />
    <input type="hidden" id="onekey_view"  value="Y" />
    <link href="/ultra/Styles/waitMe/waitMe.css" rel="stylesheet" />    
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
                                    <col style="width: 25%" />
                                    <col style="width: 20%" />
                                    <col style="width: 30%" />
                                    <col style="width: 25%" />
                                </colgroup> 
                                <tr>
                                    <th>
                                        HCP
                                    </th>
                                    <th>Code</th>
                                    <th>HCP</th>
                                    <th>Specialty</th>
                                </tr>
                            </table>
                        </div>
                        <!-- // .tb-head -->
                        <div class="tb-body" style="min-height:100px;height:auto">
                            <table id="tblEP_HCPKorea">
                                <colgroup>
                                    <col style="width: 25%" />
                                    <col style="width: 20%" />
                                    <col style="width: 30%" />
                                    <col style="width: 25%" />
                                </colgroup>
                                <tbody>
                                </tbody>
                            </table>
                        </div>
                        <!-- // .tb-body -->
                    </div>
                   
				</div> 
				<button type="button" class="btn-panel-close"><i class="fa fa-close"><span class="tts">Close</span></i></button>
			</div><!-- // .box-panel -->
		
       







        </div>
    <%} %>
    <script>

        var search_hcp = new SearchHCP();
        search_hcp.Init();

    </script>
    
</asp:Content>

