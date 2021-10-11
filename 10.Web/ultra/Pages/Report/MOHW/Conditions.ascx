<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Conditions.ascx.cs" Inherits="Pages_Report_MOHW_Conditions" %>
    <style type="text/css" >
        .inline{
            display:inline !important;
            margin-left:0px;
            padding-left:10px;
        }
        .inline input{
            margin-left:0px !important;
        }
    </style>
    <div class="write-area" style="width: 100%; margin-bottom: 10px;">
            <table class="table form-group-sm">
                <tbody>
                    <tr>
                        <th>Start Time</th>
                        <td>
                            <div class="input-group date form-date-hour-min form_StartTime" data-date="" data-date-format="yyyy-mm-dd hh:ii:ss" data-link-field="dtStartTime">
                                <input id="datStartTime" class="form-control" size="16" type="text" value="" readonly />
                                <span class="input-group-addon"><span class="fa fa-times"></span></span>
                                <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                            </div>
                            <input type="hidden" id="dtStartTime" value="" />
                        </td> 
                        <th>End Time</th>
                        <td>
                            <div class="input-group date form-date-hour-min form_EndTime" data-date="" data-date-format="yyyy-mm-dd hh:ii:ss" data-link-field="dtEndTime">
                                <input id="datEndTime" class="form-control" size="16" type="text" value="" readonly />
                                <span class="input-group-addon"><span class="fa fa-times"></span></span>
                                <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                            </div>
                            <input type="hidden" id="dtEndTime" value="" />
                        </td>
                    </tr> 
                </tbody>
            </table>
             <div class="box-panel" id="divExceptArea" style="display:none;">
                    <span class="checkbox inline" id="hspanNotFoodCostYN">
                        <input type="checkbox" id="chkNotFoodCostYN" /><label for="chkNotFoodCostYN">식음료 비용이 아닌 금액</label>
                    </span>
                    <span class="checkbox inline">
                        <input type="checkbox" id="chkBelowOneWonYN" /><label for="chkBelowOneWonYN" id="lblBelowOneWonYN">판촉물 또는 식음료 중에 1만원 이하 금액</label>
                    </span>
                    <span class="checkbox inline">
                        <input type="checkbox" id="chkExceptIecturerFoodCostYN"  /><label for="chkExceptIecturerFoodCostYN">강연자 식음료 금액 제외</label>
                    </span>
                    <span class="checkbox inline">
                        <input type="checkbox" id="chkOnlyAttendYN" /><label for="chkOnlyAttendYN">Only 참석자(“Attend”)</label>
                    </span>
                    <span class="checkbox inline">
                        <input type="checkbox" id="chkOnlyMedicineYN" /><label for="chkOnlyMedicineYN">Only 의약품</label>
                    </span>
                    <span class="checkbox inline">
                        <input type="checkbox" id="chkOnlyMedicalEquipmentYN"  /><label for="chkOnlyMedicalEquipmentYN">Only 의료기기</label>
                    </span>
                    <span class="checkbox inline">
                        <input type="checkbox" id="chkExceptBayerEmployYN" /><label for="chkExceptBayerEmployYN">Bayer Employee 제외</label>
                    </span>
             </div> 
            <!--version 1.0.6 KRPIA Report-->
            <div class="box-panel" id="divKRPIAArea" style="display:none;">
                    <span class="checkbox inline" >
                        <input type="checkbox" id="chkTYPEConsultingYN" /><label for="chkTYPEConsultingYN">Consulting</label>
                    </span>
                    <span class="checkbox inline">
                        <input type="checkbox" id="chkTYPELectureYN" /><label for="chkTYPELectureYN" >Lecture</label>
                    </span>
                   
             </div> 


    </div>
    <input type="hidden" id="hhdStatDate" runat="server" />
    <input type="hidden" id="hhdEndDate" runat="server" />
    <script src="../../../Scripts/Pages/Report/MohwConditions.js"></script>
    <script src="../../../Scripts/JQuery/jquery-ui-1.12.1.custom/jquery-ui.js"></script>
