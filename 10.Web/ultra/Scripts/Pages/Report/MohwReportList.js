$(function () {
    var item = GridData();
    loadGrid(item);

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
    $("#jsGridList").jsGrid({
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
        },
        controller: {
            data: item,
            loadData: function (filter) {
                return $.grep(this.data, function (item) {
                    return (!filter.MOHW_TYPE_N || item.MOHW_TYPE_N.toLowerCase().indexOf(filter.MOHW_TYPE_N.toLowerCase()) > -1)
                        && (!filter.SUBJECT || item.SUBJECT.toLowerCase().indexOf(filter.SUBJECT.toLowerCase()) > -1)
                          && (!filter.STATUS_NAME || item.STATUS_NAME.toLowerCase().indexOf(filter.STATUS_NAME.toLowerCase()) > -1);
                });
            }
        },
        fields: [
            {
                title: "Delete", type: "date", width: 30,
                itemTemplate: function (value, item) {
                    var $obj = $("<span>");
                    var status = item.STATUS;
                    status = status.toLowerCase();

                    if (status != 'complete') {
                        var $btnDel = $("<button>")
                                        .attr("type", "button")
                         .css("margin-left", "5px")
                        .addClass("btn btn-sm btn-gray")
                        .append($("<span>").text("Del"))
                             .on("click", function () {
                                 fn_Execute(item, 'Delete');
                             });
                        $obj.append($btnDel);
                    }
                    return $obj;
                }
            },
             { name: "CREATE_DATE", title: "생성일자", type: "date", width: 30 }, 
             { name: "MOHW_TYPE_N", title: "Report Type", type: "text", width: 50  
             },
            {
                name: "SUBJECT", title: "Subject", type: "text",
                itemTemplate: function (value, item) {
                    return "<a href='#' class='link text-ellipsis' onclick='fn_DetailView(this);'>" + value + "</a>";
                }
            },
            {
                name: "Condition", title: "Condition", type: "date", width: 80,
                 itemTemplate: function (value, item) {
                     var $span = $("<span>");
                                
                     var html = "시작일자 : " + item.START_DATE + "<br/>"
                                + "종료일자 : " + item.END_DATE;

                     if (item.MOHW_TYPE == 'DIV_MEDICAL' || item.MOHW_TYPE == 'PLURALITY_MEDICAL')
                     {
                         if (item.IS_FOOD_COST_YN == 'Y')
                         {
                             html += "<br/>식음료 비용이 아닌 금액 ";
                        }
                         if (item.BELOW_ONE_WON_YN == 'Y') {
                             html += "<br/>기념품 또는 식음료 1만원이하";
                         }
                         if (item.EXCEPT_IECTURER_FOOD_COST_YN == 'Y') {
                             html += "<br/>강연자 식음료 금액 제외";
                         }
                         if (item.ONLY_ATTEND_YN == 'Y') {
                             html += "<br/>Only 참석자";
                         }
                         if (item.ONLY_MEDICINE_YN == 'Y') {
                             html += "<br/>Only 의약품";
                         }
                         if (item.ONLY_MEDICAL_EQUIPMENT_YN == 'Y') {
                             html += "<br/>Only 의료기기";
                         }
                         if (item.EXCEPT_BAYER_EMPLOYEEE_YN == 'Y') {
                             html += "<br/>Bayer Employee 제외";
                         }

                     }

                     $span.html(html);
                     return $span;
                }
            },
            {
                title: "Report<br/>Source", width: 50,
                itemTemplate: function (value, item) {

                    var $obj = $("<span>");
                    var status = item.FILE_STATUS;
                    status = status.toLowerCase();
                  
                    if (status == 'wait') {
                        var $btn = $("<span>").css("margin-left", "5px").html("Excel<br/>Runing");
                        $obj.append($btn);
                    }
                    else if (status == 'created') {
                        var $btnDown = $("<button>")
                                 .attr("type", "button")
                                 .css("margin-left", "5px")
                                 .addClass("btn btn-sm btn-navy")
                                 .append($("<i>").addClass('fa fa-floppy-o'))
                                 .append($("<span>").text("Excel"))
                                 .on("click", function () {
                                    var url = UPLOAD_HANDLER_URL + "?file=" + encodeURIComponent(item.FILE_PATH);
                                        
                                    //window.open(url);
                                    $('#iframeFileDown', parent.document).attr('src', url);
                                 });
                        $obj.append($btnDown);
                    }
                    else {
                        var $btnDown = $("<button>")
                                   .attr("type", "button")
                                   .css("margin-left", "5px")
                                   .addClass("btn btn-sm btn-success")
                                    .append($("<i>").addClass('fa fa-floppy-o'))
                                   .append($("<span>").text("Create"))
                                   .on("click", function () {
                                       var userID = $("input[id$=hddUserID]").val();
                                       $.get(REPORT_SERVICE_URL + "/CreateXlsMohwReport/" + item.IDX + "/" + item.MOHW_TYPE + "/" + userID);
                                       setTimeout(function () {
                                           var item = GridData();
                                           loadGrid(item);
                                       }, 500);
                                     
                                   });
                        $obj.append($btnDown);
                    }
                    return $obj;
                }
            },
            {
                title: "MOHW<br/>Report", width: 70,
                itemTemplate: function (value, item) {

                    var $obj = $("<span>");
                    
                    var status = item.FILE_STATUS;
                    status = status.toLowerCase();

                    if (status != 'created')
                    {
                        return $obj;
                    }

                    // MOHW Report(Xls) 생성
                    if (item.MOHW_STATUS.toLowerCase() == "" || item.MOHW_STATUS.toLowerCase() == "fail")
                    {
                        // Excel 생성 버튼
                        var $btnXls = $("<button>")
                               .attr("type", "button")
                                    .css("margin-left", "5px")
                               .addClass("btn btn-sm btn-success")
                               .append($("<i>").addClass('fa fa-floppy-o'))
                               .append($("<span>").text("Create"))
                               .on("click", function () {
                                   var userID = $("input[id$=hddUserID]").val();
                                   $.get(REPORT_SERVICE_URL + "/CreateMohwCompleteReport/" + item.IDX + "/" + item.MOHW_TYPE + "/" + userID, function () {
                                       var item = GridData();
                                       loadGrid(item);
                                   }); 
                               });
                        $obj.append($btnXls);
                    }
                    // MOHW Report 생성되였을 경우
                    else if (item.MOHW_STATUS.toLowerCase() == "created")
                    {
                        var $btnXls = $("<button>")
                                .attr("type", "button")
                                     .css("margin-left", "5px")
                                .addClass("btn btn-sm btn-navy")
                                .append($("<i>").addClass('fa fa-floppy-o'))
                                .append($("<span>").text("Excel"))
                                .on("click", function () {
                                    var url = UPLOAD_HANDLER_URL + "?file=" + encodeURIComponent(item.MOHW_PATH); 
                                    //window.open(url);
                                    $('#iframeFileDown', parent.document).attr('src', url);
                                });
                        $obj.append($btnXls);

                        var status = item.STATUS.toLowerCase();
                        status = status.toLowerCase();

                        if (status == 'complete') {
                            // 취소버튼
                            /*
                            var $btnCan = $("<button>")
                                     .attr("type", "button")
                                 .css("margin-left", "5px")
                                     .addClass("btn btn-sm btn-warning")
                                     .append($("<span>").text("취소"))
                                     .on("click", function () {
                                         fn_Execute(item, 'Cancel');
                                     });
                            $obj.append($btnCan);
                           */
                        }
                        else {
                            // 확정버튼
                            var $btnComfirm = $("<button>")
                                      .attr("type", "button")
                                      .css("margin-left", "5px")
                                      .addClass("btn btn-sm btn-success")
                                      .append($("<span>").text("확정"))
                                      .on("click", function () {
                                          fn_Execute(item, 'Complete');
                                      });
                            $obj.append($btnComfirm);
                        }  
                    } else if (item.MOHW_STATUS.toLowerCase() == "wait") {
                        var $btnXls = $("<span>").css("margin-left", "5px").html("Excel<br/>Runing");
                        $obj.append($btnXls);
                    }
     

                    return $obj;
                }
            } ,
            { name: "STATUS_NAME", title: "확정여부", type: "text", width: 30 }
        ]
    });
}
 
