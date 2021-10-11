<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Admin.master" AutoEventWireup="true" CodeFile="ExcelUpload.aspx.cs" Inherits="Pages_Admin_ExcelUpload" %>

<%@ Register Src="~/Common/Controls/ExcelFileUploader.ascx" TagPrefix="uc1" TagName="ExcelFileUploader" %>


<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <link rel="stylesheet" href="/ultra/Styles/Css/jquery.fileupload.css" />
    <style>
        .error-message { background: rgba(181, 59, 79, 0.5); }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderContent" runat="Server">
    <ul id="tabExcelUpload" class="nav nav-tabs nav-tabs-fixcenter" role="tablist">
        <li id="tabConcur" runat="server" role="presentation" class="active"><a href="#tpConcur" aria-controls="tpConcur" role="tab" data-toggle="tab">Concur</a></li>
        <li id="tabYourDoces" runat="server" role="presentation"><a href="#tpYourDoces" aria-controls="tpYourDoces" role="tab" data-toggle="tab">Your Doces</a></li>
    </ul>
    <div class="tab-content">
        <div role="tabpanel" class="tab-pane active" id="tpConcur">
            <div class="box-panel">
                <div class="panel panel-dashboard">
                    <h4>Upload Concur</h4>
                    <div class="row">
                        <div class="col-xs-10 col-md-10 col-lg-10">
                            <uc1:ExcelFileUploader runat="server" ID="efuConcur" UploadType="PaymentConcur" />
                        </div>
                        <div class="col-xs-2 col-md-2 col-lg-2 col align-self-end" style="text-align:right">
                            <button id="btnSaveUploadConcur" type="button" class="btn btn-sm btn-red"><i class="fa fa-floppy-o"></i>Save</button>
                        </div>
                    </div>
                    <br />
                    <div class="row" id="result_concur">
                        
                    </div>
                    <div id="jsGridUploadConcur"></div>

                </div>
            </div>
        </div>
        <div role="tabpanel" class="tab-pane" id="tpYourDoces">
            <div class="box-panel">
                <div class="panel panel-dashboard">
                    <h4>Upload YourDoces</h4>
                    <div class="row">
                        <div class="col-xs-10 col-md-10 col-lg-10">
                            <uc1:ExcelFileUploader runat="server" ID="efuYourDoces" UploadType="PaymentYourDoces" />
                        </div>
                        <div class="col-xs-2 col-md-2 col-lg-2 col align-self-end" style="text-align:right">
                            <button id="btnSaveUploadYourDoces" type="button" class="btn btn-sm btn-red"><i class="fa fa-floppy-o"></i>Save</button>
                        </div>
                    </div>
                    <br />
                    <div class="row" id="result_yourdoces"></div>
                    <div id="jsGridUploadYourDoces"></div>
                </div>
            </div>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderBottom" runat="server">
    <script type="text/javascript" src="/ultra/Scripts/JQuery/uploader/vendor/jquery.ui.widget.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/JQuery/uploader/jquery.iframe-transport.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/JQuery/uploader/jquery.fileupload.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/Common/ExcelFileUploader.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/Pages/Admin/ExcelUpload.js"></script>
</asp:Content>

