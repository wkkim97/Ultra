<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LayerError.ascx.cs" Inherits="Common_Controls_LayerError" %>
<div id="layer_error" class="modal fade" tabindex="-1" role="dialog" style="z-index: 99999">
    <div class="modal-dialog modal-alert" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i aria-hidden="true" class="fa fa-times"></i></button>
                <h4 class="modal-title">Error</h4>
                <div style="text-align: center"><span>Please contact help desk.</span></div>
            </div>
            <div class="modal-body" style="padding-bottom: 0px; padding-top: 0px;">
                <div class="alert-message" style="height: 170px;">
                    <i class="fa fa-exclamation-triangle text-warning" style="top: 50px;"></i>
                    <p>
                        <textarea class="form-control span-message" readonly="readonly" style="width: 300px; height: 160px; font-size: 12px;">실패했습니다. 다시 시도해주세요.</textarea>
                    </p>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-sm btn-warning" data-dismiss="modal">Confirm</button>
            </div>
        </div>
    </div>
</div>
