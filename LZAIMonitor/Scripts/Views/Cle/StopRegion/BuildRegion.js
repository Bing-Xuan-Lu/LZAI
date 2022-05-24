$(function () {
    RegionMap.initialize();
});

var TGmap = null;
var polygon = [];
var polygons = new Array();
var Markers = new Array();
var RegionMap = function () {
    var drawing = null;
    return {
        //TGOS 地圖初始化
        initialize: function () {
            //秀地圖
            TGmap = new Maps(option);
            console.log('Stop JS');
            MapIconFunc.Init();
            RegionMap.clear();
            CheckBtn();
        },
        //劃區域
        DrawiLine: function (mode) {
            var WKT = document.getElementById("RegionWKT");
            var hidenType = document.getElementById("StopRegionFile_StopType");
            var hidenRegion = document.getElementById("RegionPoints");
            WKT.value = "";
            hidenRegion.value = "";
            //判斷 儲存按鈕,清空坐標 哪個圖片該選擇
            CheckBtnClass("shape_b");
            hidenType.value = "Region";
            CheckBtn();
            RegionMap.clear();

            drawing = new Drawing(TGmap, 'polygon', {
                drawingControl: false,
                polygonOptions: {
                    fillColor: '#55ff55',
                    fillOpacity: 0.5,
                    strokeWeight: 3,
                    strokeColor: '#008844',
                    strokeOpacity: 0.5
                }
            });
            drawing.StartWithAdapter(adapter.TGPolygonToPointList, function (pointList) {
                pointList.push(pointList[0]);
                //console.log("圖", pointList);
                //console.log(JSON.stringify(pointList));
                var WKT = document.getElementById("RegionWKT");
                var hidenRegion = document.getElementById("RegionPoints");
                var WKText = "";
                var RegionText = "";
                pointList.forEach(function (item, i) {
                    if (i === 0) {
                        WKText += "POLYGON ((" + item.x + " " + item.y + ",";
                        RegionText += item.y + "," + item.x + "\r\n";
                    } else if (i === pointList.length - 1) {
                        WKText += item.x + " " + item.y + "))";
                        RegionText += item.y + "," + item.x + "\r\n";
                    } else {
                        WKText += item.x + " " + item.y + ",";
                        RegionText += item.y + "," + item.x + "\r\n";
                    }
                });
                WKT.value = WKText;
                hidenRegion.value = RegionText;

                CheckBtn();
            });
        },
        //畫點Marker
        AddMarker: function () {
            var stoppoint = document.getElementById("RegionPoints");
            stoppoint.value = "";
            var WKT = document.getElementById("RegionWKT");
            WKT.value = "";
            var hidenType = document.getElementById("StopRegionFile_StopType");
            hidenType.value = "Point";
            CheckBtnClass("placemark_b");
            RegionMap.clear();
            CheckBtn();
            TGmap.AddEvent('click', function (e) {
                var x = e.point.x;
                var y = e.point.y;
                var point = new Point({ x: x, y: y }, TGmap);
                point.ShowMarker({
                    icon: '../../images/StopRegion/blue.png',
                    IsSetCenter: false,
                    IsSetZoom: false,
                    width: 30,
                    height: 30
                });
                TGmap.ClearEvent('click');
                stoppoint.value += y + "," + x + "\r\n";
                WKT.value += "POINT(" + x + " " + y + ")";
                CheckBtn();
            });
        },
        //清除
        clear: function () {
            TGmap.ClearDrawing();
            TGmap.ClearPoint();
            TGmap.ClearLabel();
            TGmap.ClearPolygon();
            TGmap.ClearEvent('click');
        },
        //紀錄警示資訊
        Save: function () {
            InsRegion();
        }
    }
}();

//檢查儲存是否應該出現
function CheckBtn() {
    $('#BtnSave').addClass("disabled");
    var RegionPoints = document.getElementById("RegionPoints");
    if (RegionPoints.value !== '') {
        $('#BtnSave').removeClass("disabled");
    }
}

function CheckBtnClass(buttonId) {
    document.getElementById("placemark_b").className = "unselected";
    document.getElementById("shape_b").className = "unselected";
    document.getElementById(buttonId).className = "selected";
}


function InsRegion() {

    if ($('#tbxRegionName').val() === '') {
        SweetShowDialog('訊息', '請輸入警示區名稱', 'X');
        return false;
    }
    var form = $('#InsertDocForm');
    var validator = form.validate();
    if (form.valid()) {
        var data = form.serialize();
        ajax.SetData("post", "InsRegion", data, true)
            .done(function (response) {
                if (response.Status) {
                    SweetShowDialog('訊息', response.Message, 'V');
                    $('#table').bootstrapTable('refresh');
                    ClearRegion();
                    CheckBtn();
                } else if (response.ModelStateErrors !== null) {
                    validator.showErrors(response.ModelStateErrors);
                } else {
                    SweetShowDialog('訊息', response.Message, 'X');
                }
                RegionMap.clear();
                CheckBtn();
            }).fail(function (x, e) {
                SweetShowDialog('錯誤', response.Message, 'X');
            });
    }
}

