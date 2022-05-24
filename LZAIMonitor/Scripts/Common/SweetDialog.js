
var obj = { status: false, ele: null };
function SweetAlertCheck(btn, t, s, icon) {
    var info = '';
    switch (icon) {//sweetalert2 符號
        case "V":
            info = "success";//打勾
            break;
        case "X":
            info = "error";//錯誤
            break;
        case "!":
            info = "warning";//驚嘆號
            break;
        case "I":
            info = "info";//i
            break;
        default:
            info = "question";//問號
            break;
    }
    if (obj.status) {
        obj.status = false;
        return true;
    };

    Swal.fire({
        title: t,
        text: s,
        icon: info,
        showCancelButton: true,
        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: '確認',
        cancelButtonText: "取消"
    }).then((result) => {
        if (result.value) {

            obj.status = true;
            //do postback on success
            obj.ele.click();


        }
    });
    obj.ele = btn;
    return false;
}



function Sweet2addDialog(title, urlAction, Ifwidth, Ifheight, Data = null) {
    var src;
    if (Data != null && Data != "") {
        src = urlAction + "?" + Data;
    } else { src = urlAction }
    swal.fire({
        title: title,
        //icon:"question",
        width: Ifwidth,
        height: Ifheight,
        allowOutsideClick: false,//外部點擊
        allowEscapeKey: false,//esc

        //html: "<iframe  width=" + Ifwidth + "  height= " + Ifheight + "   frameborder=no border=0 marginwidth=0 marginheight=0  src=" + urlAction + "></iframe>",
        html: "<iframe  width=100% height=" + Ifheight + "   frameborder=no border=0 marginwidth=0 marginheight=0  src=" + src + "></iframe>",
        showCancelButton: false,//顯示取消按鈕
        showConfirmButton: false,//不顯示確認按鈕
        cancelButtonText: "取消", //顯示cancel中文名稱
        confirmButtonText: "確認",//顯示confirm中文名稱
        focusCancel: true,  //焦點
        //showCloseButton: true,//顯示右上角x
        imageUrl: '../../images/add.png',
        imageHeight: 64


    })
}
function Sweet2editDialog(title, urlAction, Ifwidth, Ifheight, data = null, otherOption = {}) {
    var src = urlAction + "?" + data;
    swal.fire({
        title: title,
        //icon:"question",
        width: Ifwidth * 1.1,
        height: Ifheight,
        allowOutsideClick: otherOption.allowOutsideClick || false,//外部點擊
        allowEscapeKey: otherOption.allowEscapeKey || false,//esc
        html: "<iframe width=100%  height= " + Ifheight + "   frameborder=no border=0 marginwidth=0 marginheight=0  src=" + src + "></iframe>",

        showCancelButton: otherOption.showCancelButton || false,//顯示取消按鈕
        showConfirmButton: otherOption.showConfirmButton || false,//不顯示確認按鈕
        cancelButtonText: otherOption.cancelButtonText || "取消", //顯示cancel中文名稱
        confirmButtonText: otherOption.confirmButtonText || "確認",//顯示confirm中文名稱
        focusCancel: true,  //焦點
        imageUrl: otherOption.imageUrl || '../../images/edit.png',
        imageHeight: otherOption.imageHeight || 64
    })
}



function sweet2load() {
    const Toast = Swal.mixin({
        icon: 'success',
        toast: true,
        position: 'top-end',
        showConfirmButton: false,
        timer: 3000,
        timerProgressBar: true,
        onBeforeOpen: () => {
            Swal.showLoading();
        }
    })

    Toast.fire({
        title: 'loading...'
    })

}

