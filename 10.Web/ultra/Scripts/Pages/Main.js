
function fn_AddTabPage(url, eventID, title, processID, delegation) {
    $("iframe").hide();
    /*이미 열려있는 페이지 검색 */
    var openedTabs = $('.top-btnset').find('a');
    
    //클릭한 페이지 가지고 오기
    var activeTab = $('.top-btnset li.active');
    var before_url = $(activeTab.find('a')[0]).data('iframe-src');

    var isOpened = false;
    var reActiveTab = null;
    var isLibrary = false;
    $.map(openedTabs, function (item, i) {
        var proID = $(item).data('process-id');
        if (proID) {
            if (proID == processID) {
                reActiveTab = $(item);
                isOpened = true;
            }
        }
        //EventID와 ProcessID가 없는 경우는 Library,Medical,Receipt
        if (eventID && eventID.indexOf("library") >= 0) {
            if (url == $(item).data("iframe-src")) {
                reActiveTab = $(item);
                isOpened = true;
            }
            isLibrary = true;
        }
    });
    $('.top-btnset > li').removeClass("active");
    if (isOpened) {
        reActiveTab.closest("li").addClass("active");
    } else {
        var $li = $("<li class='active'></li>");
        var $ahref = $("<a href='#' class='btn' title='" + title + "'>" + title + "</a>");
        $ahref.data("iframe-src", url);

        $ahref.data("before-src", before_url); //클릭된 페이지 저장

        if (eventID) $ahref.data("event-id", eventID); //이벤트 아이디
        if (processID) $ahref.data("process-id", processID); //프로세스 아디디
        if (delegation) $ahref.data("delegation-role", delegation); //Delegation
        var $span = $("<span class='btn-close'><button type='button' class='glyphicon glyphicon-remove' aria-hidden='true'><span class='tts'>Close</span></button></span>");
        $ahref.append($span);
        $li.append($ahref);
        $('.top-btnset').append($li);
    }
    if (isLibrary) {
        fn_LoadLibraryFrame(url, eventID);
    } else
        fn_LoadIFrame(url, eventID, processID, delegation);

}

function fn_LoadIFrame(url, eventID, processID, delegation) {
    $("iframe").hide();
    $('#iframeMain').show();
    var frameSrc = url
    var pt = "";
    if (eventID) {
        frameSrc = frameSrc + "?eventid=" + eventID;
        pt = "&";
    }
    else {
        pt = "?";
    }
    if (processID) frameSrc = frameSrc + pt + "processid=" + processID;
    if (delegation) frameSrc = frameSrc + "&delegation=" + delegation;
    $('#iframeMain').attr('src', frameSrc);
}

function fn_LoadLibraryFrame(url, eventID) {
    $("iframe").hide();
    var frame = $("iframe#" + eventID);
    if (frame.length > 0) {
        $(frame).show();
    } else {
        $("body").append("<iframe id=" + eventID + " class='cont_frame' name='cont_frame' title='Content' style='width: 99vw; position: relative;' frameborder='0' allowfullscreen />");
        $("#" + eventID).attr("src", url);
    }
}

function fn_RemoveTab(tab) {
    
    var activeTab = $('.top-btnset li.active');      

    tab.remove('li');
    $('.top-btnset li:first').addClass('active').siblings().removeClass('active');
    fn_LoadIFrame($('.top-btnset li:first a:first').data("iframe-src"));
}


// 문서 정렬 저장
function SaveDocumentOrder() {
    var documents = [];
    $("#hdivDocumentOrder tbody tr").each(function () {
        var doc = {
            USER_ID: $("#hhdUserID").val(),
            EVENT_ID: $(this).attr("event_id"),
            SORT: $(this).find(".doc_sort").text()
        };
        documents.push(doc);
    });

    var proDocument = {
        documents: documents
    }

    $.ajax({
        url: COMMON_SERVICE_URL + "/UpdateDocumentList",
        type: "POST",
        data: JSON.stringify(proDocument),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            window.location.reload(true);
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
            callback(false);
        },
    });
}


// 위임 리스트 가져오기
function selectDelegationList(id) {
    var userid = $("#hhdUserID").val();
    id = (id == undefined ? "" : id);

    $.ajax({
        url: COMMON_SERVICE_URL + "/SelectDelegationList/" + userid + "/" + id,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (id == "") {
                //data.IDX
                //data.FROM_DATE
                //data.TO_DATE
                //data.APPROVER_ID
                //data.APPROVER_NAME
                var len = data.length;
                $("#htblDelegation tbody").empty();
                for (var i = 0; i < len; i++) {
                    var delegator = data[i];
                    var tr = "<tr id='htr_" + delegator.IDX + "'><td>" + delegator.FROM_DATE + "</td><td>" + delegator.TO_DATE + "</td><td>" + delegator.APPROVER_NAME + "</td></tr>";
                    $("#htblDelegation tbody").append(tr);
                }
            }
            else {
                if (data.length == 0) resetDelegation();
                $("#optDelegationTo").val(data[0].APPROVER_ID);
                $("#lb_StartDate").val(data[0].FROM_DATE);
                $("#lb_EndDate").val(data[0].TO_DATE);
                $("#txtDescription").val(data[0].DESCRIPTION);
            }
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
            callback(false);
        },
    });
}

// 위임자 리스트 가져오기
function selectDelegationToList() {
    var userid = $("#hhdUserID").val();

    $.ajax({
        url: COMMON_SERVICE_URL + "/SelectDelegationToList/" + userid,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            for (i = 0; i < data.length; i++) {
                $("#optDelegationTo").append("<option value='" + data[i].USER_ID + "'>" + data[i].FULL_NAME + "</option>");
            }
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
            callback(false);
        },
    });
}

