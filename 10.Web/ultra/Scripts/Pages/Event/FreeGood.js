$(function () {
    try {
        fn_InitEventDoc();
    }
    catch (ex) {
        fn_showError({ message: ex.message });
    }
});


function fn_InitEventDoc() {
    try {
        // 컨트롤 초기 Hidden 처리
        $("#hdivHCPArea").show();
        $("#trImpactNo").hide();
        $("#trPoNo").hide();
        $("#hdivOtherArea").hide();

        //Ver 1.0.7 : Go-Direct
        
        $("#trDeliveryDate").hide();
        $("#trReturnDate").hide();
        //Ver 1.0.7 : Go-Direct

        var status = $("input[id$=hddProcessStatus]").val();
        var statusIdx = 0;
        if (status == "Saved") statusIdx = 1;
        else if (status == "Request") statusIdx = 2;
        else if (status == "Processing") statusIdx = 3;
        else if (status == "Completed") statusIdx = 4;
        else if (status == "EventCompleted") statusIdx = 5;
        else if (status == "PaymentCompleted") statusIdx = 6;


        // 컨트롤 바인드부분
        fn_GetControlBind();

        // HCP Grid 선언부
        $("#jsGridHcp").jsGrid({
            width: "100%",
            sorting: true,
            paging: true,
            pageSize: GRID_LIST_COUNT,
            autoload: false,
            editing: false,
            rowDoubleClick: function (args) {
                var item = args.item;
            },
            controller: {
                loadData: function () {

                    var selectUrl = EVENT_SERVICE_URL + "/SelectFreeGoodHCP/HCP/" + PROCESS_ID;
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
                { name: "HCO_NAME", title: "HCO", type: "text" },
                { name: "HCP_NAME", title: "HCP", type: "text" },
                { name: "SPECIALITY", title: "Specialty", type: "text" },
                {
                    name: "PRODUCT_NAME", title: "Sample Name", type: "text",
                    itemTemplate: function (value, item) {
                        return item.PRODUCT_CODE + "(" + item.PRODUCT_NAME + ")"
                    }
                },
                {
                    name: "QTY", title: "Quantitly", type: "number", width: 80, align: "right",
                    itemTemplate: function (value, item) {
                        return fn_AddComma(value);
                    }
                },
                { name: "RECEIPT_DATE", title: "인수증일자", type: "text" },
                { type: "control", modeSwitchButton: false, editButton: false }
            ]
        });
        // Other Grid 선언부
        $("#jsGridOther").jsGrid({
            width: "100%",
            sorting: true,
            paging: true,
            pageSize: GRID_LIST_COUNT,
            autoload: false,
            editing: true,
            rowDoubleClick: function (args) {
                var item = args.item;
                fn_GetPmsDetail(item.IDX);
            },
            controller: {
                loadData: function () {
                    var selectUrl = EVENT_SERVICE_URL + "/SelectFreeGoodHCP/OTHER/" + PROCESS_ID;
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
                { name: "HCO_NAME", title: "Organization", type: "text", editing: false },
                { name: "HCP_NAME", title: "Customer", type: "text", editing: false },
                {
                    name: "PRODUCT_NAME", title: "Sample Name", type: "text", editing: false,
                    itemTemplate: function (value, item) {
                        return item.PRODUCT_CODE + "(" + item.PRODUCT_NAME + ")"
                    }
                },
                {
                    name: "QTY", title: "Quantitly", type: "number", width: 80, align: "right",
                    itemTemplate: function (value, item) {
                        return fn_AddComma(value);
                    }
                },
                { name: "RECEIPT_DATE", title: "인수증일자", type: "text" },
                { type: "control", modeSwitchButton: false, editButton: false }
            ]
        });


        // 임상시험용 AutoComplete 컨트롤 선언부
        $("input[name=optHCPType]").on("change", function () {
            console.log("33333");
            if ($(this).val() == "P") {
                $("#btnHCP").attr("data-target", "#divHealthCarePharmacistSeacherModal");
            }
            // <!-- Ver 1.0.7 : Go-Direct -->
            else if ($(this).val() == "H") {
                $("#btnHCP").attr("data-target", "#divHealthCareOfficeSeacherModal");
                
                // <!-- Ver 1.0.7 : Go-Direct -->
            }
            else
                $("#btnHCP").attr("data-target", "#layer_searchHcp");
        });
 
        // Hcp 검색 선택시 이벤트 
        $("#htnSearchContractHcp").on("click", fn_SearchContractHcp);

        $(".tab-pane-korea #txtHcpName").on("keydown", function (e) {
            if (e.keyCode == 13) {
                fn_SearchContractHcp();
            }
        });
        $(".tab-pane-korea #txtHcoName").on("keydown", function (e) {
            if (e.keyCode == 13) {
                fn_SearchContractHcp();
            }
        });
        $(".tab-pane-korea #txtSpecialty").on("keydown", function (e) {
            if (e.keyCode == 13) {
                fn_SearchContractHcp();
            }
        });

        // HCP 검색 추가 이벤트
        $("#btnAttendContractHCP").on("click", function () {
            var hcpList = [];
            var $check = $('#tblHcp tbody input:checked');
            if ($check.length > 0) {
                var jsonData = JSON.parse($check.val());
                $('#taHCP').data(jsonData);
                $("#taHCP").val(jsonData.HCPName + "(" + jsonData.OrganizationName + ")");
                $("#layer_searchHcp").modal("hide");
            }
        });

        $("#layer_searchHcp .btn-panel-close").on("click", function () {
            $("#layer_searchHcp").modal("hide");
        });
        $("#layer_searchHcp").on('show.bs.modal', function () {
            $(".tab-pane-korea #txtHcpName").val("");
            $(".tab-pane-korea #txtHcoName").val("");
            $(".tab-pane-korea #txtSpecialty").val("");
            $('#tblHcp tbody').empty();
            $(".tab-pane-korea #txtHcpName").focus();
        }).on('shown.bs.modal', function () {
            $("#layer_searchHcp .box-panel").show();
        }).on('hiden.bs.modal', function () {
            $("#layer_searchHcp .box-panel").hide();
        });

        $("#btnProd").on("click", function () {
            var type = "T|" + $("#selPurpose option:selected").attr("sampleType");
            console.log(type);
            productSearch.show(fn_AddProductItem, type);
        });

        //1,2,3 이외 선택시 Sample List  선언부 
        $("#btnSample").on("click", function () {
            var type = "T|" + $("#selPurpose option:selected").attr("sampleType");
            productSearch.show(fn_AddSampleItem, type);
        });

        // 이벤트 선언부분
        $('.btn-panel-close').click(function () {
            $(this).closest('.box-panel').hide();
        });
        $("#btnHCPAdd").on("click", fn_HCPAdd);
        $("#btnOtAdd").on("click", fn_OtAdd);
        $("#btnHCPSave").on("click", fn_SaveHcp);
        $("#btnOtherSave").on("click", fn_SaveOther);

        $("#txtQty").on("keyup", fn_ValidationNumber);
        $("#txtOtQty").on("keyup", fn_ValidationNumber);
        $("#selPurpose").on('focus', function () {
            //선택한 항목 가져오기
            $(this).attr('oldValue', $(this).val());
        }).on("change", function () {
            GridHcpitems = $("#jsGridHcp").jsGrid().data("JSGrid").data.length;
            GridOtheritems = $("#jsGridOther").jsGrid().data("JSGrid").data.length;
            //alert("GridHcpitems : " + GridHcpitems + " / GridOtheritems : " + GridOtheritems);
            if (GridHcpitems > 0 || GridOtheritems > 0) {
                fn_confirm().done(function (result) {
                    if (result) {
                        //DB 에서 data 지우고, Grid(2개) 초기화 
                        $.ajax({
                            url: EVENT_SERVICE_URL + "/DeleteFreeGoodHCP/" + PROCESS_ID,
                            type: "GET",
                            //dataType: "json",
                            contentType: "application/json; charset=utf-8",
                            success: function (data) {
                                $("#jsGridHcp").jsGrid("loadData");
                                $("#jsGridOther").jsGrid("loadData");
                                changePurpose();
                            },
                            error: function (error) {
                                fn_showError({
                                    message: error.responseText
                                });
                            },
                        });
                    }
                    else {
                        // 이전 항목으로 되돌림
                        var oldValue = $("#selPurpose").attr('oldValue');
                        $("#selPurpose").val(oldValue);
                    }
                });
            }
            else {
                changePurpose();
            }
        });

        // 그리드 정보 가져오기
        $("#jsGridHcp").jsGrid("loadData").done(function () {
            // 그리드 로드 후 작성중이 아닐 경우 삭제 버튼 제거
            if (statusIdx > 1) {
                $("#jsGridHcp .jsgrid-delete-button").hide();
            }
        });
        $("#jsGridOther").jsGrid("loadData").done(function () {
            // 그리드 로드 후 작성중이 아닐 경우 삭제 버튼 제거
            if (statusIdx > 1) {
                $("#jsGridOther .jsgrid-delete-button").hide();
            }
        });

    }
    catch (ex) {
        fn_showError({ message: ex.message });
    }
}

function changePurpose() {
    var selValue = $("#selPurpose option:selected").val();
    console.log(selValue);
    // Purpose[ 1: 성상확인용, 2: 약제과 제출용, 3:임상시험용, 13:SPU(응급임상) ] 16: 구매전 의료기기대여 인경우, 
    if (selValue == "1" || selValue == "2" || selValue == "3" || selValue == "13" || selValue == "16") {
        $("#hdivHCPArea").show();
        //$(".radio[id='optHCO']").attr("checked", false);
        //$("#optHCO").attr("disabled");
        
        $("#trDeliveryDate").hide();
        $("#trReturnDate").hide();
        
        //$("#txtDeliveryDate").val("");
        //$("#txtReturnDate").val("");

        $("#optDoctor").prop("disabled", false);
        $("#optPharmacist").prop("disabled", false);
        //$("#optHCO").prop("disabled", true);
        //$("#optHCO").prop("checked", false);

        if (selValue == "3")    // 임상시험용인 경우 ImpactNo, PoNo 입력란을 활성화한다
        {
            $("#trImpactNo").show();
            //1.0.3 : PO no 사용하지 않음
            $("#trPoNo").hide();
            $("#txtQty").prop("disabled", false);
            $("#txtQty").val("");
        }
        else if (selValue == "13")    // SPU(응급임상)인 경우 ImpactNo, PoNo 입력란을 비활성화한다
        {
            $("#trImpactNo").hide();
            $("#trPoNo").hide();
            $("#txtQty").prop("disabled", false);
            $("#txtQty").val("");
        }
        else if (selValue == "16") {
            //Ver 1.0.7 : Go-Direct
            $("#trImpactNo").hide();
            $("#trPoNo").hide();
            
            $("#trDeliveryDate").show();
            $("#trReturnDate").show();
            //$("#txtDeliveryDate").val("");
            //$("#txtReturnDate").val("");
            $("#txtQty").prop("disabled", true);
            $("#txtQty").val("");

            $("#optDoctor").prop("disabled", false);
            $("#optDoctor").prop("checked", true);
            $("#optPharmacist").prop("disabled", true);
            //$("#optHCO").prop("disabled", false);
            //$("#optHCO").prop("checked", true);

            //$("#btnHCP").attr("data-target", "#divHealthCareOfficeSeacherModal");
            //$("#tpParticipants").css("display", "inline");
            $("input[name='rdoBU'][value='RAD']").prop("checked", true);
            //console.log($(".radio[id='optHCO']"));
            //$(".radio[id='optHCO']").attr("checked", true);
            ////$("#optHCO").attr("checked");
            //$("#optHCO").removeAttr("disabled");
            //Ver 1.0.7 : Go-Direct
        }
        else {
            $("#trPoNo").hide();
            $("#txtQty").val("1");
            $("#txtQty").prop("disabled", true);
            $("#trImpactNo").hide();

           
        }
        $("#hdivOtherArea").hide();
    }
    else {
        $("#hdivHCPArea").hide();
        $("#hdivOtherArea").show();
        $("#trImpactNo").hide();
        $("#trPoNo").hide();

        //Ver 1.0.7 : Go-Direct
       
        $("#trDeliveryDate").hide();
        $("#trReturnDate").hide();
        $("#txtDeliveryDate").val("");
        $("#txtReturnDate").val("");
            //Ver 1.0.7 : Go-Direct


        
        
    }

    $("#jsGridHcp").jsGrid("loadData");
    $("#jsGridOther").jsGrid("loadData");
}

function fn_AddProductItem(data) {
    $("#taProd").val(data.PRODUCT_NAME);
    $('#taProd').data(data)
}
function fn_AddSampleItem(data) {
    $("#taSample").val(data.PRODUCT_NAME);
    $('#taSample').data(data);
}
function fn_SearchContractHcp() {
    var hcpName = $(".tab-pane-korea #txtHcpName").val();
    var orgName = $(".tab-pane-korea #txtHcoName").val();
    var speName = $(".tab-pane-korea #txtSpecialty").val();
    var url = "";
    try {

        if (hcpName.length < 1 && orgName.length < 1 && speName.length < 1) {
            fn_showWarning({
                title: "input",
                message: "Please enter the conditions."
            })
            return;
        }

        var search = {};
        
        var selValue = $("#selPurpose option:selected").val();
        if (selValue == 3) {
            url = MEDICAL_SERVICE_URL + "/SelectSearchHCPList";
            search.impactNo = getMedicalSearcher();
            search.hcpName = hcpName;
            search.orgName = orgName;
            search.speName = speName;
        }
        else {
            url = COMMON_SERVICE_URL + "/SelectSearchMasterDoctor";
            search.hcpName = hcpName;
            search.orgName = orgName;
            search.speName = speName;
        }

        $.ajax({
            url: url,
            type: "POST",
            data: JSON.stringify(search),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                displayHCPResult(data);
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });
    } catch (ex) {
        fn_showError({ message: ex.message });
    }
}
function displayHCPResult(list) {
    try {
        $('#tblHcp tbody').empty();
        var cnt = list.length;
        if (cnt < 1) {
            var rowNotFound = "<tr><td>Not Found</td><tr>";
            $('#tblHcp tbody').append(rowNotFound);
            return;
        }
        for (var i = 0; i < cnt; i++) {
            var hcp = list[i];
            var row = "<tr class='tr-hcp' data-hcp='" + JSON.stringify(hcp) + "'><td class='text-left'>";
            row += "<label class='item-name'>";
            row += "<span class='fix'><input type='radio' name='rdoContractHcp' value='" + JSON.stringify(hcp) + "' /></span>";
            row += "<strong>" + hcp.HCPName + "</strong><br/><span style='display:inline-block;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;width:280px'>" + hcp.OrganizationName + "/" + hcp.SpecialtyName + "</span></label></td>";
            row += "</tr>";
            $('#tblHcp tbody').append(row);
        }
    } catch (ex) {
        fn_showError({ message: ex.message });
    }
}
function fn_SaveHcp() {
    var processId = $("input[id$=hddProcessID]").val()
        , userId = $("input[id$=hddUserID]").val()
        , docItems, prodItems
        , retvalue = 0
        , alertMessage = ""
        , validateItem = [];

    var select = $("#selPurpose option:selected").val();

    docItems = $('#taHCP').data();

    prodItems = $('#taProd').data();

    if ($('#taHCP').val().length <= 0) {
        validateItem.push("HCP");
        retvalue = -1;
    }

    if ($('#taProd').val().length <= 0) {
        validateItem.push("Sample");
        retvalue = -1;
    }

    if ($("#txtQty").val().length <= 0) {
        validateItem.push("Qty");
        retvalue = -1;
    }

    if (retvalue < 0) {
        if (retvalue == -1) {
            alertMessage = validateItem.join();
        }
        fn_showInformation({ title: "Please fill out below fields.", message: alertMessage });
    }
    else {

        if (select == "1") {
            //성상확인용인 경우
            //약사 일 경우 이름으로 중복 체크함
            var hcpcode = ($(docItems)[0].SpecialtyName == "약사" ? $(docItems)[0].HCPName : $(docItems)[0].HCPCode);

            var url = EVENT_SERVICE_URL + "/IsExistsFreeGoodHcpItem/" + processId + "/" + hcpcode + "/" + $(docItems)[0].OrganizationCode + "/" + $(prodItems)[0].SAMPLE_CODE+"/HCP";
            //alert(url);
            $.ajax({
                url: url,
                type: "GET",
                dataType: "json",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    if (result) {
                        alertMessage = "HCP, Sample는 결재된 문서가 있습니다.";
                        retvalue = -2;
                        alert(alertMessage);
                    }
                },
                error: function (error) {
                    fn_showError({
                        message: error.responseText
                    });
                },
            });
        }

        // <!-- Ver 1.0.7 : Go-Direct -->
        if (select == "16") {
            //http://localhost:4042/ultra-svc/UltraEvent.svc/IsExistsFreeGoodHcpItem/P000027900/김우경(RAD)/WKRH00006810/2
            var hcpcode = $(docItems)[0].HCPCode;
            var url = EVENT_SERVICE_URL + "/IsExistsFreeGoodHcpItem/" + processId + "/" + hcpcode + "/" + $(docItems)[0].OrganizationCode + "/" + $(prodItems)[0].SAMPLE_CODE +"/HCO";
            //alert(url);
            //return;
            $.ajax({
                url: url,
                type: "GET",
                dataType: "json",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (result) {
                    if (result) {
                        alertMessage = "HCP, Sample는 결재된 문서가 있습니다.";
                        retvalue = -2;
                        alert(alertMessage);
                    }
                },
                error: function (error) {
                    fn_showError({
                        message: error.responseText
                    });
                },
            });


        }
        // <!-- Ver 1.0.7 : Go-Direct -->
        //var fncCallback = function (result) {
        //    if (result.toLowerCase() == "true") {
        //        alertMessage = "HCP, Sample는 결재된 문서가 있습니다.";
        //        retvalue = -2;
        //    }
        //};
        //callAjax(url, "", false, "GET", fncCallback);
 
        $("#jsGridHcp").jsGrid("insertItem", {
            PROCESS_ID: processId
            , IDX: null
            , HCP_CODE: $(docItems)[0].HCPCode
            , TYPE: "HCP"
            , SPECIALITY: $(docItems)[0].SpecialtyName
            , HCP_NAME: $(docItems)[0].HCPName
            , HCO_CODE: $(docItems)[0].OrganizationCode
            , HCO_NAME: $(docItems)[0].OrganizationName
            , PRODUCT_CODE: $(prodItems)[0].PRODUCT_CODE
            , PRODUCT_NAME: $(prodItems)[0].PRODUCT_NAME
            , SAMPLE_CODE: $(prodItems)[0].SAMPLE_CODE
            , QTY: fn_RemoveComma($("#txtQty").val())
            , IS_DELETED: "N"
            , CREATOR_ID: userId
            , UPDATER_ID: userId

        }).done(function () {
            fn_HCPAdd();
            $("#jsGridHcp").jsGrid("refresh");
        }); 
    } 
}

function fn_SaveOther() {

    try {

        var processId = $("input[id$=hddProcessID]").val()
            , userId = $("input[id$=hddUserID]").val()
            , docItems, prodItems
            , retvalue = 0
            , alertMessage = ""
            , validateItem = [];

        var select = $("#selPurpose option:selected").val();

        prodItems = $('#taSample').data();

        if ($("#txtOtCum").val().length <= 0) {
            validateItem.push("Customer");
            retvalue = -1;
        }
        if ($("#txtOtOrg").val().length <= 0) {
            validateItem.push("Organization");
            retvalue = -1;
        }

        if ($('#taSample').val().length <= 0) {
            validateItem.push("Sample");
            retvalue = -1;
        }

        if ($("#txtOtQty").val().length <= 0) {
            validateItem.push("Qty");
            retvalue = -1;
        }

        if (retvalue < 0) {
            if (retvalue == -1) {
                alertMessage = validateItem.join();
            }
            fn_showInformation({ title: "Please fill out below fields.", message: alertMessage });
        }
        else {
            $("#jsGridOther").jsGrid("insertItem", {
                PROCESS_ID: processId
                , IDX: null
                , HCP_CODE: ''
                , TYPE: "OTHER"
                , SPECIALITY: ''
                , HCP_NAME: $("#txtOtCum").val()
                , HCO_CODE: ''
                , HCO_NAME: $("#txtOtOrg").val()
                , PRODUCT_CODE: $(prodItems)[0].PRODUCT_CODE
                , PRODUCT_NAME: $(prodItems)[0].PRODUCT_NAME
                , SAMPLE_CODE: $(prodItems)[0].SAMPLE_CODE
                , QTY: fn_RemoveComma($("#txtOtQty").val())
                , IS_DELETED: "N"
                , CREATOR_ID: userId
                , UPDATER_ID: userId

            }).done(function () {
                fn_OtherAdd();
                $("#jsGridOther").jsGrid("refresh");
            });
        }

    }
    catch (ex) {
        fn_showError({ message: ex.message });
    }
}

function fn_OtherAdd() {
    $("#txtOtOrg").val("");
    $("#txtOtCum").val("");
    $("#txtOtQty").val("");
    $("#taSample").val('').removeData();
    $('#hdivAddOther').show();
}

function fn_HCPAdd() {

    var selValue = $("#selPurpose option:selected").val();
    // Purpose[ 1: 성상확인용, 2: 약제과 제출용, 3:임상시험용 ] 인경우

    if (selValue == "3")    // 임상시험용인 경우  
    {
        $("#txtQty").prop("disabled", false);
        $("#txtQty").val("");
    }
    else if (selValue == "13")    // 응급임상인 경우  
    {
        $("#txtQty").prop("disabled", false);
        $("#txtQty").val("");
    }
    else {
        $("#txtQty").val("1");
        $("#txtQty").prop("disabled", true);
    }

    $("#taHCP").val('').removeData();
    $("#taProd").val('').removeData();

    $('#hdivAddHCP').show();
}

function fn_OtAdd() {
    $("#txtOtOrg").val("");
    $("#txtOtCum").val("");
    $("#txtOtQty").val("");

    $("#taSample").val('').removeData();
    $('#hdivAddOther').show();
}

function fn_ResetAutoComplete(ctrlName) {
    if (typeof $('#' + ctrlName).data == "function") {
        $('#' + ctrlName).data({});
    }
}


function fn_DisabledControls(disable) {
    $("input").prop('disabled', disable);

    $("textarea").prop('disabled', disable);
    

}

function fn_SelectEventFreeGood(data) {
    console.log(data);
    $("span[id$=hspanRequester]").text(data.REQUESTER_NAME);
    $("span[id$=hspanOrganization]").text(data.ORGANIZATION_NAME);
    $("span[id$=hspanRetaentionPeriod]").text(data.RETENTION_PERIOD);
    //$("span[id$=hspanRequestedDate]").text(data.REQUEST_DATE);
    $("span[id$=hspanEventKey]").text(data.EVENT_KEY);
    $("span[id$=hspanStatus]").text(data.PROCESS_STATUS);
    $("#txtSubject").val(data.SUBJECT);
    $("#selPurpose").val(data.PURPOSE).change();
    $("input[name=rdoBU][value=" + data.BU + "]").prop("checked", true);
    $("#selLocation").val(data.LOCATION).change();
    $("input[id$=hddLifeCycle]").val(data.LIFE_CYCLE);

    $("#txtPoNo").val(data.PO_NO);
    $("#txtComment").val(data.COMMENT);
    if (data.DELIVER_DATE) //$("#datStartTime").val(data.START_TIME.substring(0, 16));
    {
        $(".form_date_deliverydate").datetimepicker('update', data.DELIVER_DATE.substring(0, 10));
        //$("#txtDeliveryDate").val(data.DELIVER_DATE.substring(0, 10));
        //alert($("#txtDeliveryDate").val());
    }
    if (data.RETURN_DATE) //$("#datEndTime").val(data.END_TIME.substring(0, 16));
    {
        $(".form_date_returndate").datetimepicker('update', data.RETURN_DATE.substring(0, 10));
        $("#txtReturnDate").val(data.DELIVER_DATE.substring(0, 10));
    }

    if( $("#txtComment").prop("disabled") ) $("#txtComment").change();


    // Purpose 에 따른 grid 표시
    changePurpose();


    $.ajax({
        url: MEDICAL_SERVICE_URL + "/SelectMedicalMasterList/" + $('input[id$=hhdUserID]').val() + "/" + data.IMPACT_NO,
        type: "GET",
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (info) {
            setMedicalSearcher(info[0]);
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });

}

function fn_GetControlBind() {
    var PROCESS_ID = $("input[id$=hddProcessID]").val();
    var status = $("input[id$=hddProcessStatus]").val();
    if (PROCESS_ID && status != "Temp") {
        $.ajax({
            url: EVENT_SERVICE_URL + "/SelectFreeGood/" + PROCESS_ID,
            type: "GET",
            dataType: "json",
            async: false,
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                fn_SelectEventFreeGood(data);
                fn_SelectEventAttachFiles(PROCESS_ID);//첨부파일 조회 (FileUpload.js)
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });
    }
}

function fn_ValidationNumber() {
    this.value = this.value.replace(/[^0-9,]/, '');
    var str = this.value.replace(/,/ig, "");
    $($(this).attr("value-field")).val(str); 
    this.value = fn_AddComma(str);
}

/*
    Event 저장
    Request인경우 callback function이 존재
*/
function saveEvent(action, callback) {

    var status = $("[id$=hspanStatus]").text();

    /*
        action이 Request이고 현재 Status가 없으면 신규 문서
    */
    if (action == "Request") {
        if (!status) status = "Temp";
    }
    else
        status = "Saved";

    //Subject
    var subject = $("#txtSubject").val();

    var impactNo = getMedicalSearcher();

    var purpose = $("#selPurpose option:selected").val();

    var poNo = $("#txtPoNo").val();

    // HCP, Other Grid Items Set Obj
    var items = [];

    /* 필수입력 체크 */
    var msgFillOut = "";
    if (subject.trim().length < 1) {
        msgFillOut = "Subject";
    }

    if (purpose == '1' || purpose == '2' || purpose == '3' || purpose == '13' || purpose == '16') {
        if (purpose == '3' && impactNo.trim().length < 1) {
            if (msgFillOut.length == 0) {
                msgFillOut += "impact No";
            }
            else {
                msgFillOut += ",impact No";
            }
        }
        //alert($("#txtDeliveryDate").val());
        if (purpose == '16' && $("#txtDeliveryDate").val().trim().length < 1) {
            if (msgFillOut.length == 0) {
                msgFillOut += "배송일자";
            }
            else {
                msgFillOut += ",배송일자";
            }
        }
        if (purpose == '16' && $("#txtReturnDate").val().trim().length < 1) {
            if (msgFillOut.length == 0) {
                msgFillOut += "반납일자";
            }
            else {
                msgFillOut += ",반납일자";
            }
        }
        items = $("#jsGridHcp").jsGrid().data("JSGrid").data;
    }
    else {
        setMedicalSearcher();
        impactNo = '';
        items = $("#jsGridOther").jsGrid().data("JSGrid").data;
    }


    if (msgFillOut.length > 0) {
        fn_showInformation({
            title: "Please fill out below fields.",
            message: msgFillOut
        })
        return;
    }

    var PROCESS_ID = $("input[id$=hddProcessID]").val();
    var USER_ID = $("input[id$=hddUserID]").val();

    var DELIVER_DATE = null;
    var RETURN_DATE = null;
    if (purpose == "16") {
        DELIVER_DATE = $("#txtDeliveryDate").val();
        RETURN_DATE = $("#txtReturnDate").val();
    }
    //저장

    
    var freeGood = {
        PROCESS_ID: PROCESS_ID
        , SUBJECT: subject
        , EVENT_KEY: $("#hspanEventKey").text()
        , PROCESS_STATUS: status
        , REQUESTER_ID: $("input[id$=hddUserID]").val()
        , COMPANY_CODE: $("input[id$=hddCompanyCode]").val()
        , ORGANIZATION_NAME: $("[id$=hspanOrganization]").text()
        , LIFE_CYCLE: $("input[id$=hddLifeCycle]").val()
        , PURPOSE: $("#selPurpose option:selected").val()
        , BU: $("input[name=rdoBU]:checked").val()
        , LOCATION: $("#selLocation option:selected").val()
        , IMPACT_NO: impactNo
        , PO_NO: poNo
        , COMMENT: $("#txtComment").val()
        , DELIVER_DATE: DELIVER_DATE==null? "" : DELIVER_DATE.substring(0, 10)
        , RETURN_DATE: RETURN_DATE==null ?"" : RETURN_DATE.substring(0, 10)
        , IS_DISUSED: "N"
        , CREATOR_ID: USER_ID
        , UPDATER_ID: USER_ID
    }
    console.log(freeGood);
    var dto = {
        dtoFreeGood: freeGood
        , dtoHcp: items
    }
    var dtStart = $("input[id$=datStartTime]").val();
    if (dtStart.length > 0) dtStart = dtStart + ":00";

    var notExists = true;
    /* 성상확인용인 경우 샘플코드 중복 확인 */
    if (purpose == "1" ) {
        var existItems = checkExistSampleCode(items);
        if (existItems instanceof Array && existItems.length > 0) {
            displaySampleHCPInfo(existItems);
            $("#sample_hcp_info").modal('show');
            notExists = false;
        } else if (existItems == "Error") {
            notExists = false;
        }
    }
     //< !--Ver 1.0.7 : Go - Direct-- >
    if (purpose == "16") {

        var existItems = checkExistSampleCodeRAD(items);
        if (existItems instanceof Array && existItems.length > 0) {
            displaySampleHCPInfo(existItems);
            $("#sample_hcp_info").modal('show');
            notExists = false;
        } else if (existItems == "Error") {
            notExists = false;
        }
    }
    // < !--Ver 1.0.7 : Go - Direct-- >
    console.log(dto);

    if (notExists) {
        $.ajax({
            url: EVENT_SERVICE_URL + "/InsertFreeGoodHCP",
            type: "POST",
            data: JSON.stringify(dto),
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                callback(true, subject, "");
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
                callback(false);
            },
        });
    }
}

/* 이미제공된 샘플코드 체크
   샘플제품목록을 전부 던지고 웹서비스에서 IDX가 없는 것들만 조회한다.
*/
function checkExistSampleCode(checkList) {
    var existsSample = "NotExists";
    $.ajax({
        url: EVENT_SERVICE_URL + "/SelectExistFreeGoodSample",
        type: "POST",
        data: JSON.stringify(checkList),
        dataType: "json",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            existsSample = data;
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            }).done(function () {
                existsSample = "Error";
            });

        },
    });
    return existsSample;
}


