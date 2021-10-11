var canvas;
var signaturePad;
var electronicModalWidth = 900;
$(function () {
    try {
   
        fn_Init();
    }
    catch (ex) {
        fn_showError({ message: ex.message });
    }
});
function GridData() {
    var retVal = "";
    var userID = $('input[id$=hddUserID]').val();
    var search = {
        userID: userID,
        searchType: "",
        searchText: "",
        doc_Start: "",
        doc_End: "",
        evt_Start: "",
        evt_End: ""
    }
    $.ajax({
        type: "POST",
        url: EVENT_SERVICE_URL + "/SelectApprovalAdminList",
        data: JSON.stringify(search),
        async: false,
        dataType: "json",
        contentType: "application/json; charset=utf-8",
        success: function (returnData) {
            retVal = returnData;
        }
    });
    return retVal;
}
function fn_WindowResize() {
    
    electronicModalWidth = 800, signpadWidth = 400;

    if ($(window).width() < 767) {
        electronicModalWidth = 300; 
    } else {
        electronicModalWidth = 800; 
    }
    signpadWidth = electronicModalWidth * 0.50;
    canvas.width = signpadWidth;

    var divCanvas = $(canvas).parent();
    divCanvas.width(signpadWidth + 5);
    $("#modal-ElectronicSign").width(electronicModalWidth);
    
    var contH = $('#l_frame').outerHeight();
    var contH = contH + 100;
    if (parent.document) {
        
        if (contH < 900) contH = 900;
        $('iframe.cont_frame', parent.document).height(contH);
    }

    signaturePad.clear();  
}

