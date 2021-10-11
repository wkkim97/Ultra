$(function () {

    var medicalSearcher;
    var clearJsGrid = false;

    $('.medical-searcher-modal').on('show.bs.modal', function (event) {
        initMedicalSearcherModal();
        var button = $(event.relatedTarget);
        medicalSearcher = button;
    });

    function setModalSelectItem() {
        var selectItem = $('.medical-searcher-modal #txtSelectedMedical');
        var txtSearcher = $(".medical-searcher input");
        medicalSearcher.parent().parent().find('input').val(selectItem.val());
        medicalSearcher.parent().parent().find('input').data("IMPACT_NO", selectItem.data("IMPACT_NO"));
        medicalSearcher.parent().parent().find('input').data("TITLE", selectItem.data("TITLE"));
    }

    $("#jsGridMedicalSearcher").jsGrid({
        width: "100%",
        height: "250px",

        sorting: true,
        paging: false,
        autoload: false,
        rowDoubleClick: function (args) {
            var item = args.item;
            $('.medical-searcher-modal #txtSelectedMedical').val(item.TITLE);
            $('.medical-searcher-modal #txtSelectedMedical').data("IMPACT_NO", item.IMPACT_NO);
            $('.medical-searcher-modal #txtSelectedMedical').data("TITLE", item.TITLE);
            setModalSelectItem();
            $('.medical-searcher-modal').modal("hide");
        },
        rowClick: function (args) {
            var item = args.item;
            $('.medical-searcher-modal #txtSelectedMedical').val(item.TITLE);
            $('.medical-searcher-modal #txtSelectedMedical').data("IMPACT_NO", item.IMPACT_NO);
            $('.medical-searcher-modal #txtSelectedMedical').data("TITLE", item.TITLE);
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
                    var keyword = $(".medical-searcher-modal #txtMedicalSearcher").val();
                    var selectUrl = MEDICAL_SERVICE_URL + "/SelectMedicalMasterList/" + $('input[id$=hhdUserID]').val() + "/ALL/" + keyword;
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
            { name: "IMPACT_NO", title: "idx", type: "text", css: "hide", width: 0 },
            { name: "TITLE", title: "Name", type: "text" },
        ]
    });


    /* Search 버튼 클릭 */
    $(".medical-searcher-modal #btnMedicalSearcher").click(function () {
        searchMedical();
    });

    /* Enter Key */
    $(".medical-searcher-modal #txtMedicalSearcher").on("keypress", function (event) {
        if (event.keyCode == 13) {
            searchMedical();
        }
    });

    $(".medical-searcher-modal #btnOk").click(function () {
        var medicalID = $('.medical-searcher-modal #txtSelectedMedical').data("IMPACT_NO");
        if (!medicalID || medicalID.length < 1) {
            fn_showInformation({
                title: "Confirm!",
                message: "Please select a society medical."
            })
            return;
        }
        setModalSelectItem();
        $('.medical-searcher-modal').modal("hide");
    });

    function initMedicalSearcherModal() {
        $(".medical-searcher-modal #txtMedicalSearcher").val("");

        $('.medical-searcher-modal #txtSelectedMedical').val("");
        $('.medical-searcher-modal #txtSelectedMedical').data("IMPACT_NO", "");

        clearJsGrid = true;
        $("#jsGridMedicalSearcher").jsGrid("loadData");
    }

    function searchMedical() {
        $("#jsGridMedicalSearcher").jsGrid("loadData");
    }
});


function setMedicalSearcher(item)
{
    var txtSearcher = $(".medical-searcher input");

    if (item == undefined || item == null) {
        txtSearcher.val("");
        txtSearcher.data('IMPACT_NO', "");
        txtSearcher.data("TITLE", "");
        return;
    }
    txtSearcher.val(item.TITLE);
    txtSearcher.data("IMPACT_NO", item.IMPACT_NO);
    txtSearcher.data("TITLE", item.TITLE);
}

function getMedicalSearcher()
{
    var txtSearcher = $(".medical-searcher input");
    return txtSearcher.data("IMPACT_NO");
}