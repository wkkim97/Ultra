$(function () {
    var EVENT_ID;
    var PROCESS_ID;
    var USER_ID;


    /* 결재라인 */
    $('#approval-line').on('show.bs.modal', function (e) {
        EVENT_ID = $("input[id$=hddEventID]").val();
        USER_ID = $("input[id$=hddUserID]").val();
        PROCESS_ID = $("input[id$=hddProcessID]").val();

        $.ajax({
            url: EVENT_SERVICE_URL + "/SelectApprovalLine/" + EVENT_ID + "/" + USER_ID + "/" + PROCESS_ID,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                displayApprovalLine(data);
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
                $('#approval-line').modal("hide");
            },
        });
    });

    /* Modal 창에서 결재 요청 클릭 */
    $("#btnRequestApproval").click(function () {
        $('body').waitMe({
            effect: 'win8',
            text: 'Loading...'

        });
        USER_ID = $("input[id$=hddUserID]").val();
        var rows = $("#tblApprovalLine tbody tr");
        var recipients = $(".recipaient-list li");
        var reviewers = $(".reviewer-list li");

        var nextSeq = 0;
        var comment = "";
        var status = "";
        var currentApprover = "";
        var finalApprover = "";
        var approverLines = [];
        for (var i = 0; i < rows.length; i++) {
            var objApprover = $(rows[i]).data("approver");

            if (objApprover.APPROVAL_TYPE == "D" && objApprover.USER_ID == USER_ID) {
                //기안자인 경우
                status = "A";
                comment = $("#txtRequesterComment").val();
                nextSeq = objApprover.IDX + 1;
            } else {
                if (nextSeq == objApprover.IDX) {
                    //기안자 바로위
                    status = "C"; //current
                    currentApprover = objApprover.USER_ID;
                } else {
                    //나머지
                    status = "W";
                }

                if (objApprover.APPROVAL_TYPE == "A") {
                    //최종 결재자
                    finalApprover = objApprover.USER_ID;
                }
                comment = "";
            }

            var approver = {
                PROCESS_ID: PROCESS_ID,
                APPROVAL_TYPE: objApprover.APPROVAL_TYPE,
                APPROVAL_SEQ: objApprover.APPROVAL_SEQ,
                APPROVER_TYPE: objApprover.APPROVER_TYPE,
                APPROVER_ID: objApprover.USER_ID,
                APPROVER_ORG_NAME: objApprover.ORG_NAME,
                ABSENCE_APPROVER_ID: objApprover.USER_ID,
                ABSENCE_APPROVER_ORG_NAME: objApprover.ORG_NAME,
                STATUS: status,
                COMMENT: comment,
                SENT_MAIL: "N"
            }

            approverLines.push(approver);
        }

        for (var i = 0; i < recipients.length; i++) {
            var recipient = $(recipients[i]).data('recipient');
            var approver = {
                PROCESS_ID: PROCESS_ID,
                APPROVAL_TYPE: recipient.APPROVAL_TYPE,
                APPROVAL_SEQ: recipient.APPROVAL_SEQ,
                APPROVER_TYPE: recipient.APPROVER_TYPE,
                APPROVER_ID: recipient.USER_ID,
                APPROVER_ORG_NAME: recipient.ORG_NAME,
                ABSENCE_APPROVER_ID: recipient.USER_ID,
                ABSENCE_APPROVER_ORG_NAME: recipient.ORG_NAME,
                STATUS: "W",
                COMMENT: "",
                SENT_MAIL: "N"
            }
            approverLines.push(approver);
        }

        for (var i = 0; i < reviewers.length; i++) {
            var reviewer = $(reviewers[i]).data('reviewer');
            var approver = {
                PROCESS_ID: PROCESS_ID,
                APPROVAL_TYPE: reviewer.APPROVAL_TYPE,
                APPROVAL_SEQ: reviewer.APPROVAL_SEQ,
                APPROVER_TYPE: reviewer.APPROVER_TYPE,
                APPROVER_ID: reviewer.USER_ID,
                APPROVER_ORG_NAME: reviewer.ORG_NAME,
                ABSENCE_APPROVER_ID: reviewer.USER_ID,
                ABSENCE_APPROVER_ORG_NAME: reviewer.ORG_NAME,
                STATUS: "W",
                COMMENT: "",
                SENT_MAIL: "N"
            }
            approverLines.push(approver);
        }

        var processEvent = {
            PROCESS_ID: PROCESS_ID,
            EVENT_ID: EVENT_ID,
            EVENT_NAME: $("[id$=hspanEventTitle]").text(),
            SUBJECT: $("#hddModalRequestSubject").val(),
            START_DATE: $("#hddModalRequestStartDate").val(),
            //Cancel 시 event key 유지해야 하므로(by WooKyung Kim)
            //EVENT_KEY: "",
            EVENT_KEY : $("#hspanEventKey").text(),
            PROCESS_STATUS: "Request",
            COMPANY_CODE: $("input[id$=hddCompanyCode]").val(),
            REQUESTER_ID: USER_ID,
            CURRENT_APPROVER: currentApprover,
            FINAL_APPROVER: finalApprover,
            REJECTED_PROCESS_ID: $("input[id$=hddRejectedProcessId]").val(),
        }

        var approvalInfo = {

            ApproverList: approverLines,
            EventProcess: processEvent,
            EventID: EVENT_ID,
            ProcessStatus: "Request",
            UserID: USER_ID
        }

        //첨부파일을 저장한다.
        var status = $("input[id$=hddProcessStatus]").val();
        if (status == "Temp") moveToAttachFiles();

        $.ajax({
            url: EVENT_SERVICE_URL + "/InsertProcessApprove",
            type: "POST",
            data: JSON.stringify(approvalInfo),
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function () {
                $('#approval-line').modal('hide');
                $('body').waitMe('hide');
                fn_CloseTabByEventPage();
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                }).done(function () {
                    $('#approval-line').modal('hide');
                    $('body').waitMe('hide');
                    fn_CloseTabByEventPage();
                });
            },
        });

    });

    /* 결재 승인 */
    $("#btnAcceptApproval").click(function () {

        $('body').waitMe({
            effect: 'win8',
            text: 'Loading...'

        });

        EVENT_ID = $("input[id$=hddEventID]").val();
        USER_ID = $("input[id$=hddUserID]").val();
        PROCESS_ID = $("input[id$=hddProcessID]").val();

        var accept = {
            eventID: EVENT_ID,
            processID: PROCESS_ID,
            comment: $("#txtAccepterComment").val(),
            processStatus: "Processing",
            userID: USER_ID,
            approverStatus: "A"
        }

        $.ajax({
            url: EVENT_SERVICE_URL + "/UpdateProcessStatus",
            type: "POST",
            data: JSON.stringify(accept),
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $('#accept-approval').modal("hide");
                $('body').waitMe('hide');
                fn_CloseTabByEventPage();
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                }).done(function () {
                    $('#accept-approval').modal("hide");
                    $('body').waitMe('hide');
                    fn_CloseTabByEventPage();
                });
            },
        });

    });

    /* 결재 거절 */
    $("#btnRejectApproval").click(function () {

        $('body').waitMe({
            effect: 'win8',
            text: 'Loading...'

        });
        EVENT_ID = $("input[id$=hddEventID]").val();
        USER_ID = $("input[id$=hddUserID]").val();
        PROCESS_ID = $("input[id$=hddProcessID]").val();

        var accept = {
            eventID: EVENT_ID,
            processID: PROCESS_ID,
            comment: $("#txtRejecterComment").val(),
            processStatus: "Reject",
            userID: USER_ID,
            approverStatus: "R"
        }

        $.ajax({
            url: EVENT_SERVICE_URL + "/UpdateProcessStatus",
            type: "POST",
            data: JSON.stringify(accept),
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $('#reject-approval').modal("hide");
                $('body').waitMe('hide');
                fn_CloseTabByEventPage();
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                }).done(function () {
                    $('#reject-approval').modal("hide");
                    $('body').waitMe('hide');
                    fn_CloseTabByEventPage();
                });
            },
        });

    });

    // version 1.0.5 Event complete 시 Consulting Event complete 시 medical compliance 문구 추가 
    // version 1.0.6 version 1.0.6 Consulting Event complete 시 medical compliance 문구 변경-function 삭제
    $('input[name="medicalcompliance"]').change(function () {
        //var radiovalue = $(this).val();
        //if (radiovalue == "Yes") {
        //    //$("#medicalcompliance_reason").css("display", "none");
        //    $("#medicalcompliance_reason").hide();
        //    $("#txtmedicalcompliance").val("");
        //} else {
        //    //$("#medicalcompliance_reason").css("display", "inline");
        //    $("#medicalcompliance_reason").show();
        //}
    });


    /* 이벤트 완료 */
    $('#btnEventComplete').click(function () {
        var comment_free = "";

        // version 1.0.5 Event complete 시 Consulting Event complete 시 medical compliance 문구 추가 
        // version 1.0.6 version 1.0.6 Consulting Event complete 시 medical compliance 문구 변경
        if ($('#hddEventCompleteOption_medical').val() == "True") {
            var Medical = $('input[name="medicalcompliance"]:checked').val();
            //alert($("input:radio[name='medicalcompliance']").is(":checked"));
            //return;
            var medical_flag = true;
            //comment_free = "";
            comment_free = "Agree : ";
            if ($("input:radio[name='medicalcompliance']").is(":checked")) {
                medical_flag = false;                
            }
            //if (Medical == "No" && $('#txtmedicalcompliance').val().length < 1) {
            //    medical_flag = true;                
            //}
            //if ($('#txtmedicalcompliance').val().length < 1) {
            //    comment_free += "0건) : ";
            //} else {
            //    comment_free += $('#txtmedicalcompliance').val() + "건) : ";
            //}
            
            if (medical_flag) {
                fn_showInformation({
                    title: "Confirm!",
                    message: "Please confirm PV Attestation form is uploaded."
                })
                return;
            }
        }
        
        
        

        $('body').waitMe({
            effect: 'win8',
            text: 'Loading...'

        });
        EVENT_ID = $("input[id$=hddEventID]").val();
        USER_ID = $("input[id$=hddUserID]").val();
        PROCESS_ID = $("input[id$=hddProcessID]").val();

        var liFiles = $("#divAttachFiles_EventComplete .attach-list").find("li");
        var attachFile = null;
        if (liFiles.length > 0) {
            //첨부파일이 존재하는 경우
            var file = $(liFiles[0]).data("attach-file");
            attachFile = file;
        }
        comment_free +=  $("#txtEventCompleteComment").val();
        if ($("#txtEventCompleteComment").val().trim() =="") comment_free +="no comment"
        var complete = {
            eventID: EVENT_ID,
            processID: PROCESS_ID,
            commentCategory: $('select[id$=selCommentType]').val(),
            comment: comment_free,
            processStatus: "EventCompleted",
            userID: USER_ID,
            attachFile: attachFile,
        }

        $.ajax({
            url: EVENT_SERVICE_URL + "/DoEventComplete",
            type: "POST",
            data: JSON.stringify(complete),
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $('#event-complete').modal("hide");
                $('body').waitMe('hide');
                fn_CloseTabByEventPage();
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                }).done(function () {
                    $('#event-complete').modal("hide");
                    $('body').waitMe('hide');
                    fn_CloseTabByEventPage();
                });
            },
        });

    });

    /* Modal 창에서 Forward 클릭 */
    $("#btnForward").click(function () {
        var tags = $("#acbForward").textext()[0].tags()._formData;
        var receivers = [];

        var len = tags.length;
        if (len < 1) {
            fn_showInformation({
                title: "Confirm!",
                message: "Please select a user."
            })
            return;
        }

        $('body').waitMe({
            effect: 'win8',
            text: 'Loading...'

        });
        EVENT_ID = $("input[id$=hddEventID]").val();
        USER_ID = $("input[id$=hddUserID]").val();
        PROCESS_ID = $("input[id$=hddProcessID]").val();

        var comment = "";
        for (var i = 0; i < len; i++) {
            var tag = tags[i];
            var receiver = {
                PROCESS_ID: PROCESS_ID,
                APPROVAL_TYPE: "V",
                APPROVAL_SEQ: 0,
                APPROVER_TYPE: "F",
                APPROVER_ID: tag.USER_ID,
                APPROVER_ORG_NAME: tag.ORG_ACRONYM,
                ABSENCE_APPROVER_ID: tag.USER_ID,
                ABSENCE_APPROVER_ORG_NAME: tag.ORG_ACRONYM,
                STATUS: "W",
                SENT_MAIL: "N",
                COMMENT: ""
            }
            receivers.push(receiver);
            if (comment.length > 0) comment += "/"
            comment += tag.FULL_NAME;
        }

        var forward = {
            receivers: receivers,
            comment: comment,
            userID: USER_ID
        }

        $.ajax({
            url: EVENT_SERVICE_URL + "/InsertForward",
            type: "POST",
            data: JSON.stringify(forward),
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $('#forward').modal("hide");
                $('body').waitMe('hide');
                fn_CloseTabByEventPage();
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                }).done(function () {
                    $('#forward').modal("hide");
                    $('body').waitMe('hide');
                    fn_CloseTabByEventPage();
                });
            },
        });
    });

    /* Modal 창에서 Forward Approval 클릭 */
    $("#btnForwardApproval").click(function () {

        var approvarID = $('#txtForwardApprovalSelectedUser').data("user-id");
        if (approvarID.length < 1) {
            fn_showInformation({
                title: "Confirm!",
                message: "Please select a approver."
            })
            return;
        }
        $('body').waitMe({
            effect: 'win8',
            text: 'Loading...'

        });

        EVENT_ID = $("input[id$=hddEventID]").val();
        USER_ID = $("input[id$=hddUserID]").val();
        PROCESS_ID = $("input[id$=hddProcessID]").val();

        $.ajax({
            url: EVENT_SERVICE_URL + "/InsertForwardApproval/" + EVENT_ID + "/" + PROCESS_ID + "/" + USER_ID + "/" + approvarID,
            type: "GET",
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $('#forward-approval').modal("hide");
                $('body').waitMe('hide');
                fn_CloseTabByEventPage();
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                }).done(function () {
                    $('#forward-approval').modal("hide");
                    $('body').waitMe('hide');
                    fn_CloseTabByEventPage();
                });

            },
        });
    });

