const DateInterval = Object.freeze({ "Year": 1, "Month": 2, "Day": 3 });


//回傳值為{returnMsg,returnValue}  ex  → var date = diffTime(3,2020-02-01,2020-02-10);   date.returnMsg == 0  date.returnValue == 10
function diffTime(dateInterval, date1, date2) {
    var D1 = new Date(date1);
    var D2 = new Date(date2);
    var start = D1.getTime();
    var end = D2.getTime();
    var diff = new Date(end - start);
    var returnMsg = 0;

    if (diff >= 1) {            //diff >= -1 表示起始日小於結束日(OK)
        returnMsg = 0;
    }
    else if (diff == 0) {       //diff == 0 表示起始日等於結束日(NOTOK)
        returnMsg = 1;
    }
    else if (diff < 0) {        //diff < 0 表示起始日大於結束日(NOTOK)
        returnMsg = -1;
    }
    var returnValue = 0;
    if (returnMsg == 0) {
        // get days
        var days = diff / 1000 / 60 / 60 / 24;
        switch (dateInterval) {
            case DateInterval.Year:
                returnValue = days / 365;
                break;
            case DateInterval.Month:
                returnValue = Math.floor(days / 365) * 12 + Math.round(days % 365 / 366 * 12);
                break;
            case DateInterval.Day:
                returnValue = days;
                break;
            default:
                returnValue = days;
                break;
        }
    }
    else {
        returnValue = 0;
    }
    return { returnMsg, returnValue };
}

//Mydate = 起始日期  days = 天數 →取得起始日+days後的日期
function diffdays(Mydate, days) {
    var dat = new Date(Mydate);
    dat.setDate(dat.getDate() + days); 
    return window.moment(dat).format('YYYY/MM/DD');
}

//日期轉換目前For圖台使用
function DateConvert(valueTxt) {
    var date = new Date(valueTxt);
    var VYear = date.getFullYear();
    var VMonth = date.getMonth() + 1;
    var VDate = date.getDate();
    var VHour = date.getHours();
    var VMinute = date.getMinutes();
    if (VMonth < 10) {
        VMonth = '0' + VMonth;
    }
    if (VDate < 10) {
        VDate = '0' + VDate;
    }
    return VYear + '/' + VMonth + '/' + VDate;
}
function TimeConvert(valueTxt) {
    var date = new Date(valueTxt);
    var VHour = date.getHours();
    var VMinute = date.getMinutes();
    var VSecond = date.getSeconds();
    if (VHour < 10) {
        VHour = '0' + VHour;
    }
    if (VMinute < 10) {
        VMinute = '0' + VMinute;
    }
    if (VSecond < 10) {
        VSecond = '0' + VSecond;
    }
    return VHour + ':' + VMinute + ':' + VSecond;
}
function ReplaceNull(valueTxt) {
    if (valueTxt == null || valueTxt === 'undefined')
        return "";
    else
        return valueTxt;
}

export { DateConvert, TimeConvert, ReplaceNull};