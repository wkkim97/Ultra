var state = {
    now_quarter: null,
    selected_hospital: null,
    selected_hospital_name: null,
    selected_tab: $('#tabEventMaster > li.active').children('a').attr('href').replace('#', '').toLowerCase(),
    selected_q: null,
    selected_micro_marketing_copy: null,
    selected_row: {},
    masterData: {
        equipment: {
            mf_et: [{ MANUFACTURER: 'GE', EQUIPMENT_TYPE: 'Brivo' },
                { MANUFACTURER: 'GE', EQUIPMENT_TYPE: 'BRIVO MR355 1.5T' },
                { MANUFACTURER: 'GE', EQUIPMENT_TYPE: 'Discovery' },
                { MANUFACTURER: 'GE', EQUIPMENT_TYPE: 'Discovery MR750w 3.0T' },
                { MANUFACTURER: 'GE', EQUIPMENT_TYPE: 'LightSpeed' },
                { MANUFACTURER: 'GE', EQUIPMENT_TYPE: 'Optima' },
                { MANUFACTURER: 'GE', EQUIPMENT_TYPE: 'Optima 540' },
                { MANUFACTURER: 'GE', EQUIPMENT_TYPE: 'Revolution' },
                { MANUFACTURER: 'GE', EQUIPMENT_TYPE: 'Revolution Frontier' },
                { MANUFACTURER: 'GE', EQUIPMENT_TYPE: 'Revolution HD' },
                { MANUFACTURER: 'GE', EQUIPMENT_TYPE: 'OPTIMA MR 1.5T Advance' },
                { MANUFACTURER: 'GE', EQUIPMENT_TYPE: 'Signa HDxt 1.5T' },
                { MANUFACTURER: 'GE', EQUIPMENT_TYPE: 'Signa HDxt 3.0T' },
                { MANUFACTURER: 'Philips', EQUIPMENT_TYPE: 'Achieva' },
                { MANUFACTURER: 'Philips', EQUIPMENT_TYPE: 'Brilliance' },
                { MANUFACTURER: 'Philips', EQUIPMENT_TYPE: 'ICT' },
                { MANUFACTURER: 'Philips', EQUIPMENT_TYPE: 'Multiva 1.5T' },
                { MANUFACTURER: 'Philips', EQUIPMENT_TYPE: 'Ingenia 1.5T' },
                { MANUFACTURER: 'Philips', EQUIPMENT_TYPE: 'Ingenia 1.5T CX' },
                { MANUFACTURER: 'Philips', EQUIPMENT_TYPE: 'Ingenia Ambition 1.5T' },
                { MANUFACTURER: 'Philips', EQUIPMENT_TYPE: 'Ingenia Prodiva 1.5T' },
                { MANUFACTURER: 'Philips', EQUIPMENT_TYPE: 'Ingenia 3.0T' },
                { MANUFACTURER: 'Philips', EQUIPMENT_TYPE: 'Ingenia 3.0T CX' },
                { MANUFACTURER: 'Philips', EQUIPMENT_TYPE: 'Ingenia Elition 3.0T' },
                { MANUFACTURER: 'Philips', EQUIPMENT_TYPE: 'Ingenuity' },
                { MANUFACTURER: 'Philips', EQUIPMENT_TYPE: 'IQon Spectral' },
                { MANUFACTURER: 'Philips', EQUIPMENT_TYPE: 'IQon Elite Spectral' },
                { MANUFACTURER: 'Philips', EQUIPMENT_TYPE: 'MX16' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'MAGNETOM Avanto 1.5T' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'MAGNETOM Espree 1.5T' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'MAGNETOM ESSENZA 1.5T' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'MAGNETOM Sempra 1.5T' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'MAGNETOM Skyra 3T' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'MAGNETOM Spectra 3T' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'MAGNETOM Verio 3T' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'MAGNETOM Vida 3T' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'Biograph mMR' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'SOMATOM Definition AS' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'SOMATOM Definition AS+' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'SOMATOM Definition Edge' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'SOMATOM Definition Flash' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'SOMATOM Emotion' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'SOMATOM Perspective' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'SOMATOM Sensation' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'SOMATOM Spirit' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'SOMATOM Defination Flash' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'SOMATOM Drive' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'SOMATOM Force' },
                { MANUFACTURER: 'Siemens', EQUIPMENT_TYPE: 'Trio' },
                { MANUFACTURER: 'Cannon', EQUIPMENT_TYPE: 'Alexion' },
                { MANUFACTURER: 'Cannon', EQUIPMENT_TYPE: 'Alexion Advance' },
                { MANUFACTURER: 'Cannon', EQUIPMENT_TYPE: 'Aquilion ONE Genesis' },
                { MANUFACTURER: 'Cannon', EQUIPMENT_TYPE: 'Aquilion ONE Nature' },
                { MANUFACTURER: 'Cannon', EQUIPMENT_TYPE: 'Aquilion CXL Edition' },
                { MANUFACTURER: 'Cannon', EQUIPMENT_TYPE: 'Aquilion LB' },
                { MANUFACTURER: 'Cannon', EQUIPMENT_TYPE: 'Aquilion Lightning 160' },
                { MANUFACTURER: 'Cannon', EQUIPMENT_TYPE: 'Aquilion Lightning 16' },
                { MANUFACTURER: 'Cannon', EQUIPMENT_TYPE: 'Aquilion ONE' },
                { MANUFACTURER: 'Cannon', EQUIPMENT_TYPE: 'Aquilion PRIME' },
                { MANUFACTURER: 'Cannon', EQUIPMENT_TYPE: 'Aquilion VISION Editon' },
                { MANUFACTURER: 'Cannon', EQUIPMENT_TYPE: 'Vantage Elan' },
                { MANUFACTURER: 'Cannon', EQUIPMENT_TYPE: 'Vantage Titan' },
                { MANUFACTURER: 'Cannon', EQUIPMENT_TYPE: 'Vantage Titan 3T' },
                { MANUFACTURER: 'Cannon', EQUIPMENT_TYPE: 'Vantage Galan 3T' },
                { MANUFACTURER: 'Hitachi', EQUIPMENT_TYPE: 'ECHELON Smart 1.5T' },
                { MANUFACTURER: 'Hitachi', EQUIPMENT_TYPE: 'ECHELON Smart Plus 1.5T' },
                { MANUFACTURER: 'Hitachi', EQUIPMENT_TYPE: 'SCENARIA View' },
                { MANUFACTURER: 'Hitachi', EQUIPMENT_TYPE: 'SCENARIA 28 slice' },
                { MANUFACTURER: 'Hitachi', EQUIPMENT_TYPE: 'Supria Family' },
            ]
        },
        examination: {
            et_st: [{
                EXAMINATION_TYPE: 'XCM',
                SCAN_TYPE: 'CT'
            }, {
                EXAMINATION_TYPE: 'XCM',
                SCAN_TYPE: 'C-Angio'
            }, {
                EXAMINATION_TYPE: 'XCM',
                SCAN_TYPE: 'G-Antio'
            }, {
                EXAMINATION_TYPE: 'MRCM',
                SCAN_TYPE: 'MR'
            }, {
                EXAMINATION_TYPE: 'MRCM',
                SCAN_TYPE: 'MR_Liver'
            }]
        }
    }
};
var CONST = {
    marketShareSubcategory: {
        MRCM: ["Liver MRI", "MRI"],
        XCM: ["Cardiac Angio", "CT", "General Angio", "Others"]
    }
}

