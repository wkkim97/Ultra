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

                var selectUrl = REPORT_SERVICE_URL + "/SelectMedicalStudySourceList";
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
            { name: "INDEX", title: "Index", type: "text", width: 50 },
            { name: "CONTRACT_NO", title: "Contract No", type: "text" },
            { name: "AUTHOR", title: "Author", type: "text" },
            { name: "CATEGORY", title: "Category", type: "text" },
            { name: "STATUS", title: "Category", type: "text" },
            { name: "TITLE", title: "Title", type: "text" },
            { name: "HEAD_HCP", title: "Head Hcp", type: "text" },
            { name: "HEAD_HCO", title: "Head Hco", type: "text" },
            { name: "OTHER_HCP", title: "Other Hcp", type: "text" }, 
            {
                name: "PAYMENT_COST", title: "Product Qty", type: "number", width: 80, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            { name: "PAYMENT_EVIDENCE", title: "Payment Evidence", type: "text" },
            { name: "PAYMENT_CREATOR", title: "payment Creator", type: "text" },
            { name: "FREE_GOODS", title: "Free Goods", type: "text" } 
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