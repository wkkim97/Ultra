$(function () {

    /* 프로세스 상태에 따른 페이지 설정 */
    setPageStatus()
    
    /* EventComplete시 입력 옵션설정 */
    setEventComplete();

    $("#tabEventMaster a[data-toggle='tab']").on("click", function () {
        
        if ($(this).parent('li').hasClass('disabled')) {
            return false;
        };
    });

    // version 1.0.5 Admin Event Page for change by DM Team
    //$(".changeButton").on("click", function () {
    //    var id = $(this).attr('id');
    //    fn_change_value({
    //        title: "Confirm",
    //    }).done(function (result) {
    //        var processID = $("#hddProcessID").val();
    //        var CATEGORY = "";
    //        var ADJUSTMENT_AREA = "";
    //        var OLD_VALUE = $("#" + id.split("_")[1]).val();
    //        var NEW_VALUE = $("#new_value").val();
    //        var REASON = $("#cha_reason").val();
    //        var UPDATER_ID = $("#hddUserID").val();
    //        switch (id) {
    //            case "old_txtAddress":
    //                CATEGORY = "Venue";
    //                ADJUSTMENT_AREA = "ADDRESS_OF_VENUE";
    //                
    //                break;
    //
    //            case "old_txtKRPIANumber":
    //                CATEGORY = "KRPIA code";
    //                ADJUSTMENT_AREA = "KRPIA 신고번호";
    //                break;
    //            default:
    //                return;
    //                break;
    //        }
    //        if (result) {
    //            var chagne_inform = {
    //                PROCESS_ID: processID,
    //                CATEGORY: CATEGORY,
    //                ADJUSTMENT_AREA: ADJUSTMENT_AREA,
    //                OLD_VALUE: OLD_VALUE,
    //                NEW_VALUE: NEW_VALUE,
    //                REASON : REASON,
    //                UPDATER_ID: UPDATER_ID
    //            }
    //            console.log(chagne_inform);
    //            $.ajax({
    //                url: EVENT_SERVICE_URL + "/UpdateChangeValue",
    //                type: "POST",
    //                data: JSON.stringify(chagne_inform),
    //                //dataType: "json",
    //                contentType: "application/json; charset=utf-8",
    //                processdata: true,
    //                crossDomain: true,
    //                success: function (data) {
    //                    //alert("33555553");
    //                },
    //                error: function (error) {
    //                    alert(error.responseText);
    //                },
    //            });
    //        }
    //     });      
    //});



    /* Payment/ETC 정보는 페이지 로드 할 때가 아닌 Tab을 클릭할때 조회한다.(requester 는 처음 부터 조회)*/
    /* 나머지 Tab의 내용은 로드시점에 조회하여 GeneralInformation Summary정보에 Display */
    $("#tabEventMaster a[data-toggle='tab']").on('shown.bs.tab', function (e) {
        var tabText = e.target.innerText;

        if (tabText == "Payment") {
            $("#jsGridPaymentByHCP").jsGrid("loadData");
            $("#jsGridPaymentPlanActual").jsGrid("loadData");
            $("#jsGridPaymentSRMHistory").jsGrid("loadData");
            $("#jsGridPaymentInputSRM").jsGrid("loadData");
        }
        else if (tabText == "Delegation") {
            $("#jsGridDelegation").jsGrid("loadData");
        }
        //chage history 시 적용 (by wookyung kim)
        //else if (tabText == "Change History") {
        //    $("#jsGridChange").jsGrid("loadData");
        //}
    });

    $('.form-date-hour-min').datetimepicker({
        format: 'yyyy-mm-dd hh:ii',
        weekStart: 0,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        forceParse: 0,
        minuteStep: 5,
        minView: 0,
        maxView: 2,
    });

    $('.form_datehour').datetimepicker({
        format: 'yyyy-mm-dd hh:00',
        weekStart: 0,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        forceParse: 0,
        minuteStep: 60,
        minView: 1,
        maxView: 1,
    });

    $('.form_date').datetimepicker({
        format: 'yyyy-mm-dd',
        weekStart: 0,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        forceParse: 0,
        minuteStep: 60,
        minView: 2,
        maxView: 2,
    });

    // 결재자라인 Lync NameCtrl 

    $("span[id^=pre_]").each(function (i) {
        try{
            if (_nameCtrl.PresenceEnabled) {
                _nameCtrl.OnStatusChange = onStatusChange;
                var userAddress = $(this).attr("value");
                var userId = $(this).attr("userid");
                var status = _nameCtrl.GetStatus(userAddress, userId);

                $(this).bind("mouseover", function (e) {
                    var eLeft = $(this).offset().left;
                    var x = eLeft - $(window.parent).scrollLeft();

                    var eTop = $(this).offset().top;
                    var y = eTop - $(window.parent).scrollTop();
                    _nameCtrl.ShowOOUI(userAddress, 0, x, y + 100);
                    //ShowOOUI(userAddress, this); 
                });
                $(this).bind("mouseout", function () { HideOOUI(userAddress); });
            }
        }catch(e){
            console.log("lyun error:"+e.description)
        }

        
    });

    /* textarea 높이 조절 */
    $("textarea").change(function () {
        
        if ($(this).prop("disabled")) {
            $(this).height(0).height(this.scrollHeight);
        }
    });
    /* textarea 높이 조절 */

    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.backdrop = 'static';
    $.fn.modal.prototype.constructor.Constructor.DEFAULTS.keyboard = false;
});

