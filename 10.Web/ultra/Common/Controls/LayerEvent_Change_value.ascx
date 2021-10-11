<%@ Control Language="C#" AutoEventWireup="true" CodeFile="LayerEvent_Change_value.ascx.cs" Inherits="Common_Controls_LayerEvent_Change_value" %>
<div id="layer_change_value" class="modal fade" tabindex="-1" role="dialog">
    <div class="modal-dialog modal-alert" role="document">
        <div class="modal-content">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i aria-hidden="true" class="fa fa-times"></i></button>
            </div>
            <div class="modal-body">
               
                <div class="row freetextfld" style="width:80%;margin-left:10px" >
                  <div class="form-group">
                      <label>New Value</label>
                      <input class="form-control" id="new_value" type="text" />
                  </div>
                </div>

                <div class="row datefld" style="width:80%;margin-left:10px" >
                  <div class="form-group">
                      <label>New Value</label>
                      <div class="input-group date form-date-hour-min form_cha_DateTime" data-date="" data-date-format="yyyy-mm-dd hh:ii:ss" data-link-field="cha_dtDateTime">
                            <input id="cha_datDateTime" class="form-control" size="16" type="text" value="" readonly />
                            <span class="input-group-addon"><span class="fa fa-times"></span></span>
                            <span class="input-group-addon"><span class="fa fa-clock-o"></span></span>
                      </div>
                      <input type="hidden" id="cha_dtDateTime" value="" />
                  </div>
                </div>

                <div>
                  <div class="form-group">
                      <label>Reason</label>
                      <input class="form-control" id="cha_reason" type="text" />
                  </div>
                </div>


                <span class="span-message">변경 하시겠습니까?</span>                    
                
            </div>
            <div class="modal-footer">
                <button id="btnConfirm" type="button" class="btn btn-sm btn-danger">Yes</button>
                <button id="btnCancel" type="button" class="btn btn-sm btn-darkgray">No</button>
            </div>
        </div>
    </div>
</div>
