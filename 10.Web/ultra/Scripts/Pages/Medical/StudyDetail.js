// Medical(Study) 상세 이벤트 선언부
$(function () {
    try {
        fn_Init();
        fn_GetDataBind();
        fn_GetProductsData();
        fn_GetEditorsData();
    }
    catch(ex)
    {
        fn_showError({ message: ex.message });
    }
} 
);
 
function fn_Init()
{
    $("#btnSave").on("click", fn_Save);
    $("#btnDelete").on("click", fn_Delete);
    $("#ulStudy li a").on("click", fn_GoTabPage);
    $("#btnAddContract").on("click", function () {
        fn_ShowContract("new");
    });

    // Criteira 레이어 여닫기
    $('.btn-tooltip').click(function () {
        var tg = $(this).attr('href');
        $(tg).fadeIn('fast');
        return false;
    });
    $('.layer-tooltip .close').click(function () {
        $(this).closest('.layer-tooltip').fadeOut('fast');
    });
}

function fn_GoTabPage() {
    var $this = $(this);
    var medicalIdx = $("input[id$=hhdMedicalIdx").val();
    var targetDiv = $this.attr("href");
    try {
        if (medicalIdx != "")
        {
            if (targetDiv.indexOf("hdivContractArea") > 0 && _contractDetail.IsDisplay == "Y")
            {
                $(".tab-cont-area").hide();
                fn_ShowContract();
            }
            else {  
                $(".tab-cont-area").hide();
                $(targetDiv).show();
                fn_GetAreaData(targetDiv);
            }

            $(".nav-tabs li").removeClass("active");
            $this.parent().addClass("active");
        } 
    } catch (ex) {
        fn_showError({ message: ex.message });
    }

}

function fn_ShowContract(hcpCode)
{
    $("#hdivContractArea").hide();
    $("#hdivContractDetailArea").show();
    $("#hdivContractDetailArea div.tab-cont-area").show();
    try { 
        if(_contractDetail != null) 
        {
            if ( hcpCode == "new")
            { 
                _contractDetail.Set(
                    {
                        MEDICAL_IDX: $("input[id$=hhdMedicalIdx]").val()
                        , CONTRACT_NO_PREFIX: $("#txtImpactNo").val()
                        , IS_DELETED: "N"
                        , CREATOR_ID: $('input[id$=hhdUserID]').val()
                        , UPDATER_ID: $('input[id$=hhdUserID]').val()
                    }, "new"
                );
            }
            else {
        
                _contractDetail.Set(
                    {
                        MEDICAL_IDX: $("input[id$=hhdMedicalIdx]").val()
                        , HCP_CODE: hcpCode
                        , IS_DELETED: "N"
                        , CREATOR_ID: $('input[id$=hhdUserID]').val()
                        , UPDATER_ID: $('input[id$=hhdUserID]').val()
                    }, "modify"
                );
                _contractDetail.Bind(); 
            } 
            _contractDetail.IsDisplay = "Y";
        }
    } catch (ex) {
        fn_showError({ message: ex.message });
    }
}

function fn_HideContractDetail() {
    try{
 
        $("#hdivContractArea").show();
        $("#hdivContractDetailArea").hide();
        $("#hdivContractDetailArea div.tab-cont-area").hide();
        $("#jsgContractList").jsGrid("loadData");
        if (_contractDetail != null) {
            _contractDetail.IsDisplay = "N";
        }
    } catch(ex)
    {
        fn_showError({ message: ex.message });
    }
}

function fn_GetAreaData(id)
{
    try {
         
        if (id.indexOf("hdivDetailArea") > 0)
        {
            return;
        }
        else if (id.indexOf("hdivContractArea") > 0) {
            fn_GetContractListBind();
        }
        else if (id.indexOf("hdivStudyLogArea") > 0) {

        }
    } catch(ex)
    {
        fn_showError({ message: ex.message });
    }
}
 
