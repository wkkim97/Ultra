$(function () {
    $("#divInputCommentApproverList").jsGrid({
        width: "100%",
        height: "200",

        autoload: true,
        paging: false,
        pageSize: 5,
        pageButtonCount: 5,
        pageIndex: 1,
        controller: {
            loadData: function (filter) {
                var processID = $('[id$=hddProcessID]').val();
                var d = $.Deferred();
                return $.ajax({
                    type: "GET",
                    url: EVENT_SERVICE_URL + "/SelectApproverList/" + processID,
                    dataType: "json",
                    cacache: false,
                }).done(function (response) {
                    d.resolve(response.value);
                }).fail(function (message) {
                    alert("input comment : "+ message);
                });
                return d.promise();
            }
        },
        fields: [
            {
                headerTemplate: function () {
                    return $("<input>").attr("type", "checkbox").attr("id", "chkHeader")
                            .on("change", function () {
                                $(this).is(":checked") ? selectAllItem(item) : unselectAllItem(item);
                            });
                },
                itemTemplate: function (_, item) {
                    return $("<input>").attr("type", "checkbox")
                            .prop("checked", $.inArray(item, selectedItems) > -1)
                            .on("change", function () {
                                $(this).is(":checked") ? selectItem(item) : unselectItem(item);
                            });
                },
                align: "center",
                width: 20
            },
            { name: "APPROVER", title: "Approver", type: "text", width: 70 },
            { name: "APPROVER_ORG_NAME", title: "Organization", type: "text" },
        ]
    });

    var selectedItems = [];

    var selectItem = function (item) {
        selectedItems.push(item);
        $("#hhdSelectApprover").data(selectedItems);
    };

    var unselectItem = function (item) {
        selectedItems = $.grep(selectedItems, function (i) {
            return i !== item;
        });
    };

    var selectAllItem = function () {
        selectedItems = $("#divInputCommentApproverList").jsGrid("option", "data");
        $("#divInputCommentApproverList .jsgrid-grid-body input:checkbox").prop("checked", true);
    };

    var unselectAllItem = function () {
        selectedItems = [];
        $("#divInputCommentApproverList .jsgrid-grid-body input:checkbox").prop("checked", false);
    };
});
