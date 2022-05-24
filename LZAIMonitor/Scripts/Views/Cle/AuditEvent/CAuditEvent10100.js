$(document).ready(function () {

    $("#Name").prepend(new Option('全部', -1, true));
    $("#Name").val(-1).change();
    $("#Name").select2({
        allowClear: true,
        minimumInputLength: 0,
        width: 250,
        placeholder: "請選擇使用者姓名"
    });


    $("#SDate").datepicker({ dateFormat: 'yy-mm-dd' });
    $("#EDate").datepicker({ dateFormat: 'yy-mm-dd' });

})

$(function () {
    $('<div id="update"> </div>').click(() => {
        $("#queryTable").DataTable().draw(false);
        // tableDraw(queryTable, 'QueryForm', _queryTableConfig);
    }).appendTo($('body'))
})

function dateTypeToyyyyMMdd(data, type, row, meta) {

    if (data != null) {
        var date = new Date(parseInt(data.substr(6)));
        var dateStr = date.getFullYear() + "/" + ("00" + (date.getMonth() + 1)).slice(-2) + "/" + ("00" + date.getDate()).slice(-2) + " " + ("00" + date.getHours()).slice(-2) + ":" + ("00" + date.getMinutes()).slice(-2) + ":" + ("00" + date.getSeconds()).slice(-2);
        return dateStr;


    } else {
        return "";
    }
}



function Download() {
    var _form = $("#QueryForm");
    $.ajax({
        type: "POST",
        url: UrlDownload,
        cache: false,
        data: _form.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            if (data.Status) {
                var UrlName = data.FileDownload[0].FileName;
                Swal.fire({
                    title: '檔案下載?',
                    text: '是否下載' + UrlName,
                    icon: 'question',
                    showCancelButton: true,//顯示取消按鈕
                    showConfirmButton: true,//不顯示確認按鈕
                    cancelButtonText: "取消", //顯示cancel中文名稱
                    confirmButtonText: "確認",//顯示confirm中文名稱
                }).then((result) => {

                    if (result.isConfirmed) {
                        window.location.href = data.FileDownload[0].FileUrl;
                    }

                })
            } else {
               
                swal.fire({
                    title: '下載失敗!',
                    text: data.Message,
                    icon: 'error'
                });

            }


        },
        error: function (xhrInstance, status, xhrException) {
            swal.fire({
                title: '請再次查詢!',
                text: xhrInstance,
                icon: 'error'
            });

        }
    });
}