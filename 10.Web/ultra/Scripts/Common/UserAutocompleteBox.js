$(function () {
    $("#acbReviewer").each(
        function initAutocomplete(autobox) {
            $(this).textext({
                plugins: "autocomplete filter tags ajax",
                ajax: {
                    url: COMMON_SERVICE_URL + "/selectuserautocompletelist/",
                    dataType: 'json',
                    cacheResults: true,
                },
                ext: {
                    itemManager: {
                        stringToItem: function (str) {
                            var $div = $(str);
                            var user = $div.data("user");
                            if (user)
                                return { USER_ID: user.USER_ID, FULL_NAME: user.FULL_NAME, ORG_ACRONYM: user.ORG_ACRONYM }
                            else {
                                return null;
                            }
                        },
                        itemToString: function (item) {
                            return "<div class='text-label' data-user='" + JSON.stringify(item) + "'>" + item.FULL_NAME + "(" + item.ORG_ACRONYM + ")</div>";
                        },
                        compareItems: function (item1, item2) {
                            return item1.USER_ID == item2.USER_ID;
                        },
                        filter: function (list, data) {
                            var result = [], i, item;
                            var input = data.toLowerCase();
                            var l = list.length;
                            for (i = 0; i < l ; i++) {
                                item = list[i];
                                if (item.USER_ID.toLowerCase().indexOf(input) == 0
                                    || item.FULL_NAME.toLowerCase().indexOf(input) == 0) {
                                    result.push(item);
                                }
                            }
                            return result;
                        }
                    }
                },

            }).bind("isTagAllowed", function (e, data) {
                if (data.tag) {
                    if (data.tag.USER_ID) data.result = true;
                    else data.result = false;
                } else
                    data.result = false;
            });
        }
    );

    $("#acbForward").each(
        function initAutocomplete(autobox) {
            $(this).textext({
                plugins: "autocomplete filter tags ajax",
                ajax: {
                    url: COMMON_SERVICE_URL + "/selectuserautocompletelist/",
                    dataType: 'json',
                    cacheResults: true,
                },
                ext: {
                    itemManager: {
                        stringToItem: function (str) {
                            var $div = $(str);
                            var user = $div.data("user");
                            if (user)
                                return { USER_ID: user.USER_ID, FULL_NAME: user.FULL_NAME, ORG_ACRONYM: user.ORG_ACRONYM }
                            else {
                                return null;
                            }
                        },
                        itemToString: function (item) {
                            return "<div class='text-label' data-user='" + JSON.stringify(item) + "'>" + item.FULL_NAME + "(" + item.ORG_ACRONYM + ")</div>";
                        },
                        compareItems: function (item1, item2) {
                            return item1.USER_ID == item2.USER_ID;
                        },
                        filter: function (list, data) {
                            var result = [], i, item;
                            var input = data.toLowerCase();
                            var l = list.length;
                            for (i = 0; i < l ; i++) {
                                item = list[i];
                                if (item.USER_ID.toLowerCase().indexOf(input) == 0
                                    || item.FULL_NAME.toLowerCase().indexOf(input) == 0) {
                                    result.push(item);
                                }
                            }
                            return result;
                        }
                    }
                },

            }).bind("isTagAllowed", function (e, data) {
                if (data.tag) {
                    if (data.tag.USER_ID) data.result = true;
                    else data.result = false;
                } else
                    data.result = false;
            });
        }
    );


    $("#taProduct").each(
        function initAutocomplete(autobox) {
            $(this).textext({
                plugins: "autocomplete filter tags ajax",
                ajax: {
                    url: COMMON_SERVICE_URL + "/SelectMasterProductList/",
                    dataType: 'json',
                    cacheResults: true,
                },
                ext: {
                    itemManager: {
                        stringToItem: function (str) {
                            var $div = $(str);
                            var data = $div.data("product");
                            return { PRODUCT_CODE: data.PRODUCT_CODE, PRODUCT_NAME: data.PRODUCT_NAME, COMPANY_NAME: data.COMPANY_NAME }
                        },
                        itemToString: function (item) {
                            return "<div data-product='" + JSON.stringify(item) + "'>" + item.PRODUCT_NAME + "</div>";
                        },
                        compareItems: function (item1, item2) {
                            return item1.PRODUCT_CODE == item2.PRODUCT_CODE;
                        },
                        filter: function (list, data) {
                            var result = [], i, item;
                            var input = data.toLowerCase();
                            var l = list.length;
                            for (i = 0; i < l ; i++) {
                                item = list[i];
                                if (item.PRODUCT_CODE.toLowerCase().indexOf(input) == 0
                                    || item.PRODUCT_NAME.toLowerCase().indexOf(input) == 0) {
                                    result.push(item);
                                }
                            }
                            return result;
                        }
                    }
                },
            }).bind("isTagAllowed", function (e, data) {
                if (data.tag.PRODUCT_CODE) data.result = true;
                else data.result = false;
            });
        });

    $("#taCRMProduct").each(
        function initAutocomplete(autobox) {
            $(this).textext({
                plugins: "autocomplete filter tags ajax",
                ajax: {
                    url: COMMON_SERVICE_URL + "/SelectCRMProduct/",
                    dataType: 'json',
                    cacheResults: true,
                },
                ext: {
                    itemManager: {
                        stringToItem: function (str) {
                            var $div = $(str);
                            var data = $div.data("product");
                            return { PRODUCT_CODE: data.PRODUCT_CODE, PRODUCT_NAME: data.PRODUCT_NAME }
                        },
                        itemToString: function (item) {
                            return "<div data-product='" + JSON.stringify(item) + "'>" + item.PRODUCT_NAME + "</div>";
                        },
                        compareItems: function (item1, item2) {
                            return item1.PRODUCT_CODE == item2.PRODUCT_CODE;
                        },
                        filter: function (list, data) {
                            var result = [], i, item;
                            var input = data.toLowerCase();
                            var l = list.length;
                            for (i = 0; i < l ; i++) {
                                item = list[i];
                                if (item.PRODUCT_CODE.toLowerCase().indexOf(input) == 0
                                    || item.PRODUCT_NAME.toLowerCase().indexOf(input) == 0) {
                                    result.push(item);
                                }
                            }
                            return result;
                        }
                    }
                },
            }).bind("isTagAllowed", function (e, data) {
                if (data.tag.PRODUCT_CODE) data.result = true;
                else data.result = false;
            });
        });

    $(".text-core .example").on("blur", function (e) {
        if ($(this).text().length > 0) {
            $(this).text("");
        }
    });


});