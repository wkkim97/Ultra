/// <reference path="../../Common/Common.js" />
var _contractDetail;
$(function () {
    try {
        _contractDetail = new ContractDetail();
        _contractDetail.Init(); 
        
    }
    catch (ex) {
        fn_showError({ message: ex.message });
    }
}
);

function ContractDetail()
{
    this.Init = fn_init; 
    this.IsDisplay = "N";
    this.ViewStats = "new"; // new : 신규, modify : 수정
    this.SetContralDataBind = fn_SetControlBind;
    this.Bind = fn_GetDataContractDetail;
    this.Set = fn_Set;
    var Data = {};
    this.HCRList = [];
    this.SetDisabled = fn_DisabledContractArea;
    this.SaveHCR = fn_SaveHCR;

    // HCP 검색 후 선택한 목록을 HCR Object에 추가
    this.AddHCRItems = function (item)
    {
        try { 
            var medicalIdx = $('input[id$=hhdMedicalIdx]').val(); //this.Data.MEDICAL_IDX; 
            var hcpCode = $("#hhdHcpcode").val();
             
            var doc = {
                MEDICAL_IDX: medicalIdx
                , HCP_CODE: hcpCode
                , HCR_CODE: item.HCPCode
                , HCR_NAME: item.HCPName
                , HCO_CODE: item.OrganizationCode
                , HCO_NAME: item.OrganizationName
                , SPECIALTY_CODE: item.SpecialtyCode
                , SPECIALTY_NAME: item.SpecialtyName
                , REMARK: ""
                , IS_DELETED: "N"
                , CREATOR_ID: $('input[id$=hhdUserID]').val()
                , CREATE_DATE: null
                , UPDATER_ID: $('input[id$=hhdUserID]').val()
                , UPDATE_DATE: null
            };
            this.HCRList.push(doc);
            fn_InvokeSaveHCR([doc]);
            if (typeof $("#jsgHcr").jsGrid === "function") {
                $("#jsgHcr").jsGrid("loadData");
            }
            else {
                fn_SetHCRGridBind();
            }
        } 
        catch (ex) {
            fn_showError({ message: ex.message });
        } 
    }

    $("#taDoctor").on("click", function () {
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

            var elems = $('#taDoctor').textext()[0].tags().tagElements();
            for (var i = 0; i < elems.length; i++) {
                $('#taDoctor').textext()[0].tags().removeTag($(elems[i]));
            }

            $('#taDoctor').textext()[0].tags().addTags([JSON.parse($check.val())]);
            $("#layer_searchHcp").modal("hide");
        }
    });

    $(".btn-panel-close").on("click", function () {
        $("#layer_searchHcp").modal("hide");
    });

    // Contract Detail Default Object
    this.Default = {
        MEDICAL_IDX: ""
        , HCP_CODE: ""
        , HCP_NAME: ""
        , HCO_CODE: ""
        , HCO_NAME: ""
        , CONTRACT_NO: ""
        , CONTRACT_STATUS: ""
        , CONTRACT_DATE: ""
        , CONTRACT_CHANGE_DATE: ""
        , RESEARCH_FUND: 0
        , ROLE: ""
        , REMARK: ""
        , IS_DELETED: ""
        , CREATOR_ID: ""
        , CREATE_DATE: ""
        , UPDATER_ID: ""
        , UPDATE_DATE: ""
        , CONTRACT_NO_PREFIX: ""

    }

    // 초기화
    function fn_init() {
        $("#btnBack").on("click", fn_GoBack);
 
        $("#txtResearchFund").on("keyup", function () {
            this.value = this.value.replace(/[^\d]/, '');
            this.value = fn_AddComma(this.value);
        });

        $("select[id$=selContractStatus]").on("change", function () {
            if ($(this).val() == 'TERM')
            {
                $(".DueDate").show();
            }
            else 
            {
                $(".DueDate").hide();
            }
        });

        $("#btnContractDelete").on("click", fn_Delete);
        $("#btnContractHCRDelete").on("click", fn_DeleteContractHCR);

        $("#btnContractSave").on("click", fn_Save );
       
        $('#taDoctor')
        .textext({
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
                        if ($div.length > 0)
                        {
                            var user = $div.data("user");
                            o = { HCPCode: user.HCPCode, HCPName: user.HCPName, HCPName: user.HCPName, HCPName: user.HCPName, OrganizationCode: user.OrganizationCode, OrganizationName: user.OrganizationName, SpecialtyCode: user.SpecialtyCode, SpecialtyName: user.SpecialtyName }
                        } 
                        return o;
                    },
                    itemToString: function (item) {
                        var html = "";
                        if ($('#taDoctor').textext()[0].tags().tagElements().length == 0)
                        {
                            html = "<div data-user='" + JSON.stringify(item) + "'>" + item.HCPName + " / " + item.OrganizationName + "</div>";
                        }
                        else {
                            if( typeof $('#taDoctor').textext == "function") {
                                $('#taDoctor').textext()[0].onHideDropdown();
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
        }
        )
         .bind("isTagAllowed", function (e, data) {
             if (data.tag.HCPCode) data.result = true;
             else data.result = false;
         });
 
        // 입력폼 여닫기
        $('.btn-panel-open').click(function() {
            $(this).closest('.tab-cont-area').removeClass('closed');
        });
        $('.btn-panel-close').click(function() {
            $(this).closest('.tab-cont-area').addClass('closed');
        });

        var medicalIdx = $('input[id$=hhdMedicalIdx]').val(); //this.Data.MEDICAL_IDX; 
        var hcpCode = $("#hhdHcpcode").val();

        

        $("#btnAddPayment").on("click", function () {
            var medicalIdx = $('input[id$=hhdMedicalIdx]').val(); //this.Data.MEDICAL_IDX; 
            var hcpCode = $("#hhdHcpcode").val();
            _payment.show({ MEDICAL_IDX: medicalIdx, HCP_CODE: hcpCode, FN_SAVE_CALLBACK : fn_CallBackPayment } );
        });
        $("#btnAddIMP").on("click", function () {
            var medicalIdx = $('input[id$=hhdMedicalIdx]').val(); //this.Data.MEDICAL_IDX; 
            var hcpCode = $("#hhdHcpcode").val();
            _imp.show({ MEDICAL_IDX: medicalIdx, HCP_CODE: hcpCode, FN_SAVE_CALLBACK: fn_CallBackPayment });
        });
    }
  
    function fn_CallBackPayment()
    {
        $("#jsGridPaymentList").jsGrid('loadData');
        $("#jsGridIMPList").jsGrid('loadData');
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

    function fn_GoBack() { 
        fn_HideContractDetail();
        fn_EmptyControl();
    }

    // Contract HCP 저장
    function fn_Save()
    { 
        try {
           
            this.Data = $.extend({}, this.Default, this.Data);
            this.Data.MEDICAL_IDX = $('input[id$=hhdMedicalIdx]').val();
           
            var docItems = $('#taDoctor').textext()[0].tags()._formData;
            var dtoProd = [];
            
            // 수정일경우
            if ($("#hhdHcpcode").val().length > 0)
            {
                this.Data.HCP_CODE = $("#hhdHcpcode").val();
                this.Data.HCP_NAME = $("#hhdHcpname").val();
                this.Data.HCO_CODE = $("#hhdHcocode").val();
                this.Data.HCO_NAME = $("#hhdHconame").val();
                this.Data.SPECIALTY_CODE = $("#hhdSpecialtyCode").val();
                this.Data.SPECIALTY_NAME = $("#hhdSpecialtyName").val();
            }
            else if ($(docItems).length > 0) {
                this.Data.HCP_CODE = $(docItems)[0].HCPCode;
                this.Data.HCP_NAME = $(docItems)[0].HCPName;
                this.Data.HCO_CODE = $(docItems)[0].OrganizationCode;
                this.Data.HCO_NAME = $(docItems)[0].OrganizationName;
                this.Data.SPECIALTY_CODE = $(docItems)[0].SpecialtyCode;
                this.Data.SPECIALTY_NAME = $(docItems)[0].SpecialtyName;

                $("#hhdHcpcode").val(this.Data.HCP_CODE);
                $("#hhdHcpname").val(this.Data.HCP_NAME);
                $("#hhdHcocode").val(this.Data.HCO_CODE);
                $("#hhdHconame").val(this.Data.HCO_NAME);

                $("#hhdSpecialtyCode").val(this.Data.SPECIALTY_CODE);
                $("#hhdSpecialtyName").val(this.Data.SPECIALTY_NAME);
               
            } 
            
            this.Data.CONTRACT_NO = $("#spanPreFixContractNo").text() + "-" + $("#txtLastContractNo").val();
            this.Data.CONTRACT_STATUS = $("select[id$=selContractStatus]").val();
            this.Data.CONTRACT_DATE = $("#dtContractDate").val();
            this.Data.CONTRACT_CHANGE_DATE = $("#dtContractLastDate").val();
            //this.Data.RESEARCH_FUND = fn_RemoveComma( $("#txtResearchFund").val());
            this.Data.RESEARCH_FUND = 0;
            this.Data.ROLE = "";
            this.Data.IS_DELETED = "N";
            this.Data.REMARK = $("#txtRemark").val();
            this.Data.CREATOR_ID = $('input[id$=hhdUserID]').val();
            this.Data.CREATOR_DATE = null;
            this.Data.UPDATER_ID = $('input[id$=hhdUserID]').val();
            this.Data.UPDATE_DATE = null;
            var md = this.Data;
            var url = MEDICAL_SERVICE_URL + "/ModifyHcpContract";
            var d = $.Deferred();
            if (fn_ValidationCheck(md) >= 0) {
                $.ajax({
                    url: url,
                    type: "POST",
                    dataType: "json",
                    data: JSON.stringify(this.Data),
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    d.resolve(response);
                    var result = response; 
                    if (result.toLowerCase() == "ok") { 
                       // fn_showInformation({ message: "저장되었습니다." }).done(function () {
                            _contractDetail.ViewStats == "modify";
                            _contractDetail.Data = md; 
                            $("#spanDoctorInfo").html(_contractDetail.Data.HCP_NAME + " / " + _contractDetail.Data.HCO_NAME);
                            fn_SetControlBind(md);
                            fn_SetHCRGridBind(md);
                            fn_SetPaymentGridBind(md);
                            fn_SetIMPGridBind(md);
                            fn_EnableHCRCTRL(true);
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

        }
        catch (ex) {
            fn_showError({ message: ex.message });
        }
    }

    function fn_ValidationCheck(dto)
    {
        var retvalue = 0;
        var alertMessage = "";
        var validateItem = [];
        // 수정 화면일때 Impact No 체크
        if (dto.MEDICAL_IDX == "" || dto.MEDICAL_IDX == null) {
            dto.MEDICAL_IDX = -1;
        }

        if (dto.HCP_CODE == "" || dto.HCP_NAME == "" || dto.HCO_NAME == "" || dto.HCO_CODE == "") {
            validateItem.push("시험책임자");
            retvalue = -1;
        }

        //if (dto.RESEARCH_FUND == "") {
        //    validateItem.push("연구비");
        //    retvalue = -1;
        //}

        if ($("#txtLastContractNo").val() == "") {
            validateItem.push("Contract No");
            retvalue = -1;
        }

        if (dto.CONTRACT_DATE == "") {
            validateItem.push("최초계약체결일");
            retvalue = -1;
        }

        if( dto.CONTRACT_STATUS == "TERM" &&  dto.CONTRACT_CHANGE_DATE == ""  )
        {
            validateItem.push("계약만료일");
            retvalue = -1;
        }

        if (dto.HCP_CODE == "")
        {
            validateItem.push("시험책임자");
            retvalue = -1;
        }
        else  (_contractDetail.ViewStats == "new")
        {
            var url = MEDICAL_SERVICE_URL + "/IsExistsHCPContract/" + dto.MEDICAL_IDX + "/" + dto.HCP_CODE;
            var fncCallback = function (result) {
                if (result.toLowerCase() == "true") {
                    alertMessage = "시험책임자가 존재합니다. 다시 입력해주세요.";
                    retvalue = -2;
                }
            };
            callAjax(url, "", false, "GET", fncCallback);
        }
 
        if (retvalue < 0) {
            if (retvalue == -1) {
                alertMessage =  validateItem.join();
            }
            fn_showInformation({   title: "Please fill out below fields.", message: alertMessage });
        }

        return retvalue;
 
    }

    // HCP Contract 삭제
    function fn_Delete() {
        try {
            var medicalIdx = $("input[id$=hhdMedicalIdx").val();
          
            if (medicalIdx != "" && _contractDetail.Data.HCP_CODE != "")
            {
                var hcpCode = _contractDetail.Data.HCP_CODE;
                fn_confirm().done(function (result) {
                    if (result) {
                        if (medicalIdx != "") {
                            var url = MEDICAL_SERVICE_URL + "/DeleteContractHCP/" + medicalIdx + "/" + hcpCode;
                            callAjax(url, "", true, "POST", fn_cbDeleteHCP);
                        }
                    }
                });
            }

        }
        catch (ex) {
            fn_showError({ message: ex.message });
        }
    }
    function fn_cbDeleteHCP(d)
    {
        if (d.toLowerCase() == "ok") {
            fn_GoBack();
        }
        else {
            fn_showInformation({ 
                message: "failure"
            })
        }

    }
    // Contract Detail 조회 함수
    function fn_GetDataContractDetail() {
        var medicalIdx = this.Data.MEDICAL_IDX;
        var hcpCode = this.Data.HCP_CODE;

        if (medicalIdx != "" && hcpCode != "") {
            var url = MEDICAL_SERVICE_URL + "/SelectContractDetail/" + medicalIdx + "/" + hcpCode;
            callAjax(url, "", true, "GET", fn_callbackContract);

            // Payment Grid List 
            fn_SetPaymentGridBind(this.Data);
            fn_SetIMPGridBind(this.Data);
        }

    }

    function fn_SetIMPGridBind(dto) {
        
        if (dto != undefined && dto.HCP_CODE != undefined) {
            
            $("#jsGridIMPList").jsGrid({
                width: "100%",
                sorting: true,
                paging: true,
                pageSize: GRID_LIST_COUNT,
                autoload: true,
                rowDoubleClick: function (args) {
                    var item = args.item;

                    _imp.show({ MEDICAL_IDX: item.MEDICAL_IDX, HCP_CODE: item.HCP_CODE, IDX: item.IDX, FN_SAVE_CALLBACK: fn_CallBackPayment, ITEM: item });
                },
                controller: {
                    loadData: function () {

                        var selectUrl = MEDICAL_SERVICE_URL + "/SelectHcpIMPList/" + dto.MEDICAL_IDX + "/" + dto.HCP_CODE;
                        
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
                            alert(jqXHR.message)
                            
                        });

                        return d.promise();
                    }
                },

                fields: [
                    { name: "CATEGORY", title: "CATEGORY", type: "text" },
                    { name: "ORDER_NO", title: "ORDER_NO", type: "text" },
                    { name: "AIRBILL_NO", title: "AIRBILL_NO", type: "text" },
                    { name: "IMP", title: "IMP", type: "text" },
                    { name: "DOSE", title: "DOSE", type: "text" },
                    { name: "UNIT", title: "UNIT", type: "text" },
                    { name: "QTY", title: "QTY", type: "text" },
                    { name: "TYPE", title: "TYPE", type: "text" },
                    { name: "COMMENT", title: "COMMENT", type: "text" },

                    //{ name: "DATE", title: "비용지급일자", type: "text" },
                    //{
                    //    name: "AMOUNT", title: "지급비용", type: "number", width: 100, align: "right",
                    //    itemTemplate: function (value, item) {
                    //        return fn_AddComma(value);
                    //    }
                    //},
                    //{ name: "COMMENT", title: "Comment", type: "text" },
                    //{ name: "METHOD_TYPE", title: "지급비용방법", type: "text" },
                    //{ name: "EVIDENCE_ID", title: "Evidence No.", type: "text" },
                    //{ name: "CREATOR_ID", title: "Creator", type: "text" }
                ]
            });

        }
    }
    function fn_SetPaymentGridBind(dto)
    {
        if (dto != undefined && dto.HCP_CODE != undefined) {
            $("#jsGridPaymentList").jsGrid({
                width: "100%",
                sorting: true,
                paging: true,
                pageSize: GRID_LIST_COUNT,
                autoload: true,
                rowDoubleClick: function (args) {
                    var item = args.item;

                    _payment.show({ MEDICAL_IDX: item.MEDICAL_IDX, HCP_CODE: item.HCP_CODE, IDX: item.IDX, FN_SAVE_CALLBACK: fn_CallBackPayment, ITEM: item });
                },
                controller: {
                    loadData: function () {

                        var selectUrl = MEDICAL_SERVICE_URL + "/SelectHcpPaymentList/" + dto.MEDICAL_IDX + "/" + dto.HCP_CODE;
                        var d = $.Deferred();

                        $.ajax({
                            url: selectUrl,
                            type: "GET",
                            dataType: "json",
                            cache: false,
                            contentType: "application/json; charset=utf-8",
                        }).done(function (response) {
                            d.resolve(response);
                            var totalAmount = 0;
                            $.each(response, function (index, value) {
                                totalAmount += value.AMOUNT;  //Iterate over your first array and then grab the second element add the values up
                            });
                            $("#txtResearchFund").val(fn_AddComma(totalAmount));

                        }).fail(function (jqXHR, textStatus, errorThrown) {
                            d.reject(jqXHR);
                        });

                        return d.promise();
                    }
                },

                fields: [
                    { name: "DATE", title: "비용지급일자", type: "text" },
                    {
                        name: "AMOUNT", title: "지급비용", type: "number", width: 100, align: "right",
                        itemTemplate: function (value, item) {
                            return fn_AddComma(value);
                        }
                    },
                    { name: "COMMENT", title: "Comment", type: "text" },
                    { name: "METHOD_TYPE", title: "지급비용방법", type: "text" },
                    { name: "EVIDENCE_ID", title: "Evidence No.", type: "text" },
                    { name: "CREATOR_ID", title: "Creator", type: "text" }
                ]
            });
            
        }
    }
    function fn_EnableHCRCTRL(val)
    {
        if (val) { 
            $("#divKoreaHCP").show();
            $("#divHCRListArea").show();
            $("#divPaymentArea").show();
        }
        else {
            $("#divKoreaHCP").hide();
            $("#divHCRListArea").hide();
            $("#divPaymentArea").hide();
        }
    }

    // ajax 호출하여 컨트롤에 바인딩
    function fn_callbackContract(d) {
        try{
 
            if (d != null ) {
                fn_SetControlBind(d);
                if (_contractDetail.ViewStats == "modify")
                {
                    fn_SetHCRGridBind(d);
                    fn_EnableHCRCTRL(true);
                }
                else 
                {
                    fn_EnableHCRCTRL(false);
                }
            } 
        }
        catch (ex) {
            fn_showError({ message: ex.message });
        }
    };
     
    // 컨트롤 초기설정( d = 키값, status = 화면 상태)
    function fn_Set(d, status)
    {
        try{
            this.Data = null;
            this.ViewStats = status;
            if (status == "new") {
                $("#spanPreFixContractNo").text(d.CONTRACT_NO_PREFIX);
                fn_EnableHCRCTRL(false);
                fn_DisabledContractArea(false);
            }

            this.Data = $.extend({}, this.Default, d);

        }
        catch (ex) {
            fn_showError({ message: ex.message });
        } 
    }

    // 화면 컨트롤 초기화
    function fn_EmptyControl()
    {
        try{
            $("select[id$=selContractStatus]").change();
            $("#dtContractDate").val("");
            $("#dtContractLastDate").val("");
            $("#spanPreFixContractNo").text("");
            $("#txtLastContractNo").val("");
            $("#txtRemark").val("");
            $("#txtResearchFund").val("0");

            $("#taDoctor").parent().parent().show();
            $("#tdDoctorArea").css("padding-bottom", "8px");
            $("#spanDoctorInfo").hide();

            $("#spanDoctorInfo").html("");
            $("#hhdHcpcode").val("");
            $("#hhdHcocode").val("");
            $("#hhdHcpname").val("");
            $("#hhdHconame").val("");
            $("#jsgHcr").jsGrid('option', 'data', []);
            $("#jsGridPaymentList").jsGrid('option', 'data', []);
            $("#jsGridIMPList").jsGrid('option', 'data', []);
            if ($(".btn-panel-close").closest('.tab-cont-area').hasClass('closed')) {
                $(".btn-panel-close").closest('.tab-cont-area').removeClass('closed');
            }
            var elems = $('#taDoctor').textext()[0].tags().tagElements();
            for (var i = 0; i < elems.length; i++) {
                $('#taDoctor').textext()[0].tags().removeTag($(elems[i]));
            }
            jsSearchHcp1.Reset();
           
        } catch(ex)
        {
            fn_showError({ message: ex.message });
        }
       
    }

    // Contract (HCP) 상세 컨트롤 바인딩
    function fn_SetControlBind(d)
    { 
        this.Data = $.extend({}, d, this.Default);
       
        $("select[id$=selContractStatus]").val(this.Data.CONTRACT_STATUS).change();
     
        $("#dtContractDate").val(this.Data.CONTRACT_DATE);
        $("#dtContractLastDate").val(this.Data.CONTRACT_CHANGE_DATE);
  
        //$("#txtResearchFund").val(fn_AddComma(this.Data.RESEARCH_FUND));
        if (this.Data.CONTRACT_NO.indexOf("-") > 0)
        {
            $("#spanPreFixContractNo").text(this.Data.CONTRACT_NO.split("-")[0]);
            
            $("#txtLastContractNo").val(this.Data.CONTRACT_NO.split("-")[1].toString());
        }
        
        $("#txtRemark").val(this.Data.REMARK);
        
        $("#spanDoctorInfo").html(this.Data.HCP_NAME + " / " + this.Data.HCO_NAME);
        $("#hhdHcpcode").val(this.Data.HCP_CODE);
        $("#hhdHcpname").val(this.Data.HCP_NAME);
        $("#hhdHconame").val(this.Data.HCO_NAME);
        $("#hhdHcocode").val(this.Data.HCO_CODE);
        if(this.Data.HCP_CODE != "" || this.Data.HCP_CODE != null)
        {
            $("#taDoctor").parent().parent().hide();
            $("#tdDoctorArea").css("padding-bottom", "0px");
            $("#spanDoctorInfo").show();
            
        }
        else {
            $("#taDoctor").parent().parent().show();
            $("#tdDoctorArea").css("padding-bottom", "8px");
            $("#spanDoctorInfo").hide();
        }
        fn_DisabledContractArea(true);
    }

    // 공동연구자 (JsGrid)리스트에서 선택한 Object List
    var selectedHcrItems = [];
    
    var fn_SelectItem = function (item) {
        selectedHcrItems.push(item); 
    };

    var fn_UnselectItem = function (item) {
        selectedHcrItems = $.grep(selectedHcrItems, function (i) {
            return i !== item;
        }); 
    };
    
    // HCP 검색리스트에서 선택한 HCP를 공동연구자(HCR)에 저장
    function fn_SaveHCR() {
        try {  
            fn_InvokeSaveHCR(this.HCRList);
            setTimeout(function () {
                if(typeof $("#jsgHcr").jsGrid === "function")
                {
                    $("#jsgHcr").jsGrid("loadData");
                }
                else { 
                    fn_SetHCRGridBind();
                }
                if (typeof $("#jsGridPaymentList").jsGrid === "function") {
                    $("#jsGridPaymentList").jsGrid("loadData");
                }
                else {
                    fn_SetPaymentGridBind();
                }
                if (typeof $("#jsGridIMPList").jsGrid === "function") {
                    $("#jsGridIMPList").jsGrid("loadData");
                }
                else {
                    fn_SetIMPGridBind();
                }
              
                
              
            }, 500);
        }
        catch (ex) {
            fn_showError({ message: ex.message });
        }
    };

    function fn_InvokeSaveHCR(objHCR)
    {
        var d = $.Deferred();
        var url = MEDICAL_SERVICE_URL + "/ModifyHcrContract";
        $.ajax({
            url: url,
            type: "POST",
            async : false,
            dataType: "json",
            data: JSON.stringify(objHCR),
            cache: false,
            contentType: "application/json; charset=utf-8",
        }).done(function (response) {
            d.resolve(response);
            var result = response;
            if (result.toLowerCase() == "ok") {
                //fn_showInformation({ message: "Success." });
            }
            else {
                fn_showInformation({ message: result[0] });
            }
        }).fail(function (jqXHR, textStatus, errorThrown) {
            d.reject(jqXHR);
            fn_showError({ message: $(jqXHR.responseText).text() });
        });
        d.promise();

    }
     

    function fn_DisabledContractArea(disable) {
        $("#txtLastContractNo").prop('disabled', disable);
        //$("#txtRemark").prop('disabled', disable);
        //$("#txtResearchFund").prop('disabled', disable);
        $("#dtContractDate").prop('disabled', disable); 
        $("#dtContractDate input").prop("disabled", disable);
        $("#dtContractDate button").prop("disabled", disable);
     
        if (disable)
        {
            $("#dtContractDate+span>span").css("pointer-events", "none");
            $("#dtContractDate+span").attr("disabled", disable);
        }
        else {
            $("#dtContractDate+span").removeAttr("disabled");
            $("#dtContractDate+span>span").removeAttr("style");
        }


     }

    function fn_DeleteContractHCR() {
        try { 
            fn_confirm().done(function (result) {
                if (result) { 
                    $(selectedHcrItems).each(function (i, item) {
                        item.IS_DELETED = 'Y';
                        item.UPDATER_ID = $('input[id$=hhdUserID]').val();
                        item.CREATE_DATE = null;    // 시스템에서 날짜값 셋팅하므로 null 설정함
                        item.UPDATE_DATE = null;    // 시스템에서 날짜값 셋팅하므로 null 설정함
                    });
                    fn_InvokeSaveHCR(selectedHcrItems);
                    setTimeout(function () {
                        $("#jsgHcr").jsGrid("loadData");
                    }, 500); 
                }
            });
        }
        catch (ex) {
            fn_showError({ message: ex.message });
        }
    }

    function fn_SetHCRGridBind(dto)
    {
        if (dto != undefined && dto.HCO_CODE != undefined) {
            $("#jsgHcr").jsGrid({
                width: "100%",
                sorting: true,
                paging: false,
                autoload: true,
                //rowDoubleClick: function (args) {
                //    var item = args.item;
                //    fn_ShowContract(item.HCP_CODE);
                //},
                controller: {
                    loadData: function () {

                        var selectUrl = MEDICAL_SERVICE_URL + "/SelectContractHCRList/" + dto.MEDICAL_IDX + "/" +  dto.HCP_CODE ;
                        var d = $.Deferred();
                        selectedHcrItems = [];
                        $.ajax({
                            url: selectUrl,
                            type: "GET",
                            dataType: "json",
                            cache: true,
                            contentType: "application/json; charset=utf-8",
                        }).done(function (response) {
                            d.resolve(response); 
                        }).fail(function (jqXHR, textStatus, errorThrown) {
                            d.reject(jqXHR);
                        });

                        return d.promise();
                    }
                },
                headerTemplate: function () {
                    return $("<button>").attr("type", "button").text("Select All")
                        .on("click", function () {
                            $('input').prop('checked', true);
                        });
                },
                fields: [
                    {
                        name: "HCR_CODE", title: "", align: "center", width: "50px",
                        itemTemplate: function (value, item) {
                            var $hhdSelRows = $("#hhdSelHCRRows");
                            return $("<input>").attr("type", "checkbox").attr("value",value) 
                                .on("change", function() { 
                                    $(this).is(":checked") ? fn_SelectItem(item) : fn_UnselectItem(item); 
                                });
                        }
                    },
                    { name: "HCR_NAME", title: "HCP", type: "text" },
                    { name: "HCO_NAME", title: "HCO", type: "text" },
                    { name: "UPDATE_DATE", title: "Modified", type: "text" },
                    { name: "UPDATER_ID", title: "Modified By", type: "text" },
                    { name: "IS_DELETED", title: "Delete", type: "text" }
                ]
            });
        }
        
    }
}
  