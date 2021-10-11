var Month = ["Jan", "Feb", "Mar", "Apr", "May", "Jun", "Jul", "Aug", "Sep", "Oct", "Nov", "Dec"];
var createMonthOption = function () {
    var HTMLstr = "";
    var months;
    var date = new Date();

    months = (date.getFullYear() - 2018) * 12;
    months += date.getMonth() + 1;
    for (i = months - 1; i >= 0; i--) {
        var year = 2018 + parseInt(i / 12);
        var month = (i % 12) + 1;
        month = ("0" + month).substr(-2);
        //HTML
        HTMLstr += "<option value='" + year + "-" + month + "-01'>" + year + "-" + month + "</option>";
    }
    
    var DOM = document.querySelectorAll('select.year-month');
    $("#select_month").html(HTMLstr);
    
}
function loadPage() {
    $.ajaxSetup({ cache: false });
    $('body').waitMe({
        effect: 'win8',
        text: 'Loading...'

    });
    ApprovalCount();
    pageNumReset();
    var preSelected = window.parent.$("input[id$=hddHomeSelectedTopTab]").val();
    if (preSelected)
        AjaxGridBind(preSelected, 1);
    else
        AjaxGridBind("NotSubmitted", 1);
    AjaxListBind("UpcomingEvent", 1);
    AjaxListBind("Payment", 1);

    var today = new Date;
    Bind3TimeVisits(formatDate(today), "Min");
    selectFromCRMData();
    $('#hddSelectDate').val(formatDate(today));
    $('#hddSearchSelectDate').val(formatDate(today));
    $("#hspanMonth").text(Month[today.getMonth()]);
    $("#hspanSearchMonth").text(Month[today.getMonth()]);
    $("#hspanSearchMonth").attr("title", today.getFullYear().toString() + "-" + (today.getMonth() + 1).toString());
}

function pageNumReset() {
    $('#hddNotSubmittedPageNum').val(1);
    $('#hddApprovalQueuePageNum').val(1);
    $('#hddPendingApprovalPageNum').val(1);
    $('#hddDelegationPageNum').val(1);
    $('#hddUpcomingEventPageNum').val(1);
    $('#hddPaymentPageNum').val(1);
}

function formatDate(date) {
    var d = new Date(date),
        month = '' + (d.getMonth() + 1),
        day = '01',
        year = d.getFullYear();

    if (month.length < 2) month = '0' + month;
    if (day.length < 2) day = '0' + day;

    return [year, month, day].join('-');
}

$(function () {
    $('.modal').on('show.bs.modal', function (e) {
        if (window.top.document.querySelector('iframe')) {
            $('.modal').css('height', $(window.parent).height() - 52 - 32 - 20); // 윈도우 - Top메뉴 - Tab버튼 - Padding
        }
    });
    $('.modal').on('shown.bs.modal', function (e) {
        if (window.top.document.querySelector('iframe')) {
            var wh = $(window.parent).height();
            var mh = $('.modal').css('height');
            if (wh > parseInt(mh.replace("px")))
                $(".modal").css("height", $(window.parent.document).height() - 52 - 32 - 20); // 윈도우 - Top메뉴 - Tab버튼 - Padding
        }
    });

    /* Enter key */
    $("#divMobileSearch").on("keypress", "input[type=search]", function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            var keyword = $("#divMobileSearch input[type=search]").val();
            var today = new Date;
            $("#hspanSearchMonth").text(Month[today.getMonth()]);
            $("#hspanSearchMonth").attr("title", today.getFullYear().toString() + "-" + (today.getMonth() + 1).toString());
            selectHCPInfo(keyword, keyword, true);

        }
    });

    $("#hcp_info").on('show.bs.modal', function (e) {
        if (e.relatedTarget) {
            //3Time Visit에서 클릭
            selectHCPInfo($(e.relatedTarget).data("hcp-code"), $(e.relatedTarget).data("hcp-name"));
        }
    });

    $(".pagination .crm-page-left").click(function (e) {
        e.preventDefault();
        var totalPage = $(".pagination").data("total");
        var currentPage = $(".pagination").data("current");
        if (currentPage - 1 >= 1)
            displayFromCRMDataPage(currentPage - 1);
    });

    $(".pagination .crm-page-right").click(function (e) {
        e.preventDefault();
        var totalPage = $(".pagination").data("total");
        var currentPage = $(".pagination").data("current");
        if (currentPage + 1 <= totalPage)
            displayFromCRMDataPage(currentPage + 1);
    });

    $(".crm-page-button").click(function (e) {
        e.preventDefault();
        var idx = $(this).text();
        displayFromCRMDataPage(parseInt(idx));
    });

    // 부모 아이프레임 높이 자동조절
    var contH = $('#l_frame').outerHeight();
    var contH = contH + 100
    $('#l_frame').outerHeight(contH);
    if (parent.document) {
        if (contH < 900) contH = 900;
        $('iframe.cont_frame', parent.document).height(contH);
    }

    //메뉴 close
    $("body").click(function () {
        $(".dropdown-toggle", window.parent.document).parent().removeClass('open');
    });

    $(document).ajaxStop(function () {
        $('body').waitMe('hide');
    });
    $("#select_month").change(function () {
        var hcpName = $("#divSearchResult h1").text();
        selectHCPInfo(hcpName, hcpName, false, "");
    });
    createMonthOption();
});

