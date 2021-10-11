$(function () {

    var employeeSearcher;
    var clearJsGrid = false;

    $('.employee-searcher-modal').on('show.bs.modal', function (event) {
        initEmployeeSearcherModal();
        var button = $(event.relatedTarget);
        employeeSearcher = button;
    });

    function setModalSelectItem() {
        var selectItem = $('.employee-searcher-modal #txtSelectedEmployee');
        var txtSearcher = $(".employee-searcher input");
        employeeSearcher.parent().parent().find('input').val(selectItem.val());
        employeeSearcher.parent().parent().find('input').data('user-id', selectItem.data("user-id"));
        employeeSearcher.parent().parent().find('input').data('user-org', selectItem.data("user-org"));
    }

    $("#jsGridEmployeeSearcher").jsGrid({
        width: "100%",
        height: "250px",

        sorting: true,
        paging: false,
        autoload: false,
        rowDoubleClick: function (args) {
            var item = args.item;
            $('.employee-searcher-modal #txtSelectedEmployee').val(item.FULL_NAME + "(" + item.ORG_ACRONYM + ")");
            $('.employee-searcher-modal #txtSelectedEmployee').data("user-id", item.USER_ID);
            $('.employee-searcher-modal #txtSelectedEmployee').data("user-org", item.ORG_ACRONYM);
            setModalSelectItem();
            $('.employee-searcher-modal').modal("hide");
        },
        rowClick: function (args) {
            var item = args.item;
            $('.employee-searcher-modal #txtSelectedEmployee').val(item.FULL_NAME + "(" + item.ORG_ACRONYM + ")");
            $('.employee-searcher-modal #txtSelectedEmployee').data("user-id", item.USER_ID);
            $('.employee-searcher-modal #txtSelectedEmployee').data("user-org", item.ORG_ACRONYM);
            var row = $(args.event.target).closest("tr");

            if (this._clicked_row != null) {
                this._clicked_row.removeClass('jsgrid-clicked-row');
            }
            this._clicked_row = row;

            row.addClass('jsgrid-clicked-row');
        },
        controller: {
            loadData: function () {
                if (!clearJsGrid) {
                    var keyword = $(".employee-searcher-modal #txtEmployeeSearcher").val();
                    var selectUrl = COMMON_SERVICE_URL + "/SelectUserAutocompleteList/" + keyword;
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
                } else {
                    clearJsGrid = false;
                    return [];
                }
            }
        },

        fields: [
            { name: "USER_ID", title: "UserID", type: "text", width: 140 },
            { name: "FULL_NAME", title: "Name", type: "text" },
        ]
    });


    /* Search 버튼 클릭 */
    $(".employee-searcher-modal #btnEmaployeeSearcher").click(function () {
        searchEmployee();
    });

    /* Enter Key */
    $(".employee-searcher-modal #txtEmployeeSearcher").on("keypress", function (event) {
        if (event.keyCode == 13) {
            searchEmployee();
        }
    });

    $("#btnOk").click(function () {
        var employeeID = $('.employee-searcher-modal #txtSelectedEmployee').data("user-id");
        if (!employeeID || employeeID.length < 1) {
            fn_showInformation({
                title: "Confirm!",
                message: "Please select a user."
            })
            return;
        }

        setModalSelectItem();
        $('.employee-searcher-modal').modal("hide");
    });

    function initEmployeeSearcherModal() {
        $(".employee-searcher-modal #txtEmployeeSearcher").val("");

        $('.employee-searcher-modal #txtSelectedEmployee').val("");
        $('.employee-searcher-modal #txtSelectedEmployee').data("user-id", "");

        clearJsGrid = true;
        $("#jsGridEmployeeSearcher").jsGrid("loadData");
    }
});


function searchEmployee() {
    $("#jsGridEmployeeSearcher").jsGrid("loadData");
}