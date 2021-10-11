$(function () {

    var hostSearcher;
    var clearJsGrid = false;

    $('.host-searcher-modal').on('show.bs.modal', function (event) {
        inithostSearcherModal();
        var button = $(event.relatedTarget);
        hostSearcher = button;
    });

    function setModalSelectItem() {
        var selectItem = $('.host-searcher-modal #txtSelectedHost');
        var txtSearcher = $(".host-searcher input");
        hostSearcher.parent().parent().find('input').val(selectItem.val());
        hostSearcher.parent().parent().find('input').data('society-id', selectItem.data("society-id"));
    }

    $("#jsGridHostSearcher").jsGrid({
        width: "100%",
        height: "250px",

        sorting: true,
        paging: false,
        autoload: false,
        rowDoubleClick: function (args) {
            var item = args.item;
            $('.host-searcher-modal #txtSelectedHost').val(item.SOCIETY_NAME);
            $('.host-searcher-modal #txtSelectedHost').data("society-id", item.SOCIETY_IDX);
            setModalSelectItem();
            $('.host-searcher-modal').modal("hide");
        },
        rowClick: function (args) {
            var item = args.item;
            $('.host-searcher-modal #txtSelectedHost').val(item.SOCIETY_NAME);
            $('.host-searcher-modal #txtSelectedHost').data("society-id", item.SOCIETY_IDX);
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
                    var keyword = $(".host-searcher-modal #txtHostSearcher").val();
                    var selectUrl = COMMON_SERVICE_URL + "/SearchMedicalSocietyList/Y/" + keyword;
                    var d = $.Deferred();

                    $.ajax({
                        url: selectUrl,
                        type: "GET",
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                    }).done(function (response) {
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
            { name: "SOCIETY_IDX", title: "idx", type: "text", css: "hide", width: 0 },
            { name: "SOCIETY_NAME", title: "Name", type: "text" },
        ]
    });


    /* Search 버튼 클릭 */
    $(".host-searcher-modal #btnHostSearcher").click(function () {
        searchhost();
    });

    /* Enter Key */
    $(".host-searcher-modal #txtHostSearcher").on("keypress", function (event) {
        if (event.keyCode == 13) {
            searchhost();
        }
    });

    $(".host-searcher-modal #btnOk").click(function () {
        var hostID = $('.host-searcher-modal #txtSelectedHost').data("society-id");
        if (!hostID || hostID.length < 1) {
            fn_showInformation({
                title: "Confirm!",
                message: "Please select a society medical."
            })
            return;
        }
        setModalSelectItem();
        $('.host-searcher-modal').modal("hide");
    });

    function inithostSearcherModal() {
        $(".host-searcher-modal #txtHostSearcher").val("");

        $('.host-searcher-modal #txtSelectedHost').val("");
        $('.host-searcher-modal #txtSelectedHost').data("society-id", "");

        clearJsGrid = true;
        $("#jsGridHostSearcher").jsGrid("loadData");
    }

    function searchhost() {
        $("#jsGridHostSearcher").jsGrid("loadData");
    }
});


function setHostSearcher(item)
{
    var txtSearcher = $(".host-searcher input");

    if (item == undefined || item == null) {
        txtSearcher.val("");
        txtSearcher.data('society-id', "");
        return;
    }
    txtSearcher.val(item.SOCIETY_NAME);
    txtSearcher.data('society-id', item.SOCIETY_IDX);
}

function getHostSearcher()
{
    var txtSearcher = $(".host-searcher input");
    return txtSearcher.data('society-id');
}