function fn_Init()
{
    try {
  
        $.ajaxSetup({
            beforeSend: function (xhr) { 
                $('body').waitMe({
                    effect: 'win8',
                    text: 'Please wait...'
                }); 
            },
            success: function (r, status, xhr) {
                $('body').waitMe("hide");
            },
            error: function (xhr, status, error ){
                $('body').waitMe("hide");
            }
        });

        // window open Free Goods Cancel Request Page
        $("#btnCancelForm").on("click", function () {
            var item = $('#divModalReceipt').data();
            var url = './FreeGoodCancelRequest.aspx?processid=' + item.PROCESS_ID + "&idx=" + item.IDX;
            window.open(url);
        })

        var userid = $('input[id$=hddUserID]').val();
        electronicModalWidth = 800, signpadWidth = 400;

        if ($(window).width() < 767) {
            electronicModalWidth = 300;
            signpadWidth = electronicModalWidth * 0.50; 
        } else {
            electronicModalWidth = 800;
            signpadWidth = electronicModalWidth * 0.50;
        }
     
        $('.form_datetime').datetimepicker({
            format: 'yyyy-mm-dd',
            weekStart: 1,
            todayBtn: 1,
            autoclose: 1,
            showMeridian: true,
            todayHighlight: 1,
            startView: 2,
            maxView: 2,
            minView: 2,
            forceParse: 0,
            minuteStep: 10
        });
 
        $("#jsGridList").jsGrid({
            width: "100%",
            sorting: true,
            paging: true,
            pageSize: GRID_LIST_COUNT,
            autoload: true,
            
            //height:auto,
            rowDoubleClick: function (args) {
           
            },
            filtering: true,
            controller: {
                
                loadData: function (filter) { 
                    var selectUrl = REPORT_SERVICE_URL + "/SelectReceiptForFreeGoodList/" + userid;
                    var d = $.Deferred();
                   
                    $.ajax({
                        url: selectUrl,
                        type: "GET",
                        dataType: "json",
                        data: filter,  
                        cache: false,
                        contentType: "application/json; charset=utf-8",
                    }).done(function (response) { 
                        console.log(response);
                        result = $.grep(response, function (item) {
                            return (!filter.EVENT_KEY || item.EVENT_KEY.toLowerCase().indexOf(filter.EVENT_KEY.toLowerCase()) > -1)
                                && (!filter.REQUESTER_NAME || item.REQUESTER_NAME.toLowerCase().indexOf(filter.REQUESTER_NAME.toLowerCase()) > -1)
                                && (!filter.HCO_NAME || item.HCO_NAME.toLowerCase().indexOf(filter.HCO_NAME.toLowerCase()) > -1)
                                && (!filter.PRODUCT_NAME || item.PRODUCT_NAME.toLowerCase().indexOf(filter.PRODUCT_NAME.toLowerCase()) > -1)
                                && (!filter.PURPOSE || item.PURPOSE.toLowerCase().indexOf(filter.PURPOSE.toLowerCase()) > -1)
                                && (!filter.HCP_NAME || item.HCP_NAME.toLowerCase().indexOf(filter.HCP_NAME.toLowerCase()) > -1)
                                && (!filter.QTY || item.QTY.indexOf(filter.QTY) > -1)
                                && (!filter.REQUEST_DATE || item.REQUEST_DATE.indexOf(filter.REQUEST_DATE) > -1);

                        });
                        d.resolve(result);
                    }).fail(function (jqXHR, textStatus, errorThrown) {
                        d.reject(jqXHR);
                    });
                
                    return d.promise();
                }
            },

            fields: [
                {
                    name: "EVENT_KEY", title: "Link", width: 30,
                    itemTemplate: function (value, item) {
                        var $href = $("<a>");
                        $href.html("Link");
                        $href.on("click", function (e) {
                            $.ajax({
                                url: COMMON_SERVICE_URL + "/GetConfiguration/" + item.EVENT_ID,
                                type: "GET",
                                dataType: "json",
                                contentType: "application/json; charset=utf-8",
                                success: function (data) {
                                    var url = "/ultra/Pages/Event/" + data.WEB_PAGE_NAME;
                                    fn_AddTabPage(url, item.EVENT_ID, data.EVENT_NAME, item.PROCESS_ID);
                                },
                                error: function (error) {
                                    fn_showError({
                                        message: error.responseText
                                    });
                                },
                            });
                    
                    
                        });
                        return $href;
                    },
                },
                {
                    name: "EVENT_KEY", title: "Event Key", width: 60,type:"text"
                    //itemTemplate: function (value, item) {
                    //    var $href = $("<a>");
                    //    $href.html(value);
                    //    $href.on("click", function (e) {
                    //        $.ajax({
                    //            url: COMMON_SERVICE_URL + "/GetConfiguration/" + item.EVENT_ID,
                    //            type: "GET",
                    //            dataType: "json",
                    //            contentType: "application/json; charset=utf-8",
                    //            success: function (data) {
                    //                var url = "/ultra/Pages/Event/" + data.WEB_PAGE_NAME;
                    //                fn_AddTabPage(url, item.EVENT_ID, data.EVENT_NAME, item.PROCESS_ID);
                    //            },
                    //            error: function (error) {
                    //                fn_showError({
                    //                    message: error.responseText
                    //                });
                    //            },
                    //        });
                    //
                    //
                    //    });
                    //    return $href;
                    //},
                    


                }, 
                { name: "REQUESTER_NAME", title: "Requester", type: "text" },
                //v 1.0.4 : Request_date/Receipt date 추가
                {
                    name: "REQUEST_DATE", title: "Reguest Date", type: "text",
                    itemTemplate: function (value, item) {
                        return value.substring(0, 10);
                    }
                },
                { name: "HCP_NAME", title: "HCP(or Custormer)", type: "text" },
                { name: "HCO_NAME", title: "HCO(or Organization)", type: "text" },
                { name: "PRODUCT_NAME", title: "Product", type: "text" },
                {
                    name: "QTY", title: "Qty", type: "number", width: 30, align: "right",
                    itemTemplate: function (value, item) {
                        return fn_AddComma(value);
                    }
                },
                { name: "PURPOSE", title: "Purpose", type: "text" },
                {
                    name: "RECEIPT", title: "인수증",   
                    itemTemplate: function (value, item) {
                       
                        var $obj = $("<span>");
                      
                        if (item.STATUS.toLowerCase() == "returnsend")
                        {
                           var $btnObj = $("<button>")
                                            .attr("type", "button")
                                            .addClass("btn btn-sm btn-red")
                                            .append($("<span>").text("반품배송"))
                                            .on("click", function () {
                                                fn_ShowReceipt(item, $(this));
                                            });
                            $obj.append($btnObj);
                        }
                        else if ( item.STATUS.toLowerCase() == "returncomplete")
                        {
                            var $btnObj = $("<button>")
                                            .attr("type", "button")
                                            .addClass("btn btn-sm btn-red")
                                            .append($("<span>").text("반품완료"))
                                            .on("click", function () {
                                                fn_ShowReceipt(item, $(this));
                                            });
                            $obj.append($btnObj);
                        }
                        else if ( item.STATUS.toLowerCase() == "receipted")
                        {
                            if (item.RECEIPT_CATEGORY == "RMD") {
                                $obj.text(item.RECEIPT_DATE +"-(인수완료)").on("click", function () {
                                    fn_ShowReceipt(item, $(this));
                                });
                            } else {
                                $obj.text(item.RECEIPT_DATE).on("click", function () {
                                    fn_ShowReceipt(item, $(this));
                                });
                            }
                        }
                        else if (item.STATUS.toLowerCase() == "receipted_return") {
                            if (item.RECEIPT_CATEGORY == "RMD") {
                                $obj.text(item.RECEIPT_DATE + "-(반품완료)").on("click", function () {
                                    fn_ShowReceipt(item, $(this));
                                });
                            }
                        }
                        else {
                            item.STATUS = "edit";

                            var $btnObj = $("<button>")
                                            .attr("type", "button")
                                            .addClass("btn btn-sm btn-red")
                                            .append($("<i>")
                                            .addClass('fa fa-floppy-o'))
                                            .on("click", function () {
                                                console.log(item.PURPOSE);
                                                if (item.RECEIPT_CATEGORY == "RMD") {
                                                    $("#receiptType_normal").hide();
                                                    $("#receiptType_rmd").show();
                                                } else {
                                                    $("#receiptType_normal").show();
                                                    $("#receiptType_rmd").hide();
                                                }
                                                fn_ShowReceipt(item, $(this));
                                            });
                            $obj.append($btnObj);
                            
                            
                        }
                         
                        return $obj;
                    }
                } 
            ]
        });
     
        // paper, electronic, 반품 선택했을 때 이벤트
        $("input[name=rdoType]:radio").on("change", function () {
            if ($(this).val() != "paper")
            {
                var selVal = $("input[name=rdoType]:checked").val();
                if (selVal == "electronicRMD_Return") {
                    fn_VisabledControls('next', 'RMD');
                } else {
                    fn_VisabledControls('next', '');
                }
                
            }
            else {
                fn_VisabledControls('edit','');
            }

        });

        // Next 버튼 클릭 이벤트
        $("#btnNext").on("click", function () {
            var selVal = $("input[name=rdoType]:checked").val();
            if (selVal == "electronic") // Electronic Sign 선택하였을경우
            {
                $('#divModalReceipt').modal('hide');
                $('#divElectronicSign').modal('show');
                $("#electronic_comment_rmd").hide();
                $("#electronic_comment_normal").show();
                $("#electronic_comment_rmd_return").hide();

                $("#btnSignSave_RMDReturn").hide();
            }
            else if (selVal == "electronicRMD") {
                $('#divModalReceipt').modal('hide');
                $('#divElectronicSign').modal('show');
                $("#electronic_comment_rmd").show();
                $("#electronic_comment_normal").hide();
                $("#electronic_comment_rmd_return").hide();

                $("#btnSignSave_RMDReturn").hide();
            }
            else if (selVal == "electronicRMD_Return") {
                $('#divModalReceipt').modal('hide');
                $('#divElectronicSign').modal('show');                
                $("#electronic_comment_rmd").hide();
                $("#electronic_comment_normal").hide();
                $("#electronic_comment_rmd_return").show();
                var selText = $("#ReturnComment option:selected").text();
                
                $("#txtReturnType").html(selText);
                $("#btnSignSave_RMDReturn").show();
                $("#btnSignSave").hide();
            }
            else if (selVal == "return")    // 반품을 선택하였을 경우
            {
                fn_ReturnSend();
            }

        });

        // modal 이벤트
        $("#divElectronicSign").on('show.bs.modal', function () {
            var $modal = $("#modal-ElectronicSign");
            $modal.width(electronicModalWidth);
            _initial_body_overflow = $('body').css('overflow');
          
            // Let modal be scrollable
            $(this).css('overflow-y', 'auto');
            $('.l_layout').css('overflow', 'hidden');


        }).on('hide.bs.modal', function () {
         
            // Reverse previous initialization
            $(this).css('overflow-y', 'hidden');
            $('.l_layout').css('overflow', _initial_body_overflow);
        });

        // modal 이벤트 -RMD 용
        $("#divElectronicSign_RMD").on('show.bs.modal', function () {
            var $modal = $("#modal-ElectronicSign_RMD");
            $modal.width(electronicModalWidth);
            _initial_body_overflow = $('body').css('overflow');

            // Let modal be scrollable
            $(this).css('overflow-y', 'auto');
            $('.l_layout').css('overflow', 'hidden');


        }).on('hide.bs.modal', function () {

            // Reverse previous initialization
            $(this).css('overflow-y', 'hidden');
            $('.l_layout').css('overflow', _initial_body_overflow);
        });
      
        canvas = document.getElementById('signature-pad');
        var divCanvas = $(canvas).parent();
          
         signaturePad = new SignaturePad(canvas, {
             backgroundColor: 'rgb(255, 255, 255)' 
             , width: signpadWidth
             , height : 140
         });

         canvas.width = signpadWidth;
         divCanvas.width(signpadWidth+5);
        signaturePad.clear();

        //RMD 용 싸인
        canvas_RMD = document.getElementById('signature-pad_RMD');
        var divCanvas = $(canvas_RMD).parent();

        signaturePad_RMD = new SignaturePad(canvas_RMD, {
            backgroundColor: 'rgb(255, 255, 255)'
            , width: signpadWidth
            , height: 140
        });

        canvas.width = signpadWidth;
        divCanvas.width(signpadWidth + 5);
        signaturePad_RMD.clear();
     
        $(window).on("resize", fn_WindowResize);
        $(document).ajaxComplete(function () { setTimeout(function () { $(window).resize(); }, 200); });



        $("#btnPadClear").on("click", function () {
            signaturePad.clear();
        });

        // Electronic Signature 저장시 처리
        $("#btnSignSave").on("click", function () {

            var dataURL = signaturePad.toDataURL('image/jpeg');
            var parts = dataURL.split(';base64,');
            var contentType = parts[0].split(":")[1];
 
            var dto = {
                PROCESS_ID: $("input[id$=hddProcessID]").val()
                , IDX: $("#hhdIdx").val()
                , EVENT_KEY: $("#hhdEventKey").val()
                , RECEIPT_TYPE: $("input[name=rdoType]:checked").val()  
                , SIGN_IMG_PATH: parts[1]  
                , STATUS: 'receipted'
                , CREATOR_ID: $("input[id$=hddUserID]").val()
                , UPDATER_ID: $("input[id$=hddUserID]").val()
            }

            fn_SaveFreeGoodSignature(dto, fn_CallbockSignature);
     
        });



        // 의료기기 반납시 Electronic Signature 저장시 처리
        $("#btnSignSave_RMDReturn").on("click", function () {

            var dataURL = signaturePad.toDataURL('image/jpeg');
            var parts = dataURL.split(';base64,');
            var contentType = parts[0].split(":")[1];

            var dto = {
                PROCESS_ID: $("input[id$=hddProcessID]").val()
                , IDX: $("#hhdIdx").val()
                , EVENT_KEY: $("#hhdEventKey").val()
                , RECEIPT_TYPE: $("input[name=rdoType]:checked").val()
                , SIGN_IMG_PATH: parts[1]
                , STATUS: 'receipted_return'
                , CREATOR_ID: $("input[id$=hddUserID]").val()
                , UPDATER_ID: $("input[id$=hddUserID]").val()
                , RETURN_COMMENT: $("#txtReturnType").html()
            }

            fn_SaveFreeGoodSignature_return(dto, fn_CallbockSignature);

        });
         
        // Paper 저장시 처리
        $("#btnPaperSave").on("click", function () {
             
            var retvalue = 0;
            var alertMessage = "";
            var validateItem = [];
          

            if ($("#dtReceiptDate").val().length <= 0) {
                validateItem.push("Receipt Date");
                retvalue = -1;
            }

            if ($(".attach-list>li").length == 0) {
                validateItem.push("Attachment");
                retvalue = -1;
            }

            if (retvalue < 0) {
                if (retvalue == -1) {
                    alertMessage = validateItem.join();
                }
                 fn_showInformation({ title: "Please fill out below fields.", message: alertMessage });
                //$(this).alter("Please fill out below fields.\r\n" + alertMessage );
            }
            else {
                var eventFileIdx = [];
                $.each($(".attach-list>li"), function (i, obj) {
                    var item = JSON.parse($(obj).attr("data-attach-file"));
                    eventFileIdx.push(item.Index);
                });

                var $this = $(this);
                $this.button('loading');

                var dto = {
                    PROCESS_ID: $("input[id$=hddProcessID]").val()
                    , IDX: $("#hhdIdx").val()
                    , EVENT_KEY: $("#hhdEventKey").val()
                    , RECEIPT_TYPE: $("input[name=rdoType]:checked").val()
                    , RECEIPT_DATE: $("#dtReceiptDate").val()
                    , EVENT_FILE_IDX: eventFileIdx.join()
                    , STATUS: 'receipted'
                    , CREATOR_ID: $("input[id$=hddUserID]").val()
                    , UPDATER_ID: $("input[id$=hddUserID]").val()
                }

                fn_SaveFreeGoodSignature(dto, fn_CallbockPaper);
                $this.button('reset');
            }
            
        }); 
        
        //반품 취소
        $("#btnReturnCancel").on("click", function () {
     
            var today = new Date().getTime();  /*.toISOString().substring(0, 10) */
            var d = "\/Date(" + today + "+09:00)\/";
            var dto = {
                PROCESS_ID: $("input[id$=hddProcessID]").val()
                , IDX: $("#hhdIdx").val()
                , EVENT_KEY: $("#hhdEventKey").val()
                , RECEIPT_TYPE: ''
                , SIGN_IMG_PATH: ""
                , STATUS: ''
                , RECEIPT_DATE: ''
                , CREATOR_ID: $("input[id$=hddUserID]").val()
                , UPDATER_ID: $("input[id$=hddUserID]").val()
            }

            // CAO 팀 및 공장 직원에게 메일 발송 하도록 처리 필요![미작업]
            fn_SaveFreeGoodSignature(dto, fn_CallbockPaper);
        });

        // 승인 입력
        $("#btnReceipt").on("click", function () {
            var today = new Date();
            //var d = "\/Date(" + today + "+09:00)\/";
            var dto = {
                PROCESS_ID: $("input[id$=hddProcessID]").val()
                , IDX: $("#hhdIdx").val()
                , EVENT_KEY: $("#hhdEventKey").val()
                , STATUS: 'returncomplete' 
                , SAP_ORDER: $("#txtSapOrder").val()
                , UPDATER_ID: $("input[id$=hddUserID]").val()
            }

            // CAO 팀 및 공장 직원에게 메일 발송 하도록 처리 필요![미작업]
            fn_UpdateStatus(dto, fn_CallbockPaper);
        });
        
        $("#btnExcel_report").on("click", function () {
            var from = "2018-01-01";
            var to = "2018-05-01";
            var code = "EVENT_MASTER";
            
            var selectUrl = REPORT_SERVICE_URL + "/ExportXlsReportAdmin/" + code + "/" + from + "/" + to;
            
             var d = $.Deferred();
                
             $.ajax({
                 url: selectUrl,
                 type: "GET",
                 dataType: "json",
                 cache: false,
                 contentType: "application/json; charset=utf-8",
             }).done(function (response) {
                 //alert(response);
                 var url = '/Ultra/Handler/FileDownHandler.aspx?DestFileName=' + encodeURIComponent(response);
                
                 $('#iframeFileDown').attr('src', url);

                 d.resolve(response);

             }).fail(function (jqXHR, textStatus, errorThrown) {
                 
                 d.reject(jqXHR);
             });

            d.promise();
        });

        $("#btnExcel").on("click", function () {
            
            var selectUrl = REPORT_SERVICE_URL + "/ExportXlsReceiptForFreeGoodList/" + userid;
            alert(selectUrl);
            var d = $.Deferred();

            $.ajax({
                url: selectUrl,
                type: "GET",
                dataType: "json",
                cache: false,
                contentType: "application/json; charset=utf-8",
            }).done(function (response) {
                var url = '/Ultra/Handler/FileDownHandler.aspx?DestFileName=' + encodeURIComponent(response);

                $('#iframeFileDown').attr('src', url);

                d.resolve(response);

            }).fail(function (jqXHR, textStatus, errorThrown) {

                d.reject(jqXHR);
            });

            d.promise();
        });
 
    }
    catch (ex) {
        fn_showError({ message: ex.message });
    }
}
 
