$(function () {
    // 조회
    selectEmployeeMedicalTraining();
});

/* Product Seminar 조회 */
function selectEmployeeMedicalTraining() {
    PROCESS_ID = $("input[id$=hddProcessID]").val();
    var status = $("input[id$=hddProcessStatus]").val();
    if (PROCESS_ID && status != "Temp") {
        $.ajax({
        	url: EVENT_SERVICE_URL + "/SelectEmployeeTraining/" + PROCESS_ID,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                displayEmployeeMedicalTraining(data);
                fn_SelectEventAttachFiles(PROCESS_ID);//첨부파일 조회 (FileUpload.js)
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });
    }
}


function displayEmployeeMedicalTraining(data) {

    $("span[id$=hspanRequester]").text(data.REQUESTER_NAME);
    $("span[id$=hspanOrganization]").text(data.ORGANIZATION_NAME);
    $("span[id$=hspanRetaentionPeriod]").text(data.RETENTION_PERIOD);
    //$("span[id$=hspanRequestedDate]").text(data.REQUEST_DATE);
    $("span[id$=hspanEventKey]").text(data.EVENT_KEY);
    $("span[id$=hspanStatus]").text(data.PROCESS_STATUS);
    $("#txtSubject").val(data.SUBJECT);
    //$("#datStartTime").val(data.START_TIME.substring(0, 16));
    $(".form_StartTime").datetimepicker('update', data.START_TIME.substring(0, 16));
    //$("#datEndTime").val(data.END_TIME.substring(0, 16));
    $(".form_EndTime").datetimepicker('update', data.END_TIME.substring(0, 16));
    $("#txtAddress").val(data.ADDRESS_OF_VENUE);
    var selectionReason = data.VENUE_SELECTION_REASON;
    $(".chk-venue-selection-reason:checkbox").each(function () {
        var include = selectionReason.indexOf($(this).val());
        $(this).prop("checked", include > -1);
    });

    $("#txtReason").val(data.VENUE_SELECTION_REASON_MANUAL);
    //version 1.0.4 Address re-size
    $("#txtAddress").change();
}

/*
    Event 저장
    Request인경우 callback function이 존재
*/
function saveEvent(action, callback) {

    var status = $("[id$=hspanStatus]").text();

    /*
        action이 Request이고 현재 Status가 없으면 신규 문서
    */
    if (action == "Request") {
        if (!status) status = "Temp";
    }
    else
        status = "Saved";

    //Subject
    var subject = $("#txtSubject").val();
    //StartTime
    var dtStart = $("#datStartTime").val();
    if (dtStart.length > 0) dtStart = dtStart + ":00";
	//EndTime
    var dtEnd = $("#datEndTime").val();
    if (dtEnd.length > 0) dtEnd = dtEnd + ":00";
    //Venue Address
    var venue = $("#txtAddress").val();
    //Venue Selection Reason
    var checkedSelectionReason = "";
    var countSR = 0;
    $(".chk-venue-selection-reason:checkbox").each(function () {
        if (this.checked) {
            if (checkedSelectionReason.length < 1)
                checkedSelectionReason = $(this).val();
            else
                checkedSelectionReason += ";" + $(this).val();
            countSR++;
        }
    });
    var inputReason = $("#txtReason").val();

    /* 필수입력 체크 */
    var msgFillOut = "";
    if (subject.trim().length < 1) {
        msgFillOut = "Subject";
    }
    if (dtStart < 1) {
        if (msgFillOut.length > 0) msgFillOut += ", ";
        msgFillOut += "Start Time";
    }
    if (dtEnd < 1) {
        if (msgFillOut.length > 0) msgFillOut += ", ";
        msgFillOut += "End Time";
    }
    if (venue.trim().length < 1) {
        if (msgFillOut.length > 0) msgFillOut += ", ";
        msgFillOut += "Venue(Address)";
    }
    if (countSR < 3 && inputReason.length < 1) {
        if (msgFillOut.length > 0) msgFillOut += ", ";
        msgFillOut += "Reason"
    }

    if (msgFillOut.length > 0) {
        fn_showInformation({
            title: "Please fill out below fields.",
            message: msgFillOut
        })
        return;
    }

    var PROCESS_ID = $("input[id$=hddProcessID]").val();
    var USER_ID = $("input[id$=hddUserID]").val();
    //저장
    var employee = {
        PROCESS_ID: PROCESS_ID,
        SUBJECT: subject,
        EVENT_KEY: $("#hspanEventKey").text(),
        PROCESS_STATUS: status,
        REQUESTER_ID: $("input[id$=hddUserID]").val(),
        COMPANY_CODE: $("input[id$=hddCompanyCode]").val(),
        ORGANIZATION_NAME: $("[id$=hspanOrganization]").text(),
        LIFE_CYCLE: $("input[id$=hddLifeCycle]").val(),
        START_TIME: dtStart,
        END_TIME: dtEnd,
        ADDRESS_OF_VENUE: venue,
        VENUE_SELECTION_REASON: checkedSelectionReason,
        VENUE_SELECTION_REASON_MANUAL: inputReason,
        COST_PLAN: "",
        IS_DISUSED: "N",
        CREATOR_ID: USER_ID,
        UPDATER_ID: USER_ID
    }

    var proEmployee = {
    	employee: employee
    }

    $.ajax({
    	url: EVENT_SERVICE_URL + "/MergeEmployeeTraining",
        type: "POST",
        data: JSON.stringify(proEmployee),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            callback(true, subject, dtStart);
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
            callback(false);
        },
    });
}