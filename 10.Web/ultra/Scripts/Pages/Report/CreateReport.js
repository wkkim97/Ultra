$(function () {
    try {

        $("#spanUserName").text($("input[id$=hddUserName]").val());
        $("#spanCreateDate").text($("input[id$=hddGetDate]").val());
       
        $("#btnCreateReport").on("click", function () {
            try {
                if ($("#txtSubject").val().length <= 0) {
                    fn_showInformation({ title: "Please fill out below fields.", message: "Subject" });
                    return;
                }
                var type = $("input[id$=hddMohwType]").val();
                var userID = $("input[id$=hddUserID]").val();

                var dto = {
                    MOHW_TYPE: type
                    , SUBJECT: $("#txtSubject").val()
                    , CREATOR_ID: userID
                };

                dto = $.extend(dto, fn_GetFilter());
                if (type == "KRPIA") dto = $.extend(dto, fn_GetFilter_krpia());
                console.log(type);
                console.log(dto);
                $('body').waitMe('show');
                $.ajax({
                    url: REPORT_SERVICE_URL + "/InsertMohwReport",
                    type: "POST",
                    data: JSON.stringify(dto),
                    //dataType: "json",
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {
                        $('body').waitMe('hide');
                        fn_showInformation({ title: "Report가 생성되었습니다.", message: "" }).done(function () {
                            $.get(REPORT_SERVICE_URL + "/CreateXlsMohwReport/" + data + "/" + type + "/" + userID);
                            $("#txtSubject").val('');
                            $("#modalReport").modal("hide");
                        });
                    },
                    error: function (error) {
                        $('body').waitMe('hide');
                        fn_showError({
                            message: error.responseText
                        }).done(function () {
                            $("#modalReport").modal("hide");
                        });
                    },
                });
            }
            catch (ex) {
                fn_showError({ message: ex.message });
            }

        });
    }
    catch (ex) {
        fn_showError({ message: ex.message });
    }
});