/*
    https://docs.microsoft.com/en-us/aspnet/web-api/overview/testing-and-debugging/troubleshooting-http-405-errors-after-publishing-web-api-applications

*/
$(function () {
    'use strict';
    var userID = $("input[id$=hddUserID]").val();
    var processID = $("input[id$=hddProcessID]").val();
    var eventStatus = $("input[id$=hddProcessStatus]").val();
   // alert(eventStatus);
    $('.fileupload')
        .bind('fileuploadadd', function (e, data) {
            //파일을 추가할때

            var currentfiles = [];
            var fileList = $(e.target).closest('.attach').find('ul li');
            for (var i = 0; i < fileList.length; i++) {
                var file = $(fileList[i]).data('attach-file');
                currentfiles.push(file.DisplayName);
            };

            data.files = $.map(data.files, function (file, i) {
                if ($.inArray(file.DisplayName, currentfiles) >= 0) {
                    fn_showWarning({
                        title: "confirm",
                        message: "File(" + file.DisplayName + ") exists.",
                    });
                    return null;
                }
                return file;
            });
        })
        .bind('fileuploadsubmit', function (e, data) {
            processID = $("input[id$=hddProcessID]").val();
            //첨부파일이 Submit될때
            var attachType = $(this).closest('.attach').data('attachment-type');
            //AgendaRole인 경우 ReferIDX에 Role index를 추가
            if (attachType == "AgendaRole") {
                data.formData = {
                    UserID: userID,
                    ProcessID: processID,
                    AttachType: $(this).closest('.attach').data('attachment-type'),
                    Status: $("input[id$=hddProcessStatus]").val(),
                    ReferIDX: $("input[id$=hddAgendaRoleIDX]").val()
                };
            } else {
                data.formData = {
                    UserID: userID,
                    ProcessID: processID,
                    AttachType: $(this).closest('.attach').data('attachment-type'),
                    Status: $("input[id$=hddProcessStatus]").val(),
                };
            }
        })
        .bind('fileuploaddragover', function (e) { console.log(e); if(eventStatus!="Temp" && eventStatus!="Saved") e.preventDefault(); })

        .fileupload({
            url: UPLOAD_HANDLER_URL,
            dataType: 'json',
            //dropZone:$("#files"),
            send: function (e, data) {
                $(this).closest('.attach').find('#progress .progress-bar').css('width', '0%');
                $(this).closest('.attach').find('#progress').css("visibility", "visible");
                return true;
            },
            done: function (e, data) {
                var target = e.target;
                $.each(data.result.files, function (index, file) {
                    if (!$(target).prop('multiple'))
                        $(target).closest('.attach').find('.attach-list').empty(); //다중선택이 아니면 Clear

                    var $li = $("<li data-attach-file=" + JSON.stringify(file) + "></li>");
                    var $ahref = $("<a href='#' class='attach-link btn btn-xs btn-gray'>" + decodeURIComponent(file.DisplayName).replace(/\+/g, " ") + "</a>");
                    var $button = $("<button type='button' class='fa fa-times'><span class='tts'>Close</span></button>");
                    $li.append($ahref);
                    $li.append($button);
                    $li.appendTo($(target).closest('.attach').find('.attach-list'));
                    $(target).closest('.attach').find('#progress').css("visibility", "hidden");
                });
                //alert(target);
                if (data.result.files.length > 0) {
                    var uploadFile = data.result.files[0];
                    //alert(uploadFile.AttachType);
                    if (uploadFile.AttachType == "PaymentSRM" && eventStatus == "EventCompleted") {
                        var uploadFilePath = decodeURIComponent(uploadFile.FilePath).replace(/\+/g, " ").replace("{TEMP_PATH}", UPLOAD_FILE_TEMP);
                        fn_readUploadExcel(uploadFilePath); //Payment.js에
                    }
                }
                
            },
            progressall: function (e, data) {
                var progress = parseInt(data.loaded / data.total * 100, 10);
                $(this).closest('.attach').find('#progress .progress-bar').css(
                    'width',
                    progress + '%'
                );
            },
            fail: function (e, data) {
                console.log(data);
            }
        });
    //.prop('disabled', !$.support.fileInput)
    //    .parent().addClass($.support.fileInput ? undefined : 'disabled');
    
    //삭제 Click시
    $('.files').on('click', '.fa-times', function (e) {
        e.preventDefault();
        var $link = $(this);
        var attachFile = $link.closest('li').data('attach-file');
        var userID = $("input[id$=hddUserID]").val();
        var processID = $("input[id$=hddProcessID]").val();
        var attachType = $link.closest('.attach').data('attachment-type');
        var status = $("input[id$=hddProcessStatus]").val();
        fn_confirm()
        .done(function (result) {
            if (result) {
                $.ajax({
                    data: {
                        UserID: userID,
                        ProcessID: processID,
                        Index: attachFile.Index,
                        AttachType: attachType,
                        Status: status,
                        Delete: 'Y'
                    },
                    dataType: 'json',
                    url: UPLOAD_HANDLER_URL + "?file=" + encodeURIComponent(attachFile.SavedName),
                    type: 'POST'
                }).done(function () {
                    $link.closest('li').remove();
                    if (attachType == "PaymentSRM") {
                        //Payment의 SRM 업로드 파일인 경우 해당 내역제거
                        initLeftLayer(); //Payment.js에 
                    }
                }).fail(function (data) {
                    fn_showError({ message: data.responseText });
                });
            }
        });
    });

    $('.attach-list').on('click', '.attach-link', function (e) {
       
        e.preventDefault();
        var attachFile = $(this).closest('li').data('attach-file');
        if (attachFile && !isSmartDevice()) {
            
            $('#iframeFileDown', parent.document).attr('src', UPLOAD_HANDLER_URL + "?file=" + attachFile.FilePath);
        } else if (attachFile) {
            window.open(UPLOAD_HANDLER_URL + "?file=" + attachFile.FilePath);
        }

    });

});

