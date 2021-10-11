$(function () {

    var RADInjectorSearcher;
    var clearJsGrid = false;

    $('.RADInjector-searcher-modal').on('show.bs.modal', function (event) {
        initRADInjectorSearcherModal();
        var button = $(event.relatedTarget);
        RADInjectorSearcher = button;
    });

    function setModalSelectItem() {
        var selectItem = $('.RADInjector-searcher-modal #txtSelectedRADInjector');
        var txtSearcher = $(".RADInjector-searcher input");
        RADInjectorSearcher.parent().parent().find('input').val(selectItem.val());
        RADInjectorSearcher.parent().parent().find('input').data("PRODUCT_CODE", selectItem.data("PRODUCT_CODE"));
        RADInjectorSearcher.parent().parent().find('input').data("PRODUCT_NAME", selectItem.data("PRODUCT_NAME"));
    }

    $("#jsGridRADInjectorSearcher").jsGrid({
        width: "100%",
        height: "250px",

        sorting: true,
        paging: false,
        autoload: false,
        rowDoubleClick: function (args) {
            var item = args.item;
            $('.RADInjector-searcher-modal #txtSelectedRADInjector').val(item.PRODUCT_NAME + "(" + item.REGISTER_NUM+")");
            $('.RADInjector-searcher-modal #txtSelectedRADInjector').data("PRODUCT_CODE", item.PRODUCT_CODE);
            $('.RADInjector-searcher-modal #txtSelectedRADInjector').data("PRODUCT_NAME", item.PRODUCT_NAME);
            
            setModalSelectItem();
            $('.RADInjector-searcher-modal').modal("hide");
        },
        rowClick: function (args) {
            var item = args.item;
            $('.RADInjector-searcher-modal #txtSelectedRADInjector').val(item.PRODUCT_NAME + "(" + item.REGISTER_NUM + ")");
            $('.RADInjector-searcher-modal #txtSelectedRADInjector').data("PRODUCT_CODE", item.PRODUCT_CODE);
            $('.RADInjector-searcher-modal #txtSelectedRADInjector').data("PRODUCT_NAME", item.PRODUCT_NAME);
            console.log($('.RADInjector-searcher-modal #txtSelectedRADInjector'));
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
                    var keyword = $(".RADInjector-searcher-modal #txtRADInjectorSearcher").val();
                    var selectUrl = MEDICAL_SERVICE_URL + "/SelectRADInjectorMasterList/" + $('input[id$=hhdUserID]').val() + "/ALL/" + keyword;
                    var d = $.Deferred();

                    $.ajax({
                        url: selectUrl,
                        type: "GET",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                    }).done(function (response) {
                        console.log(response);
                        d.resolve(response);
                    }).fail(function (jqXHR, textStatus, errorThrown) {
                    });

                    return d.promise();
                } else {
                    clearJsGrid = false;
                    return [];
                }
            }

        },

        fields: [
            { name: "PRODUCT_CODE", title: "idx", type: "text", css: "hide", width: 0 },
            { name: "PRODUCT_NAME", title: "Name", type: "text", itemTemplate: function (value, item) { return value+"("+item.REGISTER_NUM+")"} },
        ]
    });


    /* Search 버튼 클릭 */
    $(".RADInjector-searcher-modal #btnRADInjectorSearcher").click(function () {
        searchMedical();
    });

    /* Enter Key */
    $(".RADInjector-searcher-modal #txtRADInjectorSearcher").on("keypress", function (event) {
        if (event.keyCode == 13) {
            searchMedical();
        }
    });

    $(".RADInjector-searcher-modal #btnOk").click(function () {
        var medicalID = $('.RADInjector-searcher-modal #txtSelectedRADInjector').data("PRODUCT_CODE");
        if (!medicalID || medicalID.length < 1) {
            fn_showInformation({
                title: "Confirm!",
                message: "Please select a Injector."
            })
            return;
        }
        setModalSelectItem();
        $('.RADInjector-searcher-modal').modal("hide");
    });

    function initRADInjectorSearcherModal() {
        $(".RADInjector-searcher-modal #txtRADInjectorSearcher").val("");

        $('.RADInjector-searcher-modal #txtSelectedMedical').val("");
        $('.RADInjector-searcher-modal #txtSelectedMedical').data("PRODUCT_CODE", "");

        clearJsGrid = true;
        $("#jsGridRADInjectorSearcher").jsGrid("loadData");
    }

    function searchMedical() {
        $("#jsGridRADInjectorSearcher").jsGrid("loadData");
    }
});


function setRADInjectorSearcher(item)
{
    var txtSearcher = $(".medical-searcher input");

    if (item == undefined || item == null) {
        txtSearcher.val("");
        txtSearcher.data('PRODUCT_CODE', "");
        txtSearcher.data("PRODUCT_NAME", "");
        return;
    }
    txtSearcher.val(item.PRODUCT_NAME + "(" + item.REGISTER_NUM + ")");
    txtSearcher.data("PRODUCT_CODE", item.PRODUCT_CODE);
    txtSearcher.data("PRODUCT_NAME", item.PRODUCT_NAME);
}

function getRADInjectorSearcher()
{
    var txtSearcher = $(".medical-searcher input");
    return txtSearcher.data("PRODUCT_CODE");
}