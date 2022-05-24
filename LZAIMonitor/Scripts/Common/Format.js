function bootStrapTableDateFormat(value, row, index) {
    return moment(value).format('YYYY/MM/DD');
}

//數值欄位不可輸入數值以外的值
$(".numberOnly").on("keypress keyup blur", function (event) {
    $(this).val($(this).val().replace(/[^\d].+/, ""));
    if ((event.which < 48 || event.which > 57)) {
        event.preventDefault();
    }

});


// 日期格式正規化(為Date新增原型方法)
function DatePrototypeExpansion() {
    Date.prototype.Format = function (fmt) { //author: meizz 
        var o = {
            "M+": this.getMonth() + 1, //月份 
            "d+": this.getDate(), //日 
            "h+": this.getHours(), //小时 
            "m+": this.getMinutes(), //分 
            "s+": this.getSeconds(), //秒 
            "q+": Math.floor((this.getMonth() + 3) / 3), //季度 
            "S": this.getMilliseconds() //毫秒 
        };
        if (/(y+)/.test(fmt)) fmt = fmt.replace(RegExp.$1, (this.getFullYear() + "").substr(4 - RegExp.$1.length));
        for (var k in o)
            if (new RegExp("(" + k + ")").test(fmt)) fmt = fmt.replace(RegExp.$1, (RegExp.$1.length == 1) ? (o[k]) : (("00" + o[k]).substr(("" + o[k]).length)));
        return fmt;
    }
}


//欄位class有TP時套用千分位
function TextBoxNumberToAddComma() {
    $('.TP').each(function (i, item) {
        $(item).focus(function () {
            //當獲得focus時，要把千分位給拿掉
            $(this).val(RemoveComma($(this).val()));
            $(this).select();
        })
            .blur(function () {
                //限制輸入長度
                TextAreaLength(this, 14);

                //加千分位
                AdjustComma(this, 11);
            });
    });
}

//數字處理為有千分位
function AppendComma(n) {
    var parts = n.toString().split('.');
    parts[0] = parts[0].replace(/\B(?=(\d{3})+(?!\d))/g, ',');
    return parts.join('.');
}
//將有千分位的數值轉為一般數字
function RemoveComma(n) {
    return n.replace(/[,]+/g, '');
}
//調整千分位
function AdjustComma(item, length) {
    var originalValue = $.trim($(item).val()).length > length
        ? $.trim($(item).val()).substr(0, length)
        : $.trim($(item).val());

    $(item).val(AppendComma(originalValue));
}
//動態調整輸入欄位的長度
function TextAreaLength(item, length) {
    if (item.value.length > length) {
        item.value = item.value.substring(0, length);
    }
}