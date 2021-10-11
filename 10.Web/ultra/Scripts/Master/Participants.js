﻿$(function () {

    /* clinical related meeting에서 contract 조회 */
    $("#tabAddParticipant a[data-toggle='tab']").on('shown.bs.tab', function (e) {
        var tabText = e.target.innerText;

        if (tabText == "Contract") {
            var impactNo = getMedicalSearcher();
            var processID = $('input[id$=hddProcessID]').val();
            var param = {
                impactNo: impactNo,
                processID: processID
            }
            if (impactNo && impactNo.length > 0) {
                $.ajax({
                    url: EVENT_SERVICE_URL + "/SelectParticipantsByContract",
                    type: "POST",
                    data: JSON.stringify(param),
                    dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        displayContractResult(data);
                    },
                    error: function (error) {
                        fn_showError({
                            message: error.responseText
                        });
                    },
                });
            }
        }
    });

    /* Participants 목록조회 */
    $("#jsGridPraticipants").jsGrid({
        height: "900px",
        width: "100%",

        sorting: true,
        paging: false,
        autoload: true,

        controller: {
            loadData: function () {
                var processID = $('input[id$=hddProcessID]').val();
                var d = $.Deferred();
                selectedItems = [];
                $.ajax({
                    url: EVENT_SERVICE_URL + "/SelectParticipants/" + processID,
                    type: "GET",
                    dataType: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    d.resolve(response);
                    fn_DisplayParticipants(response); //GeneralInformation에 표시
                }).fail(function (xhr, textStatus, errorThrown) {
                    alert("jsGridPraticipants :" +xhr.responseText);
                });
                return d.promise();
            }
        },
        onDataLoaded: function (args) {
            //if (args.data.length > 0 && $('#jsGridPraticipants').find('.i-squ.bg-red').length < 1)
            //    $('#tabParticipants .fa-commenting').hide();
            //else
            //    $('#tabParticipants .fa-commenting').show();
            
            if (args.data.length > 0) {
                var processStatus = $("input[id$=hddProcessStatus]").val();
                var eventID = $('input[id$=hddEventID]').val();
                var showCommenting = false;
                var showCommenting_comment = "";
                //1.0.7 ABM 이벤트에서 PV Attestation form 양식 링크 추가
                if (eventID == "E0008") {
                    $("#tbnDownloadPV").show();
                    console.log('show');
                } else {
                    $("#tbnDownloadPV").hide();
                }

                //version 1.0.4 PB 의 경우 4명 이상과 다른 병원 포함 일 경우 error  
                if (eventID == "E0001") {
                    var array_hco = [];
                    var total_paticipant = $('#jsGridPraticipants').find('.i-squ.bg-green').length;
                    for (var p_type in args.data) {
                        var minus_value = 0;
                        
                        if (args.data[p_type].PARTICIPANT_TYPE == "Employee") {
                            total_paticipant -= 1;
                        } else {
                            if (args.data[p_type].HCO_CODE != "" && args.data[p_type].STATUS=="Y" )
                                array_hco.push(args.data[p_type].HCO_CODE);
                        }
                    }
                    if (total_paticipant >= 4) {
                        showCommenting = true;
                        showCommenting_comment += "/PB 는 3명 이하/";
                    }
                    for (var hco_type in array_hco) {
                        //alert(array_hco.indexOf(array_hco[hco_type]));
                        if (array_hco.indexOf(array_hco[hco_type]) >= 1) {
                            showCommenting = true;
                            showCommenting_comment += "/PB 는 같은 병원이어야 합니다:" + array_hco[hco_type] + "/";
                            break;
                        }
                    }
                }
                 //version 1.0.4 PB 의 경우 4명 이상과 다른 병원 포함 일 경우 error  END

                if ($('#jsGridPraticipants').find('.i-squ.bg-red').length > 0) {
                    //4회 위반이 있는경우
                    showCommenting = true;
                    showCommenting_comment += "/4회 위반 참석자/";
                }
                else if ($('#jsGridPraticipants').find('.i-squ.bg-green').length > 0) {
                    //참석인 경우 상태값이 D/N character 가 있으면 
                    var isAllActive = true;
                    $('#jsGridPraticipants .i-squ.bg-green').each(function () {
                        if ($(this).text().length > 0) {
                            showCommenting = true;
                            showCommenting_comment += "/참석자 현재 상태/";
                        }
                    });
                    //version 1.0.4 D/N 인 경우 Not attend 로 변경해도 에러 표시, 무조건 delete 되어야 함
                    $('#jsGridPraticipants .i-squ.bg-yellow').each(function () {
                        if ($(this).text().length > 0) {
                            showCommenting = true;
                            showCommenting_comment += "/참석자 현재 상태/";
                        }
                    });
                    //if (isAllActive) showCommenting = false;
                    //else showCommenting = true;
                   
                } else {
                    //$('#tabParticipants .fa-commenting').hide();
                }
                //alert(showCommenting_comment);
                //alert(showCommenting);
                //console.log(showCommenting);
                if (showCommenting && processStatus == "Completed")
                    $('#tabParticipants .fa-commenting').show();
                else
                    $('#tabParticipants .fa-commenting').hide();
            } else {
                $('#tabParticipants .fa-commenting').show();
            }
            resizeScreen();
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
                    var $check = $("<input>").attr("type", "checkbox").prop("checked", false);
                    var delegation = $("input[id$=hddDelegation]").val();
                    var userID = $("input[id$=hddUserID]").val();
                    if (delegation == "only") {
                        if (item.CREATOR_ID != userID)
                            $check.prop("disabled", true);
                    }
                    // Role 있는경우는 비활성화
                    if (item.ROLE.length > 0) {
                        $check.prop("disabled", true);
                    }
                    return $check
                            .prop("checked", $.inArray(item.PARTICIPANT_IDX, selectedItems) > -1)
                            .on("change", function () {
                                $(this).is(":checked") ? selectItem(item) : unselectItem(item);
                            });
                },
                sorting: false,
                align: "center",
                width: 50
            },
            {
                name: "STATUS", title: "Status", type: "text", width: 40, align: "center",
                itemTemplate: function (value, item) {
                    if (value != "D" && item.IS_ATTENDED == 'V') {
                        var colVisitLimit = "<a href='#tooltip_visit_" + item.PARTICIPANT_IDX.toString() + "' class='btn-tooltip' data-hcp-code='" + item.HCP_CODE + "' data-event-date='" + item.EVENT_DATE + "' onclick=showVisitTooltip(this);><i class='i-squ bg-red'>" + VISIT_LIMIT_COUNT + "</i></a>";
                        return colVisitLimit;
                    } else {
                        var statusChar = "";
                        if (item.HCP_STATUS == "0" || item.HCP_STATUS == "9") statusChar = "D";
                        else if (item.HCP_STATUS == "8") statusChar = "N";
                        if (value == "D")
                            return "<i class='i-squ bg-gray'>" + statusChar + "</i>";
                        else if (value == "Y")
                            return "<i class='i-squ bg-green'>" + statusChar + "</i>";
                        else
                            return "<i class='i-squ bg-yellow'>" + statusChar + "</i>";
                    }
                }
            },
            {
                name: "HCP_NAME", title: "Name", type: "text", width: 80,
                itemTemplate: function (value, item) {
                    if (item.IS_DELETED != "Y" && item.PARTICIPANT_TYPE != 'Employee') //직원이 아닌경우
                    {
                        return "<a href='#' class='link text-ellipsis' onclick='showHCPInfo(this);'>" + item.HCP_NAME + "</a>";
                    } else {
                        return item.HCP_NAME;
                    }
                }
            },
            {
                //version 1.0.4 HCO 길이 늘리기
                name: "HCO_NAME", title: "Organization", type: "text", width:200,
                itemTemplate: function (value, item) {
                    var sectorName = "Other";
                    //version 1.0.6 CH 행사의 간호사 소속기관 구분 생성 요청(OtherHCO type 인 경우에도 소속기관 표시)
                    if (item.PARTICIPANT_TYPE == "KoreaHCP" || item.PARTICIPANT_TYPE == "OtherHCP") {
                        if (item.PARTICIPANT_TYPE == "Employee") {
                            sectorName = "Bayer";
                        } else {
                            if (item.HCO_SECTOR_NAME == "학교법인")
                                sectorName = "Private";
                            else if (item.HCO_SECTOR_NAME == "공립" || item.HCO_SECTOR_NAME == "특수법인")
                                sectorName = "Public";
                            else if (item.HCO_SECTOR_NAME == "국립" || item.HCO_SECTOR_NAME == "군병원")
                                sectorName = "Go";
                        }
                        return item.HCO_NAME + "<br/>(" + sectorName + ")";
                    }
                    else
                        return item.HCO_NAME;
                }
            },
            { name: "SPECIALTY_NAME", title: "Specialty", type: "text", width: 80 },
            { name: "ROLE", title: "Role", type: "text", width: 80 },
            { name: "DATA_SOURCE", title: "Source", type: "text", width: 50 },
            { name: "CREATOR_NAME", title: "By Whom", type: "text", width: 100 }
        ]
    });

    var selectedItems = [];

    var selectAllItem = function () {
        selectedItems = [];
        $("#jsGridPraticipants tbody input:checkbox").each(function () {
            if (!$(this).prop("disabled")) {
                selectedItems.push($(this).closest('tr').data('JSGridItem').PARTICIPANT_IDX);
                $(this).prop('checked', true);
            }
        });
    }

    var unselectAllItem = function () {
        selectedItems = [];
        $("#jsGridPraticipants tbody input:checkbox").each(function () {
            $(this).prop('checked', false);
        });
    }

    var selectItem = function (item) {
        selectedItems.push(item.PARTICIPANT_IDX);
    };

    var unselectItem = function (item) {
        selectedItems = $.grep(selectedItems, function (i) {
            return i !== item.PARTICIPANT_IDX;
        });
    };

    /* Enter key */
    $("#tabKoreaHCP").on("keypress", "input[type=text]", function (event) {
        if (event.keyCode == 13) {
            searchHCP();
        }
    });

    $("#tabEmployee").on("keypress", "input[type=text]", function (event) {
        if (event.keyCode == 13) {
            searchEmployee();
        }
    });


    /* Korea HCP 조회 */
    $("#btnSearchHCP").click(function () {
        searchHCP();
    });

    $("#btnSearchEmployee").click(function () {
        searchEmployee();
    });

    /* Participants 추가 */
    $("#btnAddParticipant").click(function () {

        if (!checkSavedStatus()) {
            fn_showWarning({
                title: "Confirm",
                message: "Please save the event."
            }).done(function () {
                $('#tabEventMaster a:first').tab('show');
            });
        } else {
            var tabIndex = $("#tabAddParticipant .active").index();

            if (tabIndex == 0) addKoreaHCP();
            else if (tabIndex == 1) addOtherHCP();
            else if (tabIndex == 2) addEmployee();
            else if (tabIndex == 3) addContractHCP();
        }
    });

    /* 참석 */
    $("#btnIsAttended").click(function () {
        if (selectedItems.length < 1) {
            showNotSelected("Please select an participant.");
            return;
        }
        updateParticipantStatus('Y', selectedItems);
        selectedItems = [];
    });

    /* 불참 */
    $("#btnIsNotAttended").click(function () {
        if (selectedItems.length < 1) {
            showNotSelected("Please select an participant.");
            return;
        }
        updateParticipantStatus('N', selectedItems);
        selectedItems = [];
    });

    /* 삭제 */
    $("#btnDeleteParticipant").click(function () {

        if (selectedItems.length < 1) {
            showNotSelected("Please select an participant.");
            return;
        }

        DeleteParticipant(selectedItems);
        selectedItems = [];
    });



    /* SRM Excel Fils Download */
    $("#tbnDownloadSRM").click(function () {
        var eventKey = $("#hspanEventKey").text();
        var srmDown = {
            fileName: "SRM_Report_" + eventKey + ".xlsx",
            userID: $("input[id$=hddUserID]").val(),
            processID: $("input[id$=hddProcessID]").val()
        };

        $.ajax({
            url: EVENT_SERVICE_URL + "/ExcelFileDown",
            type: "POST",
            data: JSON.stringify(srmDown),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (downPath) {

                fn_ExcelFileDownload(downPath);

            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });
    });

    /* Attendees List Excel Fils Download */
    $("#tbnDownloadAttendeeList").click(function () {
        
        var eventKey = $("#hspanEventKey").text();
        var userid = $("#hddUserID").val();
        var srmDown = {
            fileName: "Attendees_List_" + userid + "_" + eventKey + ".xlsx",
            userID: userid,
            processID: $("#hddProcessID").val()
        };

        $.ajax({
            url: EVENT_SERVICE_URL + "/ExcelFileDownAttendeeList",
            type: "POST",
            data: JSON.stringify(srmDown),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (downPath) {
                fn_ExcelFileDownload(downPath);

            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });

    });

    function initAddendeeListBtn() {
        var eventKey = $("#hspanEventKey").text();
        var userid = $("#hddUserID").val();
        var srmDown = {
            fileName: "Attendees_List_" + userid + "_" + eventKey + ".xlsx",
            userID: userid,
            processID: $("#hddProcessID").val()
        };

        $.ajax({
            url: EVENT_SERVICE_URL + "/ExcelFileDownAttendeeList",
            type: "POST",
            data: JSON.stringify(srmDown),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (downPath) {
                $("#tbnDownloadAttendeeList").remove();
                strUrl = "/ultra/Handler/FileDownHandler.aspx?DestFileName=" + downPath;
                $("#tbnDownloadWrapper").prepend("<a class='btn btn-sm btn-darkgray' href='" + strUrl + "' target='_blank'><i class='fa fa-download'></i>AttendeeList</a>")
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });
    }
    function initSRMBtn() {
        var eventKey = $("#hspanEventKey").text();
        var srmDown = {
            fileName: "SRM_Report_" + eventKey + ".xlsx",
            userID: $("#hddUserID").val(),
            processID: $("#hddProcessID").val()
        };

        $.ajax({
            url: EVENT_SERVICE_URL + "/ExcelFileDown",
            type: "POST",
            data: JSON.stringify(srmDown),
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (downPath) {
                var disabled = $("#tbnDownloadSRM").prop("disabled");
                if (!disabled || disabled === 'false') {
                    $("#tbnDownloadSRM").remove();
                    strUrl = "/ultra/Handler/FileDownHandler.aspx?DestFileName=" + downPath;
                    $("#tbnDownloadWrapper").append("<a class='btn btn-sm btn-darkgray' href='" + strUrl + "' target='_blank'><i class='fa fa-download'></i>SRM!</a>")
                }
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });
    }
    if (isSmartDevice()) {
        initAddendeeListBtn();
        initSRMBtn();
    }

    $("#jsGridPraticipants").on('click', 'a', function () {
        var tg = $(this).attr('href');
        $(tg).fadeIn('fast');
        return false;
    });
    $("#jsGridPraticipants").on('click', '.layer-tooltip .close', function () {
        $(this).closest('.layer-tooltip').fadeOut('fast');
    });

    $("select[id$=selHCPType]").on('change', function () {
        var hcpType = $('select[id$=selHCPType]').val();
        if (hcpType == "Foreigner" || hcpType == "Guest") {
            $("div[id$=divHCPCountry]").show();
            $("div[id$=divHCOSearcher]").hide();
            $("div[id$=divInputHCOName]").show();
        } else { 
            $("div[id$=divHCPCountry]").hide();
            $("div[id$=divHCOSearcher]").show();
            $("div[id$=divInputHCOName]").hide();
        }
    });
});

function searchHCP() {
    var hcpName = $(".tab-pane-korea #lb_name").val();
    var orgName = $(".tab-pane-korea #lb_hco").val();
    var speName = $(".tab-pane-korea #lb_special").val();
    var processID = $('input[id$=hddProcessID]').val();

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
        speName: speName,
        processID: processID
    };

    $.ajax({
        url: COMMON_SERVICE_URL + "/SelectHCP",
        type: "POST",
        data: JSON.stringify(search),
        dataType: "json",
        cache: false,
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
}

function searchEmployee() {
    var processID = $('input[id$=hddProcessID]').val();
    var keyword = $("#tabEmployee #lb_keyword").val();

    if (keyword.length < 1) {
        fn_showWarning({
            title: "input",
            message: "Please enter the conditions."
        });
        return;
    }

    $.ajax({
        url: COMMON_SERVICE_URL + "/SelectEmployeeParticipants/" + processID + "/" + keyword,
        type: "GET",
        dataType: "json",
        cache: false,
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            displayEmployeeResult(data);
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}

/* Korea HCP 저장 */
function addKoreaHCP() {
    var eventID = $('input[id$=hddEventID]').val();
    var processID = $('input[id$=hddProcessID]').val();
    var userID = $('input[id$=hddUserID]').val();

    var participants = [];
    $('#tblEP_HCPKorea').find('input[type="checkbox"]:checked').each(function () {
        var objHCP = $(this).closest('.tr-hcp').data('hcp');
        //var role = $(this).closest('.tr-hcp').find("input[type='radio']:checked").data('role');
        var role = "";
        var parti = {
            EVENT_ID: eventID,
            PROCESS_ID: processID,
            PARTICIPANT_IDX: 0,
            PARTICIPANT_TYPE: 'KoreaHCP',
            HCP_TYPE: objHCP.Type,
            HCP_CODE: objHCP.HCPCode,
            HCP_NAME: objHCP.HCPName,
            HCO_CODE: objHCP.OrganizationCode,
            HCO_NAME: objHCP.OrganizationName,
            SPECIALTY_CODE: objHCP.SpecialtyCode,
            SPECIALTY_NAME: objHCP.SpecialtyName,
            COUNTRY_CODE: '',
            COUNTRY_NAME: '',
            ROLE: role,
            DATA_SOURCE: 'Ultra',
            IS_ATTENDED: 'Y',
            IS_DELETED: 'N',
            CREATOR_ID: userID,
        };
        participants.push(parti);
    });

    if (participants.length > 0) callInsertParticipantAjax(participants);
}

/* Other HCP 저장 */
function addOtherHCP() {
    var eventID = $('input[id$=hddEventID]').val();
    var processID = $('input[id$=hddProcessID]').val();
    var userID = $('input[id$=hddUserID]').val();
    var orgCode = $("span[id$=hspanOrganization]").text();
    var participants = [];

    var hcpType = "";
    var hcpName = "";
    var hcoCode = "";
    var hcoName = "";
    var now = new Date();
    //var genCode = now.format("yyyyMMdd-hhmmss");
    var genCode = now.format("MMddHHmmss");
    //genCode = processID + "_" + genCode;
    genCode = userID+genCode;

    if (eventID == "E0007") {
        console.log(eventID);
        hcpType = $('select[id$=selHCPType]').val();
        hcoCode = $("div[id$=divHCOSearcher] .hco-code").data('hco-code');
        hcoName = $("div[id$=divHCOSearcher] .hco-code").data('hco-name');
    } else {
        if (orgCode.indexOf("BKL-CH") == 0) {
            var hcpType = $('select[id$=selHCPType]').val();
            if (hcpType == "Foreigner" || hcpType == "Guest") {
                hcoName = $("#lb_other_hco_name").val();
                hcoCode = genCode;
            } else {
                hcoCode = $("div[id$=divHCOSearcher] .hco-code").data('hco-code');
                hcoName = $("div[id$=divHCOSearcher] .hco-code").data('hco-name');
            }
        } else {
            hcpType = $('select[id$=selHCPType]').val();
            hcoName = $("#lb_other_hco_name").val();
            hcoCode = genCode;
        }
    }
    hcpName = $("#lb_other_hcp_name").val();

    var countryCode = "";
    var countryName = "";
    if (hcpType == "Foreigner" || hcpType == "Guest") {
        //CRM 도 guest 일때 HCO 입력 변경하여 추가 2021.06.10
        hcoName = $("#lb_other_hco_name").val();
        hcoCode = genCode;
        //CRM 도 guest 일때 HCO 입력 변경하여 추가 2021.06.10

        countryCode = $('select[id$=lb_country]').val();
        countryName = $('select[id$=lb_country] option:selected').text();
    }

    if (!hcpName) hcpName = "";
    if (!hcoName) hcoName = "";

    if (hcpName.length < 1 || hcoName.length < 1) {
        fn_showInformation({
            title: "Confirm!",
            message: "참석자를 입력바랍니다."
        });
        return;
    }
    var role = "";
    var parti = {
        EVENT_ID: eventID,
        PROCESS_ID: processID,
        PARTICIPANT_IDX: 0,
        PARTICIPANT_TYPE: 'OtherHCP',
        HCP_TYPE: hcpType,
        HCP_CODE: genCode,
        HCP_NAME: hcpName,
        HCO_CODE: hcoCode,
        HCO_NAME: hcoName,
        SPECIALTY_CODE: '',
        SPECIALTY_NAME: '',
        COUNTRY_CODE: countryCode,
        COUNTRY_NAME: countryName,
        ROLE: role,
        DATA_SOURCE: 'Ultra',
        IS_ATTENDED: 'Y',
        IS_DELETED: 'N',
        CREATOR_ID: userID,
    };
    participants.push(parti);

    if (participants.length > 0) callInsertParticipantAjax(participants);

    //HCO 이름은 삭제하지 않음 by sumin jo
    //$("#lb_other_hco_name").val("");
    $("#lb_other_hcp_name").val("");
    //$("div[id$=divHCOSearcher] .hco-code").data('hco-code', "");
    //$("div[id$=divHCOSearcher] .hco-code").data('hco-name', "");
   // $("#txtHCOName").val("");

}

/* 직원 저장 */
function addEmployee() {
    var eventID = $('input[id$=hddEventID]').val();
    var processID = $('input[id$=hddProcessID]').val();
    var userID = $('input[id$=hddUserID]').val();

    var participants = [];
    $('#tblEP_Employee').find('input[type="checkbox"]:checked').each(function () {
        var objEmployee = $(this).closest('.tr-employee').data('employee');
        var parti = {
            EVENT_ID: eventID,
            PROCESS_ID: processID,
            PARTICIPANT_IDX: 0,
            PARTICIPANT_TYPE: 'Employee',
            HCP_TYPE: 'Employee',
            HCP_CODE: objEmployee.USER_ID,
            HCP_NAME: objEmployee.FULL_NAME,
            HCO_CODE: '0695',
            HCO_NAME: objEmployee.ORG_ACRONYM,
            SPECIALTY_CODE: '',
            SPECIALTY_NAME: 'Employee',
            COUNTRY_CODE: '',
            COUNTRY_NAME: '',
            ROLE: '',
            DATA_SOURCE: 'CWID',
            IS_ATTENDED: 'Y',
            IS_DELETED: 'N',
            CREATOR_ID: userID,
        };
        participants.push(parti);
    });

    if (participants.length > 0) callInsertParticipantAjax(participants);
}

function addContractHCP() {
    var eventID = $('input[id$=hddEventID]').val();
    var processID = $('input[id$=hddProcessID]').val();
    var userID = $('input[id$=hddUserID]').val();

    var participants = [];
    $('#tblEP_Contract').find('input[type="checkbox"]:checked').each(function () {
        var objHCP = $(this).closest('.tr-hcp').data('hcp');
        var role = "";
        var parti = {
            EVENT_ID: eventID,
            PROCESS_ID: processID,
            PARTICIPANT_IDX: 0,
            PARTICIPANT_TYPE: 'KoreaHCP',
            HCP_TYPE: objHCP.Type,
            HCP_CODE: objHCP.HCPCode,
            HCP_NAME: objHCP.HCPName,
            HCO_CODE: objHCP.OrganizationCode,
            HCO_NAME: objHCP.OrganizationName,
            SPECIALTY_CODE: objHCP.SpecialtyCode,
            SPECIALTY_NAME: objHCP.SpecialtyName,
            COUNTRY_CODE: '',
            COUNTRY_NAME: '',
            ROLE: role,
            DATA_SOURCE: 'Ultra',
            IS_ATTENDED: 'Y',
            IS_DELETED: 'N',
            CREATOR_ID: userID,
        };
        participants.push(parti);
    });

    if (participants.length > 0) callInsertParticipantAjax(participants);
}

function callInsertParticipantAjax(participants) {
    $.ajax({
        url: EVENT_SERVICE_URL + "/InsertParticipants",
        type: "POST",
        data: JSON.stringify(participants),
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (data) {
            if (data) $('input[id$=hddProcessID]').val(data);
            $("#jsGridPraticipants").jsGrid("loadData"); //Participant 재조회
            clearSearchParticipant();
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}

/* 참석/미참석 변경 */
function updateParticipantStatus(status, indexes) {
    var processID = $('input[id$=hddProcessID]').val();
    var userID = $('input[id$=hddUserID]').val();

    var items = {
        processID: processID,
        indexes: indexes,
        isAttended: status,
        updaterID: userID
    }

    $.ajax({
        url: EVENT_SERVICE_URL + "/UpdateParticipantStatus",
        type: "POST",
        data: JSON.stringify(items),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function () {
            $("#jsGridPraticipants").jsGrid("loadData"); //Participant 재조회
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}

/* 참석자 삭제 */
function DeleteParticipant(indexes) {
    var processID = $('input[id$=hddProcessID]').val();
    var userID = $('input[id$=hddUserID]').val();

    var items = {
        processID: processID,
        indexes: indexes,
        updaterID: userID
    }

    $.ajax({
        url: EVENT_SERVICE_URL + "/DeleteParticipant",
        type: "POST",
        data: JSON.stringify(items),
        //dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function () {
            $("#jsGridPraticipants").jsGrid("loadData"); //Participant 재조회
        },
        error: function (error) {
            fn_showError({
                message: error.responseText
            });
        },
    });
}

function clearSearchParticipant() {
    $(".tab-pane-korea #lb_name").val('');
    $(".tab-pane-korea #lb_hco").val('');
    $(".tab-pane-korea #lb_special").val('');
    $('#tblEP_HCPKorea tbody').empty();
}

function clearSearchEmployee() {
    $(".tab-pane-bayer #lb_keyword").val('');
}

function displayHCPResult(list) {
    $('#tblEP_HCPKorea tbody').empty();

    var cnt = list.length;
    if (cnt < 1) {
        var rowNotFound = "<tr><td>Not Found</td><tr>";
        $('#tblEP_HCPKorea tbody').append(rowNotFound);
        return;
    }
    for (var i = 0; i < cnt; i++) {
        var hcp = list[i];
        var row = "<tr class='tr-hcp' data-hcp='" + JSON.stringify(hcp) + "'><td class='text-left'>";
        row += "<label class='item-name'>";
        if (hcp.IsExist == 'N')
            if (hcp.VisitCount >= parseInt(VISIT_LIMIT_COUNT)) {
                row += "<span class='fix'><i class='i-squ bg-red'>" + VISIT_LIMIT_COUNT + "</i></span>";
            } else
                row += "<span class='fix'><input type='checkbox'/></span>";
        else
            row += "<span class='fix'><input type='checkbox' disabled /></span>";
        row += "<strong>" + hcp.HCPName + "</strong><br/><span style='display:inline-block;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;width:280px' title='" + hcp.OrganizationName + "-" + hcp.SpecialtyName+"'>" + hcp.OrganizationName + "/" + hcp.SpecialtyName + "</span></label></td>";
        //row = row + "<td class='vertical-middle'><input type='radio' name='lb_type1_" + (i + 1).toString() + "' data-role='Core'></td>";
        //row = row + "<td class='vertical-middle'><input type='radio' name='lb_type1_" + (i + 1).toString() + "' checked data-role='Visitor'></td>";
        row += "</tr>";
        $('#tblEP_HCPKorea tbody').append(row);
    }

    if (cnt == 100) {
        fn_showInformation({
            title: "Information",
            message: "조회결과가 100건 이상입니다. 세부조건을 입력바랍니다."
        });
    }
}


function displayEmployeeResult(list) {
    $("#tblEP_Employee tbody").empty();
    var cnt = list.length;
    for (var i = 0; i < cnt; i++) {
        var employee = list[i];
        var row = "<tr class='tr-employee' data-employee='" + JSON.stringify(employee) + "'><td class='text-left'><label class='item-name'><span class='fix'>";
        if (employee.IS_EXIST == 'N')
            row = row + "<input type='checkbox'/></span><strong>" + employee.FULL_NAME + "</strong><br>";
        else
            row = row + "<input type='checkbox' disabled/></span><strong>" + employee.FULL_NAME + "</strong><br>";
        row = row + "(" + employee.USER_ID + ")</label></td><td class='text-left'>" + employee.ORG_ACRONYM + "</td></tr>";
        $("#tblEP_Employee tbody").append(row);
    }
}

function displayContractResult(list) {
    $('#tblEP_Contract tbody').empty();
    var cnt = list.length;
    for (var i = 0; i < cnt; i++) {
        var hcp = list[i];
        var row = "<tr class='tr-hcp' data-hcp='" + JSON.stringify(hcp) + "'><td class='text-left'>";
        row += "<label class='item-name'>";
        if (hcp.IsExist == 'N')
            if (hcp.VisitCount > parseInt(VISIT_LIMIT_COUNT)) {
                row += "<span class='fix'><i class='i-squ bg-red'>" + hcp.VisitCount.toString() + "</i></span>";
            } else
                row += "<span class='fix'><input type='checkbox'/></span>";
        else
            row += "<span class='fix'><input type='checkbox' disabled /></span>";
        row += "<strong>" + hcp.HCPName + "</strong><br/><span style='display:inline-block;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;width:280px' title='" + hcp.OrganizationName + "-" + hcp.SpecialtyName +"'>" + hcp.OrganizationName + "/" + hcp.SpecialtyName + "</span></label></td>";
        //row = row + "<td class='vertical-middle'><input type='radio' name='lb_type1_" + (i + 1).toString() + "' data-role='Core'></td>";
        //row = row + "<td class='vertical-middle'><input type='radio' name='lb_type1_" + (i + 1).toString() + "' checked data-role='Visitor'></td>";
        row += "</tr>";
        $('#tblEP_Contract tbody').append(row);
    }

}

function showHCPInfo(link) {
    fn_showHCPInfo($(link).closest('tr').data('JSGridItem'));
}

function fn_showHCPInfo(hcp) {
    var d = $.Deferred();
    var _show = function () {
        $("#hcp_info #hcpName").text(hcp.HCP_NAME);
        $("#hcp_info #hcpCode").text(hcp.HCP_CODE);
        $("#hcp_info #hcoName").text(hcp.HCO_NAME);
        $("#hcp_info #hcoCode").text(hcp.HCO_CODE);
        $("#hcp_info #speName").text(hcp.SPECIALTY_NAME);
        $("#hcp_info #srcName").text(hcp.DATA_SOURCE);
        $("#hcp_info #creator").text(hcp.CREATOR_NAME);
        if (hcp.UPDATER_NAME) {
            $("#hcp_info #updater").text(hcp.UPDATER_NAME);
            $("#hcp_info #descUpdater").text(hcp.UPDATER_NAME);
        }
        else {
            $("#hcp_info #updater").text(hcp.CREATOR_NAME);
            $("#hcp_info #descUpdater").text(hcp.CREATOR_NAME)
        }
        if (hcp.UPDATE_DATE) $('#hcp_info #updateDate').text(hcp.UPDATE_DATE);
        else $('#hcp_info #updateDate').text(hcp.CREATE_DATE);

        $("#hcp_info").css('height', $(window).height() * 0.85)
            .modal('show');
    }
    _show();
    return d.promise();
}

function fn_ExcelFileDownload(dnPath) {

    //var userID = $('input[id$=hddUserID]').val();
    //var processID = $('input[id$=hddProcessID]').val();
    //var strUrl = null;
    try {
        strUrl = "/ultra/Handler/FileDownHandler.aspx?DestFileName=" + dnPath;

        //alert("fn_ExcelFileDownload : " + strUrl);

        //$('#iframeFileDown', parent.document).location.href = strUrl;  // .attr('src', frameSrc);
        if (dnPath && !isSmartDevice()) {
            $('#iframeFileDown', parent.document).attr('src', strUrl);
        } else {
            window.open(strUrl);
        }
    }
    catch (ex) {
        alert("excelfiledownload" + ex.message);
    }
}

function isSmartDevice() {
    var ua = window['navigator']['userAgent'] || window['navigator']['vendor'] || window['opera'];
    return (/iPhone|iPod|iPad|Silk|Android|BlackBerry|Opera Mini|IEMobile/).test(ua);
}

function showVisitTooltip(target) {

    $('.layer-tooltip').hide();
    //var zIndex = $("#jsGridPraticipants").css("zIndex");
    var hcpCode = $(target).data('hcp-code');
    var eventDate = $(target).data('event-date');
    //var tgPos = $(target).closest('td').offset();
    //var divLayer = $(target).parent().find('div');
    //$(target).parent().find('div').css("zIndex", 1000000);
    //var tg = $(target).attr('href');


    $.ajax({
        type: "GET",
        url: EVENT_SERVICE_URL + "/SelectHomeVisitList/" + hcpCode + "/" + eventDate,
        dataType: "json",
        cache: false,
        success: function (data) {
            //$(tg).css({ top: tgPos.top + 34 + 'px', left: tgPos.left - 250 + 'px' }).fadeIn('fast');

            var $ul = $("#layer_visit_participant").find('ul');
            $ul.empty();


            for (var i = 0 ; i < data.length; i++) {

                var hcp = data[i];
                $("#layer_visit_participant #myModalLabel").text(hcp.HCP_NAME);
                var $li = $('<li/>');
                var $strong = $('<strong/>').addClass('item-name').text(hcp.EVENT_NAME + "(" + hcp.DATA_SOURCE + ")");
                var $spanType = $('<span/>').addClass('item-job').text(hcp.HCP_TYPE);
                var $spanDate = $('<span/>').addClass('item-date').text(hcp.EVENT_DATE);
                if (hcp.IS_VIOLATED == "Y") $spanDate.css('color', '#b53b4f')
                var $spanSpec = $('<span/>').addClass('item-cate').text(hcp.PROCESS_STATUS);
                var $pHco = $('<p/>').addClass('item-info');
                var hcoHtml = hcp.SUBJECT;
                $pHco.html(hcoHtml);

                $li.append($spanDate);
                $li.append($strong);
                $li.append($spanType);
                $li.append($spanSpec);
                $li.append($pHco);
                $ul.append($li);
            }
            //$(tg).fadeIn('fast');
            $("#layer_visit_participant").modal('show');
            return false;
        }
    })
}

function closeVisitTooltip(target) {
    $(target).closest('.layer-tooltip').fadeOut('fast');
}