function selectHCPInfo(hcpCode, hcpName, isSearch, visitDate) {

    var hddSelectDate = $("#hddSelectDate").val();
    
    if (visitDate)
        hddSelectDate = visitDate;
    var d1 = $.Deferred();
    hddSelectDate = $("#select_month option:selected").val();
    
    
    $.ajax({
        type: "GET",
        url: EVENT_SERVICE_URL + "/SelectHomeVisitList/" + hcpCode + "/" + hddSelectDate,
        dataType: "json",
        success: function (data) {
            d1.resolve(data);
            displayHCPInfo(hcpName, data, hcpCode);
            if (isSearch) {
                //if (data.length > 0) {
                $("#hcp_info").modal('show');
                $("#divMobileSearch input[type=search]").val("");
                //} else {
                //    fn_showInformation({
                //        title: "Information!",
                //        message: "자료가 존재하지 않습니다."
                //    });
                //}
            }
        }
    });

    d1.promise();
}
function get_month() {


}
function displayHCPInfo(hcpName, list, hcpCode) {
    var len = list.length;
    $("#tblHome_VisitList tbody").empty();
    
    

    for (var i = 0; i < len; i++) {
        var visitor = list[i];
        var color_change = "";
        if (visitor.HCP_NAME.toString().indexOf("Violation") >= 1) {
            //HCP_Name = "<font color='red'>" + visitor.HCP_NAME+"</font>"
            color_change = "<tr style='background:#F6CED8'><td>";
        } else {
            //HCP_Name = visitor.HCP_NAME;
            color_change="<tr><td>"
        }
        var tr = color_change + visitor.EVENT_DATE + "</td><td>" + visitor.EVENT_NAME + "(" + visitor.DATA_SOURCE + ")<br/>" + visitor.PROCESS_STATUS + "</td>";
        tr += "<td>" + visitor.HCP_NAME +visitor.HCP_CODE+ "<br/>" + visitor.HCO_NAME + "/" + visitor.SPECIALTY_NAME + "</td><td>" + visitor.REQUESTER_NAME + "</td>";
        tr += "<td>" + visitor.SUBJECT + "</td></tr>";
        $("#tblHome_VisitList tbody").append(tr);
    }

    $("#divSearchResult h1").text(hcpName);
    $("#divSearchResult .badge").text(len.toString());
}

function addTabPage(url, eventID, title, processID, delegation) {
    window.parent.fn_AddTabPage(url, eventID, title, processID, delegation);
}

$('.nav-tabs a').click(function (e) {
    var userID = $('#hddUserID').val();
    var tabType = $(e.target).data('tab-type');
    window.parent.$("input[id$=hddHomeSelectedTopTab]").val(tabType);
    AjaxGridBind(tabType, 1);
    ApprovalCount();
});

