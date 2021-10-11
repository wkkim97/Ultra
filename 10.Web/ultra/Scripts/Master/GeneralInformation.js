function fn_DisplayCostPlan(values) {
    $('#tblGI_CostPlan tbody').empty();
    var total = 0;
    for (var i = 0; i < values.length; i++) {
        var value = values[i];
        var amount = value.QTY * value.PRICE;
        total = total + amount;
        var row = "<tr><td>" + value.CATEGORY_NAME + "</td><td>" + value.DESC + "</td><td class='text-right'>";
        row = row + fn_AddComma(value.QTY) + "</td><td class='text-right'>" + fn_AddComma(value.PRICE) + "</td><td class='text-right'>" + fn_AddComma(amount) + "</td><tr>";
        $('#tblGI_CostPlan tbody').append(row);
    }

    $('div[id$=divGI_CostPlan] .total strong').text(fn_AddComma(total));
}

function fn_DisplayParticipants(values) {
    $("#tblGI_Participants tbody").empty();
    var participantGo = 0;
    var participantPublic = 0;
    var participantPrivate = 0;
    var participantOther = 0;
    var participantForeigner = 0;
    var participantBayer = 0;
    var total = 0;
    for (var i = 0; i < values.length; i++) {
        var participant = values[i];
        if (participant.IS_ATTENDED == "Y" && participant.IS_DELETED == "N") {
            if (participant.PARTICIPANT_TYPE == "Employee") {
                participantBayer++;
            } else if (participant.PARTICIPANT_TYPE == "OtherHCP") {
                participantForeigner++;
            } else {
                if (participant.HCO_SECTOR_NAME == "학교법인")
                    participantPrivate++;
                else if (participant.HCO_SECTOR_NAME == "공립" || participant.HCO_SECTOR_NAME == "특수법인")
                    participantPublic++;
                else if (participant.HCO_SECTOR_NAME == "국립" || participant.HCO_SECTOR_NAME == "군병원")
                    participantGo++;
                else
                    participantOther++;
            }
            total++;
        }
    }

    var row = "<tr><td>" + participantGo + "</td><td>" + participantPublic + "</td><td>" + participantPrivate + "</td>";
    row = row + "<td>" + participantOther + "</td><td>" + participantForeigner + "</td><td>" + participantBayer + "</td></tr>";
    $("#tblGI_Participants tbody").append(row);

    $('div[id$=divGI_Participants] .total strong').text(fn_AddComma(total));
}


function fn_DownloadCommentAttachFile(target) {
    var attachFile = $(target).data("attach-file");
    //alert(attachFile);
    //alert(decodeURIComponent(attachFile));
    if (attachFile) {
        $('#iframeFileDown', parent.document).attr('src', UPLOAD_HANDLER_URL + "?file=" + decodeURIComponent(attachFile));
    }
}
