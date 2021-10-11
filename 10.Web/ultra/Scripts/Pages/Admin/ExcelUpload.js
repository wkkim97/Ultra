$(function () {
    $("#jsGridUploadConcur").jsGrid({
        width: "100%",
        height: "500px",
        
        sorting: true,
        paging: false,
        autoload: false,
        paging: true,
        autoload: true,
        pageSize: GRID_LIST_COUNT,
        pageButtonCount: 5,
        pageNavigatorNextText: "...",
        pageNavigatorPrevText: "...",
        controller: {
            loadData: function () {
                var userID = $("input[id$=hddUserID]").val();
                var selectUrl = EVENT_SERVICE_URL + "/ReadEventPaymentUploadConcur/" + userID;
                var d = $.Deferred();

                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    $("#result_concur").append("  Error :" + response.length);
                  
                    d.resolve(response);
                }).fail(function (jqXHR, textStatus, errorThrown) {
                });

                return d.promise();
            }
        },
        //rowClass: function (item, itemIndex) {
        //    return item.ERROR_MESSAGE.indexOf("EVENT_KEY") >= 0 ? 'error-message' : '';
        //},
        fields: [
           /* {
                name: "ERROR_MESSAGE", title: "", type: "text", width: 20,
                itemTemplate: function (value, item) {
                    if ( item.ERROR_MESSAGE.length > 0) //직원이 아닌경우
                    {
                        return "<span class='alert-danger glyphicon glyphicon-exclamation-sign' aria-hidden='true' style='background-color:transparent'></span>";
                    } 
                }
            },*/
            { name: "ERROR_MESSAGE", title: "Error", type: "text", width: 100 },
            { name: "COMPANY_CODE", title: "Company Code", type: "text", width: 100 },
            {
                name: "REPORT_ID", title: "Report ID", type: "text", width: 170,
            },
            { name: "EMPLOYEE_ID", title: "Employee ID", type: "text", width: 100 },
            { name: "EMPLOYEE", title: "Employee", type: "text", width: 80 },
            {
                name: "TRANSACTION_ID", title: "Transaction ID", type: "text", width: 170,
                itemTemplate: function (value, item) {
                    if (item.ERROR_MESSAGE.indexOf("TRANSACTION_ID") >= 0) //ReportID가 존재하는 경우
                    {
                        return "<span style='color:#b53b4f'>" + value + "</span>";
                    } else {
                        return value;
                    }
                }

            },
            { name: "TRANSACTION_DATE", title: "Transaction DATE", type: "text", width: 100 },
            { name: "EXPENSE_TYPE", title: "Expense Type", type: "text", width: 170 },
            { name: "HCP_EXPENSE_TYPE", title: "HCP Expense Type", type: "text", width: 170 },
            { name: "MATERIAL_CODE", title: "Material Code", type: "text", width: 170 },
            { name: "LOCAL_CODE", title: "Local Code", type: "text" },
            { name: "HCP_CODE", title: "HCP Code", type: "text", width: 140 },
            {
                name: "EVENT_KEY", title: "Event Key", type: "text", width: 170,
                itemTemplate: function (value, item) {
                    if (item.ERROR_MESSAGE.indexOf("EVENT_KEY") >= 0) //직원이 아닌경우
                    {
                        return "<span style='color:#b53b4f'>" + item.EVENT_KEY + "</span>";
                    } else {
                        return item.EVENT_KEY;
                    }
                }
            },
            {
                name: "ATTENDEE_AMOUNT", title: "Attendee Amount", type: "text",
            },
            { name: "ATTENDEE_NAME", title: "Attendee Name", type: "text" },
            { name: "COMPANY", title: "Company", type: "text", width:150 },
            { name: "AFFILIATION", title: "Affiliation", type: "text" },
            { name: "ATTENDEE_TYPE", title: "Attendee Type", type: "text", width:190 },
            { name: "EXTERNAL_ID", title: "External ID", type: "text" },
        ]
    });

    $("#jsGridUploadYourDoces").jsGrid({
        width: "100%",
        height: "500px",

        sorting: true,
        paging: false,
        autoload: false,
        controller: {
            loadData: function () {
                var userID = $("input[id$=hddUserID]").val();
                var selectUrl = EVENT_SERVICE_URL + "/ReadEventPaymentUploadYourDoces/" + userID;
                var d = $.Deferred();

                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    $("#result_yourdoces").append("  Error :" + response.length);
                    d.resolve(response);
                }).fail(function (jqXHR, textStatus, errorThrown) {
                });

                return d.promise();
            }
        },
        //rowClass: function (item, itemIndex) {
        //    return item.ERROR_MESSAGE.indexOf("EVENT_KEY") >= 0 ? 'error-message' : '';
        //},
        fields: [
                    {
                        name: "ERROR_MESSAGE", title: "", type: "text", width: 20,
                        itemTemplate: function (value, item) {
                            if (item.ERROR_MESSAGE.length > 0) //직원이 아닌경우
                            {
                                return "<span class='alert-danger glyphicon glyphicon-exclamation-sign' aria-hidden='true' style='background-color:transparent'></span>";
                            }
                        }
                    },
                    { name: "ERROR_MESSAGE", title: "ERROR MESSAGE", width: 200, type: "text" },
                     {
                         name: "REFERENCE_KEY_1", title: "Reference KEY 1", width: 200, type: "text",
                         itemTemplate: function (value, item) {
                             if (item.ERROR_MESSAGE.indexOf("EVENT_KEY") >= 0) //ReportID가 존재하는 경우
                             {
                                 return "<span style='color:#b53b4f'>" + value + "</span>";
                             } else {
                                 return value;
                             }
                         }
                     },
                   
                    {
                        name: "REFERENCE_KEY_3", title: "Reference KEY 3", width: 200, type: "text",
                        itemTemplate: function (value, item) {
                            if (item.ERROR_MESSAGE.indexOf("HCP_CODE") >= 0) //ReportID가 존재하는 경우
                            {
                                return "<span style='color:#b53b4f'>" + value + "</span>";
                            } else {
                                return value;
                            }
                        }
                    },
                    {
                        name: "HCO_CODE", title: "HCO_CODE", width: 200, type: "text",
                        itemTemplate: function (value, item) {
                            if (item.ERROR_MESSAGE.indexOf("HCO_CODE") >= 0) //ReportID가 존재하는 경우
                            {
                                return "<span style='color:#b53b4f'>" + value + "</span>";
                            } else {
                                return value;
                            }
                        }
                    },
                   
                    { name: "NAME_1", title: "Name", type: "text" },
                     { name: "REFERENCE_KEY_2", title: "Reference KEY 2", type: "text" },
                    {
                        name: "DOCUMENT_NUMBER", title: "Document Number", type: "text",
                        itemTemplate: function (value, item) {
                            if (item.ERROR_MESSAGE.indexOf("DOCUMENT_NUMBER") >= 0) //ReportID가 존재하는 경우
                            {
                                return "<span style='color:#b53b4f'>" + value + "</span>";
                            } else {
                                return value;
                            }
                        }
                    },
                     { name: "ACCOUNT", title: "Account", type: "text" },
                    { name: "DOCUMENT_TYPE"             , title: "Document Type", type: "text" },
                    { name: "PAYMENT_BLOCK"             , title: "Payment Block", type: "text" },
                    { name: "DOCUMENT_HEADER_TEXT"      , title: "Document Header Text", type: "text" },
                    { name: "DOCUMENT_DATE"             , title: "Document Date", type: "date" },
                    { name: "ENTRY_DATE"                , title: "Entry Date", type: "date" },
                    { name: "POSTING_DATE"              , title: "Posting Date", type: "date" },
                    { name: "NET_DUE_DATE"              , title: "Net DUE Date", type: "date" },
                    { name: "AMOUNT_IN_DOC_CURR"        , title: "Amount IN DOC CURR", type: "text" },
                    { name: "DOCUMENT_CURRENCY"         , title: "Document CURRENCY", type: "text" },
                    { name: "AMOUNT_IN_LOCAL_CURRENCY"  , title: "Amount IN LOCAL CURRENCY", type: "text" },
                    { name: "LOCAL_CURRENCY"            , title: "Local CURRENCY", type: "text" },
                    { name: "TEXT"                      , title: "Text", type: "text" },
                    { name: "USER_NAME"                 , title: "User NAME", type: "text" },
                    { name: "CLEARING_DATE"             , title: "Clearing Date", type: "date" },
                    { name: "CLEARING_DOCUMENT"         , title: "CLEARING DOCUMENT", type: "text" },
                   
                    
                    { name: "COMMENTS"                  , title: "COMMENTS", type: "text" },
                    { name: "STATUS"                    , title: "STATUS", type: "text" },
                    
                    { name: "IS_DELETED"                , title: "IS Deleted", type: "text" },
                    { name: "CREATOR_ID"                , title: "CREATOR ID", type: "text" },
                    { name: "CREATE_DATE"               , title: "Create Date", type: "date" },
                    { name: "UPDATER_ID"                , title: "UPDateR ID", type: "text" },
                    { name: "UPDATE_DATE"               , title: "UPDate Date", type: "date" },
        ]
    });

    $("#btnSaveUploadConcur").click(function () {

        var userID = $("input[id$=hddUserID]").val();
        $.ajax({
            url: EVENT_SERVICE_URL + "/InsertEventModulePaymentFromConcur/" + userID,
            type: "GET",
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $("#jsGridUploadConcur").jsGrid("loadData");
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });

    });

    $("#btnSaveUploadYourDoces").click(function () {

        var userID = $("input[id$=hddUserID]").val();
        $.ajax({
            url: EVENT_SERVICE_URL + "/InsertEventModulePaymentFromYourDoces/" + userID,
            type: "GET",
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $("#jsGridUploadYourDoces").jsGrid("loadData");
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });

    });
    
})

function fn_ReadEventPaymentUploadConcur(filePath) {
    var userID = $("input[id$=hddUserID]").val();
    var param = {
        userID: userID,
        filePath: filePath,
    }
    $.ajax({
        url: EVENT_SERVICE_URL + "/InsertEventPaymentUploadConcur",
        type: "POST",
        data: JSON.stringify(param),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#result_concur").append("Excel total row :" + data);
            $("#jsGridUploadConcur").jsGrid("loadData");
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}

function fn_ReadEventPaymentUploadYourDoces(filePath) {
    var userID = $("input[id$=hddUserID]").val();
    var param = {
        userID: userID,
        filePath: filePath,
    }
    $.ajax({
        url: EVENT_SERVICE_URL + "/InsertEventPaymentUploadYourDoces",
        type: "POST",
        data: JSON.stringify(param),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#result_yourdoces").append("Excel total row :" + data);
            $("#jsGridUploadYourDoces").jsGrid("loadData");
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}