//程式相依於VerifyType.js
function initDialog(dialogId) {
    //document.getElementById(dialogId).style.height = document.getElementsByTagName("iframe")[0].contentWindow.document.body.scrollHeight + 50 * 1 + "px";

    if (window.parent.document.getElementById("frameid") != null) {
        //alert(window.parent.document.getElementById("frameid").height);
        $(window.parent.document.getElementById('frameid')).attr("scrolling", "no");
    }
    else if (window.parent.document.getElementsByTagName("iframe")[0].id === "iframeChild"){
        $(window.parent.document.getElementsByTagName("iframe")[0]).attr("scrolling", "no");
    }

}

function restoreClosing(dialogId) {
    if (window.parent.document.getElementById("frameid") != null) {
        $(window.parent.document.getElementById('frameid')).attr("scrolling", "yes");
    } else if (window.parent.document.getElementsByTagName("iframe")[0].id === "iframeChild") {
        $(window.parent.document.getElementsByTagName("iframe")[0]).attr("scrolling", "yes");
    }
}

//function EditShowDialog(Data, Title, Buttons, DialogId,Url) {
function EditShowDialog() {
    //var e = jQuery.Event("click");
    //e.preventDefault();

    let iframeObj = window.parent.document.getElementById("frameid");
    if (iframeObj === null) {
        iframeObj = window.parent.document.getElementsByTagName("iframe")[0];
    }
    let inputArguments = EditShowDialog.arguments;
    let Data;
    let Title;
    let Buttons;
    let DialogId = "";
    let Width = window.document.body.clientWidth * 0.95;
    let Height = iframeObj != null ? iframeObj.clientHeight - 15 : 600;
    //alert(window.parent.document.getElementById("frameid").clientHeight)
    let dialogClass;
    let Url = "";
    let BeforeClose;
    let Close;

    let Options = inputArguments[0];
    if (inputArguments.length >= 4) {

        Data = inputArguments[0];
        if (inputArguments[1] !== null && inputArguments[1] !== undefined)
            Title = inputArguments[1];

        if (inputArguments[2] !== null && inputArguments[2] !== undefined)
            Buttons = inputArguments[2];

        if (inputArguments[3] !== null && inputArguments[3] !== undefined)
            DialogId = inputArguments[3];
        if (inputArguments[4] !== null && inputArguments[4] !== undefined)
            Url = inputArguments[4];
    }
    else if (typeof (Options) === "object") {
        Data = Options.Data;
        Title = Options.Title;
        Buttons = Options.Buttons;
        DialogId = Options.DialogId;
        Width = Options.Width === "" || Options.Width == undefined ? window.document.body.clientWidth * 0.95 : Options.Width;
        Height = Options.Height === "" || Options.Height == undefined ? 900 : Options.Height;
        dialogClass = Options.dialogClass;
        Url = Options.Url;
        BeforeClose = Options.BeforeClose;
        Close = Options.Close;
    }

    if (DialogId === undefined || DialogId === "") {
        DialogId = "divCreate";
    }

    while ($('#' + DialogId).dialog('isOpen') == true) {
        DialogId += '1';
    }

    var dlg = $("#" + DialogId);

    if (dlg.length === 0) {
        dlg = $('<div id="' + DialogId + '" style="height:auto"></div>');
    }

    var $iframe = $(`<iframe id="iframeChild" style="border:0px;" width="100%" height="100%" onload="initDialog('${DialogId}')"></iframe>`);
    //var $iframe = $(`<iframe id="iframeChild" style="border:0px;overflow: hidden;" scrolling="no"  width="100%" height="100%" onload="resizeIframe('${DialogId}')"></iframe>`);
    if (Data != null && Data != "" && typeof (Data) !== "object") {
        //dlg.html(Data);
        //$iframe.ready(function () {
        //    $iframe.contents().find("body").append(Data);
        //});

    }
    else {

        $iframe.attr("src", Url + (Url.indexOf('?') > 0 ? "" : "?") + $.param(Data))
    }


    $(dlg).dialog({

        position: {
            my: "center",
            at: "center",
            of: window
        },
        title: Title,
        autoOpen: false,
        width: Width,
        height: Height,
        show: "blind",
        hide: "explode",
        modal: true,
        buttons: Buttons,
        dialogClass: dialogClass,
        beforeClose: function (event, ui) {
            if (BeforeClose !== undefined && BeforeClose !== null)
                BeforeClose(event, ui);
            restoreClosing(DialogId);//關閉時要把父層的scroll開打
            $('#' + DialogId).find("iframe").remove();
        },
        close: function (event, ui) {
            
            $(this).dialog('destroy');
            $(this).remove();
        }
        , resizeStop: function (event, ui) {
            
        }
        , open: function () {


        }

        //{
        //    //"Ok": function () { $(this).dialog("close"); },
        //    //"Cancel": function () { $(this).dialog("close"); }
        //}
    });
    if (Data != null && Data != "" && typeof (Data) !== "object") {
        dlg.html(Data);
    }
    else {
        dlg.html('');
        dlg.append($iframe);
    }
    dlg.dialog("open");
}

