$(function () {
    

    $("#jsGridHCPList").jsGrid({
        width: "100%",
        height: "500px",
        sorting: true,
        paging: false,
        autoload: false,
        controller: {
            loadData: function () {
                var hcpCode = $("#searchText").val();
                if (hcpCode!=""){
                    var hddSelectDate = "2018";
                    var d1 = $.Deferred();

                    $.ajax({
                        type: "GET",
                        url: EVENT_SERVICE_URL + "/SelectEventbyHCP/" + hcpCode + "/" + hddSelectDate,
                        dataType: "json",
                        contentType: "application/json; charset=utf-8",
                    }).done(function (response) {
                        d1.resolve(response);
                    
                    }).fail(function (xhr, textStatus, errorThrown) {
                        alert(xhr.responseText);
                    });

                    return d1.promise();
                }
            }
        },
        fields: [
            { name: "EVENT_KEY", title: "Event Key", type: "text", width: 40 },
            { name: "REQUEST_DATE", title: "Req Date", type: "date", width: 30 },
            { name: "START_DATE", title: "Start Date", type: "date", width: 30 },
            { name: "EVENT_NAME", title: "Title", type: "text", width: 60 },
            {
                name: "SUBJECT", title: "Subject", type: "text",
                itemTemplate: function (value, item) {
                    return "<a href='#' class='link text-ellipsis' onclick='fn_DetailView(this);'>" + value + "</a>";
                }
            },
            { name: "FULL_NAME", title: "Requester", type: "text", width: 35 },           
            { name: "PROCESS_STATUS", title: "Status", type: "text", width: 50 },

        ]
    });

    

    /* Enter key */
    $("#searchText").on("keypress", function (event) {
        if (event.keyCode == 13) {            
            event.preventDefault();
            var keyword = $("#searchText").val();

            $("#jsGridHCPList").jsGrid("loadData");
        }
    });


    $("#jsGridHCPList").jsGrid("loadData");

    function fn_DetailView(obj) {
        // 해당 Row dblclick 이벤트 발생
        $(obj).dblclick();
    }

})
