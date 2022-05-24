//事業廢棄物種類下拉選單共通
//程式相依select2.js 元件


//事業廢棄物種類(單選)
function GetWasteDataSelect2(element) {
    var myPlaceholder = '選擇廢棄物種類';
    element.select2({
        placeholder: myPlaceholder,
        language: "zh-TW",
        theme: 'bootstrap4',
        //allowClear: true
    });
    //select2:clear
    //若要打開清除功能，設定請開 allowClear: true
    element.on('select2:clearing', function (e) {
        element.val('-1').trigger('change');
    });
}

//事業廢棄物種類(多選)
function GetWasteDataMultiSelect2(element,isClear = true) {
    var myPlaceholder = '選擇廢棄物種類';
    //$('#WasteDatas').append($('<option></option>').val('-1').text('全部'));
    element.prop('multiple', true);
    //清空選擇之選項
    if (isClear)
        element.val(null).trigger('change');
    element.select2({
        allowClear: true,
        placeholder: myPlaceholder,
        language: "zh-TW",
        theme: 'bootstrap4'
    });
    //多選事件
    element.on('select2:select', function (e) {
        if (e.params.data.id === '-1') {
            element.val('-1').trigger('change');
        }
        else {
            let selectlist = new Array();
            $.each(element.select2('data'), function (index, value) {
                selectlist.push(value.id);
            });
            element.val(selectlist.filter(function (x) { return x !== '-1' })).trigger('change');
        }
    });
    //select2:clear
    element.on('select2:clearing', function (e) {
        element.val('-1').trigger('change');
    });
}

