$(function () {


    //$('#Query').click(function () {

    //    $('#QueryForm').submit();

    //});





});
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

let _dialogOption = {
    Data: {},
    Title: "新增單位",
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
    Title: "編輯單位",
    //Buttons : Options.Buttons;
    DialogId: 'divEdit',
    Width: 1240,
    Height: 700,
    // dialogClass : Options.dialogClass;
    Url: UrlEdit,
    BeforeClose: function (a, b) { location.reload(); }


}

function operateFormatter(value, row, index) {
   // alert(row);
    //col.Field(x => string.Format("<input type='button' value='編輯' onclick=\"AjaxDialog ('{0}',{{OrgUnitId:{1}}}, 'get')\" /><input type = 'button' value = '刪除' onclick =\"if(confirm('確定刪除嗎？')){{Delete({1})}} \" />", "/Mgr/OrgUnit/OrgUnit201E0", x.OrgUnitId)).AddProperty("data-width", "20%");
    return [
        //'<input type="button" value="編輯" class="btn btn-info" style ="margin-right:10px;" onclick="AjaxDialog (\'' + UrlEdit + '\',{OrgUnitId:' + row.OrgUnitId + '},\'get\') "/>',
        `<input type="button" value="編輯" class="btn btn-info" style ="margin-right:10px;" onclick="Sweet2editDialog('編輯','${UrlEdit}',900,200,'OrgUnitId=${row.OrgUnitId}') "/>`,
        //`<input type="button" value="編輯" class="btn btn-warning m-1"  onclick="_EditdialogOption.Data ={ OrgUnitId:${row.OrgUnitId}}; EditShowDialog(_EditdialogOption) "/>`,
        '<input type="button" value = "刪除" class="btn btn-danger" onclick ="if(SweetAlertCheck(this, \'刪除\', \'資料即將進行刪除\', \'!\')){Delete(' + row.OrgUnitId + ')} "/>'
    ].join('');
}
$(function () {
    $('<div id="tablerefresh"> </div>').click(() => {
        $('#table').bootstrapTable('refresh');

    }).appendTo($('body'))
})

function Delete(Key) {

    $.ajax({
        type: "POST",
        url: UrlDelete,
        dataType: "Json",
        cache: false,
        data: {
            OrgUnitId: Key
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