function setPageStatus() {


    var status = $("input[id$=hddProcessStatus]").val();
    var statusIdx = 0;
    if (status == "Saved") statusIdx = 1;
    else if (status == "Request") statusIdx = 2;
    else if (status == "Processing") statusIdx = 3;
    else if (status == "Completed") statusIdx = 4;
    else if (status == "EventCompleted") statusIdx = 5;
    else if (status == "PaymentCompleted") statusIdx = 6;

    if (status == "Canceled" || status == "Reject") statusIdx = 91;

    var delegation = $("input[id$=hddDelegation]").val();

    //신규입력시에는 Cancel버튼을 안보이게
    //eWorkflow에서는 behind코드에서 처리했으나 Ultra에서는  buttonStatus를 공유 하므로 script에서 처리
    if (status == "Temp") $("[id$=btnCancel]").hide();

    //Delegation이 아니면
    if (delegation == "none") {

        var userID = $("input[id$=hddUserID]").val();
        var requesterID = $("input[id$=hddRequesterID]").val();


        if (requesterID.length > 0) {
            if (userID == requesterID) {
                //로그인 유저가 기안자인 경우

                if (statusIdx > 1) {
                    //Request한 경우
                    disableGeneralInformation();
                    disableCostPlan();
                    disableAgenda();
                }

                if (statusIdx > 4) {
                    //EventComplete한 경우
                    disableParticipants();
                    disableEtc();
                    $("#tbnDownloadSRM").prop("disabled", false);
                    $("#tabEventMaster li[id='tabPayment']").removeClass('disabled');

                    //Payment Complete를 위해 로딩 시점에 한번 조회한다.
                    $("#jsGridPaymentPlanActual").jsGrid("loadData");
                }

                if (statusIdx > 5) {
                    //PaymentCompleted인 경우
                    disablePayment();
                }
            } else {
                disableGeneralInformation();
                disableCostPlan();
                disableParticipants();
                disableAgenda();
                disablePayment();
                disableEtc();
                if (statusIdx > 4) {
                    $("#tbnDownloadSRM").prop("disabled", false);
                    $("#tabEventMaster li[id='tabPayment']").removeClass('disabled');
                }
                $("#tabEventMaster li[id='tabEtc']").hide();
            }

            


        }


        if (!$("[id$=tabCostPlan]").is(":visible") && !$("[id$=tabParticipants]").is(":visible")
            && !$("[id$=tabAgenda]").is(":visible") && !$("[id$=tabPayment]").is(":visible")) {
            $("#divGI_FirstColumn").removeClass("col-md-6");
            $("#divGI_SecondColumn").removeClass("col-md-6");
            $("#divGI_FirstColumn").addClass("col-md-12");
            $("#divGI_SecondColumn").addClass("col-md-12");
        }
    } else {
        $('#tabEventMaster > li').removeClass("active")
        //$("#tabEventMaster li[id='tabForm']").addClass('disabled');
        disableGeneralInformation();
        $("#tabEventMaster li[id='tabCostPlan']").addClass('disabled');
        $("#tabEventMaster li[id='tabAgenda']").addClass('disabled');
        $("#tabEventMaster li[id='tabPayment']").addClass('disabled');
        $("#tabEventMaster li[id='tabEtc']").addClass('disabled');
        $("#tabEventMaster li[id='tabParticipants']").addClass("active");
        $(".tab-content div[id='tpForm']").removeClass("active");
        $(".tab-content div[id='tpParticipants']").addClass("active");
        //$("#tabEventMaster .nav-tabs a[href='#tpParticipants']").tab('show');
        $("button[id$=btnRequest]").hide();
        $("button[id$=btnSave]").hide();
    }

    /* 시작일자 이벤트 */
    $(".form-date-hour-min").on("change", function () {
        var inputDate = $(".form-date-hour-min input").val();
        try {
            var tmp = inputDate.split(" ");

            var startDate = new Date(tmp[0].toString() + "T" + tmp[1].toString() + ":00");
            var hour = startDate.getHours();
            var minute = startDate.getMinutes();

            // 화면에서 Today를 클릭하면 현재 시간을 반환
            // 5분단위로 시작시간을 입력하므로 
            var modResult = minute % 5;
            if (modResult != 0) {
                //5분 단위가 아니면
                minute = 0;
                var year = startDate.getFullYear();
                var month = addZero(startDate.getMonth() + 1);
                var day = addZero(startDate.getDate());

                $(".form-date-hour-min input").val(year.toString() + "-" + month.toString() + "-" + day.toString() + " " + addZero(hour) + ":" + addZero(minute));
            }

            $("#optAgendaStartHour").val(addZero(hour));
            minute = parseInt(minute / 10) * 10;
            $("#optAgendaStartMinute").val(addZero(minute));
        } catch (e) {
            console.log(e);
        }
    });

    var eventID = $('input[id$=hddEventID]').val();
    if (eventID == "E0007") {
        $("#tabAddParticipant li[id='liKoreaHCP']").hide();
        $("#tabAddParticipant a[href='#tabOtherHCP']").tab('show');
    }

    if (eventID != "E0010") {
        $("#divFMVGuideLink").hide();
    }
}