function MessageShowDialog(Data, Title, Buttons, Width, Height, BeforeClose) {

    if (Width === undefined || Width === null)
        Width = 400;
    if (Height === undefined || Height === null)
        Height = 350;


    if (Buttons === null || Buttons === undefined) {
        Buttons = {
            "Ok": function () {
                $(this).dialog("close");
                if (typeof response == 'object' && response.Url != undefined && response.Url != "") {
                    window.location = response.Url;
                }
            }
        };
    }
    var Result = true;
    var dlg = $("#divMessage");
    if (dlg.length == 0) {
        dlg = $('<div id="divMessage"></div>');
    }
    var response;
    if (isJson(Data)) {
        response = jQuery.parseJSON(Data);
    }
    else {
        response = Data;
    }

    $(dlg).dialog({
        title: Title,
        width: Width,
        height: Height,
        autoOpen: false,
        show: "blind",
        hide: "explode",
        modal: true,
        buttons: Buttons,
        beforeClose: function (event, ui) {
            if (BeforeClose !== undefined && BeforeClose !== null)
                BeforeClose(event, ui);
        },
        close: function (event, ui) {
            $(this).dialog('destroy');
            $(this).remove();
        }

    });

    if (typeof response == 'object') {
        //Result = response.Status;
        if (response != null && response.Message !== undefined && response.Message !== "") {
            if (response.FileDownload !== undefined && response.FileDownload !== null && response.FileDownload.length > 0) {
                for (var i = 0; i < response.FileDownload.length; i++) {
                    response.Message += "<br /><a style='color: blue; text-decoration: underline;' href='" + response.FileDownload[i].FileUrl + "' download>" + response.FileDownload[i].FileName + "</a>";
                }
            }

            dlg.html(response.Message);
            dlg.dialog("open");
        }
        else if (typeof response == 'object' && response.Url != undefined && response.Url != "") {
            window.location = response.Url;
        }
    }
    else {
        if (response === false)  // the response was a string "false", parseJSON will convert it to boolean false
        {
            Result = response.Success;
        }
        else { // the response was something else
            dlg.html(Data);
            dlg.dialog("open");
        }        
    }

}

//執行ajax後若回傳為Json(MessageModel), 會以訊息Dialog開啟
//若回傳View()或文字會視為要開啟編輯面畫開啟Dialog
//Url：ajax Url, Param：參數, Method：'post' or 'get', Headers：若需要防止CSRF, 請提供RequestVerificationToken
//Headers= {'RequestVerificationToken': '@CommonRazorFunctions.GetAntiForgeryToken()'}                    
//舊格式function AjaxDialog(Url, Param, Method, Headers) {


