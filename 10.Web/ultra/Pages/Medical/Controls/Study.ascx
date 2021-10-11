<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Study.ascx.cs" Inherits="Pages_Medical_Study" %>
	<div class="col-sm-9">
		<div class="box-panel">
			<form action="#">
			<div class="row row-sm">
				<div class="form-group col-sm-6 col-md-4">
					<label for="lb_">Category</label>
					<select class="form-control" id="selCategory" runat="server"> 
					</select>
				</div>
				<div class="form-group col-sm-6 col-md-4">
					<label for="lb_">Status</label>
					<select class="form-control" id="selStatus" runat="server" > 
					</select>
				</div>
				<div class="form-group col-sm-6 col-md-4">
					<label for="lb_">Team</label>
					<select class="form-control" id="selTeam" runat="server"> 
					</select>
				</div>
				<div class="form-group col-sm-6 col-md-8">
					<label for="lb_">Title</label>
					<input type="text" class="form-control" id="txtTitle">
				</div>
				<div class="form-group col-sm-6 col-md-4">
					<label for="lb_">Impact No.</label>
					<input type="text" class="form-control" maxlength="5" onkeyup="this.value=this.value.replace(/[^\d]/,'')" id="txtImpactNo">
				</div>
				<div class="form-group col-sm-6 col-md-4" style="display:none;">
					<label for="lb_">Cost Information</label>
					<input type="text" class="form-control" id="txtCostInformation">
				</div>
                <div class="form-group col-sm-6 col-md-4">
					<label for="lb_">Type <a class="btn-tooltip" style="margin-left:5px" href="#tooltip_type"><i class="fa fa-question-circle"></i></a></label>
                    <div class="layer-tooltip" id="tooltip_type" style="display: none;">
                        <dl>
                            <dt>Type</dt>
                            <dd>
                                <ul>
                                    <li><span class="num">1</span> 법 제34조 제1항 및 제7항에 따라 식품의약품안전처장의 임상시험계획 승인을 받은 임상시험</li>
                                    <li><span class="num">2</span> 「의약품등의 안전에 관한 규칙」별표4제6호에 따라 임상시험심사위원회의 임상시험계획 승인을 받은 임상시험</li>
                                    <li><span class="num">3</span> 해당 요양기관에 설치된 관련 위원회의 사전 승인을 받은 비임상시험(동물실험 또는 실험실 실험 등)</li> 
                                </ul>
                            </dd>
                        </dl>
                        <button class="close" type="button"><i class="fa fa-times" aria-hidden="true"></i></button>
                    </div> 
					<select class="form-control" id="selType" style="display:inline" >
                        <option value="1">1</option>
                        <option value="2">2</option>
                        <option value="3">3</option>
                    </select> 
				</div>
				<div class="form-group col-sm-6 col-md-4">
					<label for="lb_">승인번호</label>
					<input type="text" class="form-control" maxlength="20" id="txtApprovalNo">
				</div>
				<div class="form-group col-sm-6 col-md-4" >
					<label for="lb_">승인일자</label> 
                    <div class="input-group date form_datetime" data-date="" data-date-format="yyyy-mm-dd" data-link-field="dtApprovalDate">
                        <input id="dtApprovalDate" class="form-control"  size="10" readonly type="text" /> 
                        <span class="input-group-addon"><span class="fa fa-clock-o"></span></span> 
                    </div>
				</div>
				<div class="col-sm-6" style="display:none;">
					<dl class="study-files">
						<dt>Product</dt>
						<dd>
							<ul class="attach-list" style="overflow: visible;" >
                                <textarea id="taProduct" class="example" rows="1" style="width: 100%" ></textarea>
							</ul>
							<a href="#" class="btn-more"><i class="fa fa-plus"></i></a>
						</dd>
					</dl>
				</div>
				<div class="col-sm-12">
					<dl class="study-files">
						<dt>Editor</dt>
						<dd>
							<ul class="attach-list" style="overflow: visible;" > 
                                <textarea id="acbReviewer"  class="example" rows="1" style="width: 100%" ></textarea>
							</ul>
							<a href="#" class="btn-more"><i class="fa fa-plus"></i></a>
						</dd>
					</dl>
				</div>
			</div>
			<div class="btnset">
				<button type="button" class="btn btn-sm btn-navy fl" id="btnDelete"><i class="fa fa-trash-o"></i> Delete</button>
				<button type="button" class="btn btn-sm btn-red fr" id="btnSave"><i class="fa fa-floppy-o"></i> Save</button>
			</div>
			</form>
		</div>
	</div>
	<div class="col-sm-3">
		<dl class="meta-info">
			<dt>Author</dt>
			<dd id="ddAuthor"></dd>
			<dt>Created Date</dt>
			<dd id="ddCreateDate"></dd>
			<dt>Last Modified</dt>
			<dd ><strong id="strModifiedDate"></strong><br/><span id="hspanModifyName"></span></dd>   
		</dl>
	</div>