function fn_CreateMohwXls()
{
    
}
function fn_Execute(item, statusValue)
{
    var msg = "";
    if( statusValue == 'Delete')
    {
        msg = "'"+ item.SUBJECT + "'을(를) 삭제 하시겠습니까?";
    }
    else if ( statusValue == 'Cancel' ) {
        msg = "'" + item.SUBJECT + "'을(를) 확정취소 하시겠습니까?";
    }
    else if (statusValue == 'Complete') {
        msg = "'"+ item.SUBJECT + "'을(를) 확정 하시겠습니까?";
    }

    fn_confirm(
    {
        title: "confirm",
        message: msg
    })
    .done(function (result) {
        if (result) {
            fn_UpdateHohwStatus(item, statusValue);
        }
    });
}

function fn_UpdateHohwStatus(item, statusValue)
{
    var userid = $("input[id$=hddUserID]").val();
    var parmas = {
            idx : item.IDX
            , mohwType: item.MOHW_TYPE
            , status: statusValue
            , userId: userid
        }
    $.ajax({
        type: "POST",
        url: REPORT_SERVICE_URL + "/UpdateMohwStatus",
        data: JSON.stringify(parmas),
        async: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            var result = data.UpdateMohwStatusResult;
            if (result == "OK")
            {
                var item = GridData();
                loadGrid(item);
            }
            else if (result == "EXISTS_CONFIRM")
            {
                fn_showWarning({
                    title: "경고",
                    message: "해당 조건으로 확정된 항목이 1건이상 존재하여 확정 할 수 없습니다."
                });
            }

        }
    });
}

function fn_DetailView(obj) {
    // 해당 Row dblclick 이벤트 발생
   
}


function GridData() {
    var retVal = "";
    var userID = $('input[id$=hddUserID]').val();
 
    var search = {
        subject: '',
        mohwType: '',
        startDate: '',
        endDate: ''
    };
    $.ajax({
        type: "POST",
        url: REPORT_SERVICE_URL + "/SelectMohwList",
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
   
    var searchText = $("#lb_Subject").val(); 
    var evt_Start = $("#lb_EvtStart").val();
    var evt_End = $("#lb_EvtEnd").val();
  
    var search = { 
        subject: searchText,
        mohwType: searchType,
        startDate: evt_Start,
        endDate: evt_End
    };
    $.ajax({
        type: "POST",
        url: REPORT_SERVICE_URL + "/SelectMohwList",
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