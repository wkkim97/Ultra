$(function () {
    try {
        fn_Init();
    }
    catch (ex) {
        fn_showError({ message: ex.message });
    }
});
function fn_Init() {
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

    $("#btnSearch").on("click", fn_Search);

    $("#btnCreate").on("click", fn_CreateReport);

    fn_InitControls();
}

function fn_InitControls() {
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

                var selectUrl = REPORT_SERVICE_URL + "/SelectParticipantsSourceList";
                var d = $.Deferred();

                $.ajax({
                    url: selectUrl,
                    type: "POST",
                    dataType: "json",
                    data: JSON.stringify(fn_GetFilter()),
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    d.resolve(response);
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    d.reject(jqXHR);
                });

                return d.promise();
            }
        },

        fields: [
            { name: "EVENT_KEY", title: "Event Key", type: "text", width: 140 },
            { name: "PROCESS_ID", title: "Process ID", type: "text" },
            { name: "HOST", title: "주최자", type: "text" },
            { name: "HOST_CODE", title: "주최자 Code", type: "text" },
            { name: "SUBJECT", title: "학술대회명", type: "text" },
            { name: "VENUE", title: "Venue", type: "text" },
            { name: "START_TIME", title: "Start Date", type: "text", width: 120 },
            { name: "END_TIME", title: "End Date", type: "text", width: 120 },
            { name: "COST_CATEGORY", title: "End Date", type: "text" },
            {
                name: "AMOUNT", title: "금액", type: "number", width: 80, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            { name: "CONGREES_TYPE", title: "CongressType", type: "text" },
            {
                name: "HCP_NO", title: "참가자 지원수", type: "number", width: 60, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            { name: "KRPIA", title: "KRPIA", type: "text" },
            { name: "REQUESTER_ID", title: "Requester", type: "text" },
            { name: "REQUESTER_ORG", title: "Requester's ORG", type: "text" }
        ]
    });
}

function fn_Search() {
    $("#jsGridSourceList").jsGrid("loadData");
}

function fn_CreateReport() {
    $("#modalReport").modal("show");
}