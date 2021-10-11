
var productSearch = (function () {
    var instance;
    var fcCallback;
    var sampleType = "samplecode";
    function createInstance() {
        if (productSearch !== undefined) {
            instance = new Object();
        }
        return instance;
    }

    var productSearcher;
    var clearJsGrid = false;

    $("#jsGridproductSearcher").jsGrid({
        width: "100%",
        height: "250px",
        sorting: true,
        paging: false,
        autoload: true,
        rowDoubleClick: function (args) {
            var item = args.item;
            $('.product-searcher-modal #txtSelectedProduct').val(item.PRODUCT_NAME + "(" + item.SAMPLE_CODE + ")");
            $('.product-searcher-modal #txtSelectedProduct').data("PRODUCT_CODE", item.PRODUCT_CODE);
            $('.product-searcher-modal #txtSelectedProduct').data("SAMPLE_CODE", item.SAMPLE_CODE);
            $('.product-searcher-modal #txtSelectedProduct').data("PRODUCT_NAME", item.PRODUCT_NAME);
            $('.product-searcher-modal #txtSelectedProduct').data("COMPANY_NAME", item.COMPANY_NAME);
            $('.product-searcher-modal #txtSelectedProduct').data("SAMPLE_NAME", item.SAMPLE_NAME);
            $('.product-searcher-modal #txtSelectedProduct').data("BASE_PRICE", item.BASE_PRICE);
            fcCallback($('.product-searcher-modal #txtSelectedProduct').data());
            $('.product-searcher-modal').modal("hide");
        },
        rowClick: function (args) {
            var item = args.item;
            $('.product-searcher-modal #txtSelectedProduct').val(item.PRODUCT_NAME + "(" + item.SAMPLE_CODE + ")");
            $('.product-searcher-modal #txtSelectedProduct').data("PRODUCT_CODE", item.PRODUCT_CODE);
            $('.product-searcher-modal #txtSelectedProduct').data("SAMPLE_CODE", item.SAMPLE_CODE);
            $('.product-searcher-modal #txtSelectedProduct').data("PRODUCT_NAME", item.PRODUCT_NAME);
            $('.product-searcher-modal #txtSelectedProduct').data("COMPANY_NAME", item.COMPANY_NAME);
            $('.product-searcher-modal #txtSelectedProduct').data("SAMPLE_NAME", item.SAMPLE_NAME);
            $('.product-searcher-modal #txtSelectedProduct').data("BASE_PRICE", item.BASE_PRICE);
            var row = $(args.event.target).closest("tr");

            if (this._clicked_row != null) {
                this._clicked_row.removeClass('jsgrid-clicked-row');
            }
            this._clicked_row = row;

             row.addClass('jsgrid-clicked-row');
        },
        controller: {
            loadData: function () {


                var keyword = $(".product-searcher-modal #txtproductSearcher").val();
                
                if (keyword.length <= 0) keyword = 'ALL';
                var selectUrl = COMMON_SERVICE_URL + "/SelectSampleList/" + keyword + "/" + sampleType;
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

            }
        },
        fields: [
            { name: "PRODUCT_CODE", title: "Product Code", type: "text", width: 80 },
            { name: "PRODUCT_NAME", title: "Product Name", type: "text" },
        ]
    });

    $('.product-searcher-modal').on('show.bs.modal', function (event) {
        //Modal Popup 초기화
        initProductSearcherModal();
        var button = $(event.relatedTarget);
        productSearcher = button;
    });

    $('.product-searcher-modal').on('hide.bs.modal', function (event) {
     
    });
     
    /* Search 버튼 클릭 */
    $(".product-searcher-modal #btnProductSearcher").click(function () {
   
        searchProduct();
    });

    /* Enter Key */
    $(".product-searcher-modal #txtproductSearcher").on("keypress", function (event) {
        if (event.keyCode == 13) {
            searchProduct();
        }
    });

    $("#btnOk").click(function () {
        var productCode=  $('.product-searcher-modal #txtSelectedProduct').data("PRODUCT_CODE");
        
        if (!productCode || productCode.length < 1) {
            fn_showInformation({
                title: "Confirm!",
                message: "Please select a product."
            })
            return;
        }
        fcCallback($('.product-searcher-modal #txtSelectedProduct').data());
        $('.product-searcher-modal').modal("hide");
    });

    //Modal Popup 초기화
    function initProductSearcherModal() {
        $(".product-searcher-modal #txtproductSearcher").val("");

        $('.product-searcher-modal #txtSelectedProduct').val("");
        $('.product-searcher-modal #txtSelectedProduct').data("PROJECT_CODE", "");

        clearJsGrid = true;
        $("#jsGridproductSearcher").jsGrid("loadData");
    }

    function searchProduct() {
        $("#jsGridproductSearcher").jsGrid("loadData");
    }

    return {
           getInstance: function () {
               if (!instance) {
                       instance = createInstance();
                   }
               return instance;
           }, show: function(a, b) {
                productSearch.getInstance();
                fcCallback = a || {};
                sampleType = b;
                $('.product-searcher-modal').modal("show");
            }  
        };


})();


if ($(".product-searcher .input-group-btn").length > 0) {
    $(".product-searcher .input-group-btn").on("click", function () {
        var type = "";

        //선택한 bu 항목이 있을 경우 해당 항목만 조회함
        if ($("input[name=rdoBU]:checked").length > 0)
            type = $("input[name=rdoBU]:checked").val();

        productSearch.show(setProductSearcher, type);
    });
}

var afterProductSearcher;
function setProductSearcher(item) {
    var txtSearcher = $(".product-searcher input");
    if (item == undefined || item == null) {
        txtSearcher.val("");
        txtSearcher.data("PRODUCT_CODE", "");
        txtSearcher.data("SAMPLE_CODE", "");
        txtSearcher.data("PRODUCT_NAME", "");
        txtSearcher.data("COMPANY_NAME", "");
        txtSearcher.data("SAMPLE_NAME", "");
        txtSearcher.data("BASE_PRICE", "");
        return;
    }
    txtSearcher.val(item.PRODUCT_NAME + "(" + item.SAMPLE_CODE + ")");
    txtSearcher.data("PRODUCT_CODE", item.PRODUCT_CODE);
    txtSearcher.data("SAMPLE_CODE", item.SAMPLE_CODE);
    txtSearcher.data("PRODUCT_NAME", item.PRODUCT_NAME);
    txtSearcher.data("COMPANY_NAME", item.COMPANY_NAME);
    txtSearcher.data("SAMPLE_NAME", item.SAMPLE_NAME);
    txtSearcher.data("BASE_PRICE", item.BASE_PRICE);

    if (afterProductSearcher != null) afterProductSearcher(item);
}

function getProductSearcher() {
    var txtSearcher = $(".product-searcher input");
    return txtSearcher.data();
}