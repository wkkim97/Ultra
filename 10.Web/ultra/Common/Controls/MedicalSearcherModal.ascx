<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MedicalSearcherModal.ascx.cs" Inherits="Common_Controls_MedicalSearcherModal" %>
<div class="modal fade medical-searcher-modal" id="divMedicalSearcherModal" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
            </div>
            <div class="modal-body">
                <dl class="modal-form">
                    <dt>
                        <label for="lb_comment2">Medical Search</label></dt>
                    <dd style="margin-bottom: 15px; padding-top: 15px;">
                        <div class="row row-sm" style="margin-bottom: 10px;">
                            <div class="col-xs-8 col-md-4 col-lg-9">
                                <input type="text" id="txtMedicalSearcher" class="form-control" />
                            </div>
                            <div class="col-xs-4 col-md-4 col-lg-3">
                                <button id="btnMedicalSearcher" type="button" class="btn btn-sm btn-block btn-darkgray">Search</button>
                            </div>
                        </div>
                        <div id="jsGridMedicalSearcher"></div>
                    </dd>
                    <dd>
                        <input id="txtSelectedMedical" class="form-control" type="text" readonly />
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
