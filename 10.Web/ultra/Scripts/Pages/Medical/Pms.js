$(function () {
    try {
        fn_Init();
    }
    catch (ex) {
        fn_showError({ message: ex.message });
    } 
});

function fn_ValidationNumber()
{
    this.value = this.value.replace(/[^0-9,]/, '');
    $($(this).attr("value-field")).val(this.value.replace(/,/ig, ""));
    this.value = fn_AddComma(fn_RemoveComma(this.value));
    fn_SetAmount();
}


function fn_ValidationNumber_minus() {
    
    this.value = this.value.replace(/[^-][^0-9,]/, '');
    $($(this).attr("value-field")).val(this.value.replace(/,/ig, ""));
    this.value = fn_AddComma(fn_RemoveComma(this.value));
    fn_SetAmount();
}
 
function fn_SetAmount()
{
    var c = 0, n = 0, a = 0;
    if ($("#hhdConst").val().length > 0) {
        c = parseFloat($("#hhdConst").val());
    }
    if ($("#hhdNumber").val().length > 0) {
        n = parseFloat($("#hhdNumber").val());
    } 
    a = c * n;
    //$("#hhdAmount").val(a.toFixed(2));
    $("#hhdAmount").val(a);
    //$("#txtAmount").val(fn_AddComma(a.toFixed(2)));
    $("#txtAmount").val(fn_AddComma(a));
}

function fn_SetBtnStatus(type)
{
    var $btnSave = $("#btnSave"), $btnDelete = $("#btnDelete"), $btnNew = $("#btnNew");
    switch (type) {
        case "NEW":
            $btnSave.show();
            $btnDelete.hide();
            $btnNew.hide();
            break;
        case "MODIFY":
            $btnSave.hide();
            $btnDelete.show();
            $btnNew.show();
            break;
        case "DELETE":
            $btnSave.show();
            $btnDelete.hide();
            $btnNew.hide();
            break;
    }
}

function fn_Save() {
    try {

        var docItems = $('#taDoc').textext()[0].tags()._formData;
        var prodItems = $('#taProd').textext()[0].tags()._formData;
        var dto = {
            CREATOR_ID      : $('input[id$=hhdUserID]').val()
            ,PMS_HCP_CODE   : $(docItems)[0].HCPCode 
            ,HCP_NAME       : $(docItems)[0].HCPName 
            ,HCO_CODE       : $(docItems)[0].OrganizationCode 
            ,HCO_NAME       : $(docItems)[0].OrganizationName 
            , REVIEW_YN: $("input[name=lb_role]:checked").val()
            , DATE: $("#dtDate").val()
            , PRODUCT_CODE: $(prodItems)[0].PRODUCT_CODE
            , PRODUCT_NAME: $(prodItems)[0].PRODUCT_NAME
            , COST: $("#hhdConst").val()
            , NUMBER: $("#hhdNumber").val()
            , AMOUNT: $("#hhdAmount").val()
            , METHOD_TYPE: $("input[name=lb_role1]:checked").val()
            , CONTRACT_ID: $("#txtContractNo").val()
            , EVIDENCE_ID: $("#txtEvidenceId").val()
            , VALIDATEYN: ""
            , REMARK: $("#taComment").val()
            ,IS_DELETED     : "N"
            ,CREATE_DATE    : null
            , UPDATER_ID: $('input[id$=hhdUserID]').val()
            ,UPDATE_DATE    : null
        }

        var url = MEDICAL_SERVICE_URL + "/ModifyPms";
        var d = $.Deferred();
        
        if (fn_ValidationCheck(dto) >= 0) {
            $.ajax({
                url: url,
                type: "POST",
                dataType: "json",
                data: JSON.stringify(dto),
                cache: false,
                contentType: "application/json; charset=utf-8",
            }).done(function (response) {
                d.resolve(response);
                var result = response;
                if (result.toLowerCase() == "ok") {
                    fn_showInformation({ message: "저장되었습니다." }).done(function () {
                        fn_SetControlBind(dto);
                        $("#jsGridList").jsGrid("loadData");
                    });
                }
                else {
                    fn_showInformation({ message: result[0] });
                }
            }).fail(function (jqXHR, textStatus, errorThrown) {
                d.reject(jqXHR);
                fn_showError({ message: jqXHR.message });
            });
            d.promise();
        }

    } catch (ex) {
        
        fn_showError({ message: ex.message });
    }
}

