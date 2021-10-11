<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LayerWarning.ascx.cs" Inherits="Common_Controls_LayerWarning" %>
<div id="layer_warning" class="modal fade" tabindex="-1" role="dialog">
    <div class="modal-dialog modal-alert" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i aria-hidden="true" class="fa fa-times"></i></button>
            </div>
            <div class="modal-body">
                <div class="alert-message">
                    <i class="fa fa-exclamation-triangle text-warning"></i>
                    <p>
                        <strong class="colr-point">error</strong><br>
                        <span class="span-message">실패했습니다. 다시 시도해주세요.</span>
                    </p>
                </div>
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-sm btn-warning" data-dismiss="modal">Ok</button>
            </div>
        </div>
    </div>
</div>