function waitMe() {
    $.ajaxSetup({
        beforeSend: function (xhr) {
            $('body').waitMe({
                effect: 'win8',
                text: 'Please wait...'
            });
        },
        complete: function () {
            $('body').waitMe("hide");
        },
    });
}

$(function () {
    waitMe();
    loadPlugin();
    hospitalData()
        .done(function (data) {
            renderHospital(data);
        })
        .fail(function (error) {
            console.log(error);
        });
});
//initialize plugin , render, event binding
function loadPlugin() {
    // plug-in datetime picker
    $('.form_dateTime').datetimepicker({
        weekStart: 0,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        forceParse: 0,
        minuteStep: 60,
        minView: 2,
        maxView: 2,
        format: "yyyy-mm-dd"
    });

    //input[type=number]는 오직 숫자값만 입력 받도록 한다
    var tags = document.querySelectorAll("input[type='number']");
    var restrictNumber = function (e) {
        var newValue = this.value.replace(/\D/gi, "");
        this.value = newValue;
    }
    for (i = 0; i < tags.length; i++) {
        tags[i].addEventListener('input', restrictNumber);
    }

    //속성 number-comma=true 인 경우 천단위 구분표시를 삽입한다
    tags = document.querySelectorAll("*[number-comma='true']");
    for (i = 0; i < tags.length; i++) {
        tags[i].textContent = numberWithCommas(tags[i].textContent);
    }

    //분기 select option을 생성한다
    var date = new Date();
    var year = date.getFullYear();
    var month = date.getMonth();
    var quarter = parseInt(month / 3);
    var optionDOM = '';
    var i, j;

    for (i = quarter; i > 0; i--) {
        var val = year + '-' + i + 'Q';
        optionDOM += '<option value="' + val + '">' + val + '</option>';
    }
    //분기 option을 2017년까지 생성한다
    for (i = year - 1; i >= 2017; i--) {
        for (j = 4; j >= 1; j--) {
            var val = i + '-' + j + 'Q';
            optionDOM += '<option value="' + val + '">' + val + '</option>';
        }
    }
    $('#selQuarter').html(optionDOM);
    state.now_quarter = $('#selQuarter option:first').val();

    //new button 이벤트 바인딩
    $('.btnNew').click(function () {
        state.selected_row[state.selected_tab] = null;
        $(this).closest('.panel-body').find('input, textarea').val(null);
        $(this).closest('.panel-body').find('select').find('option:first').prop('selected', true);
    });

    //delete button 이벤트 바인딩
    $('.btnDelete').click(function () {
        var data = state.selected_row[state.selected_tab] ? state.selected_row[state.selected_tab] : null;
        if (!state.selected_row[state.selected_tab]) {
            
            fn_showWarning({
                title: "confirm",
                message: "데이타를 선택해 주세요",
            });
            return false;
        }
        //if (!confirm('해당 데이터를 삭제하시겠습니까?')) return false;
        fn_confirm({
            title: "Confirm",
            message: "해당 데이터를 삭제하시겠습니까?"
        }).done(function (result) {
            if (result) {
                data = { id: data['ID'] }
                if (state.selected_tab == 'equipment') {
                    url = RADIOLOGY_SERVICE_URL + "/DeleteEquipment";
                } else if (state.selected_tab == 'examination') {
                    url = RADIOLOGY_SERVICE_URL + "/DeleteExamination";
                } else if (state.selected_tab == 'market_share') {
                    url = RADIOLOGY_SERVICE_URL + "/DeleteMarketShare";
                }

                $.ajax({
                    url: url,
                    type: "POST",
                    dataType: "json",
                    data: JSON.stringify(data),
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                    success: function (result) {
                        loadGrid();
                    },
                    error: function (error) {
                        console.log(error)
                        fn_showError({
                            message: error.responseText
                        });
                    },
                });
            }
        });
    });

    //save button 이벤트 바인딩
    $('.btnSave').click(function () {
        var inputs = $('#' + state.selected_tab + '-table').find('input, textarea, select');
        var data = state.selected_row[state.selected_tab] ? state.selected_row[state.selected_tab] : {};
        var url;

        //생성일 경우
        if (!state.selected_row[state.selected_tab]) {
            data['CREATOR_ID'] = $('#cpHolderBottom_hhdUserID').val();
            data['USER_ID'] = $('#cpHolderBottom_hhdUserID').val();
            data['USER_NAME'] = $('#cpHolderBottom_hhdUserName').val();
            data['ORGANIZATION_ID'] = state.selected_hospital;
            data['ORGANIZATION_NAME'] = state.selected_hospital_name;
        }

        if (state.selected_tab == 'equipment') {

            url = RADIOLOGY_SERVICE_URL + "/MergeEquipment";
            for (i = 0; i < inputs.length; i++) {
                data[$(inputs[i]).attr('name')] = $(inputs[i]).val();
            }

        } else if (state.selected_tab == 'examination') {

            url = RADIOLOGY_SERVICE_URL + "/MergeExamination";
            for (i = 0; i < inputs.length; i++) {
                data[$(inputs[i]).attr('name')] = $(inputs[i]).val();
            }
            //새로 생성일 경우
            if (!state.selected_row[state.selected_tab]) {
                data['CURRENT_QUARTER'] = state.selected_q;
                data['NEW_QUATER'] = "";
            //수정일 경우
            } else {
                data['CURRENT_QUARTER'] = data['QUARTER'] + '';
            }

        } else if (state.selected_tab == 'market_share') {

            url = RADIOLOGY_SERVICE_URL + "/MergeMarketShare";
            for (i = 0; i < inputs.length; i++) {
                data[$(inputs[i]).attr('name')] = $(inputs[i]).val();
            }
            //새로 생성일 경우
            if (!state.selected_row[state.selected_tab]) {
                data['CURRENT_QUARTER'] = state.selected_q;
                data['NEW_QUATER'] = "";
            //수정일 경우
            } else {
                data['CURRENT_QUARTER'] = data['QUARTER'] + '';
            }
            
        }

        delete data['QUARTER'];
        delete data['COPY_TO'];

        mergeData(url, data);
    });

    //total = quantity * total
    $('#ms_price, #ms_quantity').on('input', function () {
        $('#ms_total').val(numberWithCommas((parseInt($('#ms_price').val().replace(/\,/g, '')) || 0) * (parseInt($('#ms_quantity').val().replace(/\,/g, '')) || 0) ));
    });
    $('#ms_modal_price, #ms_modal_quantity').on('input', function () {
        $('#ms_modal_total').val(numberWithCommas((parseInt($('#ms_modal_price').val().replace(/\,/g, '')) || 0) * (parseInt($('#ms_modal_quantity').val().replace(/\,/g, '')) || 0)));
    });
    //Care relation = cases / enhanced cases * 100
    $('#ex_number_of_cases, #ex_number_of_enhanced_cases').on('input', function () {
        if ($('#ex_number_of_cases').val()) $('#ex_case_relation').val(((($('#ex_number_of_enhanced_cases').val() || 0) / ($('#ex_number_of_cases').val() || 0)) * 100 || 0).toFixed(0));
        else $('#ex_case_relation').val(null);
    });
    $('#ex_modal_number_of_cases, #ex_modal_number_of_enhanced_cases').on('input', function () {
        if ($('#ex_modal_number_of_cases').val()) $('#ex_modal_case_relation').val(((($('#ex_modal_number_of_enhanced_cases').val() || 0) / ($('#ex_modal_number_of_cases').val() || 0)) * 100 || 0).toFixed(0));
        else $('#ex_modal_case_relation').val(null);
    });
}
function mergeData(url, data, cb) {
    // validation
    var valid = true;
    var message = '';
    if (state.selected_tab == 'equipment') {
        if (!data['CATEGORY']) {
            alert("Category을(를) 입력해주세요");
            return false;
        } else if (!data['MANUFACTURER']) {
            alert("Manufacture을(를) 입력해주세요");
            return false;
        } else if (!data['SCANNER_MODEL_TYPE']) {
            alert("Scanner model type을(를) 입력해주세요");
            return false;
        } else if (!data['STATUS']) {
            alert("Status을(를) 입력해주세요");
            return false;
        } else if (!data['INJECTOR_MANUFACTURER']) {
            alert("Injector manufacturer을(를) 입력해주세요");
            return false;
        } else if (!data['EQUIPMENT_TYPE']) {
            alert("Equipment type을(를) 입력해주세요");
            return false;
        } else if (!data['DATE_OF_INSTALLATION']) {
            alert("Date of installation을(를) 입력해주세요");
            return false;
        }
    } else if (state.selected_tab == 'examination') {
        
        if (!data['EXAMINATION_TYPE']) {
            alert("Examination type을(를) 입력해주세요");
            return false;
        } else if (!data['SCAN_TYPE']) {
            alert("Scan type을(를) 입력해주세요");
            return false;
        } else if (!data['NUMBER_OF_CASES']) {
            alert("Number of cases을(를) 입력해주세요");
            return false;
        } else if (!data['NUMBER_OF_ENHANCED_CASES']) {
            alert("Number of enhanced cases을(를) 입력해주세요");
            return false;
        }

        data.NUMBER_OF_CASES = data.NUMBER_OF_CASES || 0;
        data.NUMBER_OF_ENHANCED_CASES = data.NUMBER_OF_ENHANCED_CASES || 0;
        data.CASE_RELATION = data.CASE_RELATION || 0;

    } else if (state.selected_tab == 'market_share') {
        
        if (!data['PRODUCT_FAMAILY']) {
            message = "Product Family을(를) 입력해주세요";
            valid = false;
        } else if (!data['PRODUCT']) {
            message = "Product을(를) 입력해주세요";
            valid = false;
        } else if (!data['QUANTITY']) {
            message = "Quantity을(를) 입력해주세요";
            valid = false;
        }
        
        data.PRICE = parseInt(data.PRICE.replace(/\D/gi, "")) || 0;
        data.QUANTITY = parseInt(data.QUANTITY.replace(/\D/gi, "")) || 0;
        data.SALES = data.PRICE * data.QUANTITY;
    }

    if (!valid) {
        fn_showError({
            message: message
        });
        return false;
    }

    $.ajax({
        url: url,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (result) {
            loadGrid();
            if (cb && typeof cb == 'function') cb();
        },
        error: function (error) {
            console.log(error)
            fn_showError({
                message: error.responseText
            });
        },
    });
}
function numberWithCommas(x) {
    if (!x) return 0;
    var val = x;
    if (typeof x == 'string') val = x.replace(/\D/gi, "");    
    var num = parseInt(val);
    return num.toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
}

