$(function () {
    //공통코드 조회
    $.ajax({
        url: COMMON_SERVICE_URL + "/SelectCommonCodeAll",
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            setCommonCode(data);
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });

    //이벤트 목록 조회
    $.ajax({
        url: COMMON_SERVICE_URL + "/SelectConfigurationList",
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            setEventList(data);
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });

    //이벤트 선택시
    $("#selEventList").change(function (e) {
        e.preventDefault();
        var selectedValue = $("#selEventList option:selected").val();
        if (selectedValue != "None") {
            $("input[id$=hddEventID]").val(selectedValue);
            getEvent(selectedValue);
        }
    });

    $("#chkAddCostPlan").change(function () {
        $(".dropup button").prop("disabled", !this.checked);
    });

    $("#chkAddParticipants").change(function () {
        $("#chkCheckCountRule").prop("checked", false);
        $("#chkCheckCountRule").prop("disabled", !this.checked);
    });

    $("#chkAddAgenda").change(function () {
        $("#optAgendaRoleType").prop("disabled", !this.checked);
    });

    $("input:radio[name='optEventCompleteComment']").on('change', function () {
        $("#selCommentCategory").prop("disabled", this.value == "Y" ? false : true);
    });

    $("#btnNew").click(function () {
        $("input[id$=hddEventID]").val("");
        initEventConfiguration();
    });

    $("#btnSaveEventConfiguration").click(function () {

        var eventID = $("input[id$=hddEventID]").val();
        var tableName = $("#txtTableName").val();
        var eventName = $("#txtEventName").val();
        var dataOwner = $("#divDataOwner .employee-id").data("user-id");
        var prefixEventKey = $("#txtEventPrefix").val();
        var webPage = $("#txtPageName").val();
        var retenTionCode = $("#optRetentionPeriod").val();
        var classificationCode = $("#optInfoClassification").val();
        var afterServiceName = $("#txtServiceName").val();
        var evetnDesc = $("#txtEventDesc").val();
        var optForward = $("input:radio[name='optForward']:checked").val();
        var optAddReviewer = $("input:radio[name='optAddReviewer']:checked").val();
        var optAddReviewerDesc = $("#txtReviewerDesc").val();
        var optShowEventList = $("input:radio[name='optShowEventList']:checked").val();
        var optAddCostPlan = $('#chkAddCostPlan').prop("checked") ? "Y" : "N"; //CostPlan
        var optAddParticipants = $('#chkAddParticipants').prop("checked") ? "Y" : "N"; //Participant
        var optCheckCountRule = $("#chkCheckCountRule").prop("checked") ? "Y" : "N"; //4회체크
        if (optAddParticipants == "N") optCheckCountRule = "N";
        var optAddAgenda = $('#chkAddAgenda').prop("checked") ? "Y" : "N"; //Agenda
        var AddAgendaRoleType = $("#optAgendaRoleType").val(); //Agenda Role 타입
        var optAddPayment = $('#chkAddPayment').prop("checked") ? "Y" : "N"; //Payment
        var optSkipApproval = $("input:radio[name='optSkipApproval']:checked").val(); //Skip Approval
        var optOnlyApproval = $("input:radio[name='optOnlyApproval']:checked").val(); //Only Approval
        var approvalTypeCode = $("input:radio[name='optDefaultApproval']:checked").val(); //Approval 타입
        var approvalLevel = $("#selApprovalLevel").val();
        var approvalJobTitleCode = $("#selJobTitle").val();
        var optEventCompleteComment = $("input:radio[name='optEventCompleteComment']:checked").val();
        var EventCompleteCommentCategory = $("#selCommentCategory").val();
        var optEventCompleteAttach = $("input:radio[name='optEventCompletAttach']:checked").val();
        var userID = $("input[id$=hddUserID]").val();

        var msgFillOut = "";
        if (tableName.trim().length < 1) {
            if (msgFillOut.length > 0) msgFillOut += ", ";
            msgFillOut = "TableName";
        }

        if (eventName.trim().length < 1) {
            if (msgFillOut.length > 0) msgFillOut += ", ";
            msgFillOut = "Event Name";
        }

        if (dataOwner < 1) {
            if (msgFillOut.length > 0) msgFillOut += ", ";
            msgFillOut += "Data Owner";
        }

        if (prefixEventKey < 1) {
            if (msgFillOut.length > 0) msgFillOut += ", ";
            msgFillOut += "Prefix of Event";
        }

        if (optAddAgenda == "Y" && AddAgendaRoleType == "Select") {
            if (msgFillOut.length > 0) msgFillOut += ", ";
            msgFillOut += "Agenda Role";
        } else if (optAddAgenda == "N") {
            AddAgendaRoleType = "";
        }

        var categories = [];
        if (optAddCostPlan == "Y") {

            $.map($("#selCostCategory input:checked"), function (item, i) {
                categories.push({ EVENT_ID: eventID, CATEGORY_CODE: $(item).data("category-code"), CREATOR_ID: userID });
            });

            if (categories.length < 1) {
                if (msgFillOut.length > 0) msgFillOut += ", ";
                msgFillOut += "Cost Category";
            }
        }


        if (msgFillOut.length > 0) {
            fn_showInformation({
                title: "Please fill out below fields.",
                message: msgFillOut
            })
            return;
        }

        if (approvalTypeCode == "0001") {
            approvalJobTitleCode = "";
        } else if (approvalTypeCode == "0002") {
            approvalLevel = "1";
        }
        var config = {
            EVENT_ID: eventID,
            TABLE_NAME: tableName,
            EVENT_NAME: eventName,
            DATA_OWNER: dataOwner,
            PREFIX_EVENT_KEY: prefixEventKey,
            WEB_PAGE_NAME: webPage,
            READERS_GROUP_CODE: "",
            RETENTION_PERIOD_CODE: retenTionCode,
            CLASSIFICATION_INFO_CODE: classificationCode,
            AFTER_TREATMENT_SERVICE: afterServiceName,
            EVENT_DESC: evetnDesc,
            OPT_FORWARD: optForward,
            OPT_ADD_REVIEWER: optAddReviewer,
            OPT_ADD_REVIEWER_DESC: optAddReviewerDesc,
            OPT_SHOW_EVENT_LIST: optShowEventList,
            OPT_ADD_COST_PLAN: optAddCostPlan,
            OPT_ADD_PARTICIPANTS: optAddParticipants,
            OPT_ADD_AGENDA: optAddAgenda,
            OPT_ADD_PAYMENT: optAddPayment,
            OPT_SKIP_APPROVAL: optSkipApproval,
            OPT_ONLY_APPROVAL: optOnlyApproval,
            OPT_COUNT_RULE: optCheckCountRule,
            OPT_REPORT_MOHW: "",
            OPT_REPORT_KRPIA: "",
            AGENDA_ROLE_TYPE: AddAgendaRoleType,
            APPROVAL_TYPE_CODE: approvalTypeCode,
            APPROVAL_LEVEL: approvalLevel,
            APPROVAL_JOB_TITLE_CODE: approvalJobTitleCode,
            OPT_EVENT_COMPLETE_COMMENT: optEventCompleteComment,
            OPT_EVENT_COMPLETE_COMMENT_CATEGORY: EventCompleteCommentCategory,
            OPT_EVENT_COMPLETE_ATTACH: optEventCompleteAttach,
            MY_EVENT_KEY: "",
            MOHW_REPORT_TYPE: "",
            IS_DELETED: "N",
            CREATOR_ID: userID,
            UPDATER_ID: userID
        }

        var companies = [];
        $.map($("#divCompany input:checked"), function (item, i) {
            companies.push({ EVENT_ID: eventID, COMPANY_CODE: $(item).data("company-code"), CREATOR_ID: userID });
        });

        var param = {
            config: config,
            companies: companies,
            categories: categories
        }

        $.ajax({
            url: COMMON_SERVICE_URL + "/MergeEventConfiguration",
            type: "POST",
            data: JSON.stringify(param),
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                fn_showInformation({
                    message: "저장 되었습니다."
                }).done(function () {
                    initEventConfiguration();
                });
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });

    });

    $("#selAppreoverType").change(function () {
        initCondition();
    });

    $("#btnNewCondition").click(function () {
        initCondition();
    });

    $("#selCondition").change(function () {
        var condition = $("#selCondition").val();
        if (condition == "IsNull" || condition == "IsNotNull") {
            $("#txtConditionValue").prop("disabled", true);
            $("#txtConditionValue").val("");
        } else {
            $("#txtConditionValue").prop("disabled", false);
        }
    });

    /* 결재 라인 설정 부분 */
    $("#btnAddCondition").click(function () {
        var eventID = $("input[id$=hddEventID]").val();
        if (eventID.length < 1) {
            fn_showWarning({ message: "이벤트를 조회 바랍니다." });
            return;
        }

        var condition = $("#selCondition").val();
        if (condition != "IsNull" && condition != "IsNotNull") {
            var conditionValue = $("#txtConditionValue").val();
            if (conditionValue.length < 1) {
                fn_showWarning({ message: "Value를 입력 바랍니다." });
                return;
            }
        }

        var items = $("#divCondition").jsGrid().data("JSGrid").data;
        var seq = 0;
        $.map(items, function (item, i) {
            if (item.CONDITION_SEQ > seq) seq = item.CONDITION_SEQ;
        });
        seq += 1;

        var fieldName = $("#selTableColumn").val().split(':')[0];
        var fieldType = $("#selTableColumn").val().split(':')[1];

        $("#divCondition").jsGrid("insertItem", {
            CONDITION_SEQ: seq,
            APPROVAL_LOCATION: $("#selAppreoverType").val(),
            IS_MANDATORY: 'N',
            FIELD_NAME: fieldName,
            FIELD_TYPE: fieldType,
            CONDITION: $("#selCondition").val(),
            VALUE: $("#txtConditionValue").val(),
            OPTION: $("#selAndOr").val(),
        }).done(function () {
            $("#divCondition").jsGrid("refresh");
        });
    })

    $("#divCondition").jsGrid({
        width: "100%",
        height: "auto",

        sorting: false,
        paging: false,
        autoload: false,
        controller: {
            loadData: function () {
                var eventID = $("input[id$=hddEventID]").val();
                var approverType = $("#selAppreoverType").val();
                var conditionIDX = $("input[id$=hddConditionIDX]").val();
                var selectUrl = "";
                if (approverType == "R")
                    selectUrl = COMMON_SERVICE_URL + "/SelectEventRecipantCondition/" + eventID + "/" + conditionIDX;
                else if (approverType == "V")
                    selectUrl = COMMON_SERVICE_URL + "/SelectEventReviewerCondition/" + eventID + "/" + conditionIDX;
                else
                    selectUrl = COMMON_SERVICE_URL + "/SelectEventApproverCondition/" + eventID + "/" + approverType + "/" + conditionIDX;
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
                    d.reject(jqXHR);
                });

                return d.promise();
            }
        },
        fields: [
            { name: "CONDITION_SEQ", title: "Seq", type: "text" },
            { name: "FIELD_NAME", title: "Field", type: "text" },
            { name: "FIELD_TYPE", title: "Field", type: "text" },
            { name: "CONDITION", title: "Condition", type: "text" },
            { name: "VALUE", title: "Value", type: "text" },
            { name: "OPTION", title: "Option", type: "text", },
            { type: "control", modeSwitchButton: false, editButton: false }
        ]
    });

    /* Approver, Recipiant, Reviewer Grid */
    $("#divBeforeApprover").jsGrid({
        width: "100%",
        height: "auto",

        sorting: true,
        paging: false,
        autoload: false,
        rowDoubleClick: function (args) {
            var item = args.item;
            $("#selAppreoverType").val(item.APPROVER_LOCATION);
            $("input[id$=hddConditionIDX]").val(item.CONDITION_IDX);
            $("#divApprover .employee-id").val(item.APPROVER_NAME);
            $("#divApprover .employee-id").data("user-id", item.APPROVER_ID);
            $("#chkIsMandatory").prop("checked", item.IS_MANDATORY == "Y" ? true : false);

            $("#divCondition").jsGrid("loadData");
        },
        onItemDeleting: function (args) {
            var item = args.item;

            $.ajax({
                url: COMMON_SERVICE_URL + "/DeleteEventApprover/" + item.EVENT_ID + "/" + item.APPROVER_LOCATION + "/" + item.CONDITION_IDX,
                type: "GET",
                //dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function () {
                    $("#divBeforeApprover").jsGrid("loadData");
                },
                error: function (error) {
                    fn_showError({
                        message: error.responseText
                    });
                    args.cancel = true;
                },
            });
        },
        controller: {
            loadData: function () {
                var eventID = $("input[id$=hddEventID]").val();
                var selectUrl = COMMON_SERVICE_URL + "/SelectEventApprover/" + eventID + "/B";
                var d = $.Deferred();

                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    d.resolve(response);
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    d.reject(jqXHR);
                });

                return d.promise();
            }
        },

        fields: [
            { name: "CONDITION_IDX", title: "Seq", type: "text", width: 30 },
            { name: "APPROVER_ID", title: "ApproverId", type: "text", width: 40 },
            { name: "APPROVER_NAME", title: "Approver", type: "text", width: 50 },
            { name: "DISPLAY_CONDITION", title: "Display", type: "text" },
            { name: "SQL_CONDITION", title: "SQL", type: "text" },
            { type: "control", modeSwitchButton: false, editButton: false }
        ]
    });

    $("#divAfterApprover").jsGrid({
        width: "100%",
        height: "auto",

        sorting: true,
        paging: false,
        autoload: false,
        rowDoubleClick: function (args) {
            var item = args.item;
            $("#selAppreoverType").val(item.APPROVER_LOCATION);
            $("input[id$=hddConditionIDX]").val(item.CONDITION_IDX);
            $("#divApprover .employee-id").val(item.APPROVER_NAME);
            $("#divApprover .employee-id").data("user-id", item.APPROVER_ID);
            $("#chkIsMandatory").prop("checked", item.IS_MANDATORY == "Y" ? true : false);
            $("#divCondition").jsGrid("loadData");
        },
        onItemDeleting: function (args) {
            var item = args.item;

            $.ajax({
                url: COMMON_SERVICE_URL + "/DeleteEventApprover/" + item.EVENT_ID + "/" + item.APPROVER_LOCATION + "/" + item.CONDITION_IDX,
                type: "GET",
                //dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function () {
                    $("#divAfterApprover").jsGrid("loadData");
                },
                error: function (error) {
                    fn_showError({
                        message: error.responseText
                    });
                    args.cancel = true;
                },
            });
        },
        controller: {
            loadData: function () {
                var eventID = $("input[id$=hddEventID]").val();
                var selectUrl = COMMON_SERVICE_URL + "/SelectEventApprover/" + eventID + "/A";
                var d = $.Deferred();

                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    d.resolve(response);
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    d.reject(jqXHR);
                });

                return d.promise();
            }
        },

        fields: [
            { name: "CONDITION_IDX", title: "Seq", type: "text", width: 30 },
            { name: "APPROVER_ID", title: "ApproverId", type: "text", width: 40 },
            { name: "APPROVER_NAME", title: "Approver", type: "text", width: 50 },
            { name: "DISPLAY_CONDITION", title: "Display", type: "text" },
            { name: "SQL_CONDITION", title: "SQL", type: "text" },
            { type: "control", modeSwitchButton: false, editButton: false }
        ]
    });

    $("#divRecipient").jsGrid({
        width: "100%",
        height: "auto",

        sorting: true,
        paging: false,
        autoload: false,
        rowDoubleClick: function (args) {
            var item = args.item;
            $("#selAppreoverType").val("R");
            $("input[id$=hddConditionIDX]").val(item.CONDITION_IDX);
            $("#divApprover .employee-id").val(item.RECIPIENT_NAME);
            $("#divApprover .employee-id").data("user-id", item.RECIPIENT_ID);
            $("#chkIsMandatory").prop("checked", item.IS_MANDATORY == "Y" ? true : false);
            $("#divCondition").jsGrid("loadData");
        },
        onItemDeleting: function (args) {
            var item = args.item;

            $.ajax({
                url: COMMON_SERVICE_URL + "/DeleteEventRecipient/" + item.EVENT_ID + "/" + item.CONDITION_IDX,
                type: "GET",
                //dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function () {
                    $("#divRecipient").jsGrid("loadData");
                },
                error: function (error) {
                    fn_showError({
                        message: error.responseText
                    });
                    args.cancel = true;
                },
            });
        },
        controller: {
            loadData: function () {
                var eventID = $("input[id$=hddEventID]").val();
                var selectUrl = COMMON_SERVICE_URL + "/SelectEventRecipiant/" + eventID;
                var d = $.Deferred();

                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    d.resolve(response);
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    d.reject(jqXHR);
                });

                return d.promise();
            }
        },

        fields: [
            { name: "CONDITION_IDX", title: "Seq", type: "text", width: 30 },
            { name: "RECIPIENT_ID", title: "RecipientId", type: "text", width: 30 },
            { name: "RECIPIENT_NAME", title: "Recipient", type: "text", width: 40 },
            { name: "DISPLAY_CONDITION", title: "Display", type: "text" },
            { name: "SQL_CONDITION", title: "SQL", type: "text" },
            { type: "control", modeSwitchButton: false, editButton: false }
        ]
    });

    $("#divReviewer").jsGrid({
        width: "100%",
        height: "auto",

        sorting: true,
        paging: false,
        autoload: false,
        rowDoubleClick: function (args) {
            var item = args.item;
            $("#selAppreoverType").val("V");
            $("input[id$=hddConditionIDX]").val(item.CONDITION_IDX);
            $("#divApprover .employee-id").val(item.REVIEWER_NAME);
            $("#divApprover .employee-id").data("user-id", item.REVIEWER_ID);
            $("#chkIsMandatory").prop("checked", item.IS_MANDATORY == "Y" ? true : false);
            $("#divCondition").jsGrid("loadData");
        },
        onItemDeleting: function (args) {
            var item = args.item;

            $.ajax({
                url: COMMON_SERVICE_URL + "/DeleteEventReviewer/" + item.EVENT_ID + "/" + item.CONDITION_IDX,
                type: "GET",
                //dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function () {
                    $("#divReviewer").jsGrid("loadData");
                },
                error: function (error) {
                    fn_showError({
                        message: error.responseText
                    });
                    args.cancel = true;
                },
            });
        },
        controller: {
            loadData: function () {
                var eventID = $("input[id$=hddEventID]").val();
                var selectUrl = COMMON_SERVICE_URL + "/SelectEventReviewer/" + eventID;
                var d = $.Deferred();

                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    d.resolve(response);
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    d.reject(jqXHR);
                });

                return d.promise();
            }
        },

        fields: [
            { name: "CONDITION_IDX", title: "Seq", type: "text", width: 30 },
            { name: "REVIEWER_ID", title: "ReviewerId", type: "text", width: 30 },
            { name: "REVIEWER_NAME", title: "Reviewer", type: "text", width: 40 },
            { name: "DISPLAY_CONDITION", title: "Display", type: "text" },
            { name: "SQL_CONDITION", title: "SQL", type: "text" },
            { type: "control", modeSwitchButton: false, editButton: false }
        ]
    });

    $("#btnSaveCondition").click(function () {
        var eventID = $("input[id$=hddEventID]").val();
        if (eventID.length < 1) {
            fn_showWarning({ message: "이벤트를 조회 바랍니다." });
            return;
        }

        var approverID = $("#divApprover .employee-id").data("user-id");

        if (!approverID || approverID.length < 1) {
            fn_showWarning({ message: "사용자를 입력바랍니다." });
            return;
        }

        var approverType = $("#selAppreoverType").val();

        var isMandatory = $("#chkIsMandatory").prop("checked");
        var inputConditions = $("#divCondition").jsGrid().data("JSGrid").data;
        if (!isMandatory && inputConditions.length < 1) {
            fn_showWarning({ message: "조건을 입력바랍니다." });
            return;
        }

        if (approverType == "R") mergeEventRecipient(eventID);
        else if (approverType == "V") mergeEventReviewer(eventID);
        else mergeEventApprover(approverType, eventID);

    });
});

