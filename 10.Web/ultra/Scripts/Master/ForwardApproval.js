$(function () {

    $("#jsGridForwardApprovalEmployee").jsGrid({
        width: "100%",
        height: "250px",

        sorting: true,
        paging: false,
        autoload: false,
        rowDoubleClick: function (args) {
            var item = args.item;
            $('#txtForwardApprovalSelectedUser').val(item.FULL_NAME);
            $('#txtForwardApprovalSelectedUser').data("user-id", item.USER_ID);
        },
        controller: {
            loadData: function () {
                var keyword = $("#txtSearchForwardApproval").val();
                var selectUrl = COMMON_SERVICE_URL + "/SelectUserAutocompleteList/" + keyword;
                var d = $.Deferred();

                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    d.resolve(response);
                }).fail(function (jqXHR, textStatus, errorThrown) {
                });

                return d.promise();
            }
        },

        fields: [
            { name: "USER_ID", title: "UserID", type: "text", width: 140 },
            { name: "FULL_NAME", title: "Name", type: "text" },
        ]
    });

    $('#forward-approval').on('show.bs.modal', function (e) {
        $("#txtSearchForwardApproval").val("!@#$%"); //초기화위해
        $('#txtForwardApprovalSelectedUser').val("");
        $('#txtForwardApprovalSelectedUser').data("user-id", "");
        $("#jsGridForwardApprovalEmployee").jsGrid("loadData");
        $("#txtSearchForwardApproval").val("");
    });
    /* Search 버튼 클릭 */
    $("#btnSearchForwardApproval").click(function () {
        searchForwardApprovalEmployee();
    });

    /* Enter Key */
    $("#txtSearchForwardApproval").on("keypress", function (event) {
        if (event.keyCode == 13) {
            searchForwardApprovalEmployee();
        }
    });
});

function searchForwardApprovalEmployee() {
    $("#jsGridForwardApprovalEmployee").jsGrid("loadData");
}