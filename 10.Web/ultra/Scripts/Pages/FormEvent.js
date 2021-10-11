$(function () {
   
});

function fn_WindowOnLoad() {
    if ($('#hddInformation').val().length > 0)
        fn_OpenInformation($('#hddInformation').val());

    $('.btn-panel-open').on('click', function () {
        $(this).closest('.tab-cont-area').removeClass('closed');
    });

    $('.btn-panel-close').on('click', function () {
        $(this).closest('.tab-cont-area').addClass('closed');
    });
    loadForm();
}

function fn_OpenInformation(message) {
    $('#layer_success .alert-message p').text(message);
    $('#layer_success').modal('show');
    $('#hddInformation').val("");
}

function fn_AddTabPage(url, eventID, title, processID) {
    window.parent.fn_AddTabPage(url, eventID, title, processID);
}

// 입력폼 여닫기
//$('.btn-panel-open').click(function () {
//    $(this).closest('.tab-cont-area').removeClass('closed');
//});
//$('.btn-panel-close').click(function () {
//    $(this).closest('.tab-cont-area').addClass('closed');
//});

function fn_showInformation(options) {
    var d = $.Deferred();
    var defaults = {
        title: "success",
        message: "Successfully processed."
    }
    $.extend(defaults, options);
    var _show = function () {
        var callbackParam = "";
        $("#layer_success .alert-message strong").text(defaults.title);
        $("#layer_success .alert-message .span-message").html(defaults.message);
        $("#layer_success").css('height', $(window).height() * 0.85)
            .modal({
                backdrop: true,
                keyboard: true,
                show: true,
            }).on('hidden.bs.modal', function (e) {
                d.resolve(callbackParam)
            }).modal('show');
    }
    _show();
    return d.promise();
}

function fn_showError(options) {
    var d = $.Deferred();
    var defaults = {
        title: "error",
        message: "An exception occurred. Please contact the administrator."
    }
    $.extend(defaults, options);
    var _show = function () {
        var callbackParam = "";
        $("#layer_error .alert-message strong").text(defaults.title);
        $("#layer_error .alert-message .span-message").text(defaults.message);
        $("#layer_error").css('height', $(window).height() * 0.85)
            .modal({
                backdrop: true,
                keyboard: true,
                show: true,
            }).on('hidden.bs.modal', function (e) {
                d.resolve(callbackParam)
            }).modal('show');
    }
    _show();
    return d.promise();
}


function fn_showWarning(options) {
    var d = $.Deferred();
    var defaults = {
        title: "Caution",
        message: "Please confrim..."
    }
    $.extend(defaults, options);
    var _show = function () {
        var callbackParam = "";
        $("#layer_warning .alert-message strong").text(defaults.title);
        $("#layer_warning .alert-message .span-message").html(defaults.message);
        $("#layer_warning").css('height', $(window).height() * 0.85)
            .modal({
                backdrop: true,
                keyboard: true,
                show: true,
            }).on('hidden.bs.modal', function (e) {
                d.resolve(callbackParam)
            }).modal('show');
    }
    _show();
    return d.promise();
}

function fn_confirm(options) {
    var d = $.Deferred();
    var defaults = {
        title: "Confirm",
        message: "Are you sure you want to delete?"
    }
    $.extend(defaults, options);
    var _show = function () {
        var callbackParam = false;
        $("#layer_alert .alert-message strong").text(defaults.title);
        $("#layer_alert .alert-message .span-message").text(defaults.message);
        $("#layer_alert .btn-sm").on('click', function (e) {
            if (e.target.id == 'btnCancel') {
                callbackParam = false;
                $('#layer_alert').modal('hide');
            } else if (e.target.id == 'btnConfirm') {
                callbackParam = true;
                $('#layer_alert').modal('hide');
            }
        });
        $("#layer_alert").css('height', $(window).height() * 0.85)
            .modal({
                backdrop: true,
                keyboard: true,
                show: true,
            }).on('hidden.bs.modal', function (e) {
                d.resolve(callbackParam)
            }).modal('show');
    }
    _show();
    return d.promise();
}

// version 1.0.5 Admin Event Page for change by DM Team
function fn_change_value(options) {
    
    var d = $.Deferred();
    var defaults = {
        title: "change value",
        message: "변경?"
    }
    $.extend(defaults, options);
    var _show = function () {
        var callbackParam = false;
        $("#layer_change_value .alert-message strong").text(defaults.title);
        $("#layer_change_value .alert-message .span-message").text(defaults.message);
        $("#layer_change_value .btn-sm").on('click', function (e) {
            if (e.target.id == 'btnCancel') {
                callbackParam = false;
                $('#layer_change_value').modal('hide');
            } else if (e.target.id == 'btnConfirm') {
                callbackParam = true;
                $('#layer_change_value').modal('hide');
            }
        });
        $("#layer_change_value").css('height', $(window).height() * 0.85)
            .modal({
                backdrop: true,
                keyboard: true,
                show: true,
            }).on('hidden.bs.modal', function (e) {
                d.resolve(callbackParam)
            }).modal('show');
    }
    _show();
    return d.promise();
}

