$(function () {

    var PROCESS_ID = $('input[id$=hddProcessID]').val();
    

    //첫번째 Tab -> ByHCP
    $("#jsGridPaymentByHCP").jsGrid({
        width: "100%",
        height: "auto",

        sorting: false,
        paging: false,
        autoload: false,
        controller: {
            loadData: function () {

                var selectUrl = EVENT_SERVICE_URL + "/SelectEventPaymentByHCP/" + PROCESS_ID;
                var d = $.Deferred();

                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    d.resolve($.map(response,function(item,itemIndex){
                        return $.extend(item,{
                            Index: itemIndex + 1
                        })  
                    }));
                    selectBottomLayer();
                    
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    d.reject(jqXHR);
                    fn_showError({ message: jqXHR.responseText });
                });

                return d.promise();
            }
        },

        fields: [
            {
                name: "Index", title: "", headerTemplate: myCellFormatter, type: "number", width: 20,align:"center"
            },

            {
                name: "SECTOR_NAME", title: "Type", headerTemplate: myCellFormatter, type: "text", width: 40,
                itemTemplate: function (value, item) {
                    return "<span title=" + value + ">" + value + "</span>";
                }
            },
            {
                name: "DOC_NAME", title: "Name(HCP)", headerTemplate: myCellFormatter, type: "text", width: 70,
                itemTemplate: function (value, item) {
                    if (item.IS_ATTENDED == "Y")
                        return "<i class='i-squ bg-green'></i> <span title=" + value + ">" + value + "</span>";
                    //version 1.0.4 employee 경우 concur 에서 자료 들어오면 보여줘서, D도 표시
                    else if (item.IS_ATTENDED == "D")
                        return "<i class='i-squ bg-gray'></i> <span title=" + value + ">" + value + "</span>";
                    else
                        return "<i class='i-squ bg-yellow'></i> <span title=" + value + ">" + value + "</span>";
                }
            },
            {
                name: "HCO_NAME", title: "Organization(HCO)", headerTemplate: myCellFormatter, type: "text",
                itemTemplate: function (value, item) {
                    return "<span title=" + value + ">" + value + "</span>";
                }
            },
            { name: "ROLE", title: "Role", type: "text", width: 25 },
            {
                name: "AMOUNT_1", title: "Meal & Beverage", headerTemplate: myCellFormatter, type: "number", width: 45, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            {
                name: "AMOUNT_2", title: "Transportation", headerTemplate: myCellFormatter, type: "number", width: 45, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            {
                name: "AMOUNT_3", title: "Accommodation", headerTemplate: myCellFormatter, type: "number", width: 45, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            {
                name: "AMOUNT_4", title: "Honorarium", headerTemplate: myCellFormatter, type: "number", width: 45, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            {
                name: "AMOUNT_5", title: "Gimmick/Souvenir", headerTemplate: myCellFormatter, type: "number", width: 45, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            {
                name: "AMOUNT_TOTAL", title: "Total", headerTemplate: myCellFormatter, type: "number", width: 45, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            {
                title: "System",
                itemTemplate: function (_, item) {
                    var rtnIcon = "";
                    if (item.COUNT_CONCOUR > 0) {
                        rtnIcon += "<a data-hcp-code='" + item.HCP_CODE + "' href='#layer-concur' class='i-squ bg-skyblue'>C</a>";
                    }
                    if (item.COUNT_SRM > 0) {
                        rtnIcon += "<a data-hcp-code='" + item.HCP_CODE + "'href='#layer-srm' class='i-squ bg-green'>S</a>";
                    }
                    if (item.COUNT_YOURDOCES > 0) {
                        rtnIcon += "<a data-hcp-code='" + item.HCP_CODE + "'href='#layer-your-doces' class='i-squ bg-pink'>Y</a>";
                    }
                    return $(rtnIcon);
                },
                sorting: false,
                align: "center",
                width: 40
            },

        ]
    });
    function myCellFormatter(cellvalue, options, rowObject) {
        return '<span title="' + this.title + '">' + this.title + '</span>';
    }

    //두번째 Tab -> Plan & Actual
    $("#jsGridPaymentPlanActual").jsGrid({
        width: "100%",
        height: "auto",

        sorting: false,
        paging: false,
        autoload: false,
        controller: {
            loadData: function () {
                
                var selectUrl = EVENT_SERVICE_URL + "/SelectEventPaymentPlanActual/" + PROCESS_ID;
                var d = $.Deferred();
                
                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    d.resolve(response);
                    var amount = 0;
                    var plan_amount = 0;
                    var gap_comment = "";
                    for (var i = 0; i < response.length; i++) {
                        amount = amount + (response[i].AMOUNT_ACTUAL);
                        //version 1.0.5 Payment Complete 시, Plan vs Actual 30% Gap 발생시 comment 입력
                        plan_amount = plan_amount + (response[i].AMOUNT_PLAN);
                        gap_comment = response[i].GAP_COMMENT;
                    }
                    if (amount == 0) {
                        $("#tabPayment").find("i").addClass("fa-commenting");
                        //if no transation , don't appear the gap.
                        $("#GapReason").hide();
                        return; 
                        //2018-PPM-25 update TB_PROCESS_EVENT set REQUESTER_ID='SGWVX' where PROCESS_ID='P000002300'
                        //update TB_EVENT_PRODUCT_PRESENTATION set REQUESTER_ID = 'SGWVX' where PROCESS_ID = 'P000002300'

                    } else {
                        $("#tabPayment").find("i").removeClass("fa-commenting");
                    }
                    //Plan 이 없는 경우 GAP 이 필요없음
                    if (plan_amount == 0) {
                        $("#GapReason").hide();
                        return;
                    }

                    //version 1.0.5 Payment Complete 시, Plan vs Actual 30% Gap 발생시 comment 입력
                    $("#txtGapReason").val(gap_comment);
                    var gap_plan_actual= amount/plan_amount*100 ;
                    if (gap_plan_actual < 70 || gap_plan_actual > 130 ) {
                        $("#tabPayment").find("i").addClass("fa-commenting");
                        $("#EventPayment_tabPlanActual").find("i").addClass("fa-commenting");
                        $("#GapReason").show();
                    } else {
                        $("#tabPayment").find("i").removeClass("fa-commenting");
                        $("#EventPayment_tabPlanActual").find("i").removeClass("fa-commenting");
                        $("#GapReason").hide();
                        $("#txtGapReason").val("");
                    }
                    if (gap_comment.length > 0) {     
                        $("#tabPayment").find("i").removeClass("fa-commenting");
                        $("#EventPayment_tabPlanActual").find("i").removeClass("fa-commenting");
                        $("#btnSaveGap").hide();
                    }
                    

                }).fail(function (jqXHR, textStatus, errorThrown) {
                    d.reject(jqXHR);
                    fn_showError({ message: jqXHR.responseText });
                });

               

                return d.promise();
            }
        }
        // version 1.0.6 Payment tab 의 plan vs actual 에 총 합계표시(onDataLoaded 추가)
        , onDataLoaded: function (args) {            
            var items = args.grid.option("data");
            var total_actual = 0;
            var total_plan = 0;
            items.forEach(function (item) {
                total_actual += item.AMOUNT_ACTUAL;
                total_plan += item.AMOUNT_PLAN;
                
            });
            var total = { CATEGORY_NAME: "Total Amount", "AMOUNT_PLAN": total_plan, "AMOUNT_ACTUAL": total_actual };
            $("#jsGridPaymentPlanActual").jsGrid("insertItem", total).done(function () {
                console.log("insertion completed");
            });
        }
        ,

        fields: [
            { name: "CATEGORY_NAME", title: "Category", type: "text", width: 80 },
            {
                name: "AMOUNT_PLAN", title: "Plan Amount", type: "number", width: 100, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            {
                name: "AMOUNT_ACTUAL", title: "Actual Amount", type: "number", width: 100, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            }
            
        ]
    });

    //세번째 Tab -> Expense Upload
    $("#jsGridPaymentSRMHistory").jsGrid({
        width: "100%",
        height: "auto",

        sorting: false,
        paging: false,
        autoload: false,
        rowDoubleClick: function (args) {
            var item = args.item;

            var param = {
                processID: item.PROCESS_ID,
                poNumber: item.PO_NUMBER,
            };

            /* 첨부 파일  ----------------------------------------*/
            $("#divUploadFiles_SRM .attach-list").empty(); //Clear

            var uploadFile = {
                DisplayName: encodeURIComponent(item.DISPLAY_FILE_NAME),
                SavedName: encodeURIComponent(item.SAVED_FILE_NAME),
                AttachType: "PaymentSRM",

                FilePath: encodeURIComponent(item.FILE_PATH),
                FileHandlerUrl: item.FILE_HANDLER_URL,
            };

            var $li = $("<li data-attach-file=" + JSON.stringify(uploadFile) + "></li>");
            var $ahref = $("<a href='#' class='attach-link btn btn-xs btn-gray'>" + item.DISPLAY_FILE_NAME + "</a>");
            $ahref.css('padding-right', '8px');
            $li.append($ahref);
            $li.appendTo($("#divUploadFiles_SRM .attach-list"));
            /* 첨부 파일  ----------------------------------------*/

            $.ajax({
                url: EVENT_SERVICE_URL + "/SelectEventPaymentSavedSRM",
                type: "POST",
                data: JSON.stringify(param),
                //dataType: "json",
                cache: false,
                contentType: "application/json; charset=utf-8",
                success: function (data) {

                    displaySRMSummary(data);

                    // PO Number 표시 후 입력 비활성화
                    $("#txtUploadSRM_PoNumber").val(item.PO_NUMBER);
                    $("#txtUploadSRM_PoNumber").prop("disabled", true);
                    //저장 비활성화
                    $("#btnSaveUploadSRM").prop("disabled", true);
                    //삭제버튼 활성화
                    $("#btnDeleteUploadSRM").prop("disabled", false);
                },
                error: function (error) {
                    fn_showError({
                        message: error.responseText
                    });
                },
            });
        },
        controller: {
            loadData: function () {

                var selectUrl = EVENT_SERVICE_URL + "/SelectEventPaymentUploadSRMHistory/" + PROCESS_ID;
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
                    fn_showError({ message: jqXHR.responseText });
                });

                return d.promise();
            }
        },

        fields: [
            { name: "SRM_HISTORY_IDX", title: "ID", type: "text", width: 40 },
            { name: "PO_NUMBER", title: "PO Number", type: "text", width: 70 },
            { name: "UPLOAD_DATE", title: "Upload", type: "text", width: 70 },
            { name: "UPLOADER_NAME", title: "By Whom", type: "text", width: 80 },
            {
                name: "AMOUNT", title: "Amount", type: "number", width: 60, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            { name: "COMMENT", title: "Comment", type: "text" },

        ]
    });

    //세번째 Tab -> Input PO
    $("#jsGridPaymentInputSRM").jsGrid({
        width: "100%",
        height: "auto",

        sorting: false,
        paging: false,
        autoload: false,
        rowDoubleClick: function (args) {
            var item = args.item;
            $("#hddPayment_InputSRM").val(item.SRM_IDX); //수정
            $("#txtInputSRM_PoNumber").val(item.PO_NUMBER);
            $('select[id$=selInputSRMCategory]').val(item.CATEGORY_CODE);
            $("#datInputSRM_PostDate").val(item.POST_DATE);
            $("#numInputSRM_Amount").val(fn_AddComma(item.AMOUNT));
            $("#txtInputSRM_Comment").val(item.COMMENT);
            $("#txtInputSRM_PoNumber").prop("disabled", true); //수정시에는  PO Number 수정불가
            //$("#btnDeleteInputSRM").prop("disabled", false);
            $("#layer_InputSRM").modal("show");
        },
        controller: {
            loadData: function () {

                var selectUrl = EVENT_SERVICE_URL + "/SelectEventPaymentInputSRM/" + PROCESS_ID;
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
                    fn_showError({ message: jqXHR.responseText });
                });

                return d.promise();
            }
        },

        fields: [
            { name: "PO_NUMBER", title: "PO Number", type: "text", width: 70 },
            { name: "CATEGORY_NAME", title: "Category", type: "text", width: 70 },
            { name: "POST_DATE", title: "Post Date", type: "text", width: 70 },
            {
                name: "AMOUNT", title: "Amount", type: "number", width: 60, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            { name: "COMMENT", title: "Comment", type: "text" },

        ]
    });

    //version 1.0.5 Payment Complete 시, Plan vs Actual 30% Gap 발생시 comment 입력
    $("#btnSaveGap").click(function () {
        var processID = $('input[id$=hddProcessID]').val();
        var userID = $('input[id$=hddUserID]').val();
        
        var attachFile = null;
        var userids = [];
        var inputComment = {
            processID: processID,
            commentCategory: "",
            comment: $("#txtGapReason").val(),
            userID: userID,
            logType: "GAP_Plan-Actural(±30%)",
            attachFile: attachFile,
            sendMailApproverId: userids.join()
        }

        $.ajax({
            url: EVENT_SERVICE_URL + "/InsertInputComment",
            type: "POST",
            data: JSON.stringify(inputComment),
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                fn_showInformation({
                    title: "Saved",
                    message: "Saved your reason."
                }).done(function () {
                    $("#tabPayment").find("i").removeClass("fa-commenting");
                    $("#EventPayment_tabPlanActual").find("i").removeClass("fa-commenting");
                    $("#btnSaveGap").hide();
                });
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                }).done(function () {
                    
                });

            },
        });


    });


    $("#btnSaveUploadSRM").click(function () {

        var poNumber = $("#txtUploadSRM_PoNumber").val().toUpperCase();

        if (poNumber.length!=10) {
            fn_showWarning({
                title: "Confirm!",
                message: "Please enter PO Number or 10 digits.",
            });
            return;
        }

        var processID = $('input[id$=hddProcessID]').val();
        var userID = $('input[id$=hddUserID]').val();
        var poAmount = $("#dlUploadSRMResult").data("po-amount");
        var uploadFiles = $("#divUploadFiles_SRM .attach-list").find("li");
        if (uploadFiles.length > 0) {
            var liFile = uploadFiles[0];
            var file = uploadFiles.data('attach-file');
            var history = {
                PROCESS_ID: processID,
                SRM_HISTORY_IDX: 0,
                PO_NUMBER: poNumber,
                AMOUNT: poAmount,
                COMMENT: $("#lb_SRMComment").val(),
                IS_DELETED: "N",
                DISPLAY_FILE_NAME: file.DisplayName,
                SAVED_FILE_NAME: file.SavedName,
                FILE_PATH: file.FilePath,
                FILE_HANDLER_URL: file.FileHandlerUrl,
                UPLOADER_ID: userID,
            }
            var param = {
                processID: processID,
                userID: userID,
                history: history,
            }

            $.ajax({
                url: EVENT_SERVICE_URL + "/InsertEventpaymentUploadSRMHistory",
                type: "POST",
                data: JSON.stringify(param),
                //dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function () {
                    initLeftLayer();
                    $("#jsGridPaymentSRMHistory").jsGrid("loadData");
                    $("#jsGridPaymentByHCP").jsGrid("loadData");
                    $("#jsGridPaymentPlanActual").jsGrid("loadData");
                    
                },
                error: function (error) {
                    fn_showError({
                        message: error.responseText
                    });
                },
            });

        }
    });

    $("#btnDeleteUploadSRM").click(function () {
        fn_confirm({
            title: "Confirm",
        }).done(function (result) {
            if (result) {
                deleteSRMHistory();
            }
        });
    });

    // PO 메뉴얼 입력 Open Modal
    $("#btnInputPoNumber").click(function () {
        $("#hddPayment_InputSRM").val("0"); //신규
        $("#txtInputSRM_PoNumber").val("");
        $("#datInputSRM_PostDate").val("");
        $("#numInputSRM_Amount").val("");
        $("#txtInputSRM_Comment").val("");
        $("#txtInputSRM_PoNumber").prop("disabled", false);
        //$("#btnDeleteInputSRM").prop("disabled", true);
        $("#layer_InputSRM").modal("show");
    });

    // PO 메뉴얼 입력 저장
    $("#btnSaveInputSRM").click(function () {
        saveInputSRM();
    });

    // PO 메뉴얼 자료 삭제
    $("#btnDeleteInputSRM").click(function () {
        fn_confirm({
            title: "Confirm",
        }).done(function (result) {
            if (result) {
                var processID = $('input[id$=hddProcessID]').val();
                var srmIDX = $("#hddPayment_InputSRM").val()
                var userID = $('input[id$=hddUserID]').val();
                $.ajax({
                    url: EVENT_SERVICE_URL + "/DeleteEventPaymentInputSRM/" + processID + "/" + srmIDX + "/" + userID,
                    type: "GET",
                    //dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function () {
                        $("#layer_InputSRM").modal("hide");
                        $("#jsGridPaymentInputSRM").jsGrid("loadData");
                        $("#jsGridPaymentPlanActual").jsGrid("loadData");
                    },
                    error: function (error) {
                        fn_showError({
                            message: error.responseText
                        });
                    },
                });
            }
        });

    });

    //SRM 파일 다운로드
    $('.report-card').on('click', '.upload-link', function (e) {
        e.preventDefault();
        var attachFile = $(this).closest('div').data('attach-file');
        if (attachFile) {
            $('#iframeFileDown', parent.document).attr('src', UPLOAD_HANDLER_URL + "?file=" + attachFile.FilePath);
        }
    });


    // 레이어 여닫기
    $('#jsGridPaymentByHCP').on('click', '.i-squ', function () {
        
        var tg = $(this).attr('href');
        var tgPos = $(this).closest('td').offset();
        var tdHeight = $(this).closest('td').height();

        var processID = $('input[id$=hddProcessID]').val();
        var hcpCode = $(this).data("hcp-code");

        var dataSource = '';
        if (tg.indexOf('srm') > 0) {
            dataSource = 'SRM';
        } else if (tg.indexOf('concur') > 0) {
            dataSource = 'Concur';
        } else if (tg.indexOf('your-doces') > 0) {
            dataSource = 'YourDoces';
        }

        $.ajax({
            url: EVENT_SERVICE_URL + "/SelectEventPaymentLayerByHCP/" + processID + "/" + hcpCode + "/" + dataSource,
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                showHCPLayer(data);
                $('.layer-card').hide();
                $(tg).css({ top: tgPos.top + tdHeight + 'px', left: tgPos.left - 250 + 'px' }).fadeIn('fast');
                
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });
        //return false;
    });

    $('.layer-card .close').on('click', function () {
        $(this).closest('.layer-card').hide();
    });
});

