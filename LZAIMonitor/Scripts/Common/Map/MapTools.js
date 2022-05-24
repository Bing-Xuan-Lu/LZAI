//圖台小工具依賴Map.js

//TODO:客製化地圖時Js必須Override
var option = {
    id: "#map",
    mapType: "TGOS",
    zoom: 11,
    zoomRange: { min: 7, max: 19 },
    center: { x: 121.83521871432991, y: 24.660101861148192 },
    coordinate: "EPSG3857"
};

var LogicalJudgment = function () {
    return {
        IsTWD97X: function (x) {
            var reg = new RegExp(/^(\d{6})+(\.\d{0,})?$/);
            return reg.test(x);
        },
        IsTWD97Y: function (x) {
            var reg = new RegExp(/^(\d{7})+(\.\d{0,})?$/);
            return reg.test(x);
        },
        IsWGS84X: function (x) {
            return Boolean(x < 180 && x > -180);
        },
        IsWGS84Y: function (x) {
            return Boolean(x < 90 && x > -90);
        },
        IsWGS84X_Taiwan: function (x) {
            return Boolean(x < 123 && x > 118);
        },
        IsWGS84Y_Taiwan: function (x) {
            return Boolean(x < 27 && x > 21);
        },
        IsContainChinese: function (str) {
            return escape(str).indexOf("%u") >= 0;
        },
        IsFactoryNo: function (x) {
            //var reg = new RegExp(/^[A-Za-z]{1}\d{7}$/);
            var reg = new RegExp(/^[A-Za-z]{1}.{7}$/);
            return reg.test(x);
        }
    }
}();
var adapter = {  // 配接器，轉換不同型式介面
    TGLineToPointList: function (e) {  // TGOS.TGLine 轉為 pointList
        console.log(e.overlay.getPath());
        var pointList = e.overlay.getPath().path.map(function (item) {
            return {
                x: item.x,
                y: item.y
            }
        });

        return pointList;
    },
    TGPolygonToPointList: function (e) {  // TGOS.TGPolygon 轉為 pointList
        console.log(e.overlay.getPath().rings_[0].linestring);
        var pointList = e.overlay.getPath().rings_[0].linestring.path.map(function (item) {
            return {
                x: item.x,
                y: item.y
            }
        });

        return pointList;
    },
    TGCircleToObject: function (e) {
        var tgCircle = e.overlay.getPath();
        return {
            x: tgCircle.getCenter().x,
            y: tgCircle.getCenter().y,
            radius: tgCircle.getRadius()
        };
    }
};


//地圖小工具
//TODO:綁定class
var MapIconFunc = function () {
    return {
        Init: function () {
            //放大
            $('.increase_icon').click(function () {
                TGmap.ZoomIn();
            })
            //縮小
            $('.minify_icon').click(function () {
                TGmap.ZoomOut();
            })
            //重整
            $('.reload_icon').click(function () {
                window.location.reload();
            });
            //地圖重整
            $('.map_reset_icon').click(function () {
                MapIconFunc.ClearAllPointsOnMap();
            });
            //測量距離
            $('.distance_btn').click(function () {
                MapIconFunc.DrawDistance();
            });
        },
        ClearAllPointsOnMap: function () {
            //清除圖層
            TGmap.ClearPoint();
            TGmap.ClearEvent('click');
            TGmap.ClearDrawing();
            TGmap.ClearMultiPoint();
            TGmap.ClearCircle();
            TGmap.ClearPolygon();
            TGmap.ClearWMSLayer();
            TGmap.ClearClusterPoint();
        },
        DrawDistance: function () {
            // 畫距離 & 測量距離
            var drawing = new Drawing(map,
                'linestring',
                {
                    drawingControl: false,
                    polylineOptions: {
                        strokeWeight: 5,
                        strokeColor: '#00ff00',
                        strokeOpacity: 0.5
                    }
                });
            drawing.StartWithAdapter(adapter.TGLineToPointList,
                function (pointList) {
                    var measure = TGmap.MeasureDistance(pointList);
                    //console.log(pointList);

                    $('.distance >p').text(measure.distance.toFixed(3) + "公尺");
                });
        },
        DrawArea: function () {
            // 畫面積 & 測量面積、周長
            var drawing = new Drawing(map,
                'polygon',
                {
                    drawingControl: false,
                    polygonOptions: {
                        fillColor: '#55ff55',
                        fillOpacity: 0.5,
                        strokeWeight: 3,
                        strokeColor: '#008844',
                        strokeOpacity: 0.5
                    }
                });
            drawing.StartWithAdapter(adapter.TGPolygonToPointList,
                function (pointList) {
                    var measureArea = TGmap.MeasureArea(pointList);
                    var measureDistance;
                    //console.log(pointList);

                    if (pointList.length >= 3) { // 如果只是畫一條線時有錯，所以點有三個時再push第一個點
                        pointList.push(pointList[0]);
                    }
                    measureDistance = TGmap.MeasureDistance(pointList);

                    $('.area > p').text(measureArea.area.toFixed(3) + "平方公尺");
                    $('.length > p').text(measureDistance.distance.toFixed(3) + "公尺");
                });
        }
    }
}();

//滑鼠座標匯入事件
function pointEvent(TGmap) {
    TGmap.AddEvent('mousemove', function (data) {
        //console.log(arguments);
        // console.log(arguments.point)

        var wgs84X, wgs84Y;
        wgs84X = data.point.x.toFixed(3);
        wgs84Y = data.point.y.toFixed(3);

        $(".WGS84_x").html(wgs84X);
        $(".WGS84_y").html(wgs84Y);

        var point = new Point({}, TGmap);
        var twd97X, twd97Y;
        twd97X = point.TransCoordinate('wgs84totwd97', { x: wgs84X, y: wgs84Y }).x.toFixed(3);
        twd97Y = point.TransCoordinate('wgs84totwd97', { x: wgs84X, y: wgs84Y }).y.toFixed(3);
        $(".TWD97_x").html(twd97X);
        $(".TWD97_y").html(twd97Y);
    })

}