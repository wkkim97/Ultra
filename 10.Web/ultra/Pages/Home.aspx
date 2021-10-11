<%@ Page Language="C#" AutoEventWireup="true" CodeFile="home.aspx.cs" Inherits="Pages_Home" %>

<%@ Register Src="~/Common/Controls/LayerInformation.ascx" TagPrefix="uc1" TagName="LayerInformation" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta http-equiv="X-UA-Compatible" content="IE=edge" />
    <meta name="viewport" content="width=device-width, initial-scale=1.0, minimum-scale=1.0" />
    <meta name="format-detection" content="telephone=no, address=no, email=no" />
    <title></title>
    <link rel="stylesheet" type="text/css" href="/ultra/Styles/Css/font-awesome.min.css" />
    <link rel="stylesheet" type="text/css" href="/ultra/Styles/Bootstrap/css/bootstrap.css" />
    <link rel="stylesheet" type="text/css" href="/ultra/Styles/Css/Style.css" /> 
    <style>
        .fixed-string {
            display: inline-block;
            width: 100px;
            white-space: nowrap;
            overflow: hidden !important;
            text-overflow: ellipsis;
        }
    </style>
    <script type="text/javascript" src="/ultra/Scripts/JQuery/jquery-3.2.1.min.js"></script>
    <script type="text/javascript">
        $(window).on('load resize', function () {
            // 부모 아이프레임 높이 자동조절
            var contH = $('#l_frame').outerHeight();
            var contH = contH + 100;
            if (parent.document) {
                if (contH < 900) contH = 900;
                $('iframe.cont_frame', parent.document).height(contH);
            }
        });

        // 데이터 호출 후 창 크기 변경 (스크롤)
        $(document).ajaxComplete(function () { setTimeout(function () { $(window).resize(); }, 100); });
    </script>
