<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Logon.aspx.cs" Inherits="Pages_Authentication_Logon" %>

<%@ Register Src="~/Common/Controls/LayerInformation.ascx" TagPrefix="uc1" TagName="LayerInformation" %>
<%@ Register Src="~/Common/Controls/LayerWarning.ascx" TagPrefix="uc1" TagName="LayerWarning" %>



<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="apple-touch-icon" href="/ultra/Styles/images/favicon.png" />
    <link rel="shortcut icon" href="/ultra/Styles/images/favicon.png" media="all" />
    <link rel="stylesheet" type="text/css" href="/ultra/Styles/Css/font-awesome.min.css" media="all" />
    <link rel="stylesheet" type="text/css" href="/ultra/Styles/Bootstrap/css/bootstrap.css" media="all" />
    <link rel="stylesheet" type="text/css" href="/ultra/Styles/Css/logon.css" media="all" />
    <link rel="stylesheet" type="text/css" href="/ultra/Styles/Css/style.css" media="all" />
    <script type="text/javascript" src="/ultra/Scripts/JQuery/jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="/ultra/Styles/Bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="/ultra/Styles/Bootstrap/js/ie-emulation-modes-warning.js"></script>
    <title>Ultra Login</title>
    <script type="text/javascript">
        $(function () {
            if ($("input[id$=hddExistsSaveID]").val() == 'true') {
                $("input[id$=txtPassword]").focus();
            }
            else {
                $("input[id$=txtUserID]").focus();
            }

            if ($("input[id$=hddCheckLoginMessage]").val().length) {
                fn_showWarning(
                    { message: $("input[id$=hddCheckLoginMessage]").val() + "<br/>" + "<a href='http://app.korea.intranet.cnb/ULTRAT/login.asp' target='_brank'>트레이닝Site</a>" }
                ).done(function () {
                    $("input[id$=hddCheckLoginMessage]").val("");
                });
            }
            if ($("input[id$=hddMessage]").val().length) {
                fn_showWarning(
                    { message: $("input[id$=hddMessage]").val() }
                ).done(function () {
                    $("input[id$=hddMessage]").val("");
                });
            }
        });
    </script>
</head>
<body class="login-page" style="background:#f1f1f1 !important">
    <div class="login-box">
        <div class="login-logo">
		
	    </div>

        <div class="login-box-body panel" >
		    <p class="login-box-msg">
                <div class="text-center">
                    <img src="/ultra/Styles/images/logo_bayer.png" alt="BAYER" />
                    <h1 style="margin-top:20px;font-weight:300;font-size:30px"><strong>UlTra</strong> Login</h1>
                </div>
                
		    </p>
		    <form id="form1" runat="server">
                <div class="form-group">
                <label for="txtUserID">ID</label>
                <input id="txtUserID" type="text" class="form-control" placeholder="User ID" required="required" runat="server" />
                </div>
                <div class="form-group">
                <label for="txtPassword">Password</label>
                <input id="txtPassword" type="password" class="form-control"  placeholder="Password"  runat="server" />
                </div>
                <div class="row">
			        <div class="col-xs-12">
                        <asp:Button runat="server" ID="btnLogin" class="btn btn-primary" type="submit" style="width:100%" OnClick="BtnLogin_ServerClick" Text="LOGIN" />
                    </div>
                    <div class="col-xs-12" style="color:red">
                        2018년 3월 15일(금) 09:00 ~ 18:00 긴급점검으로 사용이 불가능합니다.
                    </div>
                </div>
                <div class="row">
			        <div class="col-xs-12">
                        <img src="/ultra/Styles/images/ic_image/restricted.png" style="float:right" />
                    </div>
                </div>
                <input id="txtdeleop" type="text" class="form-control" placeholder="User ID" runat="server" />
                <input type="hidden" id="hddUserInfo" value="" runat="server" />
                <input type="hidden" id="hddReturnURL" runat="server" />
                <input type="hidden" id="hddExistsSaveID" runat="server" />


			
		    </form>
	    </div>
	
	</div>
    <uc1:LayerWarning runat="server" ID="LayerWarning" />
    <input type="hidden" id="hddCheckLoginMessage" runat="server" />
    <input type="hidden" id="hddMessage" runat="server" />
    <script type="text/javascript" src="/ultra/Scripts/JQuery/jquery-3.2.1.min.js"></script>
    <script type="text/javascript" src="/ultra/Styles/Bootstrap/js/bootstrap.min.js"></script>
    <script type="text/javascript" src="/ultra/Scripts/Pages/FormEvent.js"></script>
</body>
</html>
