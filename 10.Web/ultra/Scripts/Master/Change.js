$(function () {
    $("#jsGridChange").jsGrid({
        width: "100%",
        height: "auto",

        sorting: true,
        paging: false,
        autoload: false,
        rowDoubleClick: function (args) {
            var item = args.item;
        },
        controller: {
            loadData: function () {

                //var selectUrl = EVENT_SERVICE_URL + "/SelectChange/" + $('input[id$=hddProcessID]').val();
                var selectUrl = EVENT_SERVICE_URL + "/SelectChange/P000007998";
                
                console.log(selectUrl);
                var d = $.Deferred();

                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    console.log(response);
                    d.resolve(response);
                }).fail(function (jqXHR, textStatus, errorThrown) {
                });

                return d.promise();
            }
        },

        fields: [
            { name: "CATEGORY", title: "Category", type: "text", width: 100 },
            { name: "ADJUSTMENT_AREA", title: "Field", type: "text" },
            { name: "OLD_VALUE", title: "Old", type: "text", width: 100 },
            { name: "NEW_VALUE", title: "New", type: "text", width: 100 },
            { name: "REASON", title: "Reason", type: "text", width: 100 },
            { name: "COMMENT", title: "Comment", type: "text", width: 100 },
            { name: "EVIDENCE", title: "Evidence", type: "text", width: 100 },
            { name: "UPDATER_ID", title: "Updated By", type: "text", width: 80 },
            { name: "UPDATE_DATE", title: "Updated", type: "text", width: 80 },
        ]
    });

});