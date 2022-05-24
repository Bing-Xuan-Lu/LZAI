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

function operateFormatter(value, row, index) {
    var result =
        [
            //'<input type="button" value="編輯" class="btn btn-info" style ="margin-right:10px;" onclick="AjaxDialog (\'' + UrlEdit + '\',{SerID:' + row.GrpFuncId + '},\'get\') "/>',
            `<input type="button" value="編輯" class="btn btn-info" style ="margin-right:10px;" onclick="Sweet2editDialog('編輯','${UrlEdit}',600,550,'SerID=${row.GrpFuncId}')"/>`,
            //`<input type="button" value="編輯" class="btn btn-info" style ="margin-right:10px;" onclick="_EditdialogOption.Data ={ SerID:${row.GrpFuncId}}; EditShowDialog(_EditdialogOption)"/>`,
            '<input type="button" value = "刪除" class="btn btn-danger" onclick ="if(SweetAlertCheck(this, \'刪除\', \'資料即將進行刪除\', \'!\')){Delete(' + row.GrpFuncId + ')} "/>'
        ].join('');

    return result;
}

let _dialogOption = {
    Data: { QueryGroupId: GroupId},
    Title: "新增功能項目",
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
    Title: "編輯功能權限",
    //Buttons : Options.Buttons;
    DialogId: 'divEdit',
    Width: 1240,
    Height: 700,
    // dialogClass : Options.dialogClass;
    Url: UrlEdit,
    BeforeClose: function (a, b) { location.reload(); }
}

function back() {
    location.href = UrlGroup;
}

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
            serId: Key,
            GroupId: GroupId
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