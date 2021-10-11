$(function () {
    // 일자, 시간 입력
    $('.form_datetime').datetimepicker({
        format: 'yyyy-mm-dd hh:ii',
        weekStart: 1,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        forceParse: 0,
        minuteStep: 10
    });

    // 일자 입력
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

    // 숫자만 입력 받음
    $(".numberOnly").on("keyup", fn_ValidationNumberOnly);

    // type 에 따른 기간 설정 변경
    $("#optType").on("change", function () {
        $("#hdivLectureDate").css("display", "none");
        $("#hdivConsultingDate").css("display", "none");

        $("#hdiv" + $(this).val() + "Date").css("display", "");
    });

    // 추가 화면 보이기
    $("#hbtnAdd").on("click", function () { clearData(); $("#hdivlist").hide(); $("#hdivadd").show(); })
    // 리스트 보이기
    $("#btnCancel").on("click", function () { $("#hdivlist").show(); $("#hdivadd").hide(); })

    // 리스트 가져오기
    SetGridDate();

    function SetGridDate() {
        var item = GridData();

        $("#jsGridICCList").jsGrid({
            width: "100%",
            height: "500px",
            sorting: true,
            paging: false,
            autoload: true,
            filtering: true,
            controller: {
                data: item,
                loadData: function (filter) {
                    return $.grep(this.data, function (item) {
                        return (!filter.TYPE || item.TYPE.toLowerCase().indexOf(filter.TYPE.toLowerCase()) > -1)
                            && (!filter.YEAR || item.YEAR.toLowerCase().indexOf(filter.YEAR.toLowerCase()) > -1)
                            && (!filter.HCP_NAME || item.HCP_NAME.toLowerCase().indexOf(filter.HCP_NAME.toLowerCase()) > -1)
                            && (!filter.HCO_NAME || item.HCO_NAME.toLowerCase().indexOf(filter.HCO_NAME.toLowerCase()) > -1)
                            && (!filter.AMOUNT_KOR || item.AMOUNT_KOR.toString().replace(",", "").indexOf(filter.AMOUNT_KOR) > -1)
                            && (!filter.COMMENT || item.COMMENT.toLowerCase().indexOf(filter.COMMENT.toLowerCase()) > -1);
                    });
                }
            },
            rowDoubleClick: function (args) {
                var item = args.item;

                clearData();
                // 데이터 넣기
                $("#hhdIccId").val(item.ICC_ID);
                $("#optType").val(item.TYPE);
                $("#htxtYear").val(item.YEAR);

                if (item.TYPE == "Lecture") {
                    //$("#datDateTime").val(item.START_TIME);
                    $('.form_datetime').datetimepicker('update', item.START_TIME.substring(0, 10));
                }
                else {
                    //$("#datStartDate").val(item.START_TIME);
                    //$("#datEndDate").val(item.END_TIME);
                    $('.form_Startdate').datetimepicker('update', item.START_TIME.substring(0, 10));
                    $('.form_Enddate').datetimepicker('update', item.END_TIME.substring(0, 10));
                }
                $("#htxtAddress").val(item.ADDRESS);
                $("#htxtPurpose").val(item.PURPOSE);
                $("#htxtHCPName").val(item.HCO_NAME + "(" + item.HCP_NAME + ")");
                $("#htxtHCPName").data("HCPCode", item.HCP_CODE);
                $("#htxtHCPName").data("HCPName", item.HCP_NAME);
                $("#htxtHCPName").data("HCOCode", item.HCO_CODE);
                $("#htxtHCPName").data("HCOName", item.HCO_NAME);
                $("#htxtSubject").val(item.SUBJECT);
                $("#htxtHostCountry").val(item.HOST_COUNTRY);
                $("#htxtInvitingCountry").val(item.INVITING_COUNTRY);
                $("#htxtPaymentCountry").val(item.PAYMENT_COUNTRY);
                //$("#datPaymentDate").val(item.PAYMENT_DATE);
                $('.form_PaymentDate').datetimepicker('update', item.PAYMENT_DATE.substring(0, 10));
                $("#htxtCurrency").val(item.CURRENCY);
                $("#htxtCurrencyAmount").val(fn_AddComma(item.AMOUNT_CURRENCY));
                $("#htxtKRWAmount").val(fn_AddComma(item.AMOUNT_KOR));
                $("input[name=optFlightClass]").each(function () {
                    if (this.value == item.FLIGHT_CLASS)
                        $(this).prop("checked", "checked");
                });
                //$("#datFlightCheckin").val(item.FLIGHT_CHECKIN_DATE);
                $('.form_FlightCheckin').datetimepicker('update', item.FLIGHT_CHECKIN_DATE.substring(0, 10));
                //$("#datFlightCheckout").val(item.FLIGHT_CHECKOUT_DATE);
                $('.form_FlightCheckout').datetimepicker('update', item.FLIGHT_CHECKOUT_DATE.substring(0, 10));
                $("#htxtFlightComment").val(item.FLIGHT_COMMENT);
                //$("#datAccommodationCheckin").val(item.ACCOMMODATION_CHECKIN_DATE);
                $('.form_AccommodationCheckin').datetimepicker('update', item.ACCOMMODATION_CHECKIN_DATE.substring(0, 10));
                //$("#datAccommodationCheckout").val(item.ACCOMMODATION_CHECKOUT_DATE);
                $('.form_AccommodationCheckout').datetimepicker('update', item.ACCOMMODATION_CHECKOUT_DATE.substring(0, 10));
                $("#htxtAccommodationComment").val(item.ACCOMMODATION_COMMENT);
                $("#htxtMealBeverage").val(fn_AddComma(item.AMOUNT_MEAL_BEVERAGE));
                $("#htxtTransportation").val(fn_AddComma(item.AMOUNT_TRANSPORTAION));
                $("#htxtComment").val(item.COMMENT);
                $("input[name=optKRPIA]").each(function () {
                    if (this.value == item.AGREE_KRPIA)
                        $(this).prop("checked", "checked");
                });

                //첨부파일 추가
                fn_SelectEventAttachFiles();

                $("#hdivlist").hide();
                $("#hdivadd").show();
            },
            fields: [
                //{ name: "ICC_ID", title: "ID", type: "text", css: "hide", width: 0 },
                { name: "TYPE", title: "Type", type: "text", width: 50 },
                { name: "YEAR", title: "해당 연도", type: "text", width: 50 },
                { name: "HCP_NAME", title: "연자명", type: "text", width: 150 },
                { name: "HCO_NAME", title: "소속", type: "text", width: 150 },
                {
                    name: "AMOUNT_KOR", title: "지급액 (한화)", type: "number", width: 100,
                    itemTemplate: function (value, item) {
                        return fn_AddComma(value);
                    }
                },
                { name: "COMMENT", title: "Comment", type: "text", width: 150 },
            ]
        });
    }

    function GridData() {
        var retVal = "";
        $.ajax({
            type: "GET",
            url: EVENT_SERVICE_URL + "/SelectICCMasterList",
            async: false,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (returnData) {
                retVal = returnData;
            }
        });
        return retVal;
    }

    function fn_ValidationNumberOnly() {
        this.value = this.value.replace(/[^0-9]/, '');
        $($(this).attr("value-field")).val(this.value.replace(/,/ig, ""));
        this.value = this.value;
    }


    function clearData() {
        $("#hdivadd input[type=hidden]").val("0");
        $("#hdivadd input[type=text]").val("");
        $("#divAttachFiles .attach-list").html("");
    }

    $("#btnSave").click(function () {
        var iccId = ($("#hhdIccId").val() == "" ? "0" : $("#hhdIccId").val());
        var type = $("#optType").val();
        var year = $("#htxtYear").val();
        var starttime, endtime;
        if (type == "Lecture") {
            starttime = $("#datDateTime").val();
            endtime = "";
        }
        else {
            starttime = $("#datStartDate").val();
            endtime = $("#datEndDate").val();
        }
        var address = $("#htxtAddress").val();
        var purpose = $("#htxtPurpose").val();
        var hcpinfo = $("#htxtHCPName").data();
        var hcpCode = hcpinfo.HCPCode;
        var hcpName = hcpinfo.HCPName;
        var hcoCode = hcpinfo.HCOCode;
        var hcoName = hcpinfo.HCOName;
        var subject = $("#htxtSubject").val();
        var hostCountry = $("#htxtHostCountry").val();
        var invitingCountry = $("#htxtInvitingCountry").val();
        var paymentCountry = $("#htxtPaymentCountry").val();
        var paymentDate = $("#datPaymentDate").val();
        var currency = $("#htxtCurrency").val();
        var amountCurrency = $("#htxtCurrencyAmount").val();
        var amountKor = $("#htxtKRWAmount").val();
        var flightClass = $("input[name=optFlightClass]:checked").val();
        var flightCheckin = $("#datFlightCheckin").val();
        var fligntCheckout = $("#datFlightCheckout").val();
        var flightComment = $("#htxtFlightComment").val();
        var accommodationCheckin = $("#datAccommodationCheckin").val();
        var accommodationCheckout = $("#datAccommodationCheckout").val();
        var accommodationComment = $("#htxtAccommodationComment").val();
        var amountMealBeverage = $("#htxtMealBeverage").val();
        var amountTransportaion = $("#htxtTransportation").val();
        var comment = $("#htxtComment").val();
        var agreeKrpia = $("input[name=optKRPIA]:checked").val();
        var userID = $("input[id$=hddUserID]").val();

        var liFiles = $("#divAttachFiles .attach-list").find("li");
        var attachFiles = [];
        if (liFiles.length > 0) {
            //첨부파일이 존재하는 경우
            for (var i = 0; i < liFiles.length; i++) {
                attachFiles.push($(liFiles[i]).data("attach-file"));
            }
        }


        /* 필수입력 체크 */
        var msgFillOut = "";
        if (type.trim().length < 1) { msgFillOut = "Type"; }
        if (year.trim().length < 1) { if (msgFillOut.length > 0) msgFillOut += ", "; msgFillOut += "해당연도"; }
        if (type == "Lecture") {
            if (starttime.trim().length < 1) { if (msgFillOut.length > 0) msgFillOut += ", "; msgFillOut += "일자"; }
        }
        else {
            if (starttime.trim().length < 1 || endtime.trim().length < 1) { if (msgFillOut.length > 0) msgFillOut += ", "; msgFillOut += "일자"; }
        }
        if (address.trim().length < 1) { if (msgFillOut.length > 0) msgFillOut += ", "; msgFillOut += "장소"; }
        if (purpose.trim().length < 1) { if (msgFillOut.length > 0) msgFillOut += ", "; msgFillOut += "목적"; }
        if (hcpCode == null || hcpCode.trim().length < 1) { if (msgFillOut.length > 0) msgFillOut += ", "; msgFillOut += "연자명"; }
        if (subject.trim().length < 1) { if (msgFillOut.length > 0) msgFillOut += ", "; msgFillOut += "강연/자문 주제"; }
        if (hostCountry.trim().length < 1) { if (msgFillOut.length > 0) msgFillOut += ", "; msgFillOut += "행사 개최국"; }
        if (invitingCountry.trim().length < 1) { if (msgFillOut.length > 0) msgFillOut += ", "; msgFillOut += "연자 초청국"; }
        if (paymentCountry.trim().length < 1) { if (msgFillOut.length > 0) msgFillOut += ", "; msgFillOut += "지급 국가"; }
        if (paymentDate.trim().length < 1) { if (msgFillOut.length > 0) msgFillOut += ", "; msgFillOut += "지급 일자"; }
        if (currency.trim().length < 1) { if (msgFillOut.length > 0) msgFillOut += ", "; msgFillOut += "계약서 통화 단위"; }
        if (amountCurrency.trim().length < 1) { if (msgFillOut.length > 0) msgFillOut += ", "; msgFillOut += "지급액(계약서 통화 단위)"; }
        if (amountKor.trim().length < 1) { if (msgFillOut.length > 0) msgFillOut += ", "; msgFillOut += "지급액(한화)"; }
        if (flightClass.trim().length < 1 || flightCheckin.trim().length < 1 || fligntCheckout.trim().length < 1) { if (msgFillOut.length > 0) msgFillOut += ", "; msgFillOut += "항공(Flight)"; }
        if (accommodationCheckin.trim().length < 1 || accommodationCheckout.trim().length < 1) { if (msgFillOut.length > 0) msgFillOut += ", "; msgFillOut += "숙박(Accommodation)"; }
        if (amountMealBeverage.trim().length < 1) { if (msgFillOut.length > 0) msgFillOut += ", "; msgFillOut += "식음료(Meal&Beverage)"; }
        if (amountTransportaion.trim().length < 1) { if (msgFillOut.length > 0) msgFillOut += ", "; msgFillOut += "교통비(Transportation)"; }
        if (attachFiles == null || attachFiles.length < 1) { if (msgFillOut.length > 0) msgFillOut += ", "; msgFillOut += "Attachment"; }

        if (msgFillOut.length > 0) {
            fn_showInformation({
                title: "Please fill out below fields.",
                message: msgFillOut
            });
            return;
        }
        //저장
        var iccinfo = {
            ICC_ID: parseInt(iccId),
            TYPE: type,
            YEAR: year,
            START_TIME: starttime,
            END_TIME: endtime,
            ADDRESS: address,
            PURPOSE: purpose,
            HCP_CODE: hcpCode,
            HCP_NAME: hcpName,
            HCO_CODE: hcoCode,
            HCO_NAME: hcoName,
            SUBJECT: subject,
            HOST_COUNTRY: hostCountry,
            INVITING_COUNTRY: invitingCountry,
            PAYMENT_COUNTRY: paymentCountry,
            PAYMENT_DATE: paymentDate,
            CURRENCY: currency,
            AMOUNT_CURRENCY: fn_RemoveComma(amountCurrency),
            AMOUNT_KOR: fn_RemoveComma(amountKor),
            FLIGHT_CLASS: flightClass,
            FLIGHT_CHECKIN_DATE: flightCheckin,
            FLIGHT_CHECKOUT_DATE: fligntCheckout,
            FLIGHT_COMMENT: flightComment,
            ACCOMMODATION_CHECKIN_DATE: accommodationCheckin,
            ACCOMMODATION_CHECKOUT_DATE: accommodationCheckout,
            ACCOMMODATION_COMMENT: accommodationComment,
            AMOUNT_MEAL_BEVERAGE: fn_RemoveComma(amountMealBeverage),
            AMOUNT_TRANSPORTAION: fn_RemoveComma(amountTransportaion),
            COMMENT: comment,
            AGREE_KRPIA: agreeKrpia,
            CREATOR_ID: userID,
        }

        var iccData = {
            iccinfo: iccinfo,
            attachFiles: attachFiles,
        }

        $.ajax({
            url: EVENT_SERVICE_URL + "/MergeICCMaster",
            type: "POST",
            data: JSON.stringify(iccData),
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (iccId > 0)
                    alert("수정되었습니다.");
                else
                    alert("생성되었습니다.");

                clearData();

                SetGridDate();
                $("#btnCancel").click();
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });

    });
});