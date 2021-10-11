var state = {
    user_type: $('#cpHolderBottom_userType').val(),
    //user_type: 'KEY USER',
    selected_row: null,
    selected_status: 'ALL',
    checked_rows: [],
    request_type: null,
    show_rows: 15
};
var CONST = {
    rejectRemark: "해당 데이터 비활성화(Inactive) 요청",
    NEW : "신청되었습니다.",
    REJECTED: "운영자에 의해 신청이 반려되었습니다.<br>Comment : ",
    REQUESTING: "재신청되었습니다.",
    REGISTERING: "Veeva에 자료를 등록중입니다.",
    COMPLETE: "처리가 완료되었습니다.",
    APPROVER: "sumin.jo@bayer.com|emmayunjung.cho@bayer.com",
    //APPROVER: "bumyoung.kim@bayer.com",
    IQVIA: "Mina.KIM@iqvia.com",
    //IQVIA: "bumyoung.kim@bayer.com",
    CC: "Jaehoon.CHUNG@iqvia.com|sumin.jo@bayer.com|emmayunjung.cho@bayer.com"
    //CC: ""
}
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
    
    loadGrid();
}


//데이터 표 그리기
function loadGrid() {

    var data = {
        user_id: $("#cpHolderBottom_hhdUserID").val(),
        user_type: state.user_type
    }
     /*
    * AJAX
    * */
    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/SelectNonOnekeyList",
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {

            var sortedData = [];

            var count = {
                ALL: data.length,
                REJECTED: 0,
                REQUESTING: 0,
                REGISTERING: 0,
                COMPLETE: 0
            };
            //상태별 데이터 카운트
            for (i = 0; i < data.length; i++) {
                count[data[i].NON_ONEKEY_STATUS]++;
                sortedData.unshift(data[i]);
            }
            for (key in count) {
                $('#count-' + key.toLowerCase()).text(count[key]);
            }
            //그리드 초기화
            if ($("#nonOneKeyGrid").jsGrid) $("#nonOneKeyGrid").jsGrid("destroy");
            //그리드 생성
            $("#nonOneKeyGrid").jsGrid({
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
                                && (!filter.CUSTOMER_NAME || item.CUSTOMER_NAME.toLowerCase().indexOf(filter.CUSTOMER_NAME.toLowerCase()) > -1)
                                && (state.selected_status == 'ALL' ? true : item.NON_ONEKEY_STATUS == state.selected_status)
                        });
                    }
                },
                rowDoubleClick: function (args) {

                    state.selected_row = args.item;
                    loadModal(args.item);
                    removeSelection();
                },
                fields: [
                    {
                        type: "control", width: 30, editButton: false, deleteButton: false,
                        filterTemplate: function () {
                            var checkStr = "<input type='checkbox' id='click-all' class='form-control' onclick='checkAll(this)'>"
                            return state.user_type == 'KEY USER' && (state.selected_status.toLowerCase() == 'requesting' || state.selected_status.toLowerCase() == 'registering') ? checkStr : "";
                        },
                        headerTemplate: function () {
                            return state.user_type == 'KEY USER' && (state.selected_status.toLowerCase() == 'requesting' || state.selected_status.toLowerCase() == 'registering') ? "All" : "";
                        },
                        itemTemplate: function (value, item) {

                            var $customCheckBox = $("<input id='" + item.NON_ONEKEY_ID + "' type='checkbox'>").attr({ class: "form-control girdCheck " + (state.user_type == 'KEY USER' && state.selected_status == item.NON_ONEKEY_STATUS && (state.selected_status == 'REQUESTING' || state.selected_status == 'REGISTERING') ? '' : 'hidden') })
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
                    { name: "ORGANIZATION_ID", title: "ONEKEY_ID(ORG)", type: "text", filtering: false },
                    { name: "ORGANIZATION_NAME", title: "Organization name", type: "text" },
                    { name: "CUSTOMER_TYPE", title: "Customer Type", type: "text", filtering: false },

                    {
                        name: "CUSTOMER_NAME", title: "Name", type: "text",
                        itemTemplate: function (value, item) {
                            return "<a href='#' class='link text-ellipsis' onclick='fn_DetailView(this);'>" + value + "</a>";
                        }
                    },
                    {
                        name: "NON_ONEKEY_STATUS", title: "Status", type: "text", filtering: false, itemTemplate: function (value, item) {
                            return "<span class='badge status " + value.toLowerCase() + "'>" + value.charAt(0).toUpperCase() + value.slice(1).toLowerCase() + "</span>"
                        }
                    },
                    {
                        name: "UPDATE_DATE", title: "Validated Date", type: "text", filtering: false, itemTemplate: function (value, item) {
                            if (item.NON_ONEKEY_STATUS == 'REGISTERING') {
                                return "<span>" + getDateStr(value) + "</span>";
                            }
                        }
                    },
                    {
                        name: "UPDATE_DATE", title: "Confirm Date", type: "text", filtering: false, itemTemplate: function (value, item) {
                            if (item.NON_ONEKEY_STATUS == 'COMPLETE') {
                                return "<span>" + getDateStr(value) + "</span>";
                            }
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
//Modal 열기 함수
function loadModal(item) {

    //입력 폼 초기화
    $('#log, #attachment_text').html(null);
    $('#contentModal *[disabled]').removeAttr('disabled');
    $('#contentModal input[type=text], #contentModal input[type=file], #contentModal textarea').val(null);
    $('#contentModal select option:selected').prop('selected', false);
    $('#contentModal select option:first-child').prop('selected', true);
    $('#contentModal input[type=radio]:checked').prop('checked', false);
    $('#contentModal input[type=checkbox]:checked').prop('checked', false);
    $('#result-wrapper').addClass('hidden');
    $('#files .attach-list li').remove();
    $('#doc-onekey-code').val(null);
    //$('#attachment_text').text(null);

    var processStatus = state.selected_row.NON_ONEKEY_STATUS
    
    if (processStatus == 'NEW') $("input[id$=hddProcessStatus]").val('Temp');
    else {
        $("input[id$=hddProcessStatus]").val('NotTemp');
    }
    $("input[id$=hddProcessID]").val(state.selected_row.NON_ONEKEY_ID);
    var scanTarget = $('#contentModal *[data-all-hidden], #contentModal *[data-admin-hidden], #contentModal *[data-user-hidden], #contentModal *[data-all-disabled], #contentModal *[data-admin-disabled], #contentModal *[data-user-disabled]');
    
    //Status에 따른 노출여부 확인
    for (i = 0; i < scanTarget.length; i++) {
        var jqTarget = $(scanTarget[i]);
        //modal 콘텐츠 hidden
        if ((state.user_type == 'KEY USER' && jqTarget.attr('data-admin-hidden') && jqTarget.attr('data-admin-hidden').indexOf(state.selected_row.NON_ONEKEY_STATUS.toLowerCase()) != -1)
            || state.user_type == 'END USER' && jqTarget.attr('data-user-hidden') && jqTarget.attr('data-user-hidden').indexOf(state.selected_row.NON_ONEKEY_STATUS.toLowerCase()) != -1) {
            jqTarget.addClass('hidden');
        } else if (jqTarget.attr('data-all-hidden') && jqTarget.attr('data-all-hidden').indexOf(state.selected_row.NON_ONEKEY_STATUS.toLowerCase()) != -1) {
            jqTarget.addClass('hidden');
        } else {
            jqTarget.removeClass('hidden');
        }
        //modal 콘텐츠 disabled
        if (state.user_type == 'KEY USER' && jqTarget.attr('data-admin-disabled') && jqTarget.attr('data-admin-disabled').indexOf(state.selected_row.NON_ONEKEY_STATUS.toLowerCase()) != -1
            || state.user_type == 'END USER' && jqTarget.attr('data-user-disabled') && jqTarget.attr('data-user-disabled').indexOf(state.selected_row.NON_ONEKEY_STATUS.toLowerCase()) != -1) {
            jqTarget.attr('disabled', 'true');
        } else if (jqTarget.attr('data-all-disabled') && jqTarget.attr('data-all-disabled').indexOf(state.selected_row.NON_ONEKEY_STATUS.toLowerCase()) != -1) {
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
        //reject 노출
        if (item.NON_ONEKEY_STATUS == 'REJECTED') {
            $('#customer-type').val(item.CUSTOMER_TYPE);
            $('#name').val(item.CUSTOMER_NAME);
            $('input[name=GENDER][value=' + item.GENDER + ']').prop('checked', true);
            if (item.REQUEST_TYPE == 'UPDATE') {
                $('#result-wrapper').removeClass('hidden');
                $('#result').html('<option disabled> * 변경이 필요하면 재검색 * </option><option selected value="' + item.CUSTOMER_NAME + '" data-hcp-name="' + item.CUSTOMER_NAME + '" data-gender="' + item.GENDER + '" data-ins-onekey="' + item.ORGANIZATION_ID + '" data-name="' + item.ORGANIZATION_NAME + '">' + item.CUSTOMER_NAME + '</option>');
                $('#new-organization').removeClass('hidden').html('<option disabled> * 변경이 필요하면 재검색 * </option><option selected value="' + item.ORGANIZATION_ID + '" data-name="' + item.ORGANIZATION_NAME + '">' + item.ORGANIZATION_NAME + '</option>');
            } else if (item.REQUEST_TYPE == 'NEW') {
                $('#organization').removeClass('hidden').html('<option disabled> * 변경이 필요하면 재검색 * </option><option selected value="' + item.ORGANIZATION_ID + '" data-name="' + item.ORGANIZATION_NAME + '">' + item.ORGANIZATION_NAME + '</option>');
            }
            if (item.REMARK.indexOf(CONST.rejectRemark) != -1) {
                $('#deleteCheckbox').removeAttr('checked').click();
            } else {
                $('#remark').val(item.REMARK);
            }
        }
        //텍스트 노출
        $('#name').text(item.CUSTOMER_NAME);
        $('#requesterText').text(item.FULL_NAME + ' | ' + item.ORG_ACRONYM);
        $('#customer_type_text').text(item.CUSTOMER_TYPE);
        $('#name_text').text(item.CUSTOMER_NAME);
        $('#gender_text').text(item.GENDER);
        $('#organization_text').text(item.ORGANIZATION_NAME);
        $('#new_organization_text').text(item.ORGANIZATION_NAME);
        $('#new_organization_text').text(item.ORGANIZATION_NAME);
        $('#remark_text').text(item.REMARK);

        //삭제요청일 경우 첨부파일은 불필요하므로 숨김
        if (item.REMARK.indexOf(CONST.rejectRemark) != -1) $('#attachment-wrapper').addClass('hidden');
        else $('#attachment-wrapper').removeClass('hidden');

        var cb = function (data) {
            //render log
            var logStr = '';
            for (i in data) {
                i == 0 ? logStr += '<p style="color:red">' : logStr += '<p>';
                logStr += getDateStr(data[i].CREATE_DATE) + ' : ' + CONST[data[i].LOG_CATEGORY];
                if (data[i].LOG_CATEGORY == 'REJECTED') logStr += data[i].COMMENT;
                logStr += '</p>';
            }
            logStr += '<p>' + getDateStr(item.CREATE_DATE) + ' : ' + CONST.NEW + '</p>';
            $('#log').html(logStr);

            //get attachment list
            selectAttach(item.NON_ONEKEY_ID, item.NON_ONEKEY_STATUS, function() {
                $('#contentModal').modal();
            });
        }
        getLog(item.NON_ONEKEY_ID, cb);
    } else {
        //open modal
        $('#contentModal').modal();
    }
}
//Validation
function validation() {
    var requestType = $('#request-type').val();
    if ($('#deleteCheckbox').prop('checked')) requestType = 'DELETE';

    if (requestType != 'NEW' && requestType != 'UPDATE' && requestType != 'DELETE') {
        fn_showError({
            message: 'Process Error, Please re-search name'
        });
        return false;
    }
    if (!$('#customer-type').val()) {
        fn_showError({
            message: 'Please select customer type'
        });
        return false;
    }
    if (!$('#name').val()) {
        fn_showError({
            message: 'Please input name'
        });
        return false;
    }
    if (requestType != 'DELETE' && !$('.gender-wrapper input[type=radio]:checked').val()) {
        fn_showError({
            message: 'Please select gender'
        });
        return false;
    }
    if (requestType == 'NEW' && !$('#organization').val()) {
        fn_showError({
            message: 'Please select organization'
        });
        return false;
    }
    if (requestType == 'UPDATE' && !$('#new-organization').val()) {
        fn_showError({
            message: 'Please select new organization'
        });
        return false;
    }
    if (requestType != 'DELETE' && !$('#remark').val()) {
        fn_showError({
            message: 'Please input remark'
        });
        return false;
    }
    
    if (requestType != 'DELETE' && $("#files .attach-list").find("li").length == 0) {
        fn_showError({
            message: 'Please select to attach file'
        });
        return false;
    }
    return true;
}

//이벤트 바인딩
//상단 Filter btn 클릭 이벤트
function filterBtnFn(e) {

    state.checked_rows = [];
    state.selected_row = null;

    var status = $(e).attr('data-value');
    $('.statusBtns button.active').removeClass('active');
    $(e).addClass('active');
    state.selected_status = status;
    //sendRequest 버튼 노출
    if (state.selected_status == 'REQUESTING' && state.user_type == 'KEY USER') {
        $('#sendRequestBtn').removeClass('hidden');
    } else {
        $('#sendRequestBtn').addClass('hidden');
    }
    //complete 버튼 노출
    if (state.selected_status == 'REGISTERING' && state.user_type == 'KEY USER') {
        $('#sendCompleteBtn').removeClass('hidden');
    } else {
        $('#sendCompleteBtn').addClass('hidden');
    }

    loadGrid();
}
//New 버튼 클릭 이벤트
function newBtnFn() {

    state.selected_row = { NON_ONEKEY_STATUS : 'NEW' }
    $('.new-item, .update-item').addClass('hidden');
    $("input[id$=hddProcessStatus]").val('Temp');
    loadModal();

}
//Customer Search 버튼 클릭 이벤트
function searchBtnFn() {
    if (!$('#customer-type').val()) {
        fn_showError({
            message: "Please select customer type"
        });
        return false;
    }
    if (!$('#name').val()) {
        fn_showError({
            message: "Please input name"
        });
        return false;
    }
    //리스트 검색
    /*
     * Ajax
     * */
    var data = {
        customer_type : $('#customer-type').val(),
        customer_name: $('#name').val()
    }
    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/SelectCustomerList",
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var optionStr = "";
            if (data.length == 0) optionStr += "<option disabled selected> * 검색 결과가 없습니다 * </option>";
            else optionStr += "<option disabled selected> * 선택 * </option>";
            optionStr += "<option value='new'>신규 입력</option>";
            for (i in data) {
                var specialty = data[i].HCP_TYPE;
                switch (specialty) {
                    case 'Radiographer': specialty = '방사선사';
                        break;
                    case 'Pharmacist': specialty = '약사';
                        break;
                    case 'Nurse': specialty = '간호사';
                        break;
                    case 'Doctor': specialty = '인턴';
                        break;
                    default: specialty = specialty ? specialty : '기타 전문 분야';
                }
                if (data[i].GENDER == 'Male') data[i].GENDER = "남성";
                else if (data[i].GENDER == 'Female') data[i].GENDER = "여성";

                if (data[i].GUBUN == 'Veeva') optionStr += "<option value='" + data[i].DOC_ONEKEY_CODE + "' data-name='" + data[i].OrganizationName + "' data-hcp-name='" + data[i].HCPName + "' data-gender='" + data[i].GENDER + "' data-ins-onekey='" + data[i].INS_ONEKEY_CODE + "'>" + data[i].HCPName + "(" + data[i].OrganizationName + " " + specialty + ")</option>";
                else if ((data[i].GUBUN == 'Request' || data[i].GUBUN == 'UlTra Request') && data[i].NON_ONEKEY_STATUS != 'COMPLETE' && data[i].NON_ONEKEY_STATUS != 'REJECT') optionStr += "<option disabled>" + data[i].HCPName + "*현재 등록 중인 고객입니다* (" + data[i].OrganizationName + " " + specialty + ")</option>";
                
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
    $('#customer-type').removeAttr('disabled');
    $('.new-item, .update-item').addClass('hidden');
    $('#closeBtn').siblings().attr('disabled', true);
}
//병원 검색 버튼 클릭 이벤트
function searchHospitalBtnFn(index) {
    var keyword = $('#search-hospital-text'+index).val();
    if (!keyword) {
        fn_showError({
            message: "Please select hospital name to search"
        });
        return false;
    }
    /*
    * Ajax Rad 병원 리스트
    * */
    var data = {
        keyword: keyword
    };

    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/SelectNonOneKeyHospitalList",
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var optionStr = "<option disabled selected> * 선택 * </option>";
            for (i in data) {
                optionStr += "<option value='" + data[i].INS_ONEKEY_CODE + "' data-name='" + data[i].OrganizationName + "'>" + data[i].OrganizationName + " (" + data[i].B_STATE + ")</option>";
            }
            var result = $('#request-type').val();
            if (result == 'NEW') {
                $('#organization').html(optionStr).removeClass('hidden');
            } else if (result == 'UPDATE') {
                $('#new-organization').html(optionStr).removeClass('hidden');
            }
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}
//Result 선택 이벤트
function selectResultFn(e) {

    $('#search-hospital-text, #search-hospital-text1, #search-hospital-text2, #remark, #new-organization').val(null).removeAttr('disabled');
    $('#organization, #new-organization').addClass('hidden');
    $('#organization option, #new-organization option').remove();
    $('#closeBtn').siblings().removeAttr('disabled');
    $('#contentModal input[type=checkbox]:checked').prop('checked', false);
    $('input[name=GENDER]:checked').prop('checked', false);

    var result = $(e).val();
    if (result == 'new') {
        $('#request-type').val('NEW');
        $('.new-item:not(.text-item)').removeClass('hidden');
        $('.update-item:not(.new-item)').addClass('hidden');
    } else if (result) {
        $('#request-type').val('UPDATE');
        $('#doc-onekey-code').val(result);
        $('.update-item:not(.text-item)').removeClass('hidden');
        $('.new-item:not(.update-item)').addClass('hidden');

        var gender = $(e).find('option:selected').attr('data-gender');
        if (gender == '남성' || gender == '여성') $('input[name=GENDER][value=' + gender + ']').prop('checked', true);
    }
}
//Request 버튼 클릭 이벤트
function requestBtnFn() {
    //validation
    if (!validation()) return false;

    var data = getModalDataObj(false, 'REQUESTING');
    var userID = $("input[id$=hddUserID]").val();
    var requester = $("input[id$=hhdUserEmail]").val();

    //Attach file save
    var liFiles = $("#divAttachFiles_ReportNonOnekey .attach-list").find("li");
    var files = "";
    for (var i = 0; i < liFiles.length; i++) {
        files += $(liFiles[i]).data('attach-file').SavedName + ";";
    }
    data = $.extend(data, {
        UserID: userID,
        AttachType: "ReportNonOnekey",
        Status: "Move",
        MoveFiles: files,
        ReferIDX: "0",
    });
    
    var cb = function () {
        SendMail([{ NON_ONEKEY_ID: 0 }], "ReportNonOnekey", "REQUESTING", $("input[id$=hhdUserEmail]").val(), CONST.APPROVER);
        loadGrid();
        $('button.close[data-dismiss=modal]').click();
    }
    
    $.ajax({
        data: data,
        dataType: 'json',
        url: UPLOAD_HANDLER_URL,
        type: 'POST',
    }).done(function (res) {
        cb();
    }).fail(function (data) {
        fn_showError({ message: data.responseText });
    });
}
//Reject 버튼 클릭 이벤트
function rejectBtnFn() {

    if (!$('#comment').val()) {
        fn_showError({
            message: "Please input reject comment"
        });
        return false;
    }
    /*
    * Ajax Log
    * */
    var cb = function () {
        loadGrid();
        //requester에게 메일 보내기
        SendMail([{ NON_ONEKEY_ID: state.selected_row.NON_ONEKEY_ID }], "ReportNonOnekey", "REJECTED", $("input[id$=hhdUserEmail]").val(), state.selected_row.MAIL_ADDRESS);
        $('button.close[data-dismiss=modal]').click();
    }
    insertLog([{ NON_ONEKEY_ID: state.selected_row.NON_ONEKEY_ID }], 'REJECTED', $('#comment').val(), cb);
    
}
//Re-request 버튼 클릭 이벤트
function reRequestBtnFn() {
    //validation
    if (!validation()) return false;

    var data = getModalDataObj(state.selected_row.NON_ONEKEY_ID, 'REQUESTING');
    var userID = $("input[id$=hddUserID]").val();
    var requester = $("input[id$=hhdUserEmail]").val();

    var cb = function () {
        SendMail([{ NON_ONEKEY_ID: state.selected_row.NON_ONEKEY_ID }], "ReportNonOnekey", "REQUESTING", $("input[id$=hhdUserEmail]").val(), CONST.APPROVER);
        loadGrid();
        $('button.close[data-dismiss=modal]').click();
    }

    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/MergeCustomerData",
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            console.log(data)
            if (data) insertLog([{ NON_ONEKEY_ID: state.selected_row.NON_ONEKEY_ID }], 'REQUESTING', "재신청되었습니다", cb);
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}
//merge data 만들기
function getModalDataObj(objOrId, status) {
    var data = {};
    //업데이트
    if (objOrId && typeof objOrId == 'object') {
        data.NON_ONEKEY_ID = objOrId.NON_ONEKEY_ID;
        data.REQUESTER_ID = objOrId.REQUESTER_ID;
        data.REQUEST_TYPE = objOrId.REQUEST_TYPE;
        data.CUSTOMER_TYPE = objOrId.CUSTOMER_TYPE;
        data.CUSTOMER_NAME = objOrId.CUSTOMER_NAME;
        data.GENDER = objOrId.GENDER;
        data.ORGANIZATION_ID = objOrId.ORGANIZATION_ID;
        data.ORGANIZATION_NAME = objOrId.ORGANIZATION_NAME;
        data.NON_ONEKEY_STATUS = status;
        data.REMARK = objOrId.REMARK;
        data.CREATOR_ID = objOrId.CREATOR_ID;
    //새로 생성(신규 & 업데이트)
    } else {
        if (objOrId) data.NON_ONEKEY_ID = objOrId;
        data.REQUESTER_ID = $('#cpHolderBottom_hhdUserID').val();
        data.REQUEST_TYPE = $('#request-type').val();
        data.CUSTOMER_TYPE = $('#customer-type').val();
        data.CUSTOMER_NAME = data.REQUEST_TYPE == 'NEW' ? $('#name').val() : $('#result option:selected').attr('data-hcp-name');
        data.GENDER = $('.gender-wrapper input[type=radio]:checked').val();
        data.ORGANIZATION_ID = data.REQUEST_TYPE == 'NEW' ? $('#organization').val() : $('#new-organization').val();
        data.ORGANIZATION_NAME = data.REQUEST_TYPE == 'NEW' ? $('#organization option:selected').attr('data-name') : $('#new-organization option:selected').attr('data-name');
        data.NON_ONEKEY_STATUS = status;
        if (data.REQUEST_TYPE == 'UPDATE') data.REMARK = $('#doc-onekey-code').val().replace('|', ' ') + "|" + $('#remark').val().replace('|',' ');
        else data.REMARK = $('#remark').val();
        
        data.CREATOR_ID = $('#cpHolderBottom_hhdUserID').val();
    }
    //삭제
    if ($('#deleteCheckbox').prop('checked')) {
        data.GENDER = $('#result option:selected').attr('data-gender');
        data.REQUEST_TYPE = 'UPDATE';
        data.ORGANIZATION_ID = $('#result option:selected').attr('data-ins-onekey');
        data.ORGANIZATION_NAME = $('#result option:selected').attr('data-name');
        data.REMARK = $('#doc-onekey-code').val().replace('|', ' ') + "|" + CONST.rejectRemark.replace('|', ' ');
    }

    //인턴일 경우 코멘트에 인턴이라고 표시한다.
    if (data.CUSTOMER_TYPE == '인턴') {
        data.REMARK = "* Specialty : 인턴\n" + data.REMARK;
    }


    return data;
}
//sendRequest 버튼 클릭 이벤트
function sendRequestFn() {
    fn_confirm({
        title: "Confirm",
        message: "Are you sure you want to register these items?"
    }).done(function (result) {
        if (result) {
            getCheckedCheckbox();
            if (state.checked_rows.length == 0) {
                fn_showError({
                    message: "Please select items to send request"
                });
                return false;
            }
            /*
             * Ajax
             * */
            var cb = function () {
                loadGrid();
                //IQVIA에 메일 보내기
                //메일 보내기
                SendMail(state.checked_rows, "RegistNonOnekey", "REGISTERING", $("input[id$=hhdUserEmail]").val(), CONST.IQVIA, CONST.CC);
                $('button.close[data-dismiss=modal]').click();
            }
            insertLog(state.checked_rows, 'REGISTERING', null, cb);
        }
    });
}
//sendComplete 버튼 클릭 이벤트
function sendCompleteFn() {
    fn_confirm({
        title: "Confirm",
        message: "Are you sure you want to complete these items?"
    }).done(function (result) {
        if (result) {
            var requester = $("input[id$=hhdUserEmail]").val();
            getCheckedCheckbox();

            if (state.checked_rows.length == 0) {
                fn_showError({
                    message: "Please select items to complete"
                });
                return false;
            }
            
            /*
             * Ajax Log with comment
             * */
            var cb = function () {
                loadGrid();
                //메일 보내기
                SendMail(state.checked_rows, "ReportNonOnekey", "COMPLETE", requester, CONST.APPROVER);
                $('button.close[data-dismiss=modal]').click();
            }
            insertLog(state.checked_rows, 'COMPLETE', null, cb);
        }
    });
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
//삭제 체크박스 클릭 이벤트
function deleteRequest(e) {
    if ($(e).prop('checked')) {
        $('#new-organization, #search-hospital-text2').attr('disabled', true);
        $('#attachment-wrapper').addClass('hidden');
        $('#remark').parents('tr').addClass('hidden');
        $('#male').parents('tr').addClass('hidden');
    } else {
        $('#new-organization, #search-hospital-text2').removeAttr('disabled');
        $('#attachment-wrapper, #remark').removeClass('hidden');
        $('#remark').parents('tr').removeClass('hidden');
        $('#male').parents('tr').removeClass('hidden');
    }
}

//함수
//Checked row ID 저장 함수
function getCheckedCheckbox() {
    state.checked_rows = [];
    $.each($('.girdCheck:checked'), function (i, e) {
        if (!$(e).hasClass('hidden')) state.checked_rows.push({
            NON_ONEKEY_ID: parseInt($(e).attr('id'))
        });
    });
}
//날짜 변환 함수
function getDateStr(value) {
    return new Date(parseInt(value.split('+')[0].replace(/\D/g, '')) + (9 * 60 * 60 * 1000)).toISOString().replace('T', ' ').split('.')[0];
}
//Log 생성 함수
function insertLog(NON_ONEKEY_ID, LOG_CATEGORY, COMMENT, cb) {
    if (!NON_ONEKEY_ID || !LOG_CATEGORY ) return false;

    var data = {
        NON_ONEKEY_ID: NON_ONEKEY_ID,
        REGISTER_ID: $("#cpHolderBottom_hhdUserID").val(),
        LOG_CATEGORY: LOG_CATEGORY
    }
    if (COMMENT) data['COMMENT'] = COMMENT;

    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/InsertLog",
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
function getLog(NON_ONEKEY_ID, cb) {
    
    if (!NON_ONEKEY_ID) return false;

    var data = {
        NON_ONEKEY_ID: NON_ONEKEY_ID
    }

    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/SelectLog",
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
//Attach select 함수
function selectAttach(id, status, cb) {

    var data = {
        NON_ONEKEY_ID: id
    };

    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/SelectNonOnekeyAttachFile",
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (attachFiles) {
            var len = attachFiles.length;
            for (var i = 0; i < len; i++) {
                var file = attachFiles[i];
                var attachFile = {
                    Index: file.IDX,
                    NON_ONEKEY_ID: file.NON_ONEKEY_ID,
                    DisplayName: encodeURIComponent(file.DISPLAY_FILE_NAME),
                    SavedName: encodeURIComponent(file.SAVED_FILE_NAME),
                    FileSize: file.FILE_SIZE,
                    AttachType: file.ATTACH_FILE_TYPE,
                    FileHandlerUrl: file.FILE_HANDLER_URL,
                    FilePath: encodeURIComponent(file.FILE_PATH),
                    ErrorMessage: "",
                }
                var $li = $("<li data-attach-file=" + JSON.stringify(attachFile) + "></li>");
                var $ahref = $("<a href='#' class='attach-link btn btn-xs btn-gray'>" + file.DISPLAY_FILE_NAME + "</a>");
                $li.append($ahref);
                if (state.user_type == 'END USER' && status == 'REJECTED') {
                    var $button = $("<button type='button' class='fa fa-times'><span class='tts'>Close</span></button>");
                    $li.append($button);
                }
                $("#divAttachFiles_ReportNonOnekey .attach-list").append($li);
                $('#attachment_text').append("<a href='#' target='_blank' class='attach-text'>" + file.DISPLAY_FILE_NAME + "</a>");
            }
            if (cb) cb();
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}
//첨부파일 data 변환 함수
function convertFileToDto(obj, NON_ONEKEY_ID, seq) {
    var dto = {
        ATTACH_FILE_TYPE: obj.AttachType,
        SEQ: seq,
        IS_DELETED: 'N',
        DISPLAY_FILE_NAME: obj.DisplayName,
        SAVED_FILE_NAME: obj.SavedName,
        FILE_SIZE: obj.FileSize,
        FILE_PATH: obj.FilePath,
        FILE_HANDLER_URL: obj.FileHandlerUrl,
        IDX : obj.Index,
        CREATOR_ID: $("#cpHolderBottom_hhdUserID").val()
    }
    if (NON_ONEKEY_ID) dto.NON_ONEKEY_ID = NON_ONEKEY_ID;
    return dto;
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
function SendMail(NON_ONEKEY_ID, AttachType, Status, FromAddress, ToAddress, CC) {

    var data = {
        NON_ONEKEY_ID: NON_ONEKEY_ID,
        AttachType: AttachType,
        Status: Status,
        FromAddress: FromAddress,
        ToAddress: ToAddress,
        CC: CC || ''
    }

    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/SendMail",
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