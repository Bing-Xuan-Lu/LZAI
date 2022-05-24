//縣市系列共用

//縣市切換鄉鎮連動
//綁定範例：$('select[name="FactoryData.Fac_CityNo"]').change(function () { ChangeCity($(this).attr('name'), 'FactoryData.Fac_DistrictNo'); });
function ChangeCity(inputCityName, inputDistrictName, districtplaceholder = '請選擇') {
    var cityVal = $('select[name="' + inputCityName + '"]').val();
    var cityText = $('select[name="' + inputCityName + '"] :selected').text();
    GetDistrict(cityVal, inputDistrictName, districtplaceholder);
    GetFullAddress(CityTownElementInfo);
}

//取得鄉鎮
function GetDistrict(cityVal, inputDistrictName, placeholder) {
    var apiName = 'GetDistrictList';//TODO:API先用這個名子
    $.ajax({
        url: apiName,
        data: { City: cityVal },
        type: 'post',
        cache: false,
        async: false,
        dataType: 'json',
        success: function (data) {
            if (data.length > 0) {
                $('select[name="' + inputDistrictName + '"]').empty();
                $('select[name="' + inputDistrictName + '"]').append($('<option></option>').val('').text(placeholder));
                $.each(data,
                    function(i, item) {
                        $('select[name="' + inputDistrictName + '"]')
                            .append($('<option></option>').val(item.Town_Id).text(item.Town_Name));
                    });
            } else {
                $('select[name="' + inputDistrictName + '"]').empty();
                $('select[name="' + inputDistrictName + '"]').append($('<option></option>').val('').text(placeholder));
            }
        }
    });
}

//鄉鎮切換事件
//綁定範例：$('select[name="ConsentData.ConsentDocument.CompanyDistrictNo"]').change(function () { ChangeDistrict($(this)) });
function ChangeDistrict() {
    GetFullAddress(CityTownElementInfo);
}

//存放下拉選單元素名稱
var CityTownElementInfo = class CityTownElementInfo {
    constructor(city, district, addr, fullAddress) {
        this.city = city;//縣市下拉選單元素 select name
        this.district = district;//鄉鎮下拉選單元素 select name
        this.addr = addr;//地址輸入框元素 input name
        this.fullAddress = fullAddress;//完整地址hidden元素 input id
    }
};

//取得完整地址(縣市選單+鄉鎮選單+地址=完整地址)
// 範例：CityTownElementInfo = new CityTownElementInfo('ConsentData.ConsentDocument.CompanyCityNo'
        //, 'ConsentData.ConsentDocument.CompanyDistrictNo'
        //, 'ConsentData.ConsentDocument.CompanyAddr'
        //, 'ConsentData_ConsentDocument_WasteFullAddress');
function GetFullAddress(cityElementName) {
    var fullAddress =
        $('select[name="' + cityElementName.city + '"] :selected').text() +
        $('select[name="' + cityElementName.district + '"] :selected').text() +
        $('input[name="' + cityElementName.addr + '"]').val();
    $('#' + cityElementName.fullAddress + '').val(fullAddress);
}