//執行ajax後若回傳為Json(MessageModel), 會以訊息Dialog開啟
//若回傳View()或文字會視為要開啟編輯面畫開啟Dialog
//Url：ajax Url, Param：參數, Method：'post' or 'get', Headers：若需要防止CSRF, 請提供RequestVerificationToken
//Headers= {'RequestVerificationToken': '@CommonRazorFunctions.GetAntiForgeryToken()'}                    
//function AjaxDialog(Url, Param, Method, Headers) 可以采用此界面呼叫或是傳入AjaxDialog(Options)
//    var options = {
//        "AjaxUrl": Url,
//        "AjaxParam": Param,
//        "AjaxMethod": Method,
//        "AjaxHeaders": Headers,
//        "Title": "",
//        "Buttons": null,
//        "DialogId": ""
//        "UseIframe": false
//    }
function AjaxDialog() {
    var Result = false;
    var inputArguments = AjaxDialog.arguments;
    var _Url = "";
    var _Param = {};
    var _Method = "";
    var _Headers = {};
    var _Title = "";
    var _Buttons = null;
    var _DialogId = "";
    var _Width = 1300;
    var _Height = 900;
    var _dialogClass = "";
    var _Progress = false;
    var _UseIframe = false;
    var Options = inputArguments[0];

    if (inputArguments.length >= 3) {

        _Url = inputArguments[0];
        if (inputArguments[1] !== null && inputArguments[1] !== undefined)
            _Param = inputArguments[1];

        if (inputArguments[2] !== null && inputArguments[2] !== undefined)
            _Method = inputArguments[2];

        if (inputArguments[3] !== null && inputArguments[3] !== undefined)
            _Headers = inputArguments[3];
        if (inputArguments[4] !== null && inputArguments[4] !== undefined)
            _Title = inputArguments[4];
    }
    else if (typeof (Options) === "object") {
        _Url = Options.AjaxUrl;
        _Param = Options.AjaxParam;
        _Method = Options.AjaxMethod === "" || Options.AjaxMethod == undefined ? "POST" : Options.AjaxMethod;
        _Headers = Options.AjaxHeaders;
        _Title = Options.Title;
        _Buttons = Options.Buttons;
        _DialogId = Options.DialogId;
        _Width = Options.Width === "" || Options.Width == undefined ? 1300 : Options.Width;
        _Height = Options.Height === "" || Options.Height == undefined ? 900 : Options.Height;
        _Progress = Options.Progress === "" || Options.Progress == undefined || typeof (Options.Progress) !== "boolean" ? false : Options.Progress;
        _dialogClass = Options.dialogClass;
        _UseIframe = Options.UseIframe == undefined || typeof (Options.UseIframe) !== "boolean" ? false : Options.UseIframe;

    }
    _Progress = false; //有bug暫不啟用 by Sage

    if (_UseIframe === true) {
        var dlgOptions = {

            Data: null,
            Title: _Title,
            Buttons: _Buttons,
            DialogId: _DialogId,
            Width: _Width,
            Height: _Height,
            dialogClass: _dialogClass,
            Url: _Url

        }
        EditShowDialog(dlgOptions);
        Result = true;
    }
    else {
        $.ajax({
            type: _Method,
            url: _Url,
            data: _Param,
            cache: false,
            async: false,
            headers: _Headers,
            success: function (res) {

                var response = {};
                if (isJson(res) || (typeof (res) === "object" && res.Message !== undefined)) {
                    if (isJson(res))    //若是Json字串
                        response = jQuery.parseJSON(res);//將Json字串轉成物件
                    else
                        response = res;
                    if (_Buttons === null) {
                        MessageShowDialog(response, "訊息");
                    }
                    else {
                        MessageShowDialog(response, "訊息", _Buttons);
                    }
                    if (response.Status !== undefined)
                        Result = response.Status;
                    //是否有Model驗證的誤錯
                    if (response.ModelStateErrors !== undefined
                        && response.ModelStateErrors !== null
                        & typeof (response.ModelStateErrors) === "object") {
                        validator.showErrors(data.ModelStateErrors);
                    }
                }
                else {
                    if (res === false)  // the response was a string "false", parseJSON will convert it to boolean false
                    {
                        Result = res;
                    }
                    else { // the response was something else
                        var dlgOptions = {

                            Data: res,
                            Title: _Title,
                            Buttons: _Buttons,
                            DialogId: _DialogId,
                            Width: _Width,
                            Height: _Height,
                            dialogClass: _dialogClass,
                            Url: _Url

                        }
                        //EditShowDialog(res, _Title, _Buttons, _DialogId);
                        EditShowDialog(dlgOptions);
                        Result = true;
                    }
                }


            },
            error: function (xhrInstance, status, xhrException) {
                if (_Progress === true) {
                    Progressdialog.dialog("close");
                }
                if (xhrInstance.status === 403) {
                    MessageShowDialog("您無權限使用此功能", "訊息");
                    return;
                }

                if (xhrInstance.status === 500) {
                    MessageShowDialog(xhrInstance.responseText, "訊息");
                    return;
                }
                // $.unblockUI(); 
            }

        });
    }
    return Result;
}

