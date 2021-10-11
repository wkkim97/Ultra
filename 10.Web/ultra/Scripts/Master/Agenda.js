/// <reference path="../../Pages/Event/Print/PrintYourDocesCover.aspx" />
/*
    AgendaRole 구분 => 1:좌장 2:패널 3:강연 4:자문
*/

$(function () {

    selectAgenda(); //Agenda 조회

    setAgendaRole(); //Layer의 Role설정

    //version 1.0.5 Material Code 입력 방법 변경
    $('input[name="radmaterialcode"]').change(function () {
        var radiovalue = $(this).val();
        if (radiovalue == "Yes") {
            //지금입력
            $("#txtAgendaRoleMaterialCode").show();
            $("#txtAgendaRoleMaterialCode").val("");
        } else {
            //추후입력
            $("#txtAgendaRoleMaterialCode").hide();
            $("#txtAgendaRoleMaterialCode").val("추후입력");
        }
    });



    /* Agenda 저장 */
    $("#btnSaveAgenda").click(function () {

        if (!checkSavedStatus()) {
            fn_showWarning({
                title: "Confirm",
                message: "Please save the event."
            }).done(function () {
                $('#tabEventMaster a:first').tab('show');
            });
        } else {

            EVENT_ID = $("input[id$=hddEventID]").val();
            USER_ID = $("input[id$=hddUserID]").val();
            PROCESS_ID = $("input[id$=hddProcessID]").val();

            var agendaIDX = $("#hddAgendaIDX").val();
            var day = $("#optAgendaDay").val();
            var hour = $("#optAgendaStartHour").val();
            var minute = $("#optAgendaStartMinute").val();
            var duration = fn_RemoveComma($("#numAgendaDuration").val());
            var subject = $("#txtAgendaSubject").val();

            if (!duration) duration = 0;
            else duration = parseInt(duration);

            var msgFillOut = "";
            if (subject.trim().length < 1) {
                msgFillOut = "Subject";
            }
            if (duration < 1) {
                if (msgFillOut.length > 0) msgFillOut += ", ";
                msgFillOut += "Duration";
            }
            if (msgFillOut.length > 0) {
                fn_showInformation({
                    title: "Please fill out below fields.",
                    message: msgFillOut
                })
                return;
            }

            var agenda = {
                EVENT_ID: EVENT_ID,
                PROCESS_ID: PROCESS_ID,
                AGENDA_IDX: agendaIDX,
                DAY: day,
                START_TIME: hour + ":" + minute,
                DURATION: duration,
                SUBJECT: subject,
                IS_DELETED: "N",
                CREATOR_ID: USER_ID,
                UPDATER_ID: USER_ID
            }


            $.ajax({
                url: EVENT_SERVICE_URL + "/MergeAgenda",
                type: "POST",
                data: JSON.stringify(agenda),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    selectAgenda();
                    clearAgendaInputArea(); //입력화면 초기화
                },
                error: function (error) {
                    fn_showError({
                        message: error.responseText
                    });
                },
            });
        }
    });

    /* Agenda 삭제 */
    $("#btnDeleteAgenda").click(function () {

        USER_ID = $("input[id$=hddUserID]").val();
        PROCESS_ID = $("input[id$=hddProcessID]").val();
        var agendaIDX = $("#hddAgendaIDX").val();

        if (parseInt(agendaIDX) < 1) {
            fn_showWarning({
                title: "Confirm",
                message: "Please select an agenda."
            })
            return;
        }

        fn_confirm()
            .done(function (result) {
                if (result) {
                    $.ajax({
                        url: EVENT_SERVICE_URL + "/DeleteAgenda/" + PROCESS_ID + "/" + agendaIDX + "/" + USER_ID,
                        type: "GET",
                        //dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            selectAgenda();
                            $("#hddAgendaIDX").val("0");
                            $("#optAgendaDay").val("1");
                            $("#numAgendaDuration").val("");
                            $("#txtAgendaSubject").val("");

                        },
                        error: function (error) {
                            fn_showError({
                                message: error.responseText
                            });
                        },
                    });
                }
            });
    });

    /* Reset */
    $("#btnSaveReset").click(function () {
        $("#hddAgendaIDX").val("0");
        clearAgendaInputArea();
    });

    /* AgendaRole 저장 */
    $("#btnSaveAgendaRole").click(function () {
        PROCESS_ID = $("input[id$=hddProcessID]").val();
        USER_ID = $("input[id$=hddUserID]").val();
        var agendaIDX = $("#hddModalAgendaIDX").val();
        var agendaRoleIDX = $("#hddAgendaRoleIDX").val();
        var participantIDX = $("#optAgendaRoleParticipant option:selected").val();
        var participantText = $("#optAgendaRoleParticipant option:selected").text();
        var participantType = $("#optAgendaRoleParticipant option:selected").data("participant-type");
        var participantCode = $("#optAgendaRoleParticipant option:selected").data("participant-code");
        var roleType = $(":radio[name$='lb_role_type']:checked").val();
        var role = $("select[id$=optAgendaRole] option:selected").val();
        var inputes = $("#divCriteria").find("input");
        var len = inputes.length;

        var criteria = "";
        var checkedCount = 0;
        for (var i = 0; i < len; i++) {
            var chk = $(inputes[i]).prop("checked");
            if (chk) {
                chk = $(inputes[i]).closest("label")[0].innerText.trim();
                checkedCount++;

                if (criteria.length > 0) {
                    criteria = criteria + "," + chk;
                } else {
                    criteria = chk;
                }
            }

        }

        var reason = $("#txtAgendaRoleReason").val();
        var amount = fn_RemoveComma($("#numAgendaRoleAmount").val());
        if (amount) amount = parseInt(amount);
        else amount = 0;

        var materialCode = $("#txtAgendaRoleMaterialCode").val().trim();

        var msgFillOut = "";

        if (!roleType) {
            if (msgFillOut.length > 0) msgFillOut += ", ";
            msgFillOut += "Role Type";
        }
        if (participantIDX == "0") {
            if (msgFillOut.length > 0) msgFillOut += ", ";
            msgFillOut += "Name";
        }

        if (participantType != "Employee" && checkedCount < 2) {
            if (msgFillOut.length > 0) msgFillOut += ", ";
            msgFillOut += "Criteria";
        }

        //version 1.0.5 Material Code 입력 방법 변경

        if (materialCode.length < 10 && materialCode != "추후입력") {
            if (msgFillOut.length > 0) msgFillOut += ", ";
            msgFillOut += "Material code 형식이 아닙니다.";
        }


        if (msgFillOut.length > 0) {
            fn_showInformation({
                title: "Please fill out below fields.",
                message: msgFillOut
            });
            return;
        }

        //지급금액 체크(Foreigner 의 경우 강의료 한도 제외 by 2018.04.16 WK Kim)
        //if (participantText.indexOf("/Foreigner") < 0) {
        if (participantType != "OtherHCP") {
            var limitAmout = parseInt(HCP_LIMIT_AMOUNT);
            var providedAmount = parseInt($("#tdAgendaRoleAccumulate").data("total-amount"));
            if (providedAmount + amount > limitAmout) {
                fn_showInformation({
                    title: "Confirm!",
                    message: "한도금액(" + fn_AddComma(HCP_LIMIT_AMOUNT) + ") 을 초과할 수 없습니다.",
                });
                return;
            }
        }


        var agendaRole = {
            PROCESS_ID: PROCESS_ID,
            AGENDA_IDX: agendaIDX,
            AGENDA_ROLE_IDX: agendaRoleIDX,
            PARTICIPANT_IDX: participantIDX,
            PARTICIPANT_CODE: participantCode,
            ROLE_TYPE: roleType,
            ROLE: role,
            CRITERIA: criteria,
            REASON: reason,
            AMOUNT: amount,
            MATERIAL_CODE: materialCode,
            IS_DELETED: "N",
            CREATOR_ID: USER_ID,
            UPDATER_ID: USER_ID
        }

        $.ajax({
            url: EVENT_SERVICE_URL + "/MergeAgendaRole",
            type: "POST",
            data: JSON.stringify(agendaRole),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var agendaRoleIDX = $("#hddAgendaRoleIDX").val();
                if (agendaRoleIDX == 0) {
                    //신규로 등록된 경우 첨부파일 처리
                    agendaRoleIDX = data;
                    moveToAgendaRoleAttachFiles(agendaRoleIDX);
                } else {
                    refreshAgendaRole();
                }
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });


    });

    /* AgendaRole 삭제 */
    $("#btnDeleteAgendaRole").click(function () {

        PROCESS_ID = $("input[id$=hddProcessID]").val();
        USER_ID = $("input[id$=hddUserID]").val();
        var agendaIDX = $("#hddModalAgendaIDX").val();
        var agendaRoleIDX = $("#hddAgendaRoleIDX").val();

        fn_confirm()
            .done(function (result) {
                if (result) {
                    $.ajax({
                        url: EVENT_SERVICE_URL + "/DeleteAgendaRole/" + PROCESS_ID + "/" + agendaIDX + "/" + agendaRoleIDX + "/" + USER_ID,
                        type: "GET",
                        //dataType: "json",
                        contentType: "application/json; charset=utf-8",
                        success: function () {
                            selectAgendaRole(); //Agenda조회
                            $("#jsGridPraticipants").jsGrid("loadData"); //Participant 재조회
                        },
                        error: function (error) {
                            fn_showError({
                                message: error.responseText
                            });
                        },
                    });
                }
            });
    });

    /* Print */
    $("#btnPrintYourDoces").click(function (e) {
        e.preventDefault();

        var eventId = $("input[id$=hddEventID]").val();
        var userid = $("input[id$=hddUserID]").val();
        var processid = $("input[id$=hddProcessID]").val();
        var processid = $("input[id$=hddProcessID]").val();
        var agendaIDX = $("#hddModalAgendaIDX").val();
        var agendaRuleIdx = $("#hddAgendaRoleIDX").val();
        var url = "/Ultra/Pages/Event/Print/PrintYourDocesCover.aspx?processid=" + processid + "&idx=" + agendaIDX + "&ruleidx=" + agendaRuleIdx;

        window.open(url, "_blank", "width=900, height=800, toolbar=no, menubar=no, scrollbars=no, resizable=no, copyhistory=no");

    });


    $("#divAgendaList").on('click', '.table-title', function (e) {
        var agenda = $(this).closest("div").data("agenda");
        $("#hddAgendaIDX").val(agenda.AGENDA_IDX);
        $("#optAgendaDay").val(agenda.DAY);
        var startTime = agenda.START_TIME.split(":");
        $("#optAgendaStartHour").val(startTime[0]);
        $("#optAgendaStartMinute").val(startTime[1]);
        $("#numAgendaDuration").val(agenda.DURATION);
        $("#txtAgendaSubject").val(agenda.SUBJECT);

    });

    // Criteira 레이어 여닫기
    $('.btn-tooltip').click(function () {
        var tg = $(this).attr('href');
        $(tg).fadeIn('fast');
        $("#new_criteria_btn").addClass("active");
        $("#old_criteria_btn").attr("disabled", false);
        $("#new_criteria_btn").attr("disabled", false);
        return false;
    });
    $('#new_criteria_btn').click(function () {
        $("#new_criteria_btn").addClass("active");
        $("#new_criteria").css("display", "inline");
        $("#old_criteria_btn").removeClass("active");
        $("#old_criteria").css("display", "none");
    });
    $('#old_criteria_btn').click(function () {
        $("#old_criteria_btn").addClass("active");
        $("#old_criteria").css("display", "inline");
        $("#new_criteria_btn").removeClass("active");
        $("#new_criteria").css("display", "none");

    });


    $('.layer-tooltip .close').click(function () {
        $(this).closest('.layer-tooltip').fadeOut('fast');
    });

    $(":radio[name$='lb_role_type']").change(function () {
        setAgendaRole();
    });

    $("#optAgendaDay").on("change", function () {
        if ($("#optAgendaDay").val() == "1") {
            //Day1 일때만
            var inputDate = $(".form-date-hour-min input").val();
            try {
                var tmp = inputDate.split(" ");

                var startDate = new Date(tmp[0].toString() + "T" + tmp[1].toString() + ":00");
                var hour = startDate.getHours();
                var minute = startDate.getMinutes();

                $("#optAgendaStartHour").val(addZero(hour));
                minute = parseInt(minute / 10) * 10;
                $("#optAgendaStartMinute").val(addZero(minute));
            } catch (e) {
                console.log(e);
            }
        }
    });
});