/* Modal 창에서 Input Comment 클릭 */
    //version 1.0.6 Consulting Event complete 시 medical compliance 문구 변경
    $('select[id$=selInputCommentType]').change(function () {
        $("#txtInputCommand").val("");
        $('input[name="pv_comment_agree"]').prop('checked', false);
        if ($('select[id$=selInputCommentType]').val() == "I7") {
            $("#free_comment").hide();
            $("#pv_comment").show();
            
        } else {
            $("#free_comment").show();
            $("#pv_comment").hide();
        }
    });
    $("#btnSaveInputComment").click(function () {
        var comment = $("#txtInputCommand").val();
        if ($("input:radio[name='pv_comment_agree']").is(":checked")) {
            comment = "Agree";
        }

        if (comment.length < 1) {
            fn_showInformation({
                title: "Confirm!",
                message: "Please enter a comment."
            })
            return;
        }
        $('body').waitMe({
            effect: 'win8',
            text: 'Loading...'

        });
        EVENT_ID = $("input[id$=hddEventID]").val();
        USER_ID = $("input[id$=hddUserID]").val();
        PROCESS_ID = $("input[id$=hddProcessID]").val();

        var liFiles = $("#divAttachFiles_InputComment .attach-list").find("li");
        var attachFile = null;
        if (liFiles.length > 0) {
            //첨부파일이 존재하는 경우
            var file = $(liFiles[0]).data("attach-file");
            attachFile = file;
        }
        var approvers = $("#hhdSelectApprover").data();
        var userids = [];
        $.each(approvers, function (index, item) {
            userids.push(item.APPROVER_ID)
        });

        var commentCategory = $('select[id$=selInputCommentType]').val();
        if (commentCategory == "0000") commentCategory = "";
        var inputComment = {
            processID: PROCESS_ID,
            commentCategory: commentCategory,
            comment: comment,
            userID: USER_ID,
            logType: "InputComment",
            attachFile: attachFile,
            sendMailApproverId: userids.join()
        }

        $.ajax({
            url: EVENT_SERVICE_URL + "/InsertInputComment",
            type: "POST",
            data: JSON.stringify(inputComment),
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $('#input-comment').modal("hide");
                $('body').waitMe('hide');
                fn_CloseTabByEventPage();
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                }).done(function () {
                    $('#input-comment').modal("hide");
                    $('body').waitMe('hide');
                    fn_CloseTabByEventPage();
                });

            },
        });

    });

    /* Modal 창에 결재라인 표시 */
    function displayApprovalLine(list) {
        $("#tblApprovalLine tbody").empty();
        $(".recipaient-list").empty();
        $(".reviewer-list").empty();
        for (var i = 0; i < list.length; i++) {
            var employee = list[i];

            var lyncMouseEvent = " onmouseover='ShowOOUI(\"" + employee.MAIL_ADDRESS + "\",this);' onmouseout='HideOOUI(\"" + employee.MAIL_ADDRESS + "\");' ";
            var spanLyncCard = "<span id='pre_user_" + employee.USER_ID + "' value='" + employee.MAIL_ADDRESS + "' userid='" + employee.USER_ID + "'>";
            spanLyncCard += "<span class='lync_status' id='pre_" + employee.USER_ID + "' " + lyncMouseEvent + " />" + employee.USER_NAME + "</span>";
            if (employee.APPROVAL_TYPE == "D" || employee.APPROVAL_TYPE == "A") {
                //Drafter Or Approver
                var row = "<tr data-approver='" + JSON.stringify(employee) + "'><td class='text-center'>"
                          + (i + 1).toString()
                          + "</td><td>" + spanLyncCard + "</td>";
                row = row + "<td>" + employee.ORG_NAME + "</td></tr>";
                $("#tblApprovalLine tbody").append(row);
            } else if (employee.APPROVAL_TYPE == "R") {
                // Recipient
                var li = "<li data-recipient='" + JSON.stringify(employee) + "'>" + spanLyncCard + "</li>"
                $(".recipaient-list").append(li);

            } else if (employee.APPROVAL_TYPE == "V") {
                // Reviewer
                var li = "<li data-reviewer='" + JSON.stringify(employee) + "'>" + spanLyncCard + "</li>"
                $(".reviewer-list").append(li);
            }
            if (typeof _nameCtrl != "undefined") {
                try{
                    if (_nameCtrl.PresenceEnabled) {
                        var status = _nameCtrl.GetStatus(employee.MAIL_ADDRESS, employee.USER_ID);
                        
                        onStatusChange('', status, employee.USER_ID);
                    }
                } catch (e) {
                    console.log("lyun error:" + e.description)
                }
            }

        }

    }

    /* Cancel Approval */
    $("#btnCancelApproval").click(function () {

        $('body').waitMe({
            effect: 'win8',
            text: 'Loading...'

        });
        var comment = $("#txtCancelComment").val();
        EVENT_ID = $("input[id$=hddEventID]").val();
        USER_ID = $("input[id$=hddUserID]").val();
        PROCESS_ID = $("input[id$=hddProcessID]").val();


        var accept = {
            eventID: EVENT_ID,
            processID: PROCESS_ID,
            comment: comment,
            processStatus: "Canceled",
            userID: USER_ID,
            approverStatus: "R"
        }

        $.ajax({
            url: EVENT_SERVICE_URL + "/UpdateProcessStatus",
            type: "POST",
            data: JSON.stringify(accept),
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $('#cancel-approval').modal("hide");
                $('body').waitMe('hide');
                fn_CloseTabByEventPage();
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                }).done(function () {
                    $('#cancel-approval').modal("hide");
                    $('body').waitMe('hide');
                    fn_CloseTabByEventPage();
                });
            },
        });
    });

    $("#btnWithdrawApproval").click(function () {

        $('body').waitMe({
            effect: 'win8',
            text: 'Loading...'

        });
        var comment = $("#txtWithdrawComment").val();
        EVENT_ID = $("input[id$=hddEventID]").val();
        USER_ID = $("input[id$=hddUserID]").val();
        PROCESS_ID = $("input[id$=hddProcessID]").val();

        var withdraw = {
            eventID: EVENT_ID,
            processID: PROCESS_ID,
            comment: comment,
            processStatus: "Withdraw",
            userID: USER_ID,
            approverStatus: "A"
        }

        $.ajax({
            url: EVENT_SERVICE_URL + "/DoWithdraw",
            type: "POST",
            data: JSON.stringify(withdraw),
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $('#withdraw-approval').modal("hide");
                $('body').waitMe('hide');
                fn_CloseTabByEventPage();
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                }).done(function () {
                    $('#withdraw-approval').modal("hide");
                    $('body').waitMe('hide');
                    fn_CloseTabByEventPage();
                });
            },
        });
    });
});