function mergeEventApprover(approverType, eventID) {
    var conditionIDX = $("input[id$=hddConditionIDX]").val();
    if (conditionIDX == "0") {
        //신규
        var idx = 0;
        var items;
        if (approverType == "B") {
            items = $("#divBeforeApprover").jsGrid().data("JSGrid").data;
        } else if (approverType == "A") {
            items = $("#divAfterApprover").jsGrid().data("JSGrid").data;
        }

        if (items && items.length > 0) {
            idx = items[0].MAX_CONDITION_IDX;
        }

        idx += 1;
        conditionIDX = idx;
    }

    var approverID = $("#divApprover .employee-id").data("user-id");
    var approverName = $("#divApprover .employee-id").val();
    var isMandatory = $("#chkIsMandatory").prop("checked");
    var userID = $("input[id$=hddUserID]").val();
    var approver;
    var conditions = [];
    if (isMandatory) {
        approver = {
            EVENT_ID: eventID,
            CONDITION_IDX: conditionIDX,
            IS_MANDATORY: "Y",
            APPROVER_LOCATION: approverType,
            APPROVER_ID: approverID,
            DISPLAY_CONDITION: approverName + "(Mandatory)",
            SQL_CONDITION: "CASE WHEN 1 = 1 THEN '" + approverID + "' END",
            CREATOR_ID: userID
        }
    } else {
        var inputConditions = $("#divCondition").jsGrid().data("JSGrid").data;
        var seq = 1;
        //DisplayCondition과 SqlCondition은 서비스에서 처리한다.
        approver = {
            EVENT_ID: eventID,
            CONDITION_IDX: conditionIDX,
            IS_MANDATORY: "N",
            APPROVER_LOCATION: approverType,
            APPROVER_ID: approverID,
            DISPLAY_CONDITION: "",
            SQL_CONDITION: "",
            CREATOR_ID: userID
        }
        $.map(inputConditions, function (item, i) {
            var condition = {
                EVENT_ID: eventID,
                CONDITION_IDX: conditionIDX,
                CONDITION_SEQ: seq,
                APPROVER_LOCATION: approverType,
                FIELD_NAME: item.FIELD_NAME,
                FIELD_TYPE: item.FIELD_TYPE,
                CONDITION: item.CONDITION,
                VALUE: item.VALUE,
                OPTION: item.OPTION,
                CREATOR_ID: userID,
            }
            conditions.push(condition);
            seq++;
        });
    }

    var param = {
        approver: approver,
        conditions: conditions,
    }

    $.ajax({
        url: COMMON_SERVICE_URL + "/MergeEventApprover",
        type: "POST",
        data: JSON.stringify(param),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function () {
            $("#divBeforeApprover").jsGrid("loadData");
            $("#divAfterApprover").jsGrid("loadData");
            initCondition();
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}

function mergeEventRecipient(eventID) {
    var conditionIDX = $("input[id$=hddConditionIDX]").val();
    if (conditionIDX == "0") {
        //신규
        var idx = 0;
        var items = $("#divRecipient").jsGrid().data("JSGrid").data;

        if (items && items.length > 0) {
            idx = items[0].MAX_CONDITION_IDX;
        }

        idx += 1;
        conditionIDX = idx;
    }

    var approverID = $("#divApprover .employee-id").data("user-id");
    var approverName = $("#divApprover .employee-id").val();
    var isMandatory = $("#chkIsMandatory").prop("checked");
    var userID = $("input[id$=hddUserID]").val();
    var recipient;
    var conditions = [];
    if (isMandatory) {
        recipient = {
            EVENT_ID: eventID,
            CONDITION_IDX: conditionIDX,
            IS_MANDATORY: "Y",
            RECIPIENT_ID: approverID,
            DISPLAY_CONDITION: approverName + "(Mandatory)",
            SQL_CONDITION: "CASE WHEN 1 = 1 THEN '" + approverID + "' END",
            CREATOR_ID: userID
        }
    } else {
        var inputConditions = $("#divCondition").jsGrid().data("JSGrid").data;
        var seq = 1;
        //DisplayCondition과 SqlCondition은 서비스에서 처리한다.
        recipient = {
            EVENT_ID: eventID,
            CONDITION_IDX: conditionIDX,
            IS_MANDATORY: "N",
            RECIPIENT_ID: approverID,
            DISPLAY_CONDITION: "",
            SQL_CONDITION: "",
            CREATOR_ID: userID
        }
        $.map(inputConditions, function (item, i) {
            var condition = {
                EVENT_ID: eventID,
                CONDITION_IDX: conditionIDX,
                CONDITION_SEQ: seq,
                FIELD_NAME: item.FIELD_NAME,
                FIELD_TYPE: item.FIELD_TYPE,
                CONDITION: item.CONDITION,
                VALUE: item.VALUE,
                OPTION: item.OPTION,
                CREATOR_ID: userID,
            }
            conditions.push(condition);
            seq++;
        });
    }

    var param = {
        recipient: recipient,
        conditions: conditions,
    }

    $.ajax({
        url: COMMON_SERVICE_URL + "/MergeEventRecipient",
        type: "POST",
        data: JSON.stringify(param),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function () {
            $("#divRecipient").jsGrid("loadData");
            initCondition();
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}

function mergeEventReviewer(eventID) {
    var conditionIDX = $("input[id$=hddConditionIDX]").val();
    if (conditionIDX == "0") {
        //신규
        var idx = 0;
        var items = $("#divReviewer").jsGrid().data("JSGrid").data;

        if (items && items.length > 0) {
            idx = items[0].MAX_CONDITION_IDX;
        }

        idx += 1;
        conditionIDX = idx;
    }

    var approverID = $("#divApprover .employee-id").data("user-id");
    var approverName = $("#divApprover .employee-id").val();
    var isMandatory = $("#chkIsMandatory").prop("checked");
    var userID = $("input[id$=hddUserID]").val();
    var reviewer;
    var conditions = [];
    if (isMandatory) {
        reviewer = {
            EVENT_ID: eventID,
            CONDITION_IDX: conditionIDX,
            IS_MANDATORY: "Y",
            REVIEWER_ID: approverID,
            DISPLAY_CONDITION: approverName + "(Mandatory)",
            SQL_CONDITION: "CASE WHEN 1 = 1 THEN '" + approverID + "' END",
            CREATOR_ID: userID
        }
    } else {
        var inputConditions = $("#divCondition").jsGrid().data("JSGrid").data;
        var seq = 1;
        //DisplayCondition과 SqlCondition은 서비스에서 처리한다.
        reviewer = {
            EVENT_ID: eventID,
            CONDITION_IDX: conditionIDX,
            IS_MANDATORY: "N",
            REVIEWER_ID: approverID,
            DISPLAY_CONDITION: "",
            SQL_CONDITION: "",
            CREATOR_ID: userID
        }
        $.map(inputConditions, function (item, i) {
            var condition = {
                EVENT_ID: eventID,
                CONDITION_IDX: conditionIDX,
                CONDITION_SEQ: seq,
                FIELD_NAME: item.FIELD_NAME,
                FIELD_TYPE: item.FIELD_TYPE,
                CONDITION: item.CONDITION,
                VALUE: item.VALUE,
                OPTION: item.OPTION,
                CREATOR_ID: userID,
            }
            conditions.push(condition);
            seq++;
        });
    }

    var param = {
        reviewer: reviewer,
        conditions: conditions,
    }

    $.ajax({
        url: COMMON_SERVICE_URL + "/MergeEventReviewer",
        type: "POST",
        data: JSON.stringify(param),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function () {
            $("#divReviewer").jsGrid("loadData");
            initCondition();
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}


/* 공통코드 */
function setCommonCode(list) {

    $.map(list, function (item, i) {
        if (item.CLASS_CODE == "S001") {
            var $chkCompay = $("<label class='checkbox-inline'><input type='checkbox' checked disabled data-company-code='" + item.SUB_CODE + "'>" + item.CODE_NAME + "</label>");
            $("#divCompany").append($chkCompay);
        } else if (item.CLASS_CODE == "S003") {
            $('#optInfoClassification').append($('<option>', {
                value: item.SUB_CODE,
                text: item.CODE_NAME
            }));
        } else if (item.CLASS_CODE == "S005") {

            $('#selJobTitle').append($('<option>', {
                value: item.SUB_CODE,
                text: item.CODE_NAME
            }));
        }
        else if (item.CLASS_CODE == "S006") {
            $('#optRetentionPeriod').append($('<option>', {
                value: item.SUB_CODE,
                text: item.CODE_NAME
            }));
        } else if (item.CLASS_CODE == "S009") {
            $("#selCostCategory").append($("<li><label class='checkbox-inline'><input type='checkbox' data-category-code='" + item.SUB_CODE + "' /> " + item.CODE_NAME + "</label></li>"));
        } else if (item.CLASS_CODE == "S013") {
            $('#selCommentCategory').append($('<option>', {
                value: item.SUB_CODE,
                text: item.CODE_NAME
            }));
        }
    });
}

/* 이벤트 목록 */
function setEventList(list) {
    $('#selEventList').append($('<option>', {
        value: "None",
        text: "-- Select --"
    }))
    $.map(list, function (item, i) {
        $('#selEventList').append($('<option>', {
            value: item.EVENT_ID,
            text: item.EVENT_NAME
        }));
    });
}

function getEvent(eventID) {
    $.ajax({
        url: COMMON_SERVICE_URL + "/GetConfiguration/" + eventID,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            selectCostCategory(eventID);
            setEvent(data);
            selectEventTableColumns(data.TABLE_NAME);
            $("#divBeforeApprover").jsGrid("loadData");
            $("#divAfterApprover").jsGrid("loadData");
            $("#divRecipient").jsGrid("loadData");
            $("#divReviewer").jsGrid("loadData");
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}

function selectCostCategory(eventID) {

    $.ajax({
        url: COMMON_SERVICE_URL + "/SelectConfigCostCategory/" + eventID,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            setCostCategory(data);
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}

function selectEventTableColumns(tableName) {


    $.ajax({
        url: COMMON_SERVICE_URL + "/SelectEventTableColumn/" + tableName,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            setEventTableColumns(data);
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}

function setCostCategory(list) {

    $("#selCostCategory input").prop('checked', false);
    $.map(list, function (item, i) {
        $("#selCostCategory input[data-category-code='" + item.CATEGORY_CODE + "']").prop('checked', true);
    });

}

function setEventTableColumns(list) {
    $('#selTableColumn').empty();
    $.map(list, function (item, i) {
        $('#selTableColumn').append($('<option>', {
            value: item.ColumnId + ":" + item.DataType,
            text: item.ColumnName
        }));
    });
}

function setEvent(data) {
    //General
    $("#txtEventName").data("event-id", data.EVENT_ID);
    $("#txtEventName").val(data.EVENT_NAME);
    $("#divDataOwner .employee-id").val(data.DATA_OWNER_NAME);
    $("#divDataOwner .employee-id").data("user-id", data.DATA_OWNER);
    $("#txtTableName").val(data.TABLE_NAME);
    $("#txtPageName").val(data.WEB_PAGE_NAME);
    $("#txtEventPrefix").val(data.PREFIX_EVENT_KEY);
    $("input:radio[name='optSkipApproval'][value='" + data.OPT_SKIP_APPROVAL + "']").prop('checked', true);
    $("input:radio[name='optShowEventList'][value='" + data.OPT_SHOW_EVENT_LIST + "']").prop('checked', true);

    $("#optInfoClassification option[value=" + data.CLASSIFICATION_INFO_CODE + "]").prop('selected', true);
    $("#optRetentionPeriod option[value=" + data.RETENTION_PERIOD_CODE + "]").prop('selected', true);
    $("#txtServiceName").val(data.AFTER_TREATMENT_SERVICE);
    $("#txtEventDesc").val(data.EVENT_DESC);
    //Approval
    $("input:radio[name='optForward'][value='" + data.OPT_FORWARD + "']").prop('checked', true);
    $("input:radio[name='optAddReviewer'][value='" + data.OPT_ADD_REVIEWER + "']").prop('checked', true);
    $("#txtReviewerDesc").val(data.OPT_ADD_REVIEWER_DESC);

    $("input:radio[name='optDefaultApproval'][value='" + data.APPROVAL_TYPE_CODE + "']").prop('checked', true);
    if (data.APPROVAL_LEVEL) $("#selApprovalLevel option[value=" + data.APPROVAL_LEVEL + "]").prop('selected', true);
    if (data.APPROVAL_JOB_TITLE_CODE) $("#selJobTitle option[value=" + data.APPROVAL_JOB_TITLE_CODE + "]").prop('selected', true);

    //Module
    $('#chkAddCostPlan').prop('checked', data.OPT_ADD_COST_PLAN == "Y");
    $(".dropup button").prop("disabled", data.OPT_ADD_COST_PLAN == "N");
    $('#chkAddParticipants').prop('checked', data.OPT_ADD_PARTICIPANTS == "Y");
    $("#chkCheckCountRule").prop("disabled", data.OPT_ADD_PARTICIPANTS == "N");//4회 체크
    $("#chkCheckCountRule").prop("checked", data.OPT_COUNT_RULE == "Y"); //4회 체크

    $('#chkAddAgenda').prop('checked', data.OPT_ADD_AGENDA == "Y");
    $("#optAgendaRoleType").prop("disabled", data.OPT_ADD_AGENDA == "N");
    $("#optAgendaRoleType").val(data.AGENDA_ROLE_TYPE);
    $('#chkAddPayment').prop('checked', data.OPT_ADD_PAYMENT == "Y");

    //Event Complete
    $("input:radio[name='optEventCompleteComment'][value='" + data.OPT_EVENT_COMPLETE_COMMENT + "']").prop('checked', true);
    $("#selCommentCategory option[value=" + data.OPT_EVENT_COMPLETE_COMMENT_CATEGORY + "]").prop('selected', true);
    $("input:radio[name='optEventCompletAttach'][value='" + data.OPT_EVENT_COMPLETE_ATTACH + "']").prop('checked', true);

}

function initEventConfiguration() {
    //General
    $("#txtEventName").val("");
    $("#divDataOwner .employee-id").val("");
    $("#divDataOwner .employee-id").data("user-id", "");
    $("#txtTableName").val("");
    $("#txtPageName").val("");
    $("#txtEventPrefix").val("");
    $("input:radio[name='optSkipApproval'][value='N']").prop('checked', true);
    $("input:radio[name='optShowEventList'][value='Y']").prop('checked', true);

    $("#optInfoClassification option:first").prop('selected', true);
    $("#optRetentionPeriod option:eq(3)").prop('selected', true);
    $("#txtServiceName").val("");
    $("#txtEventDesc").val("");
    //Approval
    $("input:radio[name='optForward'][value='Y']").prop('checked', true);
    $("input:radio[name='optAddReviewer'][value='Y']").prop('checked', true);
    $("#txtReviewerDesc").val("");

    $("input:radio[name='optDefaultApproval'][value='0001']").prop('checked', true);
    if (data.APPROVAL_LEVEL) $("#selApprovalLevel option:first").prop('selected', true);
    if (data.APPROVAL_JOB_TITLE_CODE) $("#selJobTitle option:first").prop('selected', true);

    //Module
    $('#chkAddCostPlan').prop('checked', false);
    $(".dropup button").prop("disabled", true);
    $('#chkAddParticipants').prop('checked', false);
    $("#chkCheckCountRule").prop("disabled", true);//4회 체크
    $("#chkCheckCountRule").prop("checked", false); //4회 체크

    $('#chkAddAgenda').prop('checked', false);
    $("#optAgendaRoleType").prop("disabled", true);
    $("#optAgendaRoleType").val("Select");
    $('#chkAddPayment').prop('checked', false);

    //Event Complete
    $("input:radio[name='optEventCompleteComment'][value='N']").prop('checked', true);
    $("#selCommentCategory option:first").prop('selected', true);
    $("input:radio[name='optEventCompletAttach'][value='N']").prop('checked', true);
}

function initCondition() {
    $("input[id$=hddConditionIDX]").val("0");
    $("#divCondition").jsGrid("loadData");
    $("#txtConditionValue").val("");
    $("#divApprover .employee-id").data("user-id", "");
    $("#divApprover .employee-id").val("");

}
