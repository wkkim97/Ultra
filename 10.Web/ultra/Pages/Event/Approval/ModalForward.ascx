<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModalForward.ascx.cs" Inherits="Pages_Event_Approval_ModalForward" %>
<div class="modal fade" id="forward" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
                <h4 class="modal-title">FORWARD</h4>
            </div>
            <div class="modal-body">
                <dl class="modal-form">
                    <dt>
                        <label for="lb_comment2">User</label></dt>
                    <dd style="margin-bottom: 15px;">
                        <textarea id="acbForward" class="example" rows="1" style="width: 100%"></textarea>
                    </dd>
                </dl>
            </div>
            <div class="modal-footer">
                <button id="btnForward" type="button" class="btn btn-lg btn-red">Forward</button>
                <button type="button" class="btn btn-lg btn-navy" data-dismiss="modal">Cancel</button>
            </div>
        </div>
    </div>
    <script type="text/javascript" src="/ultra/Scripts/Master/Forward.js"></script>
</div>
