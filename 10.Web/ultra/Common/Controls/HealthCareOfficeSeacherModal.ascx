<%@ Control Language="C#" AutoEventWireup="true" CodeFile="HealthCareOfficeSeacherModal.ascx.cs" Inherits="Common_Controls_HealthCareOfficeSeacherModal" %>
<div class="modal fade hco-searcher-modal" id="divHealthCareOfficeSeacherModal" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
            </div>
            <div class="modal-body">
                <dl class="modal-form">
                    <dt>
                        <label for="lb_comment2">HCO Search</label></dt>
                    <dd style="margin-bottom: 15px; padding-top: 15px;">
                        <div class="row row-sm" style="margin-bottom: 10px;">
                            <div class="col-xs-8 col-md-4 col-lg-9">
                                <input type="text" id="txtHCOSearcher" class="form-control" />
                            </div>
                            <div class="col-xs-4 col-md-4 col-lg-3">
                                <button id="btnHCOSearcher" type="button" class="btn btn-sm btn-block btn-darkgray">Search</button>
                            </div>
                        </div>
                        <div id="jsGridHCOSearcher"></div>
                    </dd>
                    <dd>
                        <input id="txtSelectedHCO" class="form-control" type="text" readonly />
                        <!-- Ver 1.0.7 : Go-Direct -->
                        <div class="row row-sm" style="margin-bottom: 10px;">
                            <div class="form-group col-xs-5 col-md-12">
                                <div class="row row-sm">
                                    <label for="lb_row" class="lb_special col-md-4">Name(HCP)</label>
                                    <div class="col-md-8">
                                        <input type="text" id="txtDoctorName" class="form-control" />
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- Ver 1.0.7 : Go-Direct -->
                    </dd>
                </dl>
            </div>
            <div class="modal-footer">
                <button id="btnOk" type="button" class="btn btn-lg btn-red">Ok</button>
                <button type="button" class="btn btn-lg btn-navy" data-dismiss="modal">Cancel</button>
            </div>
        </div>
    </div>
</div>
