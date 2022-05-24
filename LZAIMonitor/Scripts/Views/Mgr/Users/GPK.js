

var postTarget;
var timeoutId;

var p11Flag = 0;
var logMode = 0;
var console = window.console || { log: function () { }, debug: function () { }, error: function () { } };

//-----------------------------------------------------------------------------

function logMsg(title, msg) {
    if (logMode == 1)
        console.log(title, msg);
}

function makeSignature() {
    if (document.getElementById("slotDescription").value.replace(/^\s+$/m, '') == "") {
        alert("尚未選擇卡片，請重新選取卡片或確認跨平台網頁元件是否已啟動!!");
        return false;
    } else if ($("#tbxCertCardPass").val().replace(/^\s+$/m, '') == "") {
        alert("尚未輸入PIN碼或輸入空白，請重新輸入PIN碼!!");
        return false;
    } else {
        //alert($("#tbxCertCardPass").val());
        if (isBrowserAreIe()) //is IE, use ActiveX
        {
            var tbsPackage = getTbsPackage();
            var data = postData("http://localhost:61161/sign", "tbsPackage=" + tbsPackage);
            if (!data) alert("尚未安裝元件");
            else setSignature(data);
        }
        else {
            postTarget = window.open("http://localhost:61161/popupForm", "簽章中", "height=200, width=200, left=100, top=20");
            timeoutId = setTimeout(checkFinish, 3500);
        }
    }
}

function setSignature(signature) {
    var ret = JSON.parse(signature);
    $("#CertCardToken").val(ret.signature);
    if (ret.ret_code != 0) {
        alert(MajorErrorReason(ret.ret_code));
        if (ret.last_error)
            alert(MinorErrorReason(ret.last_error));
    } else {
        $("#tbxCertCardPass").val("");
        $("#btnLogin1").click();
    }
}

function postData(target, data) {
    if (!http.sendRequest) {
        return null;
    }
    http.url = target;
    http.actionMethod = "POST";
    var code = http.sendRequest(data);
    if (code != 0) return null;
    return http.responseText;
}

function checkFinish() {
    if (postTarget) {
        postTarget.close();
        alert("尚未安裝元件");
    }
}

function getPKCS11Info() {
    p11Flag = 0;
    logMsg("getPKCS11Info", "start");
    if (isBrowserAreIe()) {
        logMsg("getPKCS11Info", "ie");
        var data = postData("http://localhost:61161/pkcs11info?withcert=true", "");
        if (!data) alert("尚未安裝元件");
        else {
            parseReaderAndCardInfo(data);
        }
    }
    else {
        logMsg("getPKCS11Info", "window.open");
        postTarget = window.open("http://localhost:61161/popupForm", "資料讀取中", "height=200, width=200, left=100, top=20"); timeoutId = setTimeout(checkFinish, 5000);
        logMsg("getPKCS11Info", "window.open end");
    }
}

function parseReaderAndCardInfo(p11info) {

    var p11data = JSON.parse(p11info);
    logMsg("p11data", p11data);

    var selectSlot = document.getElementById('slotDescription');
    if (p11data.ret_code === 0) {
        var slots = p11data.slots;
        selectSlot.innerHTML = "";
        for (var index in slots) {
            if (slots[index].token instanceof Object) {
                var opt = document.createElement('option');
                opt.value = slots[index].slotDescription;
                opt.innerHTML = slots[index].slotDescription + " 卡號:[" + slots[index].token.serialNumber + "]";
                selectSlot.appendChild(opt);
                p11Flag = 1;
                logMsg("parseReaderAndCardInfo", "p11Flag = 1");
            }
            else {
                var opt = document.createElement('option');
                opt.value = "";
                opt.text = slots[index].slotDescription;
                opt.disabled = true;
                if (slots[index].token == "unknown token")
                    opt.innerHTML = slots[index].slotDescription + " (無法辨識的卡片)";
                else
                    opt.innerHTML = slots[index].slotDescription + " (未插卡)";
                selectSlot.appendChild(opt);
            }
        }
    } else {
        p11Flag = 0;
        logMsg("parseReaderAndCardInfo", "p11Flag = 0");
        alert(MajorErrorReason(p11data.ret_code));
        if (ret.last_error)
            alert(MinorErrorReason(ret.last_error));
        //if (p11data.last_error)
        //    alert(MinorErrorReason(p11data.last_error));
    }
}

function getTbsPackage() {

    var selectSlot = document.getElementById('slotDescription');

    var tbsData = {};
    tbsData["tbs"] = "tbs"; //?
    tbsData["tbsEncoding"] = "NONE";
    tbsData["hashAlgorithm"] = "SHA256";//SHA512?
    tbsData["withCardSN"] = "true";
    tbsData["slotDescription"] = selectSlot.options[selectSlot.selectedIndex].value;
    tbsData["pin"] = encodeURIComponent($("#tbxCertCardPass").val());
    tbsData["nonce"] = Session;
    tbsData["func"] = "MakeSignature";
    tbsData["signatureType"] = "PKCS7";

    var json = JSON.stringify(tbsData).replace(/\+/g, "%2B");
    return json;
}

function receiveMessage(event) {
    logMsg("receiveMessage", event);

    if (event.origin != "http://localhost:61161")
        return;

    try {
        var ret = JSON.parse(event.data);
        if (ret.func) {
            if (ret.func == "getTbs") {
                clearTimeout(timeoutId);
                var json;
                logMsg("receiveMessage p11Flag", p11Flag);
                if (p11Flag == 1) {
                    json = getTbsPackage();
                    logMsg("receiveMessage", "do getTbsPackage");
                }
                else {
                    var tbsData = { "func": "GetUserCert" };
                    json = JSON.stringify(tbsData);
                    logMsg("receiveMessage", "do getP11Package");
                }

                postTarget.postMessage(json, "*");
                logMsg("postMessage", json);
            }
            else if (ret.func == "pkcs11info") {
                parseReaderAndCardInfo(event.data);
            }
            else if (ret.func == "sign") {
                setSignature(event.data);
            }
        }
        else {
            console.error("no func");
        }
    }
    catch (e) {
        console.error(e);
    }
}
//------------------------------------------------------------------------------------------------------------------------------------------------------------

function isBrowserAreIe() {
    var ua = window.navigator.userAgent;
    return (ua.indexOf("MSIE") != -1 || ua.indexOf("Trident") != -1);
}
//For MVC版本增加
function btnLogin_Click() {
    $.ajax({
        type: "post",
        cache: false,
        url: URLGPK,
        data: {
            CertCardToken: $('#CertCardToken').val()
        },
        success: function (data) {
            if (data.ResultMessage.Status) {

                $('#mgrUsers_CertificationNo').val(data.gPKI.CardNumber);
                $('#mgrUsers_GPKI').val(data.gPKI.SignCertBase64);
                MessageShowDialog(data.ResultMessage.Message, "訊息");
                $('#divEdit1').dialog('close');

            }
            else {
                MessageShowDialog(data.ResultMessage.Message, "訊息");
            }
           

            
            //if (data.Status)
            //    window.location = data.Url;
        },
        error: function (xhr, error, code) {
            MessageShowDialog(error, "錯誤訊息");
        }


    });

}
