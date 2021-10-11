$(function () {
    // 조회
    selectScientificExchangedMeeting();
});

/* Scientific Exchanged Meeting 조회 */
function selectScientificExchangedMeeting() {
    PROCESS_ID = $("input[id$=hddProcessID]").val();
    var status = $("input[id$=hddProcessStatus]").val();
    if (PROCESS_ID && status != "Temp") {
        $.ajax({
            url: EVENT_SERVICE_URL + "/SelectScientificExchangedMeeting/" + PROCESS_ID,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                displayScientificExchangedMeeting(data);
                selectScientificExchangedMeetingProducts();
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

function selectScientificExchangedMeetingProducts() {
    $.ajax({
        url: EVENT_SERVICE_URL + "/SelectScientificExchangedMeetingProducts/" + PROCESS_ID,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            displayScientificExchangedMeetingProducts(data);
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}

function displayScientificExchangedMeetingProducts(products) {
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

function displayScientificExchangedMeeting(data) {

    $("span[id$=hspanRequester]").text(data.REQUESTER_NAME);
    $("span[id$=hspanOrganization]").text(data.ORGANIZATION_NAME);
    $("span[id$=hspanRetaentionPeriod]").text(data.RETENTION_PERIOD);
    //$("span[id$=hspanRequestedDate]").text(data.REQUEST_DATE);
    $("span[id$=hspanEventKey]").text(data.EVENT_KEY);
    $("span[id$=hspanStatus]").text(data.PROCESS_STATUS);
    $("#txtSubject").val(data.SUBJECT);
    //$("#datStartTime").val(data.START_TIME.substring(0, 16));
    $(".form_StartTime").datetimepicker('update', data.START_TIME.substring(0, 16));
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
    if (dtStart.length > 0) dtStart = dtStart + ":00";

    /* 필수입력 체크 */
    var msgFillOut = "";
    if (subject.trim().length < 1) {
        msgFillOut = "Subject";
    }
    if (dtStart < 1) {
        if (msgFillOut.length > 0) msgFillOut += ", ";
        msgFillOut += "Start Time";
    }

    var crmProducts = $(".crm-product-searcher").data("crm-products");
    if (!crmProducts || crmProducts.length < 1) {
        if (msgFillOut.length > 0) msgFillOut += ", ";
        msgFillOut += "Product";
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
    var SEMeeting = {
        PROCESS_ID: PROCESS_ID,
        SUBJECT: subject,
        EVENT_KEY: $("#hspanEventKey").text(),
        PROCESS_STATUS: status,
        REQUESTER_ID: $("input[id$=hddUserID]").val(),
        COMPANY_CODE: $("input[id$=hddCompanyCode]").val(),
        ORGANIZATION_NAME: $("[id$=hspanOrganization]").text(),
        LIFE_CYCLE: $("input[id$=hddLifeCycle]").val(),
        START_TIME: dtStart,
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

    var proSEMeeting = {
        SEMeeting: SEMeeting,
        products: products,
    }

    $.ajax({
        url: EVENT_SERVICE_URL + "/MergeScientificExchangedMeeting",
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