function setAgendaRole() {


    $("#optAgendaRole").empty();

    var roleType = $(":radio[name$='lb_role_type']:checked").val();

    if (roleType == "Lecture") {
        $("#optAgendaRole").append("<option value='1'>좌장</option>")
        $("#optAgendaRole").append("<option value='2'>패널</option>")
        $("#optAgendaRole").append("<option value='3'>강연</option>")
    } else if (roleType == "Consulting") {
        $("#optAgendaRole").append("<option value='1'>좌장</option>")
        $("#optAgendaRole").append("<option value='2'>패널</option>")
        $("#optAgendaRole").append("<option value='4'>자문</option>")
    } else {
        $("#optAgendaRole").append("<option value='1'>좌장</option>")
        $("#optAgendaRole").append("<option value='2'>패널</option>")
        $("#optAgendaRole").append("<option value='3'>강연</option>")
        $("#optAgendaRole").append("<option value='4'>자문</option>")
    }

}

/* Role Information에 HCP 조회 */
function selectAgendaRoleParticipants(processID, selectedParticipant) {
    $.ajax({
        url: EVENT_SERVICE_URL + "/SelectAgendaParticipants/" + processID + "/" + selectedParticipant,
        type: "GET",
        cache: false,
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var cnt = data.length;
            $("#optAgendaRoleParticipant").empty();
            $("#optAgendaRoleParticipant").append("<option value='0'> -- 선택해주세요 -- </option>")
            for (var i = 0; i < cnt; i++) {
                var participant = data[i];
                var opt = "<option value=" + participant.PARTICIPANT_IDX.toString() + " data-participant-type=" + participant.PARTICIPANT_TYPE;
                opt += " data-participant-code=" + participant.HCP_CODE + ">";
                opt += participant.HCP_NAME + "/" + participant.HCO_NAME + "/" + participant.SPECIALTY_NAME + "</option>";
                $("#optAgendaRoleParticipant").append(opt);
            }

            if (selectedParticipant) {
                $("#optAgendaRoleParticipant option[value='" + selectedParticipant + "']").prop("selected", true);
            }
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}

/* Role Information 이 열릴때 */
$('#role_information').on('show.bs.modal', function (e) {
    PROCESS_ID = $("input[id$=hddProcessID]").val();
    //alert($(e.relatedTarget).hasClass('edit-agenda'));
    // participant 조회
    $("#divAttachFiles_AgendaRole").find('.attach-list').empty(); //첨부파일 Clear
    if ($(e.relatedTarget).hasClass('add-agenda')) {
        //신규인 경우
        selectAgendaRoleParticipants(PROCESS_ID, "0");

        var agenda = $(e.relatedTarget).closest("div").data("agenda");
        $("#hddModalAgendaIDX").val(agenda.AGENDA_IDX);
        $("#hddAgendaRoleIDX").val("0");
        var inputes = $("#divCriteria").find("input");
        var len = inputes.length;
        for (var i = 0; i < len; i++) {
            $(inputes[i]).prop("checked", false);
        }
        $("#optAgendaRoleParticipant").attr("disabled", false);
        $("#txtAgendaRoleReason").val("");
        $("#numAgendaRoleAmount").val("0");
        //$("#txtAgendaRoleMaterialCode").val("");

        //신규인 경우, 모두 활성화

        $('#btnSaveAgendaRole').attr('disabled', false);
        $('#radmaterialcodeY').attr('disabled', false);
        $('#radmaterialcodeN').attr('disabled', false);
        $('#txtAgendaRoleMaterialCode').attr('disabled', false);

        $('#txtAgendaRoleMaterialCode').val("추후입력");

        //Your-Doces Conver
        $("#btnPrintYourDoces").hide();

    } else if ($(e.relatedTarget).hasClass('edit-agenda')) {
        //수정인 경우
        var agendaRole = $(e.relatedTarget).closest("tr").data("agenda-role");

        $("#hddModalAgendaIDX").val(agendaRole.AGENDA_IDX);
        $("#hddAgendaRoleIDX").val(agendaRole.AGENDA_ROLE_IDX);

        $("input:radio[name$='lb_role_type']").filter("[value='" + agendaRole.ROLE_TYPE + "']").attr("checked", true);

        // 우선 전체 삭제
        $("#optAgendaRole option").each(function () {
            var roleValue = $(this).val();
            if (roleValue == agendaRole.ROLE)
                $(this).prop("selected", true);
            else
                $(this).prop("selected", false);
        });
        $("#optAgendaRoleParticipant").attr("disabled", true);

        selectAgendaRoleParticipants(PROCESS_ID, agendaRole.PARTICIPANT_IDX);

        var inputes = $("#divCriteria").find("input");
        var criteria = agendaRole.CRITERIA;
        for (var i = 0; i < inputes.length; i++) {
            var include = criteria.indexOf((i + 1).toString());
            $(inputes[i]).prop("checked", include > -1);
        }
        var processStatus = "";
        if (agendaRole.ATTACH_IDX) {
            /* <-- 첨부파일 --------------------------------------------------- */
            var attachFile = {
                Index: agendaRole.ATTACH_IDX,
                DisplayName: encodeURIComponent(agendaRole.DISPLAY_FILE_NAME),
                SavedName: encodeURIComponent(agendaRole.SAVED_FILE_NAME),
                FileSize: agendaRole.FILE_SIZE,
                AttachType: agendaRole.ATTACH_FILE_TYPE,
                FileHandlerUrl: agendaRole.FILE_HANDLER_URL,
                FilePath: encodeURIComponent(agendaRole.FILE_PATH),
                ErrorMessage: "",
            }
            var $li = $("<li data-attach-file=" + JSON.stringify(attachFile) + "></li>");
            var $ahref = $("<a href='#' class='attach-link btn btn-xs btn-gray'>" + agendaRole.DISPLAY_FILE_NAME + "</a>");
            $li.append($ahref);

            processStatus = $("input[id$=hddProcessStatus]").val();
            if (processStatus == "Saved") {
                var $button = $("<button type='button' class='fa fa-times'><span class='tts'>Close</span></button>");
                $li.append($button);
            } else {
                $ahref.css('padding-right', '8px');
            }
            $li.appendTo($("#divAttachFiles_AgendaRole .attach-list"));
            /* ------------------------------------------------------------------> */

        }
        //if (processStatus == "Request" || processStatus == "Processing" || processStatus == "Completed"
        //    || processStatus == "EventCompleted" || processStatus == "PaymentCompleted") {
        //Event Complete 시에만 YourDoc print  화면 보임(by WooKyung Kim)
        if (processStatus == "EventCompleted") {
            $("#btnPrintYourDoces").show();
            $("#btnPrintYourDoces").attr("disabled", false);
        } else {
            $("#btnPrintYourDoces").show();
            $("#btnPrintYourDoces").attr("disabled", true);
        }


        $("#txtAgendaRoleReason").val(agendaRole.REASON);
        //alert(agendaRole.REASON);
        $("#numAgendaRoleAmount").val(fn_AddComma(agendaRole.AMOUNT));

        //version 1.0.5 Material Code 입력 방법 변경
        $("#txtAgendaRoleMaterialCode").val(agendaRole.MATERIAL_CODE);
        if (agendaRole.MATERIAL_CODE == "추후입력") {
            $('#txtAgendaRoleMaterialCode').hide();
            $('input[name="radmaterialcode"]:radio[value="No"]').attr("checked", true);
            //Complete 시에는 수정 가능
            if ($("input[id$=hddProcessStatus]").val() == "Completed") {
                $('#btnSaveAgendaRole').attr('disabled', false);
                //$('input[name=radmaterialcode').attr('disabled', false);
                $('#radmaterialcodeY').attr('disabled', false);
                $('#radmaterialcodeN').attr('disabled', false);
                $('#txtAgendaRoleMaterialCode').attr('disabled', false);
            }
        } else {
            $('#txtAgendaRoleMaterialCode').show();
            $('input[name="radmaterialcode"]:radio[value="Yes"]').attr("checked", true);
            if ($("input[id$=hddProcessStatus]").val() == "EventCompleted" || $("input[id$=hddProcessStatus]").val() == "PaymentCompleted") {

                $('#btnSaveAgendaRole').attr('disabled', true);
                //$('input[name=radmaterialcode').attr('disabled', true);
                $('#radmaterialcodeY').attr('disabled', true);
                $('#radmaterialcodeN').attr('disabled', true);
                $('#txtAgendaRoleMaterialCode').attr('disabled', true);
            } else {

                $('#btnSaveAgendaRole').attr('disabled', false);
                //$('input[name=radmaterialcode').attr('disabled', false);
                $('#radmaterialcodeY').attr('disabled', false);
                $('#radmaterialcodeN').attr('disabled', false);
                $('#txtAgendaRoleMaterialCode').attr('disabled', false);
            }

        }




        //조회인 경우 이미 지급된 금액은 현재 금액을 제외한 금액이다.
        var participantCode = agendaRole.PARTICIPANT_CODE;
        var processID = $("input[id$=hddProcessID]").val();
        var roleType = agendaRole.ROLE_TYPE;
        var role = agendaRole.ROLE;
        var c_date = new Date('2018-08-19');
        if (c_date <= fn_formatJSONDate(agendaRole.CREATE_DATE)) {
            $("#new_criteria_btn").addClass("active");
            $("#new_criteria").css("display", "inline");
            $("#old_criteria_btn").removeClass("active");
            $("#old_criteria").css("display", "none");
        } else {
            $("#old_criteria_btn").addClass("active");
            $("#old_criteria").css("display", "inline");
            $("#new_criteria_btn").removeClass("active");
            $("#new_criteria").css("display", "none");

        }
        //alert(fn_formatJSONDate(agendaRole.CREATE_DATE));
        selectAmountForHCP(participantCode, processID, roleType, role, agendaRole.AMOUNT);
    }
});

$('#role_information').on('hide.bs.modal', function (e) {
    selectAgendaRole(); //Agenda조회
    $("#jsGridPraticipants").jsGrid("loadData"); //Participant 재조회
});





/* HCP 변경 이벤트 */
$("#optAgendaRoleParticipant").on('change', function (e) {
    var participantType = $("#optAgendaRoleParticipant option:selected").data("participant-type");
    if (participantType) {
        var participantCode = $("#optAgendaRoleParticipant option:selected").data("participant-code");
        var processID = $("input[id$=hddProcessID]").val();
        var roleType = $(":radio[name$='lb_role_type']:checked").val();
        var role = $("select[id$=optAgendaRole] option:selected").val();
        selectAmountForHCP(participantCode, processID, roleType, role);
        //alert(participantType);

        //version 1.0.4 OtherHCP 의 경우 Criteria 비활성화
        if (participantType == 'Employee' || participantType == 'OtherHCP') {
            //바이엘 직원인 경우 Criterial 비활성화
            $("#divCriteria").find("input").attr("disabled", true);
            $("#divCriteria").find("input").prop("checked", false);
            if (participantType == 'OtherHCP') {
                $("#numAgendaRoleAmount").attr("disabled", false);
                $("#divCriteria").find("input").attr("disabled", false);
                $("#divCriteria").find("input").prop("checked", true);
            } else {
                $("#numAgendaRoleAmount").attr("disabled", true);
            }

            $("#numAgendaRoleAmount").val("0");
        } else {
            $("#divCriteria").find("input").attr("disabled", false);
            $("#numAgendaRoleAmount").attr("disabled", false);
        }
    }
});

/* 해당연도 지불금액 조회 
    기등록 자료 조회인 경우 등록 금액을 총액에서 빼준다.
*/
function selectAmountForHCP(participantCode, processID, roleType, role, amount) {


    var hcpInfo = {
        hcpCode: participantCode,
        processID: processID,
        roleType: roleType,
        role: role
    }

    $.ajax({
        url: EVENT_SERVICE_URL + "/SelectAgendaRoleAmountForHCP",
        type: "POST",
        data: JSON.stringify(hcpInfo),
        dataType: "json",
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var totalAmount = 0;
            var accumulateList = "";

            if (data != null && data.length > 0) {
                for (var i = 0; i < data.length; i++) {
                    totalAmount += parseInt(data[i].AMOUNT);
                    accumulateList += "<tr><td>" + data[i].SUBJECT + "</td><td>" + data[i].START_DATE + "</td><td>" + data[i].AMOUNT + "</td></tr>";
                }
            }

            //if (amount) totalAmount = totalAmount - amount;
            var remainAmount = parseInt(HCP_LIMIT_AMOUNT) - totalAmount;
            $("#tdAgendaRoleAccumulate").data("total-amount", totalAmount);
            $("#tdAgendaRoleAccumulate").html("<strong>" + fn_AddComma(parseInt(totalAmount)) + "</strong> (Remain : " + fn_AddComma(remainAmount) + ")");

            $("#tbdAccumulateList").html(accumulateList); //Accumulate 조회

        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            }).done(function () {
                d.resolve(-1);
            });
        },
    });
}

