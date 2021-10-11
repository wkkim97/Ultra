<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileUploader.aspx.cs" Inherits="Template_FileUploader" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <link rel="stylesheet" href="../Styles/Bootstrap/css/bootstrap.min.css" />
    <%--<link rel="stylesheet" href="css/style.css">--%>
    <!-- CSS to style the file input field as button and adjust the Bootstrap progress bars -->
    <link rel="stylesheet" href="../Styles/Css/jquery.fileupload.css" />
</head>
<body>
    <form id="form1" enctype="multipart/form-data">
        <div>
            <span class="btn btn-success fileinput-button">
                <i class="glyphicon glyphicon-plus"></i>
                <span>Select files...</span>
                <!-- The file input field used as target for the file upload widget -->
                <input id="fileupload" type="file" name="files[]" multiple />
            </span>
            <br />
            <br />
            <!-- The global progress bar -->
            <div id="progress" class="progress">
                <div class="progress-bar progress-bar-success"></div>
            </div>
            <!-- The container for the uploaded files -->
            <div id="files" class="files"></div>
        </div>
        <script src="../Scripts/JQuery/jquery-3.2.1.min.js"></script>
        <!-- The jQuery UI widget factory, can be omitted if jQuery UI is already included -->
        <script src="../Scripts/JQuery/uploader/vendor/jquery.ui.widget.js"></script>
        <!-- The Iframe Transport is required for browsers without support for XHR file uploads -->
        <script src="../Scripts/JQuery/uploader/jquery.iframe-transport.js"></script>
        <!-- The basic File Upload plugin -->
        <script src="../Scripts/JQuery/uploader/jquery.fileupload.js"></script>
        <!-- Bootstrap JS is not required, but included for the responsive demo navigation -->
        <script src="../Styles/Bootstrap/js/bootstrap.min.js"></script>
        <script>
            /*jslint unparam: true */
            /*global window, $ */
            $(function () {
                'use strict';
                // Change this to the location of your server-side upload handler:
                var url = '/ultra/Template/FileHandler.ashx';
                $('#fileupload').fileupload({               
                    formData: { UserID: "BKKEK", ProcessID: "E000000038" },
                    url: url,
                    dataType: 'json',
                    done: function (e, data) {
                        $.each(data.result.files, function (index, file) {
                            $('<p/>').text(file.name).appendTo('#files');
                        });
                    },
                    progressall: function (e, data) {
                        var progress = parseInt(data.loaded / data.total * 100, 10);
                        $('#progress .progress-bar').css(
                            'width',
                            progress + '%'
                        );
                    },
                    fail: function (e, data) {
                        console.log(data);
                    }
                }).prop('disabled', !$.support.fileInput)
                    .parent().addClass($.support.fileInput ? undefined : 'disabled');
            });
</script>
    </form>
</body>
</html>