function AjaxGridBind(tabType, pageNum) {
    var strUrl = "";
    var userID = $('#hddUserID').val();

    try {
        switch (tabType) {
            case "NotSubmitted":
                strUrl = "/SelectHomeNotSubmitted/" + userID;
                break;
            case "ApprovalQueue":
                strUrl = "/SelectHomeApprovalQueue/" + userID;
                break;
            case "PendingApproval":
                strUrl = "/SelectHomePendingApproval/" + userID;
                break;
            case "Delegation":
                strUrl = "/SelectHomeDelegation/" + userID;
                break;
        }
        var d2 = $.Deferred();

        $.ajax({
            type: "GET",
            url: EVENT_SERVICE_URL + strUrl,
            //async: false,
            dataType: "json",
            success: function (data) {
                d2.resolve(data);
                if (tabType == "3TimeVisits")
                    Create3TimeVisits(data, pageNum)
                else
                    CreateTable(tabType, data, pageNum);
            }
        });

        d2.promise();
    }
    catch (ex) {
        alert(ex.message);
    }
}

function AjaxListBind(tabType, pageNum) {
    var strUrl = "";
    var userID = $('#hddUserID').val();
    var status = "";
    if (tabType == "UpcomingEvent")
        status = "Completed"
    else
        status = "EventCompleted"
    var search = {
        processStatus: status,
        userID: userID,
        searchType: "",
        searchText: "",
        doc_Start: "",
        doc_End: "",
        evt_Start: "",
        evt_End: ""
    }
    try {
        var d3 = $.Deferred();

        $.ajax({
            type: "POST",
            url: EVENT_SERVICE_URL + "/SelectApprovalCompletedList",
            data: JSON.stringify(search),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (returnData) {
                d3.resolve(returnData);
                CreateDateList(tabType, returnData, pageNum);
            }
        });

        d3.promise();
    }
    catch (ex) {
        alert(ex.message);
    }
}

function CreateTable(type, data, pageNum) {
    var table = $('<table/>').addClass('table table-striped');
    var colgroup = $('<colgroup/>');
    var thead = $('<thead/>');
    var tbody = $('<tbody/>');

    colgroup.append('<col style=\"width:15%;\" />');
    colgroup.append('<col style=\"width:20%;\" />');
    colgroup.append('<col />');
    if (type != "NotSubmitted")
        colgroup.append('<col style=\"width:22%;\" />');
    table.append(colgroup);

    // thead
    var headtr = $('<tr/>');
    headtr.append($('<th/>').text('Date'));
    headtr.append($('<th/>').text('Requester'));
    headtr.append($('<th/>').text('Subject'));
    if (type == "ApprovalQueue")
        headtr.append($('<th/>').text('Next Approver'));
    else if (type == "PendingApproval")
        headtr.append($('<th/>').text('Current Approval'));
    else if (type == "Delegation")
        headtr.append($('<th/>').text('Status'));
    thead.append(headtr);

    if (data.length > 0) {
        // tbody
        for (var i = 0; i < data.length; i++) {
            // 3개씩 출력해야함
            if (i >= ((pageNum - 1) * 3) && i < pageNum * 3) {
                var bodytr = $('<tr/>');
                var date = $('<span/>').addClass('date').text(data[i].REQUEST_DATE);
                var subjectTD = $('<td/>');
                var subject = "";
                if (type == "Delegation")
                    subject = $('<a/>').addClass('link text-ellipsis').text(data[i].SUBJECT).attr("href", "#").attr("onclick", 'addTabPage("/ultra/Pages/Event/' + data[i].WEB_PAGE_NAME + '","' + data[i].EVENT_ID + '","' + data[i].EVENT_NAME + '","' + data[i].PROCESS_ID + '","' + data[i].DELEGATION_ROLE + '")');
                else
                    subject = $('<a/>').addClass('link text-ellipsis').text(data[i].SUBJECT).attr("href", "#").attr("onclick", 'addTabPage("/ultra/Pages/Event/' + data[i].WEB_PAGE_NAME + '","' + data[i].EVENT_ID + '","' + data[i].EVENT_NAME + '","' + data[i].PROCESS_ID + '")');
                var br = $('<br/>');
                var eventName = $('<span/>').addClass('cate').text(data[i].EVENT_NAME);
                subjectTD.append(subject).append(br).append(eventName);

                bodytr.append($('<td/>').append(date));
                bodytr.append($('<td/>').text(data[i].REQUESTER_NAME));
                bodytr.append(subjectTD);
                if (type == "PendingApproval")
                    bodytr.append($('<td/>').text(data[i].CURRENT_APPROVER_NAME));
                else if (type == "ApprovalQueue")
                    bodytr.append($('<td/>').text(data[i].NEXT_APPROVER_NAME));
                else if (type == "Delegation")
                    bodytr.append($('<td/>').text(data[i].PROCESS_STATUS));
                tbody.append(bodytr);
            }
        }
    }
    $('#' + type + "Cnt").text(data.length);
    table.append(colgroup).append(thead).append(tbody);
    $('#hdiv' + type).empty();
    $('#hdiv' + type).append(table);
    if (type == "ApprovalQueue")
        $(".nav-tabs a[href='#tpApprovalQueue']").tab('show');
    else if (type == "PendingApproval")
        $(".nav-tabs a[href='#tpPendingApproval']").tab('show');
    else if (type == "Delegation")
        $(".nav-tabs a[href='#tpDelegation']").tab('show');
    else
        $(".nav-tabs a[href='#tpNotSubmitted']").tab('show');

}