/* Agenda 조회 */
function selectAgenda() {
    PROCESS_ID = $("input[id$=hddProcessID]").val();

    $.ajax({
        url: EVENT_SERVICE_URL + "/SelectAgenda/" + PROCESS_ID,
        type: "GET",
        dataType: "json",
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            displayAgenda(data);
            var roles = selectAgendaRole(); //Agenda별 역할 조회
            selectAgendaRoleSummary(PROCESS_ID);
        },
        error: function (error) {

            fn_showError({
                message: error.responseText
            });
        },
    });
}

function displayAgenda(list) {
    $("#divAgendaList").empty();
    var cnt = list.length;
    var processStatus = $("input[id$=hddProcessStatus]").val();
    for (var i = 0; i < cnt; i++) {
        var agenda = list[i];
        agenda.SUBJECT = agenda.SUBJECT.replace(/'/g, "&#39;");
        var divAgenda = "<div class='box-panel box-panel-list agend-list'>";
        divAgenda = divAgenda + "<div class='table-heading table-heading-lg agenda-title' data-agenda='" + JSON.stringify(agenda) + "'>";
        divAgenda = divAgenda + "<span>Day" + agenda.DAY.toString() + " " + agenda.START_TIME + "</span><span class='badge'>" + agenda.DURATION.toString() + "</span>";
        divAgenda = divAgenda + "<h2 class='table-title'>" + agenda.SUBJECT + "</h2>";
        if (processStatus == "Saved" || processStatus == "Temp")
            divAgenda = divAgenda + "<a href='#' class='btn btn-sm btn-darkgray add-agenda' data-toggle='modal' data-target='#role_information'><i class='fa fa-plus'></i>Add Role</a>";
        divAgenda = divAgenda + "</div>";
        divAgenda = divAgenda + "<table id=tblAgenda_" + agenda.AGENDA_IDX.toString() + " class='table table-striped'></table>";
        divAgenda = divAgenda + "</div>";
        $("#divAgendaList").append(divAgenda);
    }
}

/* Agenda Role 조회 */
function selectAgendaRole() {
    PROCESS_ID = $("input[id$=hddProcessID]").val();

    $.ajax({
        url: EVENT_SERVICE_URL + "/SelectAgendaRole/" + PROCESS_ID,
        type: "GET",
        dataType: "json",
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            return displayAgendaRole(data);
        },
        error: function (error) {

            fn_showError({
                message: error.responseText
            });
        },
    });
}

