<%@ Control Language="C#" AutoEventWireup="true" CodeFile="FileUploader.ascx.cs" Inherits="Common_Controls_FileUploader" %>

<div class="attach">
    <div class="attach-heading">
        <h3 class="attach-title">Attachment</h3>
        <span class="btn btn-sm btn-navy fileinput-button">
            <i class="fa fa-paperclip"></i>
            <span>Attachment</span>
            <!-- The file input field used as target for the file upload widget -->
            <input id="fileupload" type="file" name="files[]" multiple />
        </span>
        <!-- The global progress bar -->
        <!-- The container for the uploaded files -->
    </div>
    <div id="progress" class="progress" style="height: 7px !important; display: none">
        <div class="progress-bar progress-bar-success">
        </div>
    </div>
    <div id="files" class="files">
        <ul class="attach-list"></ul>
    </div>
</div>
<input type="hidden" id="hddFU_AttachType" runat="server" value="" />
<input type="hidden" id="hddFU_IsMultiple" runat="server" value="" />
