//function ajaxRequest(params) {
//    var _form = $("#QueryForm");
//    $.ajax({
//        type: 'POST',
//        url: _form.attr("action"),
//        data: _form.serialize(),
//        success: function (response) {
//            params.success(response.data);
//        },
//        error: function (xhrInstance, status, xhrException) {
//            alert("請再次查詢!!" + xhrInstance);
//        }
//    });
//}
$(document).ready(function () {

    $("#SelectCleNumber").prepend(new Option('全部', -1, true));
    $("#SelectCleNumber").val(-1).change();
    $("#SelectCleNumber").select2({
        allowClear: true,
        minimumInputLength: 0,
        width: 250,
        placeholder: "請選擇編號"
    });
    $("#Fac_Id").prepend(new Option('全部', -1, true));
    $("#Fac_Id").val(-1).change();
    $("#Fac_Id").select2({
        allowClear: true,
        minimumInputLength: 0,
        width: 250,
        placeholder: "請選擇管制編號"
    });
    $("#Fac_Name").prepend(new Option('全部', -1, true));
    $("#Fac_Name").val(-1).change();
    $("#Fac_Name").select2({
        allowClear: true,
        minimumInputLength: 0,
        width: 250,
        placeholder: "請選擇事業單位"
    });
})

function operateFormatter(data, type, row, meta) {
    var result =
        [
            `<input type="button" value="編輯" class="btn btn-info" style ="margin-right:10px;" onclick="Sweet2editDialog('編輯','${UrlEdit}',1000,600,'CleId=${row.CleId}') "/>`,
            `<input type="button" value = "刪除" class="btn btn-danger" onclick ="if(SweetAlertCheck(this, '刪除', '資料即將進行刪除', '!')){Delete(${row.CleId})} "/>`

        ].join('');

    return result;
}

function Gate_Long_Gate_Lat(data, type, row, meta) {
    var result =
        [
            `<a  title="${row.Cle_Name}Google地圖(另開視窗)"  target="_blank" href="https://www.google.com/maps?q=loc:${row.Gate_Lat},${row.Gate_Long}&amp;z=15">${row.Gate_Lat},${row.Gate_Long}</a>`
        ].join('');

    return result;
}

function CarFormatter(data, type, row, meta) {



    var data = row.CarSelectGrantInfoCH.replace(/,/g, "&#13;&#10;");
   

    var result =
        [
            `<div class="showbox" text="${data}" overflow: scroll;><img src="/images/Car/Y/Not/car_l.png"></div>`
        ].join('');
    return result;

}


//不寫SweetDialog的iframe抓不到;刷新table用
//var parentQueryTable;
//$(function () {
//    parentQueryTable = queryTable; 

//})

$(function () {
    $('<div id="update"> </div>').click(() => {
        $("#queryTable").DataTable().draw(false);
        // tableDraw(queryTable, 'QueryForm', _queryTableConfig);
    }).appendTo($('body'))
})


function Delete(Key) {
    $.ajax({
        type: "POST",
        url: UrlDelete,
        dataType: "Json",
        cache: false,
        data: {
            CleId: Key
        },
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (data) {
            if (data.Status) {
                swal.fire({
                    title: '刪除成功!',
                    text: '已成功刪除',
                    icon: 'success'
                }).then(function (result) {
                    console.log(result);
                    window.parent.swal.close();
                });
                tableDraw(queryTable, 'QueryForm', _queryTableConfig);

            }
            else {
                swal.fire({
                    title: '刪除失敗!',
                    text: data.Message,
                    icon: 'error'
                });

            }
        }
        , error: function (xhr, status, message) {
            if (xhr.status === "500") {
                swal.fire({
                    title: '刪除失敗!',
                    text: '500',
                    icon: 'error'
                });

            }
        }
    });


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