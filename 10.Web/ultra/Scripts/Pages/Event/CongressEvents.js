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

	$("#hddlCongressType").on("change", changeType);
	$(".krpia_number").on("keyup", fn_ValidationKRPIA);

	// 조회
	selectCongressEvents();
});

// 영문, 숫자, ., - 만 입력 확인
function fn_ValidationKRPIA(e) {
    if (!(e.keyCode >= 37 && e.keyCode <= 40)) {
        var v = $(this).val();
        if (v.match(/[^a-z0-9-.]/gi))
            alert("'-', '.' 를 제외한 특수문자, 한글은 입력 할 수 없습니다.");
        $(this).val(v.replace(/[^a-z0-9-.]/gi, ''));
    }
}

// 타입 변경 시 컬럼 변경
function changeType() {
    //International Participants Sponsorship (국제학술대회 참가자지원) 일 경우 Venue 항목 필수
    //국내개최 일 경우 Host 항목 필수
    if ($("#hddlCongressType").val() == "IPS") {
        $("#spanVenue").css("display", "");
        $("#hdivHostText").css("display", "");
        $("#hdivHostSearch").css("display", "none");
    }
    else {
        $("#spanVenue").css("display", "none");
        $("#hdivHostText").css("display", "none");
        $("#hdivHostSearch").css("display", "");
    }

    // PS(Participants Sponsorship 참가자지원) 항목일 경우 참가자 수 표시
    if ($("#hddlCongressType").val().indexOf("PS") > 0)
        $("#htrParticipantCount").css("display", "");
    else
        $("#htrParticipantCount").css("display", "none");
}

/* Clinical Related Meeting 조회 */
function selectCongressEvents() {
	PROCESS_ID = $("input[id$=hddProcessID]").val();
	var status = $("input[id$=hddProcessStatus]").val();
	if (PROCESS_ID && status != "Temp") {
		$.ajax({
			url: EVENT_SERVICE_URL + "/SelectCongressEvent/" + PROCESS_ID,
			type: "GET",
			dataType: "json",
			contentType: "application/json; charset=utf-8",
			success: function (data) {
				displayCongressEvents(data);
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

function displayCongressEvents(data) {
	$("#hddlCongressType").val(data.TYPE);
	changeType();

	if ($("#hdivHostText").css("display") == "none") {
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
	}
	else {
	    $("#htxtHost").val(data.HOST);
	}

	$("span[id$=hspanRequester]").text(data.REQUESTER_NAME);
	$("span[id$=hspanOrganization]").text(data.ORGANIZATION_NAME);
	$("span[id$=hspanRetaentionPeriod]").text(data.RETENTION_PERIOD);
	//$("span[id$=hspanRequestedDate]").text(data.REQUEST_DATE);
	$("span[id$=hspanEventKey]").text(data.EVENT_KEY);
	$("span[id$=hspanStatus]").text(data.PROCESS_STATUS);
	$("#txtSubject").val(data.SUBJECT);
    //$("#datStartDate").val(data.START_TIME.substring(0, 10));
	$('.form_Startdate').datetimepicker('update', data.START_TIME.substring(0, 10));
    //$("#datEndDate").val(data.END_TIME.substring(0, 10));
	$('.form_Enddate').datetimepicker('update', data.END_TIME.substring(0, 10));
	$("#txtVenue").val(data.VENUE);
	var selectionPayment = data.PAYMENT_TO;
	$("input[name$=radoPaymentTo]").each(function () {
		$(this).prop("checked", ($(this).val() == selectionPayment));
	});
	$("#txtParticipantCount").val(data.PARTICIPANT_COUNT);
	$("#txtKRPIANum").val(data.KRPIA_NUMBER);

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

	var type = $("#hddlCongressType").val();
	//Subject
	var subject = $("#txtSubject").val();
	//StartDate
	var dtStart = $("#datStartDate").val();
	if (dtStart.length > 0) dtStart = dtStart + " 00:00:00";
	//EndDate
	var dtEnd = $("#datEndDate").val();
	if (dtEnd.length > 0) dtEnd = dtEnd + " 00:00:00";
	//Venue
	var Venue = $("#txtVenue").val();
    //주최
	var Host = ($("#hdivHostText").css("display") == "none" ? getHostSearcher() : $("#htxtHost").val());
	//Payment to
	var Paymentto = $("input[name$=radoPaymentTo]:checked").val();
	//참가자 지원수
	var ParticipantCount = ($("#htrParticipantCount").css("display") == "none"? "0": $("#txtParticipantCount").val());
	//KRPIA 사전 신고 번호
	var KRPIANum = $("#txtKRPIANum").val();

	/* 필수입력 체크 */
	var msgFillOut = "";
	if (type.trim().length < 1) {
		msgFillOut = "Congress Type";
	}
	if (subject.trim().length < 1) {
		msgFillOut = "학술대회명";
	}
	if (dtStart < 1) {
		if (msgFillOut.length > 0) msgFillOut += ", ";
		msgFillOut += "Start Date";
	}
	if (dtEnd < 1) {
		if (msgFillOut.length > 0) msgFillOut += ", ";
		msgFillOut += "End Date";
	}
	if (Venue == null || Venue.trim().length < 1) {
		if (msgFillOut.length > 0) msgFillOut += ", ";
		msgFillOut += "Venus (국가와 도시를 반드시 입력해주세요.)";
	}
	if (Host == null || Host <= 0) {
		if (msgFillOut.length > 0) msgFillOut += ", ";
		msgFillOut += "주최";
	}
	if (Paymentto.trim().length < 1) {
		if (msgFillOut.length > 0) msgFillOut += ", ";
		msgFillOut += "Payment To";
	}
	if ($("#htrParticipantCount").css("display") != "none" && ParticipantCount.trim().length < 1) {
		if (msgFillOut.length > 0) msgFillOut += ", ";
		msgFillOut += "참가자 지원수";
	}
	if (KRPIANum.trim().length < 1) {
		if (msgFillOut.length > 0) msgFillOut += ", ";
		msgFillOut += "KRPIA 사전 신고 번호";
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
	var Congress = {
		PROCESS_ID: PROCESS_ID,
		SUBJECT: subject,
		EVENT_KEY: $("#hspanEventKey").text(),
		PROCESS_STATUS: status,
		REQUESTER_ID: $("input[id$=hddUserID]").val(),
		COMPANY_CODE: $("input[id$=hddCompanyCode]").val(),
		ORGANIZATION_NAME: $("[id$=hspanOrganization]").text(),
		LIFE_CYCLE: $("input[id$=hddLifeCycle]").val(),
        TYPE: type,
		START_TIME: dtStart,
		END_TIME: dtEnd,
		VENUE: Venue,
		HOST: Host.toString(),
		PAYMENT_TO: Paymentto,
		PARTICIPANT_COUNT: ParticipantCount,
		KRPIA_NUMBER: KRPIANum,
		IS_DISUSED: "N",
		CREATOR_ID: USER_ID,
		UPDATER_ID: USER_ID
	}

	var proCongress = { Congress: Congress }

	$.ajax({
		url: EVENT_SERVICE_URL + "/MergeCongressEvent",
		type: "POST",
		data: JSON.stringify(proCongress),
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