function ajaxRequest(params) {
    var _form = $("#QueryForm");
    $.ajax({
        type: 'POST',
        url: _form.attr("action"),
        data: _form.serialize(),
        success: function (response) {
            params.success(response.data);
        },
        error: function (xhrInstance, status, xhrException) {
            alert("請再次查詢!!" + xhrInstance);
        }
    });
}

function ajaxPost(params) {
    $.ajax({
        type: 'POST',
        url: UrlGotoParam,
        data: { GroupId: params },
        dataType: 'json',
        success: function (result) {
            window.location.replace(result.redirectUrl);
        },
        error: function (xhrInstance, status, xhrException) {
            alert(xhrInstance);
        }
    });
}

let _dialogOption = {
    Data: {},
    Title: "新增群組",
    //Buttons : Options.Buttons;
    DialogId: 'divAdd',
    Width: 1240,
    Height: 900,
    // dialogClass : Options.dialogClass;
    Url: UrlInsert,
    BeforeClose: function (a, b) { location.reload(); }
}

let _EditdialogOption = {
    Data: {},
    Title: "編輯群組",
    //Buttons : Options.Buttons;
    DialogId: 'divEdit',
    Width: 1240,
    Height: 700,
    // dialogClass : Options.dialogClass;
    Url: UrlEdit,
    BeforeClose: function (a, b) { location.reload(); }
}

function operateFormatter(value, row, index) {
    var result =
        [
            //'<input type="button" value="編輯" class="btn btn-info" style ="margin-right:10px;" onclick="AjaxDialog (\'' + UrlEdit + '\',{SerID:' + row.GroupId + '},\'get\') "/>',
            //`<input type="button" value="編輯" class="btn btn-info" style ="margin-right:10px;" onclick="_EditdialogOption.Data ={ SerID:${row.GroupId}}; EditShowDialog(_EditdialogOption)"/>`,
            `<input type="button" value="編輯" class="btn btn-info" style ="margin-right:10px;" onclick="Sweet2editDialog('編輯','${UrlEdit}',600,550,'SerID=${row.GroupId}')"/>`,
            //'<input type="button" value="權限設定" class="btn btn-warning" style ="margin-right:10px;" onclick="location.href= \'' + UrlGrpFunc + '?GroupId=' + row.GroupId + '\'"/>',
            '<input type="button" value="權限設定" class="btn btn-warning" style ="margin-right:10px;" onclick="ajaxPost(' + row.GroupId  + '\)"/>',
            '<input type="button" value = "刪除" class="btn btn-danger" onclick ="if(SweetAlertCheck(this, \'刪除\', \'資料即將進行刪除\', \'!\')){Delete(' + row.GroupId + ')} "/>'
        ].join('');

    return result;
}


$(function () {
    $('<div id="tablerefresh"> </div>').click(() => {
        $('#table').bootstrapTable('refresh');
       
    }).appendTo($('body'))
})


function Keyword() {
    $('#table').bootstrapTable('refresh');
}
function Delete(Key) {
    $.ajax({
        type: "POST",
        url: UrlDelete,
        dataType: "Json",
        cache: false,
        data: {
            SerId: Key
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
                Keyword();
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