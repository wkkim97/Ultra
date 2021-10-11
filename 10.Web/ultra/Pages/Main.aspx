<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Main.aspx.cs" Inherits="Pages_main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0" />
    <meta name="format-detection" content="telephone=no, address=no, email=no" />
    <link rel="apple-touch-icon" href="/ultra/Styles/images/favicon.png" />
    <link rel="shortcut icon" href="/ultra/Styles/images/favicon.png" media="all" />
    <link rel="stylesheet" href="/ultra/Styles/Css/font-awesome.min.css" media="all" />
    <link rel="stylesheet" href="/ultra/Styles/Bootstrap/css/bootstrap.css" />
    <link rel="stylesheet" href="/ultra/Scripts/JQuery/jquery-ui-1.12.1.custom/jquery-ui.css" /> 
    <link rel="stylesheet" href="/ultra/Styles/Bootstrap/css/bootstrap-datetimepicker.css" />
    <link rel="stylesheet" href="/ultra/Styles/Css/style.css" />
    <title>Bayer Ultra</title>
</head>

<body id="l_layout">
    <form id="form1" runat="server">
        <header id="header" class="navbar navbar-default navbar-fixed-top">
            <div class="container">
                <div class="navbar-header">
                    <button type="button" class="navbar-toggle collapsed" data-toggle="collapse" data-target="#topFixedNavbar" aria-expanded="false"><span class="sr-only">Toggle navigation</span><span class="icon-bar"></span><span class="icon-bar"></span><span class="icon-bar"></span></button>
                    <!--<a class="navbar-brand" href="home.aspx" target="cont_frame">-->
                    <a class="navbar-brand" href="main.aspx" >
                        <img src="/ultra/Styles/images/logo.png" alt="Ultra" /></a>
                </div>
                <div class="collapse navbar-collapse" id="topFixedNavbar" role="navigation">
                    <ul class="nav navbar-nav gnb">
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">New<span class="caret"></span></a>
                            <ul id="ulEventList" class="dropdown-menu" runat="server"></ul>
                        </li>
                        <li><a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Library<span class="caret"></span></a>
                            <ul id="ulLibrary" class="dropdown-menu" runat="server">
                            </ul>
                        </li>
                        <li><a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Medical<span class="caret"></span></a>
                            <ul id="ulmedical" class="dropdown-menu" runat="server">
                            </ul>
                        </li>
                        <li><a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">Report<span class="caret"></span></a>
                            <ul id="ulReport" class="dropdown-menu" runat="server">
                            </ul>
                        </li>
                        <li><a href="https://bayergroup.sharepoint.com/sites/021843/ultra/default.aspx" target="_blank">FAQ</a>
                        </li>
                    </ul>                    
                    <!-- // .gnb -->
                    <!--<a href="https://bayergroup.sharepoint.com/sites/021843/ultra/default.aspx" target="_blank"><img src="/ultra/Styles/images/question.png" style="margin:10px 0px 0px 10px" /></a>-->
                    <ul class="nav navbar-nav navbar-right">
                        <li class="dropdown">
                            <a href="#" class="dropdown-toggle" data-toggle="dropdown" role="button" aria-haspopup="true" aria-expanded="false">
                                <span id="spanUserName" runat="server">Wookyung Kim</span><span id="spanOrgName" class="visible-lg-inline" runat="server"> |  BKL-CFO-BS-IT-ITAW</span> <span class="ico_account"></span></a>
                            <ul class="dropdown-menu">
                                <li><a href="Authentication/Logout.aspx" ><i class="fa fa-sign-out" aria-hidden="true"></i>Log out</a></li>
                                <li><a href="#" data-toggle="modal" data-target="#Configuration"><i class="fa fa-user" aria-hidden="true"></i>Configuration</a></li>
                                <li id="liAdminLink"><a href="Admin/AdminHome.aspx" target="_blank"><i class="fa fa-cog" aria-hidden="true"></i>Admin</a></li>
                            </ul>
                        </li>
                    </ul>
                    <!-- // .navbar-right -->
                </div>
            </div>
        </header>
        <div class="container">
            <div class="top-btnset">
                <li class="active"><a href="#" class="btn active" data-iframe-src="/ultra/Pages/home.aspx"><span class="glyphicon glyphicon-home" aria-hidden="true"></span>Home</a></li>
            </div>
        </div>
        <iframe id="iframeMain" class="cont_frame" name="cont_frame" title="Content" style="width: 99vw; position: relative;" src="Home.aspx" frameborder="0" allowfullscreen></iframe>
		
		<!-- layer popup -->
		<div class="modal fade" id="Configuration" tabindex="-1" role="dialog">
			<div class="modal-dialog" role="document" style="width:900px;">
				<div class="modal-content">
					<div class="modal-header">
						<button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
						<h4 class="modal-title">Configuration Setting</h4>
					</div>
					<div class="modal-body">
						<div class="left-group">
							<div class="tabpanel-main" role="tabpanel">
								<ul class="nav nav-tabs" role="tablist">
									<li role="presentation" class="active"><a href="#tbDocumentOrder" role="tab" data-toggle="tab" data-tab-type="DocumentOrder">Order Setting<%-- [Document List]--%></a></li>
									<li role="presentation"><a href="#tbDelegation" role="tab" data-toggle="tab" data-tab-type="Delegation">Delegation</a></li>
								</ul>
								<div class="tab-content">
									<div role="tabpanel" class="tab-pane in active" id="tbDocumentOrder">
										<div id="hdivDocumentOrder" class="panel panel-default panel-body">
											<table class="table table-striped">
												<colgroup>
													<col />
													<col style="width:22%;" />
												</colgroup>
												<thead>
													<tr>
														<th scope="row">Document Name</th>
														<th scope="row">Sort Number</th>
													</tr>
												</thead>
												<tbody id="htblDocOrderList" runat="server">
												</tbody>
											</table>
											<div class="modal-footer">
												<button type="button" onclick="SaveDocumentOrder()" class="btn btn-lg btn-red btn-block">Save</button>
											</div>
										</div>
									</div>
									<!-- // DocumentOrder -->
									<div role="tabpanel" class="tab-pane" id="tbDelegation">
										<div id="hdivDelegation" class="panel panel-default panel-body">
											<div class="row">
												<div class="left-group col-lg-5">
													<table id="htblDelegation" class="table table-striped">
														<colgroup>
															<col style="width:22%;" />
															<col style="width:22%;" />
															<col />
														</colgroup>
														<thead>
															<tr>
																<th scope="row">From Date</th>
																<th scope="row">To Date</th>
																<th scope="row">Delegation to</th>
															</tr>
														</thead>
														<tbody>
														</tbody>
													</table>
												</div>
												<div class="left-group col-lg-7">
													<div class="btnset">
														<button id="btnDelDelegation" type="button" class="btn btn-sm btn-navy fl" onclick="DelDelegation();"><i class="fa fa-trash-o"></i>Delete</button>
														<button id="btnReset" type="button" class="btn btn-sm btn-gray fl" style="margin-left: 3px;" onclick="resetDelegation();"><i class="fa fa-close"></i>Reset</button>
														<button id="btnAddDelegation" type="button" class="btn btn-sm btn-red fr" onclick="AddDelegation();"><i class="fa fa-floppy-o"></i>Save</button>
													</div>
													<table class="table table-striped">
														<colgroup>
															<col style="width:80px;" />
															<col />
														</colgroup>
														<tbody>
															<tr>
																<th>Delegation to</th>
																<td>
																	<select id="optDelegationTo" class="form-control">
																	</select>
																</td>
															</tr>
															<tr>
																<th>Period</th>
																<td>
																	<div class="row">
																		<div class="col-lg-5" style="padding-right:0px;">
																			<div class="input-group date" data-date="">
																				<input id="lb_StartDate" class="form-control" size="10" type="text" placeholder="Start Date" readonly="readonly" />
																				<span class="input-group-addon form_datehour"><span class="fa fa-calendar-o" id="hbtnDocStart"></span></span>
																			</div>
																		</div>
																		<div class="col-lg-1" style="padding-right:0px;">
																			~
																		</div>
																		<div class="col-lg-5" style="padding-right:0px;">
																			<div class="input-group date" data-date="">
																				<input id="lb_EndDate" class="form-control" size="10" type="text" placeholder="End Date" readonly="readonly" />
																				<span class="input-group-addon form_datehour"><span class="fa fa-calendar-o" id="hbtnDocEnd"></span></span>
																			</div>
																		</div>
																	</div>
																</td>
															</tr>
															<tr>
																<th>Description</th>
																<td>
																	<input id="txtDescription" type="text" class="form-control" />
																</td>
															</tr>
														</tbody>
													</table>
													<input type="hidden" id="hhdSelectDelegation" runat="server" />
												</div>
											</div>
										</div>
									</div>
									<!-- // Delegation -->
								</div>
								<!-- // .tab-content -->
							</div>
							<!-- // .tabpanel -->
						</div>
					</div>
				</div>
			</div>
		</div>
		<!-- // layer popup -->

		<div style="display:none;">
			<input type="hidden" id="hhdUserID" runat="server" />
            <input type="hidden" id="hhdAddTabPage" runat="server" />
            <input type="hidden" id="hhdAddTabEventId" runat="server" />
            <input type="hidden" id="hhdAddTabTitle" runat="server" />
            <input type="hidden" id="hhdAddTabProcessId" runat="server" />
            <input type="hidden" id="hddHomeSelectedTopTab" runat="server" /> <!-- 홈페이지 상단 선택된 Tab -->
		</div>


        <script type="text/javascript" src="/ultra/Scripts/JQuery/jquery-3.2.1.min.js"></script>
        <script type="text/javascript" src="/ultra/Scripts/JQuery/jquery-ui-1.12.1.custom/jquery-ui.min.js"></script>
        <script type="text/javascript" src="/ultra/Styles/Bootstrap/js/bootstrap.js"></script>
		<script type="text/javascript" src="/ultra/Styles/Bootstrap/js/bootstrap-datetimepicker.min.js"></script>
        <script type="text/javascript" src="/ultra/Scripts/Pages/Main.js"></script>
        <script type="text/javascript" src="/ultra/Scripts/JQuery/cookie.js"></script>
        <iframe id="iframeFileDown" width="0" height="0"></iframe>
    </form>
</body>
</html>
