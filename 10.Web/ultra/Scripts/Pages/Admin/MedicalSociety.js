$(function () {

    GridData(function (output) {
        loadGrid(output);
    });
    

    $("#btnNew").click(function () {
        clearNewSociety();
    });

    function clearNewSociety() {
        $("#hhdSocietyID").val("");
        $("#txtSocietyName").val("");
        $("#selSocietysStatus").val("Y");
    }

    $("#btnSave").click(function () {
        var id = ($("#hhdSocietyID").val() == "" ? "0" : $("#hhdSocietyID").val());
        var name = $("#txtSocietyName").val();
        var status = $("#selSocietysStatus").val();
        var userID = $("input[id$=hddUserID]").val();

        //저장
        var Society = {
            SOCIETY_IDX: id,
            SOCIETY_NAME: name,
            STATUS: status,
            CREATOR_ID: userID,
            UPDATER_ID: userID
        }

        var proSociety = {
            society: Society,
        }


        $.ajax({
            url: COMMON_SERVICE_URL + "/MergeMedicalSociety",
            type: "POST",
            data: JSON.stringify(proSociety),
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                if (id == "0")
                    alert("저장되었습니다.");
                else
                    alert("수정되었습니다.");
                clearNewSociety();

                GridData(function (output) {
                    loadGrid(output);
                });
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });

    });


    $("#btnDelete").click(function () {
        var id = $("#hhdSocietyID").val();

        if (id == "") return;

        $.ajax({
            url: COMMON_SERVICE_URL + "/DeleteMedicalSociety/" + id,
            type: "GET",
            //dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                alert("삭제되었습니다.");
                clearNewSociety();

                $("#jsGridMedicalSociety").jsGrid("loadData");
            },
            error: function (error) {
                fn_showError({
                    message: error.responseText
                });
            },
        });

    });

    //$("#jsGridMedicalSociety").jsGrid("loadData");
})

function loadGrid(item) {
    $("#jsGridMedicalSociety").jsGrid({
        width: "100%",
        height: "500px",
        filtering: true,
        sorting: true,
        paging: false,
        autoload: true,
        controller: {
            data: item,
            loadData: function (filter) {
                return $.grep(this.data, function (item) {
                    return (!filter.SOCIETY_NAME || item.SOCIETY_NAME.toLowerCase().indexOf(filter.SOCIETY_NAME.toLowerCase()) > -1)
                    && (!filter.STATUS || item.STATUS.toLowerCase().indexOf(filter.STATUS.toLowerCase()) > -1);
                });
            }
        },
        rowClick: function (args) {
            var item = args.item;
            $('#hhdSocietyID').val(item.SOCIETY_IDX);
            $('#txtSocietyName').val(item.SOCIETY_NAME);
            $('#selSocietysStatus').val(item.STATUS);
            var row = $(args.event.target).closest("tr");

            if (this._clicked_row != null) {
                this._clicked_row.removeClass('jsgrid-clicked-row');
            }
            this._clicked_row = row;

            row.addClass('jsgrid-clicked-row');
        },
        fields: [
            { name: "SOCIETY_IDX", title: "Society ID", width: 20 },
            { name: "SOCIETY_NAME", title: "Society Name", type: "text", width: 70 },
            { name: "STATUS", title: "Status", type: "text", width: 10 },
        ]
    });
}

function GridData(callback) {
    var selectUrl = COMMON_SERVICE_URL + "/SelectMedicalSocietyList/0";
    $.ajax({
        url: selectUrl,
        type: "GET",
        dataType: "json",
        asyn: false,
        contentType: "application/json; charset=utf-8",
        success: function (returnData) {
            callback(returnData);
        }
    });
}