function displayAgendaRole(list) {

    var len = list.length;
    var material_code_flag = false;
    var beforeAgendaIDX = 0;
    var generalInfoRoles = []; //GeneralInformation에 표시하기 위해서
    var roles = "";

    var total = 0;
    var waring = false;
    $(".agend-list table").empty();
    for (var i = 0; i < len; i++) {
        var agendaRole = list[i];
        var agendaIDX = agendaRole.AGENDA_IDX;
        var tblAgenda = "#tblAgenda_" + agendaIDX.toString();
        if (beforeAgendaIDX != agendaIDX) {
            $(tblAgenda).empty();
            $(tblAgenda + " tbody").empty();
            var tblColgroup = "<colgroup><col style='width:40px;'/><col /><col style='width:150px;'/><col style='width:150px;'/><col style='width:150px;'/><col style='width:60px;'/></colgroup>"
            $(tblAgenda).append(tblColgroup);
            var tblHead = " <thead><tr><th scope='col'>Role</th><th scope='col'>HCP</th><th scope='col' class='text-right'>Amount</th>";
            tblHead = tblHead + "<th scope='col'>Material Code</th><th scope='col'>(Criteria)Reason</th><th scope='col' class='text-center'>CV</th></tr></thead><tbody></tbody>";
            $(tblAgenda).append(tblHead);

            if (generalInfoRoles.length > 0)
                generalInfoRoles[generalInfoRoles.length - 1].Roles = roles;
            roles = "";
            generalInfoRoles.push({ AgendaIDX: agendaIDX, Roles: roles });

        }
        //AgendaRole 구분 => 1:좌장 2:패널 3:강연 4:자문
        var row = "";
        console.log(agendaRole);
        //console.log(JSON.stringify(agendaRole));
        //var file_name = agendaRole.DISPLAY_FILE_NAME.replace("'", "");
        //var file_path = agendaRole.FILE_PATH.replace("'", "");
        //var saved_file_name = agendaRole.SAVED_FILE_NAME.replace("'", "");
        //agendaRole.DISPLAY_FILE_NAME = file_name;
        //agendaRole.FILE_PATH = file_path;
        //agendaRole.SAVED_FILE_NAME = saved_file_name;
        //console.log(file_name);
        //console.log(file_path);
        console.log(fn_ConvertJsonData(agendaRole));
        //직원인 경우는 제품코드만 의사는 첨부와 제품코드 모두

        //2021년 6월 23일 발생 --- WK Kim
        //파일 이름에 ' 가 있는 경우 error 가 난다. 이 경우 해결 위해 방법을 찾아야함 encodeURIComponent 참고
        //현재는 저장되어 있는 파일이름에서 '를 삭제하면 해결됨
        if ((agendaRole.SPECIALTY_NAME != "Employee" && !agendaRole.ATTACH_IDX) || agendaRole.MATERIAL_CODE.length < 1) {
            waring = true;
            //row = "<tr class='color-point' data-agenda-role='" + JSON.stringify(agendaRole) + "'><td>";
            row = "<tr class='color-point' data-agenda-role='" + fn_ConvertJsonData(agendaRole) + "'><td>";
        }
        else {
            //row = "<tr data-agenda-role='" + JSON.stringify(agendaRole) + "'><td style='text-align:center;'>";
            row = "<tr data-agenda-role='" + fn_ConvertJsonData(agendaRole) + "'><td style='text-align:center;'>";
        }

        if (roles.length > 0) roles += ",";
        roles += agendaRole.HCP_NAME;


        if (agendaRole.ROLE == "1") {
            row += "<i class='fa fa-user-circle colr-blue'></i>"; //좌장
            roles += "(좌장)";
        }
        else if (agendaRole.ROLE == "2") {
            row += "<i class='fa fa-user-circle colr-green'></i>"; //패널
            roles += "(패널)";
        }
        else if (agendaRole.ROLE == "3") {
            row += "<i class='fa fa-user-circle colr-yellow'></i>"; //강연
            roles += "(강연)";
        }
        else if (agendaRole.ROLE == "4") {
            row += "<i class='fa fa-user-circle colr-black'></i>"; //자문
            roles += "(자문)";
        }
        var krpia = "미동의(KRPIA)";
        if (agendaRole.KRPIA == "Y") krpia = "동의(KRPIA)";
        krpia += " / " + agendaRole.COST_CENTER + " / " + agendaRole.SAP_NO;
        if (agendaRole.KRPIA == "") krpia = "";
        row += "</td><td>" + "<a href='#' class='link text-ellipsis edit-agenda' data-toggle='modal' data-target='#role_information'><strong>" + agendaRole.PARTICIPANT_NAME + "</strong></a><br>" + agendaRole.HCO_NAME + "/" + agendaRole.SPECIALTY_NAME + "<br>" + krpia + "</td>";
        row += "<td class='text-right'><strong>" + fn_AddComma(agendaRole.AMOUNT) + "</strong></td>";
        row += "<td>" + agendaRole.MATERIAL_CODE + "</td>";
        row += "<td>" + agendaRole.CRITERIA + "<br>" + agendaRole.REASON + "</td>"
        if (agendaRole.MATERIAL_CODE == "추후입력") {
            material_code_flag = true;
        }
        if (agendaRole.ATTACH_IDX) {
            //row += "<td class='text-center'><a href=\"javascript:fn_cvopen('" + agendaRole + "')\" class='fa fa-paperclip colr-darkgray'></a></td></tr>";


            var attachFile = {
                Index: agendaRole.ATTACH_IDX,
                DisplayName: encodeURIComponent(agendaRole.DISPLAY_FILE_NAME),
                SavedName: encodeURIComponent(agendaRole.SAVED_FILE_NAME),
                FileSize: agendaRole.FILE_SIZE,
                AttachType: agendaRole.ATTACH_FILE_TYPE,
                FileHandlerUrl: agendaRole.FILE_HANDLER_URL,
                FilePath: encodeURIComponent(agendaRole.FILE_PATH),
                ErrorMessage: "",
            }
            row += "<td class='text-center attach-list_cv' style='list-style:none' ><li data-attach-file=" + JSON.stringify(attachFile) + ">"
            row += "<a href='#' class='attach-link fa fa-paperclip colr-darkgray'></a></li></td></tr>"








            //row += "<td class='text-center'><a href=\"javascript:fn_cvopen(this)\" class='fa fa-paperclip colr-darkgray'></a></td></tr>";
        }
        else {
            row += "<td class='text-center'></td></tr>";
        }

        $(tblAgenda + " tbody").append(row);
        total += parseInt(agendaRole.AMOUNT);

        beforeAgendaIDX = agendaIDX;
    }
    if (generalInfoRoles.length > 0)
        generalInfoRoles[generalInfoRoles.length - 1].Roles = roles;
    $("#totalAgendaRole").text(fn_AddComma(total));

    /* ------------------------------  경고 */
    if (len > 0) {
        if (waring)
            $('#tabAgenda .fa-commenting').show();
        else
            $('#tabAgenda .fa-commenting').hide();
    }

    //version 1.0.5 Material Code 입력 방법 변경
    if ($("input[id$=hddProcessStatus]").val() == "Completed") {
        //alert("777");
        if (material_code_flag) {
            $('#tabAgenda .fa-commenting').show();
            $('#btnSaveAgendaRole').attr('disabled', false);
            //        $('input[name=radmaterialcode').attr('disabled', false);
            $('#radmaterialcodeY').attr('disabled', false);
            $('#radmaterialcodeN').attr('disabled', false);
            $('#txtAgendaRoleMaterialCode').attr('disabled', false);
        } else {
            $('#tabAgenda .fa-commenting').hide();
            $('#btnSaveAgendaRole').attr('disabled', true);
            //        $('input[name=radmaterialcode').attr('disabled', true);
            $('#radmaterialcodeY').attr('disabled', true);
            $('#radmaterialcodeN').attr('disabled', true);
            $('#txtAgendaRoleMaterialCode').attr('disabled', true);
        }
    }

    $('.attach-list_cv').on('click', '.attach-link', function (e) {
        e.preventDefault();
        var attachFile = $(this).closest('li').data('attach-file');

        if (attachFile && !isSmartDevice()) {
            $('#iframeFileDown', parent.document).attr('src', UPLOAD_HANDLER_URL + "?file=" + attachFile.FilePath);
        } else if (attachFile) {
            window.open(UPLOAD_HANDLER_URL + "?file=" + attachFile.FilePath);
        }

    });



    return generalInfoRoles;
}

