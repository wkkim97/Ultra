<%@ Page Title="" Language="C#" MasterPageFile="~/Master/Ultra_Event.master" AutoEventWireup="true" CodeFile="Donation.aspx.cs" Inherits="Pages_Event_Donation" %>

<%@ Register Src="~/Common/Controls/ProductSearcher.ascx" TagPrefix="uc1" TagName="ProductSearcher" %>
<%@ Register Src="~/Common/Controls/ProductSearchModal.ascx" TagPrefix="uc1" TagName="ProductSearchModal" %>

<asp:Content ID="Content1" ContentPlaceHolderID="cpHolderHead" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="cpHolderForm" Runat="Server">
    <table class="table form-group-sm">
        <tbody>
            <tr>
                <th>Subject</th>
                <td>
                    <input id="txtSubject" type="text" class="form-control" />
                </td>
            </tr>
            <tr>
                <th>Type</th>
                <td> 
                    <div class="checkbox" style="padding:0px;">
                        <label><input name="hchkType" type="checkbox" value="MD" /><span>Monertary Donation</span></label>
                        <label><input name="hchkType" type="checkbox" value="PD" /><span>Product Donation</span></label>
                        <label><input name="hchkType" type="checkbox" value="ETC" /><span>Other Goods and Services</span></label>
                    </div>  
                </td>
            </tr>
            <tr>
                <th>Value[KRW]</th>
                <td>
					<input id="htxtValue" type="text" class="form-control number" />
                    <br />
					<label>- 물품(제품)을 기부하는 경우, 기부금액을 시장가(권장소비자가)로 신청합니다.</label>
					<label>- EUR 50,000초과 시 Global Subgroup Head 또는 / 그리고 Management Board Chairperson of Bayer AG의 승인필요(승인요청서류)</label>
                </td>
            </tr>
            <tr>
                <th>Purpose</th>
                <td> 
                    <div class="checkbox" style="padding:0px">
                        <label><input name="hchkPurpose" type="checkbox" value="SE" />Science & Education</label>
                        <label><input name="hchkPurpose" type="checkbox" value="HS" />Health & Social</label>
                        <label><input name="hchkPurpose" type="checkbox" value="SCM" />Sports, Culture, Municipal</label>
                    </div>  
                </td>
            </tr>
            <tr>
                <th>Explanation</th>
                <td><textarea id="taExplanation" class="form-control" rows="3" style="width: 100%"></textarea></td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpHolderEtc" Runat="Server">
	<div id="divRI_Main" class="panel panel-dashboard">
        <div class="panel-heading">
            <h3 class="panel-title">Recipient Information</h3>
        </div>
        <div id="divInputEvent" class="panel-body">
			<table class="table form-group-sm">
				<tbody>
					<tr>
						<th>Recipient</th>
						<td colspan="3">
							<input id="txtRecipient" type="text" class="form-control" />
						</td>
					</tr>
					<tr>
						<th>Address</th>
						<td colspan="3">
							<input id="txtAddress" type="text" class="form-control" />
						</td>
					</tr>
					<tr>
						<th>Tel.</th>
						<td>
							<input id="txtTel" type="text" class="form-control" />
						</td>
						<th>eMail</th>
						<td>
							<input id="txtEMail" type="text" class="form-control" />
						</td>
					</tr>
					<tr>
						<th>기부단체의 적격성 여부</th>
						<td colspan="3"> 
							<div class="radio" style="padding:0px">
								<label><input name="rdoEligibility" type="radio" value="Y" />YES</label>
								<label><input name="rdoEligibility" type="radio" value="N" />NO</label>
							</div>  
						</td>
					</tr>
					<tr>
						<th>기부 이후 영수증 타입</th>
						<td colspan="3"> 
							<div class="radio" style="padding:0px">
								<label><input name="rdoReceipt" type="radio" value="EDR" />적격기부영수증</label>
								<label><input name="rdoReceipt" type="radio" value="TB" />세금계산서</label>
								<label><input name="rdoReceipt" type="radio" value="ETC" />기타</label>
							</div>  
						</td>
					</tr>
					<tr>
						<th>Comment</th>
						<td colspan="3"><textarea id="taComment" class="form-control" rows="3" style="width: 100%"></textarea></td>
					</tr>
					<tr>
						<th>Category</th>
						<td colspan="3">
							<div class="checkbox" style="padding:0px">
								<label><input name="hchkCategory" type="checkbox" value="HPO" />Healthcare Professinal Organization</label>
								<label><input name="hchkCategory" type="checkbox" value="EO" />Educational Organization</label>
								<label><input name="hchkCategory" type="checkbox" value="CO" />Charity Organization</label>
								<label><input name="hchkCategory" type="checkbox" value="ETC" />Others</label>
							</div>  
						</td>
					</tr>
				</tbody>
			</table>
        </div>
    </div>
    <div id="hdivProductArea" style="display:none;">
        <div class="box-panel"  style="margin-bottom: 20px;display:none;" id="hdivAddProduct">
            <button type="button" class="btn-panel-close"><i class="fa fa-close"><span class="tts">Close</span></i></button>
            <div class="row row-md">
                <div class="col-md-12">
                    <h3 class="panel-title">Add Data(Donation)</h3>
                    <table class="table-default" id="tblAddData">
                        <tbody>
                            <tr>
                                <th scope="row" style="text-align: left">BU</th>
                                <th scope="row" style="text-align: left">Product</th>
                                <th scope="row" style="text-align: left">Qty</th>
                                <th scope="row" style="text-align: left">Price</th>
                                <th scope="row" style="text-align: left">Amount</th>
                            </tr>
                            <tr>
                                <td>
									<div class="radio" style="padding:0px">
										<label><input name="rdoBU" type="radio" value="HH" checked="checked" />HH</label>
										<label><input name="rdoBU" type="radio" value="WH" />WH</label>
										<label><input name="rdoBU" type="radio" value="SM" />SM</label>
										<label><input name="rdoBU" type="radio" value="R" />RAD</label>
										<label><input name="rdoBU" type="radio" value="CC" />CH</label>
										<label><input name="rdoBU" type="radio" value="AH" />AH</label>
									</div>
                                </td>
                                <td>
                                    <uc1:ProductSearcher runat="server" ID="ProductSearcher" />
                                </td>
                                 <td>
                                    <input type="text" class="form-control number" id="txtQty" />
                                </td>
                                <td>
                                    <input type="text" class="form-control number" id="txtPrice" disabled="disabled" />
                                </td>
                                <td>
                                    <input type="text" class="form-control number" id="txtAmount" disabled="disabled" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <div class="btnset">
                        <button type="button" class="btn-sm btn-navy fl" id="btnDeleteProduct" style="display:none;"><i class="fa fa-trash-o"></i>Delete</button>
                        <button type="button" class="btn-sm btn-red fr" id="btnSaveProduct"><i class="fa fa-floppy-o"></i>SAVE</button>
                    </div>
                </div>
            </div>
        </div>
        <div class="box-panel" style="margin:0px 0px 20px 0px;">
            <div class="row row-md">
                <div class=" col-md-12">
                    <div class="panel-heading">
                        <button type="button"  class="btn btn-sm btn-wyellow fr" id="btnAddProduct"><i class="fa"></i>Add</button>
                        <button type="button"  class="btn btn-sm btn-darkgray fr" id="btnPrint"><i class="fa"></i>거래명세서</button>
                        <h3 class="panel-title">Product Information(Donation)</h3>
                    </div>
                    <div class="panel-body" >
                        <div id="jsGridProduct"></div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <uc1:ProductSearchModal runat="server" ID="ProductSearchModal" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="cpHolderBottom" Runat="Server">
    <script type="text/javascript" src="/ultra/Scripts/Pages/Event/Donation.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/Common/ProductSearch.js"></script>
</asp:Content>

