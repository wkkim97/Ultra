$(function () {
    $("#jsGridCostPlan").jsGrid({
        width: "100%",
        height: "200px",

        sorting: true,
        paging: false,
        autoload: true,
        rowDoubleClick: function (args) {
            var item = args.item;
            $('select[id$=lb_category]').val(item.CATEGORY_CODE);
            $("#hddCostPlanIDX").val(item.COST_PLAN_IDX);
            $('#lb_desc').val(item.DESC);
            $('#lb_qty').val(fn_AddComma(item.QTY));
            $('#lb_price').val(fn_AddComma(item.PRICE));
            $('#lb_amount').val(fn_AddComma(item.QTY * item.PRICE));
            $('#hddCostPlanIDX').val(item.COST_PLAN_IDX);
        },
        controller: {
            loadData: function () {

                var selectUrl = EVENT_SERVICE_URL + "/selectcostplan/" + $('input[id$=hddProcessID]').val();
                var d = $.Deferred();

                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    cache:false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    
                    d.resolve(response);
                    var amount = 0;
                    for (var i = 0; i < response.length; i++) {
                         amount = amount + (response[i].QTY * response[i].PRICE);
                    }

                    $('#totalCostPlan').text(fn_AddComma(amount));
                    if (response.length > 0)
                        $('#tabCostPlan .fa-commenting').hide();
                    else
                        $('#tabCostPlan .fa-commenting').show();

                    fn_DisplayCostPlan(response); /* GeneralInformation에 반영 */
                }).fail(function (jqXHR, textStatus, errorThrown) {
                });

                return d.promise();
            }
        },

        fields: [
            {
                name: "CATEGORY_NAME", title: "Category", type: "text", width: 140,
                itemTemplate: function (value, item) {
                    return "<a href='#' class='link text-ellipsis' onclick='showCostPlan(this);'>" + value + "</a>";
                }
            },
            { name: "DESC", title: "Description", type: "text" },
            {
                name: "QTY", title: "Qty", type: "number", width: 50, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            {
                name: "PRICE", title: "Price", type: "number", width: 80, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            {
                name: "AMOUNT", title: "Amount", type: "number", width: 80, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            }
        ]
    });

    /* Cost Plan 저장 */
    $('#btnSaveCostPlan').click(function () {

        if (!checkSavedStatus()) {
            fn_showWarning({
                title: "Confirm",
                message: "Please save the event."
            }).done(function () {
                $('#tabEventMaster a:first').tab('show');
            });
        } else {

            var eventID = $('input[id$=hddEventID]').val();
            var processID = $('input[id$=hddProcessID]').val();
            var costPlanIDX = parseInt($('input[id$=hddCostPlanIDX]').val());
            var categoryCode = $('select[id$=lb_category]').val();
            var qty = parseInt(fn_RemoveComma($('#lb_qty').val()));
            var price = parseInt(fn_RemoveComma($('#lb_price').val()));
            var desc = $('#lb_desc').val();
            var userID = $('input[id$=hddUserID]').val();

            var costPlan = {
                EVENT_ID: eventID,
                PROCESS_ID: processID,
                COST_PLAN_IDX: costPlanIDX,
                CATEGORY_CODE: categoryCode,
                QTY: qty,
                PRICE: price,
                DESC: desc,
                IS_DELETED: 'N',
                CREATOR_ID: userID,
                UPDATER_ID: userID
            }

            $.ajax({
                url: EVENT_SERVICE_URL + "/mergecostplan",
                type: "POST",
                data: JSON.stringify(costPlan),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                processdata: true,
                crossDomain: true,
                success: function (data) {
                    clearInputCostPlan();
                    $('input[id$=hddProcessID]').val(data);
                    $("#jsGridCostPlan").jsGrid("loadData"); //Cost Plan 재조회
                },
                error: function (error) {
                    alert("Costplan :" +error.responseText);
                },
            });
        }
    });

    /* Cost Plan 삭제 */
    $('#btnDeleteCostPlan').click(function () {


        var processID = $('input[id$=hddProcessID]').val();
        var costPlanIDX = parseInt($('input[id$=hddCostPlanIDX]').val());
        var userID = $('input[id$=hddUserID]').val();

        if (costPlanIDX < 1) {
            fn_showWarning({
                title: "Confirm",
                message: "Please select an costplan."
            })
            return;
        }

        fn_confirm()
        .done(function (result) {
            if (result) {
                var deleteCostPlan = {
                    processID: processID,
                    costPlanIDX: costPlanIDX,
                    updaterID: userID
                }

                $.ajax({
                    url: EVENT_SERVICE_URL + "/deletecostplan",
                    type: "POST",
                    data: JSON.stringify(deleteCostPlan),
                    //dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    processdata: true,
                    crossDomain: true,
                    success: function (data) {
                        clearInputCostPlan();
                        $("#jsGridCostPlan").jsGrid("loadData"); //Cost Plan 재조회
                    },
                    error: function (error) {
                        alert("CostPlan :"+error.responseText);
                    },
                });
            }
        });      
    });

    /* Reset */
    $("#btnResetCostPlan").click(function () {
        clearInputCostPlan();
    });


    $('#lb_qty').keyup(function () {
        calculateCategoryAmount();
    });

    $('#lb_price').keyup(function () {
        calculateCategoryAmount();
    });

});

function clearInputCostPlan() {
    $('input[id$=hddCostPlanIDX]').val('0');
    $("#lb_category").val($("#lb_category option:first").val())
    $('#lb_desc').val('');
    $('#lb_qty').val('0');
    $('#lb_price').val('0');
    $('#lb_amount').val('0');
    $('#totalCostPlan').text('0');
}

function calculateCategoryAmount() {
    var qty = fn_RemoveComma($('#lb_qty').val().toString());
    var price = fn_RemoveComma($('#lb_price').val().toString());

    if (!qty) qty = 0;
    else qty = parseInt(qty);

    if (!price) price = 0;
    else price = parseInt(price);

    var amount = qty * price;

    $('#lb_amount').val(fn_AddComma(amount));

}

function showCostPlan(obj) {
    $(obj).dblclick();
}