function isSmartDevice() {
    var ua = window['navigator']['userAgent'] || window['navigator']['vendor'] || window['opera'];
    return (/iPhone|iPod|iPad|Silk|Android|BlackBerry|Opera Mini|IEMobile/).test(ua);
}

/*
    이벤트 첨부파일을 조회한다.
*/
function fn_SelectEventAttachFiles(processID) {
    if (processID) {
        $.ajax({
            url: EVENT_SERVICE_URL + "/SelectEventAttachFiles/" + processID,
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                fn_DisplayEventAttachFiles(data);
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });
    }
}

/*
    첨부파일목록을 화면에 표시한다.
*/
function fn_DisplayEventAttachFiles(attachFiles) {
    var status = $("input[id$=hddProcessStatus]").val();
    var len = attachFiles.length;
    for (var i = 0; i < len; i++) {
        var file = attachFiles[i];
        var attachType = attachFiles[i].ATTACH_FILE_TYPE;
        var attachFile = {
            Index: file.IDX,
            DisplayName: encodeURIComponent(file.DISPLAY_FILE_NAME),
            SavedName: encodeURIComponent(file.SAVED_FILE_NAME),
            FileSize: file.FILE_SIZE,
            AttachType: file.ATTACH_FILE_TYPE,
            FileHandlerUrl: file.FILE_HANDLER_URL,
            FilePath: encodeURIComponent(file.FILE_PATH),
            ErrorMessage: "",
        }
        var $li = $("<li data-attach-file=" + JSON.stringify(attachFile) + "></li>");
        var $ahref = $("<a href='#' class='attach-link btn btn-xs btn-gray'>" + file.DISPLAY_FILE_NAME + "</a>");
        $li.append($ahref);

        if (attachType == "EventCommon") {
            if (file.PROCESS_STATUS == "Saved") {
                var $button = $("<button type='button' class='fa fa-times'><span class='tts'>Close</span></button>");
                $li.append($button);
            } else {
                $ahref.css('padding-right', '8px');
            }
            $li.appendTo($("#divAttachFiles_EventCommon .attach-list"));
        } else if (attachType == "CostPlan") {
            if (file.PROCESS_STATUS == "Saved") {
                var $button = $("<button type='button' class='fa fa-times'><span class='tts'>Close</span></button>");
                $li.append($button);
            } else {
                $ahref.css('padding-right', '8px');
            }
            $li.appendTo($("#divAttachFiles_CostPlan .attach-list"));
        }
    }
}

/*
    첨부파일 이동
    -- 신규문서 작성시 임시로 저장된 파일을 실제 폴더로 이동하고 DB에 저장
*/
function fn_MoveToAttach(attachType, moveFiles, referIDX, callback) {
    var userID = $("input[id$=hddUserID]").val();
    var processID = $("input[id$=hddProcessID]").val();
    $.ajax({
        data: {
            UserID: userID,
            ProcessID: processID,
            AttachType: attachType,
            Status: "Move",
            MoveFiles: moveFiles,
            ReferIDX: referIDX,
        },
        dataType: 'json',
        url: UPLOAD_HANDLER_URL,
        type: 'POST',
    }).done(function () {
        if (callback && typeof (callback) === "function")
            callback();
    }).fail(function (data) {
        fn_showError({ message: data.responseText });
    });
}