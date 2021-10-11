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

                var selectUrl = REPORT_SERVICE_URL + "/SelectMarketResearchSourceList";
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
            { name: "CONTRACT_ID", title: "ContractNo", type: "text", width: 140 },
            { name: "PRODUCT_NAME", title: "Product", type: "text" },
            { name: "HCP_NAME", title: "HCP", type: "text" },
            { name: "PAYMENT_WAY", title: "재심사", type: "text" },
            {
                name: "QTY", title: "단가/건", type: "number", width: 80, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            {
                name: "PRICE", title: "건수", type: "number", width: 60, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            {
                name: "AMOUNT", title: "총비용", type: "number", width: 100, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            { name: "COMMENT", title: "Comment", type: "text" },
            { name: "PAYMENT_DATE", title: "비용지급일자", type: "text" },
            { name: "EVIDENCE_ID", title: "Evidence No.", type: "text" }
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