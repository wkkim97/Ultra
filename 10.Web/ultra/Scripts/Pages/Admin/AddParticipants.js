$(function () {
    $("#divInputParticipants").hide();

    /* Participants 목록조회 */
    $("#jsGridPraticipants").jsGrid({
        height: "900px",
        width: "100%",
        sorting: true,
        paging: false,
        autoload: false,
        controller: {
            loadData: function () {
                var eventKey = $('input[id$=searchText]').val();
                var d = $.Deferred();
                selectedItems = [];
                $.ajax({
                    url: EVENT_SERVICE_URL + "/SelectParticipantsByEventKey/" + eventKey,
                    type: "GET",
                    dataType: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    d.resolve(response);
                    if (response.length > 0) {
                        $("#divInputParticipants").show();
                        $("input[id$=hddProcessID]").val(response[0].PROCESS_ID);
                        $("input[id$=hddEventID]").val(response[0].EVENT_ID);
                    }
                }).fail(function (xhr, textStatus, errorThrown) {
                    alert(xhr.responseText);
                });
                return d.promise();
            }
        },
        fields: [
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
                name: "HCO_NAME", title: "Organization", type: "text",
                itemTemplate: function (value, item) {
                    var sectorName = "Other";
                    if (item.PARTICIPANT_TYPE == "KoreaHCP") {
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
    $("#btnAddAttended").click(function () {
        if ($('input[id$=hddProcessID]').val() != "") {
            var tabIndex = $("#tabAddParticipant .active").index();

            if (tabIndex == 0) addKoreaHCP("Y");
            else if (tabIndex == 1) addOtherHCP("Y");
            else if (tabIndex == 2) addEmployee("Y");
            else if (tabIndex == 3) addContractHCP("Y");
        }
    });
    $("#btnAddNotAttended").click(function () {
        if ($('input[id$=hddProcessID]').val() != "") {
            var tabIndex = $("#tabAddParticipant .active").index();

            if (tabIndex == 0) addKoreaHCP("N");
            else if (tabIndex == 1) addOtherHCP("N");
            else if (tabIndex == 2) addEmployee("N");
            else if (tabIndex == 3) addContractHCP("N");
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
            $("div[id$=divInputHCOName]").show();
            $("div[id$=divHCOSearcher]").hide();
        } else {
            $("div[id$=divHCPCountry]").hide();
            $("div[id$=divInputHCOName]").hide();
            $("div[id$=divHCOSearcher]").show();
        }
    });


    /* Enter key */
    $("#searchText").on("keypress", function (event) {
        
        if (event.keyCode == 13) {
            event.preventDefault();
            var keyword = $("#searchText").val();

            $("#divInputParticipants").hide();
            $("div input[type=hidden]").val("");

            //alert($('input[id$=hddUserID]').val());
            $("#jsGridPraticipants").jsGrid("loadData");
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
function addKoreaHCP(attended) {
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
            IS_ATTENDED: attended,
            IS_DELETED: 'N',
            CREATOR_ID: userID,
        };
        participants.push(parti);
    });

    if (participants.length > 0) callInsertParticipantAjax(participants);
}

/* Other HCP 저장 */
function addOtherHCP(attended) {
    var eventID = $('input[id$=hddEventID]').val();
    var processID = $('input[id$=hddProcessID]').val();
    var userID = $('input[id$=hddUserID]').val();
    //var orgCode = $("span[id$=hspanOrganization]").text();
    var participants = [];

    var hcpType = "";
    var hcpName = "";
    var hcoCode = "";
    var hcoName = "";
    var now = new Date();
    var genCode = now.format("yyyyMMdd-hhmmss");
    genCode = processID + "_" + genCode;

    //if (eventID == "E0007") {
    hcpType = $('select[id$=selHCPType]').val();
    
    if (hcpType == "Foreigner" || hcpType == "Guest") {
        
        hcoName = $("#lb_other_hco_name").val();
        hcoCode = genCode;
    } else {
        hcoCode = $("div[id$=divHCOSearcher] .hco-code").data('hco-code');
        hcoName = $("div[id$=divHCOSearcher] .hco-code").data('hco-name');
        
    }
    
    
    //} else {
    //    if (orgCode.indexOf("BKL-CH") == 0) {
    //        // CRM 사용자
    //        hcpType = $('select[id$=selHCPType]').val();
    //        hcoCode = $("div[id$=divHCOSearcher] .hco-code").data('hco-code');
    //        hcoName = $("div[id$=divHCOSearcher] .hco-code").data('hco-name');
    //    } else {
    //        hcpType = $('select[id$=selHCPType]').val();
    //        hcoName = $("#lb_other_hco_name").val();
    //        hcoCode = genCode;
    //    }
    //}
    hcpName = $("#lb_other_hcp_name").val();

    var countryCode = "";
    var countryName = "";
    if (hcpType == "Foreigner" || hcpType == "Guest") {
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
        IS_ATTENDED: attended,
        IS_DELETED: 'N',
        CREATOR_ID: userID,
    };
    participants.push(parti);

    if (participants.length > 0) callInsertParticipantAjax(participants);

    $("#lb_other_hco_name").val("");
    $("#lb_other_hcp_name").val("");
    $("div[id$=divHCOSearcher] .hco-code").data('hco-code', "");
    $("div[id$=divHCOSearcher] .hco-code").data('hco-name', "");

}

/* 직원 저장 */
function addEmployee(attended) {
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
            HCO_CODE: '',
            HCO_NAME: objEmployee.ORG_ACRONYM,
            SPECIALTY_CODE: '',
            SPECIALTY_NAME: 'Employee',
            COUNTRY_CODE: '',
            COUNTRY_NAME: '',
            ROLE: '',
            DATA_SOURCE: 'CWID',
            IS_ATTENDED: attended,
            IS_DELETED: 'N',
            CREATOR_ID: userID,
        };
        participants.push(parti);
    });

    if (participants.length > 0) callInsertParticipantAjax(participants);
}

function addContractHCP(attended) {
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
            IS_ATTENDED: attended,
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
        row += "<strong>" + hcp.HCPName + "</strong><br/><span style='display:inline-block;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;width:280px'>" + hcp.OrganizationName + "/" + hcp.SpecialtyName + "</span></label></td>";
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
        row += "<strong>" + hcp.HCPName + "</strong><br/><span style='display:inline-block;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;width:280px'>" + hcp.OrganizationName + "/" + hcp.SpecialtyName + "</span></label></td>";
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

function tbnDownloadSRM() {
    var processID = $('input[id$=hddProcessID]').val();

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
        alert(ex.message);
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