function fn_GetContractListBind()
{
    var medicalIdx = $("input[id$=hhdMedicalIdx").val();
    try{ 
        $("#jsgContractList").jsGrid({
            width: "100%",
            height: "500px",

            sorting: true,
            paging: true,
            pageSize: GRID_LIST_COUNT,
            autoload: true,
            rowDoubleClick: function (args) {
                var item = args.item;
                fn_ShowContract(item.HCP_CODE);
            },
            controller: {
                loadData: function () {

                    var selectUrl = MEDICAL_SERVICE_URL + "/SelectContractList/" + medicalIdx;
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
                { name: "HCP_NAME", title: "시험책임자", type: "text" },
                { name: "HCO_NAME", title: "병원명", type: "text" },
                { name: "HCR_CNT", title: "공동연구", type: "number", width : 50 },
                { name: "CONTRACT_NO", title: "Contract No", type: "text" },
                { name: "CONTRACT_DATE", title: "최초 계약 체결일", type: "text" },
                { name: "CONTRACT_CHANGE_DATE", title: "계약 만료일", type: "text" },
                { name: "CONTRACT_STATUS", title: "계약 상태", type: "text" }, 
                {
                    name: "RESEARCH_FUND", title: "연구비", type: "number", width: 90, align: "right",
                    itemTemplate: function (value, item) {
                        return fn_AddComma(value);
                    }
                } ,
                { name: "REMARK", title: "Remark", type: "text" },
            ]
        });
    } catch (ex) {
        fn_showError({ message: ex.message });
    }
}


function fn_Delete() {
    try { 
        var medicalIdx = $("input[id$=hhdMedicalIdx").val();
         
        fn_confirm().done(function (result) {
            if (result) {
                if (medicalIdx != "") {
                    var url = MEDICAL_SERVICE_URL + "/DeleteMedicalInfo/" + medicalIdx;
                    callAjax(url, "", true, "POST", fn_callbackDelete);
                }
            }
        }); 
    } catch (ex) {
        fn_showError({ message: ex.message });
    }
}
  
function fn_callbackDelete(data)
{
    try{ 
        if(data.toLowerCase() == "ok")
        { 
            fn_showInformation({ message: "삭제되였습니다." }).done(function () {
                fn_CloseTabByEventPage();
            }); 
        }
    } catch (ex) {
        fn_showError({ message: ex.message });
    }
} 

function fn_Save()
{ 
    var dto = {};
    var dtoMedical = {};
    try { 
        dto.MEDICAL_IDX = $('input[id$=hhdMedicalIdx]').val();
        dto.CATEGORY = $("select[id$=selCategory]").val();
        dto.TYPE = $("#selType").val();
        dto.STATUS = $("select[id$=selStatus]").val();
        dto.TEAM = $("select[id$=selTeam]").val(); 
        dto.TITLE = $("#txtTitle").val();
        dto.IMPACT_NO = $("#txtImpactNo").val();
        dto.APPROVAL_NO =  $("#txtApprovalNo").val();
        dto.APPROVAL_DATE = $("#dtApprovalDate").val();
        dto.COST_INFORMATION =  $("#txtCostInformation").val();
        dto.AUTHOR = $("#ddAuthor").html();
        dto.IS_DELETED = "N";
        dto.CREATOR_ID = $('input[id$=hhdUserID]').val();
        dto.CREATOR_DATE = null;
        dto.UPDATER_ID = $('input[id$=hhdUserID]').val();
        dto.UPDATE_DATE = null;

        var selectUrl = MEDICAL_SERVICE_URL + "/ModifyMedical";
        var d = $.Deferred();
        if (fn_ValidationCheck(dto) >= 0) {

            dtoMedical.medicalInfo = dto;
           // dtoMedical.products = fn_GetProducts();
            dtoMedical.editors = fn_GetEditors();
 
            $.ajax({
                url: selectUrl,
                type: "POST",
                dataType: "json",
                data: JSON.stringify(dtoMedical),
                cache: false,
                contentType: "application/json; charset=utf-8",
            }).done(function (response) {
                d.resolve(response);
                var result = response.split('|');
                if (result[0].toLowerCase() == "ok")
                {
                    $('input[id$=hhdMedicalIdx]').val(result[1]);
                    fn_showInformation({ message: "저장되었습니다." });
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

function fn_GetProducts()
{
    var prodItems = $('#taProduct').textext()[0].tags()._formData;
    var dtoProd = [];
    try{
        for (var i = 0; i <= prodItems.length -1; i++)
        { 
            var obj = {};
            obj.MEDICAL_IDX = -1;
            obj.PRODUCT_CODE = $(prodItems)[i].PRODUCT_CODE;
            obj.PRODUCT_NAME = $(prodItems)[i].PRODUCT_NAME;
            obj.IS_DELETED = 'N';
            obj.CREATOR_ID = $('input[id$=hhdUserID]').val();
            obj.UPDATER_ID = $('input[id$=hhdUserID]').val();
            dtoProd.push(obj);
        }
    } catch (ex) {
        fn_showError({ message: ex.message });
    }

    return dtoProd;
}
 
function fn_GetEditors() {
    var items = $('#acbReviewer').textext()[0].tags()._formData;
    var dtoEdit = [];
    try{
        for (var i = 0; i <= items.length -1; i++) {
            var obj = {};
            obj.MEDICAL_IDX = -1;
            obj.REVIEWER_ID = $(items)[i].USER_ID;
            obj.IS_DELETED = 'N';
            obj.CREATOR_ID = $('input[id$=hhdUserID]').val(); 
            obj.UPDATER_ID = $('input[id$=hhdUserID]').val();  

            dtoEdit.push(obj);
        }
    } catch (ex) {
        fn_showError({ message: ex.message });
    }
    return dtoEdit ;
}
 
function fn_ValidationCheck(dto)
{
    var retvalue = 0;
    var alertMessage = "";
    var validateItem = [];
    try{
    // 수정 화면일때 Impact No 체크
        if (dto.MEDICAL_IDX == "" || dto.MEDICAL_IDX == null)
        {
            dto.MEDICAL_IDX = -1;
        }

        if (dto.TITLE == "")
        {
            validateItem.push("Title");
            retvalue = -1;
        }

        if (dto.IMPACT_NO == "")
        {
            validateItem.push("Impact No");
            retvalue = -1;
        }
        else {  
            var url = MEDICAL_SERVICE_URL + "/IsMedicalInfoImpactNo/" + dto.MEDICAL_IDX + "/" + dto.IMPACT_NO;
            var fncCallback = function (data) {
                if (data.toLowerCase() == "true") {
                    alertMessage = "Impact No가 존재합니다. 다시 입력해주세요.";
                    retvalue = -2;
                }
            };
            callAjax(url, "", false, "GET", fncCallback);
     
        }

        if (dto.APPROVAL_NO == "")
        {
            validateItem.push("Approval No");
            retvalue = -1;
        }
 
        if (dto.APPROVAL_DATE == "") {
            validateItem.push("Approval Date");
            retvalue = -1;
        }

        //if (dto.COST_INFORMATION == "") {
        //    validateItem.push("Cost Imformation");
        //    retvalue = -1;
        //}
     
        if(retvalue < 0)
        {  
            if (retvalue == -1) {
                alertMessage = validateItem.join();
            }
            fn_showInformation({ title: "Please fill out below fields.", message: alertMessage });
        } 
    } catch (ex) {
        fn_showError({ message: ex.message });
    }
    
    return retvalue;
 }
   
function fn_GetDataBind()
{
    var medicalIdx = $('input[id$=hhdMedicalIdx]').val(); //$.urlParam("processid");
    try{ 
        if (medicalIdx != "") { 
            var selectUrl = MEDICAL_SERVICE_URL + "/SelectMedicalInfo/" + medicalIdx;
            var d = $.Deferred();

            $.ajax({
                url: selectUrl,
                type: "GET",
                dataType: "json",
                cache: false,
                contentType: "application/json; charset=utf-8",
            }).done(function (response) {
                d.resolve(response);
                if(response != null)
                { 
                    $("select[id$=selCategory]").val(response.CATEGORY).change();
                    $("select[id$=selStatus]").val(response.STATUS).change();
                    $("select[id$=selTeam]").val(response.TEAM).change();
                    $("#selType").val(response.TYPE).change();
                    $("#txtTitle").val(response.TITLE);
                    $("#txtImpactNo").val(response.IMPACT_NO);
                    $("#txtCostInformation").val(response.COST_INFORMATION);
                    $("#txtApprovalNo").val(response.APPROVAL_NO);
                    $("#ddAuthor").html(response.AUTHOR);
                    $("#ddCreateDate").html(response.CREATE_DATE);
                    $("input[id$=hhdAuthorID]").val(response.CREATOR_ID);
                    if (response.MODIFIER != '')
                    {
                        $("#strModifiedDate").html(response.UPDATE_DATE);
                        $("#hspanModifyName").html("By " + response.MODIFIER);
                    }
                    $("#dtApprovalDate").val(response.APPROVAL_DATE);
                    $('.form_datetime').datetimepicker('setStartDate', response.APPROVAL_DATE);
                    fn_DisabledStudyArea(true);
                } 
            }).fail(function (jqXHR, textStatus, errorThrown) {
                d.reject(jqXHR);
            });
            d.promise();
        }
        else {
            $("#ddAuthor").html($("input[id$=hhdUserName]").val());
       
            $("#ddCreateDate").html(moment().format('YYYY.MM.DD'));
            $("input[id$=hhdAuthorID]").val($("input[id$=hhdUserID]").val());
        }

    } catch (ex) {
        fn_showError({ message: ex.message });
    }
}

function fn_DisabledStudyArea(disable) {
    $("select[id$=selCategory]").prop('disabled', disable);
    //$("select[id$=selStatus]").prop('disabled', disable);
    $("select[id$=selTeam]").prop('disabled', disable);
    $("#selType").prop('disabled', disable);
    $("#txtTitle").prop('disabled', disable);
    $("#txtImpactNo").prop('disabled', disable);
    $("#txtCostInformation").prop('disabled', disable);
    $("#txtApprovalNo").prop('disabled', disable);  
    $("#dtApprovalDate").prop('disabled', disable);
    $('#taProduct').prop('disabled', disable);
    //$('#acbReviewer').prop('disabled', disable);
  
    if (disable) {
        $("#dtApprovalDate+span>span").css("pointer-events", "none");
        $("#dtApprovalDate+span").attr("disabled", disable);
    }
    else {
        $("#dtApprovalDate+span").removeAttr("disabled");
        $("#dtApprovalDate+span>span").removeAttr("style");
    }
}

function fn_GetProductsData()
{
    var medicalIdx = $("input[id$=hhdMedicalIdx").val();
    try{
        if (medicalIdx != "")
        {
            var url = MEDICAL_SERVICE_URL + "/SelectMedicalProducts/" + medicalIdx;
            callAjax(url, "", true, "GET", fn_callbackProductList);
        }
    } catch (ex) {
        fn_showError({ message: ex.message });
    }


}

function fn_callbackProductList(data) {
    try{
        if (data != null && data.length > 0)
        { 
            $('#taProduct').textext()[0].tags().addTags(data); 
        }
    } catch (ex) {
        fn_showError({ message: ex.message });
    } 
} 
 
function fn_GetEditorsData() {
    var medicalIdx = $("input[id$=hhdMedicalIdx").val();
    try {
        if (medicalIdx != "") {
            var url = MEDICAL_SERVICE_URL + "/SelectMedicalReviewer/" + medicalIdx;
            callAjax(url, "", true, "GET", fn_callbackEditors);
        }
    } catch (ex) {
        fn_showError({ message: ex.message });
    }

}

function fn_callbackEditors(data) {
    try{
        if (data != null && data.length > 0) {
            var reviewer = [];
            for (var i = 0; i <= data.length - 1; i++)
            {
                var obj = {};
                obj.USER_ID = data[i].REVIEWER_ID;
                obj.FULL_NAME = data[i].NAME;
                obj.ORG_ACRONYM = data[i].ORG_ACRONYM;
                reviewer.push(obj);
            }

            $('#acbReviewer').textext()[0].tags().addTags(reviewer);
        }
    } catch (ex) {
        fn_showError({ message: ex.message });
    }

} 