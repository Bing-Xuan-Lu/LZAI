//$(function () {
//    InlinePartialViewSubmit("DocForm", "btnMgrGroupUpdate","divEdit");
//});

$(function () {
    Sweet2PartialViewSubmit("UpdateDocForm", "btnMgrGroupUpdate");
});


function Update() {
    var message = "";
    var checkedValue = $('#tree').treeview('getChecked');//.get().join(',');
    var insValue = '';
    for (var i = 0; i <= checkedValue.length - 1; i++) {
        insValue += checkedValue[i].nodeid + ',';
    }
    $("#checkValue").val(insValue);
    $.ajax({
        type: "POST",
        url: UrlUpdate,
        dataType: "Json",
        cache: false,
        data: $('#UpdateDocForm').serialize(),
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