function addAdditionalReviewer(entries) {

    PROCESS_ID = $("input[id$=hddProcessID]").val();

    var additional = [];
    for (var i = 0 ; i < entries.length; i++) {
        var entry = entries[i];

        var reviewer = {
            PROCESS_ID: PROCESS_ID,
            IDX: (i + 1),
            APPROVAL_TYPE: "T",
            USER_ID: entry.USER_ID
        }

        additional.push(reviewer);
    }

    $.ajax({
        url: EVENT_SERVICE_URL + "/InsertAdditionalReviewer",
        type: "POST",
        async: false,
        data: JSON.stringify(additional),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}

/* 결재 요청 */
function fn_RequestClicked(sender, args) {

    if ($('#tabCostPlan .fa-commenting').is(':visible')) {
        fn_showWarning({
            title: "Caution!",
            message: "Please enter costplan.",
        });
        return;
    }
    if ($('#tabParticipants .fa-commenting').is(':visible')) {
        fn_showWarning({
            title: "Caution!",
            message: "Please enter participant.",
        });
        return;
    }
    if ($('#tabAgenda .fa-commenting').is(':visible')) {
        fn_showWarning({
            title: "Caution!",
            message: "Please enter agenda.",
        });
        return;
    }

    var skipApproval = $("input[id$=hddSkipApproval]").val();
    if (skipApproval == "True") {
        var tags = $("#acbReviewer").textext()[0].tags()._formData;
        addAdditionalReviewer(tags);
        saveEvent("Saved", showConfirmCompleted);
    }
    else
        saveEvent("Request", showApprovalLine);
    }

var showConfirmCompleted = function (result, subject, startDate) {
    if (result) {
        fn_confirm({
            title: "Confirm",
            message: "Complete?"
        }).done(function (result) {
            if (result) {
                saveProcessEvent(result, subject, startDate, completeEvent);
            }
        });
    }
}

var completeEvent = function completeEvent() {
    EVENT_ID = $("input[id$=hddEventID]").val();
    USER_ID = $("input[id$=hddUserID]").val();
    PROCESS_ID = $("input[id$=hddProcessID]").val();

    var accept = {
        eventID: EVENT_ID,
        processID: PROCESS_ID,
        processStatus: "Completed",
        userID: USER_ID
    }

    $.ajax({
        url: EVENT_SERVICE_URL + "/UpdateProcessCompleted",
        type: "POST",
        data: JSON.stringify(accept),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            fn_CloseTabByEventPage();
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
            fn_CloseTabByEventPage();
        },
    });
}

var showApprovalLine = function (result, subject, startDate) {
    if (result) {
        var tags = $("#acbReviewer").textext()[0].tags()._formData;
        addAdditionalReviewer(tags);

        $(".modal-title").text("APPROVAL LINE");
        $("#hddModalRequestSubject").val(subject);
        $("#hddModalRequestStartDate").val(startDate);
        $('#approval-line').modal('show');
    }
}

/* 결재 승인 */
function fn_ApprovalClicked(sender, args) {
    $('#accept-approval').modal('show');
}

/* Froward Approval */
function fn_ForwardApprovalClicked(sender, args) {
    $('#forward-approval').modal('show');
}

function fn_RejectClicked(sender, args) {
    $('#reject-approval').modal('show');
}

function fn_ForwardClicked(sender, args) {
    $('#forward').modal('show');
}

function fn_RecallClicked(sender, args) {
    fn_confirm(
    {
        title: "confirm",
        message: "Are you sure you want to recall this document?"
    })
    .done(function (result) {
        if (result) {
            DoRecall();
        }
    });
}

function fn_WithdrawClicked(sender, args) {
    $('#withdraw-approval').modal('show');
}

function fn_RemindClicked(sender, args) {
    fn_confirm(
        {
            title: "confirm",
            message: "Do you want to send the remider mail?"
        })
        .done(function (result) {
            if (result) {

                SendRemindMail();
            }
        });
}

function fn_ExitClicked(sender, args) {
    fn_CloseTabByEventPage();
}

function fn_CloseTabByEventPage() {
    var parent = window.parent;
    window.parent.fn_RemoveTab(parent.$('.top-btnset .active'));
}

/* Event 저장 */
function fn_SaveClicked(sender, args) {
    saveEvent("Saved", saveProcessEvent);
    var status = $("input[id$=hddProcessStatus]").val();
    if (status == "Temp") moveToAttachFiles();
}

var saveProcessEvent = function (result, subject, startDate, callback) {
    if (result) {
        var tags = $("#acbReviewer").textext()[0].tags()._formData;
        addAdditionalReviewer(tags);
        PROCESS_ID = $("input[id$=hddProcessID]").val();
        EVENT_ID = $("input[id$=hddEventID]").val();
        USER_ID = $("input[id$=hddUserID]").val();
        EVENT_KEY = $("#hspanEventKey").text();
        //alert(EVENT_KEY);
        var processEvent = {
            PROCESS_ID: PROCESS_ID,
            EVENT_ID: EVENT_ID,
            EVENT_NAME: $("[id$=hspanEventTitle]").text(),
            SUBJECT: subject,
            START_DATE: startDate,
            EVENT_KEY: EVENT_KEY,
            PROCESS_STATUS: "Saved",
            COMPANY_CODE: $("input[id$=hddCompanyCode]").val(),
            REQUESTER_ID: USER_ID,
            CURRENT_APPROVER: "",
            FINAL_APPROVER: "",
            REJECTED_PROCESS_ID: $("input[id$=hddRejectedProcessId]").val(),
        }

        $.ajax({
            url: EVENT_SERVICE_URL + "/InsertProcessEvent",
            type: "POST",
            data: JSON.stringify(processEvent),
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function () {
                if (callback) callback();
                //fn_CloseTabByEventPage();
                $("input[id$=hddProcessStatus]").val("Saved");
                $("span[id$=hspanStatus]").text("Saved");
                fn_showInformation({
                    title: "Saved!",
                    message: "Event Saved."
                });
                /* 저장시점에 Requester가 추가됨 */
                $("#jsGridPraticipants").jsGrid("loadData"); //Participant 재조회

                /* 상단 Tab 에 Process ID 추가 */
                window.parent.$('.top-btnset li.active > a:first-child').data('process-id', PROCESS_ID);
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });
    }
}

function fn_InputCommentClicked(sender, args) {
    $('#input-comment').modal('show');
}

function fn_ReUseClicked(sender, args) {
    fn_showInformation({
        title: "To-DO",
        message: "개발중..."
    });
}

/* Event Complete */
function fn_EventCompleteClicked(sender, args) {

    if ($('#tabParticipants .fa-commenting').is(':visible')) {
        fn_showWarning({
            title: "confirm",
            message: "참석자를 확인 바랍니다.",
        });
        return;
    }


    //version 1.0.5 Material Code 입력 방법 변경
    if ($('#tabAgenda .fa-commenting').is(':visible')) {
        fn_showWarning({
            title: "confirm",
            message: "Material Code를 확인 바랍니다.",
        });
        return;
    }

    
    /* Product가 없는경우 */
    
    if ($(".crm-product-searcher").length > 0) {
        var crmProducts = $(".crm-product-searcher").data("crm-products");
        //alert($(".crm-product-searcher").data("crm-products").length);
        if (!crmProducts || crmProducts.length < 1) {
            fn_showWarning({
                message: "Product를 확인바랍니다."
            });
            return;
        }
    }

    var crmStatus = $("input[id$=hddCRMStatus]").val().split(":");
    if (crmStatus.length == 2) {
        if (crmStatus[0] == "Y" && crmStatus[1] != "Close") {
            fn_showWarning({
                message: "CRM Status를 확인 바랍니다."
            });
            return;
        }
    }


    $('#event-complete').modal('show');
}

/* Payment Complete */

function doPaymentComplete() {
    EVENT_ID = $("input[id$=hddEventID]").val();
    USER_ID = $("input[id$=hddUserID]").val();
    PROCESS_ID = $("input[id$=hddProcessID]").val();

    var complete = {
        eventID: EVENT_ID,
        processID: PROCESS_ID,
        processStatus: "PaymentCompleted",
        userID: USER_ID,
    }

    $.ajax({
        url: EVENT_SERVICE_URL + "/DoPaymentComplete",
        type: "POST",
        data: JSON.stringify(complete),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            fn_CloseTabByEventPage();
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
            //fn_CloseTabByEventPage();
        },
    });
}

function fn_PaymentCompleteClicked(sender, args) {
    // available  Payment complete wihtout payment

    if ($('#tabPayment .fa-commenting').is(':visible')) {
         

        fn_showWarning({
            title: "Caution!",
            message: "Please enter payment or 'Plan vs Actual' Gap Check(30% 이상).",
        });
        return;
    }

    fn_confirm(
        {
            title: "confirm",
            message: "PaymentComplete will you do it?"
        })
        .done(function (result) {
            if (result) {
                doPaymentComplete();
            }
        });
}

function DoRecall() {
    EVENT_ID = $("input[id$=hddEventID]").val();
    USER_ID = $("input[id$=hddUserID]").val();
    PROCESS_ID = $("input[id$=hddProcessID]").val();

    var recall = {
        eventID: EVENT_ID,
        processID: PROCESS_ID,
        comment: "",
        userID: USER_ID,
        logType: "Recall",
        processStatus: "Recall"
    }

    $.ajax({
        url: EVENT_SERVICE_URL + "/UpdateRecall",
        type: "POST",
        data: JSON.stringify(recall),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            fn_CloseTabByEventPage();
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            }).done(function () {
                fn_CloseTabByEventPage();
            });
        },
    });
}

