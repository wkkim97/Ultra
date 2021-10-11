var state = {
    selected_row: null,
    selected_status: 'ALL',
    checked_rows: [],
    request_type: null,
    show_rows: 15
};
var CONST = {
    USER_TYPE: $('#cpHolderBottom_userType').val(),
    //USER_TYPE: "END USER",
    NEW : "신청되었습니다.",
    PROCESSING: "자료를 생성중입니다.",
    COMPLETE: "처리가 완료되었습니다.",
    DELIVERED: "HCP 에게 전달 되었습니다.",
    APPROVER: "sumin.jo@bayer.com|emmayunjung.cho@bayer.com",
    //APPROVER: "bumyoung.kim@bayer.com",
    //CC: "Jaehoon.CHUNG@iqvia.com|sumin.jo@bayer.com|daero.lee@bayer.com"
    CC: ""
}
//freeze 옵션 : const 변경을 막음 test 로 잠시 품
//CONST = Object.freeze(CONST);

$(function () {
    waitMe();
    init();
});

//Ajax 로딩 처리 
function waitMe() {
    $.ajaxSetup({
        beforeSend: function (xhr) {
            $('body').waitMe({
                effect: 'win8',
                text: 'Please wait...'
            });
        },
        complete: function () {
            $('body').waitMe("hide");
        },
    });
}
//initialize plugin , render
function init() {
    // plug-in datetime picker
    $('.form_dateTime').datetimepicker({
        weekStart: 0,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        forceParse: 0,
        minuteStep: 60,
        minView: 2,
        maxView: 2,
        format: "yyyy-mm-dd"
    });

    //input[type=number]는 오직 숫자값만 입력 받도록 한다
    var tags = document.querySelectorAll("input[type='number']");
    var restrictNumber = function (e) {
        var newValue = this.value.replace(/\D/gi, "");
        this.value = newValue;
    }
    for (i = 0; i < tags.length; i++) {
        tags[i].addEventListener('input', restrictNumber);
    }

    //속성 number-comma=true 인 경우 천단위 구분표시를 삽입한다
    tags = document.querySelectorAll("*[number-comma='true']");
    for (i = 0; i < tags.length; i++) {
        tags[i].textContent = numberWithCommas(tags[i].textContent);
    }

    //requesterText
    var requesterText = $('#cpHolderBottom_hhdUserName').val() + ' | ' + $('#cpHolderBottom_hhdUserOrgName').val();
    $('#requesterText').text(requesterText);

    //period select year option
    resetSelectYear();

    loadGrid();
}