function CreateDateList(type, data, pageNum) {
    var ul = $('<ul/>').addClass('date-list');
    if (data.length > 0) {
        $('#' + type + "Cnt").text(data.length);
        for (var i = 0; i < data.length; i++) {
            // 3개씩 출력해야함
            var value = data[i].START_DATE;
            if (value == "" || value == null || value == undefined || (value != null && typeof value == "object" && !Object.keys(value).length)) {
            }else{
                if (i >= ((pageNum - 1) * 3) && i < pageNum * 3) {
                    var li = $('<li/>');
                
                    var date = data[i].START_DATE.split('-');
                    var datespan = $('<span/>').addClass('date').html(date[0] + " " + date[1] + "<strong>" + date[2] + "</strong>");
                    var subject = $('<a/>').addClass('link text-ellipsis').text(data[i].SUBJECT).attr("href", "#").attr("onclick", 'addTabPage("/ultra/Pages/Event/' + data[i].WEB_PAGE_NAME + '","' + data[i].EVENT_ID + '","' + data[i].EVENT_NAME + '","' + data[i].PROCESS_ID + '")');
                    var br = $('<br/>');
                    var eventName = $('<span/>').addClass('cate text-ellipsis').text(data[i].EVENT_NAME);
                    li.append(datespan).append(subject).append(br).append(eventName);
                }
                ul.append(li);
            }
        }
    }
    $('#hdiv' + type).empty();
    $('#hdiv' + type).append(ul);
}

function fn_Paging(tabType, step) {
    var totalCnt = $('#' + tabType + "Cnt").text();
    var pageNum = 1;
    if ($('#hdd' + tabType + 'PageNum').val() != "") {
        pageNum = parseInt($('#hdd' + tabType + 'PageNum').val());
    }

    if (step == "Next") {
        pageNum++;
    }
    else {
        pageNum--;
    }

    if (pageNum < 1) {
        alert("페이지의 처음입니다.")
        return false;
    }
    else if (totalCnt <= ((pageNum - 1) * 3)) {
        alert("페이지의 마지막입니다.")
        return false;
    }

    $('#hdd' + tabType + 'PageNum').val(pageNum);
    if (tabType == "UpcomingEvent" || tabType == "Payment")
        AjaxListBind(tabType, pageNum);
    else
        AjaxGridBind(tabType, pageNum);
}

function ApprovalCount() {
    var strUrl = "";
    var userID = $('#hddUserID').val();

    try {
        strUrl = EVENT_SERVICE_URL + "/SelectHomeEventCountSummary/" + userID;
        var d4 = $.Deferred();

        $.ajax({
            type: "GET",
            url: strUrl,
            dataType: "json",
            success: function (data) {
                d4.resolve(data);
                var NotSubmittedCnt = data[0].NOT_SUBMITTED;
                var ApprovalQueueCnt = data[0].APPROVAL_QUEUE;
                var PendingApprovalCnt = data[0].PENDING_APPROVAL;
                var DelegationCnt = data[0].DELEGATION;

                $('#TabNotSubmittedCnt').text("(" + NotSubmittedCnt + ")");
                $('#TabApprovalQueueCnt').text("(" + ApprovalQueueCnt + ")");
                $('#TabPendingApprovalCnt').text("(" + PendingApprovalCnt + ")");
                $('#TabDelegationCnt').text("(" + DelegationCnt + ")");
            }
        });

        d4.promise();
    }
    catch (ex) {
        alert(ex.message);
    }
}

