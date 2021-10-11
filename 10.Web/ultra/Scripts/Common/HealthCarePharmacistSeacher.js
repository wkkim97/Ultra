$(function () {

    var PharmacistSearcher;
    var clearJsGrid = false;

    $('.pharmacist-searcher-modal').on('show.bs.modal', function (event) {
        initPharmacistSearcherModal();
        var button = $(event.relatedTarget);
        PharmacistSearcher = button;
    });

    function setPharmacistModalSelectItem() {
        var selectItem = $('.pharmacist-searcher-modal #txtSelectedPharmacist');

        var rightNow = new Date();
        var hcpcode = "PHARMACIST_" + rightNow.toISOString().slice(0, 10).replace(/[^0-9]/g, "");
        var hcpname = $("#txtPharmacistName").val();

        PharmacistSearcher.parent().parent().find('input').val(hcpname + "(" + selectItem.data("hco-name") + ")");
        PharmacistSearcher.parent().parent().find('input').data("OrganizationCode", selectItem.data("hco-code"));
        PharmacistSearcher.parent().parent().find('input').data("OrganizationName", selectItem.data("hco-name"));
        PharmacistSearcher.parent().parent().find('input').data("HCPCode", hcpcode);
        PharmacistSearcher.parent().parent().find('input').data("HCPName", hcpname);
        PharmacistSearcher.parent().parent().find('input').data("SpecialtyName", "약사");
    }

    $("#jsGridPharmacistSearcher").jsGrid({
        width: "100%",
        height: "250px",
        sorting: true,
        paging: false,
        autoload: false,
        rowClick: function (args) {
            var item = args.item;
            $('.pharmacist-searcher-modal #txtSelectedPharmacist').val(item.HCO_NAME + "(" + item.HCO_CODE + ")");
            $('.pharmacist-searcher-modal #txtSelectedPharmacist').data("hco-code", item.HCO_CODE);
            $('.pharmacist-searcher-modal #txtSelectedPharmacist').data("hco-name", item.HCO_NAME);
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
                    var keyword = $(".pharmacist-searcher-modal #txtPharmacistSearcher").val();
                    var selectUrl = COMMON_SERVICE_URL + "/SelectHCO";
                    var d = $.Deferred();
                    $.ajax({
                        url: selectUrl,
                        data: JSON.stringify({ keyword: keyword, hcoType:'11' }),
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
    $(".pharmacist-searcher-modal #btnPharmacistSearcher").click(function () {
        searchPharmacist();
    });

    /* Enter Key */
    $(".pharmacist-searcher-modal #txtPharmacistSearcher").on("keypress", function (event) {
        if (event.keyCode == 13) {
            searchPharmacist();
        }
    });

    $(".pharmacist-searcher-modal #btnOk").click(function () {
        var hcoCode = $('.pharmacist-searcher-modal #txtSelectedPharmacist').data("hco-code");

        if (!hcoCode || hcoCode.length < 1) {
            fn_showInformation({
                title: "Confirm!",
                message: "Please select a hco."
            })
            return;
        }

        var hcpname = $("#txtPharmacistName").val();
        if (!hcpname || hcpname.length < 1) {
            fn_showInformation({
                title: "Confirm!",
                message: "Please select a hcp."
            })
            return;
        }

        setPharmacistModalSelectItem();
        $('.pharmacist-searcher-modal').modal("hide");
    });

    function initPharmacistSearcherModal() {
        $(".pharmacist-searcher-modal #txtPharmacistSearcher").val("");

        $('.pharmacist-searcher-modal #txtSelectedPharmacist').val("");
        $('.pharmacist-searcher-modal #txtSelectedPharmacist').data("hco-code", "");
        $('.pharmacist-searcher-modal #txtSelectedPharmacist').data("hco-name", "");
        $("#txtPharmacistName").val("")
        clearJsGrid = true;
        $("#jsGridPharmacistSearcher").jsGrid("loadData");
    }
});


function searchPharmacist() {

    var keyword = $('.pharmacist-searcher-modal #txtPharmacistSearcher').val();
    if (!keyword || keyword.length < 1) {
        fn_showInformation({
            title: "Confirm!",
            message: "조건을 입력바랍니다."
        })
        return;
    }
    clearJsGrid = false;
    $("#jsGridPharmacistSearcher").jsGrid("loadData");
}