function fn_New()
{
    try {
        fn_ResetControl();
        fn_SetBtnStatus("NEW");
        $('.btn-panel-open').click();
    } catch (ex) {
        fn_showError({ message: ex.message });
    }
}

function fn_ValidationCheck(dto) {
    var retvalue = 0;
    var alertMessage = "";
    var validateItem = [];
  
    if (dto.CONTRACT_ID == "" || dto.CONTRACT_ID == null) {
        validateItem.push("Contract No");
        retvalue = -1;
    }

    if (dto.PMS_HCP_CODE == "" || dto.HCP_NAME == "" || dto.HCO_NAME == "" || dto.HCO_CODE == "") {
        validateItem.push("HCP");
        retvalue = -1;
    }

    if (dto.PRODUCT_CODE == "") {
        validateItem.push("Product");
        retvalue = -1;
    }
 

    if (dto.COST == "") {
        validateItem.push("단가/건");
        retvalue = -1;
    }

    if (dto.NUMBER == "") {
        validateItem.push("건수");
        retvalue = -1;
    }
    if (dto.METHOD_TYPE == "")
    {
        validateItem.push("비용지급 방법");
        retvalue = -1;
    }

    if (dto.EVIDENCE_ID == "") {
        validateItem.push("Evidentce ID");
        retvalue = -1;
    }

    //if (dto.HCP_CODE == "") {
    //    validateItem.push("시험책임자");
    //    retvalue = -1;
    //}
    //else (_contractDetail.ViewStats == "new")
    //{
    //    var url = MEDICAL_SERVICE_URL + "/IsExistsHCPContract/" + dto.MEDICAL_IDX + "/" + dto.HCP_CODE;
    //    var fncCallback = function (result) {
    //        if (result.toLowerCase() == "true") {
    //            alertMessage = "시험책임자가 존재합니다. 다시 입력해주세요.";
    //            retvalue = -2;
    //        }
    //    };
    //    callAjax(url, "", false, "GET", fncCallback);
    //}

    if (retvalue < 0) {
        if (retvalue == -1) {
            alertMessage = validateItem.join();
        }
        fn_showInformation({ title: "Please fill out below fields.", message: alertMessage });
    }

    return retvalue;

}
 
function fn_Delete() {
    try {
        fn_confirm().done(function (result) {
            if (result) {
                var dto = {
                     IDX : $("#hhdIdx").val()
                   , IS_DELETED: "Y"
                   , CREATE_DATE: null
                   , UPDATER_ID: $('input[id$=hhdUserID]').val()
                   , UPDATE_DATE: null
                        }
                var url = MEDICAL_SERVICE_URL + "/ModifyPms";
                var d = $.Deferred();
               
                $.ajax({
                    url: url,
                    type: "POST",
                    dataType: "json",
                    data: JSON.stringify(dto),
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    d.resolve(response);
                    var result = response;
                    if (result.toLowerCase() == "ok") {
                        //fn_showInformation({ message: "저장되었습니다." }).done(function () {
                            fn_ResetControl();
                            fn_SetBtnStatus("NEW");
                            $("#jsGridList").jsGrid("loadData");
                       // });
                    }
                    else {
                        fn_showInformation({ message: result[0] });
                    }
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    d.reject(jqXHR);
                    fn_showError({ message: jqXHR.message });
                });
                d.promise();
            } 
             
        
        });
    }
    catch (ex) {
        fn_showError({ message: ex.message });
    }
}
 