function fn_DetailView(obj) {
    // 해당 Row dblclick 이벤트 발생
    $(obj).dblclick();
}

//병원 데이터 불러오기
function hospitalData() {

    //**
    // Ajax
    //**
    var d = $.Deferred();
    var data = {
        user_id: $("#cpHolderBottom_hhdUserID").val(),
        user_type: $('#cpHolderBottom_userType').val()
    }
    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/SelectHospitalList",
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            d.resolve(data);
        },
        error: function (error) {
            d.reject(error)
            fn_showError({
                message: error.responseText
            });
        },
    });

    return d.promise();
}
//병원 리스트 생성
function renderHospital(data) {
    var optionDOM = '<option disabled selected>병원선택</option>';
    for (i in data) {
        optionDOM += "<option data-name='" + data[i].INS_NAME +"' value='" + data[i].ORGANIZATION_ID + "'>" + data[i].INS_NAME + "(" + data[i].B_STATE + ")</option>"
    }
    $('#selHospital').html(optionDOM);

    $('#selHospital').change(function (event) {
        state.selected_hospital = $('#selHospital').val();
        state.selected_hospital_name = $('#selHospital option:selected').attr('data-name');
        loadGrid();
    });
}
//장비 데이터 불러오기
function equipmentData() {
    var d = $.Deferred();
    var result = {
        data: [],
        rowDoubleClick: function (args) {
            $('#eq_category').val(args.item.CATEGORY);
            $('#eq_scanner_model_type').val(args.item.SCANNER_MODEL_TYPE);
            $('#eq_status').val(args.item.STATUS);

            renderEquipmentManufacturerType(state.masterData['equipment'], null, function () {
                $('#eq_manufacturer').val(args.item.MANUFACTURER).trigger('change');
                $('#eq_equipment_type').val(args.item.EQUIPMENT_TYPE).trigger('change');
            });
            $('#eq_injector_manufacturer').val(args.item.INJECTOR_MANUFACTURER);
            

            $('#eq_date_of_installation').val(args.item.DATE_OF_INSTALLATION);
            
            $('#eq_memo').val(args.item.MEMO);
            state.selected_row[state.selected_tab] = args.item;
            removeSelection();
        },
        fields: [
            { name: "CATEGORY", title: "Category", type: "text" },
            //{ name: "MANUFACTURER", title: "Manufacture", type: "text" },
            {
                name: "MANUFACTURER", title: "Manufacture", type: "text",
                itemTemplate: function (value, item) {
                    return "<a href='#' class='link text-ellipsis' onclick='fn_DetailView(this);'>" + value + "</a>";
                }
            },
            { name: "SCANNER_MODEL_TYPE", title: "Scanner model type", type: "text" },
            { name: "STATUS", title: "Status", type: "text" },
            
            { name: "INJECTOR_MANUFACTURER", title: "Injector manufacturer", type: "text" },
            { name: "EQUIPMENT_TYPE", title: "Equipment type", type: "text" },

            { name: "DATE_OF_INSTALLATION", title: "Date of installatione", type: "text" },
            
            
        ]
    };
    var data = {
        organization_id: state.selected_hospital
    }
    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/SelectEquipment",
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            result.data = data;
            d.resolve(result);
        },
        error: function (error) {
            d.reject(error)
            fn_showError({
                message: error.responseText
            });
        },
    });

    return d.promise();
}

