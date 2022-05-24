//$(function () {
//    Sweet2PartialViewSubmit("InsertDocForm", "btnMgrFuncInsert");
//});

function Insert() {

    var _form = $('#InsertDocForm');
    var _validator = _form.validate();
    let iframeObj = window.parent.document.getElementById("frameid");


    if (_form.valid()) {
        $.ajax({
            type: "POST",
            url: UrlInsert,
            dataType: "Json",
            cache: false,
            headers: {
                'RequestVerificationToken': Token
            },
            data: _form.serialize(),
            success: function (data) {
                if (data.Status) {
                    swal.fire({
                        title: '新增成功!',
                      
                        icon: 'success',
                    }).then(function (result) {
                        console.log(result);

                        window.parent.swal.close();

                        window.parent.$('#table').bootstrapTable('refresh');
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
};