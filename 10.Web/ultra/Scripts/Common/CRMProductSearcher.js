$(function () {

    var crmProductSearcher;
    var clearJsGrid = false;

    $('.crm-product-searcher-modal').on('show.bs.modal', function (event) {
        initCRMProductSearcherModal();
        var button = $(event.relatedTarget);
        crmProductSearcher = button;
    });

    function setCRMProductModalSelectItem() {
        //Modal에서 선택된
        var selectedItem = $('.crm-product-searcher-modal .product-list').data('selected-products');

        //기선택된 내용
        var products = crmProductSearcher.parent().data('crm-products');
        if (products) {
            var isExist = false;
            $.map(selectedItem, function (value1, i) {
                $.map(products, function (value2, i) {
                    if (value1.PRODUCT_CODE == value2.PRODUCT_CODE) {
                        isExist = true;
                    }
                });
                if (!isExist) {
                    products.push(value1);
                    var $li = $("<li id='" + value1.PRODUCT_CODE + "'><span class='btn btn-xs btn-gray'>" + value1.PRODUCT_CODE + "(" + value1.PRODUCT_NAME + ")" + "</span><a class='text-remove'></a></li>")
                    crmProductSearcher.parent().find('.product-list').append($li);
                }
            });
            crmProductSearcher.parent().data('crm-products', products);
        } else {
            crmProductSearcher.parent().data('crm-products', selectedItem);
            $.map(selectedItem, function (value, i) {
                var $li = $("<li id='" + value.PRODUCT_CODE + "'><span class='btn btn-xs btn-gray'>" + value.PRODUCT_CODE + "(" + value.PRODUCT_NAME + ")" + "</span><a class='text-remove'></a></li>")
                crmProductSearcher.parent().find('.product-list').append($li);
            });
        }
    }

    function selectCRMProduct(item) {
        var selectedProducts = $('.crm-product-searcher-modal .product-list').data('selected-products');
        if (selectedProducts) {
            var isExist = false;
            $.map(selectedProducts, function (value, i) {
                if (item.PRODUCT_CODE == value.PRODUCT_CODE) {
                    isExist = true;
                }
            });
            if (!isExist) {
                selectedProducts.push({ PRODUCT_CODE: item.PRODUCT_CODE, PRODUCT_NAME: item.PRODUCT_NAME, PRODUCT_TYPE: item.PRODUCT_TYPE.substring(0, 1) });
                $('.crm-product-searcher-modal .product-list').data('selected-products', selectedProducts);
                var $li = $("<li id='" + item.PRODUCT_CODE + "'><span class='btn btn-xs btn-gray'>" + item.PRODUCT_CODE + "(" + item.PRODUCT_NAME + ")" + "</span><a class='text-remove'></a></li>")
                $('.crm-product-searcher-modal .product-list').append($li);

            }
        } else {
            selectedProducts = [];
            var product = {
                PRODUCT_CODE: item.PRODUCT_CODE,
                PRODUCT_NAME: item.PRODUCT_NAME,
                PRODUCT_TYPE: item.PRODUCT_TYPE.substring(0, 1),
            };
            selectedProducts.push(product);
            $('.crm-product-searcher-modal .product-list').data('selected-products', selectedProducts);
            var $li = $("<li id='" + item.PRODUCT_CODE + "'><span class='btn btn-xs btn-gray'>" + item.PRODUCT_CODE + "(" + item.PRODUCT_NAME + ")" + "</span><a class='text-remove'></a></li>")
            $('.crm-product-searcher-modal .product-list').append($li);
        }
    }

    $("#jsGridCRMProductSearcher").jsGrid({
        width: "100%",
        height: "250px",

        sorting: true,
        paging: false,
        autoload: false,
        rowDoubleClick: function (args) {
            var item = args.item;
            selectCRMProduct(item);
        },
        controller: {
            loadData: function () {
                if (!clearJsGrid) {
                    var keyword = $(".crm-product-searcher-modal #txtCRMProduct").val();
                    var selectUrl = COMMON_SERVICE_URL + "/SelectCRMProduct/" + keyword;
                    var d = $.Deferred();

                    $.ajax({
                        url: selectUrl,
                        type: "GET",
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
            { name: "PRODUCT_CODE", title: "Product Code", type: "text", width: 60 },
            { name: "PRODUCT_NAME", title: "Product Name", type: "text" },
            {
                name: "STATUS", title: "Status", type: "text", width: 40, align: "center",
                itemTemplate: function (value, item) {
                    var $btnObj = $("<button>")
                                    .attr("type", "button")
                                    .addClass("btn btn-xs btn-red")
                                    .append($("<i>").addClass('fa fa-hand-o-up'))
                                    .on("click", function () {
                                        selectCRMProduct(item);
                                    });
                    return $btnObj;
                }
            },
        ]
    });


    /* Search 버튼 클릭 */
    $(".crm-product-searcher-modal #btnCRMProductSearcher").click(function () {
        searchCRMProduct();
    });

    /* Enter Key */
    $(".crm-product-searcher-modal #txtCRMProduct").on("keypress", function (event) {
        if (event.keyCode == 13) {
            searchCRMProduct();
        }
    });

    $(".crm-product-searcher-modal #btnOk").click(function () {
        var products = $('.crm-product-searcher-modal .product-list').data("selected-products");
        if (!products || products.length < 1) {
            fn_showInformation({
                title: "Confirm!",
                message: "Please select a product."
            })
            return;
        }

        setCRMProductModalSelectItem();
        $('.crm-product-searcher-modal').modal("hide");
    });

    function initCRMProductSearcherModal() {
        $(".crm-product-searcher-modal #txtCRMProduct").val("");
        $(".crm-product-searcher-modal .product-list").empty();
        $('.crm-product-searcher-modal .product-list').data("selected-products", "");

        clearJsGrid = true;
        $("#jsGridCRMProductSearcher").jsGrid("loadData");
    }

    $(".crm-product-searcher").on("click", ".text-remove", function (e) {
        e.preventDefault();
        var $li = $(this).parent();
        var idLI = $li.attr("id");
        $('.crm-product-searcher .product-list li').each(function (i) {
            if (idLI == $(this).attr('id')) {
                $(this).remove();
            }
        });
        var crmProducts = $('.crm-product-searcher').data('crm-products');
        if (crmProducts) {
            $.map(crmProducts, function (value, i) {
                if (idLI == value.PRODUCT_CODE) {
                    crmProducts.splice(i, 1);
                }
            });
        }
    });

    $(".crm-product-searcher-modal").on("click", ".text-remove", function (e) {
        e.preventDefault();
        var $li = $(this).parent();
        var idLI = $li.attr("id");

        $('.crm-product-searcher-modal .product-list li').each(function (i) {
            if (idLI == $(this).attr('id')) {
                $(this).remove();
            }
        });

        var selectedProducts = $('.crm-product-searcher-modal .product-list').data('selected-products');
        if (selectedProducts) {
            $.map(selectedProducts, function (value, i) {
                if (value.PRODUCT_CODE == idLI) {
                    selectedProducts.splice(i, 1);
                }
            });
        }
    });
});


function searchCRMProduct() {
    $("#jsGridCRMProductSearcher").jsGrid("loadData");
}