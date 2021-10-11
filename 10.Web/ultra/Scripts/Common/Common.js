/* Number 포맷 설정 */
$('.number').on('keypress', function (event) {
    if ((event.which != 46 || $(this).val().indexOf('.') != -1) && (event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }
});

$('.number').on('keyup', function (e) {
    var value = $(this).val().replace(/[^0-9\.]/g, '');
    //if (value == "") value = "0";
    //$(this).val(parseInt(value)); //제일앞에 0이 들어가는 것을 방지하기 위해
    if ((e.keyCode >= 48 && e.keyCode <= 57) || (e.keyCode >= 96 && e.keyCode <= 105) || e.keyCode == 13 || e.keyCode == 38 || e.keyCode == 40 || e.keyCode == 8 || e.keyCode == 46) {
        //$(this).val(x.toString().replace(/,/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ","));
        //$(this).val(x.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,"));
        if(value != "")
        $(this).val(fn_AddComma(parseInt(value).toString()));
    }
});

$('.number').on('focusout', function (e) {
    var value = $(this).val().replace(/[^0-9\.]/g, '');
    if (value == "") $(this).val("0");
    else {
        $(this).val(fn_AddComma(fn_RemoveComma(value)));
    }
});

function fn_RemoveComma(value) {
    return value.replace(/,/g, '')
}


function fn_AddComma(value) {
   // alert("33");
    //return value.toString().replace(/,/g, "").replace(/\B(?=(\d{3})+(?!\d))/g, ",");
   // return fn_RemoveComma(value).toString().replace(/\B(?=(\d{3})+(?!\d))/g, ",");
    return value.toString().replace(/(\d)(?=(\d\d\d)+(?!\d))/g, "$1,");
}

function fn_urlParam(name) {
    var results = new RegExp('[\?&]' + name + '=([^&#]*)').exec(window.location.href);
    if (results == null) {
        return null;
    }
    else {
        return results[1] || 0;
    }
}

function fn_formatJSONDate(jsonDate) {
    var MyDate_String_Value = jsonDate
    var value = new Date
                (
                     parseInt(MyDate_String_Value.replace(/(^.*\()|([+-].*$)/g, ''))
                );
    var dat = value.getFullYear() + "-" + (value.getMonth() + 1) +
                           "-" + value.getDate(); //+ " " + value.getHours() + ":" + value.getMinutes();

    return dat;
}

function fn_ConvertJsonData(data) {
    var return_data = JSON.stringify(data).replace(/\\n/g, "\\n")
        .replace(/'/g, "")
        //.replace(/\\"/g, '\\"')
        //.replace(/\\&/g, "\\&")
        //.replace(/\\r/g, "\\r")
        //.replace(/\\t/g, "\\t")
        //.replace(/\\b/g, "\\b")
        //.replace(/\\f/g, "\\f");
    return return_data
}

function fn_VeeVaCRMuser(s_date) {
    var check_flag = true;
    //CRM 에서 넘오온 자료는 무조건 OK
    if ($("#hddCRMStatus").val().indexOf("Y:") >= 0) return false;
    var event_date = new Date(s_date.substring(0,10));
    var due_date_from = new Date('2019-03-31');
    var due_date_to = new Date('2019-03-11');
   
    if (event_date.getTime() <= due_date_from.getTime() && event_date.getTime() >= due_date_to.getTime()) {
        //기간내 자료
        check_flag = false;
    }
    return check_flag;
}

function callAjax(sPage, sParam, bAsync, sType, fncCallback) {
    var strReturnValue;
    var d = $.Deferred();
    try{
        $.ajax({
            type: sType,
            url: sPage,
            data: sParam,
            async: bAsync,
            dataType: "json",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                d.resolve(data);
                if (typeof fncCallback == "function")
                {
                    fncCallback(data)
                }
                else {
                    strReturnValue = data;
                }
            },
            error: function (xhr, textStatus, errorThrown) {
                d.reject(xhr);
                fn_showError({ title: errorThrown, message: xhr.responseText });
            }
        });
        d.promise();
    }
    catch(error)
    {
        fn_showError({
            message: error.responseText
        });
    }
    
    return strReturnValue;
}