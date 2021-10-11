<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ApproveMenuBar.ascx.cs" Inherits="Common_Controls_ApproveMenuBar" %>
<script type="text/javascript" src="/ultra/Scripts/Common/Command.js"></script>
<div class="panel-heading">
    <h1 id="hspanEventTitle" class="panel-title" runat="server">Product Briefing </h1>
    <div class="btn-group btn-group-sm" role="group">
        <button id="btnRequest" type="button" class="btn btn-default" runat="server" onclick="fn_RequestClicked(); return false;">Request</button>
        <button id="btnApproval" type="button" class="btn btn-default" runat="server" onclick="fn_ApprovalClicked(); return false;">Approval</button>
        <button id="btnForwardApproval" type="button" class="btn btn-default" runat="server" onclick="fn_ForwardApprovalClicked(); return false;">Approval&amp;Forward</button>
        <button id="btnReject" type="button" class="btn btn-default" runat="server" onclick="fn_RejectClicked(); return false;">Reject</button>
        <button id="btnForward" type="button" class="btn btn-default" runat="server" onclick="fn_ForwardClicked(); return false;">Forward</button>
        <button id="btnRecall" type="button" class="btn btn-default" runat="server" onclick="fn_RecallClicked(); return false;" onserverclick="btnRecall_ServerClick">Recall</button>
        <button id="btnWithdraw" type="button" class="btn btn-default" runat="server" onclick="fn_WithdrawClicked(); return false;" onserverclick="btnWithdraw_ServerClick">Withdraw</button>
        <button id="btnRemind" type="button" class="btn btn-default" runat="server" onclick="fn_RemindClicked(); return false;" onserverclick="btnRemind_ServerClick">Remind</button>
        <button id="btnExit" type="button" class="btn btn-default" runat="server" onclick="fn_ExitClicked(); return false;">Exit</button>
        <button id="btnSave" type="button" class="btn btn-default" runat="server" onclick="fn_SaveClicked(); return false;">Save</button>
        <button id="btnInputComment" type="button" class="btn btn-default" runat="server" onclick="fn_InputCommentClicked(); return false;">Input Comment</button>
        <button id="btnReUse" type="button" class="btn btn-default" runat="server" onclick="fn_ReUseClicked(); return false;" onserverclick="btnReUse_ServerClick">ReUse</button>
        <button id="btnEventComplete" type="button" class="btn btn-default" runat="server" onclick="fn_EventCompleteClicked(); return false;">Event Complete</button>
        <button id="btnPaymentComplete" type="button" class="btn btn-default" runat="server" onclick="fn_PaymentCompleteClicked(); return false;">Payment Complete</button>
        <button id="btnCancel" type="button" class="btn btn-default" runat="server" onclick="fn_CancelClicked(); return false;">Cancel</button>
        <button id="btnEdit" type="button" class="btn btn-default" runat="server" onclick="fn_EditClicked(); return false;">Edit</button>
    </div>
</div>