// 위임 정보 삭제하기
function DelDelegation() {
    var userid = $("#hhdUserID").val();
    var idx = $("#hhdSelectDelegation").val();

    if (idx == "") return;

    $.ajax({
        url: COMMON_SERVICE_URL + "/DeleteDelegation/" + userid + "/" + idx,
        type: "GET",
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            resetDelegation();
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
            callback(false);
        },
    });
}

// 위임 정보 저장하기
function AddDelegation() {
    var delegation = [];
    var delegator = {
        IDX: $("#hhdSelectDelegation").val(),
        APPROVER_ID: $("#optDelegationTo").val(),
        FROM_DATE: $("#lb_StartDate").val(),
        TO_DATE: $("#lb_EndDate").val(),
        DESCRIPTION: $("#txtDescription").val(),
        USER_ID: $("#hhdUserID").val()
    };
    delegation.push(delegator);

    var proDelegation = {
        delegation: delegator
    }

    $.ajax({
        url: COMMON_SERVICE_URL + "/MergeDelegation",
        type: "POST",
        data: JSON.stringify(proDelegation),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            resetDelegation();
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
            callback(false);
        },
    });
}

function resetDelegation() {
    selectDelegationList();
    $("#optDelegationTo").val("");
    $("#lb_StartDate").val("");
    $("#lb_EndDate").val("");
    $("#txtDescription").val("");
    $("#hhdSelectDelegation").val("");
}

function fn_ReloadIFrame() {
    var activeTab = $('.top-btnset li.active');
    if (activeTab) {
        var url = $(activeTab.find('a')[0]).data('iframe-src');
        var eventID = $(activeTab.find('a')[0]).data('event-id');
        var processID = $(activeTab.find('a')[0]).data('process-id');
        fn_LoadIFrame(url, eventID, processID);
    }
}

$(document).ready(function () {

    var cookieValue = $.cookie('ultraUserGroups');
    var isAdmin = false;
    if (cookieValue != null && (cookieValue.indexOf('ef.u.kr_localappl_87_lpc_user') >= 0
            || cookieValue.indexOf('ef.u.kr_localappl_87_lpc_user') >= 0
            || cookieValue.indexOf('ef.u.kr_localappl_87_support_user') >= 0
            || cookieValue.indexOf('ef.u.kr_localappl_87_system_admin') >= 0
            || cookieValue.indexOf('ef.u.kr_localappl_87_system_designer') >= 0
            //|| cookieValue.indexOf('ef.u.kr_localappl_87_rad_key_user') >= 0
        )) {
        isAdmin = true;
    }

    if (!isAdmin)
        $("#liAdminLink").hide();


    // GNB 클릭시 활성화
    $('#header .gnb>li>a').click(function () {
        $(this).parent().addClass('active').siblings().removeClass('active');
    });

    /**
     * Add a Tab
     */
    $('.openTab').click(function () {
        var url = $(this).data('iframe-src');
        var eventID = $(this).data('event-id');
        fn_AddTabPage(url, eventID, this.innerText);
    });

    /**
    * Remove a Tab
    */
    $('.top-btnset').on('click', '.btn-close', function (e) {
        e.stopPropagation();
        fn_RemoveTab($(this).parents('li'));
    });

    /**
    * Click Tab to show its content 
    */

    $('.top-btnset').on('click', '.btn', function () {
        $(this).parent().addClass('active').siblings().removeClass('active');
        var eventID = $(this).data("event-id");
        if (eventID && eventID.indexOf("library") >= 0)
            fn_LoadLibraryFrame($(this).data("iframe-src"), eventID);
        else
            fn_LoadIFrame($(this).data("iframe-src"), eventID, $(this).data("process-id"), $(this).data("delegation-role"));
    });

    var update_item = null;
    var start_item = null;
    $("#hdivDocumentOrder tbody").sortable({
        stop: function (event, ui) {
            $(this).find("tr").each(function () {
                if ($(this).html() == "") { return; }
                $(this).find('.doc_sort').text($(this).index() + 1);
            });
        }
    });



    //////// 위임 관련 이벤트 ////////

    // 위임자 리스트 가져오기
    selectDelegationToList();
    // 위임 리스트 가져오기
    selectDelegationList();

    // 위임 리스트 클릭 이벤트
    $("#htblDelegation").on("click", "tr", function (obj) {
        //alert($(this).attr("id").replace("htr_"));
        $("#hhdSelectDelegation").val($(this).attr("id").replace("htr_", ""));
        selectDelegationList($("#hhdSelectDelegation").val());
    })

    // 위임 일자 설정
    $('#hbtnDocStart').datetimepicker({
        weekStart: 0,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        forceParse: 0,
        minuteStep: 60,
        minView: 2,
        maxView: 2,
        linkField: "lb_StartDate",
        linkFormat: "yyyy-mm-dd"
    });

    $('#hbtnDocEnd').datetimepicker({
        weekStart: 0,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        forceParse: 0,
        minuteStep: 60,
        minView: 2,
        maxView: 2,
        linkField: "lb_EndDate",
        linkFormat: "yyyy-mm-dd"
    });

    // 표시할 텝이 존재할경우 템추가 호출함.
    if ($("input[id$=hhdAddTabPage]").val().length > 0) {
        var url = $('input[id$=hhdAddTabPage]').val();
        var processid = $('input[id$=hhdAddTabProcessId]').val();
        var eventid = $('input[id$=hhdAddTabEventId]').val();
        var title = $('input[id$=hhdAddTabTitle]').val();

        fn_AddTabPage(url, eventid, title, processid);
    }
});