function addZero(i) {
    if (i < 10) {
        i = "0" + i;
    }
    return i;
}


function setEventComplete() {
    var optEventComplete = $("input[id$=hddEventCompleteOption]").val();

    var opts = optEventComplete.split(":");
    if (opts.length == 2) {
        var showComment = opts[0];
        var showAttach = opts[1];

        if (showComment == "Y") {
            $("#divEventCompleteComment").show();
        } else {
            $("#divEventCompleteComment").hide();
        }

        if (showAttach == "Y") {
            $("#divAttachFiles_EventComplete").show();
        } else {
            $("#divAttachFiles_EventComplete").hide();
        }
    }
}

/* 이벤트 공통 */
function disableGeneralInformation() {
    $("#acbReviewer").prop("disabled", true); //Reviewer
    $("#divGI_FirstColumn input").prop("disabled", true);
    $("#divGI_FirstColumn button").prop("disabled", true);
    $("#divGI_FirstColumn span").css("pointer-events", "none");
    $('#divAttachFiles_EventCommon #fileupload').parent().hide();
    $("#divGI_FirstColumn #taProduct").prop("disabled", true);
    $("#divGI_FirstColumn select").prop("disabled", true);
    $("#divGI_FirstColumn textarea").prop("disabled", true);
    $(".text-remove").hide();
}

/* Cost Plan */
function disableCostPlan() {
    $("#divInputCostPlan button").prop("disabled", true);
    $("#divInputCostPlan .tab-cont-area").addClass('closed');
    $("#divInputCostPlan .btn-panel-open").hide();
    $('#divInputCostPlan #fileupload').parent().hide();
}

/* 참석자 */
function disableParticipants() {
    $("#divInputParticipants .tab-cont-area").addClass('closed');
    $("#divInputParticipants .btn-panel-open").hide();
    $("#btnIsAttended").prop("disabled", true);
    $("#btnIsNotAttended").prop("disabled", true);
    $("#btnDeleteParticipant").prop("disabled", true);
    //$("#jsGridPraticipants").jsGrid("fieldOption", 0, "width", "0px");
}

/* Agenda */
function disableAgenda() {
    $("#divInputAgenda .tab-cont-area").addClass('closed');
    $("#divInputAgenda .btn-panel-open").hide();
    $(".add-agenda").hide();
    $("#role_information button").prop("disabled", true);
    $("#role_information input").prop("disabled", true);
    $("#role_information span").css("pointer-events", "none");
    $("#role_information select").prop("disabled", true);
    $("#role_information textarea").prop("disabled", true);
    $("#btnPrintYourDoces").prop("disabled", false); //프린트
    $("#role_information .close").prop("disabled", false); //우측 Close
}

/* Payment */
function disablePayment() {
    $("#tpExpenseUpload .tab-cont-area").addClass('closed');
    $("#tpExpenseUpload .btn-panel-open").hide();
    $("#tpExpenseUpload button").prop("disabled", true);
    $("#btnDeleteInputSRM").prop("disabled", true);
    $("#btnSaveInputSRM").prop("disabled", true);
    $("#btnInputPoNumber").prop("disabled", true);
}

function disableEtc() {
    $("#tpEtc .tab-cont-area").addClass('closed');
    $("#tpEtc .btn-panel-open").hide();
    $("#tpEtc button").hide();
}

/* 미선택시 메시지 */
function showNotSelected(message) {
    fn_showWarning({
        title: "Caution",
        message: message
    })
}

