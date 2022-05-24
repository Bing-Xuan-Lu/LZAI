$(function () {
    Sweet2PartialViewSubmit("InsertDocForm", "btnMgrGrpFuncInsert");
    $('#FuncId').change(function () { ChangeFunc(); });
});

//功能項目動態產生其他權限
function ChangeFunc() {
    var selectedValue = $('#FuncId').val();
    if ($.trim(selectedValue).length > 0) {
        GetFuncParam(selectedValue);
    }
    else {
        $('#FuncParamList').empty();
    }

}

function GetFuncParam(value) {
    $.ajax({
        url: 'GetFuncParam',
        data: { FuncId: value },
        type: 'post',
        cache: false,
        async: false,
        dataType: 'json',
        success: function (data) {
            if (!jQuery.isEmptyObject(data)) {
                $('#FuncParamList').empty();
                //$.each(data, function (x, item) { }); 日後有多的權限請用each
                if (data['IsOthPerm1'] == true) {
                    $('#FuncParamList').append(
                        $('<label />').html(data['OthPerm1Name'])
                            .prepend($('<input/>').attr({ type: 'checkbox', id: 'IsOthPerm1', name: 'IsOthPerm1',value:true }))
                            .insertAfter($('<input/>').attr({ type: 'hidden', value: 'false', name: 'IsOthPerm1' }))
                    )
                    if ($('#_IsOthPerm1').val() == 'True') {
                        $('#IsOthPerm1').attr('checked', 'checked');
                    }
                }
                $('#FuncParamList').append('&nbsp;');
                if (data['IsOthPerm2'] == true) {
                    $('#FuncParamList').append(
                        $('<label />').html(data['OthPerm2Name'])
                            .prepend($('<input/>').attr({ type: 'checkbox', id: 'IsOthPerm2', name: 'IsOthPerm2', value: true }))
                            .insertAfter($('<input/>').attr({ type: 'hidden', value: 'false', name: 'IsOthPerm2' }))
                    )
                    if ($('#_IsOthPerm2').val() == 'True') {
                        $('#IsOthPerm2').attr('checked', 'checked');
                    }
                }
                $('#FuncParamList').append('&nbsp;');
                if (data['IsOthPerm3'] == true) {
                    $('#FuncParamList').append(
                        $('<label />').html(data['OthPerm3Name'])
                            .prepend($('<input/>').attr({ type: 'checkbox', id: 'IsOthPerm3', name: 'IsOthPerm3', value: true }))
                            .insertAfter($('<input/>').attr({ type: 'hidden', value: 'false', name: 'IsOthPerm3' }))
                    )
                    if ($('#_IsOthPerm3').val() == 'True') {
                        $('#IsOthPerm3').attr('checked', 'checked');
                    }
                }
                $('#FuncParamList').append('&nbsp;');
                if (data['IsOthPerm4'] == true) {
                    $('#FuncParamList').append(
                        $('<label />').html(data['OthPerm4Name'])
                            .prepend($('<input/>').attr({ type: 'checkbox', id: 'IsOthPerm4', name: 'IsOthPerm4', value: true }))
                            .insertAfter($('<input/>').attr({ type: 'hidden', value: 'false', name: 'IsOthPerm4' }))
                    )
                    if ($('#_IsOthPerm4').val() == 'True') {
                        $('#IsOthPerm4').attr('checked', 'checked');
                    }
                }
                $('#FuncParamList').append('&nbsp;');
                if (data['IsOthPerm5'] == true) {
                    $('#FuncParamList').append(
                        $('<label />').html(data['OthPerm5Name'])
                            .prepend($('<input/>').attr({ type: 'checkbox', id: 'IsOthPerm5', name: 'IsOthPerm5', value: true }))
                            .insertAfter($('<input/>').attr({ type: 'hidden', value: 'false', name: 'IsOthPerm5' }))
                    )
                    if ($('#_IsOthPerm5').val() == 'True') {
                        $('#IsOthPerm5').attr('checked', 'checked');
                    }
                }
            }
        },
        Error: function (xhr) {
            Console.log(xhr);
        }
    });
}

