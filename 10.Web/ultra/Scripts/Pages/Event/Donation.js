$(function () {
    // Product Donation 선택 시 Product 입력
    $("input[name=hchkType][value=PD]").on("click", function () {
        if ($(this).prop("checked"))
            $('#hdivProductArea').show();
        else
            $('#hdivProductArea').hide();
    });

    // x 클릭 시 panel 닫음
    $('.btn-panel-close').click(function () {
        $(this).closest('.box-panel').hide();
    });

    $("#btnAddProduct").on("click", fn_AddProduct);
    $("#btnSaveProduct").on("click", fn_SaveProduct);
    $("#btnPrint").on("click", fn_PrintDonation);

    // 자동 합계
    $("#txtQty").on("keyup", fn_SetAmount);

    //add product 초기화
    $("input[name=rdoBU]").on("change", fn_ClearAddProduct);

	// 조회
    selectDonation();
});

/* Donation 조회 */
function selectDonation() {
	PROCESS_ID = $("input[id$=hddProcessID]").val();
	var status = $("input[id$=hddProcessStatus]").val();
	if (PROCESS_ID && status != "Temp") {
		$.ajax({
			url: EVENT_SERVICE_URL + "/SelectDonation/" + PROCESS_ID,
			type: "GET",
			dataType: "json",
			contentType: "application/json; charset=utf-8",
			success: function (data) {
			    displayDonation(data);
			    fn_SelectEventAttachFiles(PROCESS_ID);//첨부파일 조회 (FileUpload.js)
			},
			error: function (error) {
				fn_showError({
					message: error.responseText
				});
			},
		});
	}
	selectDonationProducts();
}

function displayDonation(data) {
	$("span[id$=hspanRequester]").text(data.REQUESTER_NAME);
	$("span[id$=hspanOrganization]").text(data.ORGANIZATION_NAME);
	$("span[id$=hspanRetaentionPeriod]").text(data.RETENTION_PERIOD);
	//$("span[id$=hspanRequestedDate]").text(data.REQUEST_DATE);
	$("span[id$=hspanEventKey]").text(data.EVENT_KEY);
	$("span[id$=hspanStatus]").text(data.PROCESS_STATUS);
	$("#txtSubject").val(data.SUBJECT);

	var types = data.TYPE.split(";");
	$("input[name=hchkType]").each(function () {
	    $(this).prop('checked', false);
	    for (var i = 0; i < types.length; i++)
	    {
	        if ($(this).val() == types[i])
	            $(this).prop('checked', true);
	    }
	});
	if ($("input[name=hchkType][value=PD]:checked").length > 0) {
	    $('#hdivProductArea').show();
	}

	$("#htxtValue").val(fn_AddComma(data.VALUE));
	var purposes = data.PURPOSE.split(";");
	$("input[name=hchkPurpose]").each(function () {
	    $(this).prop('checked', false);
	    for (var i = 0; i < purposes.length; i++) {
	        if ($(this).val() == purposes[i].trim())
	            $(this).prop('checked', true);
	    }
	});
	$("#taExplanation").val(data.EXPLANATION).change();

    //Recipient Information
	$("#txtRecipient").val(data.RECIPIENT);
	$("#txtAddress").val(data.ADDRESS);
	$("#txtTel").val(data.TEL);
	$("#txtEMail").val(data.EMAIL);
	$("input[name=rdoEligibility]").each(function () {
	    if ($(this).val() == data.IS_ELIGIBILITY)
	        $(this).prop('checked', true);
	    else
	        $(this).prop('checked', false);
	});
	$("input[name=rdoReceipt]").each(function () {
	    if ($(this).val() == data.AFTER_RECEIPT)
	        $(this).prop('checked', true);
	    else
	        $(this).prop('checked', false);
	});
	$("#taComment").val(data.COMMENT);
	var categorys = data.CATEGORY.split(";");
	$("input[name=hchkCategory]").each(function () {
	    $(this).prop('checked', false);
	    for (var i = 0; i < categorys.length; i++) {
	        if ($(this).val() == categorys[i].trim())
	            $(this).prop('checked', true);
	    }
	});
}


