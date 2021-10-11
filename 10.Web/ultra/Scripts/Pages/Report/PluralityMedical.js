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

                var selectUrl = REPORT_SERVICE_URL + "/SelectPluralityMedicalSourceList";
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
            { name: "PRODUCT_STANDARD_NAME", title: "제품명 (표준코드명칭)", type: "text" },
            { name: "HCP_NAME", title: "성명", type: "text", width: 80 },
            { name: "HCO_NAME", title: "소속", type: "text", width: 100 },
            { name: "COST_CATEGORY_NAME", title: "지출항목", type: "text", width: 80 },
            {
                name: "AMOUNT", title: "합계", type: "number", width: 60, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            { name: "ADDRESS_OF_VENUE", title: "장소", type: "text", width: 80 },
            { name: "START_TIME", title: "시작일시", type: "text", width: 80 },
            { name: "END_TIME", title: "종료일시", type: "text", width: 80 },
        ]
    }); 
}

function fn_Search()
{
    $("#jsGridSourceList").jsGrid("loadData");
}

function fn_CreateReport()
{
    $("#modalReport").modal("show");
}