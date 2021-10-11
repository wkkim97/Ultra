/*
    https://docs.microsoft.com/en-us/aspnet/web-api/overview/testing-and-debugging/troubleshooting-http-405-errors-after-publishing-web-api-applications

*/
$(function () {
    'use strict';
    var userID = $("input[id$=hddUserID]").val();
    var excelType; 

    $('.fileupload')
        .bind('fileuploadadd', function (e, data) {
            //파일을 추가할때
            //var currentfiles = [];
            //data.files = $.map(data.files, function (file, i) {
            //    if ($.inArray(file.DisplayName, currentfiles) >= 0) {
            //        fn_showError({
            //            title: "confirm",
            //            message: "File(" + file.DisplayName + ") exists.",
            //        });
            //        return null;
            //    }
            //    return file;
            //});
        })
        .bind('fileuploadsubmit', function (e, data) {
            //첨부파일이 Submit될때
            //var excelType = $(this).closest('.upload-excel').data('excel-type');
            excelType = $(this).closest('.upload-excel').data('excel-type');

            data.formData = {
                UserID: userID,
                ExcelType: excelType,
            };
        })
        .fileupload({
            url: UPLOAD_EXCEL_HANDLER_URL,
            dataType: 'json',
            send: function (e, data) {
                console.log(data);
                return true;
            },
            done: function (e, data) {
                var target = e.target;
                if (data.result.files.length > 0) {
                    var file = data.result.files[0];
                    var $input = $(target).closest('.upload-excel').find('.input-file-name');
                    $input.val(file.name);
                    $input.data("excel-file", JSON.stringify(file));
                    var filePath = file.filePath;

                    //alert(excelType);
                    $("#result_concur").html("");
                    $("#result_yourdoces").html("");
                    if (excelType == "PaymentConcur")
                    {
                        fn_ReadEventPaymentUploadConcur(filePath); //ExcelUpload.js에 있음 
                    } else {
                        fn_ReadEventPaymentUploadYourDoces(filePath); //ExcelUpload.js에 있음 
                    }
                }
            },
            progressall: function (e, data) {
                var progress = parseInt(data.loaded / data.total * 100, 10);
                console.log(progress);
                //$(this).closest('.attach').find('#progress .progress-bar').css(
                //    'width',
                //    progress + '%'
                //);
            },
            fail: function (e, data) {
                console.log(data);
            }
        });

});
