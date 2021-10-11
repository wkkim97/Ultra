<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LayerInformation.ascx.cs" Inherits="Common_Controls_LayerInformation" %>
<div id="layer_success" class="modal fade" tabindex="-1" role="dialog">
    <div class="modal-dialog modal-alert" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i aria-hidden="true" class="fa fa-times"></i></button>
            </div>
            <div class="modal-body">
                <div class="alert-message">
                    <i class="fa fa-check-circle text-success"></i>
                    <p>
                        <strong class="colr-point">success</strong><br>
                        <span class="span-message">성공적으로 완료되었습니다</span>
                    </p>
                </div>
            </div>
            <div class="modal-footer">
                <%--<button type="button" class="btn btn-sm btn-darkgray" data-dismiss="modal">Cancel</button>--%>
                <button type="button" class="btn btn-sm btn-success" data-dismiss="modal">Ok</button>
            </div>
        </div>
    </div>
</div>
