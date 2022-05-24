//驗證TW身分證格式
function CheckTwID(id) {
    
    var city = new Array(
        1, 10, 19, 28, 37, 46, 55, 64, 39, 73, 82, 2, 11,

        20, 48, 29, 38, 47, 56, 65, 74, 83, 21, 3, 12, 30
    );

    id = id.toUpperCase();    

    if (id.search(/^[A-Z](1|2)\d{8}$/i) === -1) {
     //"^[A-Z][12]\d{8}$"
        return false;

    } else {        

        id = id.split('');        

        var total = city[id[0].charCodeAt(0) - 65];

        for (var i = 1; i <= 8; i++) {

            total += eval(id[i]) * (9 - i);

        }        

        total += eval(id[9]);        

        return ((total % 10 === 0));

    }
}

//2020/08/19 新版身分證驗  by Benny　宣導測試:109/10/01~110/01/01 全面換號:110/01/02
function verifyTaiwanIdIntermediateString(IDNO) {
    var idArray = IDNO.split('');
    const intRadix = 10
    const TAIWAN_ID_LOCALE_CODE_LIST = [
        1, // A -> 10 -> 1 * 1 + 9 * 0 = 1
        10, // B -> 11 -> 1 * 1 + 9 * 1 = 10
        19, // C -> 12 -> 1 * 1 + 9 * 2 = 19
        28, // D
        37, // E
        46, // F
        55, // G
        64, // H
        39, // I -> 34 -> 1 * 3 + 9 * 4 = 39
        73, // J
        82, // K
        2, // L
        11, // M
        20, // N
        48, // O -> 35 -> 1 * 3 + 9 * 5 = 48
        29, // P
        38, // Q
        47, // R
        56, // S
        65, // T
        74, // U
        83, // V
        21, // W -> 32 -> 1 * 3 + 9 * 2 = 21
        3, // X
        12, // Y
        30 // Z -> 33 -> 1 * 3 + 9 * 3 = 30
    ]

    const RESIDENT_CERTIFICATE_NUMBER_LIST = [
        '0', // A
        '1', // B
        '2', // C
        '3', // D
        '4', // E
        '5', // F
        '6', // G
        '7', // H
        '4', // I
        '8', // J
        '9', // K
        '0', // L
        '1', // M
        '2', // N
        '5', // O
        '3', // P
        '4', // Q
        '5', // R
        '6', // S
        '7', // T
        '8', // U
        '9', // V
        '2', // W
        '0', // X
        '1', // Y
        '3' // Z
    ]

    // if is not a number (居留證編號)
    if (isNaN(parseInt(idArray[1], intRadix))) {
        idArray[1] =
            RESIDENT_CERTIFICATE_NUMBER_LIST[input.charCodeAt(1) - 'A'.charCodeAt(0)]
    }
    //var result = "";
    // idArray.forEach(function(sum,index){
    //        if (index === 0) {
    //            result += sum + TAIWAN_ID_LOCALE_CODE_LIST[idArray[0].charCodeAt(0) - 'A'.charCodeAt(0)]
                
    //        } else if (index === 9) {
    //            result += sum + parseInt(idArray[9], intRadix)
    //        }
    //    result += sum + parseInt(idArray[index], intRadix) * (9 - index)
    //    },
    //    0
    //)
    const result = idArray.reduce(function
        (sum, n, index){
            if (index === 0) {
                return (
                    sum +
                    TAIWAN_ID_LOCALE_CODE_LIST[
                    idArray[0].charCodeAt(0) - 'A'.charCodeAt(0)
                    ]
                )
            } else if (index === 9) {
                return sum + parseInt(idArray[9], intRadix)
            }
            return sum + parseInt(idArray[index], intRadix) * (9 - index)
        },
        0
    )

    if (result % 10 === 0) {
        return true
    }

    return false
}


//(簡易版)檢查身分證 20210118調整 by Benny
function CheckESIDNO(strNo) {
    var ESIDNO = /^[A-Z0-9]{10}$/;
    if (ESIDNO.test(strNo)) {
        return true;
    }
    else {
        return false;
    }
}


//檢查護照
function CheckPassport(strNo) {
    var PAS = /^[0-9]{10}$/;
    if (PAS.test(strNo)) {
        return true;
    }
    else {
        return false;
    }
}


//統編驗證
function CheckBusieseNo(strNo) {
    var BNo = /^[0-9]{8}$/;

    if (BNo.test(strNo)) {
        return true;
    }
    else {        
        return false;
    }
};  

//電子信箱
function CheckEmail(strEmail) {
    if (strEmail.search(/^\w+((-\w+)|(\.\w+))*\@[A-Za-z0-9]+((\.|-)[A-Za-z0-9]+)*\.[A-Za-z0-9]+$/) !== -1) {
        return true;
    }
    else {
        //alert("電子郵箱格式錯誤!!");
        return false;
    }
}

//手機驗證
function CheckCellPhone(strPhone) {
    var cellphone = /^09[0-9]{8}$/;

    if (cellphone.test(strPhone)) {        
        return true; 
    }
    else {
        //alert("手機號碼格式錯誤!!");
        return false; 
    }
};  


//市話驗證
function CheckTelNumber(strTel) {
    var Tel = /^(\d{2,4})-\d{6,8}#?(\d{1,6})$/;

    if (Tel.test(strTel)) {
        return true;
    }
    else {
        //alert("市話格式錯誤!!")
        return false;
    }
}

//檢查長度
function CheckLengh(str, length) {
    if (str.length < length) {
        //alert("不能小於" + length + "個字符！");
        return false;
    }
    else {
        return true;
    }
}

//檢查數字
function checkNum(str) {
    return str.match(/\D/) === null;
}    

//檢查手機&市話
function CheckPhone(Phone) {
var phone = /((^|, )(^09[0-9]{2}-[0-9]{6}$|\d{2,3}-\d{6,8}#?(\d+)?))+$/;
    if (phone.test(Phone)) {
        return true;
    }
    else {
        //alert("市話格式錯誤!!")
        return false;
    }

}