function ajaxRequest(params) {
    var _form = $("#QueryForm");
    $.ajax({
        type: 'POST',
        url: _form.attr('action'),
        data: _form.serialize(),
        success: function (response) {
            params.success(response.data);
        },
        headers: {
            'RequestVerificationToken': Token
        },
        error: function (xhrInstance, status, xhrException) {
            alert("請再次查詢!!" + xhrInstance);
        }
    });
}

function operateFormatter(value, row, index) {
    var result =
        [
            `<input type="button" value="展示" class="btn btn-info"  onclick="ShowStopRegion(${row.ID},'${row.RegionName}')"/>`,
            `<input type="button" value = "刪除" class="btn btn-danger ms-sm-3" onclick ="if(SweetAlertCheck(this, \'刪除\', \'資料即將進行刪除\', \'!\')){Delete(${row.ID})} "/>`
        ].join('');

    return result;
}

//警示區展示
function ShowStopRegion(SSRID,regionName) {

    document.getElementById("placemark_b").className = "unselected";
    document.getElementById("shape_b").className = "unselected";
    RegionMap.clear();
    ajax.SetData("post", "Get_Stop_Region_Location", { StopRegionID: SSRID }, true, Token)
        .done(function (response) {
            if (response.length > 0) {
                var DrawPoints = [];
                if (response.length === 1) {
                    var obj = {
                        x: response[0].WGS_LAT
                        , y: response[0].WGS_LON
                    }
                    var point = new Point(obj, TGmap);
                    point.ShowMarker({
                        icon: '../../images/StopRegion/blue.png',
                        IsSetCenter: false,
                        IsSetZoom: false,
                        width: 30,
                        height: 30
                    });
                } else {
                    var RegionResult = response.groupBy("RegionCount");
                    $.each(RegionResult,
                        function (key, region) {
                            //個別組出一個一個Polygon
                            $.each(region,
                                function (key, item) {
                                    var point = {
                                        x: item.WGS_LAT
                                        , y: item.WGS_LON
                                    };
                                    DrawPoints.push(point);
                                });
                            var polygonOptions = {
                                fillColor: '#55ff55',
                                fillOpacity: 0.5,
                                strokeWeight: 3,
                                strokeColor: '#008844',
                                strokeOpacity: 0.5
                            }
                            var polygon = new Polygon(DrawPoints, TGmap);
                            polygon.ShowPolygon(polygonOptions);
                            polygon.AddLabel(regionName, { opacity:3});
                        });
                }
            }
        })
        .fail(function (x, e) {
            console.log(e);
        });
}

//清除警示區圖層
function ClearRegion() {
    $("#RegionWKT").val("");
    $("#RegionPoints").val("");
    $('#StopRegionFile_RegionName').val("");
    CheckBtn();
}

//刪除警示區
function Delete(id) {
    ajax.SetData("post", "DelRegion", { stopRegionID: id }, true, Token)
        .done(function (response) {
            if (response.Status) {
                SweetShowDialog('訊息', response.Message, 'V');
                $('#table').bootstrapTable('refresh');
                ClearRegion();
                CheckBtn();
            } else {
                SweetShowDialog('訊息', response.Message, 'X');
            }
            RegionMap.clear();
            CheckBtn();
        }).fail(function (x, e) {
            console.log(e);
        });
}

//定位
function LocateMap() {
    var locate = new Locate(TGmap);
    switch ($('input[name="inlineRadioOptions"]:checked').val()) {
        case '1':
            if ($('#x').val() === '' || $('#y').val() === '') {
                SweetShowDialog('錯誤', '請輸入正確的經緯度!!', 'X');
                return false;
            }
            ShowLocatePoint($('#x').val(), $('#y').val(), $('#x').val() + ',' + $('#y').val());
            $('#closeModal').click();
            break;
        case '2':
            locate.ByAddress($('#addr').val(),
                function (result) {
                    if (result.errorMessage === '') {
                        ShowLocatePoint(result.x, result.y, $('#addr').val());
                        $('#closeModal').click();
                    } else {
                        SweetShowDialog('錯誤', result.errorMessage, 'X');
                    }
                });
            break;
    }
    
}

function ShowLocatePoint(x, y, content) {
    var point = new Point({ x: x, y: y }, TGmap);
    point.ShowMarker({
        icon: '../../images/StopRegion/blue.png',
        IsSetCenter: true,
        IsSetZoom: true,
        width: 30,
        height: 30
    });
    if (content) {
        point.SetInfoWindow("<div>" + content + "</div>");
        point.AddEvent('click', function () {
            point.OpenInfoWindow();
        });
    }
}
