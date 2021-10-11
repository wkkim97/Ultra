$(function () {
    $("#divJsGridEmployee").jsGrid({
        width: "100%",
        height: "300",

        autoload: true,
        paging: true,
        pageSize: 5,
        pageButtonCount: 5,
        pageIndex: 1,
        rowClick: function (args) {
            $('#divNoneSelectedApprover').hide();
            var data = args.item;
            $('[id$=txtForwardApprovalSelectedUser]').val(data.FULL_NAME);
            $('[id$=hddForwardApprovalUserID]').val(data.USER_ID);
        },
        controller: {
            loadData: function (filter) {
                var d = $.Deferred();
                return $.ajax({
                    type: "GET",
                    url: COMMON_SERVICE_URL + "/selectapprovaluserlist/" + filter,
                    dataType: "json",
                    cacache: false,
                }).done(function (response) {
                    d.resolve(response.value);
                }).fail(function (message) {
                    alert("forwoard approval :"+message);
                });
                return d.promise();
            }
        },
        fields: [
            { name: "USER_ID", title: "User ID", type: "text", width: 100 },
            { name: "FULL_NAME", title: "Name", type: "text" },
        ]
    });
});

function loadUserInfo(keyword) {
    if (!keyword) keyword = '';
    $("#divJsGridEmployee").jsGrid("loadData", keyword);
}

function searchUserInfo() {
    var keyworkd = $('#txtSearch').val();
    loadUserInfo(keyworkd);
}

