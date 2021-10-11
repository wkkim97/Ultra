<%@ Control Language="C#" AutoEventWireup="true" CodeFile="CreateReport.ascx.cs" Inherits="Pages_Report_MOHW_CreateReport" %>

 <div class="modal fade" id="modalReport"  tabindex="-1" role="dialog">
    <div class="modal-dialog" role="document" id="modal-CreateReport">
        <div class="modal-content modal-Report">
            <div class="modal-header">
                <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
                <h4 class="modal-title"  >Create Report</h4>
            </div>
            <div class="modal-body">
                <div class="modal-table">
                    <table class="write">
                        <colgroup>
                            <col style="width: 22%" />
                            <col />
                        </colgroup>
                        <tbody>
                            <tr >
                                <th scope="row">Subject</th>
                                <td>
                                    <input id="txtSubject" class="form-control" type="text" />
                                </td>
                            </tr>
                            <tr id="trReturnComplete">
                                <th scope="row">생성일자</th>
                                <td>
                                    <span id="spanCreateDate"></span>
                                </td>
                            </tr> 
                        </tbody>
                    </table>
                </div>
            </div> 
            <div class="modal-footer"> 
                <button type="button" id="btnCreateReport"  data-loading-text="<i class='fa fa-circle-o-notch fa-spin'></i> Processing" class="btn btn-red fr">Save</button>
            </div>
        </div>
    </div>
</div>
<script src="/ultra/Scripts/Pages/Report/CreateReport.js"></script>
