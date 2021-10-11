<%@ Control Language="C#" AutoEventWireup="true" CodeFile="ExcelFileUploader.ascx.cs" Inherits="Common_Controls_ExcelFileUploader" %>

<div class="row">
    <div class="col-lg-12 col-sm-12 col-12">
        <div class="input-group upload-excel" data-excel-type="<%= UploadType %>">
            <label class="input-group-btn ">
                <span class="btn btn-sm btn-navy fileinput-button">
                    <i class="fa fa-file-excel-o"></i>
                    <span>Excel</span>
                    <input class="fileupload" id="fileupload" type="file" name="files" accept=".xlsx" style="display: none;" />
                </span>
            </label>
            <input type="text" class="form-control input-sm input-file-name" readonly>
        </div>
    </div>

</div>
