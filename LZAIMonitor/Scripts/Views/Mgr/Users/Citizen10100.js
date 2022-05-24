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

                MessageShowDialog(data, "訊息");
                $('#divEdit').dialog('close');
            }
            else {
                MessageShowDialog(data.Message, "訊息");
            }
        }

        , error: function (xhr, status, message) {
            //alert(xhr);
            if (xhr.status == "500") {
                MessageShowDialog(xhr.responseText, "訊息");
            }
        }
    });
}

function operateFormatter(value, row, index) {
    var result =
        [
            '<input type="button" value="編輯" class="btn btn-info" style ="margin-right:10px;" onclick="AjaxDialog (\'' + UrlEdit + '\',{SerId:' + row.SerId + '},\'get\') "/>',
            CheckIsDelete(row.IsDelete, row.SerId)
        ].join('');

    return result;
}

function CheckIsDelete(IsDelete, SerId) {
    var result = '';
    if (!IsDelete) {
        result = '<input type="button" value = "停用" class="btn btn-danger" onclick ="if(SweetAlertCheck(this, \'停用帳號\', \'確定停用此帳號嗎\', \'!\')){Delete(' + SerId + ')} "/>'
    }
    else {
        result = '<input type="button" value = "啟用" class="btn btn-success" onclick ="if(SweetAlertCheck(this, \'啟用帳號\', \'確定啟用此帳號嗎\', \'?\')){Enable(' + SerId + ')} "/>'
    }
    return result;
}

function Enable(Key) {
    $.ajax({
        type: "POST",
        url: UrlEnable,
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

                MessageShowDialog(data, "訊息");
                $('#divEdit').dialog('close');
            }
            else {
                MessageShowDialog(data.Message, "訊息");
            }
        }

        , error: function (xhr, status, message) {
            //alert(xhr);
            if (xhr.status == "500") {
                MessageShowDialog(xhr.responseText, "訊息");
            }
        }


    });
}

function ExcelExport() {
    var _form = $("#QueryForm");
    $.ajax({
        type: 'POST',
        url: UrlExport,
        data: _form.serialize(),
        headers: {
            'RequestVerificationToken': Token
        },
        success: function (response) {
            MessageShowDialog(response);
        },
        error: function (xhrInstance, status, xhrException) {
            alert("請再次查詢!!" + xhrInstance);
        }
    });
}