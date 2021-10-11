$(function () {
    // 조회
    selectProductSeminar();
});

/* Product Seminar 조회 */
function selectProductSeminar() {
    PROCESS_ID = $("input[id$=hddProcessID]").val();
    var status = $("input[id$=hddProcessStatus]").val();
    if (PROCESS_ID && status != "Temp") {
        $.ajax({
            url: EVENT_SERVICE_URL + "/SelectProductSeminar/" + PROCESS_ID,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                displayProductSeminar(data);
                selectProductSeminarProducts();
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

function selectProductSeminarProducts() {
    $.ajax({
        url: EVENT_SERVICE_URL + "/SelectProductSeminarProducts/" + PROCESS_ID,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            displayProductSeminarProducts(data);
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}

function displayProductSeminarProducts(products) {
    //var productTags = [];
    //for (var i = 0; i < products.length; i++) {
    //    var product = {
    //        PRODUCT_CODE: products[i].PRODUCT_CODE,
    //        PRODUCT_NAME: products[i].PRODUCT_NAME
    //    }

    //    productTags.push(product);
    //}
    //$('#taCRMProduct').textext()[0].tags().addTags(productTags);

    var productData = [];
    var processStatus = $("[id$=hspanStatus]").text();
    for (var i = 0; i < products.length; i++) {
        var item = products[i];
        //EventCommon.js에 적용이후에 데이터를 불러오는 문제로 삭제버튼을 상태에 따라 처리
        var $li;
        if (processStatus == "Saved" || processStatus == "Temp")
            $li = $("<li id='" + item.PRODUCT_CODE + "'><span class='btn btn-sm btn-gray'>" + item.PRODUCT_CODE + "(" + item.PRODUCT_NAME + ")" + "</span><a class='text-remove'></a></li>")
        else
            $li = $("<li id='" + item.PRODUCT_CODE + "'><span class='btn btn-sm btn-gray'>" + item.PRODUCT_CODE + "(" + item.PRODUCT_NAME + ")" + "</span></li>")
        $(".crm-product-searcher .product-list").append($li);
        var product = {
            PRODUCT_CODE: item.PRODUCT_CODE,
            PRODUCT_NAME: item.PRODUCT_NAME,
            PRODUCT_TYPE: item.PRODUCT_TYPE
        }

        productData.push(product);
    }
    $(".crm-product-searcher").data("crm-products", productData);

}

function displayProductSeminar(data) {

    $("span[id$=hspanRequester]").text(data.REQUESTER_NAME);
    $("span[id$=hspanOrganization]").text(data.ORGANIZATION_NAME);
    $("span[id$=hspanRetaentionPeriod]").text(data.RETENTION_PERIOD);
    //$("span[id$=hspanRequestedDate]").text(data.REQUEST_DATE);
    $("span[id$=hspanEventKey]").text(data.EVENT_KEY);
    //$("span[id$=hspanEventKey_1]").text(data.EVENT_KEY);
    $("span[id$=hspanStatus]").text(data.PROCESS_STATUS);
    $("#txtSubject").val(data.SUBJECT);
    //$("#datStartTime").val(data.START_TIME.substring(0, 16));
    $(".form_StartTime").datetimepicker('update', data.START_TIME.substring(0, 16));
    $("#txtAddress").val(data.ADDRESS_OF_VENUE);
    var selectionReason = data.VENUE_SELECTION_REASON;
    $(".chk-venue-selection-reason:checkbox").each(function () {
        var include = selectionReason.indexOf($(this).val());
        $(this).prop("checked", include > -1);
    });

    $("#txtReason").val(data.VENUE_SELECTION_REASON_MANUAL);

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
    //StatTime
    var dtStart = $("#datStartTime").val();
    if (dtStart.length == 16) dtStart = dtStart + ":00";
    //Venue Address
    var venue = $("#txtAddress").val();
    //Venue Selection Reason
    var checkedSelectionReason = "";
    var checkedCount = 0;
    $(".chk-venue-selection-reason:checkbox").each(function () {
        if (this.checked) {
            if (checkedSelectionReason.length < 1)
                checkedSelectionReason = $(this).val();
            else
                checkedSelectionReason += ";" + $(this).val();
            checkedCount++;
        }
    });
    var inputReason = $("#txtReason").val();
    //Purpose or Objective
    var checkedPurposeObjective = "";
    $(".chk-purpose-objective:checkbox").each(function () {
        if (this.checked) {
            if (checkedPurposeObjective.length < 1)
                checkedPurposeObjective = $(this).val();
            else
                checkedPurposeObjective += ";" + $(this).val();
        }
    });

    
    

    /* 필수입력 체크 */
    var msgFillOut = "";
    if (subject.trim().length < 1) {
        msgFillOut = "Subject";
    }
    if (dtStart < 1) {
        if (msgFillOut.length > 0) msgFillOut += ", ";
        msgFillOut += "Start Time";
    }
    if (checkedCount < 3 && inputReason.length < 1) {
        if (msgFillOut.length > 0) msgFillOut += ", ";
        msgFillOut += "Reason";
    }
    //var tags = $("#taCRMProduct").textext()[0].tags()._formData;
    var crmProducts = $(".crm-product-searcher").data("crm-products");
    if (!crmProducts || crmProducts.length < 1) {
        if (msgFillOut.length > 0) msgFillOut += ", ";
        msgFillOut += "Product";
    }

    if (venue.trim().length < 1) {
        if (msgFillOut.length > 0) msgFillOut += ", ";
        msgFillOut += "Venue(Address)";
    }

    if (checkedPurposeObjective.length < 1) {
        if (msgFillOut.length > 0) msgFillOut += ", ";
        msgFillOut += "Purpose/Objective";
    }

    //<VeeVa Roll-out : Create event by CRM user>
    var isCRMuser = $("#hddisCRMUser").val();
    if (isCRMuser == "True") {        
        if (fn_VeeVaCRMuser(dtStart)) {
            if (msgFillOut.length > 0) msgFillOut += ", ";
            msgFillOut += "CRM user 의 경우 3월 11일 부터 31일까지의 Event 만 입력 가능합니다.";
        }
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
    var seminar = {
        PROCESS_ID: PROCESS_ID,
        SUBJECT: subject,
        EVENT_KEY: $("#hspanEventKey").text(),
        PROCESS_STATUS: status,
        REQUESTER_ID: $("input[id$=hddUserID]").val(),
        COMPANY_CODE: $("input[id$=hddCompanyCode]").val(),
        ORGANIZATION_NAME: $("[id$=hspanOrganization]").text(),
        LIFE_CYCLE: $("input[id$=hddLifeCycle]").val(),
        START_TIME: dtStart,
        PRODUCT_LIST: "",
        ADDRESS_OF_VENUE: venue,
        VENUE_SELECTION_REASON: checkedSelectionReason,
        VENUE_SELECTION_REASON_MANUAL: inputReason,
        PURPOSE_OBJECTIVE: checkedPurposeObjective,
        COST_PLAN: "",
        IS_DISUSED: "N",
        CREATOR_ID: USER_ID,
        UPDATER_ID: USER_ID
    }

    var products = [];
    for (var i = 0; i < crmProducts.length; i++) {
        var pro = crmProducts[i];
        var product = {
            PROCESS_ID: PROCESS_ID,
            PRODUCT_IDX: 0,
            PRODUCT_CODE: pro.PRODUCT_CODE,
            PRODUCT_NAME: pro.PRODUCT_NAME,
            PRODUCT_TYPE: pro.PRODUCT_TYPE,
            IS_DELETED: "N",
            CREATOR_ID: USER_ID
        };
        products.push(product);
    }

    var proSeminar = {
        seminar: seminar,
        products: products,
    }

    $.ajax({
        url: EVENT_SERVICE_URL + "/MergeProductSeminar",
        type: "POST",
        data: JSON.stringify(proSeminar),
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