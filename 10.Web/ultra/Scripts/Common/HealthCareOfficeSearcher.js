$(function () {

    var hcoSearcher;
    var clearJsGrid = false;

    $('.hco-searcher-modal').on('show.bs.modal', function (event) {
        initHCOSearcherModal();
        var button = $(event.relatedTarget);
        hcoSearcher = button;
    });

    function setHCOModalSelectItem() {
        //var selectItem = $('.hco-searcher-modal #txtSelectedHCO');
        //hcoSearcher.parent().parent().find('input').val(selectItem.val());
        //hcoSearcher.parent().parent().find('input').data('hco-code', selectItem.data("hco-code"));
        //hcoSearcher.parent().parent().find('input').data('hco-name', selectItem.data("hco-name"));


        // <!-- Ver 1.0.7 : Go-Direct -->
        var selectItem = $('.hco-searcher-modal #txtSelectedHCO');

        var rightNow = new Date();
        var hcpcode = "DEVICE_" + rightNow.toISOString().slice(0, 10).replace(/[^0-9]/g, "");
        var hcpname = $("#txtDoctorName").val();

        hcoSearcher.parent().parent().find('input').val(hcpname + "(" + selectItem.data("hco-name") + ")");
        hcoSearcher.parent().parent().find('input').data("OrganizationCode", selectItem.data("hco-code"));
        hcoSearcher.parent().parent().find('input').data("OrganizationName", selectItem.data("hco-name"));
        hcoSearcher.parent().parent().find('input').data("HCPCode", hcpcode);
        hcoSearcher.parent().parent().find('input').data("HCPName", hcpname);
        hcoSearcher.parent().parent().find('input').data("SpecialtyName", "의사");
        // <!-- Ver 1.0.7 : Go-Direct -->
    }

    // <!-- Ver 1.0.7 : Go-Direct -->
    $(".hco-searcher-modal #btnOk").click(function () {
        var hcoCode = $('.hco-searcher-modal #txtSelectedHCO').data("hco-code");
        
        if (!hcoCode || hcoCode.length < 1) {
            fn_showInformation({
                title: "Confirm!",
                message: "Please select a hco."
            })
            return;
        }

        var hcpname = $("#txtDoctorName").val();
        
        if (!hcpname || hcpname.length < 1) {
            console.log("여기여기여기");
            fn_showInformation({
                title: "Confirm!",
                message: "Please select a HCP."
            })
            return;
        } 
        
        setHCOModalSelectItem();
        $('.hco-searcher-modal').modal("hide");
        
        
    });
     //< !--Ver 1.0.7 : Go - Direct-- >

    $("#jsGridHCOSearcher").jsGrid({
        width: "100%",
        height: "250px",

        sorting: true,
        paging: false,
        autoload: false,
        rowDoubleClick: function (args) {
            //var item = args.item;
            //$('.hco-searcher-modal #txtSelectedHCO').val(item.HCO_NAME + "(" + item.HCO_CODE + ")");
            //$('.hco-searcher-modal #txtSelectedHCO').data("hco-code", item.HCO_CODE);
            //$('.hco-searcher-modal #txtSelectedHCO').data("hco-name", item.HCO_NAME);
            //setHCOModalSelectItem();
            //$('.hco-searcher-modal').modal("hide");
        },
        rowClick: function (args) {
            var item = args.item;
            $('.hco-searcher-modal #txtSelectedHCO').val(item.HCO_NAME + "(" + item.HCO_CODE + ")");
            $('.hco-searcher-modal #txtSelectedHCO').data("hco-code", item.HCO_CODE);
            $('.hco-searcher-modal #txtSelectedHCO').data("hco-name", item.HCO_NAME);
            var row = $(args.event.target).closest("tr");

            if (this._clicked_row != null) {
                this._clicked_row.removeClass('jsgrid-clicked-row');
            }
            this._clicked_row = row;

            row.addClass('jsgrid-clicked-row');
        },
        controller: {
            loadData: function () {
                if (!clearJsGrid) {
                    var keyword = $(".hco-searcher-modal #txtHCOSearcher").val();
                    var selectUrl = COMMON_SERVICE_URL + "/SelectHCO";
                    var d = $.Deferred();
                    $.ajax({
                        url: selectUrl,
                        data: JSON.stringify({ keyword: keyword, hcoType:'ALL' }),
                        type: "POST",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                    }).done(function (response) {
                        d.resolve(response);
                    }).fail(function (jqXHR, textStatus, errorThrown) {
                        d.reject(jqXHR);
                        fn_showError({ message: jqXHR.responseText });
                    });

                    return d.promise();
                } else {
                    clearJsGrid = false;
                    return [];
                }
            }
        },

        fields: [
            { name: "HCO_CODE", title: "HCO Code", type: "text", width: 100 },
            { name: "HCO_NAME", title: "HCO Name", type: "text", width: 120 },
            { name: "ADDRESS", title: "Address", type: "text", width: 120 },
        ]
    });


    /* Search 버튼 클릭 */
    $(".hco-searcher-modal #btnHCOSearcher").click(function () {
        searchHCO();
    });

    /* Enter Key */
    $(".hco-searcher-modal #txtHCOSearcher").on("keypress", function (event) {
        if (event.keyCode == 13) {
            searchHCO();
        }
    });

    //$(".hco-searcher-modal #btnOk").click(function () {
    //    var hcoCode = $('.hco-searcher-modal #txtSelectedHCO').data("hco-code");
    //    if (!hcoCode || hcoCode.length < 1) {
    //        fn_showInformation({
    //            title: "Confirm!",
    //            message: "Please select a hco."
    //        })
    //        return;
    //    }
    //
    //    setHCOModalSelectItem();
    //    $('.hco-searcher-modal').modal("hide");
    //});

    function initHCOSearcherModal() {
        $(".hco-searcher-modal #txtHCOSearcher").val("");

        $('.hco-searcher-modal #txtSelectedHCO').val("");
        $('.hco-searcher-modal #txtSelectedHCO').data("hco-code", "");
        $('.hco-searcher-modal #txtSelectedHCO').data("hco-name", "");
        clearJsGrid = true;
        $("#jsGridHCOSearcher").jsGrid("loadData");
    }
});


function searchHCO() {

    var keyword = $('.hco-searcher-modal #txtHCOSearcher').val();
    if (!keyword || keyword.length < 1) {
        fn_showInformation({
            title: "Confirm!",
            message: "조건을 입력바랍니다."
        })
        return;
    }
    $("#jsGridHCOSearcher").jsGrid("loadData");
}