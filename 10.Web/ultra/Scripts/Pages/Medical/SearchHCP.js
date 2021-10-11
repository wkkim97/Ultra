 
function SearchHCP()
{   
    this.Init = function () {        
        try {

            $("#btnSearchHCP").on("click", fn_Search);
            $(".tab-pane-korea #lb_name").on("keydown", function (e) {
                if (e.keyCode == 13) {
                    fn_Search();
                }
            });
            $(".tab-pane-korea #lb_hco").on("keydown", function (e) {
                if (e.keyCode == 13) {
                    fn_Search();
                }
            });
            $(".tab-pane-korea #lb_special").on("keydown", function (e) {
                if (e.keyCode == 13) {
                    fn_Search();
                }
            });

            $("#btnAttendHCP").on("click", function () {
                var hcpList = [];
                if ($('#tblEP_HCPKorea tbody input:checked').length > 0) {
                    $('#tblEP_HCPKorea tbody input:checked').each(function (i, item) {
                        _contractDetail.AddHCRItems(JSON.parse($(this).val()));
                    }); 
                }
            });
        } catch (ex) {
            fn_showError({ message: ex.message });
        }

    };

    this.Reset = function () {
        try{
            $(".tab-pane-korea #lb_name").val("");
            $(".tab-pane-korea #lb_hco").val("");
            $(".tab-pane-korea #lb_special").val("");
            $('#tblEP_HCPKorea tbody').empty();
        }catch (ex) {
            fn_showError({ message: ex.message });
        } 
    }
     
    function fn_Search() {
        var hcpName = $(".tab-pane-korea #lb_name").val();
        var orgName = $(".tab-pane-korea #lb_hco").val();
        var speName = $(".tab-pane-korea #lb_special").val();

        try{

            if (hcpName.length < 1 && orgName.length < 1 && speName.length < 1) {
                fn_showWarning({
                    title: "input",
                    message: "Please enter the conditions."
                })
                return;
            }

            var search = {
                hcpName: hcpName,
                orgName: orgName,
                speName: speName
            };

            $.ajax({
                url: COMMON_SERVICE_URL + "/SelectSearchMasterDoctor",
                type: "POST",
                data: JSON.stringify(search),
                dataType: "json",
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    // version 1.0.7 HCP validation function for Easy On
                    
                    if ($("#onekey_view").length) {                    
                        displayHCPResult_onekey(data);
                    } else {
                        displayHCPResult(data);
                    }
                    
                },
                error: function (error) {
                    fn_showError({
                        message: error.responseText
                    });
                },
            });
        } catch (ex) {
            fn_showError({ message: ex.message });
        }
    }
    function displayHCPResult(list) {
        try{
            $('#tblEP_HCPKorea tbody').empty();
            var cnt = list.length;
            for (var i = 0; i < cnt; i++) {
                var hcp = list[i];
                var row = "<tr class='tr-hcp' data-hcp='" + JSON.stringify(hcp) + "'><td class='text-left'>";
                row += "<label class='item-name'>";
                row += "<span class='fix'><input type='checkbox' value='" + JSON.stringify(hcp) + "' /></span>";
                row += "<strong>" + hcp.HCPName + "</strong><br/><span style='display:inline-block;overflow:hidden;text-overflow:ellipsis;white-space:nowrap;width:280px'>" + hcp.OrganizationName + "/" + hcp.SpecialtyName + "</span></label></td>";
                row += "</tr>";
                $('#tblEP_HCPKorea tbody').append(row);
            }
        } catch (ex) {
            fn_showError({ message: ex.message });
        } 
    }

    // version 1.0.7 HCP validation function for Easy On
    function displayHCPResult_onekey(list) {
        
        try {
            $('#tblEP_HCPKorea tbody').empty();
            var cnt = list.length;
            for (var i = 0; i < cnt; i++) {
                var hcp = list[i];
                var row = "<tr class='tr-hcp' data-hcp='" + JSON.stringify(hcp) + "'>";
                row += "<td>"
                row += "<strong>" + hcp.HCPName + "</strong></td>";
                row += "<td>"
                row += "EASY"+hcp.HCPCode.replace("WKR","") + "</td>"
                row += "<td>"
                row += hcp.OrganizationName + "</td>";
                row += "<td>"
                row += hcp.SpecialtyName + "</td>";
                ;
                row += "</tr>";
                $('#tblEP_HCPKorea tbody').append(row);
            }
        } catch (ex) {
            fn_showError({ message: ex.message });
        }
    }
}