function SweetShowDialog(title, text, icon) {
    var info = '';
    switch (icon) {//sweetalert2 符號
        case "V":
            info = "success";//打勾
            break;
        case "X":
            info = "error";//錯誤
            break;
        case "!":
            info = "warning";//驚嘆號
            break;
        case "I":
            info = "info";//i
            break;
        default:
            info = "question";//問號
            break;
    }
    Swal.fire({
        title: title,
        text: text,
        icon: info,

        confirmButtonColor: '#3085d6',
        cancelButtonColor: '#d33',
        confirmButtonText: '確認',

    })
}

function Sweet2PartialViewSubmit(formId, buttonId) {
    $("#" + buttonId).click(function (e) {
        e.preventDefault();
        var t = '新增';
        var _this = $(this);
        var _form = $("#" + formId);
        var _validator = _form.validate();
        let iframeObj = window.parent.document.getElementById("frameid");
        if (formId === 'UpdateDocForm') { var t = '編輯' }
        if (_form.valid()) {
            $.ajax({
                type: "POST",
                url: _form.attr("action"),
                dataType: "Json",
                cache: false,
                headers: {
                    'RequestVerificationToken': Token
                },
                data: _form.serialize(),
                success: function (data) {
                    if (data.Status) {
                        swal.fire({
                            title: t + '成功!',
                            text: '已成功' + t,
                            icon: 'success',
                        }).then(function (result) {
                            console.log(result);

                            window.parent.swal.close();

                            window.parent.$('#tablerefresh').trigger('click')
                        });

                    }
                    else {
                        if (data.ModelStateErrors !== null) {
                            _validator.showErrors(data.ModelStateErrors);
                        }

                        swal.fire({
                            title: t + '失敗!',
                            html: data.Message,
                            icon: 'error'
                        });
                    }

                }
                , error: function (xhr, status, message) {
                    if (xhr.status === 403) {

                        Swal.fire({
                            title: t + '失敗!',
                            //html: xhr.responseText,
                            text: '您無權限使用此功能',
                            icon: 'error'
                        });
                    }
                    if (xhr.status === 500) {

                        Swal.fire({
                            title: t + '失敗!',
                            //html: xhr.responseText,
                            text: '錯誤:500',
                            icon: 'error'
                        });
                    }
                }
            });
        }
    });




}



function Sweet2PartialdataTableSubmit(formId, buttonId) {
    $("#" + buttonId).click(function (e) {
        e.preventDefault();
        var t = '新增';
        var _this = $(this);
        var _form = $("#" + formId);
        var _validator = _form.validate();
        let iframeObj = window.parent.document.getElementById("frameid");
        if (formId === 'UpdateDocForm') { var t = '編輯' }
        if (_form.valid()) {
            $.ajax({
                type: "POST",
                url: _form.attr("action"),
                dataType: "Json",
                cache: false,
                headers: {
                    'RequestVerificationToken': Token
                },
                data: _form.serialize(),
                success: function (data) {
                    if (data.Status) {
                        swal.fire({
                            title: t + '成功!',
                            text: '已成功' + t,
                            icon: 'success',
                        }).then(function (result) {
                            console.log(result);

                            window.parent.swal.close();
                            // window.parent.tableDraw(window.parent.parentQueryTable, 'QueryForm', window.parent._queryTableConfig);
                            window.parent.$('#update').trigger('click')
                        });

                    }
                    else {
                        if (data.ModelStateErrors !== null) {
                            _validator.showErrors(data.ModelStateErrors);
                        }

                        swal.fire({
                            title: t + '失敗!',
                            html: data.Message,
                            icon: 'error'
                        });
                    }

                }
                , error: function (xhr, status, message) {
                    if (xhr.status === 403) {

                        Swal.fire({
                            title: t + '失敗!',
                            //html: xhr.responseText,
                            text: '您無權限使用此功能',
                            icon: 'error'
                        });
                    }
                    if (xhr.status === 500) {

                        Swal.fire({
                            title: t + '失敗!',
                            //html: xhr.responseText,
                            text: '錯誤:500',
                            icon: 'error'
                        });
                    }
                }
            });
        }
    });




}

