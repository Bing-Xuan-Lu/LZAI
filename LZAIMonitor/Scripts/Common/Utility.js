/*
 將key,value型態的資料的陣列，轉成Json{key:value}
 若是有相同的key，value會以字串相加，","為分隔
 * @param {any} aryObj
 * @param {any} k
 * @param {any} v
 */
//aryObj:[{name:"id",value:1},{name:"firstname",value:"test"}]
//k:"name" <--if not set value, default value:name
//v:"value" <--if not set value, default value:value
function KeyPairsArrayToJson(aryObj, k, v) {
    var _parameter = {};
    k = k === undefined ? "name" : k;
    v = v === undefined ? "value" : v;
    if (aryObj instanceof Array) {
        $.each(aryObj,
            function (index) {
                let val = aryObj[index];
                if (val instanceof Object) {
                    if (_parameter[val[k]] === undefined)
                        _parameter[val[k]] = val[v].toUpperCase() === "TRUE" || val[v].toUpperCase() === "FALSE" ? val[v].toUpperCase() === "TRUE" : val[v];
                    else
                        _parameter[val[k]] = _parameter[val[k]] + "," + val[v];
                }
                else
                    _parameter[index] = val;

            });
    }
    return _parameter;
}

//TextFindByValue
function BindValueFromText(text, ddlid, hiddenname, isDisabledDDl = true) {
    //對應介接資料都是中文代碼
    var options = $('#' + ddlid).children();
    $.map(options, function (e) {
        if (e.text === text) {
            $('#' + ddlid).val(e.value);
            if (hiddenname !== '')
                $('input[name="' + hiddenname + '"]').val(e.value);
        }
    });
    if ($('#' + ddlid).val() !== '' && isDisabledDDl)
        $('#' + ddlid).attr('disabled', 'disabled');
}

//區之前的文字都清除掉
function ReplaceDirect(addr) {
    const paragraph = addr;
    const searchTerm = '區';
    const indexOfFirst = paragraph.lastIndexOf(searchTerm);
    //TODO:如果路名是「高雄市前鎮區環區一路」這種路名也會被取代掉
    var resultDirect = paragraph.substring(indexOfFirst + 1);
    return resultDirect;
}

//re-usable method
// ReSharper disable once NativeTypePrototypeExtending
Array.prototype.groupBy = function (prop) {
    return this.reduce(function (groups, item) {
            var val = item[prop];
            groups[val] = groups[val] || [];
            groups[val].push(item);
            return groups;
        },
        {});
};