//시험 데이터 불러오기
function examinationData() {

    var d = $.Deferred();
    var result = {
        data: [],
        rowDoubleClick: function (args) {
            renderExaminationScanType(state.masterData['examination'], null, function () {
                $('#ex_examination_type').val(args.item.EXAMINATION_TYPE).trigger('change');
                $('#ex_scan_type').val(args.item.SCAN_TYPE).trigger('change');
            });
            $('#ex_examination_type').val(args.item.EXAMINATION_TYPE);
            $('#ex_scan_type').val(args.item.SCAN_TYPE);
            $('#ex_number_of_cases').val(args.item.NUMBER_OF_CASES);
            $('#ex_number_of_enhanced_cases').val(args.item.NUMBER_OF_ENHANCED_CASES);
            $('#ex_case_relation').val(args.item.CASE_RELATION);
            $('#ex_comment').val(args.item.COMMENT);
            state.selected_row[state.selected_tab] = args.item;
            removeSelection();
        },
        fields: [
            //{ name: "EXAMINATION_TYPE", title: "Examination type", type: "text" },
            {
                name: "EXAMINATION_TYPE", title: "Examination type", type: "text",
                itemTemplate: function (value, item) {
                    return "<a href='#' class='link text-ellipsis' onclick='fn_DetailView(this);'>" + value + "</a>";
                }
            },
            { name: "SCAN_TYPE", title: "Scan type", type: "text" },
            { name: "NUMBER_OF_CASES", title: "Number of cases", type: "text" },
            { name: "NUMBER_OF_ENHANCED_CASES", title: "Number of enhaced cases", type: "text" },
            { name: "CASE_RELATION", title: "Case relation", type: "text" },
            {
                type: "control", width: 50, editButton: false, deleteButton: false,
                itemTemplate: function (value, item) {
                    var $result = jsGrid.fields.control.prototype.itemTemplate.apply(this, arguments);

                    var $customEditButton = $("<button>").text('copy').attr({ class: "btn btn-sm btn-info" + (item.COPY_TO || state.now_quarter == state.selected_q ? 'disabled' : '') })
                        .click(function (e) {
                            if (item.COPY_TO) {
                                //alert('이미 다음 분기로 복사된 데이터입니다');
                                fn_showWarning({
                                    title: "confirm",
                                    message: '이미 다음 분기로 복사된 데이터입니다',
                                });
                                return false;
                            } else if (state.now_quarter == state.selected_q) {
                                //alert('현재 분기의 데이터는 복사할 수 없습니다');

                                fn_showWarning({
                                    title: "confirm",
                                    message: '현재 분기의 데이터는 복사할 수 없습니다',
                                });
                                return false;
                            }
                            state.selected_micro_marketing_copy = item;
                            renderExaminationScanType(state.masterData['examination'], 'modal', function () {
                                $('#ex_modal_examination_type').val(item.EXAMINATION_TYPE).trigger('change');
                                $('#ex_modal_scan_type').val(item.SCAN_TYPE).trigger('change');
                            });
                            $('#ex_modal_examination_type').val(item.EXAMINATION_TYPE);
                            $('#ex_modal_scan_type').val(item.SCAN_TYPE);
                            $('#ex_modal_number_of_cases').val(item.NUMBER_OF_CASES);
                            $('#ex_modal_number_of_enhanced_cases').val(item.NUMBER_OF_ENHANCED_CASES);
                            $('#ex_modal_case_relation').val(item.CASE_RELATION);
                            $('#ex_modal_enhanced_cases_per_year').val(item.ENHANCED_CASES_PER_YEAR);
                            $('#ex_modal_comment').val(item.COMMENT);

                            $('#contentModal .modalTab').addClass('hidden');
                            $('#exModal').removeClass('hidden');
                            $('#contentModal').modal();
                            e.stopPropagation();
                            return false;
                        });
                    return $("<div>").append($customEditButton);
                    //return $result.add($customButton);
                }
            }
        ]
    }

    var data = {
        id : "",
        organization_id: state.selected_hospital,
        quarter : state.selected_q
    }
    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/SelectExamination",
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            result.data = data;
            d.resolve(result);
        },
        error: function (error) {
            d.reject(error)
            fn_showError({
                message: error.responseText
            });
        },
    });

    return d.promise();
    
}

