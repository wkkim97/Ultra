<%@ Control Language="C#" AutoEventWireup="true" CodeFile="EventCostPlan.ascx.cs" Inherits="Pages_Event_Controls_EventCostPlan" %>
<%--<%@ Register Src="~/Common/Controls/FileUploader.ascx" TagPrefix="uc1" TagName="FileUploader" %>--%>

<div id="divInputCostPlan" class="box-panel">
    <div class="tab-cont-area row row-md">
        <div class="write-area">
            <div class="box-panel">
                <form action="#">
                    <div class="form-group">
                        <label for="lb_category">Category</label>
                        <select id="lb_category" class="form-control" runat="server">
                        </select>
                    </div>
                    <div class="form-group">
                        <label for="lb_desc">Description</label>
                        <textarea id="lb_desc" rows="4" class="form-control"></textarea>
                    </div>
                    <div class="row row-sm">
                        <div class="form-group col-xs-3">
                            <label for="lb_qty">Qty</label>
                            <input type="text" id="lb_qty" class="form-control number" value="0" />
                        </div>
                        <div class="form-group col-xs-4">
                            <label for="lb_price">Price</label>
                            <input type="text" id="lb_price" class="form-control number" value="0" />
                        </div>
                        <div class="form-group col-xs-5">
                            <label for="lb_amount">Amount</label>
                            <input type="text" id="lb_amount" class="form-control" readonly value="0" />
                        </div>
                    </div>
                    <div class="btnset">
                        <button type="button" class="btn btn-sm btn-navy fl" id="btnDeleteCostPlan"><i class="fa fa-trash-o"></i>Delete</button>
                        <button type="button" class="btn btn-sm btn-gray fl" id="btnResetCostPlan" style="margin-left: 3px;"><i class="fa fa-close"></i>Reset</button>
                        <button type="button" class="btn btn-sm btn-red fr" id="btnSaveCostPlan"><i class="fa fa-floppy-o"></i>Save</button>
                    </div>
                </form>
                <button type="button" class="btn-panel-close"><i class="fa fa-close"><span class="tts">Close</span></i></button>
            </div>
            <!-- // .box-panel -->
        </div>
        <!-- // .write-area -->
        <div class="list-area">
            <button type="button" class="btn-panel-open"><i class="fa fa-plus-square"><span class="tts">Open</span></i></button>
            <div class="row">
                <div class="col-xs-8">
                    <div id="divFMVGuideLink" style="margin-top:10px;">
                        <a href="https://bayergroup.sharepoint.com/sites/021843/ultra/ulink/FMV%20Guideline.pdf?Web=1" target="_blank">FMV Guide</a>
                    </div>
                </div>
                <div class="col-xs-4">
                    <p class="total">Total <strong><span id="totalCostPlan">0</span></strong></p>
                </div>
            </div>
            <div id="jsGridCostPlan"></div>
            <%--<uc1:FileUploader runat="server" ID="FileUploader" />--%>
            <div id="divAttachFiles_CostPlan" class="attach" data-attachment-type="CostPlan">
                <div class="attach-heading">
                    <h3 class="attach-title">Attachment</h3>
                    <span class="btn btn-sm btn-navy fileinput-button">
                        <i class="fa fa-paperclip"></i>
                        <span>Attachment</span>
                        <!-- The file input field used as target for the file upload widget -->
                        <input class="fileupload" id="fileupload" type="file" name="files[]" multiple />
                    </span>
                    <!-- The global progress bar -->
                    <!-- The container for the uploaded files -->
                </div>
                <div id="progress" class="progress" style="height: 7px !important; display: none">
                    <div class="progress-bar progress-bar-success">
                    </div>
                </div>
                <div id="files" class="files">
                    <ul class="attach-list"></ul>
                </div>
            </div>
            <!-- // .attach -->
        </div>
        <!-- // .list-area  -->
    </div>
    <!-- // .row-md -->
    <input type="hidden" id="hddCostPlanIDX" value="0" />
</div>
<!-- // .box-panel -->
