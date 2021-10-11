var _imp = (function () {
    var instance;
    var medicalidx, hcpcode, fnSaveCallBack, idx = null;
    function createInstance() {
        if (_imp !== undefined) {
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
            _imp.getInstance();
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
                $("#btnIMPDelete").show();
            }
            else {
                idx = null;
                fn_ResetControls();
                $("#btnIMPDelete").hide();
            }
            _imp.getInstance();
            fnSaveCallBack = args.FN_SAVE_CALLBACK; 
            _imp.Init(medicalidx, hcpcode); 
            $('.IMP-modal').modal("show");
        }
    }
     
    function fn_ValidationNumber(e) {
        $(this).val(fn_AddComma(fn_RemoveComma(this.value)));
    }

    function fn_PageInit()
    {
       // $("#txtAmount").on("keyup", fn_ValidationNumber);
        $('#btnIMPSave').unbind('click');
        $('#btnIMPDelete').unbind('click');
        $("#btnIMPSave").on("click", function (e) {
            e.preventDefault();
            fn_impSave("N");
        });

        $("#btnIMPDelete").on("click", function (e) {
            e.preventDefault();
            fn_confirm().done(function (val) {
                if (val) {
                    fn_impSave("Y");
                } 
            });
        });

        $('.IMP-modal').on('show.bs.modal', function (event) {
           
        });

        $('.IMP-modal').on('hide.bs.modal', function (event) {
            delete _imp;
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
    function fn_ValidationCheckforIMP(dto, deleteYN) {
        var retvalue = 0;
        

        return retvalue;

    }

    function fn_impSave(deleteYN) {
        
        try {
          
            var dto = {
                MEDICAL_IDX: medicalidx
                , HCP_CODE: hcpcode
                , IDX : idx
                , DATE: $("#dtIMPDate").val()
                , CATEGORY: $("input[name=lb_role0]:checked").val()
                , ORDER_NO: $("#orderNo").val()
                , AIRBILL_NO: $("#airbillNo").val()
                , IMP: $("#IMPtxt").val()
                , DOSE: $("#DoseNo").val()
                , UNIT: $("input[name=lb_role3]:checked").val()
                , QTY : $("#QtyNo").val()
                , TYPE: $("input[name=lb_role2]:checked").val()
                , COMMENT: $("#taComment_imp").val()
                , IS_DELETED: deleteYN
                , CREATOR_ID: $('input[id$=hhdUserID]').val()
                , CREATE_DATE: null
                , UPDATER_ID: $('input[id$=hhdUserID]').val()
                , UPDATE_DATE: null
            };

           
            
            var url = MEDICAL_SERVICE_URL + "/ModifyIMP";
            var d = $.Deferred();
           
            if (fn_ValidationCheckforIMP(dto, deleteYN) >= 0) {
                $.ajax({
                    url: url,
                    type: "POST",
                    async: true,
                    dataType: "json",
                    data: JSON.stringify(dto),
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                        d.resolve(response);
                        var result = response;
                        if (result.toLowerCase() == "ok") {
                        $('.IMP-modal').modal("hide");
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
        $("#orderNo").val(d.ORDER_NO);
        $("#dtIMPDate").val(d.DATE);
        $("#airbillNo").val(d.AIRBILL_NO);
        $("#IMPtxt").val(d.IMP);
        $("#DoseNo").val(d.DOSE);
        $("#QtyNo").val(d.QTY);
        $("input[name=lb_role0][value="+d.CATEGORY+"]").attr("checked", true);
        $("input[name=lb_role3][value=" + d.UNIT + "]").attr("checked", true);
        $("input[name=lb_role2][value=" + d.TYPE + "]").attr("checked", true);
        $("#taComment_imp").val(d.COMMENT);

    }

    function fn_ResetControls() {
        $("#orderNo").val('');
        $("#dtIMPDate").val('');
        $("#airbillNo").val('');
        $("#IMPtxt").val('');
        $("#DoseNo").val('');
        $("#QtyNo").val('');
        $("input[name=lb_role0][value=Shipped]").attr("checked", true);
        $("input[name=lb_role3][value=mg]").attr("checked", true);
        $("input[name=lb_role2][value=vial]").attr("checked", true);
        $("#taComment_imp").val('');
    }
})();


 