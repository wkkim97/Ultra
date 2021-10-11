$(function () {
    $("#jsGridDelegation").jsGrid({
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
                
                var selectUrl = EVENT_SERVICE_URL + "/SelectDelegation/" + $('input[id$=hddProcessID]').val();
                //var selectUrl = EVENT_SERVICE_URL + "/SelectDelegation/P000002582";
                //alert(selectUrl);
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
            {
                headerTemplate: function () {
                    return $("<input>").attr("type", "checkbox")
                            .on("change", function () {
                                $(this).is(":checked") ? selectAllItem() : unselectAllItem();
                            });
                },
                itemTemplate: function (_, item) {
                    return $("<input>").attr("type", "checkbox")
                            .prop("checked", $.inArray(item.USER_ID, selectedItems) > -1)
                            .on("change", function () {
                                $(this).is(":checked") ? selectItem(item) : unselectItem(item);
                            });
                },
                sorting: false,
                align: "center",
                width: 50
            },
            { name: "USER_NAME", title: "Name", type: "text", width: 80 },
            { name: "ORGANIZATION", title: "Org.", type: "text" },
            { name: "USER_ID", title: "UserId", type: "text", width: 50 },
            { name: "ROLE", title: "Role", type: "text", width: 80 },
            { name: "CREATE_DATE", title: "Created Date", type: "text", width: 80 },
        ]
    });

    var selectedItems = [];

    var selectAllItem = function () {
        selectedItems = [];
        $("#jsGridDelegation tbody input:checkbox").each(function () {
            selectedItems.push($(this).closest('tr').data('JSGridItem').USER_ID);
            $(this).prop('checked', true);
        });
    }

    var unselectAllItem = function () {
        selectedItems = [];
        $("#jsGridDelegation tbody input:checkbox").each(function () {
            $(this).prop('checked', false);
        });
    }

    var selectItem = function (item) {
        selectedItems.push(item.USER_ID);
    };

    var unselectItem = function (item) {
        selectedItems = $.grep(selectedItems, function (i) {
            return i !== item.USER_ID;
        });
    };

    $("#divEtcDelegation").on("keypress", "input[type=text]", function (event) {
        if (event.keyCode == 13) {
            searchDelegationEmployee();
        }
    });


    $("#btnSearchDelegationEmployee").click(function () {
        searchDelegationEmployee();
    });

    $("#btnAddDelegation").click(function () {
        if (!checkSavedStatus()) {
            fn_showWarning({
                title: "Confirm",
                message: "Please save the event."
            }).done(function () {
                $('#tabEventMaster a:first').tab('show');
            });
        } else {
            var processID = $('input[id$=hddProcessID]').val();
            var userID = $('input[id$=hddUserID]').val();
            var role = $("#divRdoDelegationRole input[type='radio']:checked").val();
            var delegators = [];
            $('#tblEE_Employee').find('input[type="checkbox"]:checked').each(function () {
                var objEmployee = $(this).closest('.tr-employee').data('employee');
                var dele = {
                    PROCESS_ID: processID,
                    USER_ID: objEmployee.USER_ID,
                    ROLE: role,
                    IS_DELETED: 'N',
                    CREATOR_ID: userID,
                    UPDATER_ID: userID
                };
                delegators.push(dele);
            });

            if (delegators.length > 0) callMergeDelegationAjax(delegators);
        }
    });

    $("#btnDeleteDelegation").click(function () {
        if (selectedItems.length < 1) {
            showNotSelected("Please select an user.");
            return;
        }

        fn_confirm()
        .done(function (result) {
            if (result) {
                deleteDelegation(selectedItems);
                selectedItems = [];
            }
        });

    });
});

function deleteDelegation(userIDs) {
    var processID = $('input[id$=hddProcessID]').val();
    var userID = $('input[id$=hddUserID]').val();

    var items = {
        processID: processID,
        userIDs: userIDs,
        updaterID: userID
    }

    $.ajax({
        url: EVENT_SERVICE_URL + "/DeleteDelegation",
        type: "POST",
        data: JSON.stringify(items),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function () {
            $("#jsGridDelegation").jsGrid("loadData"); //Participant 재조회
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}


function callMergeDelegationAjax(delegation) {
    $.ajax({
        url: EVENT_SERVICE_URL + "/MergeDelegation",
        type: "POST",
        data: JSON.stringify(delegation),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#jsGridDelegation").jsGrid("loadData"); //Participant 재조회
            clearSearchDelegationUser();
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}

function clearSearchDelegationUser() {
    $("#tblEE_Employee tbody").empty();
}

function searchDelegationEmployee() {
    var processID = $('input[id$=hddProcessID]').val();
    var keyword = $("#divEtcDelegation #lb_keyword").val();

    if (keyword.length < 1) {
        fn_showWarning({
            title: "input",
            message: "Please enter the conditions."
        });
        return;
    }

    $.ajax({
        url: EVENT_SERVICE_URL + "/SelectDelegationSearchUser/" + processID + "/" + keyword,
        type: "GET",
        dataType: "json",
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            displayDelegationEmployeeResult(data);
            if (data.length > 0)
                $("#divEtcDelegation #lb_keyword").val("");
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}

function displayDelegationEmployeeResult(employees) {
    $("#tblEE_Employee tbody").empty();
    var cnt = employees.length;
    for (var i = 0; i < cnt; i++) {
        var employee = employees[i];
        var row = "<tr class='tr-employee' data-employee='" + JSON.stringify(employee) + "'><td class='text-left'><label class='item-name'><span class='fix'>";
        if(employee.IS_EXIST == "Y")
            row += "<input type='checkbox' disabled>";
        else
            row += "<input type='checkbox'>";
        row += "</span><strong>" + employee.USER_NAME + "</strong><br>";
        row += "(" + employee.USER_ID + ")</label></td><td class='text-left'>" + employee.ORGANIZATION + "</td></tr>";
        $("#tblEE_Employee tbody").append(row);
    }
}
