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

function Keyword() {
    $('#table').bootstrapTable('refresh');
}
$(function () {
    $('<div id="tablerefresh"> </div>').click(() => {
        $('#table').bootstrapTable('refresh');

    }).appendTo($('body'))
})

let _dialogOption = {
    Data: {},
    Title: "新增帳戶",
    //Buttons : Options.Buttons;
    DialogId: 'divAdd',
    Width: 1240,
    Height: 700,
    // dialogClass : Options.dialogClass;
    Url: UrlInsert,
    BeforeClose: function (a, b) { location.reload(); }


}

let _EditdialogOption = {
    Data: {},
    Title: "編輯人員",
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
         
            `<input type="button" value="編輯" class="btn btn-info"  onclick="Sweet2editDialog('編輯','${UrlEdit}',900,450,'UserId=${row.UserId}') "/>`,
            ` <input type="button" value = "刪除" class="btn btn-danger" onclick ="if(SweetAlertCheck(this, '刪除', '資料即將進行刪除', '!')){Delete(${row.UserId})} "/>`
          
        ].join('');
    return result;
}

function Delete(Key) {

    $.ajax({
        type: "POST",
        url: UrlDelete,
        dataType: "Json",
        cache: false,
        data: {
            UserId: Key
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
               // window.parent.$('#divAdd').dialog('close');
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
            //alert(xhr);
            if (xhr.status == "500") {
                swal.fire({
                    title: '刪除失敗!',
                    text: '500',
                    icon: 'error'
                });
            }
        }


    });




}

//修改密碼 儲存
function Alter() {
    var message = "";
    var result = true;
    var UserId = $('#UserId').val();
    var worps = document.getElementById("NewPwd");
    if (worps.value.length < 6) {
        //  MessageShowDialog("密碼必須6~8碼！！", "訊息");
        $('#NewPwd').val('');
        $('#NewPwdCheck').val('');
        message += "密碼必須6~8碼！！" + "\r\n";
        result = false;
    }

    if ($('#NewPwd').val() != $('#NewPwdCheck').val()) {

        // MessageShowDialog("密碼輸入不相同請檢查！！", "訊息");
        $('#NewPwd').val('');
        $('#NewPwdCheck').val('');
        message += "密碼輸入不相同請檢查！！" + "\r\n";
        result = false;
    }

    if (UserId == "") {
        message += "ID空白，失敗! \r\n";
        result = false;
    }
    if (result) {
        $.ajax({
            type: "POST",
            url: UrlAlterPwd,
            dataType: "Json",
            cache: false,
            data: $("#AlterForm").serialize(),
            headers: {
                'RequestVerificationToken': Token
            },
            success: function (data) {
                if (data.Status) {
                    MessageShowDialog(data, "訊息");
                    $('#divEdit').dialog('close');
                }
                else {
                    MessageShowDialog(data.Message, "訊息");
                }
            }
            , error: function (xhr, status, message) {
                if (xhr.status === "500") {
                    MessageShowDialog(xhr.responseText, "訊息");
                }
            }
        });
    }
    else {
        alert(message);
    }
}
//清除重填
function Clean() {
    $('#OldPwd').val("");
    $('#NewPwd').val("");
    $('#NewPwdCheck').val("");
}