/* 이미제공된 샘플코드 체크 (RAD Injector)
   샘플제품목록을 전부 던지고 웹서비스에서 IDX가 없는 것들만 조회한다.
    <!-- Ver 1.0.7 : Go-Direct -->
*/
function checkExistSampleCodeRAD(checkList) {
    console.log(checkList);
    var existsSample = "NotExists";
    $.ajax({
        url: EVENT_SERVICE_URL + "/SelectExistFreeGoodSampleRAD",
        type: "POST",
        data: JSON.stringify(checkList),
        dataType: "json",
        async: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            existsSample = data;
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            }).done(function () {
                existsSample = "Error";
            });

        },
    });
    return existsSample;
}

function displaySampleHCPInfo(list) {
    var len = list.length;
    $("#tblExistSample tbody").empty();
    for (var i = 0; i < len; i++) {
        var hcp = list[i];
        var tr = "<tr><td>" + hcp.REQUEST_DATE + "</td><td>" + hcp.SUBJECT + "<br/>(" + hcp.PROCESS_STATUS + ")</td>";
        tr += "<td>" + hcp.EVENT_KEY + "</td><td>" + hcp.REQUESTER_NAME + "<br/>(" + hcp.ORGANIZATION_NAME + ")</td>";
        tr += "<td>" + hcp.PRODUCT_NAME + "<br/>(" + hcp.SAMPLE_CODE + ")</td>";
        tr += "<td>" + hcp.HCP_NAME + "<br/>(" + hcp.HCO_NAME + ")</td></tr>";
        $("#tblExistSample tbody").append(tr);
    }

    $("#divSampleHcp h1").text("제공된 Sample");
}