var ajax = function () {
    return {
        //Ajax
        SetData: function (type, url, data, booleanNum,Token = '') {
            var ajax;
            ajax =
                $.ajax({
                    type: type,        //  post get
                    url: url,          //  path
                    data: data,        //  request 參數
                    timeout: 600000,
                    async: booleanNum,
                    dataType: "Json",
                    headers: {
                        'RequestVerificationToken': Token
                    },
                    cache: false
                });

            return ajax;
        }
    }
}();


//Example Ajax GetData
function DemoData(DataSource, callback) {
    $('.loader-default').addClass('is-active');
    setTimeout(function () {
        ajax.SetData("post", "api/Data/GetExhibitionPointData", { DataSource: DataSource }, true)
            .done(function (response) {
                var LayerList = response.LayerList;
                var Legend = response.Legend;
                var Name = response.Name;
                var HighLightLegend = response.HighLightLegend;
                callback(LayerList, Name, Legend, HighLightLegend);
            })
            .fail(function(data) {
                if (data.responseCode)
                    console.log(data.responseCode);
            });
    });
}

function GetMileageName(roadClass, callback) {
    $('.loader-default').addClass('is-active');
    setTimeout(function () {
        ajax.SetData("get", "https://gist.motc.gov.tw/gist_api/V3/Map/Basic/RoadClass/" + roadClass + "?$format=JSON", "", true)
            .done(function (response) {
                console.log(response);
                callback(response);
            });
    });
}

function GetMileageLocation(roadClass, roadID, mileage, callback, ajaxErrorCallback) {
    $('.loader-default').addClass('is-active');
    setTimeout(function () {
        ajax.SetData("get", "https://gist.motc.gov.tw/gist_api/V3/Map/GeoCode/Coordinate/RoadClass/" + roadClass + "/RoadID/" + roadID + "/Mileage/" + mileage + "?$format=JSON", "", true)
            .done(function (response) {
                console.log(response);
                var result = {
                    geometry: [],
                    errorMessage: ''
                };

                if (response.length > 0) {
                    console.log(response[0].Geometry);
                    result.geometry = response[0].Geometry;
                } else {
                    result.errorMessage = '查無資料！'
                }

                callback(result);

            }).fail(function () {
                ajaxErrorCallback(arguments);
            });
    });
}