function fn_readUploadExcel(excelFilePath) {
    //alert("33");
    //조회후 엑셀 업로드시 이전자료 삭제하고 input 활성화
    //$("#txtUploadSRM_PoNumber").val("");
    $("#txtUploadSRM_PoNumber").prop("disabled", false);
    $("#lb_SRMComment").val("");

    var processID = $('input[id$=hddProcessID]').val();
    var userID = $('input[id$=hddUserID]').val();
    var param = {
        processID: processID,
        userID: userID,
        filePath: excelFilePath,
    }
    $.ajax({
        url: EVENT_SERVICE_URL + "/ReadEventPaymentUploadExcel",
        type: "POST",
        data: JSON.stringify(param),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            displaySRMSummary(data);
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}

function displaySRMSummary(list) {
    var ultraTotal = 0;
    var accomodation = 0;       //0001
    var agencyCost = 0;         //0002
    var banquetRoomVenue = 0;   //0003
    var gimmickSouvenir = 0;    //0004
    var mealBeverage = 0;       //0005
    var prints = 0;             //0006
    var transportation = 0;     //0007
    var isSuccess = true;
    $.each(list, function (index, row) {
        if (row.ERROR_MESSAGE.length > 0) isSuccess = false;

        if (row.CATEGORY_CODE == "0001") {
            accomodation += row.AMOUNT;
        } else if (row.CATEGORY_CODE == "0002") {
            agencyCost += row.AMOUNT;
        } else if (row.CATEGORY_CODE == "0003") {
            banquetRoomVenue += row.AMOUNT;
        } else if (row.CATEGORY_CODE == "0004") {
            gimmickSouvenir += row.AMOUNT;
        } else if (row.CATEGORY_CODE == "0005") {
            mealBeverage += row.AMOUNT;
        } else if (row.CATEGORY_CODE == "0006") {
            prints += row.AMOUNT;
        } else if (row.CATEGORY_CODE == "0007") {
            transportation += row.AMOUNT;
        }

        ultraTotal += row.AMOUNT;
    });
    $("#dlUploadSRMResult").data("po-amount", ultraTotal);
    $("#dlUploadSRMResult").find("dd").remove();
    $("<dd><strong>PO Amount</strong> <span>0</span></dd>").appendTo($("#dlUploadSRMResult"));
    $("<dd><strong>Ultra Total</strong> <span>" + fn_AddComma(ultraTotal) + "</span></dd>").appendTo($("#dlUploadSRMResult"));
    $("<dd><strong>Acccommodation</strong> <span>" + fn_AddComma(accomodation) + "</span></dd>").appendTo($("#dlUploadSRMResult"));
    $("<dd><strong>Meal & Beverage</strong> <span>" + fn_AddComma(mealBeverage) + "</span></dd>").appendTo($("#dlUploadSRMResult"));
    $("<dd><strong>Transpiration</strong> <span>" + fn_AddComma(transportation) + "</span></dd>").appendTo($("#dlUploadSRMResult"));
    $("<dd><strong>Gimmick/Souvenir</strong> <span>" + fn_AddComma(gimmickSouvenir) + "</span></dd>").appendTo($("#dlUploadSRMResult"));
    $("<dd><strong>Agency Cost</strong> <span>" + fn_AddComma(agencyCost) + "</span></dd>").appendTo($("#dlUploadSRMResult"));
    $("<dd><strong>Banquet Room & Venue</strong> <span>" + fn_AddComma(banquetRoomVenue) + "</span></dd>").appendTo($("#dlUploadSRMResult"));
    $("<dd><strong>Prints</strong> <span>" + fn_AddComma(prints) + "</span></dd>").appendTo($("#dlUploadSRMResult"));
    $("#hddUploadSRMList").val(JSON.stringify(list));
    if (list.length > 0)
        $("#lb_SRMComment").val(list[0].HISTORY_COMMENT);
    else
        $("#lb_SRMComment").val("");
    //저장버튼 활성화
    $("#btnSaveUploadSRM").prop("disabled", false);
}

function showUploadSRMDetail() {
    if ($("#hddUploadSRMList").val().length < 1) {
        fn_showWarning({
            title: "Confirm!",
            message: "Excel을 업로드 바랍니다."
        })
        return;
    }
    var excelUploadList = JSON.parse($("#hddUploadSRMList").val());

    var agencyCategory = $.map(excelUploadList, function (data) {
        if (data.PARTICIPANT_TYPE == "Total") return data;
    });

    var hcpList = $.map(excelUploadList, function (data) {
        if (data.PARTICIPANT_TYPE == "KoreaHCP" || data.PARTICIPANT_TYPE == "Employee") return data;
    });

    if (hcpList.length < 1) return;
    var $detailBody = $("#layer_uploadSRMDetail .modal-body");

    //HCP 목록
    var participantType = "";
    var hcpCode = hcpList[0].HCP_CODE;
    var hcpName = "";
    var hcoCode = "";
    var hcoName = "";
    var role = "";
    var isAttended = "";
    var subTotal = 0;
    var participantTotal = 0;
    var accomodation = 0;       //0001
    var agencyCost = 0;         //0002
    var banquetRoomVenue = 0;   //0003
    var gimmickSouvenir = 0;    //0004
    var mealBeverage = 0;       //0005
    var prints = 0;             //0006
    var transportation = 0;     //0007
    var $tr;
    var comment = "";
    $.map(hcpList, function (data) {
        if (hcpCode != data.HCP_CODE) {
            $tr = $("<tr/>");
            $("#layer_uploadSRMDetail .modal-body tbody").append($tr);
            var tdList = "<td>" + participantType + "</td><td>" + hcpName + "</td>";
            tdList += "<td>" + hcoName + "</td><td>" + role + "</td><td>" + isAttended + "</td>";
            tdList += "<td>" + fn_AddComma(subTotal) + "</td>";
            tdList += "<td>" + fn_AddComma(accomodation) + "</td>";
            tdList += "<td>" + fn_AddComma(mealBeverage) + "</td>";
            tdList += "<td>" + fn_AddComma(transportation) + "</td>";
            tdList += "<td>" + fn_AddComma(gimmickSouvenir) + "</td>";
            tdList += "<td>" + comment + "</td>";

            $(tdList).appendTo($tr);
            subTotal = 0;
            accomodation = 0;
            mealBeverage = 0;
            transportation = 0;
            gimmickSouvenir = 0;
        }

        participantType = data.PARTICIPANT_TYPE;
        hcpCode = data.HCP_CODE;
        hcpName = data.HCP_NAME;
        hcoName = data.HCO_NAME;
        role = data.ROLE;
        isAttended = data.IS_ATTENDED;
        if (data.CATEGORY_CODE == "0001")
            accomodation += data.AMOUNT;
        else if (data.CATEGORY_CODE == "0002")
            agencyCost += data.AMOUNT;
        else if (data.CATEGORY_CODE == "0003")
            banquetRoomVenue += data.AMOUNT;
        else if (data.CATEGORY_CODE == "0004")
            gimmickSouvenir += data.AMOUNT;
        else if (data.CATEGORY_CODE == "0005")
            mealBeverage += data.AMOUNT;
        else if (data.CATEGORY_CODE == "0006")
            prints += data.AMOUNT;
        else if (data.CATEGORY_CODE == "0007")
            transportation += data.AMOUNT;
        comment = data.COMMENT;
        subTotal += data.AMOUNT;
        participantTotal += data.AMOUNT;
    });
    $tr = $("<tr/>");
    $("#layer_uploadSRMDetail .modal-body tbody").append($tr);
    var tdList = "<td>" + participantType + "</td><td>" + hcpName + "</td>";
    tdList += "<td>" + hcoName + "</td><td>" + role + "</td><td>" + isAttended + "</td>";
    tdList += "<td>" + fn_AddComma(subTotal) + "</td>";
    tdList += "<td>" + fn_AddComma(accomodation) + "</td>";
    tdList += "<td>" + fn_AddComma(mealBeverage) + "</td>";
    tdList += "<td>" + fn_AddComma(transportation) + "</td>";
    tdList += "<td>" + fn_AddComma(gimmickSouvenir) + "</td>";
    tdList += "<td>" + fn_AddComma(gimmickSouvenir) + "</td>";
    tdList += "<td>" + comment + "</td>";

    $(tdList).appendTo($tr);

    //$("#layer_uploadSRMDetail .modal-body tbody").css("overflow-y", "auto");

    //헤더
    var total = 0;
    $("#dlUploadSRMDetailHeader").empty(); //클리어
    //$("<dt>PO Number</dt>").appendTo($("#dlUploadSRMDetailHeader"));
    //$("<dd>" + agencyCategory[0].PO_NUMBER + "</dd>").appendTo($("#dlUploadSRMDetailHeader"));
    $("<dt class='srm-total'>Total cost for participants</dt>").appendTo($("#dlUploadSRMDetailHeader"));
    $("<dd class='srm-total'>" + fn_AddComma(participantTotal) + "</dd>").appendTo($("#dlUploadSRMDetailHeader"));
    $.map(agencyCategory, function (data) {
        $("<dt>" + data.CATEGORY_NAME + "</dt>").appendTo($("#dlUploadSRMDetailHeader"));
        $("<dd>" + fn_AddComma(data.AMOUNT) + "</dd>").appendTo($("#dlUploadSRMDetailHeader"));
        total += data.AMOUNT;
    });
    total += participantTotal;
    $("<dt class='srm-total'>Total</dt>").appendTo($("#dlUploadSRMDetailHeader"));
    $("<dd class='srm-total'>" + fn_AddComma(total) + "</dd>").appendTo($("#dlUploadSRMDetailHeader"));
    $("#layer_uploadSRMDetail").modal('show');
}

function initLeftLayer() {
    $("#dlUploadSRMResult").find("dd").remove();
    $("#btnSaveUploadSRM").prop("disabled", true);
    $("#btnDeleteUploadSRM").prop("disabled", true);
    $("#hddUploadSRMList").val("");
    $("#lb_SRMComment").val("");
    $("#txtUploadSRM_PoNumber").val("");
    $("#divUploadFiles_SRM .attach-list").empty(); //파일 제거
}

function deleteSRMHistory() {

    var processID = $('input[id$=hddProcessID]').val();
    var poNUmber = $("#txtUploadSRM_PoNumber").val();
    var userID = $('input[id$=hddUserID]').val();
    var param = {
        processID: processID,
        poNumber: poNUmber,
        updaterID: userID,
    }
    $.ajax({
        url: EVENT_SERVICE_URL + "/DeleteEventPaymentUploadSRMHistory",
        type: "POST",
        data: JSON.stringify(param),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            initLeftLayer();
            $("#jsGridPaymentSRMHistory").jsGrid("loadData");
            $("#jsGridPaymentByHCP").jsGrid("loadData");
            $("#jsGridPaymentPlanActual").jsGrid("loadData");
            
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}

function selectBottomLayer() {
    var processID = $('input[id$=hddProcessID]').val();

    $.ajax({
        url: EVENT_SERVICE_URL + "/SelectEventPaymentLayer/" + processID,
        type: "GET",
        dataType: "json",
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            showBottomLayer(data);
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}

function showBottomLayer(list) {
    $("#dlPaymentSRM").find("dd").remove();
    $("#dlPaymentConcur").find("dd").remove();
    $("#dlPaymentYourDoces").find("dd").remove();
    $.map(list, function (data) {
        if (data.DataSource == "SRM") {
            var uploadFile = {
                DisplayName: encodeURIComponent(data.DisplayName),
                SavedName: encodeURIComponent(data.SavedName),
                AttachType: "PaymentSRM",
                FilePath: encodeURIComponent(data.FilePath),
                FileHandlerUrl: data.FileHandlerUrl,
            };
            var dd = generateSRMLayer(data, uploadFile);
            $("#dlPaymentSRM").append($(dd));
        } else if (data.DataSource == "Concur") {
            var dd = generateConcurLayer(data);
            $("#dlPaymentConcur").append($(dd));
        } else if (data.DataSource == "YourDoces") {
            var dd = generateYourDoces(data);
            $("#dlPaymentYourDoces").append($(dd));
        }
    });
    resizeScreen();

}

function showHCPLayer(list) {
    $("#layer-concur").find("dd").remove();
    $("#layer-srm").find("dd").remove();
    $("#layer-your-doces").find("dd").remove();
    $.map(list, function (data) {
        if (data.DataSource == "SRM") {
            var uploadFile = {
                DisplayName: encodeURIComponent(data.DisplayName),
                SavedName: encodeURIComponent(data.SavedName),
                AttachType: "PaymentSRM",
                FilePath: encodeURIComponent(data.FilePath),
                FileHandlerUrl: data.FileHandlerUrl,
            };
            var dd = generateSRMLayer(data, uploadFile);
            $("#layer-srm .report-card").append($(dd));
        } else if (data.DataSource == "Concur") {
            var dd = generateConcurLayer(data);
            $("#layer-concur .report-card").append($(dd));
        } else if (data.DataSource == "YourDoces") {
            var dd = generateYourDoces(data);
            $("#layer-your-doces .report-card").append($(dd));
        }
    });
    
}

function generateConcurLayer(data) {
    var dd = "<dd><div class='info'><strong class='date'>" + data.Date + "</strong><span class='user-name'>" + data.USER_NAME + "</span>";
    dd += "<p>" + data.Line1 + "</p><p>" + data.Line2 + "</p><p>" + data.CATEGORY_NAME + "</p></div>";
    dd += "<strong class='sum'>" + fn_AddComma(data.Amount) + "</strong></dd>";
    return dd;
}

function generateSRMLayer(data, uploadFile) {
    var dd = "<dd><strong class='date'>" + data.Date + "</strong><p>" + data.Line1 + "</p>";
    dd += "<div class='remove-item' data-attach-file=" + JSON.stringify(uploadFile) + "><a href='#' class='btn btn-xs btn-gray upload-link'>" + data.DisplayName + "</a></div>";
    dd += "<strong class='sum'>" + fn_AddComma(data.Amount) + "</strong>";
    dd += "<p>" + data.Comment + "</p></dd>";
    return dd;
}

function generateYourDoces(data) {

    var dd = "<dd><div class='info'><strong class='date'>" + data.Date + "</strong><p>" + data.Line1 + "</p><p>" + data.Line2 + "</p></div>";
    dd += "<strong class='sum'>" + fn_AddComma(data.Amount) + "</strong></dd>";
    return dd;
}

function saveInputSRM() {
    var processID = $('input[id$=hddProcessID]').val();
    var userID = $('input[id$=hddUserID]').val();
    var poNumber = $("#txtInputSRM_PoNumber").val().toUpperCase();
    var categoryCode = $('select[id$=selInputSRMCategory]').val();
    var postDate = $("#datInputSRM_PostDate").val();
    var amount = $("#numInputSRM_Amount").val();
    var comment = $("#txtInputSRM_Comment").val();

    /* 필수입력 체크 */
    var msgFillOut = "";
    if (poNumber.trim().length < 1) {
        msgFillOut = "PO Number";
    }
    if (postDate.trim().length < 1) {
        if (msgFillOut.length > 0) msgFillOut += ", ";
        msgFillOut += "Post Date";
    }
    if (amount.length < 1) {
        if (msgFillOut.length > 0) msgFillOut += ", ";
        msgFillOut += "Amount";
    }
    if (msgFillOut.length > 0) {
        fn_showInformation({
            title: "Please fill out below fields.",
            message: msgFillOut
        })
        return;
    }

    var poAmount = parseInt(fn_RemoveComma(amount));

    var srm = {
        PROCESS_ID: processID,
        SRM_IDX: $("#hddPayment_InputSRM").val(),
        PO_NUMBER: poNumber,
        CATEGORY_CODE: categoryCode,
        POST_DATE: postDate,
        AMOUNT: poAmount,
        COMMENT: comment,
        IS_DELETED: "N",
        CREATOR_ID: userID,
        UPDATER_ID: userID
    }
    $.ajax({
        url: EVENT_SERVICE_URL + "/MergeEventPaymentInputSRM",
        type: "POST",
        data: JSON.stringify(srm),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            $("#layer_InputSRM").modal("hide");
            $("#jsGridPaymentInputSRM").jsGrid("loadData");
            $("#jsGridPaymentPlanActual").jsGrid("loadData");
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}