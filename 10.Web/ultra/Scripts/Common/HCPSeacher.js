$(function () {

    var HCPSearcher;
    var clearJsGrid = false;

    $('.hcp-searcher-modal').on('show.bs.modal', function (event) {
        initHCPSearcherModal();
        var button = $(event.relatedTarget);
        HCPSearcher = button;
    });

    function setHCPModalSelectItem() {
        var selectItem = $('.hcp-searcher-modal #txtSelectedHCP');

        HCPSearcher.parent().parent().find('input').val(selectItem.data("hcp-name") + "(" + selectItem.data("hco-name") + ")");
        HCPSearcher.parent().parent().find('input').data("HCOCode", selectItem.data("hco-code"));
        HCPSearcher.parent().parent().find('input').data("HCOName", selectItem.data("hco-name"));
        HCPSearcher.parent().parent().find('input').data("HCPCode", selectItem.data("hcp-code"));
        HCPSearcher.parent().parent().find('input').data("HCPName", selectItem.data("hcp-name"));
    }

    $("#jsGridHCPSearcher").jsGrid({
        width: "100%",
        height: "250px",
        sorting: true,
        paging: false,
        autoload: false,
        rowClick: function (args) {
            var item = args.item;
            $('.hcp-searcher-modal #txtSelectedHCP').val(item.HCPName + "(" + item.OrganizationName + ")");
            $('.hcp-searcher-modal #txtSelectedHCP').data("hco-code", item.OrganizationCode);
            $('.hcp-searcher-modal #txtSelectedHCP').data("hco-name", item.OrganizationName);
            $('.hcp-searcher-modal #txtSelectedHCP').data("hcp-code", item.HCPCode);
            $('.hcp-searcher-modal #txtSelectedHCP').data("hcp-name", item.HCPName);
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
                    var hcpName = $(".hcp-searcher-modal #txtHCPSearcher").val();

                    var search = {
                        hcpName: hcpName,
                        orgName: "",
                        speName: "",
                        processID: ""
                    };

                    var selectUrl = COMMON_SERVICE_URL + "/SelectHCP";
                    var d = $.Deferred();
                    $.ajax({
                        url: selectUrl,
                        data: JSON.stringify(search),
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
            { name: "HCPCode", title: "HCP Code", type: "text", css: "hide", width: 0 },
            { name: "HCPName", title: "HCP Name", type: "text", width: 120 },
            { name: "OrganizationCode", title: "Organization Code", type: "text", css: "hide", width: 0 },
            { name: "OrganizationName", title: "Organization Name", type: "text", width: 120 },
        ]
    });


    /* Search 버튼 클릭 */
    $(".hcp-searcher-modal #btnHCPSearcher").click(function () {
        searchHCP();
    });

    /* Enter Key */
    $(".hcp-searcher-modal #txtHCPSearcher").on("keypress", function (event) {
        if (event.keyCode == 13) {
            searchHCP();
        }
    });

    $(".hcp-searcher-modal #btnOk").click(function () {
        var hcpCode = $('.hcp-searcher-modal #txtSelectedHCP').data("hcp-code");

        if (!hcpCode || hcpCode.length < 1) {
            fn_showInformation({
                title: "Confirm!",
                message: "Please select a hcp."
            })
            return;
        }

        setHCPModalSelectItem();
        $('.hcp-searcher-modal').modal("hide");
    });

    function initHCPSearcherModal() {
        $(".hcp-searcher-modal #txtHCPSearcher").val("");

        $('.hcp-searcher-modal #txtSelectedHCP').val("");
        $('.hcp-searcher-modal #txtSelectedHCP').data("hco-code", "");
        $('.hcp-searcher-modal #txtSelectedHCP').data("hco-name", "");
        $("#txtHCPName").val("")
        clearJsGrid = true;
        $("#jsGridHCPSearcher").jsGrid("loadData");
    }
});


function searchHCP() {

    var keyword = $('.hcp-searcher-modal #txtHCPSearcher').val();
    if (!keyword || keyword.length < 1) {
        fn_showInformation({
            title: "Confirm!",
            message: "조건을 입력바랍니다."
        })
        return;
    }
    clearJsGrid = false;
    $("#jsGridHCPSearcher").jsGrid("loadData");
}