function Bind3TimeVisits(date, type) {
    var strUrl = "";
    var userID = $('#hddUserID').val();

    try {
        strUrl = EVENT_SERVICE_URL + "/SelectHome3TimeVisits/" + userID + "/" + date;
        var d5 = $.Deferred();
        $(".btn-more").hide();
        $.ajax({
            type: "GET",
            url: strUrl,
            dataType: "json",
            success: function (data) {
                d5.resolve(data);
                Create3TimeVisits(data, type);
                $(".btn-more").show();
            }
        });

        d5.promise();
    }
    catch (ex) {
        alert(ex.message);
    }
}

function Create3TimeVisits(data, type) {
    var ul = $('<ul/>').addClass('person-list person-list-icon');
    var Count = 0;
    if (type == "Min") {
        Count = 4;
    }
    else {
        if (data.length < 3)
            Count = 4;
        else
            Count = data.length;
    }
    for (var i = 0; i < Count; i++) {
        var hcp = data[i];
        var li = $('<li/>');
        if (hcp) {
            var image = $('<i/>');
            var job = $('<span/>').addClass("item-job");
            job.html(hcp.HCP_TYPE + " <span class='badge' style='background-color:#67b8f3 !important'>" + hcp.VISIT_COUNT.toString() + "</span>");
            if (hcp.HCP_TYPE == "Doctor") {
                image.addClass('fa fa-stethoscope').attr("aria-hidden", true);
            }
            else if (hcp.HCP_TYPE == "Nurse") {
                image.addClass('fa fa-needle').attr("aria-hidden", true);
            }
            else if (hcp.HCP_TYPE == "Pharmacist") {
                image.addClass('fa fa-pill').attr("aria-hidden", true);
            } else if (hcp.HCP_TYPE == "Intern") {
                image.addClass('fa fa-stethoscope fa-intern').attr("aria-hidden", true);
            }
            else if (hcp.HCP_TYPE == "Radiologist") {
                image.addClass('fa fa-radiologist').attr("aria-hidden", true);
            } else {
                image.addClass('fa').attr("aria-hidden", true);
            }
            var name = $('<strong/>').addClass("item-name").text(hcp.HCP_NAME);
            var infoHtml = hcp.HCO_NAME + "<span class='divider'></span>" + hcp.SPECIALTY_NAME;
            var info = $('<p/>').addClass("item-info").html(infoHtml);
            var dot = $('<a/>').addClass("fa fa-ellipsis-h").attr("data-hcp-code", hcp.HCP_CODE).attr("data-hcp-name", hcp.HCP_NAME).attr("href", "#").attr("data-toggle", "modal").attr("data-target", "#hcp_info").html("<span class='tts'>More</span>");
            li.append(image).append(name).append(job).append(info).append(dot);
        } else {
            li.height(72);
        }
        ul.append(li);
    }
    $("#visitscnt").text(data.length);
    $('#hdiv3TimeVisits').append(ul);
    var h = $('#l_frame').height();
    $('#l_frame').height(h + $('#hdiv3TimeVisits').height() - 287); //287:기본값
    
}

function fn_MoreVisits(obj) {
    $("#hdiv3TimeVisits").empty();
    var hddSelectDate = $("#hddSelectDate").val();
    if ($("#hiMore").hasClass("fa-plus")) {
        $("#hiMore").toggleClass("fa-plus fa-minus");
        Bind3TimeVisits(hddSelectDate, "Max");
    }
    else {
        $("#hiMore").toggleClass("fa-minus fa-plus");
        Bind3TimeVisits(hddSelectDate, "Min");
    }
}

function fn_ChangeMonth(type) {
    $("#hdiv3TimeVisits").empty();
    var selectDate = new Date($("#hddSelectDate").val());
    var changeDate = "";
    if (type == "Next") {
        changeDate = new Date(selectDate.setMonth(selectDate.getMonth() + 1));
        $("#hspanMonth").text(Month[changeDate.getMonth()]);
        $('#hddSelectDate').val(formatDate(changeDate));
        if ($("#hiMore").hasClass("fa-plus"))
            Bind3TimeVisits(formatDate(changeDate), "Min");
        else
            Bind3TimeVisits(formatDate(changeDate), "Max");
    }
    else {
        changeDate = new Date(selectDate.setMonth(selectDate.getMonth() - 1));
        $("#hspanMonth").text(Month[changeDate.getMonth()]);
        $('#hddSelectDate').val(formatDate(changeDate));
        if ($("#hiMore").hasClass("fa-plus"))
            Bind3TimeVisits(formatDate(changeDate), "Min");
        else
            Bind3TimeVisits(formatDate(changeDate), "Max");
    }
}