//마켓 데이터 불러오기
function market_shareData() {
    var d = $.Deferred();
    var result = {
        data: [],
        rowDoubleClick: function (args) {

            $('#ms_segment').val(args.item.SEGMENT);
            $('#ms_manufacturer').val(args.item.MANUFACTURER);

            //$('#ms_product_famaily').val(args.item.PRODUCT_FAMAILY);
            //$('#ms_product').val(args.item.PRODUCT)
            //$('#ms_subsegment').val(args.item.SUB_CATEGORY);
            renderMarketShareProduct(state.masterData['market_share'], null, function () {
                $('#ms_famaily').val(args.item.PRODUCT_FAMAILY).trigger('change');
                $('#ms_product').val(args.item.PRODUCT).trigger('change')
                $('#ms_subsegment').val(args.item.SUB_CATEGORY);
            });

            $('#ms_price').val(args.item.PRICE);
            $('#ms_quantity').val(args.item.QUANTITY);
            $('#ms_total').val(args.item.SALES);
            $('#ms_comment').val(args.item.COMMENT);
            state.selected_row[state.selected_tab] = args.item;
            removeSelection();
        },
        fields: [
            { name: "SEGMENT", title: "Segment", type: "text" },
            { name: "SUB_CATEGORY", title: "Sub category", type: "text" },
            { name: "MANUFACTURER", title: "Manufacturer", type: "text" },

            { name: "PRODUCT_FAMAILY", title: "Product family", type: "text" },
            //{ name: "PRODUCT", title: "Product", type: "text" },
            {
                name: "PRODUCT", title: "Product", type: "text",
                itemTemplate: function (value, item) {
                    return "<a href='#' class='link text-ellipsis' onclick='fn_DetailView(this);'>" + value + "</a>";
                }
            },
            { name: "PRICE", title: "Price", type: "text" },
            { name: "QUANTITY", title: "Quantity", type: "text" },
            { name: "SALES", title: "Total", type: "text" },
            {
                type: "control", width: 50, editButton: false, deleteButton: false,
                itemTemplate: function (value, item) {
                    var $result = jsGrid.fields.control.prototype.itemTemplate.apply(this, arguments);

                    var $customEditButton = $("<button>").text('copy').attr({ class: "btn btn-sm btn-info" + (item.COPY_TO || state.now_quarter == state.selected_q ? 'disabled' : '') })
                        .click(function (e) {
                            if (item.COPY_TO) {
                                //alert('이미 다음 분기로 복사된 데이터입니다');
                                fn_showWarning({
                                    title: "confirm",
                                    message: '이미 다음 분기로 복사된 데이터입니다',
                                });
                                
                                return false;
                            } else if (state.now_quarter == state.selected_q) {
                                //alert('현재 분기의 데이터는 복사할 수 없습니다');
                                fn_showWarning({
                                    title: "confirm",
                                    message: '현재 분기의 데이터는 복사할 수 없습니다',
                                });
                                return false;
                            }
                            state.selected_micro_marketing_copy = item;

                            renderMarketShareProduct(state.masterData['market_share'], 'modal', function () {
                                $('#ms_modal_family').val(item.PRODUCT_FAMAILY).trigger('change');
                                $('#ms_modal_product').val(item.PRODUCT).trigger('change')
                                $('#ms_modal_subsegment').val(item.SUB_CATEGORY);
                            });

                            $('#ms_modal_segment').val(item.SEGMENT);
                            $('#ms_modal_manufacturer').val(item.MANUFACTURER);
                            $('#ms_modal_product_code').val(item.PRODUCT_CODE);
                            $('#ms_modal_price').val(item.PRICE);
                            $('#ms_modal_quantity').val(item.QUANTITY);
                            $('#ms_modal_total').val(item.SALES);
                            $('#ms_modal_date').val(item.DATE);
                            $('#ms_modal_comment').val(item.COMMENT);

                            $('#contentModal .modalTab').addClass('hidden');
                            $('#msModal').removeClass('hidden');
                            $('#contentModal').modal();
                            e.stopPropagation();
                            return false;
                        });
                    return $("<div>").append($customEditButton);
                    //return $result.add($customButton);
                }
            }
        ]
    }
    var data = {
        organization_id: state.selected_hospital,
        quarter: state.selected_q
    }
    $.ajax({
        url: RADIOLOGY_SERVICE_URL + "/SelectMarketShare",
        type: "POST",
        dataType: "json",
        data: JSON.stringify(data),
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            result.data = data;
            for (i in result.data) {
                result.data[i].PRICE = numberWithCommas(result.data[i].PRICE);
                result.data[i].QUANTITY = numberWithCommas(result.data[i].QUANTITY);
                result.data[i].SALES = numberWithCommas(result.data[i].SALES);
            }
            d.resolve(result);
        },
        error: function (error) {
            d.reject(error)
            fn_showError({
                message: error.responseText
            });
        },
    });

    return d.promise();
}

