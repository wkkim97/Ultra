$(function () {
    var item = GridData();
    loadGrid(item);

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
        linkField: "lb_DocStart",
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
        linkField: "lb_DocEnd",
        linkFormat: "yyyy-mm-dd"
    });

    $('#hbtnEvtStart').datetimepicker({
        weekStart: 0,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        forceParse: 0,
        minuteStep: 60,
        minView: 2,
        maxView: 2,
        linkField: "lb_EvtStart",
        linkFormat: "yyyy-mm-dd"
    });

    $('#hbtnEvtEnd').datetimepicker({
        weekStart: 0,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        forceParse: 0,
        minuteStep: 60,
        minView: 2,
        maxView: 2,
        linkField: "lb_EvtEnd",
        linkFormat: "yyyy-mm-dd"
    });
});

function loadGrid(item) {
    $("#divCompletedApproval").jsGrid({
        width: "100%",
        filtering: true,
        sorting: true,
        paging: true,
        autoload: true,
        pageSize: GRID_LIST_COUNT,
        pageButtonCount: 5,
        pageNavigatorNextText: "...",
        pageNavigatorPrevText: "...",
        rowDoubleClick: function (args) {
            var data = args.item;
            var url = "/ultra/Pages/Event/" + data.WEB_PAGE_NAME;
            var eventID = data.EVENT_ID;
            var title = data.EVENT_NAME;
            var processID = data.PROCESS_ID;
            fn_AddTabPage(url, eventID, title, processID);
        },
        controller: {
            data: item,
            loadData: function (filter) {
                return $.grep(this.data, function (item) {
                    return (!filter.EVENT_NAME || item.EVENT_NAME.toLowerCase().indexOf(filter.EVENT_NAME.toLowerCase()) > -1)
                        && (!filter.SUBJECT || item.SUBJECT.toLowerCase().indexOf(filter.SUBJECT.toLowerCase()) > -1)
                        && (!filter.REQUESTER || item.REQUESTER.toLowerCase().indexOf(filter.REQUESTER.toLowerCase()) > -1)
                        && (!filter.FINAL_APPROVER_NAME || item.FINAL_APPROVER_NAME.toLowerCase().indexOf(filter.FINAL_APPROVER_NAME.toLowerCase()) > -1)
                        && (!filter.PROCESS_STATUS || item.PROCESS_STATUS.toLowerCase().indexOf(filter.PROCESS_STATUS.toLowerCase()) > -1)
                        && (!filter.EVENT_KEY || item.EVENT_KEY.toLowerCase().indexOf(filter.EVENT_KEY.toLowerCase()) > -1)
                        && (!filter.REQUEST_DATE || item.REQUEST_DATE.indexOf(filter.REQUEST_DATE) > -1)
                        && (!filter.START_DATE || item.START_DATE.toString().indexOf(filter.START_DATE.toString()) > -1);

                });
            }
        },
        fields: [
             { name: "EVENT_KEY", title: "Event Key", type: "text", width: 50 },
            { name: "REQUEST_DATE", title: "Req Date", type: "text", width: 30 },
            { name: "START_DATE", title: "Start Date", type: "text", width: 30 },
            { name: "EVENT_NAME", title: "Title", type: "text", width: 60 },
            {
                name: "SUBJECT", title: "Subject", type: "text",
                itemTemplate: function (value, item) {
                    return "<a href='#' class='link text-ellipsis' onclick='fn_DetailView(this);'>" + value + "</a>";
                }
            },
            { name: "REQUESTER", title: "Requester", type: "text", width: 35 },
            { name: "FINAL_APPROVER_NAME", title: "Final Approver", type: "text", width: 35 },
            { name: "PROCESS_STATUS", title: "Status", type: "text", width: 50 }
           
        ]
    });
}

function fn_DetailView(obj) {
    // 해당 Row dblclick 이벤트 발생
    $(obj).dblclick();
}


function GridData() {
    var retVal = "";
    var userID = $('input[id$=hddUserID]').val();
    var search = {
        processStatus: "PaymentCompleted",
        userID: userID,
        searchType: "",
        searchText: "",
        doc_Start: "",
        doc_End: "",
        evt_Start: "",
        evt_End: ""
    }
    $.ajax({
        type: "POST",
        url: EVENT_SERVICE_URL + "/SelectApprovalCompletedList",
        data: JSON.stringify(search),
        async: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (returnData) {
            retVal = returnData;
        }
    });
    return retVal;
}

function SearchData() {
    var retVal = "";
    var userID = $('input[id$=hddUserID]').val();
    var searchType = "";
    if ($('input[name$=chk]:checked').length > 0) {
        searchType = $('input[name$=chk]:checked').map(function () {
            return $(this).data("event-id");
        }).get().join(",");
    }
    var searchText = $("#lb_Subject").val();
    var doc_Start = $("#lb_DocStart").val();
    var doc_End = $("#lb_DocEnd").val();
    var evt_Start = $("#lb_EvtStart").val();
    var evt_End = $("#lb_EvtEnd").val();

    var search = {
        processStatus: "PaymentCompleted",
        userID: userID,
        searchType: searchType,
        searchText: searchText,
        doc_Start: doc_Start,
        doc_End: doc_End,
        evt_Start: evt_Start,
        evt_End: evt_End
    };
    $.ajax({
        type: "POST",
        url: EVENT_SERVICE_URL + "/SelectApprovalCompletedList",
        data: JSON.stringify(search),
        async: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (returnData) {
            retVal = returnData;
        }
    });
    return retVal;
}

function CheckAll() {
    if ($("#checkall").prop("checked")) {
        $("input[name=chk]").prop("checked", true);
    } else {
        $("input[name=chk]").prop("checked", false);
    }
}

function detailSearch() {
    var item = SearchData();
    loadGrid(item);
    $("#Detail_Search").modal('hide');
}

function fn_DelInput(input) {
    $('input[id$=' + input + ']').val('');
}