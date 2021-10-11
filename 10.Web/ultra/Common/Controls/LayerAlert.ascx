<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LayerAlert.ascx.cs" Inherits="Common_Controls_LayerAlert" %>
<div id="layer_alert" class="modal fade" tabindex="-1" role="dialog">
    <div class="modal-dialog modal-alert" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i aria-hidden="true" class="fa fa-times"></i></button>
            </div>
            <div class="modal-body">
                <div class="alert-message">
                    <i class="fa fa-minus-circle text-danger"></i>
                    <p>
                        <strong class="colr-point">alert</strong><br>
                        <span class="span-message">취소하시겠습니까?</span>
                    </p>
                </div>
            </div>
            <div class="modal-footer">
                <button id="btnConfirm" type="button" class="btn btn-sm btn-danger">Yes</button>
                <button id="btnCancel" type="button" class="btn btn-sm btn-darkgray">No</button>
            </div>
        </div>
    </div>
</div>