//검색 결과 창에서 Pre/Next
function fn_ChangeSearchMonth(type) {
    var selectDate = new Date($("#hddSearchSelectDate").val());
    var hcpName = $("#divSearchResult h1").text();

    var changeDate = "";
    if (type == "Next") {
        changeDate = new Date(selectDate.setMonth(selectDate.getMonth() + 1));
    }
    else {
        changeDate = new Date(selectDate.setMonth(selectDate.getMonth() - 1));
    }
    $("#hspanSearchMonth").text(Month[changeDate.getMonth()]);
    $("#hspanSearchMonth").attr("title", changeDate.getFullYear().toString() + "-" + (changeDate.getMonth() + 1).toString());
    $('#hddSearchSelectDate').val(formatDate(changeDate));
    selectHCPInfo(hcpName, hcpName, false, formatDate(changeDate));
}

function selectFromCRMData() {

    var userID = $('#hddUserID').val();
    var d6 = $.Deferred();

    $.ajax({
        type: "GET",
        url: EVENT_SERVICE_URL + "/SelectHomeFromCRMData/" + userID,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            d6.resolve(data);
            setFromCRMData(data);
        },
        error: function (error) {
            d6.reject(error);
            fn_showError({
                message: error.responseText
            });
        },
    });

    d6.promise();
}

function setFromCRMData(list) {
    $(".person-list-crm").data("list", list);
    $("#crmData").text(list.length.toString()); //전체 건수 표시
    displayFromCRMDataPage(1);
}

function displayFromCRMDataPage(idx) {
    var dataList = $(".person-list-crm").data("list");
    var totalCnt = dataList.length;

    var totalPage = parseInt((totalCnt / 3) + 0.9); //전체 페이지
    $(".pagination").data("total", totalPage);
    var pageStartArrayIdx = (idx - 1) * 3;          //페이지 시작 index
    var pageLastArrayIdx = pageStartArrayIdx + 2;   //페이지 마지작 index
    //if (pageLastArrayIdx > totalCnt - 1) pageLastArrayIdx = totalCnt - 1;

    //해당 페이지 내용 표시
    $(".person-list-crm").empty();
    for (var i = pageStartArrayIdx ; i <= pageLastArrayIdx; i++) {

        var hcp = dataList[i];

        var $li = $('<li/>');
        if (hcp) {
            var $strong = $('<strong/>').addClass('item-name').text(hcp.HCP_NAME);
            var $spanType = $('<span/>').addClass('item-job').text(hcp.HCP_TYPE);
            var $spanDate = $('<span/>').addClass('item-date').text(hcp.EVENT_DATE);
            if (hcp.IS_VIOLATED == "V") $spanDate.css('color', '#b53b4f')
            var $spanSpec = $('<span/>').addClass('item-cate').text(hcp.CRM_STATUS);
            var $pHco = $('<p/>').addClass('item-info');
            var hcoHtml = hcp.HCO_NAME + "<span class='divider'></span>" + hcp.SPECIALTY_NAME;
            $pHco.html(hcoHtml);

            $li.append($spanDate);
            $li.append($strong);
            $li.append($spanType);
            $li.append($spanSpec);
            $li.append($pHco);
            $(".person-list-crm").append($li);
        } else {
            $li.height(64);
            $(".person-list-crm").append($li);
        }
    }

    //페이지 관련 설정
    $(".pagination").data("current", idx);

    var d = idx / 2.0;
    var rem = idx % 2.0;
    if (rem > 0)
        d = d + 0.5;
    var firstBtn = ((parseInt(d) - 1) * 2) + 1;
    var secondBtn = firstBtn + 1;
    $(".pagination li").removeClass();

    $(".pagination #firstButton").text(firstBtn.toString());
    $(".pagination #secondButton").text(secondBtn.toString());

    $.map($(".pagination").children(), function (li) {
        if ($(li).text() == idx.toString()) {
            $(li).addClass("active");
        }
    });
}