//Inline PartialView Ajax Post Submit
function InlinePartialViewSubmit(formId, buttonId, divId) {

    if (divId === 'undefind' || divId === '')
        divId = 'divEdit';

    $("#" + buttonId).click(function (e) {
        e.preventDefault();
        let _this = $(this);
        let _form = $("#" + formId);
        let _validator = _form.validate();

        if (_form.valid()) {
            $.ajax({
                type: "POST",
                url: _form.attr("action"),
                dataType: "Json",
                cache: false,
                data: _form.serialize(),
                success: function (data) {
                    if (data.Status) {
                        MessageShowDialog(data, "訊息", null, 400, 300, function () { window.parent.$('#' + divId).dialog('close') });
                    }
                    else {
                        if (data.ModelStateErrors !== null) {
                            _validator.showErrors(data.ModelStateErrors);
                        }
                        MessageShowDialog(data, "訊息");
                    }
                }
                , error: function (xhr, status, message) {
                    if (xhr.status === 403) {
                        MessageShowDialog("您無權限使用此功能", "訊息");
                    }
                    else if (xhr.status === 500) {
                        MessageShowDialog(xhr.responseText, "訊息");
                    }
                    else {
                        MessageShowDialog(xhr.responseText, "訊息");
                    }
                }
            });
        }
        else {
            _validator.showErrors();
        }
    });
}


function AjaxSubmit(formId) {

    //$("#" + buttonId).click(function (e) {
    //e.preventDefault();

    var _form = $("#" + formId);
    var _validator = _form.validate();

    //if (_form.valid()) {
    $.ajax({
        type: "POST",
        url: _form.attr("action"),
        dataType: "Json",
        cache: false,
        data: _form.serialize(),
        success: function (data) {
            if (data.Status) {
                MessageShowDialog(data, "訊息");
                $('#divEdit').dialog('close');
                // $('#table').bootstrapTable('refresh');
            }
            else {
                if (data.ModelStateErrors !== null) {
                    _validator.showErrors(data.ModelStateErrors);
                }
                MessageShowDialog(data, "訊息");
            }
        }
        , error: function (xhr, status, message) {
            if (xhr.status === 403) {
                MessageShowDialog("您無權限使用此功能", "訊息");
            }
            if (xhr.status === 500) {
                MessageShowDialog(xhr.responseText, "訊息");
            }
        }
    });
    //}
    //else {
    //    _validator.showErrors();
    //}

}

// 將Modal(或dialog)下的所有input清空並解鎖
function ResetModal(id) {
    var inputList = $(id).find('input[type="text"]');
    inputList.each(function (key, item) {
        //console.log(item);
        $(this).val("").attr('disabled', false);
    });
}

function bindDDLFromAjax(url, params, ddlId) {
    $.ajax({
        type: "get",
        url: url,
        data: params,
        cache: false,
        async: false,
        success: function (json) {
            var ddlObj = $("#" + ddlId);
            if (json !== undefined) {
                ddlObj.empty();
            }
            for (var ind in json) {
                ddlObj.append($("<option value='" + json[ind].Value + "'>" + json[ind].Text + "</option>"));
            }

        }
    });
}