//데이터 표 그리기
function loadGrid() {

    var data = {
        USER_ID: $("#cpHolderBottom_hhdUserID").val(),
        USER_TYPE: CONST.USER_TYPE
    }
     /*
    * AJAX
    * */
    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/SelectHCPInquiryList",
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var sortedData = [];

            var count = {
                ALL: data.length,
                REQUESTING: 0,
                PROCESSING: 0,
                COMPLETE: 0,
                DELIVERED : 0,
            };
            //상태별 데이터 카운트
            for (i = 0; i < data.length; i++) {
                count[data[i].INQUIRY_STATUS]++;
                sortedData.unshift(data[i]);
            }
            for (key in count) {
                $('#count-' + key.toLowerCase()).text(count[key]);
            }
            //그리드 초기화
            if ($("#HCPInquiryGrid").jsGrid) $("#HCPInquiryGrid").jsGrid("destroy");
            //그리드 생성
            $("#HCPInquiryGrid").jsGrid({
                width: "100%",
                filtering: true,
                sorting: true,
                paging: true,
                autoload: true,
                pageButtonCount: 5,
                pageNavigatorNextText: "...",
                pageNavigatorPrevText: "...",
                pageSize: state.show_rows,

                controller: {
                    data: sortedData,
                    loadData: function (filter) {
                        return $.grep(this.data, function (item) {
                            return (!filter.FULL_NAME || item.FULL_NAME.toLowerCase().indexOf(filter.FULL_NAME.toLowerCase()) > -1)
                                && (!filter.CREATE_DATE || getDateStr(item.CREATE_DATE.toLowerCase()).indexOf(filter.CREATE_DATE.toLowerCase()) > -1)
                                && (!filter.ORGANIZATION_NAME || item.ORGANIZATION_NAME.toLowerCase().indexOf(filter.ORGANIZATION_NAME.toLowerCase()) > -1)
                                && (!filter.SPECIALTY_NAME || item.SPECIALTY_NAME.toLowerCase().indexOf(filter.SPECIALTY_NAME.toLowerCase()) > -1)
                                && (!filter.CUSTOMER_NAME || item.CUSTOMER_NAME.toLowerCase().indexOf(filter.CUSTOMER_NAME.toLowerCase()) > -1)
                                && (state.selected_status == 'ALL' ? true : item.INQUIRY_STATUS == state.selected_status)
                        });
                    }
                },
                rowDoubleClick: function (args) {

                    state.selected_row = args.item;
                    //console.log(args.item);
                    loadModal(args.item);
                    removeSelection();
                },
                fields: [
                    {
                        type: "control", width: 30, editButton: false, deleteButton: false,
                        filterTemplate: function () {
                            var checkStr = "<input type='checkbox' id='click-all' class='form-control' onclick='checkAll(this)'>"
                            return CONST.USER_TYPE == 'KEY USER' && (state.selected_status.toLowerCase() == 'requesting') ? checkStr : "";
                        },
                        headerTemplate: function () {
                            return CONST.USER_TYPE == 'KEY USER' && (state.selected_status.toLowerCase() == 'requesting') ? "All" : "";
                        },
                        itemTemplate: function (value, item) {

                            var $customCheckBox = $("<input id='" + item.HCP_INQUIRY_REQUEST_ID + "' data-email='" + item.MAIL_ADDRESS + "' type='checkbox'>").attr({ class: "form-control girdCheck " + (CONST.USER_TYPE == 'KEY USER' && state.selected_status == item.INQUIRY_STATUS && (state.selected_status == 'REQUESTING') ? '' : 'hidden') })
                                .click(function (e) {

                                });
                            return $("<div>").append($customCheckBox);
                        }
                    },
                    { name: "FULL_NAME", title: "Requester", type: "text" },
                    {
                        name: "CREATE_DATE", title: "Request Date", type: "text",
                        itemTemplate: function (value, item) {
                            return getDateStr(value);
                        }
                    },
                    { name: "ORGANIZATION_NAME", title: "Organization name", type: "text" },
                    { name: "SPECIALTY_NAME", title: "Specialty name", type: "text" },
                    {
                        name: "CUSTOMER_NAME", title: "Name", type: "text",
                        itemTemplate: function (value, item) {
                            return "<a href='#' class='link text-ellipsis' onclick='fn_DetailView(this);'>" + value + "</a>";
                        }
                    },
                    {
                        name: "", title: "Period", type: "text", filtering: false,
                        itemTemplate: function (value, item) {
                            return item.YEAR_FROM + " - " + item.YEAR_TO;
                        }
                    },
                    {
                        name: "INQUIRY_STATUS", title: "Status", type: "text", filtering: false, itemTemplate: function (value, item) {
                            return "<span class='badge status " + value.toLowerCase() + "'>" + value.charAt(0).toUpperCase() + value.slice(1).toLowerCase() + "</span>"
                        }
                    },
                    {
                        name: "REMARK", title: "Complete Date", type: "text", filtering: false, itemTemplate: function (value, item) {
                            return "<span>" + value + "</span>";
                            //if (item.INQUIRY_STATUS == 'COMPLETE') {
                            //    return "<span>" + getDateStr(value) + "</span>";
                            //}
                        }
                    },
                    {
                        name: "DELIVERED_TO_HCP", title: "Delivery Date", type: "text", filtering: false, itemTemplate: function (value, item) {
                            return "<span>" + value + "</span>";
                            //if (item.INQUIRY_STATUS == 'COMPLETE') {
                            //    return "<span>" + getDateStr(value) + "</span>";
                            //}
                        }
                    }
                ]
            });

            //Status 지정
            var status = $("input[id$=hhdStatus]").val();
            if (status) {
                status = status.toUpperCase();
                $('button.btn.btn-sm[data-value=' + status + ']').click();
                $("input[id$=hhdStatus]").val(null)
            }

            $("#HCPInquiryGrid").jsGrid("sort", { field: "CREATE_DATE", order: "desc" });
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}
//아이패드용 모달 오픈 함수
function fn_DetailView(obj) {
    $(obj).dblclick();
}
//Check All 클릭 클릭 이벤트
function checkAll(e) {
    //check 토글
    if ($(e).prop('checked')) {
        $('.girdCheck').not(':checked').click();
    } else {
        $('.girdCheck:checked').click();
    }
}
//Modal 열기 함수
function loadModal(item) {
    console.log(item);
    //입력 폼 초기화
    $('#log').html(null);
    $('#contentModal *[disabled]').removeAttr('disabled');
    $('#contentModal input[type=text], #contentModal input[type=file], #contentModal textarea').val(null);
    $('#contentModal select option:selected').prop('selected', false);
    $('#contentModal select option:first-child').prop('selected', true);
    $('#contentModal input[type=radio]:checked').prop('checked', false);
    $('#contentModal input[type=checkbox]:checked').prop('checked', false);
    $('#result-wrapper').addClass('hidden');
    $('#name_text').text('');
    $('#org_text').text('');
    $('#spe_text').text('');
    $('#period_text').text('');
    $('#delivery_text').text('');
    $('#delivery_text_toHCP').text('');

    var processStatus = state.selected_row.INQUIRY_STATUS
    if (processStatus == 'NEW') {
        $("input[id$=hddProcessStatus]").val('Temp');
        $('#request-type').val(processStatus);
    }
    else {
        $("input[id$=hddProcessStatus]").val('NotTemp');
    }
    $("input[id$=hddProcessID]").val(state.selected_row.HCP_INQUIRY_REQUEST_ID);
    var scanTarget = $('#contentModal *[data-all-hidden], #contentModal *[data-admin-hidden], #contentModal *[data-user-hidden], #contentModal *[data-all-disabled], #contentModal *[data-admin-disabled], #contentModal *[data-user-disabled]');
    
    //TEST 용
    CONST["USER_TYPE"] = $('#cpHolderBottom_userType').val();
    
    //Status에 따른 노출여부 확인
    for (i = 0; i < scanTarget.length; i++) {
        var jqTarget = $(scanTarget[i]);
        //modal 콘텐츠 hidden
        if ((CONST.USER_TYPE == 'KEY USER' && jqTarget.attr('data-admin-hidden') && jqTarget.attr('data-admin-hidden').indexOf(state.selected_row.INQUIRY_STATUS.toLowerCase()) != -1)
            || CONST.USER_TYPE == 'END USER' && jqTarget.attr('data-user-hidden') && jqTarget.attr('data-user-hidden').indexOf(state.selected_row.INQUIRY_STATUS.toLowerCase()) != -1) {
            jqTarget.addClass('hidden');
        } else if (jqTarget.attr('data-all-hidden') && jqTarget.attr('data-all-hidden').indexOf(state.selected_row.INQUIRY_STATUS.toLowerCase()) != -1) {
            jqTarget.addClass('hidden');
        } else {
            jqTarget.removeClass('hidden');
        }
        //modal 콘텐츠 disabled
        if (CONST.USER_TYPE == 'KEY USER' && jqTarget.attr('data-admin-disabled') && jqTarget.attr('data-admin-disabled').indexOf(state.selected_row.INQUIRY_STATUS.toLowerCase()) != -1
            || CONST.USER_TYPE == 'END USER' && jqTarget.attr('data-user-disabled') && jqTarget.attr('data-user-disabled').indexOf(state.selected_row.INQUIRY_STATUS.toLowerCase()) != -1) {
            jqTarget.attr('disabled', 'true');
        } else if (jqTarget.attr('data-all-disabled') && jqTarget.attr('data-all-disabled').indexOf(state.selected_row.INQUIRY_STATUS.toLowerCase()) != -1) {
            jqTarget.attr('disabled', 'true');
        } else {
            jqTarget.removeAttr('disabled');
        }
    }
    //Class에 따른 노출여부 확인
    if (!state.selected_row.REQUEST_TYPE) {
        $('.update-item, .new-item').addClass('hidden');
    } else if (state.selected_row.REQUEST_TYPE == 'NEW') {
        $('.update-item:not(.new-item.update-item)').addClass('hidden');
    } else if (state.selected_row.REQUEST_TYPE == 'UPDATE') {
        $('.new-item:not(.new-item.update-item)').addClass('hidden');
    }

    if (item) {
        $('#request-type').val(item.REQUEST_TYPE);

        //텍스트 노출
        $('#name').val(item.CUSTOMER_NAME);
        $('#org').val(item.ORGANIZATION_NAME);
        $('#spe').val(item.SPECIALTY_NAME);

        $('#requesterText').text(item.FULL_NAME + ' | ' + item.ORG_ACRONYM);
        $('#name_text').text(item.CUSTOMER_NAME);
        $('#org_text').text(item.ORGANIZATION_NAME);
        $('#spe_text').text(item.SPECIALTY_NAME);
        $('#period_text').text(item.YEAR_FROM + " - " + item.YEAR_TO);
        $('#delivery_text').text(item.REMARK);
        $('#delivery_text_toHCP').text(item.DELIVERED_TO_HCP);

        var cb = function (data) {
            //render log
            var logStr = '';
            for (i in data) {
                i == 0 ? logStr += '<p style="color:red">' : logStr += '<p>';
                logStr += getDateStr(data[i].CREATE_DATE) + ' : ' + CONST[data[i].LOG_CATEGORY];
                logStr += '</p>';
            }
            logStr += '<p>' + getDateStr(item.CREATE_DATE) + ' : ' + CONST.NEW + '</p>';
            $('#log').html(logStr);

            $('#contentModal').modal();
        }
        getLog(item.HCP_INQUIRY_REQUEST_ID, cb);
    } else {
        //open modal
        $('#contentModal').modal();
    }
}
//Validation
function validation() {
    var requestType = $('#request-type').val();

    if (requestType != 'NEW' && requestType != 'UPDATE') {
        fn_showError({
            message: 'Process Error, Please re-search name'
        });
        return false;
    }

    if (requestType == 'NEW' && !$('.agree-check input[type=checkbox]:checked').val()) {
        fn_showError({
            message: 'Please check agreement'
        });
        return false;
    }
    if (requestType == 'NEW' && !$('#select-from').val()) {
        fn_showError({
            message: 'Please select "from" of period'
        });
        return false;
    }
    if (requestType == 'NEW' && !$('#select-to').val()) {
        fn_showError({
            message: 'Please select "to" of period'
        });
        return false;
    }
    return true;
}

