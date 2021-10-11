var _nameCtrl  = new ActiveXObject('Name.nameCtrl.1');


$(document).ready(function () {

    try {
        if (window.ActiveXObject) {
            _nameCtrl = new ActiveXObject("Name.NameCtrl");

        } else {
            _nameCtrl = CreateNPApiOnWindowsPlugin("application/x-sharepoint-uc");
        }
    }
    catch (ex) { }
});

function IsNPAPIOnWinPluginInstalled(a) {
    return Boolean(navigator.mimeTypes) && navigator.mimeTypes[a] && navigator.mimeTypes[a].enabledPlugin
}

function onStatusChange(name, status, id) {
    //alert(name + ", " + status + ", " + id);
    var lyncpresencecolor = "gray";
    switch (status) {
        case 0:
            document.getElementById("pre_" + id).style.backgroundColor = "#5DD255";
            break;
        case 1:
            document.getElementById("pre_" + id).style.backgroundColor = "#B6CFD8";
            break;
        case 2:
            document.getElementById("pre_" + id).style.backgroundColor = "#FFD200";
            break;
        case 3:
            document.getElementById("pre_" + id).style.backgroundColor = "#D00E0D";
            break;
        case 4:
            document.getElementById("pre_" + id).style.backgroundColor = "#FFD200";
            break;
        case 5:
            document.getElementById("pre_" + id).style.backgroundColor = "#D00E0D";
            break;
        case 6:
            document.getElementById("pre_" + id).style.backgroundColor = "#D00E0D";
            break;
        case 7:
            document.getElementById("pre_" + id).style.backgroundColor = "#D00E0D";
            break;
        case 8:
            //document.getElementById("pre_" + id).style.borderLeft = "dashed";
            //document.getElementById("pre_" + id).style.borderLeftWidth = "10px";
            document.getElementById("pre_" + id).style.backgroundColor = "#E57A79";
            break;
        case 9:
            document.getElementById("pre_" + id).style.backgroundColor = "#D00E0D";
            break;
        case 15:
            document.getElementById("pre_" + id).style.backgroundColor = "#D00E0D";
            break;
        case 16:
            document.getElementById("pre_" + id).style.backgroundColor = "#FFD200";
            break;
        default:
            document.getElementById("pre_" + id).style.backgroundColor = "#B6CFD8";
            break;
    }

}

function ShowOOUI(sipUri, target) {
    //var zoomLevel = screen.deviceXDPI / screen.logicalXDPI;
    //var oouiX = 15, oouiY = 15;
    //oouiX = event.clientX;
    //oouiY = event.clientY;
 
    //_nameCtrl.ShowOOUI(sipUri, 0, oouiX, oouiY);
    var eLeft = $(target).offset().left;
    var x = eLeft - $(window).scrollLeft();

    var eTop = $(target).offset().top;
    var y = eTop - $(window).scrollTop();
    console.log(x.toString() + "," + y.toString());
    _nameCtrl.ShowOOUI(sipUri, 0, x, y);
}

function HideOOUI(sipUri) {
 
    _nameCtrl.HideOOUI();
}

function CreateNPApiOnWindowsPlugin(b) {
    var c = null;
    if (IsSupportedNPApiBrowserOnWin())
        try {
            c = document.getElementById(b);
            if (!Boolean(c) && IsNPAPIOnWinPluginInstalled(b)) {
                var a = document.createElement("object");
                a.id = b;
                a.type = b;
                a.width = "0";
                a.height = "0";
                a.style.setProperty("visibility", "hidden", "");
                document.body.appendChild(a);
                c = document.getElementById(b)
            }
        } catch (d) {
            c = null
        }
    return c
}