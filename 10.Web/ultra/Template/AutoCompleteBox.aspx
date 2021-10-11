<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AutoCompleteBox.aspx.cs" Inherits="Template_AutoCompleteBox" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <link rel="stylesheet" href="/ultra/Styles/TextExt/css/textext.core.css" />
    <link rel="stylesheet" href="/ultra/Styles/TextExt/css/textext.plugin.arrow.css" />
    <link rel="stylesheet" href="/ultra/Styles/TextExt/css/textext.plugin.autocomplete.css" />
    <link rel="stylesheet" href="/ultra/Styles/TextExt/css/textext.plugin.clear.css" />
    <link rel="stylesheet" href="/ultra/Styles/TextExt/css/textext.plugin.focus.css" />
    <link rel="stylesheet" href="/ultra/Styles/TextExt/css/textext.plugin.prompt.css" />
    <link rel="stylesheet" href="/ultra/Styles/TextExt/css/textext.plugin.tags.css" />
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <textarea id="textarea" class="example" rows="1" style="width: 300px"></textarea>
        </div>
        <button type="button" id="btnTest" onclick="fn_GetTags('textarea')">GetTags</button>
        <script type="text/javascript" src="/ultra/Scripts/JQuery/jquery-3.2.1.min.js"></script>
        <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.core.js"></script>
        <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.plugin.ajax.js"></script>
        <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.plugin.arrow.js"></script>
        <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.plugin.autocomplete.js"></script>
        <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.plugin.clear.js"></script>
        <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.plugin.filter.js"></script>
        <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.plugin.focus.js"></script>
        <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.plugin.prompt.js"></script>
        <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.plugin.suggestions.js"></script>
        <script type="text/javascript" src="/ultra/Styles/TextExt/js/textext.plugin.tags.js"></script>
        <script type="text/javascript">

            $(function () {
                $('#textarea').textext({
                    plugins: 'autocomplete filter tags ajax',
                    ajax: {
                        url: 'http://localhost/ultra-svc/UltraCommon.svc/selectuserautocompletelist/',
                        dataType: 'json',
                        cacheResults: true,
                    },
                    html:{
                        hidden : '<input id=hddForwardEntries type=hidden runat="server"/>',
                    },
                    ext: {
                        itemManager: {
                            stringToItem: function (str) {
                                var $div = $(str);
                                var user = $div.data("user");
                                return { USER_ID: user.USER_ID, FULL_NAME: user.FULL_NAME, ORG_ACRONYM: user.ORG_ACRONYM }
                            },
                            itemToString: function (item) {
                                return "<div data-user='" + JSON.stringify(item) + "'>" + item.FULL_NAME + "(" + item.ORG_ACRONYM + ")</div>";
                            },
                            compareItems: function (item1, item2) {
                                return item1.USER_ID == item2.USER_ID;
                            },
                            filter: function (list, data) {
                                var result = [], i, item;
                                var input = data.toLowerCase();
                                for (i = 0; i < list.length ; i++) {
                                    item = list[i];
                                    if (item.USER_ID.toLowerCase().indexOf(input) == 0
                                        || item.FULL_NAME.toLowerCase().indexOf(input) == 0) {
                                        result.push(item);
                                    }
                                }
                                return result;
                            }
                        }
                    }
                }).bind('isTagAllowed', function (e, data) {
                    if (data.tag.USER_ID) data.result = true;
                    else data.result = false;
                });
            });

            $(function () {
                var users = [];
                users.push({ USER_ID: "BKKWK", FULL_NAME: "WooKyung Kim", ORG_ACRONYM: "ITAM" });
                users.push({ USER_ID: "SGWVX", FULL_NAME: "Youngwoo Lee", ORG_ACRONYM: "ITAM" });
                $('#textarea').textext()[0].tags().addTags(users);
            });

            function fn_GetTags(textext) {
                var elements = $('#' + textext).parent().find('input[type=hidden]').val();
            }
        </script>
    </form>
</body>
</html>
