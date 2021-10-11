var _payment = (function () {
    var instance;
    var medicalidx, hcpcode, fnSaveCallBack, idx = null;
    function createInstance() {
        if (_payment !== undefined) {
            instance = {};
        }
        return instance;
    }

    return {
        getInstance: function () {
            if (!instance) {
                instance = createInstance();
            }
            return instance;
        }, Init: function (a, b) {
            _payment.getInstance();
            medicalidx = a;
            hcpcode = b;
            fn_PageInit();
        }, show: function (args) {
            args = args || {};
            medicalidx = args.MEDICAL_IDX;
            hcpcode = args.HCP_CODE;
            if (args.IDX != null || args.IDX != undefined)
            {
                idx = args.IDX;
                fn_SetControlBind(args.ITEM);
                $("#btnPaymentDelete").show();
            }
            else {
                idx = null;
                fn_ResetControls();
                $("#btnPaymentDelete").hide();
            }
            _payment.getInstance();
            fnSaveCallBack = args.FN_SAVE_CALLBACK; 
            _payment.Init(medicalidx, hcpcode); 
            $('.payment-modal').modal("show");
        }
    }
     
    function fn_ValidationNumber(e) {
        $(this).val(fn_AddComma(fn_RemoveComma(this.value)));
    }

    function fn_PageInit()
    {
        $("#txtAmount").on("keyup", fn_ValidationNumber);
        $('#btnPaymentSave').unbind('click');
        $('#btnPaymentDelete').unbind('click');
        $("#btnPaymentSave").on("click", function (e) {
            e.preventDefault();
            fn_PaymentSave("N");
        });

        $("#btnPaymentDelete").on("click", function (e) {
            e.preventDefault();
            fn_confirm().done(function (val) {
                if (val) {
                    fn_PaymentSave("Y");
                } 
            });
        });

        $('.payment-modal').on('show.bs.modal', function (event) {
           
        });

        $('.payment-modal').on('hide.bs.modal', function (event) {
            delete _payment;
        });
    }

    function fn_ValidationCheck(dto, deleteYN) {
        var retvalue = 0;
        var alertMessage = "";
        var validateItem = [];
        // 삭제일경우 Validation 제외
        if (deleteYN == 'Y') return true;

        if (dto.DATE == "") {
            validateItem.push("비용지급일자");
            retvalue = -1;
        }

        if (dto.AMOUNT == "") {
            validateItem.push("지급비용");
            retvalue = -1;
        }

        if (dto.EVIDENCE_ID == "") {
            validateItem.push("Evidentce ID");
            retvalue = -1;
        }
         
        return retvalue;

    }

    function fn_PaymentSave(deleteYN) {
        try {
          
            var dto = {
                MEDICAL_IDX: medicalidx
                , HCP_CODE: hcpcode
                , IDX : idx
                , DATE: $("#dtPaymentDate").val()
                , AMOUNT: fn_RemoveComma($("#txtAmount").val())
                , METHOD_TYPE: $("input[name=lb_role1]:checked").val()
                , EVIDENCE_ID: $("#txtEvidenceNo").val()
                , COMMENT: $("#taComment").val()
                , IS_DELETED: deleteYN
                , CREATOR_ID: $('input[id$=hhdUserID]').val()
                , CREATE_DATE: null
                , UPDATER_ID: $('input[id$=hhdUserID]').val()
                , UPDATE_DATE: null
            };

          
            var url = MEDICAL_SERVICE_URL + "/ModifyHcpPayment";
            var d = $.Deferred();
            if (fn_ValidationCheck(dto, deleteYN) >= 0) {
                $.ajax({
                    url: url,
                    type: "POST",
                    async:true,
                    dataType: "json",
                    data: JSON.stringify(dto),
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    d.resolve(response);
                    var result = response;
                    if (result.toLowerCase() == "ok") {
                            $('.payment-modal').modal("hide");
                        //fn_showInformation({ message: "저장되었습니다." }).done(function () {
                            fnSaveCallBack();
                          
                        //});
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

        } catch (ex) {
            fn_showError({ message: ex.message });
        }
    }
     
    function fn_SetControlBind(d) {
        $("#dtPaymentDate").val(d.DATE);
        $("#txtAmount").val(fn_AddComma(d.AMOUNT)); 
        $("input[name=lb_role1][value=" + d.METHOD_TYPE + "]").prop("checked", true);
        $("#txtEvidenceNo").val(d.EVIDENCE_ID);
        $("#taComment").val(d.COMMENT);
    }

    function fn_ResetControls() {
        $("#dtPaymentDate").val('');
        $("#txtAmount").val('');
        $("input[name=lb_role1][value=PO]").attr("checked", true);
        $("#txtEvidenceNo").val('');
        $("#taComment").val('');
    }
})();


 