//탭 선택 이벤트 바인딩
function handleTabSelected(target, e) {
    $(e).off("shown.bs.tab");
    $(e).on('shown.bs.tab', function (e) {
        state.selected_tab = target;
        loadGrid();
    });
    $('#subtitle').text(target.charAt(0).toUpperCase() + target.slice(1));

    //병원 분기 설정 변경
    if (target == 'equipment') {
        $('.sel-wrapper').addClass('equipment');
    } else {
        $('.sel-wrapper').removeClass('equipment');
    }
}

//데이터 표 그리기
function loadGrid() {
    //form 초기화
    $('.panel-body').find('input, textarea').val(null);
    $('.panel-body').find('select').find('option:first').prop('selected', true);
    state.selected_row = {};

    if (!state.selected_hospital) {

        //alert('조회할 병원을 선택해 주세요');
        fn_showWarning({
            title: "confirm",
            message: '조회할 병원을 선택해 주세요',
        });
        return false;
    }
    if (!state.selected_tab) {
        //alert('조회할 정보를 선택해 주세요');
        fn_showWarning({
            title: "confirm",
            message: '조회할 정보를 선택해 주세요',
        });
        return false;
    }

    state.selected_q = $('#selQuarter').val();

    if ($("#JsGrid_" + state.selected_tab).jsGrid) $("#JsGrid_" + state.selected_tab).jsGrid("destroy");

    var fnStr = state.selected_tab + 'Data()';

    var cb = function () {
        eval(fnStr).done(function (item) {
            showNoData(item.data.length);
            $("#JsGrid_" + state.selected_tab).jsGrid({
                width: "100%",
                sorting: true,
                paging: true,
                pageSize: GRID_LIST_COUNT,
                autoload: true,
                rowDoubleClick: item.rowDoubleClick,
                data: item.data,
                fields: item.fields
            });
        });
    }

    loadMasterData(cb);
    //$('.selected-hospital').text(state.selected_hospital.HOSPITAL_NAME);
}
//분기 변경 이벤트
function changeQuarter(val) {
    loadGrid();
}