function SendRemindMail() {

    $('body').waitMe({
        effect: 'win8',
        text: 'Loading...'

    });
    USER_ID = $("input[id$=hddUserID]").val();
    PROCESS_ID = $("input[id$=hddProcessID]").val();

    $.ajax({
        url: COMMON_SERVICE_URL + "/MailSend/" + PROCESS_ID + "/Remind/" + USER_ID,
        type: "GET",
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $('body').waitMe('hide');
            fn_showInformation({
                title: "Information!",
                message: "Sent an email."
            })
        },
        error: function (error) {
            $('body').waitMe('hide');
            fn_showError({
                message: error.responseText
            });
        },
    });
}

function fn_CancelClicked(sender, args) {

    var processStatus = $("input[id$=hddProcessStatus]").val();

    if (processStatus == "Temp" || processStatus == "Saved") {
        fn_confirm({
            title: "Confirm",
            message: "삭제하시겠습니까? 삭제할 경우, 문서를 되살릴 수 없습니다."
        }).done(function (result) {
            if (result) {
                deleteEventProcess();
            }
        });
    } else {
        $('#cancel-approval').modal('show');
    }
}

function fn_EditClicked(sender, args) {

    var crmStatus = $("input[id$=hddCRMStatus]").val().split(":");
    if (crmStatus.length == 2) {
        if (crmStatus[0] == "Y" && crmStatus[1] == "Canceled") {
            fn_showWarning({
                message: "CRM Status를 확인 바랍니다.(현재 CRM 에서 Cancel 된 Event 입니다.)"
            });
            return;
        }
    }

    fn_confirm({
        title: "Confirm",
        message: "수정하시겠습니까? Saved 상태로 변경되어 수정이 가능하게 됩니다. 반드시 모든 참석자의 상태를 확인하십시오."
    }).done(function (result) {
        if (result) {
            changeToSaved();
        }
    });
}

