$(function () {
	// 조회
    selectConsultingMeeting();

    //version 1.0.4 추가 by  wookyung kim
    $("input:radio[name=rdocompliance1]").change(function () {
        if (this.value == "Yes") {
            $("#compliance_1_reason").css("display", "inline");
        } else {
            $("#compliance_1_reason").css("display", "none");
            $("#txtReasoncompliance1").val("");
        }
    });
    $("input:radio[name=rdocompliance2]").change(function () {
        if (this.value == "Yes") {
            $("#compliance_2_reason").css("display", "inline");
        } else {
            $("#compliance_2_reason").css("display", "none");
            $("#txtReasoncompliance2").val("");
        }
    });
    
});

/* Consulting/ABM(Medical MSL) Meeting 조회 */
function selectConsultingMeeting() {
	PROCESS_ID = $("input[id$=hddProcessID]").val();
	var status = $("input[id$=hddProcessStatus]").val();
	if (PROCESS_ID && status != "Temp") {
		$.ajax({
			url: EVENT_SERVICE_URL + "/SelectConsultingMeeting/" + PROCESS_ID,
			type: "GET",
			dataType: "json",
			contentType: "application/json; charset=utf-8",
			success: function (data) {
				displayConsultingMeeting(data);
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

function displayConsultingMeeting(data) {

	$("span[id$=hspanRequester]").text(data.REQUESTER_NAME);
	$("span[id$=hspanOrganization]").text(data.ORGANIZATION_NAME);
	$("span[id$=hspanRetaentionPeriod]").text(data.RETENTION_PERIOD);
	//$("span[id$=hspanRequestedDate]").text(data.REQUEST_DATE);
	$("span[id$=hspanEventKey]").text(data.EVENT_KEY);
	$("span[id$=hspanStatus]").text(data.PROCESS_STATUS);
	$("#txtSubject").val(data.SUBJECT);
    //$("#datStartTime").val(data.START_TIME.substring(0, 16));
    //  version 1.0.6 ABM 문서에 Category  추가
    $("input:radio[name='rdoType'][value=" + data.CATEGORY + "]").prop("checked", true);

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
    //version 1.0.4 추가 by  wookyung kim
    $("input:radio[name='rdocompliance1'][value=" + data.COMPLIANCE_1 + "]").prop("checked", true);
    if (data.COMPLIANCE_1 == "Yes") $("#compliance_1_reason").css("display", "inline");
    $("input:radio[name='rdocompliance2'][value=" + data.COMPLIANCE_2 + "]").prop("checked", true);
    if (data.COMPLIANCE_2 == "Yes") $("#compliance_2_reason").css("display", "inline");
    $("#txtReasoncompliance1").val(data.COMPLIANCE_REASON_1);
    $("#txtReasoncompliance2").val(data.COMPLIANCE_REASON_2);


	var purposeAndObjective = data.PURPOSE_OBJECTIVE;
	$(".chk-purpose-objective:checkbox").each(function () {
		var include = purposeAndObjective.indexOf($(this).val());
		$(this).prop("checked", include > -1);
	});
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
	//Purpose or Objective
	var checkedPurposeObjective = "";
	var countPO = 0;
	$(".chk-purpose-objective:checkbox").each(function () {
		if (this.checked) {
			if (checkedPurposeObjective.length < 1)
				checkedPurposeObjective = $(this).val();
			else
				checkedPurposeObjective += ";" + $(this).val();
			countPO++;
		}
    });

    //version 1.0.4 추가 by  wookyung kim
    var compliance_1 = $("input:radio[name='rdocompliance1']:checked").val();
    var compliance_2 = $("input:radio[name='rdocompliance2']:checked").val();
    var compliance_reason_1 = $("#txtReasoncompliance1").val();
    var compliance_reason_2 = $("#txtReasoncompliance2").val();
    


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
	if (countSR < 3 && inputReason.length < 1) {
		if (msgFillOut.length > 0) msgFillOut += ", ";
		msgFillOut += "Reason"
	}
	if (countPO < 1) {
		if (msgFillOut.length > 0) msgFillOut += ", ";
		msgFillOut += "Purpose/Objective"
	}
	if (venue.trim().length < 1) {
	    if (msgFillOut.length > 0) msgFillOut += ", ";
	    msgFillOut += "Venue(Address)";
    }
   

    //version 1.0.4 추가 by  wookyung kim
    if ((compliance_1 == "Yes" && compliance_reason_1.trim() == "") || !$("input:radio[name='rdocompliance1']").is(":checked")) {
        if (msgFillOut.length > 0) msgFillOut += ", ";
        msgFillOut += "동일한 주제";
    }
    if ((compliance_2 == "Yes" && compliance_reason_2.trim() == "") || !$("input:radio[name='rdocompliance2']").is(":checked")) {
        if (msgFillOut.length > 0) msgFillOut += ", ";
        msgFillOut += "동일한 자문자";
    }

    //version 1.0.6 ABM 문서에 Category  추가
    if (!$("input:radio[name='rdoType']").is(":checked")) {
        if (msgFillOut.length > 0) msgFillOut += ", ";
        msgFillOut += "Event Type";
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
	var consulting = {
		PROCESS_ID: PROCESS_ID,
		SUBJECT: subject,
		EVENT_KEY: $("#hspanEventKey").text(),
		PROCESS_STATUS: status,
		REQUESTER_ID: $("input[id$=hddUserID]").val(),
		COMPANY_CODE: $("input[id$=hddCompanyCode]").val(),
		ORGANIZATION_NAME: $("[id$=hspanOrganization]").text(),
        LIFE_CYCLE: $("input[id$=hddLifeCycle]").val(),
        //version 1.0.6 ABM 문서에 Category  추가
        CATEGORY: $("input:radio[name='rdoType']:checked").val(),
		START_TIME: dtStart,
		END_TIME: dtEnd,
		ADDRESS_OF_VENUE: venue,
		VENUE_SELECTION_REASON: checkedSelectionReason,
		VENUE_SELECTION_REASON_MANUAL: inputReason,
        PURPOSE_OBJECTIVE: checkedPurposeObjective,
        COMPLIANCE_1: compliance_1,
        COMPLIANCE_2: compliance_2,
        COMPLIANCE_REASON_1: compliance_reason_1,
        COMPLIANCE_REASON_2: compliance_reason_2,
		COST_PLAN: "",
		IS_DISUSED: "N",
		CREATOR_ID: USER_ID,
		UPDATER_ID: USER_ID
	}

	var proConsulting = {
		consulting: consulting,
	}

	$.ajax({
		url: EVENT_SERVICE_URL + "/MergeConsultingMeeting",
		type: "POST",
		data: JSON.stringify(proConsulting),
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