$(function () {
	$('.form_date').datetimepicker({
		weekStart: 0,
		todayBtn: 1,
		autoclose: 1,
		todayHighlight: 1,
		startView: 2,
		forceParse: 0,
		minuteStep: 60,
		minView: 2,
		maxView: 2,
		linkFormat: "yyyy-mm-dd"
	});

    // 조회
	selectAdventisementBooth();
});

/* Adventisement Booth 조회 */
function selectAdventisementBooth() {
    PROCESS_ID = $("input[id$=hddProcessID]").val();
    var status = $("input[id$=hddProcessStatus]").val();
    if (PROCESS_ID && status != "Temp") {
        $.ajax({
        	url: EVENT_SERVICE_URL + "/SelectAdventiseBooth/" + PROCESS_ID,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
            	displayAdventisementBooth(data);
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

function displayAdventisementBooth(data) {
	$.ajax({
		url: COMMON_SERVICE_URL + "/SelectMedicalSocietyList/" + data.HOST,
		type: "GET",
		dataType: "json",
		contentType: "application/json; charset=utf-8",
		success: function (info) {
		    setHostSearcher(info[0]);
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
    //$("span[id$=hspanEventKey]").text(data.EVENT_KEY);
    $("span[id$=hspanStatus]").text(data.PROCESS_STATUS);
    $("#txtSubject").val(data.SUBJECT);
    //$("#datStartDate").val(data.START_TIME.substring(0, 10));
    $('.form_Startdate').datetimepicker('update', data.START_TIME.substring(0, 10));

    //$("#datEndDate").val(data.END_TIME.substring(0, 10));
    $('.form_Enddate').datetimepicker('update', data.END_TIME.substring(0, 10));

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
    //Start Date
    var dtStart = $("#datStartDate").val();
    if (dtStart.length > 0) dtStart = dtStart + " 00:00:00";
	//End Date
    var dtEnd = $("#datEndDate").val();
    if (dtEnd.length > 0) dtEnd = dtEnd + " 00:00:00";
	//주최
    var host = getHostSearcher();

    /* 필수입력 체크 */
    var msgFillOut = "";
    if (subject.trim().length < 1) {
        msgFillOut = "Subject";
    }
    if (dtStart < 1) {
        if (msgFillOut.length > 0) msgFillOut += ", ";
        msgFillOut += "Start Date";
    }
    if (dtEnd < 1) {
    	if (msgFillOut.length > 0) msgFillOut += ", ";
    	msgFillOut += "End Date";
    }
    if (host == null || host <= 0) {
    	if (msgFillOut.length > 0) msgFillOut += ", ";
    	msgFillOut += "주최";
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
    var Adventise = {
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
        HOST: host.toString(),
        IS_DISUSED: "N",
        CREATOR_ID: USER_ID,
        UPDATER_ID: USER_ID
    }
	
    var proSEMeeting = {
    	Adventise: Adventise,
    }

    $.ajax({
    	url: EVENT_SERVICE_URL + "/MergeAdventiseBooth",
        type: "POST",
        data: JSON.stringify(proSEMeeting),
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