$(function () {

    var cookieValue = $.cookie('ultraUserGroups');
    var isAdmin = false;
    if (cookieValue != null && cookieValue.indexOf('ef.u.kr_localappl_87_medical_admin') >= 0)
    {
        isAdmin = true;
    }

    $("#jsGridList").jsGrid({
        width: "100%",
        height: "500px",
        sorting: true,
        paging: true,
        pageSize: GRID_LIST_COUNT,
        autoload: true,
        rowDoubleClick: function (args) { 
            var item = args.item;
            var url = "/ultra/Pages/Medical/Study/StudyDetail.aspx";
            fn_AddTabPage(url, "", "Study - " + item.TITLE, item.MEDICAL_IDX );
        },
        controller: {
            loadData: function () {

                var selectUrl = MEDICAL_SERVICE_URL + "/SelectMedicalStudyList/" + $('input[id$=hhdUserID]').val() + "/" + isAdmin.toString();
                var d = $.Deferred();

                $.ajax({
                    url: selectUrl,
                    type: "GET",
                    dataType: "json",
                    cache: false,
                    contentType: "application/json; charset=utf-8",
                }).done(function (response) {
                    d.resolve(response);
                }).fail(function (jqXHR, textStatus, errorThrown) {
                    d.reject(jqXHR);
                });

                return d.promise();
            }
        },

        fields: [
            { name: "CATEGORY", title: "Category", type: "text", width: 50 },
            { name: "STATUS", title: "Status", type: "text", width: 40 },
            { name: "TEAM", title: "Team", type: "text", width: 40 },
            { name: "IMPACT_NO", title: "Impact No", type: "text", width: 40 },
            { name: "TITLE", title: "Title", type: "text" },
            //{ name: "METERIAL_NO", title: "Meterial No", type: "text" },
            { name: "APPROVAL_NO", title: "Approval No", type: "text", width: 85 },
            { name: "APPROVAL_DATE", title: "Approval Date", type: "text", width: 50 },
            //{ name: "COST_INFORMATION", title: "Const Infomation", type: "text" },
            { name: "AUTHOR", title: "Author", type: "text", width: 60 }
        ]
    });
 

});
   
function fn_NewStudyPage()
{
    var url = "/ultra/Pages/Medical/Study/StudyDetail.aspx";
    fn_AddTabPage(url, null, "New Study", "");
}
