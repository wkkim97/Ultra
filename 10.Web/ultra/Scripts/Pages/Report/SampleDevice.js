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

                var selectUrl = REPORT_SERVICE_URL + "/SelectSampleSourceList";
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
            { name: "EVENT_KEY", title: "Event Key", type: "text", width: 50 },
            { name: "PRODUCT_NAME", title: "Product Name", type: "text" },
            { name: "PRODUCT_STANDARD_NAME", title: "Product Standard Name", type: "text" },
            { name: "PRODUCT_STANDARD_CODE", title: "Product Standard Code", type: "text" },
            {
                name: "PRODUCT_QTY", title: "Product Qty", type: "number", width: 80, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            { name: "HCO_NAME", title: "HCO Name", type: "text" },
            { name: "HCO_CODE", title: "HCO Code", type: "text" },
            { name: "HCO_HIRA_CODE", title: "HCO HIRA Code", type: "text" },
            { name: "HCP_NAME", title: "HCP Name", type: "text" },
            { name: "HCP_CODE", title: "HCP Code", type: "text" },
            {
                name: "QTY", title: "Qty", type: "number", width: 80, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            }, 
            { name: "PURPOSE", title: "Purpose", type: "text" } 
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