function fn_MarkingContractNo(e)
{
    var $this = $(this);
    if (e.keyCode != 8 && e.keyCode != 46)
    {
        //this.value = this.value.replace(/[^0-9\-]/, '');
        
        if ($this.val().length == 9 || $this.val().length == 5) {
            $this.val($this.val() + "-");
        }
    }
  
}

function fn_AddProductItem(data) {
    fn_ResetAutoComplete("taProd");

    var elems = $('#taProd').textext()[0].tags().tagElements();
    for (var i = 0; i < elems.length; i++) {
        $('#taProd').textext()[0].tags().removeTag($(elems[i]));
    }

    $('#taProd').textext()[0].tags().addTags([data]);
}

function fn_Init()
{
    fn_SetBtnStatus("NEW");
    $("#txtConst").on("keyup", fn_ValidationNumber_minus);
    $("#txtNumber").on("keyup", fn_ValidationNumber);
    $("#txtAmount").on("keyup", fn_ValidationNumber);
    $("#txtContractNo").on( "keydown",fn_MarkingContractNo );
    $("#taProd").on("click", function () {
        productSearch.show(fn_AddProductItem, 'all');
    });

    $("#hrefHelp").on("click", function () {
        window.open('./pmp.html','', 'width=800px, height=412px, top=10px, left=10px,location=no,titlebar=no,status=no,scrollbars=yes,menubar=no,toolbar=no,directories=no,resizable=yes,copyhistory=no');
    });

    var cookieValue = $.cookie('ultraUserGroups');
    var isAdmin = false;
    if (cookieValue != null && cookieValue.indexOf('ef.u.kr_localappl_87_medical_admin') >= 0) {
        isAdmin = true;
    }

    $("#jsGridList").jsGrid({
        width: "100%",
        sorting: true,
        paging: true,
        pageSize: GRID_LIST_COUNT,
        autoload: true,
        filtering: true,
        rowDoubleClick: function (args) {
            var item = args.item;
            fn_GetPmsDetail(item.IDX);
        },
        controller: {
            loadData: function (filter) {

                var selectUrl = MEDICAL_SERVICE_URL + "/SelectMedicalPmsList/" + $('input[id$=hhdUserID]').val() + "/" + isAdmin.toString();
                var d = $.Deferred();

                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    response = $.grep(response, function (item) {
                        return (!filter.CONTRACT_ID || item.CONTRACT_ID.toLowerCase().indexOf(filter.CONTRACT_ID.toLowerCase()) > -1)
                            && (!filter.HCP_NAME || item.HCP_NAME.toLowerCase().indexOf(filter.HCP_NAME.toLowerCase()) > -1)
                            && (!filter.CREATOR_NAME || item.CREATOR_NAME.toLowerCase().indexOf(filter.CREATOR_NAME.toLowerCase()) > -1)
                            && (!filter.EVIDENCE_ID || item.EVIDENCE_ID.toLowerCase().indexOf(filter.EVIDENCE_ID.toLowerCase()) > -1)
                            && (!filter.DATE || item.DATE.toLowerCase().indexOf(filter.DATE.toLowerCase()) > -1)
                            && (!filter.PRODUCT_NAME || item.PRODUCT_NAME.toLowerCase().indexOf(filter.PRODUCT_NAME.toLowerCase()) > -1);
                    });
                    d.resolve(response);
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    d.reject(jqXHR);
                });

                return d.promise();
            }
        },

        fields: [
            { name: "CONTRACT_ID", title: "ContractNo", type: "text", width: 140 },
            { name: "PRODUCT_NAME", title: "Product", type: "text" },
            { name: "HCP_NAME", title: "HCP", type: "text" },
            { name: "REVIEW_ID", title: "재심사", type: "text", filtering: false },
            //{ name: "COST", title: "단가/건", type: "text" },
            {
                name: "COST", title: "단가/건", type: "number", width: 80, align: "right", filtering : false,
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            }, 
            //{ name: "NUMBER", title: "건수", type: "text" },
            {
                name: "NUMBER", title: "건수", type: "number", width: 60, align: "right", filtering: false,
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            //{ name: "AMOUNT", title: "총비용", type: "text" },
            {
                name: "AMOUNT", title: "총비용", type: "number", width: 100, align: "right", filtering: false,
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            { name: "REMARK", title: "Comment", type: "text", filtering: false},
            {
                name: "DATE", title: "비용지급일자", type: "text",
                itemTemplate: function (value, item) {
                    return value.toString().substring(0,10);
                }
            },
            { name: "EVIDENCE_ID", title: "Evidence No.", type: "text" },
            { name: "CREATOR_NAME", title: "Creator", type: "text" }
        ]
    });

    //export to Excel
    $("#btnExcel").on("click", function () {

        //var selectUrl = REPORT_SERVICE_URL + "/ExportXlsReceiptForFreeGoodList/" + userid;

        var selectUrl = MEDICAL_SERVICE_URL + "/SelectXlsMedicalPmsList/" + $('input[id$=hhdUserID]').val() + "/" + isAdmin.toString();
        
        var d = $.Deferred();

        $.ajax({
            url: selectUrl,
            type: "GET",
            dataType: "json",
            cache: false,
            contentType: "application/json; charset=utf-8",
        }).done(function (response) {
           // alert(response);
            var url = '/Ultra/Handler/FileDownHandler.aspx?DestFileName=' + encodeURIComponent(response);

            $('#iframeFileDown').attr('src', url);

            d.resolve(response);

        }).fail(function (jqXHR, textStatus, errorThrown) {

            d.reject(jqXHR);
        });

        d.promise();
    });


    // 입력폼 여닫기
    $('.btn-panel-open').click(function () {
        if( $(this).closest('.tab-cont-area').hasClass('closed') ){
            fn_SetBtnStatus("NEW");
            $(this).closest('.tab-cont-area').removeClass('closed');
        } 
    });
    $('.btn-panel-close').click(function () {
        fn_ResetControl();
        $(this).closest('.tab-cont-area').addClass('closed');
    });

    $('#taDoc').textext({
        plugins: 'autocomplete filter tags',
        //ajax: {
        //    url: COMMON_SERVICE_URL + "/SelectSearchDoctor/",
        //    dataType: 'json',
        //    cacheResults: false,
        //},
        ext: {
            itemManager: {
                stringToItem: function (str) {
                    var $div = $(str);
                    var o = null;
                    if ($div.length > 0) {
                        var user = $div.data("user");
                        o = { HCPCode: user.HCPCode, HCPName: user.HCPName, OrganizationCode: user.OrganizationCode, OrganizationName: user.OrganizationName };
                    } 
                    return o;
                },
                itemToString: function (item) {
                    var html = "";
                    if ($('#taDoc').textext()[0].tags().tagElements().length == 0) {
                        html = "<div data-user='" + JSON.stringify(item) + "'>" + item.HCPName + " / " + item.OrganizationName + "</div>";
                    }
                    else {
                        if (typeof $('#taDoc').textext == "function" && typeof $('#taDoc').textext()[0].onHideDropdown == "function") {
                            $('#taDoc').textext()[0].onHideDropdown();
                        }
                    }
                    return html;
                },
                compareItems: function (item1, item2) {
                    $(".text-tags text-tags-on-top").css("padding-top", "7px");
                    return item1.HCPCode == item2.HCPCode;
                },
                filter: function (list, data) {
                    var result = [], i, item;
                    var input = data.toLowerCase();
                    var l = list.length;
                    for (i = 0; i < l ; i++) {
                        item = list[i];
                        if (item.HCPCode.toLowerCase().indexOf(input) == 0
                            || item.HCPName.toLowerCase().indexOf(input) == 0) {
                            result.push(item);
                        }
                    }
                    return result;
                }
            }
        }
    }).bind("isTagAllowed", function (e, data) {
        try
        {
            if (data.tag.HCPCode) data.result = true;
            else data.result = false;
        }
        catch(e) {} 
    }).on("keyup", function (e) {
        if ($(e.target).textext()[0].tags().tagElements().length > 0) {
            $(this).text("");
        }
    });
     
    $("#taProd").textext({
        plugins: "autocomplete filter tags",
        //ajax: {
        //    url: COMMON_SERVICE_URL + "/SelectMasterProductList/",
        //    dataType: 'json',
        //    cacheResults: true,
        //},
        ext: {
            itemManager: {
                stringToItem: function (str) {
                    var $div = $(str);
                    var o = null;
                    if ($div.length > 0) {
                        var data = $div.data("product");
                        o = { PRODUCT_CODE: data.PRODUCT_CODE, PRODUCT_NAME: data.PRODUCT_NAME, COMPANY_NAME: data.COMPANY_NAME };
                    }
                    return o;
                },
                itemToString: function (item) {
                    var html = "";
                    if ($('#taProd').textext()[0].tags().tagElements().length == 0) {
                        html = "<div data-product='" + JSON.stringify(item) + "'>" + item.PRODUCT_NAME + "</div>";
                    }
                    else {
                        if (typeof $('#taProd').textext == "function" && typeof $('#taProd').textext()[0].onHideDropdown == "function") {
                            $('#taProd').textext()[0].onHideDropdown();
                        }
                    }
                    return html;
                }, 
                compareItems: function (item1, item2) {
                    return item1.PRODUCT_CODE == item2.PRODUCT_CODE;
                },
                filter: function (list, data) {
                    var result = [], i, item;
                    var input = data.toLowerCase();
                    var l = list.length;
                    for (i = 0; i < l ; i++) {
                        item = list[i];
                        if (item.PRODUCT_CODE.toLowerCase().indexOf(input) == 0
                            || item.PRODUCT_NAME.toLowerCase().indexOf(input) == 0) {
                            result.push(item);
                        }
                    }
                    return result;
                }
            }
        },
    }).bind("isTagAllowed", function (e, data) {
        if (data.tag.PRODUCT_CODE) data.result = true;
        else data.result = false;
    }).on("keyup", function (e) {
        if($(e.target).textext()[0].tags().tagElements().length > 0)
        {
            $(this).val("");
        }
    });
    
    $("#btnDelete").on("click", fn_Delete);
    $("#btnSave").on("click", fn_Save);
    $("#btnNew").on("click", fn_New);
    
    $("#taDoc").on("click", function () {
        $("#layer_searchHcp").modal("show");
    });

    $("#layer_searchHcp").on('show.bs.modal', function () {
        $(".tab-pane-korea #txtHcpName").val("");
        $(".tab-pane-korea #txtHcoName").val("");
        $(".tab-pane-korea #txtSpecialty").val("");
        $('#tblHcp tbody').empty();

    }).on('hide.bs.modal', function () {

    });


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

    $("#btnAttendContractHCP").on("click", function () {
        var hcpList = [];
        var $check = $('#tblHcp tbody input:checked');
        if ($check.length > 0) {

            var elems = $('#taDoc').textext()[0].tags().tagElements();
            for (var i = 0; i < elems.length; i++) {
                $('#taDoc').textext()[0].tags().removeTag($(elems[i]));
            }

            $('#taDoc').textext()[0].tags().addTags([JSON.parse($check.val())]);
            $("#layer_searchHcp").modal("hide");
        }
    });

    $(".btn-panel-close").on("click", function () {
        $("#layer_searchHcp").modal("hide");
    });
}
  