function fn_UpdateStatus(dto, callbackFc) {
    var d = $.Deferred();
    var url = REPORT_SERVICE_URL + "/UpdateReceiptStatus";
    $.ajax({
        url: url,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(dto),
        //async:false,
        cache: false,
        contentType: "application/json; charset=utf-8",
    }).done(function (response) {
        d.resolve(response);
        var result = response;
        if (result.toLowerCase() == "ok") {
            callbackFc();
        }
        else {
            fn_showInformation({ message: result[0] });
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        d.reject(jqXHR);
        fn_showError({ message: jqXHR.message });
    });
    d.promise();
}

function fn_ReturnSend()
{

    var today = new Date().getTime();  /*.toISOString().substring(0, 10) */
    var d  = "\/Date(" + today + "+09:00)\/";
    var dto = {
        PROCESS_ID: $("input[id$=hddProcessID]").val()
        , IDX: $("#hhdIdx").val()
        , EVENT_KEY: $("#hhdEventKey").val()
        , RECEIPT_TYPE: $("input[name=rdoType]:checked").val()
        , SIGN_IMG_PATH: ""
        , STATUS: 'returnsend' 
        , RECEIPT_DATE: new Date().toISOString()
        , CREATOR_ID: $("input[id$=hddUserID]").val()
        , UPDATER_ID: $("input[id$=hddUserID]").val()
    }

    // CAO 팀 및 공장 직원에게 메일 발송 하도록 처리 필요![미작업]
    fn_SaveFreeGoodSignature(dto, fn_CallbockPaper);
     
}
 

function fn_CallbockPaper() {
    $('#divModalReceipt').modal('hide');
    $("#jsGridList").jsGrid("loadData");
}

function fn_CallbockSignature()
{
    try{
        $('#divElectronicSign').modal('hide');
        $("#jsGridList").jsGrid("loadData");
    }
    catch (ex) {
        fn_showError({ message: ex.message });
    }
}

function fn_ShowReceipt(item, btn)
{ 
    $('#divModalReceipt').modal('show');
    $('#divModalReceipt').data(item);
    if (item.RECEIPT_TYPE == null || item.RECEIPT_TYPE.length == 0)
    {
        $("input[name=rdoType][value=paper]").prop("checked", true);
    }
    else {
        $("input[name=rdoType][value=" + item.RECEIPT_TYPE + "]").prop("checked", true);
    }
    $("#files").find('.attach-list').empty();
    $("#btnPadClear").click();
    fn_SetItemBind(item);
    
    if (item.RECEIPT_CATEGORY == "RMD") {
        fn_VisabledControls(item.STATUS, 'RMD');
    } else {
        fn_VisabledControls(item.STATUS, '');
    }
    
  
}

function fn_SaveFreeGoodSignature(dto, callbackFc)
{
    console.log(dto);
    var d = $.Deferred();
    var url = REPORT_SERVICE_URL + "/ModifyReceiptFreeGood";
    $.ajax({
        url: url,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(dto),
        //async:false,
        cache: false,
        contentType: "application/json; charset=utf-8",
    }).done(function (response) {
        d.resolve(response);
        var result = response;
        if (result.toLowerCase() == "ok") {
            
            callbackFc();
        }
        else {
            fn_showInformation({ message: result[0] });
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        d.reject(jqXHR);
        fn_showError({ message: jqXHR.message });
    });
    d.promise();
}


function fn_SaveFreeGoodSignature_return(dto, callbackFc) {
    console.log(dto);
    var d = $.Deferred();
    var url = REPORT_SERVICE_URL + "/ModifyReceiptFreeGood_return";
    $.ajax({
        url: url,
        type: "POST",
        dataType: "json",
        data: JSON.stringify(dto),
        //async:false,
        cache: false,
        contentType: "application/json; charset=utf-8",
    }).done(function (response) {
        d.resolve(response);
        var result = response;
        if (result.toLowerCase() == "ok") {

            callbackFc();
        }
        else {
            fn_showInformation({ message: result[0] });
        }
    }).fail(function (jqXHR, textStatus, errorThrown) {
        d.reject(jqXHR);
        fn_showError({ message: jqXHR.message });
    });
    d.promise();
}

function fn_SetItemBind(item)
{
    console.log(item);
    $("#txtProduct").html(item.PRODUCT_NAME);
    $("#txtPurpose").html(item.PURPOSE);
    $("#txtDivision").html(item.BU);
    $("#txtEventKey").html(item.EVENT_KEY);
    $("#txtProductCode").html(item.PRODUCT_CODE);
    $("#txtPackage").html(item.STD_CODE);
    $("#txtQty").html(item.QTY);
    $("#txtSignDate").html($("input[id$=hhdDate]").val());
    $("#txtHcoName").html(item.HCO_NAME);
    $("#hhdHcoCode").html(item.HCO_CODE);
    $("#txtHcpName").html(item.HCP_NAME);
    $("#hhdHcpCode").html(item.HCP_CODE);
    $("#txtRequestName").html(item.REQUESTER_NAME);
    
    $("#hhdIdx").val(item.IDX);
    $("#hhdEventKey").val(item.EVENT_KEY);
    $("#dtReceiptDate").val(item.RECEIPT_DATE);
    $("input[id$=hddProcessID]").val(item.PROCESS_ID);
    // 반품요청일은 Receipt_date에서 조회해 온다
    $("#hspanreturnDate").text(item.RECEIPT_DATE);
    $("#hspanreturncompleteDate").text(item.RETURN_DATE);
    $("#txtSapOrder").text(item.SAP_ORDER);
    if (item.EVENT_FILE_IDX != null )
    {
        fn_SelectEventAttachFilesIdxs(item.PROCESS_ID, item.EVENT_FILE_IDX);

        if (item.RETURN_EVENT_FILE_IDX != null) {
            fn_SelectEventAttachFilesIdxs(item.PROCESS_ID, item.RETURN_EVENT_FILE_IDX);
        }
    }

}

function fn_SelectEventAttachFilesIdxs(processID, idxs) {
    console.log(processID);
    console.log(idxs);
    if (processID) {
        $.ajax({
            url: EVENT_SERVICE_URL + "/SelectEventAttachFilesIdxs/" + processID + "/" + idxs,
            //url: EVENT_SERVICE_URL + "/SelectEventAttachFiles/" + processID ,
            
            type: "GET",
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                $('body').waitMe("hide");
                fn_SetAttachControl(data); 
            },
            error: function (error) {
                $('body').waitMe("hide");
                fn_showError({
                    message: error.responseText
                }); 
            },
        });
    }
}

function fn_SetAttachControl(attachFiles)
{
    console.log(attachFiles);
    var status = $("input[id$=hddProcessStatus]").val();
    var len = attachFiles.length;
   
    for (var i = 0; i < len; i++) {
        var file = attachFiles[i];
        var attachType = attachFiles[i].ATTACH_FILE_TYPE;
        var attachFile = {
            Index: file.IDX,
            DisplayName: file.DISPLAY_FILE_NAME,
            SavedName: file.SAVED_FILE_NAME,
            FileSize: file.FILE_SIZE,
            AttachType: file.ATTACH_FILE_TYPE,
            FileHandlerUrl: file.FILE_HANDLER_URL,
            FilePath: file.FILE_PATH,
            ErrorMessage: "",
        }
        var $li = $("<li data-attach-file=" + JSON.stringify(attachFile) + "></li>");
        var href = UPLOAD_HANDLER_URL + "?file=" + encodeURIComponent(attachFile.FilePath);
        var $ahref = $("<a href='#' class='attach-link btn btn-xs btn-gray'>" + file.DISPLAY_FILE_NAME + "</a>").on("click", function () {
            var url = UPLOAD_HANDLER_URL + "?file=" + encodeURIComponent(attachFile.FilePath);
            if (attachFile) {
                window.open(url);
                //$('#iframeFileDown', parent.document).attr('src', url);
            }
        });
        $li.append($ahref);
 
        if (file.PROCESS_STATUS == "Saved") {
            var $button = $("<button type='button' class='fa fa-times'><span class='tts'>Close</span></button>");
            $li.append($button);
        } else {
            $ahref.css('padding-right', '8px');
        }
        $li.appendTo($("#divAttachFiles_Receipt .attach-list"));
    }
}
 
function fn_VisabledControls(status,rmd_flag)
{
    console.log(rmd_flag);
    switch (status)
    {
        case 'receipted':
            if (rmd_flag == "RMD") {
                
                $("#receiptType_rmd").show();
                $("#receiptType_normal").hide();
                
                //$("input[name=rdoType]").prop('disabled', true);
                $("#trReceiptDate").show();
                $("#trSapOrder").hide();
                $("#trReturn").hide();
                $("#trReturnComplete").show();
                $("#btnPaperSave").hide();
                $("#btnReturnCancel").hide();
                $("#trReturnType").show();
                $("#btnReceipt").hide();
                $("#btnClose").show();
                $("#btnNext").hide();
                $("#divAttachArea").show();
                $("#hspanAddAttach").hide();
            } else {
                $("#receiptType_rmd").hide();
                $("#receiptType_normal").show();
                $("input[name=rdoType]").prop('disabled', true);
                $("#trReceiptDate").show();
                $("#trSapOrder").hide();
                $("#trReturn").hide();
                $("#trReturnComplete").hide();
                $("#btnPaperSave").hide();
                $("#btnReturnCancel").hide();
                $("#trReturnType").hide();
                $("#btnReceipt").hide();
                $("#btnClose").show();
                $("#btnNext").hide();
                $("#divAttachArea").show();
                $("#hspanAddAttach").hide();
            }
            break;
        case 'receipted_return':
            $("#receiptType_rmd").hide();
            $("#receiptType_normal").show();
            $("input[name=rdoType]").prop('disabled', true);
            $("#trReceiptDate").show();
            $("#trSapOrder").hide();
            $("#trReturn").hide();
            $("#trReturnComplete").hide();
            $("#btnPaperSave").hide();
            $("#btnReturnCancel").hide();
            $("#trReturnType").hide();
            $("#btnReceipt").hide();
            $("#btnClose").show();
            $("#btnNext").hide();
            $("#divAttachArea").show();
            $("#hspanAddAttach").hide();
            break;
        case 'edit':
            $("input[name=rdoType]").prop('disabled', false);
            $("#trReceiptDate").show();
            $("#trSapOrder").hide();
            $("#trReturn").hide();
            $("#trReturnComplete").hide();
            $("#btnPaperSave").show();
            $("#btnReturnCancel").hide();
            $("#trReturnType").hide();
            $("#btnReceipt").hide();
            $("#btnClose").hide();
            $("#btnNext").hide();
            $("#divAttachArea").show();
            $("#hspanAddAttach").show();
            break;
        case 'returnsend':
            $("input[name=rdoType]").prop('disabled', true);
            $("#trReceiptDate").hide();
            $("#trSapOrder").show();
            $("#trReturn").show();
            $("#trReturnComplete").hide();
            $("#btnPaperSave").hide();
            $("#btnReturnCancel").show();
            $("#trReturnType").hide();
            $("#btnReceipt").show();
            $("#btnClose").hide();
            $("#btnNext").hide();
            $("#divAttachArea").hide();
            $("#hspanreturnDate").text(new Date().toISOString().substring(0, 10));
            break;
        case 'returncomplete':
            $("input[name=rdoType]").prop('disabled', true);
            $("#trReceiptDate").hide();
            $("#trSapOrder").show();
            $("#txtSapOrder").prop('disabled', true);
            $("#trReturn").show();
            $("#trReturnComplete").show();
            $("#trReturnType").hide();
            $("#btnPaperSave").hide();
            $("#btnReturnCancel").hide();
            $("#btnReceipt").hide();
            $("#btnClose").hide();
            $("#btnNext").hide();
            $("#divAttachArea").hide();
            break;
        case 'next':
            $("input[name=rdoType]").prop('disabled', false);
            $("#trReceiptDate").hide();
            $("#trSapOrder").hide();
            $("#trReturn").hide();
            $("#trReturnComplete").hide();
            $("#btnPaperSave").hide();
            $("#btnReturnCancel").hide();
            $("#btnReceipt").hide();
            $("#btnClose").hide();
            $("#btnNext").show();
            $("#divAttachArea").hide();
            $("#trReturnType").hide();
            if (rmd_flag == "RMD") {
                $("#trReturnType").show();
            }
            
            
            break;

    }
}


function fn_PrintReceiptFreeGood() {
    var processid = $("input[id$=hddProcessID]").val();
    var idx = $("#hhdIdx").val();
    var url = "/Ultra/Pages/Report/PrintReceiptFreeGood.aspx?processid=" + processid + "&idx=" + idx;

    window.open(url, "_blank", "width=900, height=800, toolbar=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no");
}
 