//function fn_cvopen(idx) {
//    alert($(this).attr('class'));
//    return;
//
//
//    if (idx !='' && !isSmartDevice()) {
//        $('#iframeFileDown', parent.document).attr('src', idx );
//    } else if (idx != '') {
//        window.open(idx);
//    }
//
//}

function clearAgendaInputArea() {
    $("#hddAgendaIDX").val("0");
    $("#optAgendaDay").val("1");
    $("#numAgendaDuration").val("");
    $("#txtAgendaSubject").val("");
}

function refreshAgendaRole() {
    //Modal이 닫히는 event시점에 재조회한다.
    $('#role_information').modal('hide');

}

function moveToAgendaRoleAttachFiles(agendaRoleIDX) {
    var liFiles = $("#divAttachFiles_AgendaRole .attach-list").find("li");
    var files = "";
    for (var i = 0; i < liFiles.length; i++) {
        files += $(liFiles[i]).data('attach-file').SavedName + ";";
    }
    if (files.length > 0)
        fn_MoveToAttach("AgendaRole", files, agendaRoleIDX, refreshAgendaRole); // FileUpload.js에 있습니다.
    else
        refreshAgendaRole();
}

//General Information의 Agenda summary 조회
//Role 때문에 별로로 조회해 온다
function selectAgendaRoleSummary(processID) {

    $.ajax({
        url: EVENT_SERVICE_URL + "/SelectAgendaRoleSummary/" + processID,
        type: "GET",
        dataType: "json",
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            displayAgendaRoleSummary(data);
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    }).done(function () {
        resizeScreen();
    });
}

function displayAgendaRoleSummary(list) {

    $("#tblAgendaRoleSummary tbody").empty();
    $.map(list, function (item) {
        var $tr = "<tr><td>" + item.START_DATE + "</td><td>" + item.START_TIME + "</td><td>" + item.SUBJECT + "</td><td>" + item.ROLES + "</td></tr>";
        $("#tblAgendaRoleSummary tbody").append($tr);
    });
}