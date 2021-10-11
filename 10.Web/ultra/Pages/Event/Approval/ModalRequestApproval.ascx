<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModalRequestApproval.ascx.cs" Inherits="Pages_Event_Approval_ModalRequestApproval" %>
<div class="modal fade" id="approval-line" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
                <h4 class="modal-title">APPROVAL LINE</h4>
            </div>
            <div class="modal-body">
                <dl class="modal-form">
                    <dt>
                        <label for="lb_comment2">Comment</label></dt>
                    <dd>
                        <textarea id="txtRequesterComment" rows="4" class="form-control"></textarea></dd>
                    <dt>Approval Line</dt>
                    <dd class="full">
                        <table id="tblApprovalLine" class="table">
                            <thead>
                                <tr>
                                    <th></th>
                                    <th scope="col">Name</th>
                                    <th scope="col">Org Name</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </dd>
                    <dt>Recipient</dt>
                    <dd>
                        <ul class="recipaient-list">
                        </ul>
                    </dd>
                    <dt>Reviewer</dt>
                    <dd>
                        <ul class="reviewer-list">
                        </ul>
                    </dd>

                </dl>
            </div>
            <div class="modal-footer">
                <button id="btnRequestApproval" type="button" class="btn btn-lg btn-red">Request</button>
                <button type="button" class="btn btn-lg btn-navy" data-dismiss="modal">Cancel</button>
            </div>
            <input type="hidden" id="hddModalRequestSubject" value=""/>
            <input type="hidden" id="hddModalRequestStartDate" value=""/>
        </div>
    </div>
</div>
<!-- // #approval-line -->
