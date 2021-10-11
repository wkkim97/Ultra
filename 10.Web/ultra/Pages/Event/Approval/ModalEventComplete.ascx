<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ModalEventComplete.ascx.cs" Inherits="Pages_Event_Approval_ModalCompleteEvent" %>
<div class="modal fade" id="event-complete" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
                <h4 class="modal-title">EVENT COMPLETE</h4>
            </div>
            <div class="modal-body">
                <div id="divEventCompleteComment">
                    <dl class="modal-form">
                        <dt>
                            <label for="lb_commentType">Comment Type</label></dt>
                        <dd>
                            <select id="selCommentType" class="form-control" runat="server" disabled/></dd>

                        <dt>
                            <label for="lb_comment2">Comment</label></dt>
                        <dd>
                            <textarea id="txtEventCompleteComment" rows="4" class="form-control"></textarea></dd>
                        <!-- version 1.0.5 Consulting Event complete 시 medical compliance 문구 추가 -->
                        <!--
                        <div id="medical_area_old" runat="server" visible="false">
                        <dt>
                            <label for="lb_comment_medical">PV 보고 대상 케이스 발생 여부</label></dt>
                        <dd>
                            <div class="radio" style="margin:0px;padding: 0px">                        
                                <label>
                                    <input name="medicalcompliance_old" type="radio" value="Yes"  />PV 보고 대상 케이스가 없음을 확인하였음</label>
                                <label>
                                    <input name="medicalcompliance_old" type="radio" value="No" />PV 보고 대상 케이스가 확인되어 PV 팀에 보고함</label>                        
                            </div>
                            <div id="medicalcompliance_reason_old" style="display:none">
                                <strong>보고된 Case 갯수 : </strong><br />
                                <input id="txtmedicalcompliance_old" type="text" class="form-control"  />
                            </div>
                            <p style="text-align:left;color:indianred">
                            자문미팅 관련 모든 자료 (발표 슬라이드, 토의 내용을 포함한 회의록 등)를 검토하였으며, 이러한 사실을 본인의 상사에게 전달하였습니다.
                            </p>
                        </dd>
                        </div>
                        -->
                        <!--version 1.0.6 Consulting Event complete 시 medical compliance 문구 변경-->
                        <div id="medical_area" runat="server" visible="false">
                        <dt>
                            <label for="lb_comment_medical">
                                <a href='https://bayergroup.sharepoint.com/sites/021843/ultra/ulink/RD-SOP-1271%20-%20Post%20advisory%20board%20AE_PTC%20reporting%20attestation%20form.docx' target="_blank">[RD-SOP-1271]Post advisory board AE/PTC Reporting Attestation Form</a></label></dt>
                        <dd>
                            <div class="radio" style="margin:0px;padding: 0px">   
                                <p>
                                우측 상단 ‘Input Comment’란의 drop down list중 ‘PV Attestation Form’을 선택하여 완성된 Post advisory board AE/PTC Reporting Attestation Form
의 스캔본을 반드시 첨부하여 주시기 바랍니다. 본 스캔본은 Safety RR이 회의록을 받은 후 3일 내에 첨부되어야 합니다.
                                </p>
                                <label>
                                    <input name="medicalcompliance" type="radio" value="Yes"  />위 내용 확인 되었음.</label>
                                                       
                            </div>
                           
                            
                        </dd>
                        </div>

                    </dl>
                </div>
                <div id="divAttachFiles_EventComplete" class="attach attach-area" data-attachment-type="EventCompleted" style="padding-bottom: 10px; padding-top: 3px;">
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
               
            </div>
            <div class="modal-footer" style="padding-top:0px">
                <p style="text-align:left;color:indianred">
                    <!--version 1.0.6 Consulting Event complete 시 medical compliance 문구 변경
                        이벤트를 진행하는 과정 및 회의록 검토 과정에서 인지한 ‘바이엘 제품 사용 후 발생한 이상사례 또는 바이엘 제품에 대한 안전성 정보’가 모두 PV 팀으로 보고 되었음을 확인하였습니다.
                        -->
                    
                    이벤트를 진행하는 과정 및 회의록 검토 과정에서 인지한 바이엘 제품 사용 후 발생한 이상사례 또는 바이엘 제품에 대한 안전성 정보가 모두 PV팀으로 보고 되었음을 Safety reporting responsible 과 확인하였고, 이에 대해 ‘Post advisory board AE/PTC Reporting Attestation Form’을 작성하여 ultra 내 스캔본을 첨부하였음을 확인하였습니다
                </p>
                <button id="btnEventComplete" type="button" class="btn btn-lg btn-red">Ok</button>
                <button type="button" class="btn btn-lg btn-navy" data-dismiss="modal">Cancel</button>
            </div>
        </div>
    </div>
</div>
