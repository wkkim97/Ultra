$(function () {
    try {
        fn_ConditionInit();
    }
    catch (ex) {
        fn_showError({ message: ex.message });
    }
});

function fn_ConditionInit()
{
    var type = $("input[id$=hddMohwType]").val();
    var $exceptArea = $("#divExceptArea");
    switch (type)
    {
        case 'DIV_MEDICAL': $exceptArea.show(); $("#hspanNotFoodCostYN").addClass("inline").css("display", ""); $("#lblBelowOneWonYN").text("판촉물 또는 식음료 중에 1만원 이하 금액"); break;
        case 'PLURALITY_MEDICAL': $exceptArea.show(); $("#hspanNotFoodCostYN").removeClass("inline").css("display", "none"); $("#lblBelowOneWonYN").text("기념품 또는 식음료 중에 1만원 이하 금액"); break;
        case 'SAMPLE': $exceptArea.hide(); break;
        case 'PARTICIPANTS': $exceptArea.hide(); break;
        case 'MARKET_RESEARCH': $exceptArea.hide(); break;
        default: $exceptArea.hide(); break;
    }

    $('.form_StartTime').datetimepicker({
        weekStart: 0,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        forceParse: 0,
        minuteStep: 60,
        minView: 2,
        maxView: 2,
        linkField: "datStartTime",
        linkFormat: "yyyy-mm-dd"
    });

    $('.form_EndTime').datetimepicker({
        weekStart: 0,
        todayBtn: 1,
        autoclose: 1,
        todayHighlight: 1,
        startView: 2,
        forceParse: 0,
        minuteStep: 60,
        minView: 2,
        maxView: 2,
        linkField: "datEndTime",
        linkFormat: "yyyy-mm-dd"
    });

    $(".form_StartTime").datetimepicker('update', $("input[id$=hhdStatDate]").val());
    $(".form_EndTime").datetimepicker('update', $("input[id$=hhdEndDate]").val());

    $("#chkNotFoodCostYN").val('');
    $("#chkBelowOneWonYN").val('');
    $("#chkExceptIecturerFoodCostYN").val('');
    $("#chkOnlyAttendYN").val('');
    $("#chkOnlyMedicineYN").val('');
    $("#chkOnlyMedicalEquipmentYN").val('');
    $("#chkExceptBayerEmployYN").val('');
}

function fn_GetFilter() {
    
    var notFoodConstYN = $("#chkNotFoodCostYN").is(":checked") ? "Y" : "N" ,
        belowOneWonYN = $("#chkBelowOneWonYN").is(":checked") ? "Y" : "N",
        exceptIectureFoodCostYN =  $("#chkExceptIecturerFoodCostYN").is(":checked") ? "Y" : "N",
        onlyAttendYN = $("#chkOnlyAttendYN").is(":checked") ? "Y" : "N",
        onlyMedicineYN = $("#chkOnlyMedicineYN").is(":checked") ? "Y" : "N",
        onlyMedicalEquipmentYN = $("#chkOnlyMedicalEquipmentYN").is(":checked") ? "Y" : "N",
        onlyExceptBayerEmployYN = $("#chkExceptBayerEmployYN").is(":checked") ? "Y" : "N";
     
    var datepo = new Date($("#datEndTime").val());
    var days = 1;
    datepo.setDate(datepo.getDate() + days);
    var strFormatedPODate = $.datepicker.formatDate('yy-mm-dd', new Date(datepo));

    var dto = {
         START_DATE: $("#datStartTime").val()
       , END_DATE: strFormatedPODate
       , IS_FOOD_COST_YN: notFoodConstYN
       , BELOW_ONE_WON_YN: belowOneWonYN
       , EXCEPT_IECTURER_FOOD_COST_YN: exceptIectureFoodCostYN
       , ONLY_ATTEND_YN: onlyAttendYN
       , ONLY_MEDICINE_YN: onlyMedicineYN
       , ONLY_MEDICAL_EQUIPMENT_YN: onlyMedicalEquipmentYN
       , EXCEPT_BAYER_EMPLOYEEE_YN: onlyExceptBayerEmployYN
       , CONFIRM_YN: "N"
    };
    return dto;
}
 