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

    $("#divExceptArea").show();

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
        autoload: false,
        controller: {
            loadData: function () {

                var selectUrl = REPORT_SERVICE_URL + "/SelectIndividualMedicalSourceList";
                var d = $.Deferred();
                var dto = fn_GetFilter();
                $.ajax({
                    url: selectUrl,
                    type: "POST",
                    dataType: "json",
                    data: JSON.stringify(dto),
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
            { name: "EVENT_KEY", title: "Event Key", type: "text" },
            { name: "HCP_NAME", title: "성명", type: "text" },
            { name: "HCO_NAME", title: "소속", type: "text" },
            { name: "COST_CATEGORY_NAME", title: "지출항목", type: "text" },
            {
                name: "AMOUNT", title: "합계", type: "number", width: 80, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            { name: "ADDRESS_OF_VENUE", title: "장소", type: "text" },
            { name: "EVENT_DATE", title: "Event Date", type: "text" },
            { name: "EVENT_TYPE", title: "Event Type", type: "text" },
        ]
    });
}

function fn_Search() {
    $("#jsGridSourceList").jsGrid("loadData");
}

function fn_CreateReport() {
    $("#modalReport").modal("show");
}