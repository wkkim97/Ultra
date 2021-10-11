var state = {
    selected_row: {}
}
$(function () {

    GridData(function (output) {
        loadGrid(output);
    });

    $("#btnNew").click(function () {
        clearNewMaster();
    });

    $("#btnSave").click(function () {

        var data = {};
        if (state.selected_row && state.selected_row.ID) data.ID = state.selected_row.ID;
        data.MANUFACTURE = $("#txtManufacture").val();
        data.SEGMENT = $("#txtSegment").val();
        data.PRODUCT_FAMILY = $("#txtProductFamily").val();
        data.PRODUCT = $("#txtProduct").val();
        data.PRICE = parseInt($("#txtPrice").val().replace(/\D/gi, "")) || 0;
        data.CREATOR_ID = $('#hddUserID').val();

        $.ajax({
            url: RADIOLOGY_SERVICE_URL + "/MergeMasterMarketShare",
            type: "POST",
            data: JSON.stringify(data),
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                alert("저장되었습니다.");
                clearNewMaster();

                GridData(function (output) {
                    loadGrid(output);
                });
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });

    });


    $("#btnDelete").click(function () {

        var id = 0;
        if (state.selected_row && state.selected_row.ID) id = state.selected_row.ID;

        if (!id) {
            alert("삭제할 항목을 선택해 주세요.")
            return false;
        }

        if (!confirm("삭제하시겠습니까?")) return false;

        $.ajax({
            url: RADIOLOGY_SERVICE_URL + "/DeleteMasterMarketShare/" + id,
            type: "GET",
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                alert("삭제되었습니다.");
                clearNewMaster();

                GridData(function (output) {
                    loadGrid(output);
                });
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });

    });
});

function clearNewMaster() {
    $("#txtManufacture").val(null);
    $("#txtSegment").val(null);
    $("#txtProductFamily").val(null);
    $("#txtProduct").val(null);
    $("#txtPrice").val(null);
    $('.jsgrid-clicked-row').removeClass('jsgrid-clicked-row');
    state.selected_row = {};
}

function loadGrid(item) {
    $("#jsGridMicroMarketing").jsGrid({
        width: "100%",
        height: "500px",
        filtering: true,
        sorting: true,
        paging: false,
        autoload: true,
        controller: {
            data: item,
            loadData: function (filter) {
                return $.grep(this.data, function (item) {
                    return (!filter.MANUFACTURE || item.MANUFACTURE.toLowerCase().indexOf(filter.MANUFACTURE.toLowerCase()) > -1)
                        && (!filter.SEGMENT || item.SEGMENT.toLowerCase().indexOf(filter.SEGMENT.toLowerCase()) > -1)
                        && (!filter.PRODUCT_FAMILY || item.PRODUCT_FAMILY.toLowerCase().indexOf(filter.PRODUCT_FAMILY.toLowerCase()) > -1)
                        && (!filter.PRODUCT || item.PRODUCT.toLowerCase().indexOf(filter.PRODUCT.toLowerCase()) > -1)
                        && (!filter.PRICE || item.PRICE.replace(/\D/gi, "").indexOf(filter.PRICE) > -1)
                        && (!filter.UPDATER_ID || item.UPDATER_ID.toLowerCase().indexOf(filter.UPDATER_ID.toLowerCase()) > -1)
                        && (!filter.UPDATE_DATE || getDateStr(item.UPDATE_DATE).split(' ')[0].indexOf(filter.UPDATE_DATE) > -1)
                });
            }
        },
        rowClick: function (args) {
            var item = args.item;
            state.selected_row = item;
            
            $('#txtManufacture').val(item.MANUFACTURE);
            $('#txtSegment').val(item.SEGMENT);
            $('#txtProductFamily').val(item.PRODUCT_FAMILY);
            $('#txtProduct').val(item.PRODUCT);
            $('#txtPrice').val(item.PRICE);
            //$('#txtID').val(item.UPDATER_ID);
            
            var row = $(args.event.target).closest("tr");
            $('.jsgrid-clicked-row').removeClass('jsgrid-clicked-row');
            row.addClass('jsgrid-clicked-row');
        },
        fields: [
            { name: "ID", title: "ID", width: 20 },
            { name: "MANUFACTURE", title: "MANUFACTURE", type: "text", width: 70 },
            { name: "SEGMENT", title: "SEGMENT", type: "text", width: 70 },
            { name: "PRODUCT_FAMILY", title: "PRODUCT FAMILY", type: "text", width: 70 },
            { name: "PRODUCT", title: "PRODUCT", type: "text", width: 70 },
            { name: "PRICE", title: "PRICE", type: "text", width: 70 },
            { name: "UPDATER_ID", title: "UPDATER ID", type: "text", width: 70 },
            {
                name: "UPDATE_DATE", title: "UPDATE DATE", type: "text", itemTemplate: function (value, item) {
                    return "<span>" + getDateStr(value).split(' ')[0] + "</span>";
                }
            },
        ]
    });
}

function GridData(callback) {
    var selectUrl = RADIOLOGY_SERVICE_URL + "/SelectMasterMarketShare";
    var datajson = {
        id: "",
    };
    $.ajax({
        url: selectUrl,
        type: "POST",
        data: JSON.stringify(datajson),
        dataType: "json",
        asyn: false,
        contentType: "application/json; charset=utf-8",
        success: function (returnData) {
            for (i in returnData) {
                returnData[i].PRICE = numberWithCommas(returnData[i].PRICE);
            }
            callback(returnData);
        }
    });
}
//3자리마다 콤마 삽입 함수
function numberWithCommas(x) {
    if (!x) return 0;
    var val = x;
    if (typeof x == 'string') val = x.replace(/\D/gi, "");
    var num = parseInt(val);
    return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}
//날짜 변환 함수
function getDateStr(value) {
    return new Date(parseInt(value.split('+')[0].replace(/\D/g, '')) + (9 * 60 * 60 * 1000)).toISOString().replace('T', ' ').split('.')[0];
}