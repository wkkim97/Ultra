﻿<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModalAcceptApproval.ascx.cs" Inherits="Pages_Event_Approval_ModalAcceptApproval" %>
<div class="modal fade" id="accept-approval" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
                <h4 class="modal-title">ACCEPT APPROVAL</h4>
            </div>
            <div class="modal-body">
                <dl class="modal-form">
                    <dt>
                        <label for="lb_comment2">Comment</label></dt>
                    <dd>
                        <textarea id="txtAccepterComment" rows="4" class="form-control"></textarea></dd>
                </dl>
            </div>
            <div class="modal-footer">
                <button id="btnAcceptApproval" type="button" class="btn btn-lg btn-red">Ok</button>
                <button type="button" class="btn btn-lg btn-navy" data-dismiss="modal">Cancel</button>
            </div>
        </div>
    </div>
</div>