function selectDonationProducts() {

    var status = $("input[id$=hddProcessStatus]").val();
    var statusIdx = 0;
    if (status == "Saved") statusIdx = 1;
    else if (status == "Request") statusIdx = 2;
    else if (status == "Processing") statusIdx = 3;
    else if (status == "Completed") statusIdx = 4;
    else if (status == "EventCompleted") statusIdx = 5;
    else if (status == "PaymentCompleted") statusIdx = 6;

    var fields = [
            { name: "BU", title: "BU", type: "text" },
            { name: "PRODUCT_CODE", title: "Product", type: "text"},
            { name: "PRODUCT_NAME", title: "Product", type: "text" },
            {
                name: "QTY", title: "Quantitly", type: "number", width: 80, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            {
                name: "BASE_PRICE", title: "Price", type: "number", width: 80, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            {
                name: "AMOUNT", title: "Amount", type: "number", width: 80, align: "right",
                itemTemplate: function (value, item) {
                    return fn_AddComma(value);
                }
            },
            { name: "Delete", type: "control", modeSwitchButton: false, editButton: false }
    ];

    //요청 이후 삭제 컬럼 제거
    if (statusIdx > 1) {
        for (var i = 0; i < fields.length; i++)
        {
            if(fields[i].name == "Delete")
                fields.splice(i, 1);
        }
    }

    // 완료 후 거래명세서 버튼 활성화
    if (statusIdx > 3)
        $("#btnPrint").prop("disabled", false);
    else
        $("#btnPrint").prop("disabled", true);

    // Product Grid 선언부
    $("#jsGridProduct").jsGrid({
        width: "100%",
        sorting: true,
        paging: true,
        pageSize: GRID_LIST_COUNT,
        autoload: false,
        editing: false,
        controller: {
            loadData: function () {
                var selectUrl = EVENT_SERVICE_URL + "/SelectDonationProducts/" + PROCESS_ID;
                var d = $.Deferred();

                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    d.resolve(response);
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    d.reject(jqXHR);
                });
                return d.promise();
            }
        },
        fields: fields

    }).jsGrid("loadData");
}

function setPrice(item) {
    //alert(item.BASE_PRICE);
    $("#txtPrice").val(fn_AddComma(item.BASE_PRICE));
}

afterProductSearcher = setPrice;

function fn_AddProduct() {
    fn_ClearAddProduct();
	$('#hdivAddProduct').show();
}


// Add Product 초기화
function fn_ClearAddProduct() {
    var products = setProductSearcher(null);
    $("#tblAddData input[type=text]").val("");
}


// 총 가격 계산
function fn_SetAmount() {
    var c = 0, n = 0, a = 0;
    if ($("#txtQty").val().length > 0) {
        c = parseFloat($("#txtQty").val().replace(/,/ig, ""));
    }
    if ($("#txtPrice").val().length > 0) {
        n = parseFloat($("#txtPrice").val().replace(/,/ig, ""));
    }
    a = c * n;
    $("#txtAmount").val(fn_AddComma(a));
}