function changeToSaved() {
    $('body').waitMe({
        effect: 'win8',
        text: 'Loading...'

    });

    var eventID = $("input[id$=hddEventID]").val();
    var userID = $("input[id$=hddUserID]").val();
    var processID = $("input[id$=hddProcessID]").val();

    var accept = {
        eventID: eventID,
        processID: processID,
        comment: "",
        processStatus: "Saved",
        userID: userID,
        approverStatus: "A"
    }

    $.ajax({
        url: EVENT_SERVICE_URL + "/UpdateProcessStatus",
        type: "POST",
        data: JSON.stringify(accept),
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            window.parent.fn_ReloadIFrame();
            $('body').waitMe('hide');
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            }).done(function () {
                $('body').waitMe('hide');
            });
        },
    });
}

function deleteEventProcess() {

    $('body').waitMe({
        effect: 'win8',
        text: 'Loading...'

    });
    var processID = $("input[id$=hddProcessID]").val();

    $.ajax({
        url: EVENT_SERVICE_URL + "/DeleteEventProcess/" + processID,
        type: "GET",
        contentType: "application/json; charset=utf-8",
        success: function () {
            fn_CloseTabByEventPage();
            $('body').waitMe('hide');
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            }).done(function () {
                $('body').waitMe('hide');
            });
        },
    });
}

/* 결재 공통 첨부파일 처리 */
function moveToAttachFiles() {

    var liFiles = $("#divAttachFiles_EventCommon .attach-list").find("li");
    var files = "";
    for (var i = 0; i < liFiles.length; i++) {
        files += $(liFiles[i]).data('attach-file').SavedName + ";";
    }
    if (files.length > 0)
        fn_MoveToAttach("EventCommon", files, "0"); // FileUpload.js에 있습니다.
}

function checkSavedStatus() {

    var processStatus = $("input[id$=hddProcessStatus]").val();

    if (processStatus == "Temp") return false;
    else return true;

}