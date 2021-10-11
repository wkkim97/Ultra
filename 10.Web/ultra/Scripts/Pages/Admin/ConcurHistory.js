$(function () {
    $.ajax({
        url: COMMON_SERVICE_URL + "/SelectCommonCode/S009",
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            for (var i = 0 ; i < data.length; i++) {
                //selCategoryList
                $("#selCategoryList").append($('<option>', {
                    value: data[i].SUB_CODE,
                    text: data[i].CODE_NAME
                }));
            }
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });

    $("#jsGridConcurList").jsGrid({
        width: "100%",
        height: "500px",
        sorting: true,
        paging: false,
        autoload: false,
        controller: {
            loadData: function () {
                var selectUrl = EVENT_SERVICE_URL + "/SelectPaymentConcurList/" + $("#searchText").val();
                var d = $.Deferred();

                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    d.resolve(response);
                }).fail(function (jqXHR, textStatus, errorThrown) {
                });

                return d.promise();
            }
        },
        rowClick: function (args) {
            var item = args.item;
            $('#hhdProcessID').val(item.PROCESS_ID);
            $('#lblEventKey').text(item.EVENT_KEY);
            $('#lblSubject').text(item.SUBJECT);
            $('#lblStartDate').text(item.START_DATE);
            $('#lblConcurID').text(item.CONCUR_TRANSACTION_ID);
            $('#selCategoryList').val(item.CATEGORY_CODE);
            var row = $(args.event.target).closest("tr");

            if (this._clicked_row != null) {
                this._clicked_row.removeClass('jsgrid-clicked-row');
            }
            this._clicked_row = row;

            row.addClass('jsgrid-clicked-row');
        },
        rowDoubleClick: function (args) {
            //var item = args.item;
            $("#lblSelConcurId").text($('#lblConcurID').text());
            $("#jsGridConcurHistory").jsGrid("loadData");
            $('.concur-history-modal').modal("show");
        },
        fields: [
            { name: "EVENT_KEY", title: "Event Key", type: "text", width: 150 },
            { name: "PROCESS_ID", title: "Process ID", type: "text", css: "hide", width: 0 },
            { name: "PROCESS_STATUS", title: "Process Status", type: "text", css: "hide", width: 0 },
            { name: "SUBJECT", title: "Subject", type: "text" },
            { name: "START_DATE", title: "Start Date", type: "text", width: 100 },
            { name: "CONCUR_TRANSACTION_ID", title: "Concur ID", type: "text", width: 150 },
            { name: "CATEGORY_CODE", title: "Category Code", type: "text", css: "hide", width: 0 },
            { name: "CODE_NAME", title: "Category Name", type: "text", width: 100 },
        ]
    });

    $("#jsGridConcurHistory").jsGrid({
        width: "100%",
        height: "500px",
        sorting: true,
        paging: false,
        autoload: false,
        controller: {
            loadData: function () {
                var processId = $('#hhdProcessID').val();
                var concurId = $('#lblConcurID').text();
                var selectUrl = EVENT_SERVICE_URL + "/SelectConcurHistory/" + processId + "/" + concurId;
                var d = $.Deferred();

                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    d.resolve(response);
                }).fail(function (jqXHR, textStatus, errorThrown) {
                });

                return d.promise();
            }
        },
        fields: [
            { name: "CATEGORY_CODE", title: "Category Code", type: "text", css: "hide", width: 0 },
            { name: "CODE_NAME", title: "Category Name", type: "text", width: 100 },
            { name: "UPDATER", title: "Updater", type: "text", css: "hide", width: 0 },
            { name: "UPDATER_NAME", title: "Updater", type: "text", width: 100 },
            { name: "UPDATE_DATE", title: "Update Date", type: "text", width: 100 },
        ]
    });

    function clearNewConcur() {
        $("#hhdProcessID").val("");
        $("#lblSubject").text("");
        $("#lblStartDate").text("");
        $("#lblConcurID").text("");
        $("#selCategoryList").val("");
    }

    $("#btnSave").click(function () {
        var processId = $('#hhdProcessID').val();
        var concurId = $('#lblConcurID').text();
        var CategoryCode = $('#selCategoryList').val();
        var userID = $("input[id$=hddUserID]").val();

        if (processId == "" || concurId == "") return;

        //저장
        var concur = {
            PROCESS_ID: processId,
            CONCUR_TRANSACTION_ID: concurId,
            CATEGORY_CODE: CategoryCode,
            UPDATER: userID
        }

        $.ajax({
            url: EVENT_SERVICE_URL + "/UpdatePaymentConcur",
            type: "POST",
            data: JSON.stringify(concur),
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                alert("수정되었습니다.");
                clearNewConcur();

                $("#jsGridConcurList").jsGrid("loadData");
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });

    });


    /* Enter key */
    $("#searchText").on("keypress", function (event) {
        if (event.keyCode == 13) {
            event.preventDefault();
            var keyword = $("#searchText").val();

            $("#jsGridConcurList").jsGrid("loadData");
        }
    });


    $("#jsGridConcurList").jsGrid("loadData");
})