function fn_SaveProduct() {
    var processId = $("input[id$=hddProcessID]").val()
        , userId = $("input[id$=hddUserID]").val()
        , prodItems
        , retvalue = 0
        , alertMessage = ""
        , validateItem = [];

    product = getProductSearcher();
    
    if (product == null || product <= 0) {
        validateItem.push("Product");
        retvalue = -1;
    }

    if ($("#txtQty").val().length <= 0) {
        validateItem.push("Qty");
        retvalue = -1;
    }

    if (retvalue < 0) {
        if (retvalue == -1) {
            alertMessage = validateItem.join();
        }
        fn_showInformation({ title: "Please fill out below fields.", message: alertMessage });
    }
    else {
        $("#jsGridProduct").jsGrid("insertItem", {
            PROCESS_ID: processId
            , PRODUCT_IDX: 0
            , BU: $("input[name=rdoBU]:checked").val()
            , PRODUCT_CODE: product.PRODUCT_CODE
            , PRODUCT_NAME: product.PRODUCT_NAME
            , BASE_PRICE: product.BASE_PRICE//$("#txtPrice").val().replace(/,/ig, "")
            , QTY: $("#txtQty").val().replace(/,/ig, "")
            , AMOUNT: $("#txtAmount").val().replace(/,/ig, "")
            , IS_DELETED: "N"
            , CREATOR_ID: userId
            , UPDATER_ID: userId
        }).done(function () {
            fn_AddProduct();
            $("#jsGridProduct").jsGrid("refresh");
        });
    }
}

function fn_PrintDonation(e)
{
    e.preventDefault();

    var processid = $("input[id$=hddProcessID]").val();
    var url = "/Ultra/Pages/Event/Print/PrintDonation.aspx?processid=" + processid;

    window.open(url, "_blank", "width=900, height=800, toolbar=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no");
}




