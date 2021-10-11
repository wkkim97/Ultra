<%@ Control Language="C#" AutoEventWireup="true" CodeFile="IMP.ascx.cs" Inherits="Pages_Medical_Controls_IMP" %>
 
<div class="modal fade IMP-modal" id="divIMPModal" tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <h3 class="table-title">IMP</h3>
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
            </div>
            <div class="modal-body">
                <div class="row">
                    <div class="form-group col-sm-6 col-md-6">
                        <label for="lb_">Order No.</label>
                        <input type="text" class="form-control" id="orderNo" style="text-align: left;" />
                    </div>
                    <div class="form-group col-sm-6 col-md-6">
                        <label for="lb_">Date</label>
                        <div class="input-group date form_datetime" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtIMPDate">
                            <input type="text" id="dtIMPDate" class="form-control" />
                            <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-sm-6 col-md-6">
                        <label for="lb_">Category</label>
                        <div class="radio">
                            <label>
                                <input name="lb_role0" type="radio" value="Shipped" />Shipped</label>
                            <label>
                                <input name="lb_role0" type="radio" value="Returned" />Returned</label>
                        </div>
                    </div>
                    <div class="form-group col-sm-6 col-md-6">
                        <label for="lb_">Airbill No.</label>
                        <input type="text" class="form-control" id="airbillNo" style="text-align: left;" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-sm-12 col-md-12">
                        <label for="lb_">IMP</label>
                        <input type="text" class="form-control" id="IMPtxt" style="text-align: left;" />
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-sm-6 col-md-6">
                        <label for="lb_">Dose</label>
                        <input type="text" class="form-control" id="DoseNo" style="text-align: right;" />
                    </div>
                    <div class="form-group col-sm-6 col-md-6">
                        <label for="lb_">Unit</label>
                        <div class="radio">
                            <label>
                                <input name="lb_role3" type="radio" value="mg" />mg</label>
                            <label>
                                <input name="lb_role3" type="radio" value="g" />g</label>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="form-group col-sm-6 col-md-6">
                        <label for="lb_">Qty</label>
                        <input type="text" class="form-control" id="QtyNo" style="text-align: right;" />
                    </div>
                    <div class="form-group col-sm-6 col-md-6">
                        <label for="lb_">Type</label>
                        <div class="radio">
                            <label>
                                <input name="lb_role2" type="radio" value="vial" />vial</label>
                            <label>
                                <input name="lb_role2" type="radio" value="tab" />tab</label>
                            <label>
                                <input name="lb_role2" type="radio" value="cap" />cap</label>
                        </div>
                    </div>
                </div>
                <div class="row">
                    <div class="col-sm-12">
                        <label for="lb_">Comment</label>
                        <textarea class="form-control" id="taComment_imp"></textarea>
                    </div>
                </div>
                   
                 
            </div> 
            
            <div class="modal-footer">
                <button type="button" class="btn btn-sm btn-navy" id="btnIMPDelete" style="display:none;">Delete</button>
                <button type="button" class="btn btn-sm btn-red" id="btnIMPSave"><i class="fa fa-floppy-o"></i>SAVE</button>
                <button type="button" class="btn btn-sm btn-navy" data-dismiss="modal">Cancel</button>
                
            </div>
        </div>
    </div>
</div>
 
<input type="hidden" id="hhdIMPIdx" />
 
<script src="../../../Scripts/Pages/Medical/IMP.js"></script>