//Period select option 생성
function resetSelectYear() {
    var thisYear = (new Date()).getFullYear();
    var htmlStr = "";
    for (var year = thisYear; year >= 2017; year--) {
        htmlStr += '<option value="' + year + '">' + year + '</option>';
    }
    $("#select-from").html(htmlStr);
    $("#select-to").html(htmlStr);
}
//이벤트 바인딩
//year 선택 이벤트
function selectYear(type, e) {
    var val = $(e).val();
    var thisYear = (new Date()).getFullYear();
    var htmlStr = "";
    if (type === "from") {
        var toVal = $("#select-to").val();
        for (var year = thisYear; year >= val; year--) {
            htmlStr += '<option value="' + year + '"';
            if (toVal == year) htmlStr += ' selected '
            htmlStr += '>' + year + '</option>';
        }
        $("#select-to").html(htmlStr);
    } else if (type === "to") {
        var fromVal = $("#select-from").val();
        for (var year = val; year >= 2017; year--) {
            htmlStr += '<option value="' + year + '"';
            if (fromVal == year) htmlStr += ' selected '
            htmlStr += '>' + year + '</option>';
        }
        $("#select-from").html(htmlStr);
    }
}
//상단 Filter btn 클릭 이벤트
function filterBtnFn(e) {

    state.checked_rows = [];
    state.selected_row = null;

    var status = $(e).attr('data-value');
    $('.statusBtns button.active').removeClass('active');
    $(e).addClass('active');
    state.selected_status = status;
    //complete 버튼 노출
    if (state.selected_status == 'PROCESSING' && CONST.USER_TYPE == 'KEY USER') {
        $('#sendConfirmBtn').addClass('hidden');
    } else if (state.selected_status == 'REQUESTING' && CONST.USER_TYPE == 'KEY USER') {
        $('#sendConfirmBtn').removeClass('hidden');
    } else {
        $('#sendConfirmBtn').addClass('hidden');
    }

    loadGrid();
}
//New 버튼 클릭 이벤트
function newBtnFn() {

    state.selected_row = { INQUIRY_STATUS : 'NEW' }
    $('.new-item, .update-item').addClass('hidden');
    $("input[id$=hddProcessStatus]").val('Temp');
    loadModal();

}
//Confirm 버튼 클릭 이벤트
function confirmBtnFn() {
    if (CONST.USER_TYPE != "KEY USER") return;
    /*
    * Ajax Log
    * */
    //console.log(state.selected_row);
    
    var cb = function () {
        loadGrid();
        //requester에게 메일 보내기
        SendMail([{
            HCP_INQUIRY_REQUEST_ID: state.selected_row.HCP_INQUIRY_REQUEST_ID,
            MAIL_ADDRESS: state.selected_row.MAIL_ADDRESS
        }], "ReportHCPInquiry", "PROCESSING", null);
        $('button.close[data-dismiss=modal]').click();
    }
    insertLog([{ HCP_INQUIRY_REQUEST_ID: state.selected_row.HCP_INQUIRY_REQUEST_ID }], 'PROCESSING', null, cb);

}
//Confirm 버튼 클릭 이벤트
function completeBtnFn() {
    if (CONST.USER_TYPE != "KEY USER") return;
    if (!$("#delivery-date").val()) {
        fn_showError({
            message: 'Please select delivery date'
        });
        return;
    }

    
    
    var data = getModalDataObj(state.selected_row, 'COMPLETE');

    
    /*
    * Ajax
    * */

    var cb2 = function () {
        loadGrid();
        //requester에게 메일 보내기
        SendMail([{
            HCP_INQUIRY_REQUEST_ID: state.selected_row.HCP_INQUIRY_REQUEST_ID,
            MAIL_ADDRESS: state.selected_row.MAIL_ADDRESS
        }], "ReportHCPInquiry", "COMPLETE", null);
        $('button.close[data-dismiss=modal]').click();
    }

    var cb1 = function () {
        insertLog([{ HCP_INQUIRY_REQUEST_ID: state.selected_row.HCP_INQUIRY_REQUEST_ID }], 'COMPLETE', null, cb2);
    }

    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/MergeCustomerRequest",
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        cache: false,
        contentType: "application/json; charset=utf-8",
    }).done(function (res) {
        if(res == "Ok") cb1();
    }).fail(function (data) {
        fn_showError({ message: data.responseText });
    });

}
//Delivered 버튼 클릭 이벤트
function deliveredBtnFn() {
    //if (CONST.USER_TYPE != "KEY USER") return; 
    if (!$("#delivery-date-toHCP").val() || !$("#agreeCheckbox_1").is(":checked")) {
        fn_showError({
            message: 'Please select delivery date'
        });
        return;
    }

    

    var data = getModalDataObj(state.selected_row, 'DELIVERED');


    /*
    * Ajax
    * */

    var cb2 = function () {
        loadGrid();
        //requester에게 메일 보내기
        //SendMail([{
        //    HCP_INQUIRY_REQUEST_ID: state.selected_row.HCP_INQUIRY_REQUEST_ID,
        //    MAIL_ADDRESS: state.selected_row.MAIL_ADDRESS
        //}], "ReportHCPInquiry", "DELIVERED", null);
        $('button.close[data-dismiss=modal]').click();
    }

    var cb1 = function () {
        insertLog([{ HCP_INQUIRY_REQUEST_ID: state.selected_row.HCP_INQUIRY_REQUEST_ID }], 'DELIVERED', null, cb2);
    }

    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/MergeCustomerRequest",
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        cache: false,
        contentType: "application/json; charset=utf-8",
    }).done(function (res) {
        if (res == "Ok") cb1();
    }).fail(function (data) {
        fn_showError({ message: data.responseText });
    });

}
//sendConfirm 버튼 클릭 이벤트
function sendConfirmFn () {
    fn_confirm({
        title: "Confirm",
        message: "Are you sure you want to Comfirm these items?"
    }).done(function (result) {
        if (result) {
            var requester = $("input[id$=hhdUserEmail]").val();
            getCheckedCheckbox();

            if (state.checked_rows.length == 0) {
                fn_showError({
                    message: "Please select items to confirm"
                });
                return false;
            }

            /*
             * Ajax Log with comment
             * */
            var cb = function () {
                loadGrid();
                //메일 보내기
                SendMail(state.checked_rows, "ReportHCPInquiry", "PROCESSING", null)
                $('button.close[data-dismiss=modal]').click();
            }
            insertLog(state.checked_rows, 'PROCESSING', null, cb);
        }
    });
}
//Checked row ID 저장 함수
function getCheckedCheckbox() {
    state.checked_rows = [];
    state.toAddress = [];
    $.each($('.girdCheck:checked'), function (i, e) {
        if (!$(e).hasClass('hidden')) {
            state.checked_rows.push({
                HCP_INQUIRY_REQUEST_ID: parseInt($(e).attr('id')),
                MAIL_ADDRESS: $(e).attr('data-email')
            });
        }
    });
}
//Customer Search 버튼 클릭 이벤트
function searchBtnFn() {
    
    if (!$('#name').val()) {
        fn_showError({
            message: "Please input name"
        });
        return false;
    }
    $('#nameSearchBtn').addClass('hidden');
    $('#result-wrapper').removeClass('hidden');
    $('.new-item, .update-item').addClass('hidden');
    $('#resethBtn').removeClass('hidden');
    $('#name').attr('disabled', true);
    $('#org').attr('disabled', true);
    $('#specialty').attr('disabled', true);

    /*
     * Ajax
     * */
    var data = {
        name: $('#name').val(),
        org: $('#org').val(),
        specialty: $('#specialty').val()
    }
    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/SelectInquiryCustomerList",
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var optionStr = "";
            if (data.length == 0) optionStr += "<option disabled selected> * 검색 결과가 없습니다 * </option>";
            else {
                optionStr += "<option disabled selected> * 선택 * </option>";

                for (i in data) {
                    optionStr += "<option val='" + data[i].HCPCode + "' data-hcp-id='" + data[i].HCPCode + "' data-hcp-name='" + data[i].HCPName + "'" + "' data-org-id='" + data[i].OrganizationCode + "'" + "' data-org-name='" + data[i].OrganizationName + "'" + "' data-spe-id='" + data[i].SpecialtyCode + "'" + "' data-spe-name='" + data[i].SpecialtyName + "'>[" + data[i].OrganizationName + " " + data[i].SpecialtyName + "] " + data[i].HCPName + "</option>";
                }
            }
            $("#result").html(optionStr);
            $('#nameSearchBtn').addClass('hidden');
            $('#result-wrapper').removeClass('hidden');
            $('.new-item, .update-item').addClass('hidden');
            $('#resethBtn').removeClass('hidden');
            $('#name').attr('disabled', true);
            $('#customer-type').attr('disabled', true);
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}
//Reset 버튼 클릭 이벤트
function resetBtnFn() {

    $('#result-wrapper').addClass('hidden');
    $('#resethBtn').addClass('hidden');
    $('#nameSearchBtn').removeClass('hidden');
    $('#name').removeAttr('disabled');
    $('#org').removeAttr('disabled');
    $('#specialty').removeAttr('disabled');
    $('.new-item, .update-item').addClass('hidden');
    $('#closeBtn').siblings().attr('disabled', true);
    resetSelectYear();
}
//Result 선택 이벤트
function selectResultFn(e) {

    $('#search-hospital-text, #search-hospital-text1, #search-hospital-text2, #remark, #new-organization').val(null).removeAttr('disabled');
    $('#organization, #new-organization').addClass('hidden');
    $('#organization option, #new-organization option').remove();
    $('#closeBtn').siblings().removeAttr('disabled');
    $('#contentModal input[type=checkbox]:checked').prop('checked', false);
    $('.update-item:not(.text-item)').removeClass('hidden');
    $('.new-item:not(.update-item)').addClass('hidden');

}
//Request 버튼 클릭 이벤트
function requestBtnFn() {
    //validation
    if (!validation()) return false;

    var data = getModalDataObj(false, 'REQUESTING');

    var cb = function () {
        SendMail([{ HCP_INQUIRY_REQUEST_ID: 0, MAIL_ADDRESS: CONST.APPROVER }], "RegistHCPInquiry", "REQUESTING", $("input[id$=hhdUserEmail]").val(), CONST.APPROVER);
        loadGrid();
        $('button.close[data-dismiss=modal]').click();
    }
        
    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/MergeCustomerRequest",
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        cache: false,
        contentType: "application/json; charset=utf-8",
    }).done(function (res) {
        if (res == "Ok") cb();
    }).fail(function (data) {
        fn_showError({ message: data.responseText });
    });
}
//merge data 만들기
function getModalDataObj(objOrId, status) {
    var data = {};
    //업데이트
    if (objOrId && typeof objOrId == 'object') {
        data.HCP_INQUIRY_REQUEST_ID = objOrId.HCP_INQUIRY_REQUEST_ID;
        data.REQUESTER_ID = objOrId.REQUESTER_ID;
        data.REQUEST_TYPE = objOrId.REQUEST_TYPE;
        data.CUSTOMER_ID = objOrId.CUSTOMER_ID;
        data.CUSTOMER_NAME = objOrId.CUSTOMER_NAME;
        data.ORGANIZATION_ID = objOrId.ORGANIZATION_ID;
        data.ORGANIZATION_NAME = objOrId.ORGANIZATION_NAME;
        data.SPECIALTY_ID = objOrId.SPECIALTY_ID;
        data.SPECIALTY_NAME = objOrId.SPECIALTY_NAME;
        data.YEAR_FROM = objOrId.YEAR_FROM;
        data.YEAR_TO = objOrId.YEAR_TO;
        data.INQUIRY_STATUS = status;
        data.REMARK = objOrId.REMARK == "" ? $('#delivery-date').val() : objOrId.REMARK ;
        data.DELIVERED_TO_HCP = $('#delivery-date-toHCP').val();
        data.CREATOR_ID = objOrId.CREATOR_ID;
    //새로 생성(신규 & 업데이트)
    } else {
        if (objOrId) data.HCP_INQUIRY_REQUEST_ID = objOrId;
        data.REQUESTER_ID = $('#cpHolderBottom_hhdUserID').val();
        data.REQUEST_TYPE = $('#request-type').val();
        data.CUSTOMER_ID = $('#result option:selected').attr('data-hcp-id');
        data.CUSTOMER_NAME = $('#result option:selected').attr('data-hcp-name');
        data.ORGANIZATION_ID = $('#result option:selected').attr('data-org-id');
        data.ORGANIZATION_NAME = $('#result option:selected').attr('data-org-name');
        data.SPECIALTY_ID = $('#result option:selected').attr('data-spe-id');
        data.SPECIALTY_NAME = $('#result option:selected').attr('data-spe-name');
        data.YEAR_FROM = $('#select-from').val();
        data.YEAR_TO = $('#select-to').val();
        
        data.INQUIRY_STATUS = status;
        
        data.CREATOR_ID = $('#cpHolderBottom_hhdUserID').val();
    }
    return data;
}
//함수
//날짜 변환 함수
function getDateStr(value) {
    return new Date(parseInt(value.split('+')[0].replace(/\D/g, '')) + (9 * 60 * 60 * 1000)).toISOString().replace('T', ' ').split('.')[0];
}
//Log 생성 함수
function insertLog(HCP_INQUIRY_REQUEST_ID, LOG_CATEGORY, COMMENT, cb) {
    if (!HCP_INQUIRY_REQUEST_ID || !LOG_CATEGORY ) return false;

    var data = {
        IDs: HCP_INQUIRY_REQUEST_ID,
        REGISTER_ID: $("#cpHolderBottom_hhdUserID").val(),
        LOG_CATEGORY: LOG_CATEGORY
    }
    if (COMMENT) data['COMMENT'] = COMMENT;

    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/InsertHCPInquiryLog",
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (cb && data == 'ok') cb();
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}
//Log 호출 함수
function getLog(HCP_INQUIRY_REQUEST_ID, cb) {
    
    if (!HCP_INQUIRY_REQUEST_ID) return false;

    var data = {
        HCP_INQUIRY_REQUEST_ID: HCP_INQUIRY_REQUEST_ID
    }

    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/SelectHCPInquiryLog",
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (cb) cb(data);
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}
//remove text selection
function removeSelection() {
    var sel = window.getSelection ? window.getSelection() : document.selection;
    if (sel) {
        if (sel.removeAllRanges) {
            sel.removeAllRanges();
        } else if (sel.empty) {
            sel.empty();
        }
    }
}
//3자리마다 콤마 삽입 함수
function numberWithCommas(x) {
    if (!x) return 0;
    var val = x;
    if (typeof x == 'string') val = x.replace(/\D/gi, "");
    var num = parseInt(val);
    return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
//Mail 발생
function SendMail(IDs, sendMailType, Status, FromAddress, ToAddress, CC) {

    var data = {
        IDs: IDs,
        sendMailType: sendMailType,
        Status: Status,
        FromAddress: FromAddress,
        ToAddress: ToAddress,
        CC: CC || ''
    }
    console.log(ToAddress);

    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/SendHCPInquiryMail",
        method: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}
function inputEnter(id, event) {
    if (event.which == 13 || event.keyCode == 13) {
        $('#' + id).click();
    }
}
function changeGridRows(e) {
    state.show_rows = parseInt($(e).val() || 15);
    $("#nonOneKeyGrid").jsGrid("option", "pageSize", state.show_rows);
    $(window).trigger('resize');
    $('#click-all').prop('checked', false);
}