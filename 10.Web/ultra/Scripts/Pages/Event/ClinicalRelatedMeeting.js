$(function () {
	// 조회
    selectClinicalRelatedMeeting();
});

/* Clinical Related Meeting 조회 */
function selectClinicalRelatedMeeting() {
    PROCESS_ID = $("input[id$=hddProcessID]").val();
    var status = $("input[id$=hddProcessStatus]").val();
    if (PROCESS_ID && status != "Temp") {
        $.ajax({
            url: EVENT_SERVICE_URL + "/SelectClinicalRelatedMeeting/" + PROCESS_ID,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
            	displayClinicalRelatedMeeting(data);
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

function displayClinicalRelatedMeeting(data) {
    $.ajax({
    	url: MEDICAL_SERVICE_URL + "/SelectMedicalMasterList/" + $('input[id$=hhdUserID]').val() + "/" + data.IMPACT_NO,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (info) {
            setMedicalSearcher(info[0]);
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });

    $("span[id$=hspanRequester]").text(data.REQUESTER_NAME);
    $("span[id$=hspanOrganization]").text(data.ORGANIZATION_NAME);
    $("span[id$=hspanRetaentionPeriod]").text(data.RETENTION_PERIOD);
    //$("span[id$=hspanRequestedDate]").text(data.REQUEST_DATE);
    $("span[id$=hspanEventKey]").text(data.EVENT_KEY);
    $("span[id$=hspanStatus]").text(data.PROCESS_STATUS);
    $("#txtSubject").val(data.SUBJECT);
    $("#hddlCategory").val(data.CATEGORY);
    //$("#datStartTime").val(data.START_TIME.substring(0, 16));
    $(".form_StartTime").datetimepicker('update', data.START_TIME.substring(0, 16));
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
    //Category
    var category = $("#hddlCategory").val();
	//Impact No
    var impactNo = getMedicalSearcher();
    //StatTime
    var dtStart = $("#datStartTime").val();
    if (dtStart.length > 0) dtStart = dtStart + ":00";
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
    if (category.trim().length < 1) {
    	if (msgFillOut.length > 0) msgFillOut += ", ";
    	msgFillOut += "Category";
    }
    if (impactNo == null || impactNo.trim().length < 1) {
    	if (msgFillOut.length > 0) msgFillOut += ", ";
    	msgFillOut += "Impact No.";
    }
    if (dtStart < 1) {
        if (msgFillOut.length > 0) msgFillOut += ", ";
        msgFillOut += "Start Time";
    }
    if (venue.trim().length < 1) {
    	if (msgFillOut.length > 0) msgFillOut += ", ";
    	msgFillOut += "Venus(Address)";
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
    var CRMeeting = {
        PROCESS_ID: PROCESS_ID,
        SUBJECT: subject,
        EVENT_KEY: $("#hspanEventKey").text(),
        PROCESS_STATUS: status,
        REQUESTER_ID: $("input[id$=hddUserID]").val(),
        COMPANY_CODE: $("input[id$=hddCompanyCode]").val(),
        ORGANIZATION_NAME: $("[id$=hspanOrganization]").text(),
        LIFE_CYCLE: $("input[id$=hddLifeCycle]").val(),
        CATEGORY: category,
        IMPACT_NO: impactNo,
        START_TIME: dtStart,
        ADDRESS_OF_VENUE: venue,
        VENUE_SELECTION_REASON: checkedSelectionReason,
        VENUE_SELECTION_REASON_MANUAL: inputReason,
        IS_DISUSED: "N",
        CREATOR_ID: USER_ID,
        UPDATER_ID: USER_ID
    }

    var proCRMeeting = { CRMeeting : CRMeeting }

    $.ajax({
        url: EVENT_SERVICE_URL + "/MergeClinicalRelatedMeeting",
        type: "POST",
        data: JSON.stringify(proCRMeeting),
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