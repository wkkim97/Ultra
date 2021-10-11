<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Payment.ascx.cs" Inherits="Pages_Medical_Controls_Payment" %>
 
<div class="modal fade payment-modal" id="divPaymentModal" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h3 class="table-title">Payment</h3>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
            </div>
            <div class="modal-body">
                     <div class="row row-sm">
            <div class="form-group col-sm-6 col-md-6">
                <label for="lb_">지급비용</label>
                <input type="text" class="form-control" id="txtAmount" style="text-align: right;" />
            </div>
            <div class="form-group col-sm-6 col-md-6">
                <label for="lb_">비용지급일자</label>
                <div class="input-group date form_datetime" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtPaymentDate">
                    <input type="text" id="dtPaymentDate" class="form-control" />
                    <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                </div>
            </div>
            <div class="form-group col-sm-6 col-md-6">
                <label for="lb_">비용지급 방법</label>
                <div class="radio">
                    <label>
                        <input name="lb_role1" type="radio" value="PO" />PO(SRM)</label>
                    <label>
                        <input name="lb_role1" type="radio" value="NPO" />Non-PO(Your-Doces)</label>
                </div>
            </div>
            <div class="form-group col-sm-6 col-md-8">
                <label for="lb_">Evidence No.</label>
                <input type="text" class="form-control" id="txtEvidenceNo" />
            </div>
            <div class="col-sm-12">
                <label for="lb_">Comment</label>
                <textarea class="form-control" id="taComment"></textarea>
            </div>
        </div> 
            </div>
            <div class="modal-footer">
                <button type="button" class="btn btn-sm btn-navy" id="btnPaymentDelete" style="display:none;">Delete</button>
                <button type="button" class="btn btn-sm btn-red" id="btnPaymentSave"><i class="fa fa-floppy-o"></i>SAVE</button>
                <button type="button" class="btn btn-sm btn-navy" data-dismiss="modal">Cancel</button>
                <input type="hidden" id="hhdSampleType" />
            </div>
        </div>
    </div>
</div>
 
<input type="hidden" id="hhdPaymentIdx" />
 
<script src="../../../Scripts/Pages/Medical/Payment.js"></script>