/*
    Event 저장
    Request인경우 callback function이 존재
*/
function saveEvent(action, callback) {

	var status = $("[id$=hspanStatus]").text();

	/*
        action이 Request이고 현재 Status가 없으면 신규 문서
    */
	if (action == "Request") {
		if (!status) status = "Temp";
	}
	else
		status = "Saved";

	//Subject
	var subject = $("#txtSubject").val();
	
    //타입
	var checkedType = "";
	var countType = 0;
	$("input[name=hchkType]:checked").each(function () {
	    if (checkedType.length > 0) checkedType += ";";
	    checkedType += $(this).val();
	    countType++;
	});
    //기부금액
	var value = $("#htxtValue").val().replace(/,/ig, "");
    // 목적
	var checkedPurpose = "";
	var countPurpose = 0;
	$("input[name=hchkPurpose]:checked").each(function () {
	    if (checkedPurpose.length > 0) checkedPurpose += ";";
	    checkedPurpose += $(this).val();
	    countPurpose++;
	});
    // 설명
	var explanation = $("#taExplanation").val();

    //Recipient Information
    //기부단체명
	var recipient = $("#txtRecipient").val();
    //주소
	var address = $("#txtAddress").val();
    //전화번호
	var tel = $("#txtTel").val();
    //메일주소
	var email = $("#txtEMail").val();
    //기부단체의 적격성 여부
	var checkedEligibility = "";
	var countEligibility = 0;
	$("input[name=rdoEligibility]:checked").each(function () {
	    if (checkedEligibility.length > 0) checkedEligibility += ";";
	    checkedEligibility += $(this).val();
	    countEligibility++;
	});
    //기부 이후 영수증 타입
	var checkedReceipt = "";
	var countReceipt = 0;
	$("input[name=rdoReceipt]:checked").each(function () {
	    if (checkedReceipt.length > 0) checkedReceipt += ";";
	    checkedReceipt += $(this).val();
	    countReceipt++;
	});
    //코멘트
	var comment = $("#taComment").val();
    //분류
	var checkedCategory = "";
	var countCategory = 0;
	$("input[name=hchkCategory]:checked").each(function () {
	    if (checkedCategory.length > 0) checkedCategory += ";";
	    checkedCategory += $(this).val();
	    countCategory++;
	});
    //product
	var products = [];

    /* 필수입력 체크 */
	var msgFillOut = "";
	if (subject.trim().length < 1) {
		msgFillOut = "Subject";
	}
	if (countType < 1) {
	    if (msgFillOut.length > 0) msgFillOut += ", ";
	    msgFillOut += "Type"
	}

	if ($("input[name=hchkType][value=PD]:checked").length > 0) {
	    products = $("#jsGridProduct").jsGrid().data("JSGrid").data;

	    if (products.length < 1) {
	        if (msgFillOut.length > 0) msgFillOut += ", ";
	        msgFillOut += "Product";
	    }
	}

	if (value.trim().length < 1) {
	    if (msgFillOut.length > 0) msgFillOut += ", ";
	    msgFillOut += "Value"
	}
	if (countPurpose < 1) {
	    if (msgFillOut.length > 0) msgFillOut += ", ";
	    msgFillOut += "Purpose"
	}
	if (explanation.trim().length < 1) {
	    if (msgFillOut.length > 0) msgFillOut += ", ";
	    msgFillOut += "Explanation"
	}

	if (recipient.trim().length < 1) {
	    if (msgFillOut.length > 0) msgFillOut += ", ";
	    msgFillOut += "Recipient"
	}
	if (address.trim().length < 1) {
	    if (msgFillOut.length > 0) msgFillOut += ", ";
	    msgFillOut += "Address"
	}
	if (tel.trim().length < 1) {
	    if (msgFillOut.length > 0) msgFillOut += ", ";
	    msgFillOut += "Tel"
	}
	if (email.trim().length < 1) {
	    if (msgFillOut.length > 0) msgFillOut += ", ";
	    msgFillOut += "eMail"
	}
	if (countEligibility < 1) {
	    if (msgFillOut.length > 0) msgFillOut += ", ";
	    msgFillOut += "기부단체의 적격성 여부"
	}
	if (countReceipt < 1) {
	    if (msgFillOut.length > 0) msgFillOut += ", ";
	    msgFillOut += "기부 이후 영수증 타입"
	}
	if (comment.trim().length < 1) {
	    if (msgFillOut.length > 0) msgFillOut += ", ";
	    msgFillOut += "Comment"
	}
	if (countCategory < 1) {
	    if (msgFillOut.length > 0) msgFillOut += ", ";
	    msgFillOut += "Category"
	}

	if (msgFillOut.length > 0) {
		fn_showInformation({
			title: "Please fill out below fields.",
			message: msgFillOut
		})
		return;
	}

	var PROCESS_ID = $("input[id$=hddProcessID]").val();
	var USER_ID = $("input[id$=hddUserID]").val();
	//저장
	var Donation = {
		PROCESS_ID: PROCESS_ID,
		SUBJECT: subject,
		EVENT_KEY: $("#hspanEventKey").text(),
		PROCESS_STATUS: status,
		REQUESTER_ID: $("input[id$=hddUserID]").val(),
		COMPANY_CODE: $("input[id$=hddCompanyCode]").val(),
		ORGANIZATION_NAME: $("[id$=hspanOrganization]").text(),
		LIFE_CYCLE: $("input[id$=hddLifeCycle]").val(),
		TYPE: checkedType,
		VALUE: value,
		PURPOSE: checkedPurpose,
		EXPLANATION: explanation,
		RECIPIENT: recipient,
		ADDRESS: address,
		TEL: tel,
		EMAIL: email,
		IS_ELIGIBILITY: checkedEligibility,
		AFTER_RECEIPT: checkedReceipt,
		COMMENT: comment,
        CATEGORY: checkedCategory,
		IS_DISUSED: "N",
		CREATOR_ID: USER_ID,
		UPDATER_ID: USER_ID
	}

	var proDonation = {
	    donation: Donation,
	    products: products
	}

	$.ajax({
		url: EVENT_SERVICE_URL + "/MergeDonation",
		type: "POST",
		data: JSON.stringify(proDonation),
		//dataType: "json",
		contentType: "application/json; charset=utf-8",
		success: function (data) {
			callback(true, subject, "");
		},
		error: function (error) {
			fn_showError({
				message: error.responseText
			});
			callback(false);
		},
	});
}