function fn_GetPmsDetail(idx)
{
    try { 
        var url = MEDICAL_SERVICE_URL + "/SelectMedicalPms/" + idx; 
        callAjax(url, "", true, "GET", fn_SetControlBind);
    } catch (ex) {
        fn_showError({ message: ex.message });
    }
}

function fn_SetControlBind(d) {
    try {
        fn_DisabledControls(false);
        fn_ResetAutoComplete("taProd");
        fn_ResetAutoComplete("taDoc");
        $("input[name=lb_role]").attr("checked", false);
        $("input[name=lb_role1]").attr("checked", false);
        $("input[name=lb_role][value=" + d.REVIEW_YN + "]").prop("checked", true);
   
        $("#hhdConst").val(d.COST);
        $("#hhdNumber").val(d.NUMBER);
        $("#hhdAmount").val(d.AMOUNT);
        $("#txtConst").val(fn_AddComma(d.COST));
        $("#txtNumber").val(fn_AddComma(d.NUMBER));
        $("#txtAmount").val(fn_AddComma(d.AMOUNT));
        $("input[name=lb_role1][value=" + d.METHOD_TYPE + "]").prop("checked", true);
        $("#txtContractNo").val(d.CONTRACT_ID);
        $("#txtEvidenceId").val(d.EVIDENCE_ID);
        $("#taComment").val(d.REMARK);
        $("#dtDate").datetimepicker('setStartDate', d.DATE);
        $("#dtDate").val(d.DATE);

        var prod = {PRODUCT_CODE: d.PRODUCT_CODE,PRODUCT_NAME: d.PRODUCT_NAME,COMPANY_NAME: ""};
        $('#taProd').textext()[0].tags().addTags([prod]);

        var doc = o = {
            HCPCode: d.PMS_HCP_CODE,
            HCPName: d.HCP_NAME,
            OrganizationCode: d.HCO_CODE,
            OrganizationName: d.HCO_NAME
        };
        $("#hhdIdx").val(d.IDX);
        $("#taDoc").textext()[0].tags().addTags([doc]);
        fn_DisabledControls(true); 
        $('.btn-panel-open').click();
        fn_SetBtnStatus("MODIFY");
    } catch (ex) {
        fn_showError({ message: ex.message });
    }
}

