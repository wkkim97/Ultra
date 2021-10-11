<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModalInputComment.ascx.cs" Inherits="Pages_Event_Approval_ModalInputComment" %>

<div class="modal fade" id="input-comment" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
                <h4 class="modal-title">INPUT COMMENT</h4>
            </div>
            <div class="modal-body">
                <dl class="modal-form">
                    <dt>
                        <label for="selInputCommentType">Comment Type</label></dt>
                    <dd>
                        <select id="selInputCommentType" class="form-control" runat="server"/></dd>
                    <dt>
                        
                        <label for="txtInputCommand">Comment</label></dt>
                        
                       
                    <dd>
                        <!--version 1.0.6 Consulting Event complete 시 medical compliance 문구 변경-->
                        <div id="free_comment">
                        <textarea id="txtInputCommand" rows="4" class="form-control"></textarea>
                         </div>
                         <div id="pv_comment" style="display:none">
                            <label>
                            <input name="pv_comment_agree" id="pv_comment_agree" type="radio" value="Yes"  />아래 내용 확인
                           <br />
                                PV Attestation Form(
                                <a href='https://bayergroup.sharepoint.com/sites/021843/ultra/ulink/RD-SOP-1271%20-%20Post%20advisory%20board%20AE_PTC%20reporting%20attestation%20form.docx' target="_blank">
                                RD-SOP-1271 Post advisory board AE/PTC Reporting Attestation Form</a>)을 Safety RR이 회의록을 받은 후 3일 이내에 완성하여 본 란에 첨부하였음을 확인합니다.</label>

                        </div>




                    </dd>
                    <dd>
                        <div id="divAttachFiles_InputComment" class="attach attach-area" data-attachment-type="InputComment" style="padding-bottom: 10px; padding-top: 3px; padding-left: 0px; border-bottom-width: 0px;">
                            <div id="progress" class="progress" style="height: 7px !important; visibility: hidden; margin-bottom: 0px;">
                                <div class="progress-bar progress-bar-success">
                                </div>
                            </div>
                            <span class="btn btn-sm btn-navy fileinput-button">
                                <i class="fa fa-paperclip"></i>
                                <span>Attachment</span>
                                <!-- The file input field used as target for the file upload widget -->
                                <input class="fileupload" id="fileupload" type="file" name="files" />
                            </span>
                            <!-- The global progress bar -->
                            <!-- The container for the uploaded files -->
                            <ul class="attach-list files"></ul>
                        </div>
                    </dd>
                    <dt>To Mail <small>* Send the notification mail to whom is selected in below list.</small></dt>
                    <dd>
                        <%--<ul class="tomail-list checkbox">
                            <li>
                                <label>
                                    <input type="checkbox">
                                    <strong class="colr-point">Check All</strong></label></li>
                            <li>
                                <label>
                                    <input type="checkbox">
                                    <span>HyunHee Hwang</span></label>
                                <span>BKL-CPL-CFO-BS-GS/O2C-O2CP</span></li>
                            <li>
                                <label>
                                    <input type="checkbox">
                                    <span>KyungHee Kim</span></label>
                                <span>BKL-CPL-CFO-BS-GS/O2C-O2CP</span></li>
                            <li>
                                <label>
                                    <input type="checkbox">
                                    <span>HyeJin Noh</span></label>
                                <span>BKL-CPL-CFO-BS-GS/O2C-O2CP</span></li>
                            <li>
                                <label>
                                    <input type="checkbox">
                                    <span>JeongHo Lee</span></label>
                                <span>BKL-CPL-CFO-BS-GS/O2C-O2CP</span></li>
                        </ul>--%>
                       
                    </dd>
                </dl>
                <div id="divInputCommentApproverList"></div>
            </div>
            <div class="modal-footer">
                <button id="btnSaveInputComment" type="button" class="btn btn-lg btn-red">Save</button>
                <button type="button" class="btn btn-lg btn-navy" data-dismiss="modal">Cancel</button>
                <input type="hidden" id="hhdSelectApprover" />
            </div>
        </div>
    </div>
</div>
<!-- // #input-comment -->
