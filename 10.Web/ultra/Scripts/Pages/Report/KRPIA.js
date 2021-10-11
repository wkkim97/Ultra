$(function () {
    try {
        fn_Init();
    }
    catch (ex) {
        fn_showError({ message: ex.message });
    }
});
function fn_Init()
{
    $.ajaxSetup({
        beforeSend: function (xhr) {
            $('body').waitMe({
                effect: 'win8',
                text: 'Please wait...'
            });
        },
        success: function (r, status, xhr) {
            $('body').waitMe("hide");
        },
        error: function (xhr, status, error) {
            $('body').waitMe("hide");
        }
    });
    $("#divKRPIAArea").show();
    $("#btnSearch").on("click", fn_Search);

    $("#btnCreate").on("click", fn_CreateReport);
     
    fn_InitControls();
}

function fn_InitControls()
{
    $("#jsGridSourceList").jsGrid({
        width: "100%",
        sorting: true,
        paging: true,
        pageSize: GRID_LIST_COUNT,
        autoload: true,
        rowDoubleClick: function (args) {
            var item = args.item;
            fn_GetPmsDetail(item.IDX);
        },
        controller: {
            loadData: function () {

                var selectUrl = REPORT_SERVICE_URL + "/SelectKRPIASourceList";
                
                var d = $.Deferred();

                $.ajax({
                    url: selectUrl,
                    type: "POST",
                    dataType: "json",
                    data: JSON.stringify(fn_GetFilter_krpia()),
                    
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    
                    d.resolve(response);
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    console.log(jqXHR);
                    console.log(textStatus);
                    console.log(errorThrown);
                    d.reject(jqXHR);
                });

                return d.promise();
            }
        },

        fields: [
            { name: "START_DATE", title: "START_DATE", type: "text", width: 50 },
            { name: "END_DATE", title: "START_DATE", type: "text" },
            { name: "LOCATION", title: "LOCATION", type: "text" },
            { name: "PURPOSE", title: "PURPOSE", type: "text" },
            { name: "HCP_NAME", title: "HCP_NAME", type: "text" },
            { name: "HCO_NAME", title: "HCO_NAME", type: "text" },
            { name: "YOURDOCS_AMOUNT", title: "YOURDOCS_AMOUNT", type: "text" },
            { name: "SUBJECT", title: "SUBJECT", type: "text" },
            { name: "CLEARING_DATE", title: "CLEARING_DATE", type: "text" },
            { name: "REMARK", title: "REMARK", type: "text" },
            { name: "KRPIA", title: "KRPIA", type: "text" },
            { name: "HCP_CODE", title: "HCP_CODE", type: "text" },
            { name: "YOUR_DOCES_VENDER", title: "YOUR_DOCES_VENDER", type: "text" } 
        ]
    }); 
}
function fn_GetFilter_krpia() {

    var chkTYPEConsultingYN = $("#chkTYPEConsultingYN").is(":checked") ? "Y" : "N",
        chkTYPELectureYN = $("#chkTYPELectureYN").is(":checked") ? "Y" : "N"
    var type = "ALL";
    if (chkTYPEConsultingYN == "Y") { type = "Consulting"; }
    if (chkTYPELectureYN == "Y") { type = "Lecture"; }
    if (chkTYPEConsultingYN == "Y" && chkTYPELectureYN == "Y") {
        type = "ALL";
    }
        

    var datepo = new Date($("#datEndTime").val());
    var days = 1;
    datepo.setDate(datepo.getDate() + days);
    var strFormatedPODate = $.datepicker.formatDate('yy-mm-dd', new Date(datepo));

    var dto = {
        START_DATE: $("#datStartTime").val()
        , END_DATE: strFormatedPODate
        , MOHW_TYPE: type
        , USER_ID: ""
        
    };
    console.log(dto);
    return dto;
}

function fn_Search()
{
    
    $("#jsGridSourceList").jsGrid("loadData");
}

function fn_CreateReport()
{
    $("#modalReport").modal("show");
}