function fn_DisabledControls(disable)
{
    $("input").prop('disabled', disable);
    $("textarea").prop('disabled', disable);

    if (disable) {
        $("#dtDate+span>span").css("pointer-events", "none");
        $("#dtDate+span").attr("disabled", disable);
    }
    else {
        $("#dtDate+span").removeAttr("disabled");
        $("#dtDate+span>span").removeAttr("style");
    }
}

function fn_ResetControl()
{
    $("input[name=lb_role]").attr("checked", false);
    $("input[name=lb_role1]").attr("checked", false);
    $("input[name=lb_role][value=Y]").attr("checked", true);
    $("#dtDate").val("");
    $("#txtConst").val("");
    $("#txtNumber").val("");
    $("#txtAmount").val("");
    $("#hhdConst").val("0");
    $("#hhdNumber").val("0");
    $("#hhdAmount").val("0");
    $("input[name=lb_role1][value=PO]").attr("checked", true); 
    $("#txtContractNo").val("");
    $("#txtEvidenceId").val("");
    $("#taComment").val("");
    $("#hhdIdx").val("");
 
    fn_ResetAutoComplete("taProd");
    fn_ResetAutoComplete("taDoc");

    fn_DisabledControls(false);
    $('.btn-panel-open').click();
}
 
function fn_ResetAutoComplete(ctrlName)
{
    if (typeof $('#' + ctrlName).textext == "function"){
        var $Ctrl = $('#' + ctrlName);
        var elems = $Ctrl.textext()[0].tags().tagElements();
        for (var i = 0; i < elems.length; i++) {
            $Ctrl.textext()[0].tags().removeTag($(elems[i]));
        }
    } 
}

function fn_SearchContractHcp() {
    var hcpName = $(".tab-pane-korea #txtHcpName").val();
    var orgName = $(".tab-pane-korea #txtHcoName").val();
    var speName = $(".tab-pane-korea #txtSpecialty").val();

    try {

        if (hcpName.length < 1 && orgName.length < 1 && speName.length < 1) {
            fn_showWarning({
                title: "input",
                message: "Please enter the conditions."
            })
            return;
        }

        var search = {
            hcpName: hcpName,
            orgName: orgName,
            speName: speName
        };

        $.ajax({
            url: COMMON_SERVICE_URL + "/SelectSearchMasterDoctor",
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