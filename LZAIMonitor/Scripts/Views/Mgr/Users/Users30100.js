$(function () {
    $('<div id="update"> </div>').click(() => {
        $("#queryTable").DataTable().draw(false);   //刷新頁面
    }).appendTo($('body'))
})

function operateFormatter(data, type, row, meta) {
    var result =
        [

            `<input type="button" value="編輯" class="btn btn-info"  onclick="Sweet2editDialog('編輯','${UrlEdit}',900,450,'UserId=${row.UserId}') "/>`
            

        ].join('');
    return result;
}