function showNoData(length) {
    if (true) {
        $('.no-data').addClass('hidden');
        $('.has-data').removeClass('hidden');
    } else {
        $('.no-data').removeClass('hidden');
        $('.has-data').addClass('hidden');
    }
}
//micro marketing modal save
function copyExamination() {

    var inputs = $('#exModal').find('input, textarea, select');
    var data = {};
    var url = RADIOLOGY_SERVICE_URL + "/MergeExamination";
    for (i = 0; i < inputs.length; i++) {
        data[$(inputs[i]).attr('name')] = $(inputs[i]).val();
    }
    data['ID'] = state.selected_micro_marketing_copy.ID;
    data['CREATOR_ID'] = $('#cpHolderBottom_hhdUserID').val();
    data['USER_ID'] = $('#cpHolderBottom_hhdUserID').val();
    data['USER_NAME'] = $('#cpHolderBottom_hhdUserName').val();
    data['ORGANIZATION_ID'] = state.selected_hospital;
    data['ORGANIZATION_NAME'] = state.selected_hospital_name;
    data['CURRENT_QUARTER'] = state.selected_q;
    data['NEW_QUATER'] = nextQuarter(state.selected_q);

    var callback = function () {
        //$('#exModal').modal('hide');
        $('#exModal').find('.close').click();
    }

    mergeData(url, data, callback);
    
}
function copyMarketShare() {

    var inputs = $('#msModal').find('input, textarea, select');
    var data = {};
    var url = RADIOLOGY_SERVICE_URL + "/MergeMarketShare";
    for (i = 0; i < inputs.length; i++) {
        data[$(inputs[i]).attr('name')] = $(inputs[i]).val();
    }
    data['ID'] = state.selected_micro_marketing_copy.ID;
    data['CREATOR_ID'] = $('#cpHolderBottom_hhdUserID').val();
    data['USER_ID'] = $('#cpHolderBottom_hhdUserID').val();
    data['USER_NAME'] = $('#cpHolderBottom_hhdUserName').val();
    data['ORGANIZATION_ID'] = state.selected_hospital;
    data['ORGANIZATION_NAME'] = state.selected_hospital_name;
    data['CURRENT_QUARTER'] = state.selected_q;
    data['NEW_QUATER'] = nextQuarter(state.selected_q);

    var callback = function () {
        //$('#exModal').modal('hide');
        $('#msModal').find('.close').click();
    }

    mergeData(url, data, callback);
}
//remove text selection
function removeSelection() {
    var sel = window.getSelection ? window.getSelection() : document.selection;
    if (sel) {
        if (sel.removeAllRanges) {
            sel.removeAllRanges();
        } else if (sel.empty) {
            sel.empty();
        }
    }
}
//Calculate next quarter
function nextQuarter(current) {
    var q_arr = current.split('-');
    var q = parseInt(q_arr[1]);
    var year = parseInt(q_arr[0]);
    q++;
    if (q == 5) {
        q = 1;
        year++;
    }
    return year + '-' + q + 'Q';
}
//Load Master data
function loadMasterData(cb) {
    if (state.selected_tab == 'market_share' && !state.masterData['market_share']) {
        var selectUrl = RADIOLOGY_SERVICE_URL + "/SelectSearchMasterMarketShare";
        var data = {};

        if ($("#ms_famaily").val()) data.family = $("#ms_famaily").val();
        if ($("#ms_product").val()) data.product = $("#ms_product").val();

        $.ajax({
            url: selectUrl,
            type: "POST",
            data: JSON.stringify(data),
            dataType: "json",
            asyn: false,
            contentType: "application/json; charset=utf-8",
            success: function (returnData) {
                renderMarketShareProduct(returnData);
                if (cb) cb();
            }
        });
    } else if (state.selected_tab == 'examination' && state.masterData['examination'] != null) {
        renderExaminationScanType(state.masterData['examination']);
        if (cb) cb();
    } else if (state.selected_tab == 'equipment' && state.masterData['equipment'] != null) {
        renderEquipmentManufacturerType(state.masterData['equipment']);
        if (cb) cb();
    } else {
        if (cb) cb();
    }
}
//Market share master data rendering & 이벤트 바인딩
function renderMarketShareProduct(returnData, modal, cb) {
    var familyArr = {};
    var familyHtml = "<option value='reset' selected>전체</option>";
    var productHtml = "<option disabled selected>선택</option>";
    var toSortArr = [];
    returnData.sort(function (a, b) {
        var caseA = a.PRODUCT_FAMILY.trim().toLowerCase();
        var caseB = b.PRODUCT_FAMILY.trim().toLowerCase();
        if (caseA > caseB) return 1;
        else if (caseA == caseB) return 0;
        else return -1;
    });
    for (i in returnData) {
        state.masterData['market_share'] = returnData;
        //Listing Product family
        var obj = {};
        var data = returnData[i];
        obj.price = data.PRICE;
        obj.segment = data.SEGMENT;
        obj.manufacture = data.MANUFACTURE;
        obj.product = data.PRODUCT;

        if (familyArr[data.PRODUCT_FAMILY]) {
            familyArr[data.PRODUCT_FAMILY].push(obj);
        }
        else {
            familyArr[data.PRODUCT_FAMILY] = [obj];
        }
        //Listing Product
        productHtml += "<option value='" + data.PRODUCT + "'>" + data.PRODUCT + "</option>";
    }
    toSortArr = [];
    for (key in familyArr) {
        toSortArr.push(key);
    }
    toSortArr.sort();
    for (i in toSortArr) {
        familyHtml += "<option value='" + toSortArr[i] + "'>" + toSortArr[i] + "</option>";
    }
    var modalStr = '';
    if (modal) modalStr = 'modal_';

    $('#ms_' + modalStr + 'famaily').html(familyHtml);
    $('#ms_' + modalStr + 'product').html(productHtml);
    $('#ms_' + modalStr + 'famaily').off().on('change', function (e) {
        var val = $(e.target).val();
        //product 변경
        if (val != 'reset') {
            var newProductHtml = "<option disabled selected>선택</option>";;
            for (i in familyArr[val]) {
                var data = familyArr[val][i];
                newProductHtml += "<option value='" + data.product + "'>" + data.product + "</option>";
            }

            $('#ms_' + modalStr + 'product').html(newProductHtml);
            // product 초기화
        } else {
            $('#ms_' + modalStr + 'product').html(productHtml);
        }

        $('#ms_' + modalStr + 'segment').val(null);
        $('#ms_' + modalStr + 'manufacturer').val(null);
        $('#ms_' + modalStr + 'price').val(null);
        $('#ms_' + modalStr + 'quantity').val(null);
        $('#ms_' + modalStr + 'total').val(null);

        $('#ms_' + modalStr + 'subsegment').html("<option disabled selected>선택</option>");
    });
    $('#ms_' + modalStr + 'product').off().on('change', function (e) {
        var val = $(e.target).val();
        var segment;
        var newSubcategoryHtml = "<option disabled selected>선택</option>";

        for (i in state.masterData['market_share']) {
            var data = state.masterData['market_share'][i];
            if (data.PRODUCT == val) {
                $('#ms_' + modalStr + 'famaily').val(data.PRODUCT_FAMILY);
                $('#ms_' + modalStr + 'segment').val(data.SEGMENT);
                $('#ms_' + modalStr + 'manufacturer').val(data.MANUFACTURE);
                $('#ms_' + modalStr + 'price').val(numberWithCommas(data.PRICE));
                segment = data.SEGMENT;
                break;
            }
        }

        for (i in CONST.marketShareSubcategory[segment]) {
            var subcategoryArr = CONST.marketShareSubcategory[segment];
            newSubcategoryHtml += "<option value='" + subcategoryArr[i] + "'>" + subcategoryArr[i] + "</option>";
        }
        $('#ms_' + modalStr + 'subsegment').html(newSubcategoryHtml);

    });

    if (cb) cb();

}
function renderExaminationScanType(returnData, modal, cb) {
    var exArr = [];
    var exHtml = "<option value='reset' selected>전체</option>";
    var scanHtml = "<option disabled selected>선택</option>";
    for (i in returnData.et_st) {
        var obj = {};
        var data = returnData.et_st[i];
        obj.SCAN_TYPE = data.SCAN_TYPE;

        if (exArr[data.EXAMINATION_TYPE]) {
            exArr[data.EXAMINATION_TYPE].push(obj);
        }
        else {
            exArr[data.EXAMINATION_TYPE] = [obj];
        }
        //Listing ex type
        var data = returnData.et_st[i];
        //Listing Product
        scanHtml += "<option value='" + data.SCAN_TYPE + "'>" + data.SCAN_TYPE + "</option>";
    }
    for (key in exArr) {
        exHtml += "<option value='" + key + "'>" + key + "</option>";
    }
    var modalStr = '';
    if (modal) modalStr = 'modal_';
    $('#ex_' + modalStr + 'examination_type').html(exHtml);
    $('#ex_' + modalStr + 'scan_type').html(scanHtml);
    $('#ex_' + modalStr + 'examination_type').off().on('change', function (e) {
        var val = $(e.target).val();
        //product 변경
        if (val != 'reset') {
            var newScanHtml = "<option disabled selected>선택</option>";
            for (i in returnData.et_st) {
                if (returnData.et_st[i].EXAMINATION_TYPE == val) {
                    var data = returnData.et_st[i];
                    newScanHtml += "<option value='" + data.SCAN_TYPE + "'>" + data.SCAN_TYPE + "</option>";
                }
            }

            $('#ex_' + modalStr + 'scan_type').html(newScanHtml);
            // product 초기화
        } else {
            $('#ex_' + modalStr + 'scan_type').html(scanHtml);
        }
    });
    $('#ex_' + modalStr + 'scan_type').off().on('change', function (e) {
        var val = $(e.target).val();

        for (i in returnData.et_st) {
            if (returnData.et_st[i].SCAN_TYPE == val) {
                var data = returnData.et_st[i];
                $('#ex_' + modalStr + 'examination_type').val(data.EXAMINATION_TYPE);
                break;
            }
        }

    });

    if (cb) cb();
}