</head>
<body onload="loadPage()" id="l_frame">
    <form id="form1" runat="server">
        <div class="modal fade" id="hcp_info" tabindex="-1" role="dialog">
            <div class="modal-dialog modal-lg" role="document">
                <div class="modal-content">
                    <div class="modal-header">
                        <button type="button" class="close" data-dismiss="modal" aria-label="Close"><i class="fa fa-times-circle-o" aria-hidden="true"></i></button>
                    </div>
                    <div class="modal-body">
                        <div id="divSearchResult" class="name-heading panel-fill">
                            <h1 class="name-title"></h1>
                            <span class="badge">5</span>
                            <div class="select-month">
                                <select id="select_month" class="form-control year-month">
                                   
                                </select>
                                <!--
                                <button type="button" onclick="fn_ChangeSearchMonth('Prev')"><i class="fa fa-arrow-left" aria-hidden="true"></i><span class="tts">Prev</span></button>
                                <span id="hspanSearchMonth" class="month" title="">Oct</span>
                                <button type="button" onclick="fn_ChangeSearchMonth('Next')"><i class="fa fa-arrow-right" aria-hidden="true"></i><span class="tts">Next</span></button>
                                -->
                            </div>
                        </div>
                        <table id="tblHome_VisitList" class="table table-striped">
                            <thead>
                                <tr>
                                    <th>Date</th>
                                    <th>Activity</th>
                                    <th>HCP</th>
                                    <th>Requester</th>
                                    <th>Subject</th>
                                </tr>
                            </thead>
                            <tbody>
                            </tbody>
                        </table>
                    </div>
                    <div class="modal-footer">
                        <button type="button" class="btn btn-lg btn-navy" data-dismiss="modal">Close</button>
                    </div>
                </div>
            </div>
        </div>
        <uc1:LayerInformation runat="server" ID="LayerInformation" />
        <!-- // #layer_success -->
        <div id="wrap" class="container">
            <div class="row">
                <div id="divMobileSearch" class="search-area col-lg-4">
                    <input type="search" class="form-control" placeholder="Search Doctor" />
                </div>
                <!-- // .search-area -->
                <div class="left-group col-lg-8">
                    <div class="tabpanel-main" role="tabpanel">
                        <ul class="nav nav-tabs" role="tablist">
                            <li role="presentation" class="active"><a href="#tpNotSubmitted" aria-controls="tpNotSubmitted" role="tab" data-toggle="tab" data-tab-type="NotSubmitted">Not Submitted <span id="TabNotSubmittedCnt"></span></a></li>
                            <li role="presentation"><a href="#tpApprovalQueue" role="tab" aria-controls="tpApprovalQueue"  data-toggle="tab" data-tab-type="ApprovalQueue">Approval Queue <span id="TabApprovalQueueCnt"></span></a></li>
                            <li role="presentation"><a href="#tpPendingApproval" role="tab" aria-controls="tpPendingApproval"  data-toggle="tab" data-tab-type="PendingApproval">Pending Approval <span id="TabPendingApprovalCnt"></span></a></li>
                            <li role="presentation"><a href="#tpDelegation" role="tab" aria-controls="tpDelegation" data-toggle="tab" data-tab-type="Delegation">Delegation <span id="TabDelegationCnt"></span></a></li>
                        </ul>
                        <div class="tab-content">
                            <div role="tabpanel" class="tab-pane active" id="tpNotSubmitted">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h2 class="panel-title"><span>Not Submitted</span> <span class="badge" id="NotSubmittedCnt"></span></h2>
                                        <ul class="pagination pagination-sm">
                                            <li><a href="#" aria-label="Previous" onclick="fn_Paging('NotSubmitted','Prev')"><i class="fa fa-arrow-left" aria-hidden="true"></i></a></li>
                                            <li><a href="#" aria-label="Next" onclick="fn_Paging('NotSubmitted','Next')"><i class="fa fa-arrow-right" aria-hidden="true"></i></a></li>
                                        </ul>
                                    </div>
                                    <div id="hdivNotSubmitted" class="panel-body">
                                    </div>
                                </div>
                            </div>
                            <!-- // NotSubmitted -->
                            <div role="tabpanel" class="tab-pane" id="tpApprovalQueue">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h2 class="panel-title"><span>Approval Queue</span> <span class="badge" id="ApprovalQueueCnt"></span></h2>
                                        <ul class="pagination pagination-sm">
                                            <li><a href="#" aria-label="Previous" onclick="fn_Paging('ApprovalQueue','Prev')"><i class="fa fa-arrow-left" aria-hidden="true"></i></a></li>
                                            <li><a href="#" aria-label="Next" onclick="fn_Paging('ApprovalQueue','Next')"><i class="fa fa-arrow-right" aria-hidden="true"></i></a></li>
                                        </ul>
                                    </div>
                                    <div id="hdivApprovalQueue" class="panel-body">
                                    </div>
                                </div>
                            </div>
                            <!-- // ApprovalQueue -->
                            <div role="tabpanel" class="tab-pane" id="tpPendingApproval">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h2 class="panel-title"><span>Pending Approval</span> <span class="badge" id="PendingApprovalCnt"></span></h2>
                                        <ul class="pagination pagination-sm">
                                            <li><a href="#" aria-label="Previous" onclick="fn_Paging('PendingApproval','Prev')"><i class="fa fa-arrow-left" aria-hidden="true"></i></a></li>
                                            <li><a href="#" aria-label="Next" onclick="fn_Paging('PendingApproval','Next')"><i class="fa fa-arrow-right" aria-hidden="true"></i></a></li>
                                        </ul>
                                    </div>
                                    <div id="hdivPendingApproval" class="panel-body">
                                    </div>
                                </div>
                            </div>
                            <!-- // PendingApproval -->
                            <div role="tabpanel" class="tab-pane" id="tpDelegation">
                                <div class="panel panel-default">
                                    <div class="panel-heading">
                                        <h2 class="panel-title"><span>Delegation</span> <span class="badge" id="DelegationCnt"></span></h2>
                                        <ul class="pagination pagination-sm">
                                            <li><a href="#" aria-label="Previous" onclick="fn_Paging('Delegation','Prev')"><i class="fa fa-arrow-left" aria-hidden="true"></i></a></li>
                                            <li><a href="#" aria-label="Next" onclick="fn_Paging('Delegation','Next')"><i class="fa fa-arrow-right" aria-hidden="true"></i></a></li>
                                        </ul>
                                    </div>
                                    <div id="hdivDelegation" class="panel-body">
                                    </div>
                                </div>
                            </div>
                            <!-- // PendingApproval -->
                        </div>
                        <!-- // .tab-content -->
                    </div>
                    <!-- // .tabpanel -->
                    <div class="row">
                        <div class="col-md-6 col-lg-6">
                            <div class="panel panel-default panel-custom-event">
                                <div class="panel-heading">
                                    <h2 class="panel-title"><i class="fa fa-calendar-o" aria-hidden="true"></i>Upcoming Event <span class="badge" id="UpcomingEventCnt"></span></h2>
                                    <ul class="pagination pagination-sm">
                                        <li><a href="#" aria-label="Previous" onclick="fn_Paging('UpcomingEvent','Prev')"><i class="fa fa-arrow-left" aria-hidden="true"></i></a></li>
                                        <li><a href="#" aria-label="Next" onclick="fn_Paging('UpcomingEvent','Next')"><i class="fa fa-arrow-right" aria-hidden="true"></i></a></li>
                                    </ul>
                                </div>
                                <div id="hdivUpcomingEvent" class="panel-body">
                                </div>
                            </div>
                            <!-- // .panel -->
                        </div>
                        <div class="col-md-6 col-lg-6">
                            <div class="panel panel-default panel-custom-payment">
                                <div class="panel-heading">
                                    <h2 class="panel-title"><i class="fa fa-credit-card" aria-hidden="true"></i>Payment <span class="badge" id="PaymentCnt"></span></h2>
                                    <ul class="pagination pagination-sm">
                                        <li><a href="#" aria-label="Previous" onclick="fn_Paging('Payment','Prev')"><i class="fa fa-arrow-left" aria-hidden="true"></i></a></li>
                                        <li><a href="#" aria-label="Next" onclick="fn_Paging('Payment','Next')"><i class="fa fa-arrow-right" aria-hidden="true"></i></a></li>
                                    </ul>
                                </div>
                                <div id="hdivPayment" class="panel-body">
                                </div>
                            </div>
                            <!-- // .panel -->
                        </div>
                    </div>
                </div>
                <!-- // .left-group -->
                <div class="right-group col-lg-4">
                    <div class="panel panel-fill panel-fill-red">
                        <div class="panel-heading">
                            <h2 class="panel-title"><i class="fa fa-clock-o" aria-hidden="true"></i><span>3 Time Visits</span> <span class="badge" id="visitscnt">0</span></h2>
                            <div class="select-month">
                                <button type="button" onclick="fn_ChangeMonth('Prev')"><i class="fa fa-arrow-left" aria-hidden="true"></i><span class="tts">Prev</span></button>
                                <span id="hspanMonth" class="month">Oct</span>
                                <button type="button" onclick="fn_ChangeMonth('Next')"><i class="fa fa-arrow-right" aria-hidden="true"></i><span class="tts">Next</span></button>
                            </div>
                            <a href="#" class="btn-more" onclick="fn_MoreVisits(this)"><i id="hiMore" class="fa fa-plus" aria-hidden="true"></i></a>
                        </div>
                        <div id="hdiv3TimeVisits" class="panel-body">
                        </div>
                    </div>
                    <!-- // .panel -->
                    <div class="panel panel-fill panel-fill-green">
                        <div class="panel-heading">
                            <h2 class="panel-title"><i class="fa fa-coin" aria-hidden="true"></i>From CRM Data <span class="badge" id="crmData">0</span></h2>
                        </div>
                        <div class="panel-body">
                            <ul class="person-list person-list-crm">
                                <li></li>
                            </ul>
                            <div class="text-right">
                                <ul class="pagination pagination-sm">
                                    <li><a href="#" aria-label="Previous" class="crm-page-left"><i class="fa fa-arrow-left" aria-hidden="true"></i></a></li>
                                    <li class="active"><a href="#" class="crm-page-button" id="firstButton">1</a></li>
                                    <li><a href="#" class="crm-page-button" id="secondButton">2</a></li>
                                    <li><a href="#" aria-label="Next" class="crm-page-right"><i class="fa fa-arrow-right" aria-hidden="true"></i></a></li>
                                </ul>
                            </div>
                        </div>
                    </div>
                    <!-- // .panel -->
                </div>
                <!-- // .right-group -->
            </div>
            <input type="hidden" id="hddUserID" runat="server" />
            <input type="hidden" id="hddNotSubmittedPageNum" runat="server" />
            <input type="hidden" id="hddApprovalQueuePageNum" runat="server" />
            <input type="hidden" id="hddPendingApprovalPageNum" runat="server" />
            <input type="hidden" id="hddDelegationPageNum" runat="server" />
            <input type="hidden" id="hddUpcomingEventPageNum" runat="server" />
            <input type="hidden" id="hddPaymentPageNum" runat="server" />
            <input type="hidden" id="hddFromCRMDataPageNum" runat="server" />
            <input type="hidden" id="hddSelectDate" runat="server" />
            <input type="hidden" id="hddSearchSelectDate" runat="server" />
        </div>
        <script type="text/javascript" src="/ultra/Styles/Bootstrap/js/bootstrap.js"></script>
        
        <script type="text/javascript" src="/ultra/Scripts/Pages/FormEvent.js"></script>
        <script type="text/javascript" src="/ultra/Scripts/Pages/Home.js"></script>
        <link href="/ultra/Styles/waitMe/waitMe.css" rel="stylesheet" />
        <script src="/ultra/Styles/waitMe/waitMe.js"></script>

    </form>
</body>
</html>