function renderEquipmentManufacturerType(returnData, modal, cb) {
    var mfArr = [];
    var mfHtml = "<option value='reset' selected>전체</option>";
    var etHtml = "<option disabled selected>선택</option>";
    for (i in returnData.mf_et) {
        var obj = {};
        var data = returnData.mf_et[i];
        obj.EQUIPMENT_TYPE = data.EQUIPMENT_TYPE;

        if (mfArr[data.MANUFACTURER]) {
            mfArr[data.MANUFACTURER].push(obj);
        }
        else {
            mfArr[data.MANUFACTURER] = [obj];
        }
        //Listing ex type
        var data = returnData.mf_et[i];
        //Listing Product
        etHtml += "<option value='" + data.EQUIPMENT_TYPE + "'>" + data.EQUIPMENT_TYPE + "</option>";
    }
    for (key in mfArr) {
        mfHtml += "<option value='" + key + "'>" + key + "</option>";
    }
    var modalStr = '';
    if (modal) modalStr = 'modal_';
    $('#eq_' + modalStr + 'manufacturer').html(mfHtml);
    $('#eq_' + modalStr + 'equipment_type').html(etHtml);
    $('#eq_' + modalStr + 'manufacturer').off().on('change', function (e) {
        var val = $(e.target).val();
        //product 변경
        if (val != 'reset') {
            var newEtHtml = "<option disabled selected>선택</option>";
            for (i in returnData.mf_et) {
                if (returnData.mf_et[i].MANUFACTURER == val) {
                    var data = returnData.mf_et[i];
                    newEtHtml += "<option value='" + data.EQUIPMENT_TYPE + "'>" + data.EQUIPMENT_TYPE + "</option>";
                }
            }

            $('#eq_' + modalStr + 'equipment_type').html(newEtHtml);
            // product 초기화
        } else {
            $('#eq_' + modalStr + 'equipment_type').html(etHtml);
        }
    });
    $('#eq_' + modalStr + 'equipment_type').off().on('change', function (e) {
        var val = $(e.target).val();

        for (i in returnData.mf_et) {
            if (returnData.mf_et[i].EQUIPMENT_TYPE == val) {
                var data = returnData.mf_et[i];
                $('#eq_' + modalStr + 'manufacturer').val(data.MANUFACTURER);
                break;
            }
        }

    });

    if (cb) cb();
}